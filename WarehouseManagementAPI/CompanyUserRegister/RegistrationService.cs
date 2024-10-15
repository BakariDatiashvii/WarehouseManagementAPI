using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementAPI.Models;
using WarehouseManagementAPI.Services;
using conectionm.packages;
using Microsoft.AspNetCore.Http;
using System.Net;
using Oracle.ManagedDataAccess.Types;

namespace WarehouseManagementAPI.CompanyUserRegister
{
    public class PKG_EMPLOYEE_BD : PKG_BASE, IPKG_Managment
    {
        private readonly IAuthorizedUserService _tokenService;

        public PKG_EMPLOYEE_BD(IConfiguration configuration, IAuthorizedUserService tokenService) : base(configuration)
        {
            _tokenService = tokenService;
        }

        public IActionResult RegisterCompany(registerDTO employee)
        {
            using (OracleConnection con = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.RegisterCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = employee.Username;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = employee.Password;
                    cmd.Parameters.Add("p_nameEmployee", OracleDbType.Varchar2).Value = employee.NameEmployee;
                    cmd.Parameters.Add("p_lastEmployee", OracleDbType.Varchar2).Value = employee.lastEmployee;
                    cmd.Parameters.Add("p_nameOrganization", OracleDbType.Varchar2).Value = employee.NameOrganization;
                    cmd.Parameters.Add("p_organizationAddress", OracleDbType.Varchar2).Value = employee.OrganizationAddress;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = employee.Email;
                    cmd.Parameters.Add("p_phoneNumber", OracleDbType.Varchar2).Value = employee.PhoneNumber;
                    cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = (int)employee.role;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return new OkResult();
                }
            }





        }

