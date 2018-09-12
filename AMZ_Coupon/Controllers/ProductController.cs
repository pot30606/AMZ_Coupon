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
        [Route("api/product/{MethodName}/{json}")]
        [HttpPost]
        public bool PostProductInfo(object jsons)
        {
            dynamic json = JsonConvert.SerializeObject(jsons);
            var product = new Product
            {
                ProductName = json["ProductName"].ToString(),
                Discount = (Decimal)json["Discount"],
                PCoupon = json["PCoupon"].ToString(),
                Price = (Decimal)json["Price"],
                StartTime = (DateTime)json["StartTime"],
                EndTime = (DateTime)json["EndTime"],
                Shelf = (Char)json["Shelf"],
                Valid = (Char)json["Valid"]
            };

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
