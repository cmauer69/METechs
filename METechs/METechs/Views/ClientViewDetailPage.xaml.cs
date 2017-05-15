using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace METechs.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientViewDetailPage : ContentPage
	{
        private readonly string AddressLine2 ;

        public ClientViewDetailPage (string clientid)
		{
			InitializeComponent ();

            String connectionString = "Data Source=me2017.database.windows.net;Initial Catalog=MeTechs;User ID=clifford.mauer;Password=BlueRabbit2008;";
            String sql = "SELECT [id],rtrim([name]) as name,[address1],[address2],[city],[zip],[state],[phone] FROM[dbo].[Clients] where [id] = "+ clientid;
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand sqlCmd;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            cnn.Open();
            sqlCmd = new SqlCommand(sql, cnn);
            da.SelectCommand = sqlCmd;

            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                BusinessName.Text = (string)row["name"];
                Address1.Text = (string)row["address1"];
                AddressLine2 = (string)row["address2"];
                if (AddressLine2 != null)
                    {
                    Address2.Text = AddressLine2;
                    } 
                City.Text = (string)row["city"];
                Zip.Text = (string)row["zip"];
                State.Text = (string)row["state"];
                Phone.Text = (string)row["phone"];

            }
        }

        public async void OnDoneButtonClickedAsync()
        {
            Page ClientViewDetailPage = await Navigation.PopAsync();
            return;
        }
    }
}
