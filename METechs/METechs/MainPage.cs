using System;
using Xamarin.Forms;

using METechs.Views;

namespace METechs.LoginNavigation
{
    public class MainPageCS : ContentPage
    {
        public MainPageCS()
        {
            var toolbarItem = new ToolbarItem
            {
                Text = "Logout"
            };
            toolbarItem.Clicked += OnLogoutButtonClicked;
            ToolbarItems.Add(toolbarItem);
            Title = "Main Page";
            Grid grid = new Grid();
            Button btnScheduled = new Button { Text = "Scheduled" };
            Button btnComplete = new Button { Text = "Complete" };
            Button btnInvoiced = new Button { Text = "Invoiced" };
            Button btnItinery = new Button { Text = "Itinery" };
            Button btnMarket = new Button { Text = "Market" };
            Button btnEarnings = new Button { Text = "Earnings" };
            Button btnJobs = new Button { Text = "Jobs" };
            Button btnClients = new Button { Text = "Clients" };
            

            grid.Children.Add(btnScheduled,0,0);
            grid.Children.Add(btnComplete,0,1);
            grid.Children.Add(btnInvoiced,0,2);
            grid.Children.Add(btnItinery,0,3);
            grid.Children.Add(btnMarket ,0,4);
            grid.Children.Add(btnEarnings ,0,5);
            grid.Children.Add(btnJobs ,0,6);
            grid.Children.Add(btnClients,0,7);

            btnScheduled.Clicked += On_btnScheduled_ClickedAsync;


            Content = grid;

            //            Content = new StackLayout
            //{
            //Children = {
            //new Label
            //{
            //    Text = "Main app content goes here",
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.CenterAndExpand
            //}
            //}
            //};

            //Content = new TabbedPage()
            //App.Current.MainPage = new TabbedPage
            //{
            //    Children = {
            //         new NavigationPage(new ItemsPage())
            //        {
            //            Title = "Browse",
            //            Icon = Device.OnPlatform("tab_feed.png",null,null)
            //        },
            //        new NavigationPage(new AboutPage())
            //        {
            //            Title = "About",
            //            Icon = Device.OnPlatform("tab_about.png",null,null)
            //        },
            //   }
            //};
        }

        private async void On_btnScheduled_ClickedAsync(object sender, EventArgs e)
        {
            //await DisplayAlert("Scheduled button has been clicked.");
            throw new NotImplementedException();
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPageCS(), this);
            await Navigation.PopAsync();
        }
    }
}