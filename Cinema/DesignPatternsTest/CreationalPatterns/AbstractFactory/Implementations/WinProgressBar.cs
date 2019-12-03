using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsTest.CreationalPatterns.AbstractFactory.Interfaces;

namespace DesignPatternsTest.CreationalPatterns.AbstractFactory.Implementations
{
    public class WinProgressBar:IProgressBar
    {
        public void Paint()
        {
            Console.WriteLine("Render a progress bar in a Windows style.");
        }

        public void SetProgress(int percent)
        {
            Console.WriteLine($"Win progress bar value {percent}%.");
        }
    }
}
