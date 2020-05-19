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
            var processService = new ProcessService();
            
            try
            {                
                return processService.GetStatistics();
            }
            catch(Exception ex)
            {
                return BadRequest(new { exceptionMessage = ex.Message });
            }                       
        }        
    }
}