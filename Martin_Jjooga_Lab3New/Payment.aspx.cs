using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Microsoft.Azure.Documents.Client;

namespace Martin_Jjooga_Lab3New
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FlightFair.Text = Session["Price"].ToString();
            FlightNumber.Text = Session["FlightNumber"].ToString();
            Carrier.Text = Session["Carrier"].ToString();
            From.Text = Session["CityFrom"].ToString();
            To.Text = Session["CityTo"].ToString();
            Date.Text = Session["Date"].ToString();
            Expiry_Date.Text = "YYYY-MM-DD";

        }

        protected void FinalPay_Click(object sender, EventArgs e)
        {
            string connetionString = @"data source=martin2020i.database.windows.net;initial catalog=FlightDB;persist security info=True;user id=martin2020i;password=Namagela123i;multipleactiveresultsets=True;application name=EntityFramework";
            string insStmt = "INSERT into [dbo].[Customer] (Card_Number,Holder_Name,Expiry_Date,Balance) values (@Card_Number,@HolderName,@ExpiryDate,@Balance)";

            string insStmt2 = "INSERT into [dbo].[Routes] (flightNumber,carrier,departure_airport,arrival_airport) values (@flightNumber,@carrier,@departure_airport,@Arrival_Airport)";
            string insStmt3 = "INSERT into [dbo].[Flights] (PsgrName,Passport_Number,Departure_Date,Airfare) values (@Name,@Pssnumber,@date,@price)";

            string flightNumber = Session["FlightNumber"].ToString();
            string carrier = Session["Carrier"].ToString();
            string from = Session["CityFrom"].ToString();
            string to = Session["CityTo"].ToString();
            string date = Session["Date"].ToString();


            using (SqlConnection cnn = new SqlConnection(connetionString))
            {

                {

                }
                float balance = Convert.ToSingle(Session["Price"].ToString());

                cnn.Open();
                SqlCommand insCmd = new SqlCommand(insStmt, cnn);
                // use sqlParameters to prevent sql injection!


                insCmd.Parameters.AddWithValue("@Card_Number", Card_Number.Text);
                insCmd.Parameters.AddWithValue("@HolderName", Holders_Name.Text);
                insCmd.Parameters.AddWithValue("@ExpiryDate", Expiry_Date.Text);
                insCmd.Parameters.AddWithValue("@Balance", balance);
                int affectedRows = insCmd.ExecuteNonQuery();

                SqlCommand insCmd2 = new SqlCommand(insStmt2, cnn);


                insCmd2.Parameters.AddWithValue("@flightNumber", flightNumber);
                insCmd2.Parameters.AddWithValue("@carrier", carrier);
                insCmd2.Parameters.AddWithValue("@departure_airport", from);
                insCmd2.Parameters.AddWithValue("@Arrival_Airport", to);

                int affectedRows2 = insCmd2.ExecuteNonQuery();


                SqlCommand insCmd3 = new SqlCommand(insStmt3, cnn);

                insCmd3.Parameters.AddWithValue("@Name", Holders_Name.Text);
                insCmd3.Parameters.AddWithValue("@Pssnumber", PassportNumber.Text);
                insCmd3.Parameters.AddWithValue("@date", date);
                insCmd3.Parameters.AddWithValue("@price", balance);


                int affectedRows3 = insCmd3.ExecuteNonQuery();


                //connection settings for CosmoDB used AZure CosmoDb Emulator
                string EndpointUrl;
                string PrimaryKey;
                DocumentClient client;

                EndpointUrl = "https://localhost:8081";
                PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

                //add to Cosmodb 

                dynamic customer = new
                {

                    Card_Number = Card_Number.Text,
                    Holder_Name = Holders_Name.Text,
                    Expiry_Date = Expiry_Date.Text,
                    Balance = balance


                };

                var document1 = client.CreateDocumentAsync(
                    UriFactory.CreateDocumentCollectionUri("CharterResor", "Customer"),
                    customer);

                dynamic routes = new
                {
                    Id = flightNumber,
                    flightNumber = flightNumber,
                    carrier = Carrier.Text,
                    departure_airport = from,
                    rrival_Airport = to

                };

                var document2 = client.CreateDocumentAsync(
                    UriFactory.CreateDocumentCollectionUri("CharterResor", "Route"),
                    routes);


                dynamic flight = new
                {
                    PsgrName = Holders_Name.Text,
                    Passport_Number = PassportNumber.Text,
                    Departure_Date = date,
                    Airfare = balance,
                    id = PassportNumber.Text

                };

                var document3 = client.CreateDocumentAsync(
                    UriFactory.CreateDocumentCollectionUri("CharterResor", "Flight"),
                    flight);


                string message = "Booked Successfully , do you wish to book more flights?";
                string title = "Comfirmation";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Response.Redirect("~/FrontEndUser.aspx");
                }
                else
                {

                }



            }






        }
    }
}