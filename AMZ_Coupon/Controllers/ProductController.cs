using AMZ_Coupon.Models;
using AMZ_Coupon.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMZ_Coupon.Controllers
{
    [RoutePrefix("View/p")]
    public class ProductController : ApiController
    {
        [Route("api/product/{MethodName}")]
        public bool PostProductInfo(Product product)
        {
            var result = CouponDB.InsertIntoProduct(product);

            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
