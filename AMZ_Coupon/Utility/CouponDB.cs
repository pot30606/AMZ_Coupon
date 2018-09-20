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

        public static string CheckLogin(string account, string password)
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
        #region GetData
        public static IEnumerable<Product> GetProducts()
        {
            var db = GetDbInstance();
            var source = from p in db.Product
                         select p;

            return source.ToList();
        }

        public static IEnumerable<ProductCouponTable> GetSingleProduct(string ID)
        {
            var db = GetDbInstance();
            var pc = new List<ProductCouponTable>();
            var CouponIsValidSource = (from p in db.Product
                                       join c in db.Coupon on p.ProductID equals c.ProductID
                                       where p.ProductID == ID && p.Shelf == "y" && c.Valid == "y"
                                       select new { p.ProductName, p.ProductID, p.Price, p.Shelf, c.Discount, c.PCoupon, c.StartTime, c.EndTime, c.Valid }).FirstOrDefault();

            if (CouponIsValidSource != null)
            {
                pc.Add(new ProductCouponTable()
                {
                    ProductID = CouponIsValidSource.ProductID,
                    ProductName = CouponIsValidSource.ProductName,
                    Price = CouponIsValidSource.Price,
                    Shelf = CouponIsValidSource.Shelf,
                    Discount = CouponIsValidSource.Discount,
                    PCoupon = CouponIsValidSource.PCoupon,
                    StartTime = CouponIsValidSource.StartTime,
                    EndTime = CouponIsValidSource.EndTime,
                    Valid = CouponIsValidSource.Valid.Replace(" ", "")
                });
            }
            else
            {
                pc = CouponAreSoldOut(ID);
            }

            return pc;
        }

        private static List<ProductCouponTable> CouponAreSoldOut(string ID)
        {
            var db = GetDbInstance();
            var pc = new List<ProductCouponTable>();

            var CouponAreSoldOut = (from p in db.Product
                                    join c in db.Coupon on p.ProductID equals c.ProductID
                                    where p.ProductID == ID && p.Shelf == "y" //&& c.Valid == "y"
                                    select new { p.ProductName, p.ProductID, p.Price, p.Shelf, c.Discount, c.PCoupon, c.StartTime, c.EndTime, c.Valid }).FirstOrDefault();
            if (CouponAreSoldOut != null)
            {
                pc.Add(new ProductCouponTable()
                {
                    ProductID = CouponAreSoldOut.ProductID,
                    ProductName = CouponAreSoldOut.ProductName,
                    Price = CouponAreSoldOut.Price,
                    Shelf = CouponAreSoldOut.Shelf,
                    Discount = CouponAreSoldOut.Discount,
                    StartTime = CouponAreSoldOut.StartTime,
                    EndTime = CouponAreSoldOut.EndTime,
                    Valid = CouponAreSoldOut.Valid.Replace(" ", "")
                }); 
            }
            else
            {
                return null;
            }

            return pc;

        }




        #endregion

        #region SaveData
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

            try
            {
                db.SubmitChanges();
            }
            catch (Exception error)
            {
                return false;
            }

            var Coupons = product.PCoupon.Split('\n');
            for (int i = 0; i < Coupons.Length; i++)
            {
                if (Coupons[i].Length > 10)
                {
                    var cp = new Coupon()
                    {
                        ProductID = DateTime.Now.ToString("yyyyMMddhhmmss"),
                        CouponID = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + i,
                        Discount = product.Discount,
                        EndTime = product.EndTime,
                        StartTime = product.StartTime,
                        PCoupon = Coupons[i],
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
        #endregion

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
