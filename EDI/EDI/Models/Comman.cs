using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EDI.Models
{
    public static class Comman
    {
        public static string getConnection
        {
            get
            {
                string conn = ConfigurationManager.ConnectionStrings["EDIConnection"].ConnectionString;
                return conn;
            }
        }

        public static List<RoleModel> getAllMenuWhichNotAssignedToTheRole1(int RoleID)
        {
            List<RoleModel> listMenu = new List<RoleModel>();
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(getConnection))
            {
                cmd = new SqlCommand("SPO_getAllMenuWhichNotAssignedToTheRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleId", RoleID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    RoleModel listItems = new RoleModel();
                    listItems.MenuId = (int)dr["m_id"];
                    listItems.MenuName = (string)dr["m_name"];
                    listMenu.Add(listItems);
                }

            }
            return listMenu.ToList();
        }

        public static List<menu_master> getAllMenuWhichNotAssignedToTheRole(int RoleID)
        {
            List<menu_master> listMenu = new List<menu_master>();
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(getConnection))
            {
                cmd = new SqlCommand("SPO_getAllMenuWhichNotAssignedToTheRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleId", RoleID);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    menu_master listItems = new menu_master ();
                    listItems.m_id = (string)dr["m_id"];
                    listItems.m_name = (string)dr["m_name"];
                    listMenu.Add(listItems);
                }
               
            }
            return listMenu.ToList();
        }
    }
}

