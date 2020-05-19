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
            var preparedProcesses = foundProcesses.Select(ReadDataFromProcess).ToList();

            var result = new StatisticsResponse()
            {
                processes = preparedProcesses.OrderBy(x => x.Pid).ToList(),
                SumNonpagedSystemMemorySize64InBytes = preparedProcesses.Sum(x => x.NonpagedSystemMemorySize64InBytes ?? 0),
                SumPagedMemorySize64InBytes = preparedProcesses.Sum(x => x.PagedMemorySize64InBytes ?? 0)
            };

            return result;
        }

        private ProcessItemResponse ReadDataFromProcess(Process process)
        {
            return new ProcessItemResponse()
            {
                Pid = TryGetIdFromProcess(process),
                Command = TryGetProcessNameFromProcess(process),
                NonpagedSystemMemorySize64InBytes = TryGetNonpagedSystemMemorySize64FromProcess(process),
                PagedMemorySize64InBytes = TryGetpagedMemorySize64FromProcess(process)
            };
        }

        private static int? TryGetIdFromProcess(Process process)
        {
            try
            {
                return process.Id;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Time: {DateTime.UtcNow}, Level: Error, Message:{ex.Message}");
                return null;
            }
        }

        private static string TryGetProcessNameFromProcess(Process process)
        {
            try
            {
                return process.ProcessName;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Time: {DateTime.UtcNow}, Level: Error, Message:{ex.Message}");
                return null;
            }
        }

        private static long? TryGetNonpagedSystemMemorySize64FromProcess(Process process)
        {
            try
            {
                return process.NonpagedSystemMemorySize64;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Time: {DateTime.UtcNow}, Level: Error, Message:{ex.Message}");
                return null;
            }
        }

        private static long? TryGetpagedMemorySize64FromProcess(Process process)
        {
            try
            {
                return process.PagedMemorySize64;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Time: {DateTime.UtcNow}, Level: Error, Message:{ex.Message}");
                return null;
            }
        }
    }    
}
