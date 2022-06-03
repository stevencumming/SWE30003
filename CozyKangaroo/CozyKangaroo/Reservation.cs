using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKangaroo
{
    class Reservation
    {
        private List<Table> tables;

        public List<Table> Tables
        {
            get
            {
                return tables;
            }
        }

        public Reservation(List<Table> tables )
        {
            this.tables = tables;
        }

        public Table CreateReservation(Customer customer, int tableNumber, DateTime reservationTime)
        {
            // first of all, check if the table is availabe or not
            if (IsTableAvailable(reservationTime, tableNumber))
            {
                // if it is available, search for table index in the list
                int tableIndex = GetTableIndex(tableNumber);
                tables[tableIndex].TableStatus = TableStatus.Reserved;
                tables[tableIndex].CustomerInfo = customer;
                tables[tableIndex].ReservationTime = reservationTime;
                Console.WriteLine("Table has reserved successfully");
                return tables[tableIndex];
            }
            else
            {
                Console.WriteLine("Table has already reserved. Please select another table.");
            }

            return null;
        }

        public void CancelReservation(Customer cusomter, int tableNumber, DateTime reservationTime)
        {

            if (IsTableAvailable(reservationTime, tableNumber) == false)
            {
                int tableIndex = GetTableIndex(tableNumber);
                tables[tableIndex].TableStatus = TableStatus.Vacant;
                tables[tableIndex].CustomerInfo = null;
                tables[tableIndex].ReservationTime = null;
                Console.WriteLine("Reservation has been cancelled.");
            }
            else
            {
                Console.WriteLine("Invalid Input Data.");
            }


        }

        public void AddNewTable(Table table)
        {
            for (int i = 0; i < this.tables.Count; i++)
            {
                if (tables[i].TableNumber == table.TableNumber)
                {
                    Console.WriteLine("Table number already existed. Please try different table number. ");
                }
                else
                {
                    tables.Append(table);
                    Console.WriteLine("Table has successfully added.");
                }
            }
        }



        public int GetTableIndex(int tableNumber)
        {
            for(int i = 0; i< this.tables.Count; i++)
            {
                if(tables[i].TableNumber == tableNumber)
                {
                    return i;
                }
            }
            // table not found
            return -1;
        }
    
     
        public Boolean IsTableAvailable(DateTime reservationTime, int tableNumber)
        {

            foreach(Table t in tables)
            {
                if(t.TableNumber == tableNumber)
                {
                    if(t.TableStatus == TableStatus.Vacant && t.ReservationTime != reservationTime)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Table FindReservation(DateTime reservationTime, int tableNumber)
        {
            foreach(Table t in tables)
            {
                if(t.TableNumber == tableNumber)
                {
                    if(t.TableStatus == TableStatus.Vacant && t.ReservationTime != reservationTime)
                    {
                        return t;
                    }
                }
            }
            return null;
        }
    }
}
