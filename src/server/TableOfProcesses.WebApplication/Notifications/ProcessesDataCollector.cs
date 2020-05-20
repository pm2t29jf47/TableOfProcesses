using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TableOfProcesses.WebApplication.DataAccess.Interfaces;
using TableOfProcesses.WebApplication.Helpers;

namespace TableOfProcesses.WebApplication.Notifications
{
	public class ProcessesDataCollector : IHostedService
	{
		private readonly IProcessesDataAccess processesDataAccess;
		public ProcessesDataCollector(IProcessesDataAccess processesDataAccess)
		{
			this.processesDataAccess = processesDataAccess;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(async () =>
			{
				double previousTotalProcessesTimeMs = 0;				
				var previousTimeStamp = DateTime.UtcNow;								
				double cpuThreshold = 0.1;
				long bytesInGigabyte = 1000000000;
				long memThreshold = bytesInGigabyte * 5;
				while (!cancellationToken.IsCancellationRequested)
				{
					var processes = processesDataAccess.GetProcesses();
					double totalProcessesTimeMs = 0;
					long totalProcessesMemorySizeInBytes = 0;
					var timeStamp = DateTime.UtcNow;
					var cpuTimeSpanMs = (timeStamp - previousTimeStamp).TotalMilliseconds * Environment.ProcessorCount;
					var cpuThresholdTimeSpanMs = cpuTimeSpanMs * cpuThreshold;

					foreach (var process in processes)
					{
						try
						{
							totalProcessesTimeMs += process.TotalProcessorTime.TotalMilliseconds;
							totalProcessesMemorySizeInBytes += process.WorkingSet64;
						}
						catch (InvalidOperationException) { }
						catch (Win32Exception) { }
					}

					var totalProcessesTimeSpanMs = totalProcessesTimeMs - previousTotalProcessesTimeMs;					

					if (totalProcessesTimeSpanMs > cpuThresholdTimeSpanMs || totalProcessesMemorySizeInBytes > memThreshold)
					{
						var cpuValue = Math.Round((totalProcessesTimeSpanMs / cpuTimeSpanMs) * 100, 2);
						var memValue = Math.Round((double)totalProcessesMemorySizeInBytes / bytesInGigabyte, 2);
						var message = $"Attention! CPU: {cpuValue} %, RAM: {memValue} GB";
						LongPollingInfrastructure.PublishNotification(message);
					}					

					try
					{
						await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
					}
					catch (OperationCanceledException) { }

					previousTotalProcessesTimeMs = totalProcessesTimeMs;
					previousTimeStamp = timeStamp;
				}
			}, cancellationToken);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
