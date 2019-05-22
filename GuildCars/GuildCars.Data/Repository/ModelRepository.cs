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
    public class ModelRepository : IModelRepository
    {
        public void Add(Model model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelAdd", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                cmd.Parameters.AddWithValue("@MakeId", model.MakeId);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                                
                cn.Open();
                cmd.ExecuteNonQuery();

                model.ModelId = (int)param.Value;
            }
        }

        public IEnumerable<ModelItem> GetAll()
        {
            List<ModelItem> models = new List<ModelItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelItemSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ModelItem currentRow = new ModelItem();
                        currentRow.ModelId = (int)dr["ModelId"];
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];

                        models.Add(currentRow);
                    }
                }
            }

            return models;
        }

        
        public IEnumerable<Model> GetModelsByMake(int makeId)
        {
            List<Model> models = new List<Model>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelSelectByMake", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MakeId", makeId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model currentRow = new Model();
                        currentRow.ModelId = (int)dr["ModelId"];
                        currentRow.ModelName = dr["ModelName"].ToString();
                        
                        models.Add(currentRow);
                    }
                }
            }

            return models;
        }
    }
}
