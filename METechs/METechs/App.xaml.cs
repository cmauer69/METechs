using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using METechs.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace METechs
{
	public partial class App : Application
	{
        public static bool IsUserLoggedIn { get; set; }
        public App()
        {
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginNavigation.LoginPageCS());
            }
            else
            {
                InitializeComponent();
                SetMainPage();
                //MainPage = new NavigationPage(new LoginNavigation.MainPageCS());
            }
        }

        public static void SetMainPage()
		{
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform<string>("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    },
                }
            };
        }
	}
}
