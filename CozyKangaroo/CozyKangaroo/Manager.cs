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

        public bool generateReport() {
            return true;
        }

        public string viewReport(int reportId) {
            return "report text";
        }
    }
}
