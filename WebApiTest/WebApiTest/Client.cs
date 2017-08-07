using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

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
        AngularJS = 2,
        NativeConfidential = 3,
        Mvc = 4,
        Console = 5
    };
    public enum OAuthGrant
    {
        Code = 1,
        Implicit = 2,
        ResourceOwner = 3,
        Client = 4
    }
}
