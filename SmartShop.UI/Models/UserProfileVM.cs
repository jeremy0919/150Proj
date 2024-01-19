using Models;
using System;

namespace SmartShop.UI.Models
{
    // Extension of userprofile to add some properties that will make using the drop-down lists easier
    public class UserProfileVM : UserProfile
    {
        public List<Allergies> Allergies { get; set; } = new List<Allergies>();
    }
}
