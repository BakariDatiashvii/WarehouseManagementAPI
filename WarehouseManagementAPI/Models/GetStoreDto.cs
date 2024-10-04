namespace WarehouseManagementAPI.Models
{
    public class GetStoreDto
    {
        public int  Id { get; set; }  

        public int STOREOPERATORid { get; set; }
        public string storename { get; set; } // String for the store name
        public string storeadress { get; set; }

        

        public List<GetproductDto> getproductDtos { get; set; }
        public List<GetproductSellDto> getproductSellDtos { get; set; }
         public List<GetproductdtNashtiDto> getproductdtNashtiDtos { get; set; }



    }
}
