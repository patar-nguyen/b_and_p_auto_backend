using Microsoft.Data.SqlClient;
using LearningAPIs.Model;
using Microsoft.AspNetCore.DataProtection;
using System.Data;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Dapper;

namespace LearningAPIs.Service.UserAccountService
{
    public class UserAccountService : IUserAccountService
    {

        public IEnumerable<UserAccount> Get()
        {
            throw new NotImplementedException();
        }

        public UserAccount GetUserAccount(string username, string emailaddress = "")
        {
            UserAccount response = null;
            DataTable dt = new DataTable();

            if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(emailaddress))
            {
                return response;
            }

            string query = String.IsNullOrEmpty(username) ? "SELECT * FROM [UserAccountInfo] ua WITH(NOLOCK) WHERE ua.Email = @Email" : "SELECT * FROM [UserAccountInfo] ua WITH(NOLOCK) WHERE ua.Username = @Username";

            using (SqlConnection conn = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!String.IsNullOrEmpty(username))
                    {
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar);
                        cmd.Parameters["@Username"].Value = username;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar);
                        cmd.Parameters["@Email"].Value = emailaddress;
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                }
                conn.Close();
            }

            if (dt.Rows.Count > 0)
            {
                response = new UserAccount
                {
                    UserId = new Guid(dt.Rows[0]["UserId"].ToString()),
                    UserName = dt.Rows[0]["UserName"].ToString(),
                    FirstName = dt.Rows[0]["FirstName"].ToString(),
                    LastName = dt.Rows[0]["LastName"].ToString(),
                    Email = dt.Rows[0]["Email"].ToString(),
                    DateOfBirth = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString()),
                    EnrollDate = Convert.ToDateTime(dt.Rows[0]["EnrollDate"].ToString()),
                    Street = dt.Rows[0]["Street"].ToString(),
                    Province = dt.Rows[0]["Province"].ToString(),
                    Country = dt.Rows[0]["Country"].ToString(),
                    Zipcode = dt.Rows[0]["Zipcode"].ToString(),
                };
            }
            return response;
        }


        public UserAccount CreateUserAccount(UserAccountRequest request)
        {
            DataTable dt = new DataTable();
            UserAccount response = null;
            Guid userid;
            string sql = @"BEGIN TRY
                                BEGIN TRANSACTION
                                    DECLARE @UID UNIQUEIDENTIFIER = NEWID()
                                    INSERT INTO UserAccount ([UserId], [Username], [Password], [Email])
                                        VALUES
                                    (@UID, @Username, @Password, @Email)
                                    INSERT INTO UserInfo ([UserId], [FirstName], [LastName], [DateOfBirth], [Street], [Province], [Country], [Zipcode], [City])
                                        VALUES
                                    (@UID, @FirstName, @LastName, @DateOfBirth, @Street, @Province, @Country, @Zipcode, @City)
                                    SELECT @UID as 'UserId'
                                COMMIT
                                END TRY
                                BEGIN CATCH
	                                ROLLBACK;
	                                THROW;
                                END CATCH;
                        ";

            using (SqlConnection conn = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar);
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar);
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar);

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar);
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime);
                    cmd.Parameters.Add("@Street", SqlDbType.VarChar);
                    cmd.Parameters.Add("@Province", SqlDbType.VarChar);
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar);
                    cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar);
                    cmd.Parameters.Add("@City", SqlDbType.VarChar);

                    cmd.Parameters["@Username"].Value = request.UserName;
                    cmd.Parameters["@Password"].Value = request.Password;
                    cmd.Parameters["@Email"].Value = request.Email;

                    cmd.Parameters["@FirstName"].Value = request.FirstName;
                    cmd.Parameters["@LastName"].Value = request.LastName;
                    cmd.Parameters["@DateOfBirth"].Value = request.DateOfBirth;
                    cmd.Parameters["@Street"].Value = request.Street;
                    cmd.Parameters["@Province"].Value = request.Province;
                    cmd.Parameters["@Country"].Value = request.Country;
                    cmd.Parameters["@Zipcode"].Value = request.Zipcode;
                    cmd.Parameters["@City"].Value = request.City;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();

                    if (dt.Rows.Count > 0)
                    {
                        userid = new Guid(dt.Rows[0]["UserId"].ToString());
                        response = new UserAccount
                        {
                            UserId = userid,
                            UserName = request.UserName,
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            Email = request.Email,
                            DateOfBirth = Convert.ToDateTime(request.DateOfBirth),
                            //EnrollDate = Convert.ToDateTime(dt.Rows[0]["EnrollDate"].ToString()),
                            Street = request.Street,
                            Province = request.Province,
                            Country = request.Country,
                            Zipcode = request.Zipcode,

                        };
                        return response;
                    }
                }
            }
            return null;
        }

        public UserAccount GetUserAccountById(Guid userId)
        {
            UserAccount response = null;
            DataTable dt = new DataTable();

            string query = "SELECT * FROM [UserAccountInfo] ua WITH(NOLOCK) WHERE ua.UserId = @UserId";
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier);
                    cmd.Parameters["@UserId"].Value = userId;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                }
                conn.Close();
            }
            if (dt.Rows.Count > 0)
            {
                response = new UserAccount
                {
                    UserId = new Guid(dt.Rows[0]["UserId"].ToString()),
                    UserName = dt.Rows[0]["UserName"].ToString(),
                    FirstName = dt.Rows[0]["FirstName"].ToString(),
                    LastName = dt.Rows[0]["LastName"].ToString(),
                    Email = dt.Rows[0]["Email"].ToString(),
                    DateOfBirth = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString()),
                    EnrollDate = Convert.ToDateTime(dt.Rows[0]["EnrollDate"].ToString()),
                    Street = dt.Rows[0]["Street"].ToString(),
                    Province = dt.Rows[0]["Province"].ToString(),
                    Country = dt.Rows[0]["Country"].ToString(),
                    Zipcode = dt.Rows[0]["Zipcode"].ToString(),
                };
            }
            return response;
        }


        public bool UpdaterUserPassword(Guid userId, string password)
        {
             bool result = false;
             //updates password if password sent in request
            if (password != "" && password != null)
            {
                using (var connection = new SqlConnection(_connection))
                {
                    connection.Execute("UPDATE UserAccount SET [Password] = @Password WHERE UserId = @UserId", new { Password = password, UserId = userId });
                    result = true;
                }
            }
            return result;
        }

        public UserAccount UpdateUserAccount(Guid userId, UserAccountPatchRequest request)
        {
            UserAccount userAccount = GetUserAccountById(userId);

            //Updates email if email value sent in request
            if (request.Email != null && request.Email != "")
            {
                using (var connection = new SqlConnection(_connection))
                {
                    connection.Execute("UPDATE UserAccount SET Email = @Email WHERE UserId = @UserId", new { Email = request.Email, UserId = userId });
                }
                userAccount.Email = request.Email;
            }

            if (request.FirstName != null && !request.FirstName.Equals(""))
            {
                userAccount.FirstName = request.FirstName;
            }
            if (request.LastName != null && !request.LastName.Equals(""))
            {
                userAccount.LastName = request.LastName;
            }
            if (request.Street != null && !request.Street.Equals(""))
            {
                userAccount.Street = request.Street;
            }
            if (request.City != null && !request.City.Equals(""))
            {
                userAccount.City = request.City;
            }
            if (request.Province != null && !request.Province.Equals(""))
            {
                userAccount.Province = request.Province;
            }
            if (request.Country != null && !request.Country.Equals(""))
            {
                userAccount.Country = request.Country;
            }
            if (request.Zipcode != null && !request.Zipcode.Equals(""))
            {
                userAccount.Zipcode = request.Zipcode;
            }
            if (request.DateOfBirth != null && !request.DateOfBirth.Equals(""))
            {
                userAccount.DateOfBirth = Convert.ToDateTime(request.DateOfBirth);
            }

            using (var connection = new SqlConnection(_connection))
            {
                connection.Execute("UPDATE UserInfo SET FirstName = @FirstName, LastName = @LastName, Street = @Street, City = @City, Province = @Province, Country = @Country," +
                    "Zipcode = @Zipcode, DateOfBirth = @DateOfBirth WHERE UserId = @UserId"
                    , new { FirstName = userAccount.FirstName, LastName = userAccount.FirstName, Street = userAccount.Street, City = userAccount.City, Province = userAccount.Province, Country = userAccount.Country,
                            Zipcode = userAccount.Zipcode, DateOfBirth = userAccount.DateOfBirth, UserId = userId});
            }

            UserAccount response = userAccount;

            return response;
        }

    }
}
