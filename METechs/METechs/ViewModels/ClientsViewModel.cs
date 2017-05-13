using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Xamarin.Forms;
using METechs.Helpers;
using METechs.Models;
using METechs.Views;

namespace METechs.ViewModels
{
    class ClientsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Clients { get; set; }
        public Command LoadClientsCommand { get; set; }

        public ClientsViewModel()
        {
            Title = "Browse";
            Clients = new ObservableRangeCollection<Item>();
            LoadClientsCommand = new Command(async () => await ExecuteLoadClientsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Clients.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadClientsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Clients.Clear();
                //SqlDataAdapter da = new SqlDataAdapter();
                //SqlDataAdapter da = new SqlDataAdapter("select * from technicians where user_name ='" + user.Username + "' " + "and password='" +
                //user.Password + "' ", @"Data Source=localhost;Initial Catalog=TechsForMe;User ID=TestUser;Password=Rabbit2008;");
                String connectionString = "Data Source=me2017.database.windows.net;Initial Catalog=MeTechs;User ID=clifford.mauer;Password=BlueRabbit2008;";
                String sql = "select * from clients";
                SqlConnection cnn = new SqlConnection(connectionString);
                SqlCommand sqlCmd;
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                cnn.Open();
                sqlCmd = new SqlCommand(sql, cnn);
                da.SelectCommand = sqlCmd;

                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                //DataRow dr = dt.Rows[0];

                List<string> clients = new List<string>();
                ListView listViewClients = new ListView();
                listViewClients.ItemsSource = clients;
                
                for (int i = 1; i<= ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    clients.Add(dr["Name"].ToString());
                }


                da.Dispose();
                sqlCmd.Dispose();
                cnn.Close();

                //return User_Name.Trim() == Constants.Username && Password.Trim() == Constants.Password;

                //var items = await DataStore.GetItemsAsync(true);
                //Items.ReplaceRange(items);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
