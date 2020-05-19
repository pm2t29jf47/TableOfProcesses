using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableOfProcesses.WebApplication.Models;

namespace TableOfProcesses.WebApplication.Services
{
    interface IProcessService
    {
        /// <summary>
        /// Statistics about processes
        /// </summary>
        /// <returns></returns>
        public StatisticsResponse GetStatistics();
    }
}
