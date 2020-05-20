using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TableOfProcesses.WebApplication.Notifications
{
    public class LongPollingInfrastructure
    {
        private static readonly List<LongPollingInfrastructure> instanses = new List<LongPollingInfrastructure>();        

        private readonly TaskCompletionSource<bool> notificationTask = new TaskCompletionSource<bool>();

        private int OneMinuteInMillesecond => 60 * 1000;

        private string NotificationMessage { get; set; }

        public LongPollingInfrastructure()
        {
            lock (instanses)
            {
                instanses.Add(this);
            }
        }       

        public async Task<string> WaitAsync(int milliseconds)
        {
            await Task.WhenAny(notificationTask.Task, Task.Delay(milliseconds));
            lock (instanses)
            {
                instanses.Remove(this);
            }
            return this.NotificationMessage;
        }

        public async Task<string> WaitAsync()
        {
            return await WaitAsync(OneMinuteInMillesecond);
        }

        public static void PublishNotification(string notificationMessage)
        {
            lock (instanses)
            {
                instanses.ToList().ForEach(x =>
                {
                    x.NotificationMessage = notificationMessage;
                    x.notificationTask.SetResult(true);
                });
            }
        }
    }
}
