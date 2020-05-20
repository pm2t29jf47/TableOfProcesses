using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TableOfProcesses.WebApplication.Models;
using TableOfProcesses.WebApplication.Notifications;
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
        
        [HttpGet("notifyme")]
        public async Task<ActionResult<string>> GetNotification()
        {
            Console.WriteLine($"Time: {DateTime.UtcNow}, Level: Info, Message: TableOfProcesses/notifyme");
            var notificationService = new LongPollingInfrastructure();
            var message = await notificationService.WaitAsync();
            return message;
        }

        [HttpGet("doknock")]
        public void DoKnock()
        {
            LongPollingInfrastructure.PublishNotification("PID 34 CPU 40%");            
        }
    }
}