using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.CompanyUserRegister
{
    public interface IPKG_Managment
    {
        IActionResult RegisterCompany(registerDTO employee);

        IActionResult LoginCompany(loginDTO login);

        IActionResult RegisterManager(RegisterEmployeeDTO manager);

        IActionResult GetAllUsers(int? CompanyId);

        IActionResult GetAllUsersID(int? userId);

        bool DeleteUser(int userId);

        bool UpdateUser(UserDTO user);

        IActionResult AddStore(StoreDTO store);
        List<StoreDTO> GetStoresByManagerID(int managerID);

        IEnumerable<StoreGetDTO> GetAllStoresWithNullOperatorID();

        bool AddProduct(AddProductDTO dto);

        bool DeleteProductById(int productId);

        bool UpdateProduct(UpdateProductDTO updateProductDto);

        bool UpdateProductSell(UpdateProductDTO updateProductDto);

        GetStoreDto GetStoreDto(int storeId, int storeOperatorId);

        GetStoreIdDto GetstoreByOperatorID(int storeOperatorId);
        GetstoreProductMeneger getproductdtomanager(int storeId);


    }
}
