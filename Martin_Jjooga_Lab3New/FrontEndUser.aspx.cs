using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Martin_Jjooga_Lab3New
{
    public partial class FrontEndUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void AdminPage_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Home/Index");

        }


        protected void GetOffer_Click(object sender, EventArgs e)
        {

            string from = From_List.SelectedItem.Value;
            string to = To_List.SelectedItem.Value;
            string nbrOfInfant = Infants.SelectedItem.Value;
            string nbrOfChildren = Children.SelectedItem.Value;
            string nbrOfAdult = Adults.SelectedItem.Value;
            string nbrOfSenior = Seniors.SelectedItem.Value;
            float rate = (float)Base_rate(@from, to);
            DateTime date = DateOfTravel.SelectedDate.Date;

            float log1 = 0;
            float lat1 = 0;

            float log2 = 0;
            float lat2 = 0;

            var flightNumber = 0;
            String carrier = null;
            string cityFrom = null;
            string cityTo = null;

            switch (from)
            {
                case "STO":
                    log1 = (float)17.9186;
                    lat1 = (float)59.6519;
                    flightNumber = 78900;
                    carrier = "SAS";
                    cityFrom = "STOCKHOLM";

                    break;

                case "CPH":
                    cityFrom = "CopenHagen";
                    carrier = "NOR";
                    log1 = (float)12.6561;
                    lat1 = (float)55.618;
                    flightNumber = 78904;

                    break;
                case "CDG":
                    cityFrom = "PARIS";
                    log1 = (float)2.5478;
                    lat1 = (float)49.0097;
                    flightNumber = 78908;
                    carrier = "AEA";


                    break;
                case "LHK":
                    cityFrom = "London";
                    log1 = (float)-0.4543;
                    lat1 = (float)51.47079;
                    flightNumber = 78909;
                    carrier = "BA";


                    break;

            }



            switch (to)
            {
                case "STO":
                    cityTo = "STOCKHOLM";
                    log2 = (float)17.9186;
                    lat2 = (float)59.6519;


                    break;
                case "CPH":
                    cityTo = "CopenHagen";
                    log2 = (float)12.6561;
                    lat2 = (float)55.618;

                    break;
                case "CDG":
                    cityTo = "Paris";
                    log2 = (float)2.5478;
                    lat2 = (float)49.0097;


                    break;
                case "LHK":
                    cityTo = "London";
                    log2 = (float)-0.4543;
                    lat2 = (float)51.47079;

                    break;


            }


            float distance = Distance(lat1, log1, lat2, log2);

            double finalPrice = FlightPrice(from, to, Convert.ToSingle(nbrOfInfant), Convert.ToSingle(nbrOfChildren), Convert.ToSingle(nbrOfAdult), Convert.ToSingle(nbrOfSenior), distance, rate);

            Price_Amount.Text = finalPrice.ToString();
            Session["Price"] = Price_Amount.Text;
            Session["FlightNumber"] = flightNumber;
            Session["Carrier"] = carrier;
            Session["CityFrom"] = cityFrom;
            Session["CityTo"] = cityTo;
            Session["Date"] = date;

        }

        protected void Payment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Payment.aspx");
        }

        private double FlightPrice(string from, string to, float infant, float children, float adults, float seniors, float distance, float baserate)
        {
            float adultCost = distance * baserate * adults;
            double childrenCost = distance * (baserate * 0.33) * children;
            double infantCost = distance * (baserate * 0.90) * infant;
            double seniorCost = distance * (baserate * 0.25) * seniors;

            return adultCost + childrenCost + infantCost + seniorCost;

        }
        double rate;

        private double Base_rate(string from, string to)
        {
            if (from.Equals("STO"))
            {
                if (to.Equals("CPH"))
                {
                    rate = 2;
                }
                else if (to.Equals("CDG"))
                {
                    rate = 1.2;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 2;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 2;
                }
            }
            if (from.Equals("CPH"))
            {
                if (to.Equals("STO"))
                {
                    rate = 2;
                }
                else if (to.Equals("CDG"))
                {
                    rate = 2;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 3;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 1.2;
                }
            }
            if (from.Equals("CDG"))
            {
                if (to.Equals("CPH"))
                {
                    rate = 2;
                }
                else if (to.Equals("STO"))
                {
                    rate = 1.7;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 2;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 1.3;
                }
            }
            if (from.Equals("LHK"))
            {
                if (to.Equals("CPH"))
                {
                    rate = 1.4;
                }
                else if (to.Equals("CDG"))
                {
                    rate = 1.9;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 2.3;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 1.5;
                }
            }
            if (from.Equals("FRA"))
            {
                if (to.Equals("CPH"))
                {
                    rate = 2;
                }
                else if (to.Equals("CDG"))
                {
                    rate = 2.1;
                }
                else if (to.Equals("LHK"))
                {
                    rate = 2.3;
                }
                else if (to.Equals("FRA"))
                {
                    rate = 1.3;
                }
            }
            return rate;
        }





        private float Distance(
            float lat1, float lon1, float lat2, float lon2)
        {
            const double radius = 6371;
            lat1 = DegreesToRadians(lat1);
            lon1 = DegreesToRadians(lon1);
            lat2 = DegreesToRadians(lat2);
            lon2 = DegreesToRadians(lon2);
            double d_lat = lat2 - lat1;
            double d_lon = lon2 - lon1;
            double h = Math.Sin(d_lat / 2) * Math.Sin(d_lat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(d_lon / 2) * Math.Sin(d_lon / 2);
            return (float)(2 * radius * Math.Asin(Math.Sqrt(h)));
        }

        private float DegreesToRadians(float degrees)
        {
            return (float)(degrees * Math.PI / 180.0);
        }

    }
}