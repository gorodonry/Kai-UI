﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kai_UI.Models
{
    public class SpecialProduct : Product
    {
        // Subclass of product that is only for sale on a certain day of the week
        protected string dayAvailable;

        public SpecialProduct(string name, string type, bool vOption, bool vGOption, bool containsSugar, double price, string dayAvailable, string imageURI) : base(name, type, vOption, vGOption, containsSugar, price, imageURI)
        {
            this.dayAvailable = dayAvailable;
        }

        public string DayAvailable
        {
            get { return dayAvailable; }
        }
    }
}
