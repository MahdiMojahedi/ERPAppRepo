using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
namespace IDP.Entities
{

    public class ApplicationUser : IdentityUser<int>
    {   
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set;}
        public int Age { get; set; }
        public int NationalID { get; set; }

        public ICollection<Master> CreatorMaster { get; set; }
        public ICollection<Subsidiary> CreatorSubsidiary { get; set; }
    }
    
}
