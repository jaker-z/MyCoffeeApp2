namespace MyCoffeeApp2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CoffeeDetailsPage), typeof(CoffeeDetailsPage));
        }
    }
}
