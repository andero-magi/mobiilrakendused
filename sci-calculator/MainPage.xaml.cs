namespace sci_calculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new CalculatorViewModel();
        }
    }
}
