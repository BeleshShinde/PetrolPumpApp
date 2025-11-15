using PetrolPumpApp.Filters;
using PetrolPumpApp.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PetrolPumpApp.Controllers
{
    [RoutePrefix("api/dispensing")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DispensingController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("")]
        [JwtAuthentication]
        public IHttpActionResult GetRecords(int page = 1, int pageSize = 10)
        {
            try
            {
                var totalRecords = db.DispensingRecords.Count();

                var records = db.DispensingRecords
                    .OrderByDescending(r => r.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new
                {
                    Success = true,
                    Records = records,
                    TotalRecords = totalRecords
                });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [JwtAuthentication]
        public IHttpActionResult GetRecord(int id)
        {
            try
            {
                var record = db.DispensingRecords.Find(id);
                if (record == null)
                {
                    return Ok(new { Success = false, Message = "Record not found" });
                }
                return Ok(new { Success = true, Record = record });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("")]
        [JwtAuthentication]
        public IHttpActionResult CreateRecord()
        {
            string imagePath = null;

            try
            {
                var httpContext = HttpContext.Current;
                var request = httpContext.Request;

                
                string dispenserNo = request.Form["DispenserNo"];
                string quantityStr = request.Form["QuantityFilled"];
                string vehicleNumber = request.Form["VehicleNumber"];
                string paymentMode = request.Form["PaymentMode"];

                if (string.IsNullOrEmpty(dispenserNo) || string.IsNullOrEmpty(quantityStr))
                {
                    return Ok(new { Success = false, Message = "Required fields missing" });
                }

                decimal quantity = 0;
                decimal.TryParse(quantityStr, out quantity);

                
                if (request.Files != null && request.Files.Count > 0)
                {
                    var file = request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        try
                        {
                            string uploadsPath = httpContext.Server.MapPath("~/Uploads");
                            if (!Directory.Exists(uploadsPath))
                            {
                                Directory.CreateDirectory(uploadsPath);
                            }

                            string extension = Path.GetExtension(file.FileName);
                            string fileName = Guid.NewGuid().ToString() + extension;
                            string fullPath = Path.Combine(uploadsPath, fileName);

                            file.SaveAs(fullPath);
                            imagePath = "/Uploads/" + fileName;
                        }
                        catch (Exception fileEx)
                        {
                            System.Diagnostics.Debug.WriteLine("File error: " + fileEx.Message);
                        }
                    }
                }

                
                var record = new DispensingRecord
                {
                    DispenserNo = dispenserNo,
                    NozzleNo = null,
                    FuelGrade = null,
                    Volume = quantity,
                    Amount = 0,
                    PaymentMode = paymentMode,
                    VehicleNumber = vehicleNumber,
                    TransactionDate = DateTime.Now,
                    ImagePath = imagePath,
                    CreatedAt = DateTime.Now
                };

                db.DispensingRecords.Add(record);
                db.SaveChanges();

                return Ok(new
                {
                    Success = true,
                    Message = "Record saved successfully" + (imagePath != null ? " with image" : ""),
                    RecordId = record.Id
                });
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    try
                    {
                        var fullPath = HttpContext.Current.Server.MapPath("~" + imagePath);
                        if (File.Exists(fullPath)) File.Delete(fullPath);
                    }
                    catch { }
                }
                return Ok(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{id}/download")]
        [JwtAuthentication]
        public IHttpActionResult DownloadRecordPdf(int id)
        {
            try
            {
                var record = db.DispensingRecords.Find(id);

                if (record == null)
                {
                    return NotFound();
                }

                byte[] pdfBytes = Helpers.PdfHelper.GenerateRecordPdf(record);

                var response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
                response.Content = new System.Net.Http.ByteArrayContent(pdfBytes);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"PetrolPump_Record_{record.Id}_{record.DispenserNo}_{DateTime.Now:yyyyMMdd}.pdf"
                };

                return ResponseMessage(response);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PDF Download Error: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null) db.Dispose();
            base.Dispose(disposing);
        }
    }
}