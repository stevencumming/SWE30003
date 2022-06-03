using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    class Report
    {
        private int reportID;
        List<Invoice> allOrders;
        List<string> timeAndDates;
        // Constructor
        public Report(List<Invoice> orders, int id)
        {
            reportID = id;
            allOrders = orders;
            TimeAndDates();
        }
        
        public void reportAllOrders()
        {
            Console.Clear();
            Console.WriteLine("------ Report ------\n");
            Console.WriteLine("All Orders");
            Console.WriteLine("Report ID:" + reportID);
            Console.WriteLine("Number of all time orders: "+ allOrders.Count);
            Console.WriteLine("Total earnings of all time orders: " + totalEarnings());
            Console.WriteLine("list of ordered items: " );
            Console.WriteLine(itemsInOrder());

            
        }
        // Getters and Setters
        public int ReportID
        {
            get => reportID;
            set => reportID = value;
        }
        private string itemsInOrder()
        {
            string allItems = "";
            foreach (Invoice invoice in allOrders)
            {
                foreach (Meal meal in invoice.Meals)
                {
                    allItems += meal.Name + ", ";
                }
            }

            return allItems;
        }

        private Double totalEarnings()
        {
            Double totalmoney = 0;
            foreach (Invoice invoice in allOrders)
            {
                foreach (Meal meal in invoice.Meals)
                {
                    totalmoney += meal.Price;
                }
            }
            return totalmoney;
        }
        private void TimeAndDates()
        {
            List<string> allTandD = new List<string>();
            for (int i = 0; i > allOrders.Count; i++)
            {
                for (int x = 0; i > allOrders[i].Meals.Count; x++)
                {
                    allTandD[i] = allTandD + allOrders[i].TimeandDate;
                }
            }

            timeAndDates = allTandD;
        }
    }
}
