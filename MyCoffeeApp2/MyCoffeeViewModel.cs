using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
//using MvvmHelpers;
//using MvvmHelpers.Commands;


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
            await CoffeeService.AddCoffee(name, roaster);
            await RefreshAsync();
        }

        [RelayCommand]
        async Task RemoveAsync(Coffee coffee)
        {
            await CoffeeService.RemoveCoffee(coffee.Id);
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
        async Task GoToDetailsAsync()
        {
            await Shell.Current.GoToAsync(nameof(CoffeeDetailsPage));

            // add arguments to the above command
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
