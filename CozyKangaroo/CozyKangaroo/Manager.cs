using System;
using System.Collections.Generic;
using System.Text;

namespace CozyKangaroo
{
    class Manager: Person
    {
        private string username;
        private string password;

        public Manager(string name, string username, string password)
        {
            this.name = name;
            this.username = username;
            this.password = password;
        }

        public bool login(String username, String password) {
            return this.username == username && this.password == password;
        }

        public Report generateReport(int reportId) {
            string reportTypeStr;
            char reportTypeChar = ' ';
            do {
                if (reportTypeChar != ' ') {
                    Console.WriteLine("\nPlease enter a valid report type!\n");
                }
                Console.Write(
                    "Generate Report Type\n" +
                    "  A    All Invoice Report\n" +
                    "  X    Exit\n" +
                    "Select report type to generate: "
                );
                reportTypeStr = Console.ReadLine().Trim().ToUpper();
                if (reportTypeStr != "") {
                    reportTypeChar = reportTypeStr[0];
                }
            } while (reportTypeChar != 'A' && reportTypeChar != 'X');

            Report report = null;
            switch (reportTypeChar) {
                case 'A':
                    report = new Report(ApplicationFacade.Singleton.Invoices, reportId);
                    break;
                // Has room for extension
            }
            return report;
        }
    }
}
