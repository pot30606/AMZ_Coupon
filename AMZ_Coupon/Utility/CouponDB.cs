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
                         where p.ProductID == ID && p.Shelf.ToString() == "y"
                         select p;
            return source.ToList();
        }


        public static bool InsertIntoProduct(Product product)
        {
            var db = GetDbInstance();


            db.Product.InsertOnSubmit(product);
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
 * 
 * Discount = (Decimal)json["Discount"],
                PCoupon = json["PCoupon"].ToString(),
                Price = (Decimal)json["Price"],
                StartTime = (DateTime)json["StartTime"],
                EndTime = (DateTime)json["EndTime"],
                Shelf = (Char)json["Shelf"],
                Valid = (Char)json["Valid"]


    */