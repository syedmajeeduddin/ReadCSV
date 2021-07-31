using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MeterReadingAPI.Common;
using MeterReadingAPI.Models;
using MeterReadingAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeterReadingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MeterReadsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost, DisableRequestSizeLimit]
        //[ActionName("meter-reading-uploads")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Uploads", "Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                   
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    ResultSet results;

                   var meterReads = MeterReadsProcessor.ProcessFile(fullPath, out results );

                   _unitOfWork.MeterReads.AddRange(meterReads);
                    _unitOfWork.Complete();
                    return Ok(new { results });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}