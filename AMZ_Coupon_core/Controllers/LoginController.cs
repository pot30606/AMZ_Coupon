using AMZ_Coupon.Models;
using AMZ_Coupon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMZ_Coupon.Controllers
{
    [RoutePrefix("View/l")]
    public class LoginController : ApiController
    {
        [Route("api/login/checkLogin")]
        public string checkLogin(MemberTable member)
        {
            var account = member.Email;
            var password = member.password;

            var result = CouponDB.CheckLogin(account , password);
            if(result == "y")
            {
                return "y";
            }

            return null;
        }

        [Route("api/login/check/{parameter}")]
        public bool check(string parameter)
        {
            if(parameter == "y")
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
