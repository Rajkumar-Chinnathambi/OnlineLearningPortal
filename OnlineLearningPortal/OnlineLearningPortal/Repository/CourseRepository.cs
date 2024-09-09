using OnlineLearningPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace OnlineLearningPortal.Repository
{
    public class CourseRepository
    {
        private string connectionString="Data Source=NUVOBOOK_S2\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI";
        public CourseRepository() { }
        
        public List<CourseModel> GetAllCourses()
        {
            using (SqlConnection _connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPR_AllCourses",_connection);
                    cmd.CommandType=CommandType.StoredProcedure;
                    _connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<CourseModel> list = new List<CourseModel>();
                    while (reader.Read()) {
                        list.Add(new CourseModel()
                        {
                            CourseID = (int)reader["CourseID"],
                            CourseCatagory = (string)reader["CourseCatagory"],
                            Coursename = (string)reader["Coursename"],
                            Coursesrc = (string)reader["Coursesrc"],
                            Coursedesc = (string)reader["Coursedesc"]
                        });
                    }
                    return list;
                }
                catch {
                    return null;
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
                    cmd.Parameters.AddWithValue("@Coursesrc", course.Coursesrc); ;
                    cmd.Parameters.AddWithValue("@Coursedesc", course.Coursedesc);
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
                            Coursedesc = (string)reader["Coursedesc"]
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
                    cmd.Parameters.AddWithValue("@Password", model.Password);
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
            using (SqlConnection _connection = new SqlConnection("Data Source=NUVOBOOK_S2\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
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

            return null;
        }

    }
}