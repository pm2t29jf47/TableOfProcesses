using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TableOfProcesses.WebApplication.Models
{
    public static class Helpers
    {
        public static ProcessItemResponse Map(Process process)
        {
            return new ProcessItemResponse()
            {
                Pid = process.Id,
                Command = process.ProcessName,
                NonpagedSystemMemorySize64InBytes = process.NonpagedSystemMemorySize64,
                PagedMemorySize64InBytes = process.PagedMemorySize64
            };
        }
    }
}
