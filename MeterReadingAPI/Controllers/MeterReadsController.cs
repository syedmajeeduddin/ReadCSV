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

        /// <summary>
		/// https://localhost:44331/api/meterreads
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public IActionResult Index()
        {
            return Ok("Syed's Technical Assessment");
        }

        [HttpPost("meter-reading-uploads"), DisableRequestSizeLimit]
        public IActionResult Upload(IFormFile csvFile)
        {
            if (csvFile == null)
                return BadRequest();

            using var fileStream = csvFile.OpenReadStream();

            var meterReadsInfo = MeterReadsProcessor.ProcessFile(fileStream);

            _unitOfWork.MeterReads.AddRange(meterReadsInfo.meterReads);
            _unitOfWork.Complete();

            return Ok(new
            {
                success = meterReadsInfo.successCount,
                failed = meterReadsInfo.failedCount
            });
        }

    }
}