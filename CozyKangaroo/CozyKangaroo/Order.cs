using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    enum OrderType
    {
        DineIn,
        Takeaway
    }
    enum OrderStatus
    {
        Placed,
        Preparing,
        Ready,
        Dining,
        Complete
    }

    class Order
    {
        // knows an order

        // calculates invoice total by the meals in order

        // don't forget to regex the payment details etc
        // DATA validation


        private int orderNumber;
        private List<Meal> meals;
        private OrderType orderType;
        private Person placedBy;
        private Table table;
        private Reservation reservation;
        private OrderStatus orderStatus;
        private bool paid;

        // Constructor
        public Order(int aOrderNumber, List<Meal> aMeals, OrderType aOrderType, Person aPlacedBy)
        {
            orderNumber = aOrderNumber;
            meals = aMeals;
            orderType = aOrderType;
            placedBy = aPlacedBy;

            orderStatus = OrderStatus.Placed;
            paid = false;
        }

        // Overloaded constructor (for dine-in)
        public Order(int aOrderNumber, List<Meal> aMeals, OrderType aOrderType, Person aPlacedBy, Table aTable, Reservation aReservation)
        {
            if (aOrderType != OrderType.DineIn)
            {
                // aTable and aReservation were parsed, so order needs to be of type DineIn
                // if it's not, error out
                throw new NotSupportedException();
            }

            orderNumber = aOrderNumber;
            meals = aMeals;
            orderType = aOrderType;
            placedBy = aPlacedBy;
            table = aTable;
            reservation = aReservation;

            orderStatus = OrderStatus.Placed;
            paid = false;
        }

        // Update Order Status
        public void setOrderStatus(OrderStatus aOrderStatus)
        {
            orderStatus = aOrderStatus;
        }

        // Pay for Order
        public Invoice pay()
        {
            // pay for the order and return Invoice

            Console.WriteLine("")


            return null;
        }


    }
}
