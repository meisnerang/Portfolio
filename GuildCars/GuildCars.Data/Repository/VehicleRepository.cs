using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        public void Add(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleAdd", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@VehicleId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeId", vehicle.MakeId);
                cmd.Parameters.AddWithValue("@ModelId", vehicle.ModelId);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@BodyStyleId", vehicle.BodyStyleId);
                cmd.Parameters.AddWithValue("@IsManualTransmission", vehicle.IsManualTransmission);
                cmd.Parameters.AddWithValue("@ExteriorColorId", vehicle.ExteriorColorId);
                cmd.Parameters.AddWithValue("@InteriorColorId", vehicle.InteriorColorId);
                cmd.Parameters.AddWithValue("@UserId", vehicle.UserId);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@Vin", vehicle.Vin);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);

                if (string.IsNullOrEmpty(vehicle.PhotoFile))
                {
                    cmd.Parameters.AddWithValue("@PhotoFile", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PhotoFile", vehicle.PhotoFile);
                }

                cmd.Parameters.AddWithValue("@IsFeatured", vehicle.IsFeatured);
                cmd.Parameters.AddWithValue("@IsNew", vehicle.IsNew);
                cmd.Parameters.AddWithValue("@IsSold", vehicle.IsSold);

                cn.Open();

                cmd.ExecuteNonQuery();

                vehicle.VehicleId = (int)param.Value;
            }
        }

        public void Delete(int vehicleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSaleVehicle(int vehicleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SaleDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Edit(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleEdit", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("VehicleId", vehicle.VehicleId);
                cmd.Parameters.AddWithValue("@MakeId", vehicle.MakeId);
                cmd.Parameters.AddWithValue("@ModelId", vehicle.ModelId);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@BodyStyleId", vehicle.BodyStyleId);
                cmd.Parameters.AddWithValue("@IsManualTransmission", vehicle.IsManualTransmission);
                cmd.Parameters.AddWithValue("@ExteriorColorId", vehicle.ExteriorColorId);
                cmd.Parameters.AddWithValue("@InteriorColorId", vehicle.InteriorColorId);
                cmd.Parameters.AddWithValue("@UserId", vehicle.UserId);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@Vin", vehicle.Vin);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);

                if (string.IsNullOrEmpty(vehicle.PhotoFile))
                {
                    cmd.Parameters.AddWithValue("@PhotoFile", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PhotoFile", vehicle.PhotoFile);
                }

                cmd.Parameters.AddWithValue("@IsFeatured", vehicle.IsFeatured);
                cmd.Parameters.AddWithValue("@IsNew", vehicle.IsNew);
                cmd.Parameters.AddWithValue("@IsSold", vehicle.IsSold);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<VehicleItem> GetAll()
        {
            List<VehicleItem> vehicles = new List<VehicleItem>();

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

                        vehicles.Add(currentRow);
                    }
                }
            }

            return vehicles;
        }

        public VehicleItem GetById(int vehicleId)
        {
            VehicleItem vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new VehicleItem();
                        vehicle.VehicleId = (int)dr["VehicleId"];
                        vehicle.PhotoFile = dr["PhotoFile"].ToString();
                        vehicle.Year = (int)dr["Year"];
                        vehicle.MakeName = dr["MakeName"].ToString();
                        vehicle.ModelName = dr["ModelName"].ToString();
                        vehicle.BodyStyleName = dr["BodyStyleName"].ToString();
                        vehicle.IsManualTransmission = (bool)dr["IsManualTransmission"];
                        vehicle.ExteriorColor = dr["Exterior_Color"].ToString();
                        vehicle.InteriorColor = dr["Interior_Color"].ToString();
                        vehicle.Mileage = (int)dr["Mileage"];
                        vehicle.Vin = dr["Vin"].ToString();
                        vehicle.Description = dr["Description"].ToString();
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.IsSold = (bool)dr["IsSold"];
                    }
                }
            }

            return vehicle;
        }

        public Vehicle GetByIdByVehicle(int vehicleId)
        {
            Vehicle vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleSelectById_Vehicle", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new Vehicle();
                        vehicle.VehicleId = (int)dr["VehicleId"];
                        vehicle.PhotoFile = dr["PhotoFile"].ToString();
                        vehicle.Year = (int)dr["Year"];
                        vehicle.MakeId = (int)dr["MakeId"];
                        vehicle.ModelId = (int)dr["ModelId"];
                        vehicle.BodyStyleId = (int)dr["BodyStyleId"];
                        vehicle.IsManualTransmission = (bool)dr["IsManualTransmission"];
                        vehicle.ExteriorColorId = (int)dr["ExteriorColorId"];
                        vehicle.InteriorColorId = (int)dr["InteriorColorId"];
                        vehicle.Mileage = (int)dr["Mileage"];
                        vehicle.Vin = dr["Vin"].ToString();
                        vehicle.Description = dr["Description"].ToString();
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.IsSold = (bool)dr["IsSold"];
                        vehicle.IsNew = (bool)dr["IsNew"];
                    }
                }
            }

            return vehicle;
        }

        public IEnumerable<FeaturedItem> GetFeatured()
        {
            List<FeaturedItem> featured = new List<FeaturedItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("FeaturedSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FeaturedItem currentRow = new FeaturedItem();
                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.PhotoFile = dr["PhotoFile"].ToString();
                        currentRow.Year = (int)dr["Year"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.Special = new Special();

                        featured.Add(currentRow);
                    }
                }
            }

            return featured;
        }

        public IEnumerable<VehicleItem> SearchAllNotSoldInventory(VehicleSearchParameters parameters)
        {
            List<VehicleItem> vehicles = new List<VehicleItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 VehicleId, PhotoFile, [Year], MakeName, ModelName, BodyStyleName, IsManualTransmission, C1.ColorName AS Exterior_Color, C2.ColorName AS Interior_Color, Mileage, Vin, SalePrice, MSRP FROM Vehicle INNER JOIN Make ON Vehicle.MakeId = Make.MakeId INNER JOIN Model ON Vehicle.ModelId = Model.ModelId INNER JOIN BodyStyle ON Vehicle.BodyStyleId = BodyStyle.BodyStyleId INNER JOIN Color AS C1 ON Vehicle.ExteriorColorId = C1.ColorId INNER JOIN Color AS C2 ON Vehicle.InteriorColorId = C2.ColorId AND Vehicle.IsSold = 0 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND MSRP >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND MSRP <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND Year >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "AND Year <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                }

                if (!string.IsNullOrEmpty(parameters.MakeModelOrYear))
                {
                    query += "AND (MakeName LIKE @MakeModelOrYear OR ModelName LIKE @MakeModelOrYear OR [Year] LIKE @MakeModelOrYear) ";
                    cmd.Parameters.AddWithValue("@MakeModelOrYear", parameters.MakeModelOrYear + '%');
                }

                query += "ORDER BY MSRP DESC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleItem currentRow = new VehicleItem();
                        currentRow.VehicleId = (int)dr["VehicleId"];

                        if (dr["PhotoFile"] != DBNull.Value)
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
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.MSRP = (decimal)dr["MSRP"];

                        vehicles.Add(currentRow);
                    }
                }
            }

            return vehicles;
        }

        public IEnumerable<VehicleItem> SearchNewInventory(VehicleSearchParameters parameters)
        {
            List<VehicleItem> vehicles = new List<VehicleItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 VehicleId, PhotoFile, [Year], MakeName, ModelName, BodyStyleName, IsManualTransmission, C1.ColorName AS Exterior_Color, C2.ColorName AS Interior_Color, Mileage, Vin, SalePrice, MSRP FROM Vehicle INNER JOIN Make ON Vehicle.MakeId = Make.MakeId INNER JOIN Model ON Vehicle.ModelId = Model.ModelId INNER JOIN BodyStyle ON Vehicle.BodyStyleId = BodyStyle.BodyStyleId INNER JOIN Color AS C1 ON Vehicle.ExteriorColorId = C1.ColorId INNER JOIN Color AS C2 ON Vehicle.InteriorColorId = C2.ColorId WHERE IsNew = 1 AND Vehicle.IsSold = 0 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND MSRP >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND MSRP <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND Year >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "AND Year <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                }

                if (!string.IsNullOrEmpty(parameters.MakeModelOrYear))
                {
                    query += "AND (MakeName LIKE @MakeModelOrYear OR ModelName LIKE @MakeModelOrYear OR [Year] LIKE @MakeModelOrYear) ";
                    cmd.Parameters.AddWithValue("@MakeModelOrYear", parameters.MakeModelOrYear + '%');
                }

                query += "ORDER BY MSRP DESC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleItem currentRow = new VehicleItem();
                        currentRow.VehicleId = (int)dr["VehicleId"];

                        if (dr["PhotoFile"] != DBNull.Value)
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
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.MSRP = (decimal)dr["MSRP"];

                        vehicles.Add(currentRow);
                    }
                }
            }

            return vehicles;
        }

        public IEnumerable<VehicleItem> SearchUsedInventory(VehicleSearchParameters parameters)
        {
            List<VehicleItem> vehicles = new List<VehicleItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 VehicleId, PhotoFile, [Year], MakeName, ModelName, BodyStyleName, IsManualTransmission, C1.ColorName AS Exterior_Color, C2.ColorName AS Interior_Color, Mileage, Vin, SalePrice, MSRP FROM Vehicle INNER JOIN Make ON Vehicle.MakeId = Make.MakeId INNER JOIN Model ON Vehicle.ModelId = Model.ModelId INNER JOIN BodyStyle ON Vehicle.BodyStyleId = BodyStyle.BodyStyleId INNER JOIN Color AS C1 ON Vehicle.ExteriorColorId = C1.ColorId INNER JOIN Color AS C2 ON Vehicle.InteriorColorId = C2.ColorId WHERE IsNew = 0 AND Vehicle.IsSold = 0 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND MSRP >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND MSRP <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND Year >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "AND Year <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                }

                if (!string.IsNullOrEmpty(parameters.MakeModelOrYear))
                {
                    query += "AND (MakeName LIKE @MakeModelOrYear OR ModelName LIKE @MakeModelOrYear OR [Year] LIKE @MakeModelOrYear) ";
                    cmd.Parameters.AddWithValue("@MakeModelOrYear", parameters.MakeModelOrYear + '%');
                }

                query += "ORDER BY MSRP DESC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleItem currentRow = new VehicleItem();
                        currentRow.VehicleId = (int)dr["VehicleId"];

                        if (dr["PhotoFile"] != DBNull.Value)
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
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.MSRP = (decimal)dr["MSRP"];

                        vehicles.Add(currentRow);
                    }
                }
            }

            return vehicles;
        }

        public IEnumerable<InventoryReportItem> GetNewInventory()
        {
            List<InventoryReportItem> inventory = new List<InventoryReportItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryReportNew", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReportItem currentRow = new InventoryReportItem();

                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["Make"].ToString();
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = (decimal)dr["StockValue"];

                        inventory.Add(currentRow);
                    }
                }
            }

            return inventory;
        }

        public IEnumerable<InventoryReportItem> GetUsedInventory()
        {
            List<InventoryReportItem> inventory = new List<InventoryReportItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryReportUsed", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReportItem currentRow = new InventoryReportItem();

                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["Make"].ToString();
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = (decimal)dr["StockValue"];

                        inventory.Add(currentRow);
                    }
                }
            }

            return inventory;
        }

        public int CountOfVehicles()
        {
            int vehicleCount = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleCount", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                vehicleCount = (Int32)cmd.ExecuteScalar();
            }

            return vehicleCount;
        }

        public int GetMaxVehicleId()
        {
            int maxId = 0;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetMaxVehicleId", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                maxId = (Int32)cmd.ExecuteScalar();
            }

            return maxId;
        }
    }
}
