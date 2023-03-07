using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary
{
    public class CRUD
    {

        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["EcomContext"].ToString());

        public void InsertIntoAspNetUserRoles(string UserId,string RoleId)
        {
            string query = $"insert into AspNetUserRoles (UserId,RoleId) values ('{UserId}','{RoleId}')";
            SqlCommand cmd = new SqlCommand(query, Con);
            Con.Open();

            cmd.ExecuteNonQuery();

            Con.Close();

        }

        public string FetchUserTypeInAspNetUsers(string Email)
        {
            string query = $"select UserType from AspNetUsers where Email='{Email}'";
            SqlDataAdapter da = new SqlDataAdapter(query,Con);

            DataTable dt=new DataTable();

            da.Fill(dt);

            string UserType = "";

            foreach (DataRow drow in dt.Rows)
            {
                UserType = (drow["UserType"]).ToString();

            }

            return UserType;
        }
        
        public void deleteFromAspNetUsers(string alphaId)
        {
            string query = $"delete from AspNetUsers where Id='{alphaId}'";
            SqlCommand cmd = new SqlCommand(query, Con);
            Con.Open();

            cmd.ExecuteNonQuery();

            Con.Close();
        }
    }
}
