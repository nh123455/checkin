using Checkin.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkin.DAL
{
    public class userDAL
    {

        public bool InsertUser(List<userDTO> list, string connectstring)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();

                    string sqlString = "Insert into checkin (qrcode, name, email, phone, company, type) values (@qrcode, @name, @email, @phone, @company, @type)";

                    using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                    {
                        foreach (var item in list)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@qrcode", item.qrcode);
                            cmd.Parameters.AddWithValue("@name", item.name);
                            cmd.Parameters.AddWithValue("@email", item.email);
                            cmd.Parameters.AddWithValue("@phone", item.phone);
                            cmd.Parameters.AddWithValue("@company", item.company);
                            cmd.Parameters.AddWithValue("@type", 0);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ResetCheckin(string connectString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();

                    string sqlString = "Update checkin_Details set type = 1";

                    using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteData(string connectString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();

                    string sqlString = "Update checkin set type = 1";

                    using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<userDTO> GetUserByQrcode(string qrcode, string connectString)
        {
            List<userDTO> userResult = new List<userDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    string query = $"Select * from checkin where qrcode = @qrcode";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@qrcode", qrcode));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userDTO obj = new userDTO();
                                obj.qrcode = (reader["qrcode"]).ToString();
                                obj.name = (reader["name"]).ToString();
                                obj.email = (reader["email"]).ToString();
                                obj.phone = (reader["phone"]).ToString();
                                obj.company = (reader["company"]).ToString();

                                userResult.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<userDTO>();
            }
            return userResult;
        }

        public List<userDTO> GetUser(string connectString)
        {
            List<userDTO> userResult = new List<userDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    string query = $"Select qrcode, name, email, phone, company from checkin where type = 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userDTO obj = new userDTO();
                                obj.qrcode = (reader["qrcode"]).ToString();
                                obj.name = (reader["name"]).ToString();
                                obj.email = (reader["email"]).ToString();
                                obj.phone = (reader["phone"]).ToString();
                                obj.company = (reader["company"]).ToString();

                                userResult.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<userDTO>();
            }

            return userResult;
        }

        public List<userDTO> GetUserDetail(string connectString)
        {
            List<userDTO> userResult = new List<userDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    string query = $"Select name, checkin_Details.qrcode, checkin_Details.scan_time " +
                                   $"From checkin_Details " +
                                   $"Left join checkin on checkin.qrcode = checkin_Details.qrcode" +
                                   $" where checkin_Details.type = 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userDTO obj = new userDTO();
                                obj.qrcode = (reader["qrcode"]).ToString();
                                obj.name = (reader["name"]).ToString();
                                obj.scan_time = Convert.ToDateTime(reader["scan_time"]);

                                userResult.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<userDTO>();
            }

            return userResult;
        }

        public bool Checkin(string qrcode, string connectstring)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstring))
                {
                    connection.Open();

                    string sqlString = "Insert into checkin_Details (qrcode, scan_time, type) values (@qrcode, @scan_time, @type)";

                    using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@qrcode", qrcode);
                        cmd.Parameters.AddWithValue("@scan_time", DateTime.Now);
                        cmd.Parameters.AddWithValue("@type", 0);
                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
