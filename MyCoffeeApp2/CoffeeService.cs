using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyCoffeeApp2
{
    internal class CoffeeService
    {
        static async Task Init()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            var db = new SQLiteAsyncConnection(databasePath);
        }
        public static async Task AddCoffee(string name, string roaster)
        {

        }
        public static async Task RemoveCoffee(int id)
        {

        }
        public static async Task GetCoffee()
        {

        }
        public static async Task Coffee()
        {

        }
    }
}
