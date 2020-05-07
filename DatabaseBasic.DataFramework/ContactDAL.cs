using System;
using System.Collections.Generic;
using System.Text;
using DatabaseBasic.DataFramework.Model;
using System.Data.SqlClient;

namespace DatabaseBasic.DataFramework
{
    public class ContactDAL
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Contacts;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IEnumerable<Contact> GetContacts()
        {

            List<Contact> result = new List<Contact>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Contacts";

                using (SqlCommand cmd = new SqlCommand(query,conn))
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Contact
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Sex = (SexEnum)Convert.ToInt32(reader["SexId"])
                        }) ;
                    }
                }
            }

            return result;
        }

        public Contact GetContact(int id)
        {
            var query = @"SELECT * FROM dbo.Contacts
                          WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        return new Contact
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Sex = (SexEnum)Convert.ToInt32(reader["SexId"])
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public Contact InsertContact(Contact contact)
        {
            var query = @"INSERT INTO dbo.Contacts (Name,Surname,PhoneNumber,SexId) 
                        VALUES (@Name,@Surname,@PhoneNumber,@SexId)
                        SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", contact.Name);
                    cmd.Parameters.AddWithValue("@Surname", contact.Surname);
                    cmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                    cmd.Parameters.AddWithValue("@SexId", (int)contact.Sex);

                    conn.Open();

                    var insertedId = Convert.ToInt32(cmd.ExecuteScalar());

                    return GetContact(insertedId);
                }
            }
        }

        public Contact UpdateContact(Contact contact)
        {
            var query = @"UPDATE dbo.Contacts 
                        SET Name = @Name, 
                        Surname = @Surname, 
                        PhoneNumber = @PhoneNumber, 
                        SexId = @SexId
                        WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", contact.Id);
                    cmd.Parameters.AddWithValue("@Name", contact.Name);
                    cmd.Parameters.AddWithValue("@Surname", contact.Surname);
                    cmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                    cmd.Parameters.AddWithValue("@SexId", (int)contact.Sex);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }

            return GetContact(contact.Id);
         }

        public bool DeleteContact(int id)
        {
            var query = @"DELETE FROM dbo.Contacts 
                        WHERE Id = @Id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();

                     return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public List<Contact> GetContactsByName(string contactName)
        {
            List<Contact> result = new List<Contact>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $@"SELECT * FROM Contacts 
                                WHERE Name = @Name";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", contactName);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Contact
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Sex = (SexEnum)Convert.ToInt32(reader["SexId"])
                        });
                    }
                }
            }

            return result;

        }


    }
}
