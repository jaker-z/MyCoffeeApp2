using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
 

namespace MyCoffeeApp2
{
    public partial class MyCoffeeViewModel : BaseViewModel
    {
        public ObservableCollection<Coffee> Coffee { get; } = new();

        [RelayCommand]
        async Task AddAsync()
        {
            var name = await Shell.Current.DisplayPromptAsync("Coffee Name:", "Enter the name of the coffee.");
            var roaster = await Shell.Current.DisplayPromptAsync("Roaster Name:", "Enter the name of the roaster.");
            var description = await Shell.Current.DisplayPromptAsync("Description:", "Enter the description of the coffee");

            await CoffeeService.AddCoffee(name, roaster, description);
            await RefreshAsync();
        }

        [RelayCommand]
        async Task RemoveAsync(Coffee coffee)
        {
            await CoffeeService.RemoveCoffee(coffee.Id);
            await RefreshAsync();
        }

        [RelayCommand]
        async Task UpdateAsync(Coffee coffee)
        {
            var name = await Shell.Current.DisplayPromptAsync("Coffee Name:", "Enter the name of the coffee.");
            var roaster = await Shell.Current.DisplayPromptAsync("Roaster Name:", "Enter the name of the roaster.");
            var description = await Shell.Current.DisplayPromptAsync("Description:", "Enter the description of the coffee");
            var image = "https://img.magnific.com/premium-vector/pixel-art-illustration-mug-coffee-pixelated-mug-coffee-mug-pixelated-pixel-art-game_1038602-1101.jpg?semt=ais_hybrid&w=740&q=80";

            var updatedCoffee = new Coffee
            {
                Id = coffee.Id,
                Name = name,
                Roaster = roaster,
                Description = description,
                Image = image
            };

            await CoffeeService.UpdateCoffee(updatedCoffee);
            await RefreshAsync();
        }

        [RelayCommand]
        async Task RefreshAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var coffees = await CoffeeService.GetCoffee();

                Coffee.Clear();

                foreach (var coffee in coffees)
                    Coffee.Add(coffee);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlertAsync("Error!", $"Unable to get coffees: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToDetailsAsync(Coffee coffee)
        {
            await Shell.Current.GoToAsync(nameof(CoffeeDetailsPage), true, 
                new Dictionary<string, object>
                {
                    { "Coffee", coffee }
                });
        }




        //public ObservableRangeCollection<Coffee> Coffee { get; set; }
        //public AsyncCommand RefreshCommand { get; }
        //public AsyncCommand AddCommand { get; }
        //public AsyncCommand<Coffee> RemoveCommand { get; }

        //public MyCoffeeViewModel()
        //{
        //    // Title

        //    Coffee = new ObservableRangeCollection<Coffee>();

        //    RefreshCommand = new AsyncCommand(Refresh);
        //    AddCommand = new AsyncCommand(Add);
        //    RemoveCommand = new AsyncCommand<Coffee>(Remove);
        //}

        //async Task Add()
        //{
        //    var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name");
        //    var roaster = await App.Current.MainPage.DisplayPromptAsync("Roaster", "Roaster");
        //    await CoffeeService.AddCoffee(name, roaster);
        //    await Refresh();
        //}
        //async Task Remove(Coffee coffee)
        //{
        //    await CoffeeService.RemoveCoffee(coffee.Id);
        //    await Refresh();
        //}
        //async Task Refresh()
        //{
        //    //IsBusy = true;

        //    await Task.Delay(2000);

        //    Coffee.Clear();

        //    var coffees = await CoffeeService.GetCoffee();

        //    Coffee.AddRange(coffees);

        //    //IsBusy = false;
        //}
    }
}
