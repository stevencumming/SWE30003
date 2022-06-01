using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    class Report
    {
        // Constructor
        public Report(Order order)
        {
            invoiceOrder = order;
            purchasedMeals = order.Meals;
        }
    }
}
