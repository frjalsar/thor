using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class ExportLynxFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            CompetitionName.Text = gl.GetCompetitionName();

        }

        protected void ExportLynxPpl_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExportLynxPPLFile.aspx");

        }

        protected void ExportLynxSch_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExportLynxSCHFile.aspx");
        }

        protected void ExportLynxEvt_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExportLynxEvtFile.aspx");
        }
    }
}