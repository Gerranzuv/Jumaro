using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Juomaro.Models;

namespace Juomaro.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Juomaro.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Client>("Clients");
    builder.EntitySet<ApplicationUser>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClientsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CoreController core = new CoreController();

        // GET: odata/Clients
        [EnableQuery]
        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        // GET: odata/Clients(5)
        [EnableQuery]
        public SingleResult<Client> GetClient([FromODataUri] int key)
        {
            return SingleResult.Create(db.Clients.Where(client => client.id == key));
        }

        // PUT: odata/Clients(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Client> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await db.Clients.FindAsync(key);
            if (client == null)
            {
                return NotFound();
            }

            patch.Put(client);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(client);
        }

        // POST: odata/Clients
        public async Task<IHttpActionResult> Post(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (db.Clients.Where(a => a.Email.Equals(client.Email)).ToList().Count > 0)
            {
                return BadRequest("There is another client with the same email!");
            }
            if (client.MerchantId == null)
            {
                return BadRequest("MerchantId can't be null!");
            }
 
            ApplicationUser merchant = db.Users.Find(client.MerchantId);
            if (merchant == null)
            {
                return BadRequest("No matching merchant!");
            }
            var currentUser = core.getCurrentUser();
            client.CreationDate = DateTime.Now;
            client.LastModificationDate = DateTime.Now;
            client.Creator = currentUser;
            client.Modifier = currentUser;
            client.CreatorId = currentUser.Id;
            client.ModifierId = currentUser.Id;

            db.Clients.Add(client);
            await db.SaveChangesAsync();

            return Created(client);
        }

        // PATCH: odata/Clients(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Client> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await db.Clients.FindAsync(key);
            if (client == null)
            {
                return NotFound();
            }

            patch.Patch(client);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(client);
        }

        // DELETE: odata/Clients(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Client client = await db.Clients.FindAsync(key);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Clients(5)/Creator
        [EnableQuery]
        public SingleResult<ApplicationUser> GetCreator([FromODataUri] int key)
        {
            return SingleResult.Create(db.Clients.Where(m => m.id == key).Select(m => m.Creator));
        }

        // GET: odata/Clients(5)/Merchant
        [EnableQuery]
        public SingleResult<ApplicationUser> GetMerchant([FromODataUri] int key)
        {
            return SingleResult.Create(db.Clients.Where(m => m.id == key).Select(m => m.Merchant));
        }

        // GET: odata/Clients(5)/Modifier
        [EnableQuery]
        public SingleResult<ApplicationUser> GetModifier([FromODataUri] int key)
        {
            return SingleResult.Create(db.Clients.Where(m => m.id == key).Select(m => m.Modifier));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int key)
        {
            return db.Clients.Count(e => e.id == key) > 0;
        }
    }
}
