using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repository
{
    public class SalesRepository : ISalesRepository
    {
        public void Add(SaleItem sale)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SaleAdd", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@SalesId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                //need to replace hardcoded userId
                cmd.Parameters.AddWithValue("@UserId", sale.UserId);
                cmd.Parameters.AddWithValue("@Name", sale.Name);

                if (sale.Phone == null)
                {
                    cmd.Parameters.AddWithValue("@Phone", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", sale.Phone);
                }
                if (sale.Email == null)
                {
                    cmd.Parameters.AddWithValue("@Email", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", sale.Email);
                }

                cmd.Parameters.AddWithValue("@Street1", sale.Street1);

                if (sale.Street2 == null)
                {
                    cmd.Parameters.AddWithValue("@Street2", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Street2", sale.Street2);
                }

                cmd.Parameters.AddWithValue("@City", sale.City);
                cmd.Parameters.AddWithValue("@StateId", sale.StateId);
                cmd.Parameters.AddWithValue("@Zipcode", sale.ZipCode);
                cmd.Parameters.AddWithValue("@PurchaseTypeId", sale.PurchaseTypeId);

                cmd.Parameters.AddWithValue("@VehicleId", sale.VehicleId);
                cmd.Parameters.AddWithValue("@PurchasePrice", sale.PurchasePrice);

                cn.Open();
                cmd.ExecuteNonQuery();

                sale.SalesId = (int)param.Value;
            }
            //can I now use a second sproc to flip the isSold property to true?
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ChangeToSold", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", sale.VehicleId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<VehicleItem> GetAll()
        {
            List<VehicleItem> vehicle = new List<VehicleItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleItem currentRow = new VehicleItem();
                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.PhotoFile = dr["PhotoFile"].ToString();
                        currentRow.Year = (int)dr["Year"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.IsManualTransmission = (bool)dr["IsManualTransmission"];
                        currentRow.ExteriorColor = dr["Exterior_Color"].ToString();
                        currentRow.InteriorColor = dr["Interior_Color"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.Vin = dr["Vin"].ToString();
                        currentRow.Description = dr["Description"].ToString();
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.MSRP = (decimal)dr["MSRP"];
                        currentRow.IsSold = (bool)dr["IsSold"];

                        vehicle.Add(currentRow);
                    }
                }
            }

            return vehicle;
        }

        public IEnumerable<SalesReportItem> SearchAllSales(ReportSearchParameters parameters)
        {
            List<SalesReportItem> sales = new List<SalesReportItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                string query = "SELECT u.UserName AS [User], ";
                cmd.Parameters.AddWithValue("@UserName", parameters.UserName);

                query += "SUM(s.PurchasePrice) AS TotalSales, COUNT(s.VehicleId) AS TotalVehicles From Sales s Inner Join AspNetUsers u ON s.UserId = u.Id ";

                if (parameters.MinDate.HasValue)
                {
                    query += "Where s.DateAdded Between @MinDate ";
                    cmd.Parameters.AddWithValue("@MinDate", parameters.MinDate.Value);
                }

                if (parameters.MaxDate.HasValue)
                {
                    query += "AND @MaxDate ";
                    cmd.Parameters.AddWithValue("@MaxDate", parameters.MaxDate.Value);
                }

                query += "Group By u.UserName";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReportItem currentRow = new SalesReportItem();
                        currentRow.User = dr["User"].ToString();
                        currentRow.TotalSales = (decimal)dr["TotalSales"];
                        currentRow.TotalVehicles = (int)dr["TotalVehicles"];

                        sales.Add(currentRow);
                    }
                }
            }

            return sales;
        }

    }
}
