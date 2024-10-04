namespace WarehouseManagementAPI.Models
{
    public class AddProductDTO
    {
        public int OperatorId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string DateOfEntry { get; set; }
        public int StoreId { get; set; }
    }
}
