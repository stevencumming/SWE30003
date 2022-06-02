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
            Char reportType;
            do {
                Console.Write(
                    "Generate Report Type\n" +
                    "  A    All Invoice Report\n" +
                    "  X    Exit\n" +
                    "Select report type to generate: "
                );
                reportType = Console.ReadLine().Trim()[0];
            } while (reportType != 'A' && reportType != 'X');

            Report report = null;
            switch (reportType) {
                case 'A':
                    report = new Report(ApplicationFacade.Singleton.Invoices, reportId);
                    break;
            }
            return report;
        }
    }
}
