using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kai_UI.Models
{
    public class Product : BindableBase
    {
        protected string name;
        protected string type;
        protected bool vOption;
        protected bool vGOption;
        protected bool containsSugar;
        protected double price;
        protected string imageURI;

        public Product(string name, string type, bool vOption, bool vGOption, bool containsSugar, double price, string imageURI)
        {
            this.name = name;
            this.type = type;
            this.vOption = vOption;
            this.vGOption = vGOption;
            this.containsSugar = containsSugar;
            this.price = price;
            this.imageURI = imageURI;
        }

        public string Name
        {
            get { return name; }
        }

        public string Type
        {
            get { return type; }
        }

        public bool VOption
        {
            get { return vOption; }
        }

        public bool VGOption
        {
            get { return vGOption; }
        }

        public bool ContainsSugar
        {
            get { return containsSugar; }
        }

        public double Price
        {
            get { return price; }
        }

        public string ImageURI
        {
            get { return imageURI; }
        }
    }

    public class SpecialProduct : Product
    {
        protected string dayAvailable;

        public SpecialProduct(string name, string type, bool vOption, bool vGOption, bool containsSugar, double price, string dayAvailable, string imageURI):base(name, type, vOption, vGOption, containsSugar, price, imageURI)
        {
            this.dayAvailable = dayAvailable;
        }

        public string DayAvailable
        {
            get { return dayAvailable; }
        }
    }
}
