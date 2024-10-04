namespace WarehouseManagementAPI.Models
{
    public class GetAllUserDTO
    {

        //public int Id { get; set; }
        public int UserId { get; set; }
        public int? CompanyId { get; set; }
        public int? ManagerId { get; set; }
        public int? OperatorId { get; set; }
        public string NameEmployee { get; set; }
        public string LastEmployee { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
   
        public string PhoneNumber { get; set; }


        public role Role { get; set; }
    }
}
