namespace WarehouseManagementAPI.Models
{
    public class GetproductDto
    {
        public int Id { get; set; }
  
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string DateOfEntry { get; set; }
        public int StoreId { get; set; }

        public int ProductSellId { get; set; }


    }
}
