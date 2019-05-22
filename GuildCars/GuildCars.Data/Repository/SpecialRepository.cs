using GuildCars.Data.Interfaces;
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
    public class SpecialRepository : ISpecialRepository
    {
        public void Add(Special special)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialAdd", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@SpecialId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Title", special.Title);
                cmd.Parameters.AddWithValue("@Description", special.Description);

                cn.Open();
                cmd.ExecuteNonQuery();

                special.SpecialId = (int)param.Value;
            }
        }

        public void Delete(int specialId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SpecialId", specialId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Special> GetAll()
        {
            List<Special> specials = new List<Special>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Special currentRow = new Special();
                        currentRow.SpecialId = (int)dr["SpecialId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.Description = dr["Description"].ToString();

                        specials.Add(currentRow);
                    }
                }
            }

            return specials;
        }

        public Special GetById(int specialId)
        {
            Special special = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialSelectByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SpecialId", specialId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        special = new Special();
                        special.SpecialId = (int)dr["SpecialId"];
                        special.Title = dr["Title"].ToString();
                        special.Description = dr["Description"].ToString();
                    }
                }
            }

            return special;
        }
    }
}
