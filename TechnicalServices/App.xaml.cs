
namespace TechnicalServices
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var Nav = new NavigationPage(new LogInView());
            MainPage = Nav;
        }
    }
}
