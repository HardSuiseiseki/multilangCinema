using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsTest.CreationalPatterns.AbstractFactory.Interfaces;

namespace DesignPatternsTest.CreationalPatterns.AbstractFactory.Implementations
{
    public class OsxButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("Render a button in a OSX style.");
        }
    }
}
