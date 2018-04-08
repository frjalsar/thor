using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class UpdateTVSelection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Example: EventLineNo=30000&SelectVal=2  UpdateTVSelection.aspx?EventLineNo={0}&SelectVal=21
            string EventLineNo = "";
            string TVSelection = "";
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            EventLineNo = Request.QueryString.Get("EventLineNo");
            TVSelection = Request.QueryString.Get("SelectVal");
            //QryStr.Text = Request.QueryString.ToString() + " EventL(" + EventLineNo + ") Leds(" + LEDSelection + ")";
            AthlEnt.TVEventsUpdSelection(EventLineNo, TVSelection);
            Response.Redirect("~\\FyrirSjonvarp.aspx");
  
        }
    }
}