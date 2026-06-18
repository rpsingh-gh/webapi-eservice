
namespace Eshopping_WebAPI.Model
{
    public class Product
    {
        public int pID { get; set; }
        public string pName { get; set; }
        public string pType { get; set; }
        public string pDestcription { get; set; }
        public int NoOfItemsAvailable { get; set; }
        public int MaxItemPerOder { get; set; }
        public decimal PricePerItem { get; set; }
        public string pImageName { get; set; }
        public string pStatus { get; set; }
        public bool pActive { get; set; }
        public string ModifiedBy { get; set; }
        public int pDiscount { get; set; }
        public string pDiscountDescription { get; set; }

    }
    public class ProductOrder
    {
        public decimal OrderValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal NetPaidValue { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }


    }
    public class OrderItem
    {
        // public int OrderItemId { get; set; }
        //public int OrderId { get; set; }
        // public string pDiscountDesc { get; set;}
        public int pId { get; set; }
        public decimal PricePerItem { get; set; }
        public int pDiscountPercentage { get; set; }
        public int pQuantity { get; set;}
    }
    public class ListOrder
    {
    
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public int OrderId { get; set; }
        public decimal OrderValue { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal NetPaidValue { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool PaymentStatus { get; set; }
        public int uID { get; set; }
        public string uName { get; set; }
        public string uMobileNo { get; set; }

    }

    public class OrderStatus
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}

