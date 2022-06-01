using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    class Report
    {
        private int reportID;
        List<Invoice> allOrders;
        // Constructor
        public Report(List<Invoice> orders,int id)
        {
            reportID = id;
            allOrders = orders;
        }
        
        public void reportAllOrders()
        {
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
            for (int i = 0; i > allOrders.Count; i++)
            {
                for (int x = 0; i > allOrders[i].Meals.Count; x++)
                {
                    allItems = allItems + allOrders[i].Meals[x].Name + ", ";
                }
            }

            return allItems;
        }

        private Double totalEarnings()
        {
            Double totalmoney = 0;
            for(int i = 0; i > allOrders.Count; i++)
            {
                for (int x = 0; i > allOrders[i].Meals.Count; x++)
                {
                    totalmoney = totalmoney + allOrders[i].Meals[x].Price;
                }
            }

            return totalmoney;
        }
    }
}
