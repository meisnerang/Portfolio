using DvdLibrary_Database.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibrary_Database.Models.Tables;
using DvdLibrary_Database.Models.Queries;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DvdLibrary_Database.Data.ADO
{
    public class DvdRepositoryADO : IDvdRepository
    {
        public void Create(DvdView newDvd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                 conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand("DvdCreate");
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@DvdId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Title", newDvd.Title);
                cmd.Parameters.AddWithValue("@Notes", newDvd.Notes);
                cmd.Parameters.AddWithValue("@ReleaseYearName", newDvd.ReleaseYearName);
                cmd.Parameters.AddWithValue("@DirectorName", newDvd.DirectorName);
                cmd.Parameters.AddWithValue("@RatingType", newDvd.RatingType);

                conn.Open();

                cmd.ExecuteNonQuery();

                newDvd.DvdId = (int)param.Value;

            }
        }

        public void Delete(int dvdId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdDelete";

                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<DvdView> GetAll()
        {
            List<DvdView> dvds = new List<DvdView>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdListAll";

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        DvdView currentRow = new DvdView();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();
                        currentRow.ReleaseYearName = (int)dr["ReleaseYearName"];
                        currentRow.DirectorName = dr["DirectorName"].ToString();
                        currentRow.RatingType = dr["RatingType"].ToString();

                        dvds.Add(currentRow);
                    }
                }
            }

                return dvds;
        }

        public IEnumerable<DvdView> GetByDirector(string director)
        {
            List<DvdView> dvds = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdGetByDirector";

                cmd.Parameters.AddWithValue("@DirectorName", director);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdView row = new DvdView();
                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["Title"].ToString();
                        row.Notes = dr["Notes"].ToString();
                        row.ReleaseYearName = (int)dr["ReleaseYearName"];
                        row.DirectorName = dr["DirectorName"].ToString();
                        row.RatingType = dr["RatingType"].ToString();

                        dvds.Add(row);
                    }
                }
            }
            return dvds;
        }

        public IEnumerable<DvdView> GetByRating(string rating)
        {
            List<DvdView> dvds = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdGetByRating";

                cmd.Parameters.AddWithValue("@RatingType", rating);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdView row = new DvdView();
                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["Title"].ToString();
                        row.Notes = dr["Notes"].ToString();
                        row.ReleaseYearName = (int)dr["ReleaseYearName"];
                        row.DirectorName = dr["DirectorName"].ToString();
                        row.RatingType = dr["RatingType"].ToString();

                        dvds.Add(row);
                    }
                }
            }
            return dvds;
        }

        public DvdView GetById(int dvdId)
        {
            DvdView dvd = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdGetById";

                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        dvd = new DvdView();
                        dvd.DvdId = (int)dr["DvdId"];
                        dvd.Title = dr["Title"].ToString();
                        dvd.Notes = dr["Notes"].ToString();
                        dvd.ReleaseYearName = (int)dr["ReleaseYearName"];
                        dvd.DirectorName = dr["DirectorName"].ToString();
                        dvd.RatingType = dr["RatingType"].ToString();
                    }
                }
            }
            return dvd;
        }

        public IEnumerable<DvdView> GetByReleaseYear(int releaseYear)
        {
            List<DvdView> dvds = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdGetByReleaseYear";

                cmd.Parameters.AddWithValue("@ReleaseYearName", releaseYear);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdView row = new DvdView();
                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["Title"].ToString();
                        row.Notes = dr["Notes"].ToString();
                        row.ReleaseYearName = (int)dr["ReleaseYearName"];
                        row.DirectorName = dr["DirectorName"].ToString();
                        row.RatingType = dr["RatingType"].ToString();

                        dvds.Add(row);
                    }
                }
            }
            return dvds;
        }

        public IEnumerable<DvdView> GetByTitle(string title)
        {
            List<DvdView> dvds = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdGetByTitle";

                cmd.Parameters.AddWithValue("@Title", title);

                conn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdView row = new DvdView();
                        row.DvdId = (int)dr["DvdId"];
                        row.Title = dr["Title"].ToString();
                        row.Notes = dr["Notes"].ToString();
                        row.ReleaseYearName = (int)dr["ReleaseYearName"];
                        row.DirectorName = dr["DirectorName"].ToString();
                        row.RatingType = dr["RatingType"].ToString();

                        dvds.Add(row);
                    }
                }
            }
            return dvds;
        }

        public void Update(int id, DvdView dvd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DvdEdit";

                
                cmd.Parameters.AddWithValue("@DvdId", id);
                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);
                cmd.Parameters.AddWithValue("@ReleaseYearName", dvd.ReleaseYearName);
                cmd.Parameters.AddWithValue("@DirectorName", dvd.DirectorName);
                cmd.Parameters.AddWithValue("@RatingType", dvd.RatingType);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
