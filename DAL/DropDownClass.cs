using Assessment_IRCTC_Revervation.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_IRCTC_Revervation.DAL
{
    public class DropDownClass
    {
        private readonly string connection = Startup.StaticConfiguration["customData:Connectionstring"];
        public List<TrainList> GetTrainList()
        {
            List<TrainList> train = new List<TrainList>();
            ResponseModel res = new ResponseModel();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select Id,trainName from tbl_IRCTC_TrainMaster", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TrainList u = new TrainList();
                                u.trainName = string.IsNullOrWhiteSpace(reader["trainName"].ToString()) ? "" : reader["trainName"].ToString();                               
                                u.Id = string.IsNullOrEmpty(reader["Id"].ToString()) ? 0 : Convert.ToInt32(reader["Id"]);
                                train.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";

                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
                return train;
            }

        }

        public List<TrainNumber> GetTrainNumbers(int Id)
        {
            List<TrainNumber> trainNumbers = new List<TrainNumber>();
            ResponseModel res = new ResponseModel();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select Id,trainNumber from tbl_IRCTC_TrainMaster WHERE Id = @Id", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TrainNumber u = new TrainNumber();                               
                                u.trainNumber = string.IsNullOrEmpty(reader["trainNumber"].ToString()) ? 0 : Convert.ToInt32(reader["trainNumber"]);
                                u.Id = string.IsNullOrEmpty(reader["Id"].ToString()) ? 0 : Convert.ToInt32(reader["Id"]);
                                trainNumbers.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";

                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
                return trainNumbers;
            }

        }

        public List<MailList> GetMailList()
        {
            List<MailList> mail = new List<MailList>();
            ResponseModel res = new ResponseModel();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select registrationId,emailId from tbl_IRCTC_Registration", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MailList u = new MailList();
                                u.emailId = string.IsNullOrWhiteSpace(reader["emailId"].ToString()) ? "" : reader["emailId"].ToString();
                                u.registrationId = string.IsNullOrEmpty(reader["registrationId"].ToString()) ? 0 : Convert.ToInt32(reader["registrationId"]);
                                mail.Add(u);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";

                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
                return mail;
            }

        }
    }
}
