﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

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

        // Getters and Setters
        public int OrderNumber
        {
            get => orderNumber;
        }
        public List<Meal> Meals
        {
            get => meals;
        }
        public OrderType OrderType
        {
            get => orderType;

        }

        // Update Order Status
        public void setOrderStatus(OrderStatus aOrderStatus)
        {
            orderStatus = aOrderStatus;
        }

        public void addMeal(Meal meal)
        {
            meals.Add(meal);
        }

        public double OrderTotal()
        {
            // Calculate the total price of each item in the order
            double ltotal = 0.00;
            foreach(Meal meal in meals)
            {
                ltotal += meal.Price;
            }

            return Math.Round(ltotal, 2);
        }

        // Pay for Order
        public Invoice pay()
        {
            // Submethod for credit card validation:
            bool IsValidCard(String cardNumber, String cardCVV, String cardExpiry)
            {
                // IIN List from https://www.creditcardvalidator.org/country/au-australia
                Regex rCardNum = new Regex(@"^(37\d{13}|40[157]\d{13}|41[34]\d{13}|423953\d{10}|436356\d{10}|43638410\d{8}|443\d{13}|4[56789]\d{14}|5[1245]\d{15})");
                Regex rCardCVV = new Regex(@"^\d{3}");
                Regex rCardExp = new Regex(@"[0-9]{2}\/[23][0-9]");

                return (rCardNum.IsMatch(cardNumber) && rCardCVV.IsMatch(cardCVV) && rCardExp.IsMatch(cardExpiry));
            }

            // pay for the order and return Invoice
            Console.WriteLine("Please Pay for Order: " + orderNumber);

            while (!paid)                                                                       // Loop while the order isn't paid
            {
                switch (orderType)
                {
                    case OrderType.DineIn:                                                      // Dine-in allows for both card and cash payments.
                        Console.WriteLine("Dine-In Order:");
                        Console.WriteLine("Total Due: " + OrderTotal() + "\n");
                        Console.WriteLine("Please select your payment method:");
                        Console.WriteLine("  C    Cash");
                        Console.WriteLine("  E    EFTPOS / Credit Card");
                        char lSelection = Console.ReadLine()[0];

                        switch (lSelection)
                        {
                            case 'C':
                                Console.WriteLine("Paying Cash:");
                                // Wait staff would take payment and change
                                // Convert exact due to cash denomination:
                                double lCashDue = Math.Round(OrderTotal() / 100 / 5.0) * 5 * 100;
                                Console.WriteLine("Total Cash Due: " + lCashDue);
                                Console.WriteLine("Enter Cash Paid: (e.g. 12.34)");
                                double lCashPaid = Convert.ToDouble(Console.ReadLine());

                                // calculate change
                                Console.WriteLine("Change is: " + (lCashDue - lCashPaid));

                                // mark as paid
                                paid = true;
                                break;
                            case 'E':
                                Console.WriteLine("Paying by EFTPOS:");
                                // Wait staff would take payment with EFTPOS machine
                                Console.WriteLine("Total Due: " + OrderTotal());

                                // In real-life this would tie into some banking system.
                                Console.Write("Processing..");
                                for (int i = 0; i < 20; i++)
                                {
                                    Console.Write(".");
                                    Thread.Sleep(500);
                                }

                                // mark as paid
                                Console.WriteLine("Payment Complete. Thank you.");
                                paid = true;
                                break;
                            default:
                                Console.WriteLine("Please enter a valid selection.");
                                break;
                        }

                        break;
                    case OrderType.Takeaway:
                        Console.WriteLine("Dine-In Order:");
                        Console.WriteLine("Total Due: " + OrderTotal() + "\n");
                        Console.WriteLine("Credit Card Payment:");
                        bool lCardValid = false;

                        while (lCardValid == false)
                        {
                            Console.WriteLine("Please enter your Credit Card number:");
                            String lCeditCardNumber = Console.ReadLine();

                            Console.WriteLine("Please enter your Credit Card Verification (CVV) number:");
                            String lCeditCVV = Console.ReadLine();

                            Console.WriteLine("Please enter your Credit Card Expiry Date in format MM/YY (e.g. 04/25)");
                            String lCeditExp = Console.ReadLine();

                            lCardValid = IsValidCard(lCeditCardNumber, lCeditCVV, lCeditExp);
                            if (!lCardValid)
                            {
                                Console.WriteLine("Card details incorrect. Please check your details and try again.");
                            }
                        }

                        // mark as paid
                        paid = true;
                        Console.WriteLine("Payment Complete. Thank you.");
                        break;
                    default:
                        break;
                }
            }    

            // order is marked as paid.
            // create invoice and return it

            return new Invoice(this);
        }


    }
}
