using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TableOfProcesses.WebApplication.Models;
using TableOfProcesses.WebApplication.Services;

namespace TableOfProcesses.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableOfProcessesController : ControllerBase
    {
        [HttpGet("statistics")]
        public ActionResult<StatisticsResponse> GetStatistics()
        {
            Console.WriteLine($"Time: {DateTime.UtcNow}, Level: Info, Message: Incoming request");
            var processService = new ProcessService();
            
            try
            {
                return processService.GetStatistics();
            }
            catch(Exception ex)
            {                
                Console.WriteLine($"Time: {DateTime.UtcNow}, Level: Error, Message:{ex.Message}");
                return BadRequest(new { exceptionMessage = ex.Message });
            }                       
        }        
    }
}