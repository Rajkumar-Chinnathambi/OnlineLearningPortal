using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningPortal.Repository
{
    public class EncPassword
    {
        public string Encrpte(string password)
        {
            byte[] encrptePassword = new byte[password.Length];
            encrptePassword = System.Text.Encoding.UTF8.GetBytes(password);
            string encodePassword = Convert.ToBase64String(encrptePassword);
            return encodePassword;
        }
    }
}