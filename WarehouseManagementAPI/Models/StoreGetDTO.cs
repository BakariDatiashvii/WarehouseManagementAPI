namespace WarehouseManagementAPI.Models
{
    public class StoreGetDTO
    {
        public int ID { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public int StoreManagerId { get; set; }
    }
}
