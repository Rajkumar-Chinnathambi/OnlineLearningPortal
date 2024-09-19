using OnlineLearningPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Collections.Generic;

namespace OnlineLearningPortal.Repository
{
    public class CourseRepository
    {
        private string connectionString= "Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI";
        public CourseRepository() { }
        
        public List<CourseModel> GetAllCourses()
        {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                List<CourseModel> list = new List<CourseModel>();
                try
                {
                    SqlCommand cmd = new SqlCommand("SPR_AllCourses",_connection);
                    cmd.CommandType=CommandType.StoredProcedure;
                    _connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                   
                    while (reader.Read()) {
                        list.Add(new CourseModel()
                        {
                            CourseID = (int)reader["CourseID"],
                            CourseCatagory = (string)reader["CourseCatagory"],
                            Coursename = (string)reader["Coursename"],
                            Coursesrc = (string)reader["Coursesrc"],
                            Coursedesc = (string)reader["Coursedesc"],
                            CoursePhoto = (byte[])reader["Courseimage"],
                            CourseType = (string)reader["CourseType"]
                        });
                    }
                    return list;
                }
                catch {
                    return list;
                }
            }
            return null;
        }

        public bool AddCourse(CourseModel course)
        {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_Course", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    _connection.Open();
                    cmd.Parameters.AddWithValue("@CourseCatagory", course.CourseCatagory);
                    cmd.Parameters.AddWithValue("@Coursename", course.Coursename);
                    cmd.Parameters.AddWithValue("@Coursesrc", course.Coursesrc); 
                    cmd.Parameters.AddWithValue("@Coursedesc", course.Coursedesc);
                    cmd.Parameters.AddWithValue("@Courseimage", course.CoursePhoto);
                    cmd.Parameters.AddWithValue("@CourseType", course.CourseType);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { return false; }
            }
        }

        public List<CourseModel> GetCourseById(CourseModel coursemodel) {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_SingleCourse", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", coursemodel.CourseID);
                    _connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<CourseModel> list = new List<CourseModel>();
                    while (reader.Read())
                    {
                        list.Add(new CourseModel() {
                            CourseID = (int)reader["CourseID"],
                            CourseCatagory = (string)reader["CourseCatagory"],
                            Coursename = (string)reader["Coursename"],
                            Coursesrc = (string)reader["Coursesrc"],
                            Coursedesc = (string)reader["Coursedesc"],
                            CoursePhoto = (byte[])reader["Courseimage"],
                            CourseType=(string)reader["CourseType"],
                            Enroll=(string)reader["CourseType"],
                        });
                    }
                    return list;
                }
                catch {
                    return null;
                }
            }
        }
        public bool SaveEditCourse(CourseModel coursemodel) {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPU_Course", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", coursemodel.CourseID);
                    cmd.Parameters.AddWithValue("@CourseCatagory", coursemodel.CourseCatagory);
                    cmd.Parameters.AddWithValue("@Coursename", coursemodel.Coursename);
                    cmd.Parameters.AddWithValue("@Coursesrc", coursemodel.Coursesrc);
                    cmd.Parameters.AddWithValue("@Coursedesc", coursemodel.Coursedesc);
                    _connection.Open();
                    int updateStatus = cmd.ExecuteNonQuery();
                    if (updateStatus > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch {
                    return false ;
                }
            }
        }
        public bool deleteCourse(CourseModel coursemodel) {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPD_Course", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseID", coursemodel.CourseID);                   
                    _connection.Open();
                    int deleteStatus = cmd.ExecuteNonQuery();
                    if (deleteStatus > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }

        }
        public List<UserModel> GetAllUsers()
        {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPR_AllUser", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    _connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<UserModel> list = new List<UserModel>();
                    while (reader.Read())
                    {
                        list.Add(new UserModel()
                        {
                            Id = (int)reader["UserId"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            DOB = reader["DateOfBith"].ToString(),
                            Gender = (string)reader["Gender"],
                            Email = (string)reader["Email"],
                            Address = (string)reader["Address"],
                            City = (string)reader["City"],
                            State = (string)reader["State"],
                            UserName = (string)reader["UserName"],
                            Password = (string)reader["Password"],
                            UserType= (string)reader["UserType"],
                            PhoneNumber = reader["Mobile"].ToString()
                        });
                    }
                    return list;
                }
                catch
                {
                    return null;
                }
            }
        }
        public int AllUserCount()
        {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPR_AllUserCount", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    _connection.Open();
                    int userCount =(int)cmd.ExecuteScalar();
                    return userCount;
                    
                }
                catch
                {
                    return 0;
                }
            }
        }
        public int AllCourseCount()
        {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPR_CourseCount", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    _connection.Open();
                    int courseCount = (int)cmd.ExecuteScalar();
                    return courseCount;

                }
                catch
                {
                    return 0;
                }
            }
        }
        public bool EditUser(UserModel model)
        {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPU_User", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.Id);
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBith", model.DOB);
                    cmd.Parameters.AddWithValue("@Gender", model.Gender);
                    cmd.Parameters.AddWithValue("@Mobile", model.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Address", model.Address);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@State", model.State);
                    cmd.Parameters.AddWithValue("@UserName", model.UserName);
                    EncPassword encPassword = new EncPassword();
                    cmd.Parameters.AddWithValue("@Password", encPassword.Encrpte(model.Password));
                    _connection.Open();
                    int updateStatus = cmd.ExecuteNonQuery();
                    if (updateStatus > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool DeleteUser(UserModel model)
        {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPD_User", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", model.Id);                   
                    _connection.Open();
                    int deleteStatus = cmd.ExecuteNonQuery();
                    if (deleteStatus>0)
                    {
                        return true;
                    }
                    else {
                        return false ;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        
        public List<UserModel> GetSingleUser(UserModel usermodel)
        {
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    SqlCommand _command = new SqlCommand("SPR_GetUserById", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@UserId", usermodel.Id);
                    _connection.Open();
                    SqlDataReader _reader = _command.ExecuteReader();
                    List<UserModel> list = new List<UserModel>();
                    while (_reader.Read())
                    {
                        list.Add(new UserModel()
                        {
                            Id = (int)_reader["UserId"],
                            FirstName = (string)_reader["FirstName"],
                            LastName = (string)_reader["LastName"],
                            DOB = _reader["DateOfBith"].ToString(),
                            Gender = (string)_reader["Gender"],
                            Email = (string)_reader["Email"],
                            Address = (string)_reader["Address"],
                            City = (string)_reader["City"],
                            State = (string)_reader["State"],
                            UserName = (string)_reader["UserName"],
                            Password = (string)_reader["Password"],
                            UserType = (string)_reader["UserType"],
                            PhoneNumber = _reader["Mobile"].ToString()
                        });
                    }
                    return list;
                }
                catch (Exception ex) {
                    return null;
                }
            }

            return null;
        }
        public  UserCourse IsUserCourseEnroll(UserModel usermodel, CourseModel coursemodel)
        {
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();//SPR_ChecKUserByCourse
                    SqlCommand _command = new SqlCommand("SPR_ChecKUserByCourse", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@UserId", usermodel.Id);
                    _command.Parameters.AddWithValue("@CourseId", coursemodel.CourseID);
                    SqlDataReader rd  = _command.ExecuteReader();
                    while (rd.Read()) { 
                        var usercourse = new UserCourse()
                        {
                            UserId = (int)rd["UserId"],
                            CourseId = (int)rd["CourseId"],
                            EnrollStatus = (string)rd["EnrollStatus"]
                        };
                        return usercourse;
                    }
                    return null;
                }
                catch (Exception ex) { return null; }
            }
        }
        public string CourseEnroll(UserModel usermodel,CourseModel coursemodel)
        {
            using(SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                                     
                    if (IsUserCourseEnroll(usermodel,coursemodel)==null)
                    {
                        _connection.Open();
                        SqlCommand _cmd = new SqlCommand("SPI_UserCourse", _connection);
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.AddWithValue("@UserId", usermodel.Id);
                        _cmd.Parameters.AddWithValue("@CourseId", coursemodel.CourseID);
                        _cmd.Parameters.AddWithValue("@EnrollStatus", "Pending");
                        int result = _cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return "Waiting For Admin Approval";
                        }
                        else
                        {
                            return "Something error,try again";
                        }
                   
                    }
                    return "Waiting For Admin Approval";
                }
                catch (Exception ex) { 
                    return "Something errror,try again"; ;
                }
            }
        }
        /*public CourseModel GetCourseById(CourseModel coursemodel) {
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("SPR_GetCourseById", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@CourseId", coursemodel.CourseID);
                    SqlDataReader rd = _command.ExecuteReader();
                    while (rd.Read()) {
                        return new CourseModel()
                        {
                            CourseID = (int)rd["CourseID"],
                            CourseCatagory = (string)rd["CourseCatagory"],
                            Coursename = (string)rd["Coursename"],
                            Coursesrc = (string)rd["Coursesrc"],
                            Coursedesc = (string)rd["Coursedesc"],
                            CoursePhoto = (byte[])rd["CoursePhoto"],
                            CourseType = (string)rd["CourseType"]
                        };
                    }
                    return null;
                }
                catch (Exception ex) {
                    return null;
                }
            }
        }*/
        public List<CourseModel> GetCourseByUser(UserModel usermodel) {
            List<CourseModel> list = new List<CourseModel>();
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("SPR_GetCourseByUser", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@UserId", usermodel.Id);
                    SqlDataReader reader = _command.ExecuteReader();
                    while (reader.Read()) {
                        list.Add(new CourseModel()
                        {
                            CourseID = (int)reader["CourseID"],
                            Coursename = (string)reader["Coursename"],
                            Coursedesc = (string)reader["Coursedesc"],
                            CoursePhoto = (byte[])reader["Courseimage"],
                            Enroll = (string)reader["Enroll"],
                            CourseType = (string)reader["CourseType"],
                            CourseCatagory= (string)reader["CourseCatagory"]
                        });
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    return list;
                }
            }
            return list;
        }
        public List<CourseModel> UnEnrollCourse(UserModel usermodel)
        {
            List<CourseModel> list = new List<CourseModel>();
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("SPR_UnEnrollCourse", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@UserId", usermodel.Id);
                    SqlDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new CourseModel()
                        {
                            CourseID = (int)reader["CourseID"],
                            CourseCatagory = (string)reader["CourseCatagory"],
                            Coursename = (string)reader["Coursename"],
                            Coursesrc = (string)reader["Coursesrc"],
                            Coursedesc = (string)reader["Coursedesc"],
                            CoursePhoto = (byte[])reader["Courseimage"],
                            Enroll = "Enroll"
                        });
                    }
                }
                catch (Exception ex)
                {
                    return list;
                }
            }
            return list;
        }
        public string DeleteCourse(CourseModel coursemodel,UserModel usermodel) {
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("SPD_UserCourse", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@CourseId", coursemodel.CourseID);
                    _command.Parameters.AddWithValue("@UserId", usermodel.Id);
                    int result = _command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return "Successfully Deleted";
                    }
                    else {
                        return "Something error, try after some time";
                    }
                }
                catch (Exception ex)
                {
                    return "Something error, try again";
                }
            }
        }
        public string VideoSave(VideoModel videomodel)
        {
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("SPI_Video", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@videoName", videomodel.videoName);                   
                    _command.Parameters.AddWithValue("@videoData", SqlDbType.VarBinary).Value = videomodel.videoPath;
                    _command.Parameters.AddWithValue("@fileExt", SqlDbType.VarChar).Value = "mp4";
                    int result = _command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return "Successfully Deleted";
                    }
                    else
                    {
                        return "Something error, try after some time";
                    }
                }
                catch (Exception ex)
                {
                    return "Something error, try again";
                }
            }
        }
        public List<VideoModel> GetAllVideos()
        {
            List<VideoModel> list = new List<VideoModel>();
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("SPR_GetAllVideo", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new VideoModel()
                        {
                            videoId = (int)reader["videoId"],
                            videoName = (string)reader["videoName"],
                            videoPath = (byte[])reader["videoData"]
                        });
                    }
                }
                catch (Exception ex)
                {
                    return list;
                }
            }
            return list;
        }

        public List<PendingCourse> GetPendingApprovalCourse()
        {
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    List<PendingCourse> list = new List<PendingCourse>();
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("GetPendingApprovalCourse", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new PendingCourse()
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            CourseId = (int)reader["CourseId"],
                            UserName= (string)reader["UserName"],
                            CourseName = (string)reader["CourseName"],
                            EnrollStatus = (string)reader["EnrollStatus"],
                            CourseType = (string)reader["CourseType"]
                        });
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
        public bool CourseAcceptOrReject(int id, int status)
        {

            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("UpdateApprovalCourse", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@id", id);
                    if (status == 1)
                    {
                        _command.Parameters.AddWithValue("@EnrollStatus", "Approved");
                    }
                    else
                    {
                        _command.Parameters.AddWithValue("@EnrollStatus", "Reject");
                    }
                    int result = _command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public List<Quize> GetAllQuize(CourseModel course)
        {
            List<Quize> list = new List<Quize>();
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("SPR_QuizeByCourseId", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@courseID", course.CourseID);
                    SqlDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Quize()
                        {
                            quizeId = (int)reader["quizeId"],
                            courseID = (int)reader["courseID"],
                            quize = (string)reader["quize"],
                            option1 = (string)reader["option1"],
                            option2 = (string)reader["option2"],
                            option3 = (string)reader["option3"],
                            option4 = (string)reader["option4"],
                            answer = (string)reader["answer"]
                        });
                    }
                    return list;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }
        public List<UserModel> GetCourseWithUsers(CourseModel course)
        {
            List<UserModel> userlist = new List<UserModel>();
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _command = new SqlCommand("SPR_GetAllUserByCourse", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@courseID", course.CourseID);
                    SqlDataReader reader = _command.ExecuteReader();
                    while (reader.Read())
                    {
                        userlist.Add(new UserModel()
                        {
                            Id = (int)reader["UserId"],
                            UserName = (string)reader["UserName"],
                            Email = (string)reader["Email"],
                            PhoneNumber = reader["Mobile"].ToString()
                        });
                    }
                    return userlist;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public bool SavePayment(PaymentModel payment)
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionString))
                {
                    _connection.Open();
                    SqlCommand cmd = new SqlCommand("SPI_Payement", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@courseId", payment.CourseId);
                    cmd.Parameters.AddWithValue("@userId", payment.UserId);
                    cmd.Parameters.AddWithValue("@paymentMode", payment.paymentMode);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch { 
                return false;
            }
        }

        public PaymentModel GetPaymentByUserCourse(PaymentModel payment) {
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionString))
                {
                    _connection.Open();
                    SqlCommand cmd = new SqlCommand("GetPaymentByUserCourse", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@courseId", payment.CourseId);
                    cmd.Parameters.AddWithValue("@userId", payment.UserId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        payment.CourseId = (int)reader["courseId"];
                        payment.UserId = (int)reader["userId"];
                        payment.PaymentId = (int)reader["paymentMode"];
                        payment.CreateAt = (DateTime)reader["createAt"];
                    }
                    return payment;
                }
            }
            catch
            {
                return null;
            } 
        }
        public List<PaymentModel> GetAllPaymentList() {
            try
            {
                using (SqlConnection _connection = new SqlConnection(connectionString))
                {
                    List<PaymentModel> paymentList= new List<PaymentModel>();
                    _connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllPayments", _connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        paymentList.Add(new PaymentModel
                        {
                            CourseId = (int)reader["courseId"],
                            UserId = (int)reader["userId"],
                            paymentMode = (string)reader["paymentMode"],
                            CreateAt = (DateTime)reader["createAt"],
                            Username = (string)reader["UserName"],
                            Coursename = (string)reader["Coursename"],
                        });
                    }
                    return paymentList;
                }
            }
            catch(Exception ex) 
            {
                return null;
            }
        }
    }
}