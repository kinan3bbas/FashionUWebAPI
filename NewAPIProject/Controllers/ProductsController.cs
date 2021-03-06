﻿using System;
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
using NewAPIProject.Models;

namespace NewAPIProject.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using NewAPIProject.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Product>("Products");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

        [Authorize]
    public class ProductsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private CoreController core = new CoreController();

        // GET: odata/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: odata/Products(5)
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.Products.Where(product => product.id == key));
        }

        // PUT: odata/Products(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Product> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = await db.Products.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

            patch.Put(product);

            try
            {
                product.LastModificationDate = DateTime.Now;
                product.Modifier = core.getCurrentUser().Id;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // POST: odata/Products
        public async Task<IHttpActionResult> Post(Product product)
        {
            if(product.AttachmentId==null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Attachment attachment = db.Attachments.Where(a => a.id.Equals(product.AttachmentId)).FirstOrDefault();
            if (attachment == null)
                return BadRequest("Please Upload a valid photo");
            product.AttachmentId = attachment.id;
            product.CreationDate = DateTime.Now;
            product.LastModificationDate = DateTime.Now;
            product.Creator = core.getCurrentUser().UserName;
            product.Modifier = core.getCurrentUser().UserName;

            
            db.Products.Add(product);
            await db.SaveChangesAsync();

            return Created(product);
        }

        // PATCH: odata/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Product> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Product product = await db.Products.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }
            bool photoUpdated = false;
            foreach (var fieldname in patch.GetChangedPropertyNames())
            {
                if (fieldname.Equals("AttachmentId"))
                {
                    photoUpdated = true;
                    break;
                }

            }
            if (photoUpdated)
            {
                Attachment attachment = db.Attachments.Where(a => a.id.Equals(product.AttachmentId)).FirstOrDefault();
                if (attachment == null)
                    return BadRequest("Please Upload a valid photo");
                product.AttachmentId = attachment.id;
            }
            product.LastModificationDate = DateTime.Now;
            product.Modifier = core.getCurrentUser().UserName;

            patch.Patch(product);

            try
            {
                
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // DELETE: odata/Products(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Product product = await db.Products.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int key)
        {
            return db.Products.Count(e => e.id == key) > 0;
        }
    }
}
