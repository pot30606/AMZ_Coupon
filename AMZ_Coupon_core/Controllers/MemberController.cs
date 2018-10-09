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

    [RoutePrefix("View/m")]
    public class MemberController : ApiController
    {
        [Route("api/member/{MethodName}")]
        public bool PostUserInfo(MemberTable memberinfo)
        {
            var InsertMember = CouponDB.InsertIntoMember(memberinfo);
            var AlterCouponStatus = CouponDB.AlterCouponStatus(memberinfo.PCoupon);
            if (InsertMember == true && AlterCouponStatus == true)
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
