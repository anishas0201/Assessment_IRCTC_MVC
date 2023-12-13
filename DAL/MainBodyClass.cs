using Assessment_IRCTC_Revervation.ErrorLog;
using Assessment_IRCTC_Revervation.Interface;
using Assessment_IRCTC_Revervation.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Assessment_IRCTC_Revervation.DAL
{
    public class MainBodyClass : MainBodyInterface
    {
        private readonly string connection = Startup.StaticConfiguration["customData:Connectionstring"];
        ResponseModel res = new ResponseModel();


        public ResponseModel SaveTrainDetails(TrainMaster roleuser)
        {
            ResponseModel res = new ResponseModel();
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_saveTrainDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@trainName", roleuser.trainName);
                        cmd.Parameters.AddWithValue("@trainNumber", roleuser.trainNumber);
                        cmd.Parameters.AddWithValue("@fromPlace", roleuser.fromPlace);
                        cmd.Parameters.AddWithValue("@toDestination", roleuser.toDestination);
                        cmd.Parameters.AddWithValue("@seatAvailability", roleuser.seatAvailability);
                        var Id = cmd.ExecuteNonQuery();
                        if (Id > 0)
                        {
                            res.status = true;
                            res.message = "Train Details Saved Successfully!!";
                        }
                        else
                        {
                            res.status = false;
                            res.message = "oops!! Error Occured!!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";
                    Helper.WriteLog("The error is:" + ex);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return res;
        }


        public List<TrainMaster> GetTrainList()
        {

            ResponseModel res = new ResponseModel();
            List<TrainMaster> result = new List<TrainMaster>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("select * from tbl_IRCTC_TrainMaster", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TrainMaster user = new TrainMaster();
                                user.Id = (int)reader["Id"];
                                user.trainName = (string)reader["trainName"];
                                user.trainNumber = (int)reader["trainNumber"];
                                user.fromPlace = (string)reader["fromPlace"];
                                user.toDestination = (string)reader["toDestination"];
                                user.seatAvailability = (Boolean)reader["seatAvailability"];
                                result.Add(user);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";
                    Helper.WriteLog("The error is:" + ex);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return result;
        }


        public ResponseModel DeleteTrain(int Id)
        {
            ResponseModel res = new ResponseModel();
            using (SqlConnection con = new SqlConnection(connection))
            {

                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_IRCTC_delete_TainById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        var id = cmd.ExecuteNonQuery();
                        if (id > 0)
                        {
                            res.status = true;
                            res.message = "Data Deleted Successfully..!!";
                        }
                        else
                        {
                            res.status = false;
                            res.message = "Please Check...Something Went wrong...!!!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Exception Occure..!!";
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return res;

        }

        public TrainMaster GetTrainbyId(int Id)
        {
            ResponseModel res = new ResponseModel();
            TrainMaster user = new TrainMaster();

            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_IRCTC_get_TrainbyId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user.Id = string.IsNullOrEmpty(reader["Id"].ToString()) ? 0 : Convert.ToInt32(reader["Id"]);
                                user.trainName = string.IsNullOrWhiteSpace(reader["trainName"].ToString()) ? "" : reader["trainName"].ToString();
                                user.trainNumber = string.IsNullOrEmpty(reader["trainNumber"].ToString()) ? 0 : Convert.ToInt32(reader["trainNumber"]);
                                user.fromPlace = string.IsNullOrWhiteSpace(reader["fromPlace"].ToString()) ? "" : reader["fromPlace"].ToString();
                                user.toDestination = string.IsNullOrWhiteSpace(reader["toDestination"].ToString()) ? "" : reader["toDestination"].ToString();
                                user.seatAvailability = string.IsNullOrWhiteSpace(reader["seatAvailability"].ToString()) ? false : Convert.ToBoolean(reader["seatAvailability"].ToString());

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


            }
            return user;

        }

        public ResponseModel UpdateTrainDetails(TrainMaster tmaster)
        {

            ResponseModel res = new ResponseModel();

            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_IRCTC_Update_TrainDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", tmaster.Id);
                        cmd.Parameters.AddWithValue("@trainName", tmaster.trainName);
                        cmd.Parameters.AddWithValue("@trainNumber", tmaster.trainNumber);
                        cmd.Parameters.AddWithValue("@fromPlace", tmaster.fromPlace);
                        cmd.Parameters.AddWithValue("@toDestination", tmaster.toDestination);
                        cmd.Parameters.AddWithValue("@seatAvailability", tmaster.seatAvailability);



                        var Id = cmd.ExecuteNonQuery();

                        if (Id > 0)
                        {
                            res.status = true;
                            res.message = "Train Details Updated Successfully!!";

                        }
                        else
                        {
                            res.status = false;
                            res.message = "oops!! Error Occured!!";
                        }

                    }

                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";
                    Helper.WriteLog("The error is:" + ex);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }

            }
            return res;
        }




        public ResponseModel SaveTicket(TicketBookingUser form)
        {
            ResponseModel res = new ResponseModel();

            MailMessage msg = new MailMessage();

            SmtpClient smtp = new SmtpClient();
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_IRCTC_SaveTicket", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@trainName", form.trainName);
                        cmd.Parameters.AddWithValue("@trainNumber", form.trainNumber);
                        cmd.Parameters.AddWithValue("@emailId", form.emailId);
                        cmd.Parameters.AddWithValue("@departureDate", form.departureDate);
                        cmd.Parameters.AddWithValue("@ticketFare", form.ticketFare);

                        var Id = cmd.ExecuteNonQuery();
                        if (Id > 0)
                        {
                            res.status = true;
                            msg.From = new MailAddress("anisha.singh@cylsys.com");
                            msg.To.Add(form.emailId);

                            msg.Subject = "Testing Mail";
                            msg.Body = "Dear Customer" + " Your Ticket Booking is Confirmed..!! The Journey starts from " + form.departureDate + ". Have a Safe Journey.";
                            msg.IsBodyHtml = true;
                            smtp.Host = "smtp-mail.outlook.com";
                            smtp.Port = 587;
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential("anisha.singh@cylsys.com", "Cylsys@2");
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(msg);

                            res.message = "Ticket Booking Confirmed Successfully!!";
                        }
                        else
                        {
                            res.status = false;
                            res.message = "oops!! Error Occured!!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";
                    Helper.WriteLog("The error is:" + ex);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return res;
        }


        public List<TicketBookingUser> GetTicketList()
        {

            ResponseModel res = new ResponseModel();
            List<TicketBookingUser> result = new List<TicketBookingUser>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetTicketList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TicketBookingUser user = new TicketBookingUser();
                                user.ticketId = (int)reader["ticketId"];
                                user.trainName = (string)reader["trainName"];
                                user.trainNumber = (string)reader["trainNumber"];
                                user.emailId = (string)reader["emailId"];
                                user.departureDate = (DateTime)reader["departureDate"];
                                user.ticketFare = (int)reader["ticketFare"];
                                result.Add(user);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Errorr!!!";
                    Helper.WriteLog("The error is:" + ex);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return result;
        }





    }
}

