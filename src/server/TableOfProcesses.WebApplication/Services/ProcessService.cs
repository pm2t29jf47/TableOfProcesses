using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TableOfProcesses.WebApplication.DataAccess;
using TableOfProcesses.WebApplication.DataAccess.Interfaces;
using TableOfProcesses.WebApplication.Models;

namespace TableOfProcesses.WebApplication.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessDataAccess processDataAccess;
        public ProcessService()
        {
            this.processDataAccess = new ProcessessDataAccess();
        }

        public StatisticsResponse GetStatistics()
        {
            var foundProcesses = processDataAccess.GetProcesses();

            var result = new StatisticsResponse()
            {
                processes = foundProcesses.Select(Helpers.Map),
                SumNonpagedSystemMemorySize64InBytes = foundProcesses.Sum(x => x.NonpagedSystemMemorySize64),
                SumPagedMemorySize64InBytes = foundProcesses.Sum(x => x.PagedMemorySize64)
            };

            return result;
        }                
    }
}
