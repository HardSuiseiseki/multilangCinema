using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DesignPatternsTest.BehavioralPatterns.Strategy.Implementations;
using DesignPatternsTest.CreationalPatterns.AbstractFactory.Implementations;
using DesignPatternsTest.CreationalPatterns.AbstractFactory.Interfaces;
using DesignPatternsTest.CreationalPatterns.FactoryMethod.AbstractClasses;
using DesignPatternsTest.CreationalPatterns.FactoryMethod.Implementations;

namespace DesignPatternsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //FactoryMethodDemo();
            //AbstractFactoryPatternDemo();
            //StrategyDemo();
            //AsyncAwaitTesting.GetSiteInfo();
            //AsyncAwaitTesting.GetSiteInfoDetailedAsync();
            //AsyncAwaitTesting.GetSiteInfoLateAwaitAsync();
            //AsyncAwaitTesting.GetSiteInfoWithContinuation();
            //AsyncAwaitTesting.TestDelayCall();
            //AsyncAwaitTesting.TestExceptionCall();
            //RegexpTesting.TagEntranceWithNamedGroupsTest();
            //ThreadSyncTest.RunNotSyncedThreads();
            //ThreadSyncTest.RunSyncMonitorThreads();
            //ThreadSyncTest.RunSyncMutexThreads();

            //bool existed;
            //string guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();
            //Mutex mutexObj = new Mutex(true, guid, out existed);

            //if (existed)
            //{
            //    Console.WriteLine("Application is working");
            //}
            //else
            //{
            //    Console.WriteLine("Application is already running. Shutdown in 3 seconds");
            //    Thread.Sleep(3000);
            //    return;
            //}

            ThreadSyncTest.RunSyncSemaphoreThreads();

            Console.ReadKey();
        }

        private static void FactoryMethodDemo()
        {
            var winButtonCreator = new WinButtonCreator();
            var winButton = winButtonCreator.CreateButton();
            winButton.Paint();
            var osxButtonCreator = new OsxButtonCreator();
            var osxButton = osxButtonCreator.CreateButton();
            osxButton.Paint();
        }

        private static void CreateWindowWithTwoButtonsAndProgressBar(IGUIFactory factory)
        {
            var button1 = factory.CreateButton();
            var button2 = factory.CreateButton();
            var progressBar = factory.CreateProgressBar();
            button1.Paint();
            button2.Paint();
            progressBar.Paint();
            progressBar.SetProgress(45);
        }

        private static void AbstractFactoryPatternDemo()
        {
            var key = Console.ReadLine();
            IGUIFactory factory;

            switch (key)
            {
                case "W":
                    factory = new WinFactory();
                    break;
                case "O":
                    factory = new OsxFactory();
                    break;
                default:
                    factory = new WinFactory();
                    break;
            }

            CreateWindowWithTwoButtonsAndProgressBar(factory);
        }

        private static void StrategyDemo()
        {
            var hybridCar = new HybridCar(new PetrolMove());

            hybridCar.Move();

            hybridCar.Strategy = new ElectricMove();

            hybridCar.Move();
        }
    }
}
