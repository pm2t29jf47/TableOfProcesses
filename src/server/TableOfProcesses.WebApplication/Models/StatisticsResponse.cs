using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TableOfProcesses.WebApplication.Models
{
    public class StatisticsResponse
    {   
        public long? SumNonpagedSystemMemorySize64InBytes { get; set; }

        public long? SumPagedMemorySize64InBytes { get; set; }

        public IEnumerable<ProcessItemResponse> processes { get; set; }
    }
}
