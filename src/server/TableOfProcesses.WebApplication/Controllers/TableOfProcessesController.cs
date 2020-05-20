using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TableOfProcesses.WebApplication.Helpers;
using TableOfProcesses.WebApplication.Models;
using TableOfProcesses.WebApplication.Notifications;
using TableOfProcesses.WebApplication.Services;

namespace TableOfProcesses.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableOfProcessesController : ControllerBase
    {
        private readonly IProcessService processService;

        public TableOfProcessesController(IProcessService processService)
        {
            this.processService = processService;
        }

        [HttpGet("statistics")]
        public ActionResult<StatisticsResponse> GetStatistics()
        {            
            Helper.LogInfo("GetStatistics()", "Begin execution");
            try
            {
                Helper.LogInfo("GetStatistics()", "End execution");
                return processService.GetStatistics();
            }
            catch(Exception ex)
            {
                Helper.LogError("GetStatistics()", ex.Message);
                Helper.LogInfo("GetStatistics()", "End execution");
                return BadRequest(new { exceptionMessage = ex.Message });
            }            
        }
        
        [HttpGet("notifyme")]
        public async Task<ActionResult<string>> GetNotification()
        {
            Helper.LogInfo("GetNotification()", "Begin execution");            
            var notificationService = new LongPollingInfrastructure();
            var message = await notificationService.WaitAsync();
            Helper.LogInfo("GetNotification()", message);
            Helper.LogInfo("GetNotification()", "End execution");            
            return message;            
        }

        [HttpGet("doknock")]
        public void DoKnock()
        {
            LongPollingInfrastructure.PublishNotification("knock knock");            
        }
    }
}