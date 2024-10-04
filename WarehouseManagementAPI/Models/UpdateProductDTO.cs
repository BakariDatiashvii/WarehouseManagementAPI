namespace WarehouseManagementAPI.Models
{
    public class UpdateProductDTO
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string DateOfEntry { get; set; }
    }
}
