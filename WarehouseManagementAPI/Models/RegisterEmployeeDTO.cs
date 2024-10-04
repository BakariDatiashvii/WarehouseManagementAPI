namespace WarehouseManagementAPI.Models
{
    public class RegisterEmployeeDTO
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
      
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public int? companyid { get; set; }
        public int? sawyobiID { get; set; }

        public role role { get; set; }




       
    }
}