        public List<StoreDTO> GetStoresByManagerID(int managerID)
        {
            List<StoreDTO> stores = new List<StoreDTO>();

            using (var conn = new OracleConnection(Connstr))
            {
                conn.Open();
                using (var cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.GetStoreByManagerID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_managerID", OracleDbType.Int32).Value = managerID;
                    cmd.Parameters.Add("p_stores_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stores.Add(new StoreDTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                managerID = reader.GetInt32(reader.GetOrdinal("STOREMANAGERID")),
                                
                                storename = reader.GetString(reader.GetOrdinal("STORENAME")),
                                storeadress = reader.GetString(reader.GetOrdinal("STOREADRESS"))
                            });
                        }
                    }
                }
            }

            return stores;
        }





        public IActionResult RegisterManager(RegisterEmployeeDTO manager)
        {


            if ((int)manager.role == 1)
            {




                using (OracleConnection con = new OracleConnection(Connstr))
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.RegisterManager", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = manager.UserName;
                        cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = manager.Password;
                        cmd.Parameters.Add("p_nameEmployee", OracleDbType.Varchar2).Value = manager.FirsName;
                        cmd.Parameters.Add("p_lastEmployee", OracleDbType.Varchar2).Value = manager.LastName;
                        cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = manager.Email;
                        cmd.Parameters.Add("p_phoneNumber", OracleDbType.Varchar2).Value = manager.PhoneNumber;
                        cmd.Parameters.Add("p_companyid", OracleDbType.Decimal).Value = (int)manager.companyid; // Example manager ID, adjust as needed
                        cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = (int)manager.role;

                        con.Open();
                        cmd.ExecuteNonQuery();

                        return new OkResult();
                    }
                }
            }

            else if ((int)manager.role == 2)
            {
                using (OracleConnection con = new OracleConnection(Connstr))
                {
                    using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.RegisterOperator", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = manager.UserName;
                        cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = manager.Password;
                        cmd.Parameters.Add("p_nameEmployee", OracleDbType.Varchar2).Value = manager.FirsName;
                        cmd.Parameters.Add("p_lastEmployee", OracleDbType.Varchar2).Value = manager.LastName;
                        cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = manager.Email;
                        cmd.Parameters.Add("p_phoneNumber", OracleDbType.Varchar2).Value = manager.PhoneNumber;
                        cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = (int)manager.role;
                        cmd.Parameters.Add("p_companyid", OracleDbType.Int32).Value = (int)manager.companyid;
                        cmd.Parameters.Add("p_sawyobiID", OracleDbType.Int32).Value = (int)manager.sawyobiID; 
                        // Open connection and execute the stored procedure
                        con.Open();
                        cmd.ExecuteNonQuery();

                        return new OkResult();
                    }
                }

            }
            return new OkResult();
        }






        public IActionResult LoginCompany(loginDTO login)
        {
            using (OracleConnection con = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.LoginCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Define input parameters
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = login.Username;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = login.Password;

                    // Define output parameters
                    var userIdParam = new OracleParameter("p_user_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var roleParam = new OracleParameter("p_role", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var managerIdParam = new OracleParameter("p_manager_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var operatorIdParam = new OracleParameter("p_operator_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var companyIDParm = new OracleParameter("p_company_id", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };


                    // Add parameters to command
                    cmd.Parameters.Add(userIdParam);
                    cmd.Parameters.Add(roleParam);
                    cmd.Parameters.Add(managerIdParam);
                    cmd.Parameters.Add(operatorIdParam);
                    cmd.Parameters.Add(companyIDParm);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Helper function to safely convert output parameters
                    int SafeGetInt32(OracleParameter param)
                    {
                        int result;
                        // Check if the parameter value is not null and can be converted to an integer
                        if (param.Value != DBNull.Value && int.TryParse(param.Value.ToString(), out result))
                        {
                            return result;
                        }
                        return 0;
                    }

                    // Get output parameters using the helper function
                    int userId = SafeGetInt32(userIdParam);
                    int role = SafeGetInt32(roleParam);
                    int managerId = SafeGetInt32(managerIdParam);
                    int operatorId = SafeGetInt32(operatorIdParam);

                    int CompanyID = SafeGetInt32(companyIDParm);

                    // Retrieve user details for token generation
                    var user = new User
                    {
                        Id = userId,
                        Email = login.Username, // Assuming username is the email
                        role = (role)role,
                        managerID = managerId,
                        operatorID = operatorId,
                        CompanyID = CompanyID,
                        
                    };

                    // Generate JWT token
                    var token = _tokenService.GenerateToken(user);

                    // Return token and user details
                    return new OkObjectResult(new { Token = token });
                }
            }
        }



        public IActionResult GetAllUsers(int? CompanyId)
        {
            using (OracleConnection con = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.GetAllUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Define the parameters
                    cmd.Parameters.Add("p_companyId", OracleDbType.Int32).Value = (object)CompanyId ?? DBNull.Value;

                    // Define the output cursor parameter
                    OracleParameter p_users = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                    p_users.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(p_users);

                    con.Open();

                    // Execute the command and retrieve the data using a data reader
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        List<GetAllUserDTO> users = new List<GetAllUserDTO>();

                        while (reader.Read())
                        {
                            users.Add(new GetAllUserDTO
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Email = reader["email"].ToString(),
                                NameEmployee = reader["NameEmployee"].ToString(),
                                LastEmployee = reader["LastEmployee"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Role = (role)Convert.ToInt32(reader["Role"]),
                                CompanyId = reader["CompanyId"] != DBNull.Value ? Convert.ToInt32(reader["CompanyId"]) : (int?)null,
                                ManagerId = reader["ManagerId"] != DBNull.Value ? Convert.ToInt32(reader["ManagerId"]) : (int?)null,
                                OperatorId = reader["OperatorId"] != DBNull.Value ? Convert.ToInt32(reader["OperatorId"]) : (int?)null,
                                CompanyName = reader["CompanyName"].ToString()
                            });
                        }

                        // Return the list of users as a JSON response
                        return new OkObjectResult(users);
                    }
                }
            }
        }



        public IActionResult GetAllUsersID(int? userId)
        {
            using (OracleConnection con = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.GetAllUsersID", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Define the parameters
                    cmd.Parameters.Add("p_userId", OracleDbType.Int32).Value = (object)userId ?? DBNull.Value;

                    // Define the output cursor parameter
                    OracleParameter p_users = new OracleParameter("p_cursor", OracleDbType.RefCursor);
                    p_users.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(p_users);

                    con.Open();

                    // Execute the command and retrieve the data using a data reader
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        List<GetAllUserDTO> users = new List<GetAllUserDTO>();

                        while (reader.Read())
                        {
                            users.Add(new GetAllUserDTO
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Email = reader["email"].ToString(),
                                NameEmployee = reader["NameEmployee"].ToString(),
                                LastEmployee = reader["LastEmployee"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Role = (role)Convert.ToInt32(reader["Role"]),
                                ManagerId = reader["ManagerId"] != DBNull.Value ? Convert.ToInt32(reader["ManagerId"]) : (int?)null,
                                OperatorId = reader["OperatorId"] != DBNull.Value ? Convert.ToInt32(reader["OperatorId"]) : (int?)null,
                                CompanyName = reader["CompanyName"].ToString()
                            });
                        }

                        // Return the list of users as a JSON response
                        return new OkObjectResult(users);
                    }
                }
            }
        }




        // New DeleteUser method
        public bool DeleteUser(int userId)
        {
            using (OracleConnection con = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.DeleteUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_userId", OracleDbType.Int32).Value = userId;

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return true; // Return true if the deletion was successful
                    }
                    catch (OracleException ex)
                    {
                        // Handle Oracle exceptions (e.g., log the error)
                        Console.WriteLine($"Oracle error code: {ex.Number}, Message: {ex.Message}");
                        return false; // Return false if there was an Oracle-specific error
                    }
                    catch (Exception ex)
                    {
                        // Handle other exceptions
                        Console.WriteLine($"General error: {ex.Message}");
                        return false; // Return false if there was a general error
                    }
                }
            }
        }



        public bool UpdateUser(UserDTO user)
        {
            using (OracleConnection con = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.UpdateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_userId", OracleDbType.Int32).Value = user.UserId;
                   
                    cmd.Parameters.Add("p_nameEmployee", OracleDbType.Varchar2).Value = user.NameEmployee;
                    cmd.Parameters.Add("p_lastEmployee", OracleDbType.Varchar2).Value = user.LastEmployee;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = user.Email;
                    cmd.Parameters.Add("p_phoneNumber", OracleDbType.Varchar2).Value = user.PhoneNumber;
                    // cmd.Parameters.Add("p_role", OracleDbType.Int32).Value = (int)user.Role;

                    try
                    {
                        con.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return true; // Return true if the update was successful
                        }
                        else
                        {
                            return false; // Return false if the user was not found or no changes were made
                        }
                    }
                    catch (OracleException ex)
                    {
                        // Handle Oracle exceptions (e.g., log the error)
                        Console.WriteLine($"Oracle error code: {ex.Number}, Message: {ex.Message}");
                        return false; // Return false if there was an Oracle-specific error
                    }
                    catch (Exception ex)
                    {
                        // Handle other exceptions
                        Console.WriteLine($"General error: {ex.Message}");
                        return false; // Return false if there was a general error
                    }
                }
            }
        }



        public IActionResult AddStore(StoreDTO store)
        {
            using (OracleConnection con = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.AddStore", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameters for the procedure
                    cmd.Parameters.Add("p_managerID", OracleDbType.Int32).Value = store.managerID;
                    cmd.Parameters.Add("p_storeName", OracleDbType.Varchar2).Value = store.storename;
                    cmd.Parameters.Add("p_storeAddress", OracleDbType.Varchar2).Value = store.storeadress;

                    try
                    {
                        // Open the connection
                        con.Open();

                        // Execute the stored procedure
                        cmd.ExecuteNonQuery();

                        // Return success result
                        return new OkResult();
                    }
                    catch (OracleException ex)
                    {
                        // Handle Oracle exceptions (e.g., log the error)
                        Console.WriteLine($"Oracle error code: {ex.Number}, Message: {ex.Message}");
                        return new StatusCodeResult(500); // Return an error status code
                    }
                    catch (Exception ex)
                    {
                        // Handle other exceptions
                        Console.WriteLine($"General error: {ex.Message}");
                        return new StatusCodeResult(500); // Return an error status code
                    }
                }
            }
        }


        public IEnumerable<StoreGetDTO> GetAllStoresWithNullOperatorID()
        {
            var stores = new List<StoreGetDTO>();

            using (var connection = new OracleConnection(Connstr))
            {
                using (var command = new OracleCommand("PKG_BAKARI_USER_REGISTER.GetAllStoresWithNullOperatorID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stores.Add(new StoreGetDTO
                            {
                                ID = reader.GetInt32(0),
                                StoreName = reader.GetString(1),
                                StoreAddress = reader.GetString(2),
                                StoreManagerId = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }

            return stores;
        }


        public bool AddProduct(AddProductDTO dto)
        {
            using (var connection = new OracleConnection(Connstr))
            {
                using (var command = new OracleCommand("PKG_BAKARI_USER_REGISTER.ADD_PRODUCT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add("p_operatorid", OracleDbType.Int32).Value = dto.OperatorId;
                    command.Parameters.Add("p_barcode", OracleDbType.Varchar2).Value = dto.Barcode;
                    command.Parameters.Add("p_productname", OracleDbType.Varchar2).Value = dto.ProductName;
                    command.Parameters.Add("p_quantity", OracleDbType.Int32).Value = dto.Quantity;  // Updated to Int32
                    command.Parameters.Add("p_dateofentry", OracleDbType.Varchar2).Value = dto.DateOfEntry;
                    command.Parameters.Add("p_storeid", OracleDbType.Int32).Value = dto.StoreId;

                    // Open connection and execute
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }




        public bool DeleteProductById(int productId)
        {
            bool isSuccess = false;

            using (var connection = new OracleConnection(Connstr))
            {
                using (var command = new OracleCommand("PKG_BAKARI_USER_REGISTER.DELETE_PRODUCT_BY_ID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add("p_product_id", OracleDbType.Int32).Value = productId;

                    // Add output parameter
                    var successParam = new OracleParameter("p_success", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(successParam);

                    // Open connection and execute
                    connection.Open();
                    command.ExecuteNonQuery();

                    
                }
            }

            return isSuccess;
        }


        public bool UpdateProduct(UpdateProductDTO updateProductDto)
        {
            using (var connection = new OracleConnection(Connstr))
            {
                connection.Open();

                using (var command = new OracleCommand("PKG_BAKARI_USER_REGISTER.UPDATE_PRODUCTI", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.Add("p_product_id", OracleDbType.Int32).Value = updateProductDto.ProductId;
                    command.Parameters.Add("p_barcode", OracleDbType.Varchar2).Value = updateProductDto.Barcode;
                    command.Parameters.Add("p_productname", OracleDbType.Varchar2).Value = updateProductDto.ProductName;
                    command.Parameters.Add("p_quantity", OracleDbType.Int32).Value = updateProductDto.Quantity;
                    command.Parameters.Add("p_dateofentry", OracleDbType.Varchar2).Value = updateProductDto.DateOfEntry;

                    command.ExecuteNonQuery();
                }
            }

            return true;
        }


        public bool UpdateProductSell(UpdateProductDTO updateProductDto)
        {
            using (var connection = new OracleConnection(Connstr))
            {
                connection.Open();

                using (var command = new OracleCommand("PKG_BAKARI_USER_REGISTER.UPDATE_PRODUCTSELL", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.Add("p_product_id", OracleDbType.Int32).Value = updateProductDto.ProductId;
                    command.Parameters.Add("p_barcode", OracleDbType.Varchar2).Value = updateProductDto.Barcode;
                    command.Parameters.Add("p_productname", OracleDbType.Varchar2).Value = updateProductDto.ProductName;
                    command.Parameters.Add("p_quantity", OracleDbType.Int32).Value = updateProductDto.Quantity;
                    command.Parameters.Add("p_dateofentry", OracleDbType.Varchar2).Value = updateProductDto.DateOfEntry;

                    command.ExecuteNonQuery();
                }
            }

            return true;
        }


        public GetStoreDto GetStoreDto(int storeId, int storeOperatorId)
        {
            GetStoreDto storeDto = new GetStoreDto();

            using (OracleConnection conn = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.FilterStoreByOperator", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.Add("p_store_id", OracleDbType.Int32).Value = storeId;
                    cmd.Parameters.Add("p_storeoperator_id", OracleDbType.Int32).Value = storeOperatorId;

                    // Output cursors
                    cmd.Parameters.Add("o_store_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("o_product_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("o_productsell_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Handle store cursor output
                        using (OracleRefCursor storeCursor = (OracleRefCursor)cmd.Parameters["o_store_cursor"].Value)
                        {
                            using (OracleDataReader storeReader = storeCursor.GetDataReader())
                            {
                                if (storeReader.Read())
                                {
                                    storeDto.Id = storeReader.GetInt32(0);
                                    storeDto.storename = storeReader.GetString(1);
                                    storeDto.storeadress = storeReader.GetString(2);
                                    storeDto.STOREOPERATORid = storeReader.GetInt32(3);
                                }
                            }
                        }

                        // Handle product cursor output
                        using (OracleRefCursor productCursor = (OracleRefCursor)cmd.Parameters["o_product_cursor"].Value)
                        {
                            using (OracleDataReader productReader = productCursor.GetDataReader())
                            {
                                List<GetproductDto> productList = new List<GetproductDto>();
                                while (productReader.Read())
                                {
                                    productList.Add(new GetproductDto
                                    {
                                        Id = productReader.GetInt32(0),
                                        Barcode = productReader.GetString(1),
                                        ProductName = productReader.GetString(2),
                                        Quantity = productReader.GetInt32(3),
                                        DateOfEntry = productReader.GetString(4),
                                        StoreId = productReader.GetInt32(5),
                                        ProductSellId = productReader.GetInt32(6) // Mapping ProductSellId correctly
                                    });
                                }
                                storeDto.getproductDtos = productList;
                            }
                        }

                        // Handle product sell cursor output
                        using (OracleRefCursor productSellCursor = (OracleRefCursor)cmd.Parameters["o_productsell_cursor"].Value)
                        {
                            using (OracleDataReader productSellReader = productSellCursor.GetDataReader())
                            {
                                List<GetproductSellDto> productSellList = new List<GetproductSellDto>();
                                while (productSellReader.Read())
                                {
                                    productSellList.Add(new GetproductSellDto
                                    {
                                        Id = productSellReader.GetInt32(0),
                                        Barcode = productSellReader.GetString(1),
                                        ProductName = productSellReader.GetString(2),
                                        Quantity = productSellReader.GetInt32(3),
                                        DateOfEntry = productSellReader.GetString(4),
                                        StoreId = productSellReader.GetInt32(5),
                                        pdoductID = productSellReader.GetInt32(6) // Fetching product ID correctly
                                    });
                                }
                                storeDto.getproductSellDtos = productSellList;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log and rethrow the exception
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
            // Ensure lists are initialized before looping
            if (storeDto.getproductDtos == null)
                storeDto.getproductDtos = new List<GetproductDto>();
            if (storeDto.getproductSellDtos == null)
                storeDto.getproductSellDtos = new List<GetproductSellDto>();
            if (storeDto.getproductdtNashtiDtos == null)
                storeDto.getproductdtNashtiDtos = new List<GetproductdtNashtiDto>();




            foreach (var x in storeDto.getproductDtos)
            {
                foreach (var y in storeDto.getproductSellDtos)
                {
                    if (x.Id == y.pdoductID)
                    {
                        storeDto.getproductdtNashtiDtos.Add(new GetproductdtNashtiDto()
                        {
                            Id = x.Id,
                            Barcode = x.Barcode,
                            ProductName = x.ProductName,
                            Nashti = x.Quantity - y.Quantity,
                            DateOfEntry = x.DateOfEntry,
                            StoreId = x.StoreId
                        });
                    }
                }
            }

            return storeDto;
        }


        public GetStoreIdDto GetstoreByOperatorID(int storeOperatorId)
        {
            GetStoreIdDto storeDetails = null;

            using (OracleConnection conn = new OracleConnection(Connstr))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.GetStoreDetailsByOperator", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameter for storeOperatorId
                    cmd.Parameters.Add("p_storeoperator_id", OracleDbType.Int32).Value = storeOperatorId;

                    // Output cursor
                    cmd.Parameters.Add("o_store_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storeDetails = new GetStoreIdDto
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                storeOperatorId = Convert.ToInt32(reader["STOREOPERATORID"]),
                                storename = reader["STORENAME"].ToString(),
                                storeadress = reader["STOREADRESS"].ToString()
                            };
                        }   
                    }
                }
            }

            return storeDetails;
        }


        public GetstoreProductMeneger getproductdtomanager(int storeId)
        {
            GetstoreProductMeneger storeDto = new GetstoreProductMeneger();

            using (OracleConnection conn = new OracleConnection(Connstr))
            {
                using (OracleCommand cmd = new OracleCommand("PKG_BAKARI_USER_REGISTER.FilterStoreByManager", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.Add("p_store_id", OracleDbType.Int32).Value = storeId;
                  
                    // Output cursors
                    cmd.Parameters.Add("o_store_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("o_product_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("o_productsell_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Handle store cursor output
                        using (OracleRefCursor storeCursor = (OracleRefCursor)cmd.Parameters["o_store_cursor"].Value)
                        {
                            using (OracleDataReader storeReader = storeCursor.GetDataReader())
                            {
                                if (storeReader.Read())
                                {
                                    storeDto.Id = storeReader.GetInt32(0);
                                    storeDto.storename = storeReader.GetString(1);
                                    storeDto.storeadress = storeReader.GetString(2);
                                    storeDto.STOREOPERATORid = storeReader.IsDBNull(3) ? (int?)null : storeReader.GetInt32(3);

                                }
                            }
                        }

                        // Handle product cursor output
                        using (OracleRefCursor productCursor = (OracleRefCursor)cmd.Parameters["o_product_cursor"].Value)
                        {
                            using (OracleDataReader productReader = productCursor.GetDataReader())
                            {
                                List<GetproductDto> productList = new List<GetproductDto>();
                                while (productReader.Read())
                                {
                                    productList.Add(new GetproductDto
                                    {
                                        Id = productReader.GetInt32(0),
                                        Barcode = productReader.GetString(1),
                                        ProductName = productReader.GetString(2),
                                        Quantity = productReader.GetInt32(3),
                                        DateOfEntry = productReader.GetString(4),
                                        StoreId = productReader.GetInt32(5),
                                        ProductSellId = productReader.GetInt32(6) // Mapping ProductSellId correctly
                                    });
                                }
                                storeDto.getproductDtos = productList;
                            }
                        }

                        // Handle product sell cursor output
                        using (OracleRefCursor productSellCursor = (OracleRefCursor)cmd.Parameters["o_productsell_cursor"].Value)
                        {
                            using (OracleDataReader productSellReader = productSellCursor.GetDataReader())
                            {
                                List<GetproductSellDto> productSellList = new List<GetproductSellDto>();
                                while (productSellReader.Read())
                                {
                                    productSellList.Add(new GetproductSellDto
                                    {
                                        Id = productSellReader.GetInt32(0),
                                        Barcode = productSellReader.GetString(1),
                                        ProductName = productSellReader.GetString(2),
                                        Quantity = productSellReader.GetInt32(3),
                                        DateOfEntry = productSellReader.GetString(4),
                                        StoreId = productSellReader.GetInt32(5),
                                        pdoductID = productSellReader.GetInt32(6) // Fetching product ID correctly
                                    });
                                }
                                storeDto.getproductSellDtos = productSellList;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log and rethrow the exception
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }
            // Ensure lists are initialized before looping
            if (storeDto.getproductDtos == null)
                storeDto.getproductDtos = new List<GetproductDto>();
            if (storeDto.getproductSellDtos == null)
                storeDto.getproductSellDtos = new List<GetproductSellDto>();
            if (storeDto.getproductdtNashtiDtos == null)
                storeDto.getproductdtNashtiDtos = new List<GetproductdtNashtiDto>();




            foreach (var x in storeDto.getproductDtos)
            {
                foreach (var y in storeDto.getproductSellDtos)
                {
                    if (x.Id == y.pdoductID)
                    {
                        storeDto.getproductdtNashtiDtos.Add(new GetproductdtNashtiDto()
                        {
                            Id = x.Id,
                            Barcode = x.Barcode,
                            ProductName = x.ProductName,
                            Nashti = x.Quantity - y.Quantity,
                            DateOfEntry = x.DateOfEntry,
                            StoreId = x.StoreId
                        });
                    }
                }
            }

            return storeDto;
        }






    }





}


