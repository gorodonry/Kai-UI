using Kai_UI.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Kai_UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // Title of the window
        private string _title = "Kai UI";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Model data = new();
        public Model Data
        {
            get { return data; }
            set { SetProperty(ref data, value); }
        }

        public List<string> DaysOfTheSchoolWeek
        {
            get { return Data.DaysOfTheSchoolWeek; }
        }

        // Observable collection of all sandwiches
        private ObservableCollection<Product> sandwiches = new()
        {
            new Product("Ham & egg sandwich", "sandwich", false, false, false, 3.5, "/Images/ham_and_egg_sandwich.jpg"),
            new Product("Chicken mayo sandwich", "sandwich", false, false, false, 3.5, "/Images/chicken_mayo_sandwich.jpg"),
            new Product("Egg sandwich", "sandwich", true, false, false, 3, "/Images/egg_sandwich.jpg"),
            new Product("Beef sandwich", "sandwich", false, false, false, 3.8, "/Images/beef_sandwich.jpg"),
            new Product("Salad sandwich", "sandwich", true, true, false, 3.2, "/Images/salad_sandwich.jpg")
        };

        public ObservableCollection<Product> Sandwiches
        {
            get { return sandwiches; }
            set { SetProperty(ref sandwiches, value); }
        }

        // Observable collection of all sushi
        private ObservableCollection<Product> sushi = new()
        {
            new Product("Chicken (3pc)", "sushi", false, false, false, 4.5, "/Images/chicken_sushi.jpg"),
            new Product("Tuna (3pc)", "sushi", false, false, false, 4.5, "/Images/tuna_sushi.jpg"),
            new Product("Avocado sushi (3pc)", "sushi", true, true, false, 4.8, "/Images/avocado_sushi.jpg"),
            new Product("Chicken rice bowl", "sushi", false, false, false, 5.5, "/Images/chicken_rice_bowl.jpg"),
            new Product("Vegetarian rice bowl", "sushi", true, true, false, 5.5, "/Images/vegetarian_rice_bowl.jpg")
        };

        public ObservableCollection<Product> Sushi
        {
            get { return sushi; }
            set { SetProperty(ref sushi, value); }
        }

        // Observable collection of all drinks
        private ObservableCollection<Product> drinks = new()
        {
            new Product("Soda can", "drink", true, true, true, 2, "/Images/soda_can.jpg"),
            new Product("Aloe vera drink", "drink", true, false, true, 3.5, "/Images/aloe_vera_drink.jpg"),
            new Product("Chocolate milk", "drink", false, false, true, 3.5, "/Images/chocolate_milk.jpg"),
            new Product("Water bottle", "drink", true, true, false, 2.5, "/Images/water_bottle.jpg"),
            new Product("Instant hot chocolate", "drink", true, false, true, 1.5, "/Images/instant_hot_chocolate.jpg")
        };

        public ObservableCollection<Product> Drinks
        {
            get { return drinks; }
            set { SetProperty(ref drinks, value); }
        }

        // Observable collection of all kai unique to specific days
        private ObservableCollection<SpecialProduct> specialProducts = new()
        {
            new SpecialProduct("Kale moa", "special", false, false, true, 6, "monday", "/Images/kale_moa.jpg"),
            new SpecialProduct("Potjiekos", "special", false, false, false, 6, "tuesday", "/Images/potjiekos.jpg"),
            new SpecialProduct("Hangi", "special", true, true, false, 6, "wednesday", "/Images/hangi.jpg"),
            new SpecialProduct("Paneer tikka masala", "special", true, false, true, 6, "thursday", "/Images/paneer_tikka_masala.jpg"),
            new SpecialProduct("Chow mein", "special", true, true, false, 6, "friday", "/Images/chow_mein.jpg")
        };

        public ObservableCollection<SpecialProduct> SpecialProducts
        {
            get { return specialProducts; }
            set { SetProperty(ref specialProducts, value); }
        }

        // Day of the week in index form, bound to a combobox that allows the user to select the day
        private string _dayOfTheSchoolWeek = string.Empty;
        public string DayOfTheSchoolWeek
        {
            get { return _dayOfTheSchoolWeek; }
            set
            { 
                SetProperty(ref _dayOfTheSchoolWeek, value);

                // Reset the number of ordered items for each special item if the day changes
                for (int i=0; i<SpecialProducts.Count(); i++)
                {
                    SpecialProducts[i].NoOrdered = 0;
                }

                // Alert relevant views to the change
                RaisePropertyChanged(nameof(DayOfTheSchoolWeekName));
                RaisePropertyChanged(nameof(EnableSpecialItemSelection));
                RaisePropertyChanged(nameof(TodaysSpecialProducts));
                RaisePropertyChanged(nameof(TotalPrice));
            }
        }

        // Set to true if the user specifies a day, special items are only available on certain days
        private bool _enableSpecialItemSelection = false;
        public bool EnableSpecialItemSelection
        {
            get { return _enableSpecialItemSelection; }
            set { SetProperty(ref _enableSpecialItemSelection, value); }
        }

        // Converts index form into a name that can be displayed to the user
        public string DayOfTheSchoolWeekName
        {
            get
            {
                // Only allow the election of daily specials if the day is selected
                try
                {
                    string day = DaysOfTheSchoolWeek[Int32.Parse(DayOfTheSchoolWeek)];
                    EnableSpecialItemSelection = true;
                    return day;
                }
                catch (ArgumentOutOfRangeException)
                {
                    EnableSpecialItemSelection = false;
                    return "Daily";
                }
            }
        }

        // Observable collection of products unique to the selected day
        public ObservableCollection<Product> TodaysSpecialProducts
        {
            get
            {
                string day = DayOfTheSchoolWeekName;
                
                if (day != "Daily")
                {
                    ObservableCollection<Product> todaysSpecialProducts = new();

                    foreach (SpecialProduct product in SpecialProducts)
                    {
                        if (product.DayAvailable == day.ToLower())
                        {
                            todaysSpecialProducts.Add(product);
                        }
                    }

                    return todaysSpecialProducts;
                }
                else
                {
                    return new ObservableCollection<Product>();
                }
            }
        }

        // Observable collection of all products, indiscriminate of type, used by the view order drawer and the cart UI when displaying the order
        public ObservableCollection<Product> AllProducts
        {
            get
            {
                ObservableCollection<Product> allProducts = new();

                allProducts.AddRange(Sandwiches);
                allProducts.AddRange(Sushi);
                allProducts.AddRange(Drinks);
                allProducts.AddRange(SpecialProducts);

                return allProducts;
            }
        }

        // Increase the number of the specified product ordered by one, always available as an option
        private DelegateCommand<Product> _incrementOrder;
        public DelegateCommand<Product> IncrementOrder =>
            _incrementOrder ?? (_incrementOrder = new DelegateCommand<Product>(ExecuteIncrementOrder, CanExecuteIncrementOrder));

        void ExecuteIncrementOrder(Product product)
        {
            product.NoOrdered++;
            DecrementOrder.RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(TotalPrice));
            SubmitOrder.RaiseCanExecuteChanged();
        }

        bool CanExecuteIncrementOrder(Product product)
        {
            return true;
        }

        // Decrease the number of the specified product ordered by one, available if applying the command won't cause a negative order
        private DelegateCommand<Product> _decrementOrder;
        public DelegateCommand<Product> DecrementOrder =>
            _decrementOrder ?? (_decrementOrder = new DelegateCommand<Product>(ExecuteDecrementOrder, CanExecuteDecrementOrder));

        void ExecuteDecrementOrder(Product product)
        {
            product.NoOrdered--;
            DecrementOrder.RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(TotalPrice));
            SubmitOrder.RaiseCanExecuteChanged();
        }

        bool CanExecuteDecrementOrder(Product product)
        {
            if (product.NoOrdered == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Total price of the order
        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;

                foreach (Product product in AllProducts)
                {
                    totalPrice += product.Price * product.NoOrdered;
                }
                return totalPrice;
            }
        }

        // Name of the user, bound to a textbox in the cart UI
        private string _userName = string.Empty;
        public string UserName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value);
                SubmitOrder.RaiseCanExecuteChanged();
            }
        }

        // Command called by the 'submit order' button
        private DelegateCommand _submitOrder;
        public DelegateCommand SubmitOrder =>
            _submitOrder ?? (_submitOrder = new DelegateCommand(ExecuteSubmitOrder, CanExecuteSubmitOrder));

        void ExecuteSubmitOrder()
        {

        }

        bool CanExecuteSubmitOrder()
        {
            // User must have entered a name and ordered something to submit an order to the canteen
            if (UserName.Trim().Length != 0 && TotalPrice != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MainWindowViewModel()
        {
            
        }
    }
}
