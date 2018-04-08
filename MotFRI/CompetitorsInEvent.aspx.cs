using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class CompetitorsInEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            if (!IsPostBack)
            {
                CompetitionName.Text = gl.GetCompetitionName();
                CompetitionEventName.Text = gl.GetCompetitonEventName();
                CompCode.Text = gl.GetCompetitionCode();
                //EventLineNo.Text = gl.GetCompetitionEventNo();
                EventLineNo.Text = Request.QueryString.Get("Event");
            }
        }
    }
}