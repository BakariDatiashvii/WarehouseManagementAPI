using Microsoft.AspNetCore.Mvc;
using WarehouseManagementAPI.CompanyUserRegister;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IPKG_Managment _pkgService;

        public EmployeeController(IPKG_Managment pkgService)
        {
            _pkgService = pkgService;
        }



        [HttpPost("create")]
        public IActionResult AddEmployee([FromBody] registerDTO employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            return _pkgService.RegisterCompany(employee);
        }


        [HttpPost("login")]
        public IActionResult LoginCompany([FromBody] loginDTO login)
        {
            if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Username and password are required.");
            }

            // Extract username and password from loginDTO


            // Call the LoginCompany method with the extracted parameters
            var result = _pkgService.LoginCompany(login);

            return result;
        }

        [HttpPost("register-managerandoperator")]
        public IActionResult RegisterManager([FromBody] RegisterEmployeeDTO manager)
        {
            if (manager == null)
            {
                return BadRequest("Manager data is required.");
            }

            // Call RegisterManager method from the service
            var result = _pkgService.RegisterManager(manager);
            return result;
        }

        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers([FromQuery] int? CompanyId)
        {
            // Call GetAllUsers method from the service
            var result = _pkgService.GetAllUsers(CompanyId);
            return result;
        }


        [HttpGet("get-user-by-id")]
        public IActionResult GetAllUsersID([FromQuery] int? userId)
        {
            // Call GetAllUsersID method from the service
            var result = _pkgService.GetAllUsersID(userId);
            return result;
        }




        [HttpDelete("delete/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            bool isDeleted = _pkgService.DeleteUser(userId);
            if (isDeleted)
            {
                return Ok("User deleted successfully.");
            }
            return StatusCode(500, "An error occurred while deleting the user.");
        }

        [HttpPost("update-user")]
  
        public IActionResult UpdateUser([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            try
            {
                bool isUpdated = _pkgService.UpdateUser(user);
                if (isUpdated)
                {
                    
                }
                return Ok ("User updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred: {ex.Message}");
            }
        }


        [HttpPost("add-store")]
        public IActionResult AddStore([FromBody] StoreDTO store)
        {
            

            // Call the AddStore method from the service
            var result = _pkgService.AddStore(store);
            return result;
        }



        [HttpGet("get-all-stores/{managerID}")]
        public IActionResult GetAllStores(int managerID)
        {
            if (managerID <= 0)
            {
                return BadRequest("Invalid manager ID.");
            }

            try
            {
                // Call the service method to fetch stores by managerID
                var stores = _pkgService.GetStoresByManagerID(managerID);

                if (stores == null || stores.Count == 0)
                {
                    return NotFound("No stores found for the given manager ID.");
                }

                return Ok(stores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error occurred: {ex.Message}");
            }
        }

        [HttpGet("carielisawyobebi-operatorisregistraciistvis")]
        public ActionResult<IEnumerable<StoreGetDTO>> GetStoresWithNullOperatorID()
        {
            var stores = _pkgService.GetAllStoresWithNullOperatorID();
            return Ok(stores);
        }


        [HttpPost("add-product-opretor")]
        public IActionResult AddProduct([FromBody] AddProductDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid product data.");
            }

            bool isSuccess = _pkgService.AddProduct(dto);

            if (isSuccess)
            {
                return Ok("Product added successfully.");
            }
            else
            {
                return StatusCode(500, "Failed to add product.");
            }
        }


        [HttpDelete("product{id}")]
        public  IActionResult DeleteProductById(int id)
        {
            bool isDeleted =  _pkgService.DeleteProductById(id);

            
            
                return Ok(new { message = "Product deleted successfully." });
            
            
        }


        [HttpPut("update-product")]

        public IActionResult UpdateProduct([FromBody] UpdateProductDTO updateProductDto)
        {
            bool isDeleted = _pkgService.UpdateProduct(updateProductDto);

            return Ok(new { message = "Product update successfully." });
        }

        [HttpPut("update-productSell")]

        public IActionResult UpdateProductSell(UpdateProductDTO updateProductDto)
        {
            bool isDeleted = _pkgService.UpdateProductSell(updateProductDto);

            return Ok(new { message = "Product update successfully." });
        }


        [HttpGet("operatorgetall/{storeId}/{storeOperatorId}")]
        public IActionResult GetStore(int storeId, int storeOperatorId)
        {
            var X = _pkgService.GetStoreDto(storeId, storeOperatorId);

            return Ok(X);
        }


        [HttpGet("operatorproduct/{storeOperatorId}")]
        public ActionResult<GetStoreIdDto> GetstoreByOperatorID(int storeOperatorId)
        {

            var X = _pkgService.GetstoreByOperatorID(storeOperatorId);
            return Ok(X);


            
        }

        [HttpGet("managergetall/{storeId}")]
        public ActionResult<GetstoreProductMeneger> GetsoreProductManager(int storeId)
        {
            var X = _pkgService.getproductdtomanager(storeId);

            return Ok(X);
        }



    }



}


