using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsTest.BehavioralPatterns.Strategy.Interfaces;

namespace DesignPatternsTest.BehavioralPatterns.Strategy.Implementations
{
    public class ElectricMove : IMovable
    {
        public void Move()
        {
            Console.WriteLine("Moving with electricity");
        }
    }
}
