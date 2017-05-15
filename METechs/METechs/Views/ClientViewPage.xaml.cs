using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using METechs.ViewModels;
using METechs.Models;

namespace METechs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientViewPage : ContentPage
    {
        public ClientViewPage()
        {
			InitializeComponent ();
            BindingContext  = new ClientViewPageViewModel();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string clientid ="";
            if (e.SelectedItem == null)
                return;
            //Once a client is selected, start the details page

            //set the clientid that will be in the app object
            clientid = e.SelectedItem.ToString();

            await Navigation.PushAsync(new ClientViewDetailPage(clientid));
            //await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }



    class ClientViewPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> Clients { get; }
        public ObservableCollection<Grouping<string, Item>> ItemsGrouped { get; }

        public ClientViewPageViewModel()
        {
            var clients = new ObservableCollection<Clients>();
            var Items = new ObservableCollection<Item>();

            String connectionString = "Data Source=me2017.database.windows.net;Initial Catalog=MeTechs;User ID=clifford.mauer;Password=BlueRabbit2008;";
            String sql = "SELECT [id],rtrim([name]) as name,[address1],[address2],[city],[zip],[state],[phone] FROM[dbo].[Clients]";
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand sqlCmd;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            cnn.Open();
            sqlCmd = new SqlCommand(sql, cnn);
            da.SelectCommand = sqlCmd;

            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            dt.Columns.Add(new DataColumn("Id_client", typeof(int)));
            //DataRow dr = dt.Rows[0];

            foreach (DataRow row in dt.Rows)
            {
                clients.Add(new Clients()
                {
                    Id_client = (int)row["id"],
                    Name = (string)row["name"],
                    City = (string)row["city"]
                });
                Items.Add(new Item { Id_Client = Convert.ToString((int)row["id"]),Name = (String)row["name"]  +"-"+(string)row["city"], Phone = (string)row["phone"]  });
            } 


            da.Dispose();
            sqlCmd.Dispose();
            cnn.Close();

            var sorted = from item in Items
                         orderby item.Name
                         group item by item.Name[0].ToString() into itemGroup
                         select new Grouping<string, Item>(itemGroup.Key, itemGroup);

            ItemsGrouped = new ObservableCollection<Grouping<string, Item>>(sorted);

            RefreshDataCommand = new Command(
                async () => await RefreshData());
        }

        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            IsBusy = true;
            //Load Data Here
            await Task.Delay(2000);

            IsBusy = false;
        }

        bool busy;
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public class Item
        {
            public string Name { get; set; }
            public string Phone { get; set; }
            public override string ToString() => Id_Client;
            public string Id_Client { get; set; }
        }

        public class Grouping<K, T> : ObservableCollection<T>
        {
            private string key;
            private IGrouping<string, Clients> itemGroup;

            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> clients)
            {
                Key = key;
                foreach (var item in clients)
                    this.Items.Add(item);
            }

            public Grouping(string key, IGrouping<string, Clients> itemGroup)
            {
                this.key = key;
                this.itemGroup = itemGroup;
            }
        }
    }
}
