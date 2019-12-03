using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsTest.CreationalPatterns.AbstractFactory.Interfaces;

namespace DesignPatternsTest.CreationalPatterns.AbstractFactory.Implementations
{
    public class OsxProgressBar : IProgressBar
    {
        public void Paint()
        {
            Console.WriteLine("Render a progress bar in a OSX style.");
        }

        public void SetProgress(int percent)
        {
            Console.WriteLine($"OSX progress bar value {percent}%.");
        }
    }
}
