using System;
using System.Data;
using System.Data.SqlClient;
using Xamarin.Forms;

namespace METechs.LoginNavigation
{
    public class LoginPageCS : ContentPage
    {
        Entry usernameEntry, passwordEntry;
        Label messageLabel;

        public LoginPageCS()
        {
            var toolbarItem = new ToolbarItem
            {
                Text = "Sign Up"
            };
            toolbarItem.Clicked += OnSignUpButtonClicked;
            ToolbarItems.Add(toolbarItem);

            messageLabel = new Label();
            usernameEntry = new Entry
            {
                Placeholder = "username"
            };
            passwordEntry = new Entry
            {
                IsPassword = true
            };
            var loginButton = new Button
            {
                Text = "Login"
            };
            loginButton.Clicked += OnLoginButtonClicked;

            Title = "Login";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Username" },
                    usernameEntry,
                    new Label { Text = "Password" },
                    passwordEntry,
                    loginButton,
                    messageLabel
                }
            };
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPageCS());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var isValid = AreCredentialsCorrect(user);
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPageCS(), this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
        }

        bool AreCredentialsCorrect(User user)
        {
            //SqlDataAdapter da = new SqlDataAdapter();
            //SqlDataAdapter da = new SqlDataAdapter("select * from technicians where user_name ='" + user.Username + "' " + "and password='" +
            //user.Password + "' ", @"Data Source=localhost;Initial Catalog=TechsForMe;User ID=TestUser;Password=Rabbit2008;");
            String connectionString = "Data Source=me2017.database.windows.net;Initial Catalog=MeTechs;User ID=clifford.mauer;Password=BlueRabbit2008;";
            String sql = "select * from technicians";
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand sqlCmd;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            cnn.Open();
            sqlCmd = new SqlCommand(sql, cnn);
            da.SelectCommand = sqlCmd;

            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];

            String User_Name = dr["User_name"].ToString();
            String Password = dr["Password"].ToString();

            da.Dispose();
            sqlCmd.Dispose();
            cnn.Close();

            return User_Name.Trim() == Constants.Username && Password.Trim() == Constants.Password;
        }
    }
}
