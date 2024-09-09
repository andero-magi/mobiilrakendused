using maui_planets.Views;

namespace maui_planets
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new StartPage();
        }
    }
}
