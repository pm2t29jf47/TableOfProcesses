using System.Collections.Generic;

namespace TableOfProcesses.WebApplication.Models
{
    public class StatisticsResponse
    {
        /// <summary>
        /// Aggregated Nonpaged system memory size in bytes
        /// </summary>
        public long? SumNonpagedSystemMemorySize64InBytes { get; set; }

        /// <summary>
        /// Aggregated paged memory size in bytes
        /// </summary>
        public long? SumPagedMemorySize64InBytes { get; set; }

        /// <summary>
        /// System processes collection
        /// </summary>
        public IEnumerable<ProcessItemResponse> processes { get; set; }
    }
}
