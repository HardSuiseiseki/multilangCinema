using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTest.CreationalPatterns.AbstractFactory.Interfaces
{
    public interface IProgressBar
    {
        void Paint();
        void SetProgress(int percent);
    }
}
