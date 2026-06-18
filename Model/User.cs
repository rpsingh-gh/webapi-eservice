using System.Xml.Linq;

namespace Eshopping_WebAPI.Model
{
    public class UserInsert
    {
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Subs_Newsletter { get; set; }
        public string VerificationCode { get; set; }
    }

    public class UserUpdate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public bool Subs_Newsletter { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class UserLogin
    {
        public string EmailID { get; set; }
        public string Password { get; set; }

    }

    public class Parameter
    {
        public string parmName { get; set; }
        public string paramValue { get; set; }

    }

    public class userData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public bool Subs_Newsletter { get; set; }
        public bool Verified { get; set; }
        public bool  Active { get; set; }
        public string UserRole { get; set; }
        public string ModifiedBy { get; set; }
        public string ImageName { get; set; }

    }

    public class userAction
    {
        public string userEmailId { get; set; }
        public string ActionName { get; set; }
        public string ActionCode { get; set; }

    } 
    public class Response
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

    }
}
