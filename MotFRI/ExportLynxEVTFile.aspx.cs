﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MotFRI
{
    public partial class ExportLynxEVTFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();

            string CompCode = gl.GetCompetitionCode();

            string TxtOut = string.Empty;

            string cs = ConfigurationManager.ConnectionStrings["AthleticsConnectionString"].ConnectionString;

            SqlConnection sqlConnection1 = new SqlConnection(cs);
            //"Data Source=LOCLAHOST;Initial Catalog=Athletics;Persist Security Info=True;User ID=FyrirspurnIFrjalsar;Password=Langst0kk.");

            SqlCommand cmd2 = new SqlCommand();
            SqlDataReader reader2;

            cmd2.CommandText = "dbo.ReturnLynxEvtFile";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@CompCode", CompCode);
            cmd2.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                string Linxxx = reader2[15].ToString();
                TxtOut = TxtOut + Linxxx + "\r\n";
            }

            sqlConnection1.Close();

            //Download the Lynx.Evt file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Lynx.Evt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(TxtOut);
            Response.Flush();
            Response.End();


            Response.Redirect("ExportLynxFiles.aspx");
        }
    }
}