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
    public class ContactRepository : IContactRepository
    {
        public void Add(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactAdd", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ContactId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Name", contact.Name);

                if (contact.Email == null)
                {
                    cmd.Parameters.AddWithValue("@Email", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", contact.Email);
                }

                if (contact.Phone == null)
                {
                    cmd.Parameters.AddWithValue("@Phone", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                }

                cmd.Parameters.AddWithValue("@Message", contact.Message);

                cn.Open();
                cmd.ExecuteNonQuery();

                contact.ContactId = (int)param.Value;
            }
        }

        public IEnumerable<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contact currentRow = new Contact();
                        currentRow.ContactId = (int)dr["ContactId"];
                        currentRow.Name = dr["Name"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.Phone = dr["Phone"].ToString();
                        currentRow.Message = dr["Message"].ToString();

                        contacts.Add(currentRow);
                    }
                }
            }

            return contacts;
        }
    }
}
