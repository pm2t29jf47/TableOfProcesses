using TableOfProcesses.WebApplication.Models;

namespace TableOfProcesses.WebApplication.Services
{
    /// <summary>
    /// Business logic layer
    /// </summary>
    public interface IProcessService
    {
        /// <summary>
        /// Statistics about processes
        /// </summary>        
        public StatisticsResponse GetStatistics();
    }
}
