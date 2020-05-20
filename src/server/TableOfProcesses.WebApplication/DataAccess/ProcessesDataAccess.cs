using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
