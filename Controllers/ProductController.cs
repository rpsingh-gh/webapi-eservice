using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshopping_WebAPI.DBservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eshopping_WebAPI.Model;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Hosting.Server;

namespace Eshopping_WebAPI.Controllers
{

    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [Route("api/Product/GetAll")]
        [HttpGet]
        public IEnumerable<Product> GetPro()
        {
            try
            {


                CrudDataService objCrd = new CrudDataService();
                List<Product> listProduct = objCrd.GetPorductList();
                return listProduct;
            }
            catch
            {
                throw;
            }
        }

        [Route("api/product/photo")]
        [HttpGet]
        public IActionResult getPhoto(string fileName)
        {

            try
            {
                if (fileName != "undefined" && fileName != string.Empty)
                {
                    if (!String.IsNullOrEmpty(fileName))
                    {
                        var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;
                        Byte[] b = System.IO.File.ReadAllBytes(physicalPath);   // You can use your own method over here.         
                        return File(b, "image/jpeg");
                    }
                    else return NotFound();
                }
                else return NotFound();
            }
            catch
            {
                throw;
            }
        }
       
        // upload photo
        [Route("api/Product/SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {

            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    stream.Flush();
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("dummy.jpg");
            }
        }


        [Route("api/Product/CreateOrder")]
        [HttpPost]
        public string CreateOrder(ProductOrder pOrder)
        {
            try
            {


                CrudDataService objCrd = new CrudDataService();
                int orderID = objCrd.CreateOrder(pOrder);
                if (orderID > 0)
                {
                    Parameter parm= new Parameter();
                    Utilities util = new Utilities();
                         
                    parm.parmName = "ID";
                    parm.paramValue = pOrder.CustomerId.ToString();
                    userData objuser = objCrd.getDataByParam(parm);


                    String body = "Dear "+ objuser.Name +"," + System.Environment.NewLine + System.Environment.NewLine;
                    body = body + "Thanks for placing your order with us. Your package is being processed and will be on its way soon." + System.Environment.NewLine + System.Environment.NewLine;
                    body = body + "You order ID for you ref: " + orderID + System.Environment.NewLine + System.Environment.NewLine;
                    body = body +"Please visit the following link and login to track the order under your profile" 
                       + System.Environment.NewLine + System.Environment.NewLine
                       + " https://eshopping-webapp.azurewebsites.net/#/ProfileViewPage"
                       + System.Environment.NewLine + System.Environment.NewLine
                        + System.Environment.NewLine + System.Environment.NewLine +
                       "Regards," + System.Environment.NewLine + "User Management Team";
                    bool IsEmailSent = util.send_email_Gen(objuser.EmailID, "Your order has beeb placed successfully", body);
                }
                return orderID.ToString();
                

            }
            catch
            {
                throw;
            }
        }


        [Route("api/Order/GetAll")]
        [HttpGet]
        public IEnumerable<ListOrder> GetOrder()
        {
            try
            {


                CrudDataService objCrd = new CrudDataService();
                List<ListOrder> listOrder = objCrd.GetOrderList();
                return listOrder;
            }
            catch
            {
                throw;
            }
        }

        // Ram
        [Route("api/Order/getOrderStatus")]
        [HttpGet]
        public IEnumerable<OrderStatus> getOrderStatus()
        {
            try
            {


                CrudDataService objCrd = new CrudDataService();
                List<OrderStatus> listOrder = objCrd.getOrderStatus();
                return listOrder;
            }
            catch
            {
                throw;
            }
        }


        [Route("api/Order/getOrderItems")]
        [HttpGet]
        public JsonResult getOrderItems(string OrderID)
        {
            try
            {
                CrudDataService objCrd = new CrudDataService();
                String result = objCrd.getOrderItems(OrderID);
                return new JsonResult(result);
            }
            catch
            {
                throw;
            }
        }


        [Route("api/Product/CreateProduct")]
        [HttpPost]
        public string CreateProduct(Product procreate)
        {
            try
            {
                CrudDataService objCrd = new CrudDataService();
                Int32 message = 0;
                message = objCrd.CreateProduct(procreate);
                return message.ToString();



            }
            catch
            {
                throw;
            }
        }
    }
}
