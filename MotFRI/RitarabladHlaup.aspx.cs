using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class RitarabladHlaup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            CompCode.Text = gl.GetCompetitionCode();
            CompetitionName.Text = gl.GetCompetitionName();
            EventName.Text = "Ritarablað fyrir " + gl.GetCompetitonEventName();
            EventLineNo.Text = Request.QueryString.Get("Event");
        }
    }
}