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
    public class StatesRepository : IStatesRepository
    {
        public List<States> GetAll()
        {
            List<States> states = new List<States>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("StatesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        States currentRow = new States();
                        currentRow.StateId = dr["StateId"].ToString();
                        currentRow.StateName = dr["StateName"].ToString();

                        states.Add(currentRow);
                    }
                }
            }

            return states;
        }
    }
}
