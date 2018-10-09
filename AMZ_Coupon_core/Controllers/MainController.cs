using AMZ_Coupon.Models;
using AMZ_Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace AMZ_Coupon.Controllers
{
    [RoutePrefix("View/p")]
    public class MainController : ApiController
    {

        [Route("api/main/{MethodName}")]
        [ActionName("AllProducts")]
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var result = CouponDB.GetProducts();
            return result;
        }

        [Route("api/main/{MethodName}/{productid}")]
        [ActionName("GetProduct")]
        [HttpGet]
        public IEnumerable<Models.ProductCouponTable> GetProduct(string productid)
        {
            var result = CouponDB.GetSingleProduct(productid);
            var test = CouponDB.GetAll();
            foreach(var item in test)
            {
                var x = item;
            }
            if (result.Count() > 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        

    }
}
