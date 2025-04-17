<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Flight._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flight Finder</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
    <style>
        body {
            background-color: #f5f7fa;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            padding: 40px;
        }
        .container {
            background: #ffffff;
            padding: 30px;
            border-radius: 16px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.1);
        }
        .form-label {
            font-weight: 600;
        }
        .heading {
            color: #2c3e50;
            margin-bottom: 25px;
        }
        .btn-custom {
            background-color: #3498db;
            border: none;
        }
        .btn-custom:hover {
            background-color: #2980b9;
        }
        .filter-section {
            margin-bottom: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="heading text-center">Check Flight Availability</h2>

            <div class="row filter-section">
                <div class="col-md-6">
                    <label for="ddlFrom" class="form-label">Origin</label>
                    <asp:DropDownList ID="ddlFrom" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-md-6">
                    <label for="ddlTo" class="form-label">Destination</label>
                    <asp:DropDownList ID="ddlTo" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
            </div>

            <div class="d-flex justify-content-center mb-4">
                <asp:Button ID="btnCheckAvailability" runat="server" Text="Check Availability" CssClass="btn btn-custom btn-lg" OnClick="btnCheckAvailability_Click" />
            </div>

            <h4 class="text-center mb-3">Available Flights</h4>
            <asp:GridView ID="gvFlights" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover">
                <Columns>
                    <asp:BoundField DataField="FlightNumber" HeaderText="Flight Number" />
                    <asp:BoundField DataField="AirlineName" HeaderText="Airline Name" />
                    <asp:BoundField DataField="DepartureTime" HeaderText="Departure Time" />
                    <asp:BoundField DataField="ArrivalTime" HeaderText="Arrival Time" />
                    <asp:BoundField DataField="AvailableSeats" HeaderText="Available Seats" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
