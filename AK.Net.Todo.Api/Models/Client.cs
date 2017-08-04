using System.ComponentModel.DataAnnotations;
using System;

namespace AK.Net.Todo.Api.Models
{
    public class Client
    {
        [Key]
        public string Id { get; set; }

        //[Required]
        public string ClientId { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ApplicationType ApplicationType { get; set; }

        public bool Active { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        [MaxLength(100)]
        [Required]
        public string AllowedOrigin { get; set; }

        public OAuthGrant AllowedGrant { get; set; }

        public string CreatedOn { get; set; }
    }
    public enum ApplicationType
    {
        JavaScript = 0,
        NativeConfidential = 1,
        Mvc = 2
    };
    public enum OAuthGrant
    {
        Code = 1,
        Implicit = 2,
        ResourceOwner = 3,
        Client = 4
    }
}