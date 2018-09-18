using AMZ_Coupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMZ_Coupon.Utility
{
    public class CouponDB
    {
        

        public static DataClasses1DataContext GetDbInstance()
        {
            DataClasses1DataContext DB = new DataClasses1DataContext();
            return DB;
        }

        public static string CheckLogin(string account , string password)
        {
            var db = GetDbInstance();
            var source = from p in db.Member
                         where p.Email == account && p.passwrod == password && p.Manager == "y"
                         select p.Manager;
            if (source.Count() > 0)
            {
                return "y";
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<Product> GetProducts()
        {
            var db = GetDbInstance();
            var source = from p in db.Product
                         select p;

            return source.ToList();
        }

        public static IEnumerable<Product> GetSingleProduct(int ID)
        {
            var db = GetDbInstance();
            var source = from p in db.Product
                         where p.ProductID == ID.ToString() && p.Shelf.ToString() == "y"
                         select p;
            return source.ToList();
        }


        public static bool InsertIntoProduct(ProductCouponTable product)
        {
            var db = GetDbInstance();
            var pd = new Product()
            {
                ProductID = DateTime.Now.ToString("yyyyMMddhhmmss"),
                Shelf = product.Shelf,
                ProductName = product.ProductName,
                Price = product.Price
            };
            db.Product.InsertOnSubmit(pd);

            var Coupons = product.PCoupon.Split('\n');
            for (int i = 0; i < Coupons.Length; i++)
            {
                if (Coupons[i].Length > 10)
                {
                    var cp = new Coupon()
                    {
                        CouponID = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + i,
                        Discount = product.Discount,
                        EndTime = product.EndTime,
                        StartTime = product.StartTime,
                        Coupon1 = Coupons[i],
                        Valid = product.Valid
                    };
                    db.Coupon.InsertOnSubmit(cp);
                }
            }


            try
            {
                db.SubmitChanges();
            }
            catch (Exception error)
            {
                return false;
            }

            return true;
        }

    }
}

/*

var db = GetDbInstance();
            var Plist = new List<Product>();

            var Coupons = product.PCoupon.Split('\n');
            for(int i =0; i< Coupons.Length; i++)
            {
                product.ProductID = DateTime.Now.ToString("yyyyMMddhhmmss") +  i;
                product.PCoupon = Coupons[i];
                Plist.Add(product);
            }
            db.Product.InsertAllOnSubmit(Plist);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception error)
            {
                return false;
            }

            return true;
 * */
