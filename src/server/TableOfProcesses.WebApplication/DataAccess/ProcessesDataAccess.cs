using System.Diagnostics;
using TableOfProcesses.WebApplication.DataAccess.Interfaces;

namespace TableOfProcesses.WebApplication.DataAccess
{
    public class ProcessesDataAccess : IProcessesDataAccess
    {
        public Process[] GetProcesses()
        {
            return Process.GetProcesses();
        }
    }
}
