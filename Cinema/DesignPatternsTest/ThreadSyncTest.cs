using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternsTest
{
    public static class ThreadSyncTest
    {
        private static int x = 0;

        public static void RunNotSyncedThreads()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => Count());
            }
            Console.ReadLine();
        }

        public static void Count()
        {
            Console.WriteLine("Thread started:{0}", Thread.CurrentThread.ManagedThreadId);
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Thread {0}:{1}", Thread.CurrentThread.ManagedThreadId, x);
                x++;
                Thread.Sleep(100);
            }
        }

        #region Lock
        public static object Locker = new object();
        public static void RunSyncLockThreads()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => SynchronizedCount());
            }
            Console.ReadLine();
        }

        public static void SynchronizedCount()
        {
            Console.WriteLine("Thread started:{0}", Thread.CurrentThread.ManagedThreadId);
            lock (Locker)
            {
                x = 1;
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine("Thread {0}:{1}", Thread.CurrentThread.ManagedThreadId, x);
                    x++;
                    Thread.Sleep(100);
                }
            }

            for (int j = 9; j >= 0; j--)
            {
                Console.WriteLine("Thread profit {0}:{1}", Thread.CurrentThread.ManagedThreadId, j);
                Thread.Sleep(100);
            }
        }
        #endregion

        #region Monitor

        public static object Mon = new object();
        public static void RunSyncMonitorThreads()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => SynchronizedMonitorCount());
            }
            Console.ReadLine();
        }

        public static void SynchronizedMonitorCount()
        {
            Console.WriteLine("Thread started:{0}", Thread.CurrentThread.ManagedThreadId);
            try
            {
                Monitor.Enter(Mon);
                x = 1;
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine("Thread {0}:{1}", Thread.CurrentThread.ManagedThreadId, x);
                    x++;
                    Thread.Sleep(100);
                }
            }
            finally
            {
                Monitor.Exit(Mon);
            }

            for (int j = 9; j >= 0; j--)
            {
                Console.WriteLine("Thread profit {0}:{1}", Thread.CurrentThread.ManagedThreadId, j);
                Thread.Sleep(100);
            }
        }
        #endregion

        #region Mutex

        public static Mutex mutex = new Mutex();
        public static void RunSyncMutexThreads()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => SynchronizedMutexCount());
            }
            Console.ReadLine();
        }

        public static void SynchronizedMutexCount()
        {
            Console.WriteLine("Thread started:{0}", Thread.CurrentThread.ManagedThreadId);

            mutex.WaitOne();
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Thread {0}:{1}", Thread.CurrentThread.ManagedThreadId, x);
                x++;
                Thread.Sleep(100);
            }
            mutex.ReleaseMutex();

            for (int j = 9; j >= 0; j--)
            {
                Console.WriteLine("Thread profit {0}:{1}", Thread.CurrentThread.ManagedThreadId, j);
                Thread.Sleep(100);
            }
        }

        #endregion

        #region Semaphore

        public static Semaphore semaphore = new Semaphore(0, 4);
        public static void RunSyncSemaphoreThreads()
        {
            Task.Run(() => ReadFile());

            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => ProcessFile());
            }
            Console.ReadLine();
        }

        public static void ReadFile()
        {
            Console.WriteLine("File read started:{0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(500);
            Console.WriteLine("File read ended:{0}", Thread.CurrentThread.ManagedThreadId);
            semaphore.Release(2);
        }

        public static void ProcessFile()
        {
            Console.WriteLine("Waiting for data:{0}", Thread.CurrentThread.ManagedThreadId);
            
            semaphore.WaitOne();
            Console.WriteLine("File processing started:{0}", Thread.CurrentThread.ManagedThreadId);
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Thread {0}:{1}", Thread.CurrentThread.ManagedThreadId, x);
                x++;
                Thread.Sleep(100);
            }
            Console.WriteLine("File processing ended:{0}", Thread.CurrentThread.ManagedThreadId);
        }

        #endregion
    }
}
