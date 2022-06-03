using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    class Menu
    {
        private String name;
        private List<Meal> meals;


        public Menu(String aName, List<Meal> aMeals)
        {
            name = aName;
            meals = aMeals;
        }

        public Meal GetMeal(String name)
        {
            // fetch meal from menu with name
            return meals.Find(meal => meal.Name == name);
        }

        public void PrintMenu()
        {
            for (int i = 0; i < meals.Count; i++)
            {
                Console.WriteLine(String.Format("{0:000}", i) + "  -  " + meals[i].Name + "  -  " + meals[i].Description + "  -  " + meals[i].Price);
            }
        }
    }
}
