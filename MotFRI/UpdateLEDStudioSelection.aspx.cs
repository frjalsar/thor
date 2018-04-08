using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class UpdateLEDStudioSelection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Example: EventLineNo=30000&SelectVal=2  
            string EventLineNo = "";
            string LEDSelection = "";
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            
            EventLineNo = Request.QueryString.Get("EventLineNo");
            LEDSelection = Request.QueryString.Get("SelectVal");
            QryStr.Text = Request.QueryString.ToString() + " EventL(" + EventLineNo + ") Leds(" + LEDSelection + ")";
            AthlEnt.ScoreboardEventsUpdSelection(EventLineNo, LEDSelection);
            Response.Redirect("~\\Upplysingatafla.aspx");
        }
    }
}