using Assessment_IRCTC_Revervation.ErrorLog;
using Assessment_IRCTC_Revervation.Interface;
using Assessment_IRCTC_Revervation.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Assessment_IRCTC_Revervation.DAL
{
    public class RegisterClass : RegisterInterface
    {
        private readonly string connection = Startup.StaticConfiguration["customData:Connectionstring"];
        public ResponseModel SignUpAction(RegisterUser objmodel)
        {

            ResponseModel res = new ResponseModel();
            RegisterUser User = new RegisterUser();
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand checkUsernameCmd = new SqlCommand("SELECT COUNT(*) FROM tbl_IRCTC_Registration WHERE emailId = @emailId", con))
                    {
                        checkUsernameCmd.Parameters.AddWithValue("@emailId", objmodel.emailId);
                        int existingUserCount = (int)checkUsernameCmd.ExecuteScalar();

                        if (existingUserCount > 0)
                        {
                            res.status = false;
                            res.message = "This EmailId already exists. Please choose a different MailId.";
                        }
                        else
                        {

                            using (SqlCommand cmd = new SqlCommand("sp_IRCTC_SignUp", con))

                            {

                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@fullName", objmodel.fullName);
                                cmd.Parameters.AddWithValue("@emailId", objmodel.emailId);
                                string encryptedPassword = EncryptPassword(objmodel.decryptedPassword);
                                cmd.Parameters.AddWithValue("@encryptedPassword", encryptedPassword);
                                cmd.Parameters.AddWithValue("@decryptedPassword", objmodel.decryptedPassword);
                                cmd.Parameters.AddWithValue("@userType", objmodel.userType);
                                var id = cmd.ExecuteNonQuery();
                                if (id > 0)
                                {
                                    res.status = true;
                                    res.message = "Registered Successfully";
                                }
                                else
                                {
                                    res.status = false;
                                    res.message = "Registration failed. Please try again.";
;

                                }
                            }

                        }
                    }
                }

                catch (Exception ex)
                {
                    res.status = false;
                    res.message = "Error!!";
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

        private string EncryptPassword(string password)
        {
            string encryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public ResponseModel CheckLogin(RegisterUser objmodel)
        {
            ResponseModel res = new ResponseModel();


            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_validate_IRCTCLogin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@fullName", objmodel.fullName);
                        cmd.Parameters.AddWithValue("@emailId", objmodel.emailId);
                        cmd.Parameters.AddWithValue("@decryptedPassword", objmodel.decryptedPassword);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                RegisterUser user = new RegisterUser
                                {

                                    fullName = Convert.ToString(reader["fullName"]),
                                    emailId = Convert.ToString(reader["emailId"])
                                };

                                res.status = true;
                                res.message = "Login successful!!";
                            }
                            else
                            {
                                res.status = false;
                                res.message = "Login failed. Check your credentials.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.status = false;
                res.message = "Error occurred during login.";
                Helper.WriteLog("The error is:" + ex);
            }

            return res;
        }
    }
}
