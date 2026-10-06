using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyCoffeeApp2
{
    public class CoffeeService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            
            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Coffee>();
        }

        public static async Task AddCoffee(string name, string roaster, string description)
        {
            await Init();
            var image = "https://img.magnific.com/premium-vector/pixel-art-illustration-mug-coffee-pixelated-mug-coffee-mug-pixelated-pixel-art-game_1038602-1101.jpg?semt=ais_hybrid&w=740&q=80";
            var coffee = new Coffee
            {
                Name = name,
                Roaster = roaster,
                Description = description,
                Image = image
            };

            var id = await db.InsertAsync(coffee);
        }

        public static async Task RemoveCoffee(int id)
        {
            await Init();

            await db.DeleteAsync<Coffee>(id);
        }

        public static async Task<IEnumerable<Coffee>> GetCoffee()
        {
            await Init();

            var coffee = await db.Table<Coffee>().ToListAsync();
            return coffee;
        }
        
    }
}
