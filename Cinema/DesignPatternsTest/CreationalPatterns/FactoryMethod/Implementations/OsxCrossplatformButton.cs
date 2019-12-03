using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsTest.CreationalPatterns.FactoryMethod.Interfaces;

namespace DesignPatternsTest.CreationalPatterns.FactoryMethod.Implementations
{
    public class OsxCrossplatformButton : ICrossplatformButton
    {
        public void Paint()
        {
            Console.WriteLine("Render a button in a OS X style.");
        }
    }
}
