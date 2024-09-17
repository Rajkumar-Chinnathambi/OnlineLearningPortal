using OnlineLearningPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Data;

namespace OnlineLearningPortal.Repository
{
    public class HomeRepository
    {
        
        private SqlCommand _command;
        private SqlDataReader _reader;
        EncPassword encPassword;
        public HomeRepository()
        {
            encPassword = new EncPassword();
        }
        public List<UserModel> UserValid(UserModel usermodel)
        {
            using(SqlConnection _connection= new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
            {
                
                _command = new SqlCommand("SPR_SingleUser", _connection);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("@Email", usermodel.Email); 
                if (usermodel.Password == "testAdmin@123" || usermodel.Password == "sri@12345")
                {
                    _command.Parameters.AddWithValue("@Password", usermodel.Password);
                }
                else
                {
                    _command.Parameters.AddWithValue("@Password", encPassword.Encrpte(usermodel.Password));
                }
                _connection.Open();
                _reader = _command.ExecuteReader();
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
        }

        public bool RegisterValid(UserModel usermodel) {
            using (SqlConnection _connection = new SqlConnection("Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI"))
                try
                {
                    var list = new List<UserModel>();
                    _command = new SqlCommand("SPI_User", _connection);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@FirstName", usermodel.FirstName);
                    _command.Parameters.AddWithValue("@LastName", usermodel.LastName);
                    _command.Parameters.AddWithValue("@DateOfBith", usermodel.DOB);
                    _command.Parameters.AddWithValue("@Gender", usermodel.Gender);
                    _command.Parameters.AddWithValue("@Mobile", usermodel.PhoneNumber);
                    _command.Parameters.AddWithValue("@Email", usermodel.Email);
                    _command.Parameters.AddWithValue("@Address", usermodel.Address);
                    _command.Parameters.AddWithValue("@City", usermodel.City);
                    _command.Parameters.AddWithValue("@State", usermodel.State);
                    _command.Parameters.AddWithValue("@UserName", usermodel.UserName);                      
                    _command.Parameters.AddWithValue("@Password", encPassword.Encrpte(usermodel.Password));
                    _command.Parameters.AddWithValue("@UserType", usermodel.UserType);
                    _connection.Open();
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
                catch (Exception ex) { 
                    return false;
                }

        }
       
    }
}