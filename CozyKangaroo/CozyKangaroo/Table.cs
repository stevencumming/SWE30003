using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    enum TableStatus
    {
        Reserved,
        Vacant
    }

    class Table
    {
        private int tableNumber;
        private int numberOfSeats;
        private TableStatus tableStatus;
        private Customer customerInfo;
        private DateTime? reservationTime = null;
        
        public Table(int tableNumber, int numberOfSeats)
        {
            this.tableNumber = tableNumber;
            this.numberOfSeats = numberOfSeats;
            this.tableStatus = TableStatus.Vacant;
            this.customerInfo = null;

        }

        public int TableNumber
        {
            get
            {
                return tableNumber;
            }
        }

        public TableStatus TableStatus
        {
            get
            {
                return tableStatus;
            }

            set
            {
                this.tableStatus = value;
            }
        }

        public DateTime? ReservationTime
        {
            get
            {
                return reservationTime;
            }
            set
            {
                reservationTime = value;
            }

        }

        public Customer CustomerInfo
        {
            get
            {
                return customerInfo;
            }
            set
            {
                this.customerInfo = value;
            }
        }

        public int NumberOfSeats
        {
            get
            {
                return numberOfSeats;
            }
        }

        public override string ToString()
        {
            return "\nTable Reservation\n" +
                $"Number of Seats: {this.NumberOfSeats}\n" +
                $"Reservation Time: {this.ReservationTime}\n" +
                $"Table Number: {this.TableNumber}\n";
        }
    }
}
