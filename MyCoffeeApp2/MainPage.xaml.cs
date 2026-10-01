namespace MyCoffeeApp2
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MyCoffeeViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
