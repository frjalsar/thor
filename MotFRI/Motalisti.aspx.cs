using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data.Objects;

namespace MotFRI
{
    public partial class Motalisti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global Gl = new Global();
            Gl.SetCompetitionCode("");
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            string CurrentUserID;
            string CurrCompCode;
            Int32 AccessLevelInt;
            Int32 CanEnterScores;
            if (Session["CurrentUserName"] != null)
            {
                CurrentUserID = Session["CurrentUserName"].ToString();
            
            }
            else
            {
                CurrentUserID = "";
            }
            CurrCompCode = "";
            System.Data.Objects.ObjectParameter CurrAccessLevel = new System.Data.Objects.ObjectParameter("CurrentAccessLevel", "0");
            System.Data.Objects.ObjectParameter CurrCanEnterScores = new System.Data.Objects.ObjectParameter("CanEnterScores", "0");

            AthlEnt.GetAccessLevel(CurrentUserID, CurrCompCode, CurrAccessLevel, CurrCanEnterScores);
            AccessLevelInt = Convert.ToInt32(CurrAccessLevel.Value);
            CanEnterScores = Convert.ToInt32(CurrCanEnterScores.Value);

            //--AccessLevel
            //--
            //--0 = Queries Only
            //--1 = Club Representative
            //--2 = Administrator
            //--3 = Selected Competition Only
            //
            //--    CanEnterScores 0 = No, 1 = Yes

            if ((AccessLevelInt == 1) || (AccessLevelInt == 2))
            {
                NewCompetition.Visible = true;
            }
            else
            {
                NewCompetition.Visible = false;
            }



        }

        protected void MotArsins_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global Gl = new Global();

            string CompetitionCode = MotArsins.SelectedValue.ToString();
            Gl.SetCompetitionCode(CompetitionCode);
            //Response.Redirect("NyttMot.aspx?Comp=" + CompetitionCode);
            //Response.Redirect("NyttMot.aspx");
            Response.Redirect("SelectedCompetitionResults.aspx");
                


        }

        protected void NewCompetition_Click(object sender, EventArgs e)
        {


            //Response.Redirect("CompetitionSetup.aspx");
            //ClientScriptManager CSM = Page.ClientScript;
            //if (!ReturnValue())
            //{
            //    string strconfirm = "<script>if(!window.confirm('Are you sure?')){window.location.href='Default.aspx'}</script>";
            //    CSM.RegisterClientScriptBlock(this.GetType(), "Confirm", strconfirm, false);

            //    SvaridEr.Text = strconfirm;
            //}

            Global gl = new Global();
            gl.SetCompetitionCode("");
            Response.Redirect("CompetitionSetup.aspx");

        }

        //private void button3_Click(object sender, System.EventArgs e) 
        //{ 
        //    if (MessageBox.Show("Really delete?","Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes) 
        //    { // a 'DialogResult.Yes' value was returned from the MessageBox // proceed with your deletion } 
        //    }
        
    }
}