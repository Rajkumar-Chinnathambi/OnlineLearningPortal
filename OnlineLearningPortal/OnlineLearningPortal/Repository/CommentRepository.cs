using OnlineLearningPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineLearningPortal.Repository
{
    public class CommentRepository
    {
        string connettion;
        public CommentRepository()
        {
            connettion = "Data Source=SYSLP779\\SQLEXPRESS;database=ClaySys;Integrated Security=SSPI";
        }
        public List<CommentModel> GetAllCommentsByCourse(CourseModel course)
        {
            List<CommentModel> list = new List<CommentModel>();
            using (SqlConnection con = new SqlConnection(connettion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPR_allComments", con);
                    cmd.Parameters.AddWithValue("@CourseId", course.CourseID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new CommentModel
                        {
                            CourseId = (int)reader["CourseId"],
                            commentText = (string)reader["comment"]
                        });
                    }
                    return list;

                }
                catch (Exception ex) { }
                return list;
            }
        }

        public void SaveComment(CommentModel comment)
        {
            using (SqlConnection con = new SqlConnection(connettion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_comment", con);
                    cmd.Parameters.AddWithValue("@CourseId", comment.CourseId);
                    cmd.Parameters.AddWithValue("@UserId", comment.UserId);
                    cmd.Parameters.AddWithValue("@comment", comment.commentText);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex) { }

            }
        }
        public void SaveContactComment(ContactCommentModel contactComment)
        {
            using (SqlConnection con = new SqlConnection(connettion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_ContactComment", con);
                    cmd.Parameters.AddWithValue("@name", contactComment.Name);
                    cmd.Parameters.AddWithValue("@email", contactComment.Email);
                    cmd.Parameters.AddWithValue("@country", contactComment.Country);
                    cmd.Parameters.AddWithValue("@message", contactComment.Message);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex) { }

            }
        }
        public List<ContactCommentModel> GetAllContactComments()
        {
            using (SqlConnection con = new SqlConnection(connettion))
            {
                try
                {
                    List<ContactCommentModel> contactComments = new List<ContactCommentModel>();
                    SqlCommand cmd = new SqlCommand("SPR_AllContactComment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader=cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        contactComments.Add(new ContactCommentModel
                        {
                            Name = reader["name"].ToString(),
                            Email = reader["email"].ToString(),
                            Country = reader["country"].ToString(),
                            Message = reader["message"].ToString()
                        });
                    }
                    return contactComments;

                }
                catch (Exception ex) { }

            }
            return null;
        }
    }
}