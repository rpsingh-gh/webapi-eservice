using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshopping_WebAPI.DBservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eshopping_WebAPI.Model;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Cors;
using System.Web.Http.Results;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Eshopping_WebAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {

        // GET: api/Customer/Create  
        [Route("api/User/Create")]
        [HttpPost]
        // [ResponseType(typeof(tblCustomer))]
        public Response Create(UserInsert objuser)
        {
            
            try
            {
                CrudDataService objCrd = new CrudDataService();
                Utilities util = new Utilities();
                
        Int32 Id = 0;

                Random rnd = new Random();
                string VerificationCode= rnd.Next(1, 1000000).ToString("D6");
                Response res = new Response();
                objuser.VerificationCode = VerificationCode;
                if ((objuser.Name != null) && (objuser.EmailID != null))
                {
                    Id = objCrd.InsertUser(objuser);
                    if(Id>0)
                    {
                        String VerificationLink = "https://happymindmeal-eservice-g4dxbtbqhzane2g7.canadacentral-01.azurewebsites.net/api/User/Verification?userId=" + Id + "&VerficationCode=" + VerificationCode;
                        bool IsEmailSent = util.send_email(objuser.EmailID, objuser.Name, VerificationLink);
                        if (IsEmailSent)
                        {

                        res.Status = "OK";
                        res.StatusCode = 200;
                        res.Description = "Successfull";
                       }
                        else {
                            res.Status = "KO";
                            res.StatusCode = 0;
                            res.Description = "Unsuccessfull";
                        }
                    }
                }
                else Id = -1;
                return res;
            }
            catch
            {
                throw;
            }
        }

        //GET: api/User/Verification 
        [Route("api/User/Verification")]
        [HttpGet]
        public string VerifyUser(string userId, string VerficationCode)
        {
            try
            {
                string message = "";
                CrudDataService objCrd = new CrudDataService();
                int result = objCrd.userVerification(userId, VerficationCode );
                if(result >0)
                {
                    return message = "Verification is successful. You may proceed to login and enjoy your offers!";
                }
                else
                {
                    return message = "Verification has Failed. Please try again.";
                }

            }
            catch
            {
                throw;
            }
        }

        [Route("api/User/GetAll")]
        [HttpGet]
        public IEnumerable<userData> GetUser( )
        {
            try
            {
                
                
                CrudDataService objCrd = new CrudDataService();
                List<userData> listUser = objCrd.GetUserList();
                return listUser;
            }
            catch
            {
                throw;
            }
        }

        // GET: api/User/Delete  
        [Route("api/User/Delete")]
        [HttpDelete]
        public string Delete(long? id)
        {
            try
            {
                CrudDataService objCrd = new CrudDataService();
                Int32 message = 0;
                message = objCrd.DeleteUser(id);
                return message.ToString();
            }
            catch
            {
                throw;
            }
        }


        // GET: api/User/Edit  
        [Route("api/User/Edit")]
        [HttpPut]
        //[ResponseType(typeof(User))]
        public string Edit(UserUpdate objUser)
        {
            try
            {
                CrudDataService objCrd = new CrudDataService();
                Int32 message = 0;
                message = objCrd.EditUser(objUser);
                return message.ToString();

            }
            catch
            {
                throw;
            }
        }


        // GET: api/User/Login  
        [Route("api/User/Login")]
        [HttpPost]
        //[ResponseType(typeof(User))]
        public JsonResult Login(UserLogin objLogin)
        {
            try
            {
                Response res = new Response();
                CrudDataService objCrd = new CrudDataService();

                userData response = objCrd.loginUser(objLogin);
                if(!string.IsNullOrWhiteSpace(response.Name))
                {
                    return new JsonResult(response);
                }
                
                else
                {
                    return new JsonResult(StatusCode(401, "Unauthorized"));
                }
            }
            catch
            {
                throw;
            }
        }


        [Route("api/User/getUserByParam")]
        [HttpPost]
        //[ResponseType(typeof(User))]
        public userData getUserByParam(Parameter objParam)
        {
            try
            {
                CrudDataService objCrd = new CrudDataService();

                userData response = objCrd.getDataByParam(objParam);
                return response;

            }
            catch
            {
                throw;
            }
        }


        //GET: api/User/Verification 
        [Route("api/User/userAction")]
        [HttpPost]
        public JsonResult userAction(userAction ObjUserAction)
        {
            try
            {
                Response res = new Response();
                Utilities util = new Utilities();
               
                if(ObjUserAction.ActionName == "SendEmailVerificationCode")
                {
                    Random rnd = new Random();
                    ObjUserAction.ActionCode = rnd.Next(1, 1000000).ToString("D6");
                    CrudDataService objCrd = new CrudDataService();
                    int result = objCrd.userAction(ObjUserAction);
                    if(result>0)
                    {
                      bool IsEmailSent=  util.send_email(ObjUserAction.userEmailId, ObjUserAction.ActionCode);
                        if(IsEmailSent)
                        {
                            res.Status = "OK";
                            res.StatusCode = 200;
                            res.Description = "Successfull";
                        }
                    }
                    else
                    {
                        res.Status = "KO";
                        res.StatusCode = 0;
                        res.Description = "Un Successfull";
                    }

                }
                if(ObjUserAction.ActionName == "VerificationCode")
                {
                    CrudDataService objCrd = new CrudDataService();
                    int result = objCrd.userAction(ObjUserAction);
                    if (result > 0)
                    {
                        res.Status = "OK";
                        res.StatusCode = 200;
                        res.Description = "Successfull";
                    }
                    else
                    {
                        res.Status = "KO";
                        res.StatusCode = 0;
                        res.Description = "Un Successfull";
                    }
                }

                if (ObjUserAction.ActionName == "SetNewPassword")
                {
                    CrudDataService objCrd = new CrudDataService();
                    int result = objCrd.userAction(ObjUserAction);
                    if (result > 0)
                    {
                        res.Status = "OK";
                        res.StatusCode = 200;
                        res.Description = "Successfull";
                    }
                    else
                    {
                        res.Status = "KO";
                        res.StatusCode = 0;
                        res.Description = "Un Successfull";
                    }
                }

                if (ObjUserAction.ActionName == "UpdateProfilePhoto")
                {
                    CrudDataService objCrd = new CrudDataService();
                    int result = objCrd.userAction(ObjUserAction);
                    if (result > 0)
                    {
                        res.Status = "OK";
                        res.StatusCode = 200;
                        res.Description = "Successfull";
                    }
                    else
                    {
                        res.Status = "KO";
                        res.StatusCode = 0;
                        res.Description = "Un Successfull";
                    }
                }

                return new JsonResult(res);

            }
            catch
            {
                throw;
            }
        }


    }
}
