using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Flight
{
    public partial class _Default : Page
    {
        string connectionString = "Data Source=ANOUSHKA\\SQLEXPRESS;Initial Catalog=FlightManagementDB;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdowns();
            }
        }

        private void LoadDropdowns()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Load distinct Origins into ddlFrom
                SqlCommand cmdFrom = new SqlCommand("SELECT DISTINCT Origin FROM Flights", conn);
                SqlDataReader readerFrom = cmdFrom.ExecuteReader();
                ddlFrom.DataSource = readerFrom;
                ddlFrom.DataTextField = "Origin";
                ddlFrom.DataValueField = "Origin";
                ddlFrom.DataBind();
                readerFrom.Close(); // Important: close reader, not connection yet

                // Load distinct Destinations into ddlTo
                SqlCommand cmdTo = new SqlCommand("SELECT DISTINCT Destination FROM Flights", conn);
                SqlDataReader readerTo = cmdTo.ExecuteReader();
                ddlTo.DataSource = readerTo;
                ddlTo.DataTextField = "Destination";
                ddlTo.DataValueField = "Destination";
                ddlTo.DataBind();
                readerTo.Close();

                conn.Close();
            }

            // Add default list items
            ddlFrom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Origin--", ""));
            ddlTo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Destination--", ""));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string origin = ddlFrom.SelectedValue;
            string destination = ddlTo.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT FlightNumber, AirlineName, DepartureTime, ArrivalTime FROM Flights WHERE Origin = @Origin AND Destination = @Destination", conn);
                cmd.Parameters.AddWithValue("@Origin", origin);
                cmd.Parameters.AddWithValue("@Destination", destination);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvFlights.DataSource = dt;
                gvFlights.DataBind();
                conn.Close();
            }
        }

        protected void btnCheckAvailability_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT FlightNumber, AirlineName, DepartureTime, ArrivalTime, TotalSeats - 
                                (SELECT COUNT(*) FROM Bookings WHERE Bookings.FlightID = Flights.FlightID) AS AvailableSeats
                                FROM Flights
                                WHERE Origin = @From AND Destination = @To";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@From", ddlFrom.SelectedValue);
                cmd.Parameters.AddWithValue("@To", ddlTo.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvFlights.DataSource = dt;
                gvFlights.DataBind();
            }
        }
    }
}



