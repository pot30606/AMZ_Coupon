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

        [Route("api/main/{MethodName}/{id}")]
        [ActionName("Product")]
        [HttpGet]
        public IEnumerable<Product> GetProduct(int id)
        {
            var result = CouponDB.GetSingleProduct(id);

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
