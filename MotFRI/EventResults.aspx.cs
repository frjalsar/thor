using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class EventResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global Gl = new Global();

                EventName.Text = Gl.GetCompetitonEventName();
                CompCode5.Text = Gl.GetCompetitionCode();
                CompCode5.Text = HttpUtility.HtmlDecode(CompCode5.Text);
 
                //EvenNo.Text = Gl.GetCompetitionEventNo();
                EvenNo.Text = Request.QueryString.Get("Event");
                SelGender.Text = Gl.GetSelectedGender();
                EventCode.Text = Gl.GetSelectedEventCode();
                EventCode.Text = HttpUtility.HtmlDecode(EventCode.Text);
                CompetitionName.Text = Gl.GetCompetitionName();

            }

        }
    }
}