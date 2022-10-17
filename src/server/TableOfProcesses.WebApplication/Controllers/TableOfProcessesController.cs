using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TableOfProcesses.WebApplication.Models;
using TableOfProcesses.WebApplication.Notifications;
using TableOfProcesses.WebApplication.Services;

namespace TableOfProcesses.WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableOfProcessesController:ControllerBase
    {
        private readonly IProcessService processService;

        private readonly bool _isDebug = Log.IsEnabled(LogEventLevel.Debug);

        public TableOfProcessesController(IProcessService processService)
        {
            this.processService = processService ?? throw new ArgumentNullException(nameof(processService));
        }

        /// <summary>
        /// Get system processes statistics
        /// </summary>        
        [HttpGet("statistics")]
        public ActionResult<StatisticsResponse> GetStatistics()
        {
            var currentMethod = _isDebug ? MethodBase.GetCurrentMethod() : null;
            Log.Debug("Begin execution. Method: {type} {method}", currentMethod?.ReflectedType.FullName, currentMethod?.Name);

            try
            {
                Thread.Sleep(1000 * 20);
                var result = processService.GetStatistics();
                Log.Debug("End execution. Method: {type} {method}", currentMethod?.ReflectedType.FullName, currentMethod?.Name);

                return result;
            }
            catch(Exception ex)
            {
                Log.Error("Exception handled. Method: {type} {method} Exception: {@ex}", currentMethod.ReflectedType.FullName, currentMethod.Name, ex);
                Log.Debug("End execution. Method: {type} {method}", currentMethod?.ReflectedType.FullName, currentMethod?.Name);

                return BadRequest(new { exceptionMessage = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint for high load notification
        /// </summary>        
        [HttpGet("notifyme")]
        public async Task<ActionResult<string>> GetNotification()
        {
            var currentMethod = _isDebug ? MethodBase.GetCurrentMethod() : null;
            Log.Debug("Begin execution. Method: {method}", currentMethod?.ReflectedType.FullName);

            try
            {
                var notificationService = new LongPollingInfrastructure();
                var message = await notificationService.WaitAsync();
                Log.Debug("End execution. Method: {method}", currentMethod?.ReflectedType.FullName);

                return message;
            }
            catch(Exception ex)
            {
                Log.Error("Exception handled. Method: {method} Exception: {@ex}", currentMethod.ReflectedType.FullName, ex);
                Log.Debug("End execution. Method: {method}", currentMethod?.ReflectedType.FullName);

                return BadRequest(new { exceptionMessage = ex.Message });
            }
        }
    }
}