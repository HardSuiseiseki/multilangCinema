using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cinema.Agent
{
    public class CacheWarmingUpAgent
    {
        const int WorkersCount = 1;
        private string LoggerName => GetType().FullName;

        public void Run()
        {
            var urlsConfig = Constants.UrlsToWarmUp;

            if (urlsConfig != null && urlsConfig.AllKeys.Any())
            {
                var urls = urlsConfig.AllKeys.Select(key => urlsConfig[key]).ToList();
                var queue = new ConcurrentQueue<string>(urls);
                var workers = StartWorkers(queue, WorkersCount);
                Task.WaitAll(workers.ToArray());
            }
        }

        private IEnumerable<Task> StartWorkers(ConcurrentQueue<string> queue, int workersCount)
        {
            var tasks = new List<Task>();

            for (var i = 0; i < workersCount; i++)
            {
                var worker = new CacheWarmingUpThread(queue, LoggerName);
                var workerTask = Task.Run(() => worker.Run());
                tasks.Add(workerTask);
            }

            return tasks;
        }
    }
}