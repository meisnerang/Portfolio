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
    public class MakeRepository : IMakeRepository
    {
        public void Add(Make make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeAdd", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@MakeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@MakeName", make.MakeName);
                cmd.Parameters.AddWithValue("@UserId", make.UserId);

                cn.Open();
                cmd.ExecuteNonQuery();

                make.MakeId = (int)param.Value;
            }
        }

        public IEnumerable<Make> GetAll()
        {
            List<Make> makes = new List<Make>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Make currentRow = new Make();
                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];

                        makes.Add(currentRow);
                    }
                }
            }

            return makes;
        }

        public IEnumerable<MakeItem> GetAllMakeItem()
        {
            List<MakeItem> makes = new List<MakeItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeItemSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MakeItem currentRow = new MakeItem();
                        currentRow.MakeId = (int)dr["MakeId"];
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];

                        makes.Add(currentRow);
                    }
                }
            }

            return makes;
        }
    }
}
