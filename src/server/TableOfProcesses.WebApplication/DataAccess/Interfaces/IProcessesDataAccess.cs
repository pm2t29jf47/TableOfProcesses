using System.Diagnostics;

namespace TableOfProcesses.WebApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Process data layer
    /// </summary>
    public interface IProcessesDataAccess
    {
        /// <summary>
        /// All system processes collection
        /// </summary>        
        public Process[] GetProcesses();
    }
}
