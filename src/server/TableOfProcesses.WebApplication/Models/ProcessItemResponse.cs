using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TableOfProcesses.WebApplication.Models
{
    public class ProcessItemResponse
    {
        public int? Pid { get; set; }

        public string Command { get; set; }

        public long? NonpagedSystemMemorySize64InBytes { get; set; }

        public long? PagedMemorySize64InBytes { get; set; }   
        
        public long? WorkingSet64InBytes { get; set; }

        public double? TotalProcessorTimeInMilliseconds { get; set; }
    }
}
