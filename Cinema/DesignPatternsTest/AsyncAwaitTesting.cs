using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternsTest
{
    public static class AsyncAwaitTesting
    {
        public static void GetSiteInfo()
        {
            var googleResult = new WebClient().DownloadData("https://google.com/");
            Console.WriteLine($"Google loaded! Content lenght:{googleResult.Length}");

            var yandexResult = new WebClient().DownloadData("https://yandex.ru/");
            Console.WriteLine($"Yandex loaded! Content lenght:{yandexResult.Length}");
        }

        public static async void GetSiteInfoAsync()
        {
            var googleResult = await new WebClient().DownloadDataTaskAsync("https://google.com/");
            //{
                Console.WriteLine($"Google loaded! Content lenght:{googleResult.Length}");

                var yandexResult = await new WebClient().DownloadDataTaskAsync("https://yandex.ru/");
                    //{
                        Console.WriteLine($"Yandex loaded! Content lenght:{yandexResult.Length}");
                    //}
            //}
        }

        public static async void GetSiteInfoDetailedAsync()
        {
            var googleResultTask = new WebClient().DownloadDataTaskAsync("https://google.com/").ConfigureAwait(false);
            Console.WriteLine($"Google task thread id:{Thread.CurrentThread.ManagedThreadId}");
            var googleResult = await googleResultTask;
            Console.WriteLine($"Google task continuation thread id:{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Google loaded! Content lenght:{googleResult.Length}");

            var yandexResultTask = new WebClient().DownloadDataTaskAsync("https://yandex.ru/").ConfigureAwait(false);
            Console.WriteLine($"Yandex task thread id:{Thread.CurrentThread.ManagedThreadId}");
            var yandexResult = await yandexResultTask;
            Console.WriteLine($"Yandex task continuation thread id:{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Yandex loaded! Content lenght:{yandexResult.Length}");
        }

        public static async void GetSiteInfoLateAwaitAsync()
        {
            var googleResultTask = new WebClient().DownloadDataTaskAsync("https://google.com/");
            var yandexResultTask = new WebClient().DownloadDataTaskAsync("https://yandex.ru/");

            Console.WriteLine($"Before await thread id:{Thread.CurrentThread.ManagedThreadId}");

            var googleResult = await googleResultTask;
            var yandexResult = await yandexResultTask;
            Console.WriteLine($"After await thread id:{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Google loaded! Content lenght:{googleResult.Length}");
            Console.WriteLine($"Yandex loaded! Content lenght:{yandexResult.Length}");
        }

        public static void GetSiteInfoWithContinuation()
        {
            new WebClient().DownloadDataTaskAsync("https://google.com/")
                .ContinueWith(googleTask =>
                {
                    Console.WriteLine($"Google loaded! Content lenght:{googleTask.Result.Length}");
                    Console.WriteLine($"Google task thread id:{Thread.CurrentThread.ManagedThreadId}");
                    return new WebClient().DownloadDataTaskAsync("https://yandex.ru/");
                }).Unwrap() //Task<Task<byte[]>> => Task<byte[]>
                .ContinueWith(yandexTask =>
                {
                    Console.WriteLine($"Yandex task thread id:{Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"Yandex loaded! Content lenght:{yandexTask.Result.Length}");
                });

        }

        public static async void TestDelayCall()
        {
            Console.WriteLine("Method execution start");
            Console.WriteLine($"Before delay thread id:{Thread.CurrentThread.ManagedThreadId}");
            await DelayAsync();
            Console.WriteLine($"After delay thread id:{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Method execution end");
        }

        public static async void TestExceptionCall()
        {
            Console.WriteLine("Method execution start");
            try
            {
                await Task.Delay(1000);
                TestExceptionAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Method execution end");
        }

        public static async void TestExceptionAsync()
        {
            await Task.Delay(3000);
            throw new Exception("Some wrong argument exception");
        }

        private static async Task DelayAsync()
        {
            await Task.Delay(1000);
        }
    }
}
