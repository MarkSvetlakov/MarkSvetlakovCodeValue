using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularUI.Controllers
{
    public class ProductController : ApiController
    {
        MainDBEntities db = new MainDBEntities();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var a = db.products
                .Include(p => p.stores_products)
                .Select(p => new {
                p.manufacture_name,
                p.product_id,
                p.product_name,
                p.quantity,
                stores_products = p.stores_products.Select(s => new {
                    s.product_price,
                    s.store_id,
                }),
                p.unit_measure,
                p.unit_quantity
            });
            return Json(a);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
