namespace WarehouseManagementAPI.Models
{
    public class GetStoreIdDto
    {
        public int Id { get; set; }

        public int storeOperatorId { get; set; }
        public string storeadress { get; set; } // String for the store name
        public string storename { get; set; }

    }
}
