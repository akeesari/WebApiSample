using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using AK.Net.Todo.Api.Models;
using AK.Net.Todo.Api;

namespace Axa.Ppp.Dha.Api.Providers
{
    public class TodoAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
       
        public override async Task ValidateClientAuthentication( OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            var clientOrigin = context.Parameters.Get("origin");
            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                var dbContext = context.OwinContext.Get<ApplicationDbContext>();

                try
                {
                    var client = await dbContext
                        .Client
                        .FirstOrDefaultAsync(clientEntity => clientEntity.ClientId == clientId);
                    //Check if the client is registered
                    if (client == null)
                    {
                        context.SetError("invalid_client", string.Format("Client '{0}' is not registered in the system.", clientId));
                        context.Rejected();
                    }
                    if (client != null && !client.Active)
                    {
                        context.SetError("invalid_clientId", "Client is inactive.");
                        context.Rejected();
                    }
                    if (client != null && !string.Equals(client.AllowedOrigin, clientOrigin, StringComparison.CurrentCultureIgnoreCase))
                    {
                        context.SetError("invalid_clientId", "Client Origin is not valid.");
                        context.Rejected();
                    }
                    if (client != null &&
                        userManager.PasswordHasher.VerifyHashedPassword(
                            client.Secret, clientSecret) == PasswordVerificationResult.Success)
                    {
                        // Client has been verified.
                        context.OwinContext.Set<Client>("oauth:client", client);
                        context.Validated(clientId);
                    }
                    else
                    {
                        // Client could not be validated.
                        context.SetError("invalid_client", "Client credentials are invalid.");
                        context.Rejected();
                    }
                }
                catch
                {
                    // Could not get the client through the IClientManager implementation.
                    context.SetError("server_error");
                    context.Rejected();
                }
            }
            else
            {
                // The client credentials could not be retrieved.
                context.SetError(
                    "invalid_client",
                    "Client credentials could not be retrieved through the Authorization header.");

                context.Rejected();
            }
        }
        

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var client = context.OwinContext.Get<Client>("oauth:client");

            if (client.AllowedGrant == 0) //OAuthGrant.ResourceOwner)
            {
                // Client flow matches the requested flow. Continue...
                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

                ApplicationUser user;
                try
                {
                    user = await userManager.FindByNameAsync(context.UserName);
                }
                catch
                {
                    // Could not retrieve the user.
                    context.SetError("server_error: Could not retrieve the user.");
                    context.Rejected();
                    // Return here so that we don't process further. Not ideal but needed to be done here.
                    return;
                }
                // if new customer dhaUser is null then create new DhaUser profile before launching
                if (user == null)
                {
                    context.SetError("invalid_user: User not registered with API");
                    context.Rejected();
                }
              
                //var applicationUser = await userManager.FindAsync(dhaProfile.DhaUserName, dhaProfile.DhaUserPwd);
                var properties = CreateProperties(user);

                // User is found. Signal this by calling context.Validated
                var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
                var ticket = new AuthenticationTicket(identity, properties);
                context.Validated(ticket);

            }
            else
            {
                // Client is not allowed for the 'Resource Owner Password Credentials Grant'.
                context.SetError(
                    "invalid_grant",
                    "Client is not allowed for the 'Resource Owner Password Credentials Grant'");

                context.Rejected();
            }
        }
        //public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        //{
        //    if (context.ClientId == _publicClientId)
        //    {
        //        Uri expectedRootUri = new Uri(context.Request.Uri, "/");

        //        if (expectedRootUri.AbsoluteUri == context.RedirectUri)
        //        {
        //            context.Validated();
        //        }
        //    }

        //    return Task.FromResult<object>(null);
        //}
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        #region "Private Method"
        private static AuthenticationProperties CreateProperties(ApplicationUser user)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "UserName", user.UserName},             
            };
            return new AuthenticationProperties(data);
        }
        #endregion

    }
}