using System.Security.Claims;
using System.Security.Principal;

namespace SmartShop.UI.Models
{
    public class GoogleUser
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string GivenName { get; private set; }
        public string SurName { get; private set; }
        public string Email { get; private set; }
        public string Picture { get; private set; }

        internal static GoogleUser Create(IIdentity? identity)
        {
            if(identity == null) throw new ArgumentNullException(nameof(identity));

            var claimsIdentity = (ClaimsIdentity)identity;
            var user = new GoogleUser()
            {
                Id = claimsIdentity.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Name = claimsIdentity.Claims.Single(x => x.Type == ClaimTypes.Name).Value,
                GivenName = claimsIdentity.Claims.Single(x => x.Type == ClaimTypes.GivenName).Value,
                SurName = claimsIdentity.Claims.Single(x => x.Type == ClaimTypes.Surname).Value,
                Email = claimsIdentity.Claims.Single(x => x.Type == ClaimTypes.Email).Value,
                Picture = claimsIdentity.Claims.Single(x => x.Type == "image").Value
            };

            return user;
        }

    }
}
