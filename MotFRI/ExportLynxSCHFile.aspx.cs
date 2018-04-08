using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MotFRI
{
    public partial class ExportLynxSchFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();

            string CompCode = gl.GetCompetitionCode();

            string TxtOut = string.Empty;

            string cs = ConfigurationManager.ConnectionStrings["AthleticsConnectionString"].ConnectionString;

            SqlConnection sqlConnection1 = new SqlConnection(cs);
                //"Data Source=LAP_FRIDRIK1;Initial Catalog=Athletics;Persist Security Info=True;User ID=FyrirspurnIFrjalsar;Password=Langst0kk.");

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader3;

            cmd.CommandText = "dbo.ReturnLynxSchFile";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompCode", CompCode);
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader3 = cmd.ExecuteReader();

            while (reader3.Read())
            {
               // string Linxxx = reader3[5].ToString();
                string Linxxx = reader3[0].ToString();
                TxtOut = TxtOut + Linxxx + "\r\n";
            }

            sqlConnection1.Close();

            //Download the Lynx.sch file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Lynx.sch");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(TxtOut);
            Response.Flush();
            Response.End();

            Response.Redirect("ExportLynxFiles.aspx");

        }
    }
}