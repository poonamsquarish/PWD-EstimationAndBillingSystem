using System.ComponentModel.DataAnnotations;

namespace SQEstimationAndBillingSystem.Models
{
    public class UserModel
    {
    
        public long ID { get; set; }
        [Required]
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        [Required]
        public string Password { get; set; }
        public string Message { get; set; }
     
        public string FirstName { get; set; }
       
        public string MiddleName { get; set; }
      
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool RememberMe { get; set; }
        public bool IsEdit { get; set; }


    }
}