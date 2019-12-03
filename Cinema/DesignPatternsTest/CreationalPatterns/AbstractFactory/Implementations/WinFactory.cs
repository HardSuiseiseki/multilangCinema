﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsTest.CreationalPatterns.AbstractFactory.Interfaces;

namespace DesignPatternsTest.CreationalPatterns.AbstractFactory.Implementations
{
    public class WinFactory : IGUIFactory
    {
        public IButton CreateButton()
        {
            return new WinButton();
        }

        public IProgressBar CreateProgressBar()
        {
            return new WinProgressBar();
        }
    }
}
