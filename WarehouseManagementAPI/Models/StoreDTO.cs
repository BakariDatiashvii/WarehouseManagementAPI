namespace WarehouseManagementAPI.Models
{
    public class StoreDTO
    {
        public int Id { get; set; } 
        public int managerID { get; set; } // Make sure this is an integer
        public string storename { get; set; } // String for the store name
        public string storeadress { get; set; } // String for the store address

       
    }

}
