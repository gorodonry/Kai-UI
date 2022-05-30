using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Kai_UI.ViewModels;

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
        protected int noOrdered = 0;
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

        public int NoOrdered
        {
            get { return noOrdered; }
            set
            {
                noOrdered = value;
                RaisePropertyChanged(nameof(NoOrdered));
            }
        }

        public string ImageURI
        {
            get { return imageURI; }
        }

        private DelegateCommand _incrementOrder;
        public DelegateCommand IncrementOrder =>
            _incrementOrder ?? (_incrementOrder = new DelegateCommand(ExecuteIncrementOrder, CanExecuteIncrementOrder));

        void ExecuteIncrementOrder()
        {
            NoOrdered++;
            DecrementOrder.RaiseCanExecuteChanged();
            
        }

        bool CanExecuteIncrementOrder()
        {
            return true;
        }

        private DelegateCommand _decrementOrder;
        public DelegateCommand DecrementOrder =>
            _decrementOrder ?? (_decrementOrder = new DelegateCommand(ExecuteDecrementOrder, CanExecuteDecrementOrder));

        void ExecuteDecrementOrder()
        {
            NoOrdered--;
            DecrementOrder.RaiseCanExecuteChanged();
        }

        bool CanExecuteDecrementOrder()
        {
            if (NoOrdered == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
