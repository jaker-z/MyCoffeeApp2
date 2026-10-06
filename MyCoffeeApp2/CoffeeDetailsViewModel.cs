using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyCoffeeApp2
{

    [QueryProperty(nameof(Coffee), "Coffee")]
    public partial class CoffeeDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Coffee coffee; 


    }
}
