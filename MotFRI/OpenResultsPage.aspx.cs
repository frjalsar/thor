using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class OpenResultsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string CompCode = Request.QueryString.Get("Code");
            string EventLineNo = Request.QueryString.Get("LineNo");
            Int32 EventLin = Convert.ToInt32(EventLineNo);
            Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();

            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            AthlEvent = AthlCRUD.GetCompetitionEvent(CompCode, EventLin);
            
            if (AthlEvent.stadakeppni == 0)
            {
                Response.Redirect("CompetitorsInEv.aspx?Code=" + CompCode + "&LineNo=" + EventLineNo);                    
            }
            else
                if (AthlEvent.stadakeppni == 1)
                {
                    Response.Redirect("EventProgressLiveUpdate.aspx?EventLineNo=" + EventLineNo + "&Code=" + CompCode);
                }
                else
                    if (AthlEvent.stadakeppni == 2)
                    {
                        Response.Redirect("PrintEventResults.aspx?Event=" + EventLineNo + "&Code=" + CompCode);                
                    }
        }
    }
}