using PetrolPumpApp.Filters;
using PetrolPumpApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PetrolPumpApp.Controllers
{
    [RoutePrefix("api/dispensing")]
    [JwtAuthentication] // Protect all endpoints with JWT
    public class DispensingController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Get all dispensing records with optional filtering
        /// GET: api/dispensing
        /// </summary>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetRecords(
            string dispenserNo = null,
            string paymentMode = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            try
            {
                var query = db.DispensingRecords.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(dispenserNo))
                {
                    query = query.Where(r => r.DispenserNo == dispenserNo);
                }

                if (!string.IsNullOrEmpty(paymentMode))
                {
                    query = query.Where(r => r.PaymentMode == paymentMode);
                }

                if (startDate.HasValue)
                {
                    query = query.Where(r => r.CreatedAt >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    // Include the entire end date
                    var endDateTime = endDate.Value.Date.AddDays(1).AddTicks(-1);
                    query = query.Where(r => r.CreatedAt <= endDateTime);
                }

                var records = query.OrderByDescending(r => r.CreatedAt).ToList();

                return Ok(records);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get a single dispensing record by ID
        /// GET: api/dispensing/5
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetRecord(int id)
        {
            try
            {
                var record = db.DispensingRecords.Find(id);
                if (record == null)
                {
                    return NotFound();
                }
                return Ok(record);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Create a new dispensing record with file upload
        /// POST: api/dispensing
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateRecord()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return BadRequest("Unsupported media type");
                }

                var uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var provider = new MultipartFormDataStreamProvider(uploadPath);
                await Request.Content.ReadAsMultipartAsync(provider);

                // Extract form data
                var dispenserNo = provider.FormData["DispenserNo"];
                var quantityFilled = provider.FormData["QuantityFilled"];
                var vehicleNumber = provider.FormData["VehicleNumber"];
                var paymentMode = provider.FormData["PaymentMode"];

                // Validate required fields
                if (string.IsNullOrEmpty(dispenserNo) ||
                    string.IsNullOrEmpty(quantityFilled) ||
                    string.IsNullOrEmpty(vehicleNumber) ||
                    string.IsNullOrEmpty(paymentMode))
                {
                    return BadRequest("All fields are required");
                }

                // Parse quantity
                if (!decimal.TryParse(quantityFilled, out decimal quantity))
                {
                    return BadRequest("Invalid quantity value");
                }

                // Handle file upload
                string paymentProofPath = null;
                if (provider.FileData.Count > 0)
                {
                    var file = provider.FileData[0];
                    var originalFileName = file.Headers.ContentDisposition.FileName.Trim('"');
                    var fileExtension = Path.GetExtension(originalFileName);
                    var newFileName = $"{Guid.NewGuid()}{fileExtension}";
                    var newFilePath = Path.Combine(uploadPath, newFileName);

                    // Move file to permanent location with new name
                    File.Move(file.LocalFileName, newFilePath);
                    paymentProofPath = $"/Uploads/{newFileName}";
                }

                // Create record
                var record = new DispensingRecord
                {
                    DispenserNo = dispenserNo,
                    QuantityFilled = quantity,
                    VehicleNumber = vehicleNumber,
                    PaymentMode = paymentMode,
                    PaymentProofPath = paymentProofPath,
                    CreatedAt = DateTime.Now
                };

                db.DispensingRecords.Add(record);
                await db.SaveChangesAsync();

                return Created($"api/dispensing/{record.Id}", record);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
