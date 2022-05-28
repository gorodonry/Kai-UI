﻿using Kai_UI.Models;
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
        private string _title = "Kai UI";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly List<string> daysOfTheSchoolWeek = new List<string>()
        {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thrusday",
            "Friday"
        };

        public List<string> DaysOfTheSchoolWeek
        {
            get { return daysOfTheSchoolWeek; }
        }

        private List<Product> sandwiches = new()
        {
            new Product("Ham & egg sandwich", "sandwich", false, false, false, 3.5, "/Images/ham_and_egg_sandwich.jpg"),
            new Product("Chicken mayo sandwich", "sandwich", false, false, false, 3.5, "/Images/chicken_mayo_sandwich.jpg"),
            new Product("Egg sandwich", "sandwich", true, false, false, 3, "/Images/egg_sandwich.jpg"),
            new Product("Beef sandwich", "sandwich", false, false, false, 3.8, "/Images/beef_sandwich.jpg"),
            new Product("Salad sandwich", "sandwich", true, true, false, 3.2, "/Images/salad_sandwich.jpg")
        };

        public List<Product> Sandwiches
        {
            get { return sandwiches; }
        }

        private List<Product> sushi = new()
        {
            new Product("Chicken (3pc)", "sushi", false, false, false, 4.5, "/Images/chicken_sushi.jpg"),
            new Product("Tuna (3pc)", "sushi", false, false, false, 4.5, "/Images/tuna_sushi.jpg"),
            new Product("Avocado sushi (3pc)", "sushi", true, true, false, 4.8, "/Images/avocado_sushi.jpg"),
            new Product("Chicken rice bowl", "sushi", false, false, false, 5.5, "/Images/chicken_rice_bowl.jpg"),
            new Product("Vegetarian rice bowl", "sushi", true, true, false, 5.5, "/Images/vegetarian_rice_bowl.jpg")
        };

        public List<Product> Sushi
        {
            get { return sushi; }
        }

        private List<Product> drinks = new()
        {
            new Product("Soda can", "drink", true, true, true, 2, "/Images/soda_can.jpg"),
            new Product("Aloe vera drink", "drink", true, false, true, 3.5, "/Images/aloe_vera_drink.jpg"),
            new Product("Chocolate milk", "drink", false, false, true, 3.5, "/Images/chocolate_milk.jpg"),
            new Product("Water bottle", "drink", true, true, false, 2.5, "/Images/water_bottle.jpg"),
            new Product("Instant hot chocolate", "drink", true, false, true, 1.5, "/Images/instant_hot_chocolate.jpg")
        };

        public List<Product> Drinks
        {
            get { return drinks; }
        }

        private List<SpecialProduct> specialProducts = new()
        {
            new SpecialProduct("Kale moa", "special", false, false, true, 6, "monday", "/Images/kale_moa.jpg"),
            new SpecialProduct("Potjiekos", "special", false, false, false, 6, "tuesday", "/Images/potjiekos.jpg"),
            new SpecialProduct("Hangi", "special", true, true, false, 6, "wednesday", "/Images/hangi.jpg"),
            new SpecialProduct("Paneer tikka masala", "special", true, false, true, 6, "thursday", "/Images/paneer_tikka_masala.jpg"),
            new SpecialProduct("Chow mein", "special", true, true, false, 6, "friday", "/Images/chow_mein.jpg")
        };

        public List<SpecialProduct> SpecialProducts
        {
            get { return specialProducts; }
        }

        private string _dayOfTheSchoolWeek = string.Empty;
        public string DayOfTheSchoolWeek
        {
            get { return _dayOfTheSchoolWeek; }
            set
            { 
                SetProperty(ref _dayOfTheSchoolWeek, value);
                RaisePropertyChanged("DayOfTheSchoolWeekName");
                RaisePropertyChanged("EnableSpecialItemSelection");
            }
        }

        private bool _enableSpecialItemSelection = false;
        public bool EnableSpecialItemSelection
        {
            get { return _enableSpecialItemSelection; }
            set { SetProperty(ref _enableSpecialItemSelection, value); }
        }

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

        public MainWindowViewModel()
        {
            
        }
    }
}