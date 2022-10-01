namespace TableOfProcesses.WebApplication.Models
{
    public class ProcessItemResponse
    {
        /// <summary>
        /// Process id
        /// </summary>
        public int? Pid { get; set; }

        /// <summary>
        /// Process name
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Nonpaged system memory size in bytes
        /// </summary>
        public long? NonpagedSystemMemorySize64InBytes { get; set; }

        /// <summary>
        /// Paged memory size in bytes
        /// </summary>
        public long? PagedMemorySize64InBytes { get; set; }

        /// <summary>
        /// Working set in bytes
        /// </summary>
        public long? WorkingSet64InBytes { get; set; }

        /// <summary>
        /// Time on processor
        /// </summary>
        public double? TotalProcessorTimeInMilliseconds { get; set; }
    }
}
