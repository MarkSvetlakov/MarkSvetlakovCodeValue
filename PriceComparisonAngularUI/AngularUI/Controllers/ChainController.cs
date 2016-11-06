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
    public class ChainController : ApiController
    {
        MainDBEntities db = new MainDBEntities();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var a = db.chains
                .Include(x => x.stores)
                .Select(x => new
                {
                    x.chain_id,
                    x.chain_name,
                    stores = x.stores.Select(s => new
                    {
                        s.store_id,
                        s.store_name
                    }),
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
