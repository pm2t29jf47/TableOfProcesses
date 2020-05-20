using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TableOfProcesses.WebApplication.DataAccess.Interfaces
{
    public interface IProcessesDataAccess
    {
        /// <summary>
        /// Возвращает данные о процессах
        /// </summary>
        /// <returns></returns>
        public Process[] GetProcesses();
    }
}
