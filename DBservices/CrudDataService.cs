using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Eshopping_WebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Reflection.PortableExecutable;
using Microsoft.Extensions.Configuration;

namespace Eshopping_WebAPI.DBservices
{
    public class CrudDataService
    {
        Utilities util = new Utilities();
        // User Operations
        public Int32 InsertUser(UserInsert objUser)
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("CREATE_USER", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Name", objUser.Name);
                objCommand.Parameters.AddWithValue("@EmailID", objUser.EmailID);
                objCommand.Parameters.AddWithValue("@MobileNo", objUser.MobileNo);
                objCommand.Parameters.AddWithValue("@Password", objUser.Password);
                objCommand.Parameters.AddWithValue("@Subs_Newsletter", objUser.Subs_Newsletter);
                objCommand.Parameters.AddWithValue("@VerificationCode", objUser.VerificationCode);

               result = Convert.ToInt32(objCommand.ExecuteScalar());

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public List<userData> GetUserList( )
        {

            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            try
            {
                List<userData> _listUser = new List<userData>();

                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("VIEWALL_USER", Conn);

                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    userData objUser = new userData();
                    objUser.ID = Convert.ToInt32(_Reader["ID"]);
                    objUser.Name = _Reader["Name"].ToString();
                    objUser.EmailID = _Reader["EmailID"].ToString();
                    objUser.MobileNo = _Reader["MobileNo"].ToString();
                    objUser.Verified = Convert.ToBoolean(_Reader["IsVerified"]);
                    objUser.Subs_Newsletter = Convert.ToBoolean(_Reader["Subs_Newsletter"]);
                    objUser.Active = Convert.ToBoolean(_Reader["IsActive"]);
                    objUser.ModifiedBy = _Reader["ModifiedBy"].ToString();
                    objUser.UserRole = _Reader["UserRole"].ToString();
                    objUser.ImageName = _Reader["ImageName"].ToString();
                    _listUser.Add(objUser);
                }

                return _listUser;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public Int32 DeleteUser(long? id)
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("delete_USER", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@ID", id);
                result = Convert.ToInt32(objCommand.ExecuteScalar());

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public Int32 EditUser(UserUpdate objUser)
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("EDIT_USER", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@ID", objUser.ID);
                objCommand.Parameters.AddWithValue("@Name", objUser.Name);
                objCommand.Parameters.AddWithValue("@EmailID", objUser.EmailID);
                objCommand.Parameters.AddWithValue("@MobileNo", objUser.MobileNo);
                objCommand.Parameters.AddWithValue("@Subs_Newsletter", objUser.Subs_Newsletter);
                objCommand.Parameters.AddWithValue("@ModifiedBy", objUser.ModifiedBy);


                result = Convert.ToInt32(objCommand.ExecuteScalar());

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public int userVerification(string userId, string VerficationCode)
        {

            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            try
            {
                int result = 0;

                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("VERFICATION_USER", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@ID", userId);
                objCommand.Parameters.AddWithValue("@VerificationCode", VerficationCode);
                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    result = Convert.ToInt32(_Reader["Result"]);
                   
                }

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        // Login
        public userData loginUser(UserLogin objLogin)
        {

            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            userData result = new userData();
            Conn.Open();

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("ValidateLogin_User", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@EmailID", objLogin.EmailID);
                objCommand.Parameters.AddWithValue("@Password", objLogin.Password);
                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    result.ID = Convert.ToInt32(_Reader["ID"]);
                    result.Name = _Reader["Name"].ToString();
                    result.EmailID = _Reader["EmailID"].ToString();
                    result.MobileNo = _Reader["MobileNo"].ToString();
                    result.Verified = Convert.ToBoolean(_Reader["IsVerified"]);
                    result.Subs_Newsletter = Convert.ToBoolean(_Reader["Subs_Newsletter"]);
                    result.Active = Convert.ToBoolean(_Reader["IsActive"]);
                    result.UserRole = _Reader["UserRole"].ToString();
                    result.ModifiedBy = _Reader["ModifiedBy"].ToString();
                    result.ImageName = _Reader["ImageName"].ToString();

                }

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public userData getDataByParam(Parameter objParam)
        {

            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            userData result = new userData();
            Conn.Open();

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("GetByParameter_User", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@parmName", objParam.parmName);
                objCommand.Parameters.AddWithValue("@paramValue", objParam.paramValue);
                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    result.ID = Convert.ToInt32(_Reader["ID"]);
                    result.Name = _Reader["Name"].ToString();
                    result.EmailID = _Reader["EmailID"].ToString();
                    result.MobileNo = _Reader["MobileNo"].ToString();
                    result.Verified = Convert.ToBoolean(_Reader["IsVerified"]);
                    result.Subs_Newsletter = Convert.ToBoolean(_Reader["Subs_Newsletter"]);
                    result.Active = Convert.ToBoolean(_Reader["IsActive"]);
                    result.UserRole = _Reader["UserRole"].ToString();
                    result.ModifiedBy = _Reader["ModifiedBy"].ToString();
                    result.ImageName = _Reader["ImageName"].ToString();

                }

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public int userAction(userAction ObjUserAction)
        {

            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;

            Conn.Open();

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();
                SqlCommand objCommand = new SqlCommand("sp_DoAction_User", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@EmailID", ObjUserAction.userEmailId);
                objCommand.Parameters.AddWithValue("@ActionName", ObjUserAction.ActionName);
                objCommand.Parameters.AddWithValue("@ActionCode", ObjUserAction.ActionCode);
                int result = Convert.ToInt32(objCommand.ExecuteScalar());
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }

        // product Operations
        public List<Product> GetPorductList()
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            try
            {
                List<Product> _listProduct= new List<Product>();

                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("sp_GetAllProduct", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    Product objPro = new Product();
                    objPro.pID = Convert.ToInt32(_Reader["pID"]);
                    objPro.pName = _Reader["pName"].ToString();
                    objPro.pType = _Reader["pType"].ToString();
                    objPro.pDestcription = _Reader["pDestcription"].ToString();
                    objPro.NoOfItemsAvailable = Convert.ToInt32(_Reader["NoOfItemsAvailable"]);
                    objPro.MaxItemPerOder = Convert.ToInt32(_Reader["MaxItemPerOder"]);
                    objPro.PricePerItem = Convert.ToDecimal(_Reader["PricePerItem"]);
                    objPro.pImageName = _Reader["pImageName"].ToString();
                    objPro.pStatus = _Reader["pStatus"].ToString();
                    objPro.pActive = Convert.ToBoolean(_Reader["pActive"]);
                    objPro.ModifiedBy = _Reader["ModifiedBy"].ToString();
                    objPro.pDiscount = Convert.ToInt32(_Reader["pDiscount"]);
                    objPro.pDiscountDescription = _Reader["pDiscountDescription"].ToString();
                    _listProduct.Add(objPro);
                }

                return _listProduct;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public Int32 CreateOrder(ProductOrder crteOrdr)
        {
            int result = 0;
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            try
            {
               bool exists = HasRecentOrderAsync(crteOrdr.CustomerId, Conn).Result;  
                if (exists)
                    return result;

                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("CREATE_ORDER", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@OrderValue", crteOrdr.OrderValue);
                objCommand.Parameters.AddWithValue("@DiscountValue", crteOrdr.DiscountValue);
                objCommand.Parameters.AddWithValue("@NetPaidValue", crteOrdr.NetPaidValue);
                objCommand.Parameters.AddWithValue("@ExpectedDeliveryDate", crteOrdr.ExpectedDeliveryDate);
                objCommand.Parameters.AddWithValue("@StatusID", "91");
                objCommand.Parameters.AddWithValue("@CustomerId", crteOrdr.CustomerId);

                DataTable CartItemTable = util.CreateTable();
                foreach (var item in crteOrdr.OrderItems)
                {
                    DataRow dr = CartItemTable.NewRow();
                    dr["pID"] = item.pId;
                    dr["pricePerItem"] = item.PricePerItem;
                    dr["pDsicountPercentage"] = item.pDiscountPercentage;
                    dr["pQuantity"] = item.pQuantity;
                    CartItemTable.Rows.Add(dr);
                }
                objCommand.Parameters.AddWithValue("@CartItemTable", CartItemTable);

                result = Convert.ToInt32(objCommand.ExecuteScalar());

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }
        public async Task<bool> HasRecentOrderAsync(int customerId, SqlConnection conn)
        {
            using var command = new SqlCommand("sp_CheckRecentOrder", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@CustomerId", customerId);

            await conn.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result) == 1;
        }

        public List<ListOrder> GetOrderList()
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            try
            {
                List<ListOrder> _listOrder = new List<ListOrder>();

                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("GETALL_ORDER", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    ListOrder objOrder = new ListOrder();
                    objOrder.OrderDate = Convert.ToDateTime(_Reader["OrderDate"]);
                    objOrder.ExpectedDeliveryDate = Convert.ToDateTime(_Reader["ExpectedDeiveryDate"]);
                    objOrder.OrderId = Convert.ToInt32(_Reader["OrderId"]);
                    objOrder.OrderValue = Convert.ToDecimal(_Reader["OrderValue"]);
                    objOrder.DiscountValue = Convert.ToDecimal(_Reader["DiscountValue"]);
                    objOrder.NetPaidValue = Convert.ToDecimal(_Reader["NetPaidValue"]);
                    objOrder.StatusId = Convert.ToInt32(_Reader["StatusId"]);
                    objOrder.StatusName= _Reader["StatusName"].ToString();
                    objOrder.PaymentStatus = Convert.ToBoolean(_Reader["PaymentStatus"]);
                    objOrder.uID = Convert.ToInt32(_Reader["ID"]);
                    objOrder.uName = _Reader["Name"].ToString();
                    objOrder.uMobileNo = _Reader["MobileNo"].ToString();
                    _listOrder.Add(objOrder);
                }

                return _listOrder;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }

        public List<OrderStatus> getOrderStatus()
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            try
            {
                List<OrderStatus> _listOrderStatus = new List<OrderStatus>();

                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("SELECT StatusID, StatusName,IsActive FROM OrderStatus where IsActive=1", Conn);
                objCommand.CommandType = CommandType.Text;
                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    OrderStatus objOrderStatus = new OrderStatus();
                    objOrderStatus.StatusID = Convert.ToInt32(_Reader["StatusID"]);
                    objOrderStatus.StatusName = _Reader["StatusName"].ToString();

                    _listOrderStatus.Add(objOrderStatus);
                }

                return _listOrderStatus;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }

        public String getOrderItems(string OrderID)
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            try
            {
                var jsonResult = new StringBuilder();

                if (Conn.State != System.Data.ConnectionState.Open)
                    Conn.Open();

                SqlCommand objCommand = new SqlCommand("getOrderItems", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@OrderId", OrderID);
                SqlDataReader _Reader = objCommand.ExecuteReader();

                while (_Reader.Read())
                {
                    jsonResult.Append(_Reader.GetValue(0).ToString());
                }

                return jsonResult.ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }

        public Int32 CreateProduct(Product procreate)
        {
            dbConnector objConn = new dbConnector();
            SqlConnection Conn = objConn.GetConnection;
            Conn.Open();

            int result = 0;

            try
            {
                if (Conn.State != System.Data.ConnectionState.Open) Conn.Open();

                SqlCommand objCommand = new SqlCommand("CREATE_PRODUCT", Conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@pID", procreate.pID);
                if(procreate.pID==0)
                    {
                        objCommand.Parameters.AddWithValue("@pStatus", 0);
                        objCommand.Parameters.AddWithValue("@pActive", 0);
                    }
                else
                    {
                        objCommand.Parameters.AddWithValue("@pStatus", procreate.pStatus);
                        objCommand.Parameters.AddWithValue("@pActive", procreate.pActive);
                    }
                objCommand.Parameters.AddWithValue("@pName", procreate.pName);
                objCommand.Parameters.AddWithValue("@pType", procreate.pType);
                objCommand.Parameters.AddWithValue("@pDestcription", procreate.pDestcription);
                objCommand.Parameters.AddWithValue("@NoOfItemsAvailable", procreate.NoOfItemsAvailable);
                objCommand.Parameters.AddWithValue("@MaxItemPerOder", procreate.MaxItemPerOder);
                objCommand.Parameters.AddWithValue("@PricePerItem ", procreate.PricePerItem);
                objCommand.Parameters.AddWithValue("@pImageName ", procreate.pImageName);
                objCommand.Parameters.AddWithValue("@pDiscount ", procreate.pDiscount);
                objCommand.Parameters.AddWithValue("@pDiscountDescription ", procreate.pDiscountDescription);
                objCommand.Parameters.AddWithValue("@ModifiedBy", procreate.ModifiedBy);


                result = Convert.ToInt32(objCommand.ExecuteScalar());

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (Conn != null)
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
            }
        }






    }
}
