using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Global gl = new Global();
            Int32 CurrentAccessLevel = 0;
            if ((UserID.Text == "") && (OldPassword.Text == ""))
            {
                MessageBox.Text = "Þú verður að fylla út Notandann og gamla lykilorðið.";
            }
            else
            {
                CurrentAccessLevel = AthlCRUD.CheckUserNameAndPw(UserID.Text, OldPassword.Text);
                if (CurrentAccessLevel == -1)
                {
                    MessageBox.Text = "Notandi finnst ekki eða gamla lykilorðið er rangt.";
                }
                else
                {
                    if ((NewPassword1.Text == NewPassword2.Text) && (NewPassword1.Text != ""))
                    {
                        AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                        AthlEnt.ChangePassword(UserID.Text, OldPassword.Text, NewPassword1.Text, UserID.Text);
                        //Response.Write("<script language=javascript>alert('Lykilorðinu þínu hefur verið breytt. Þú verður að skrá þig aftur og nú með nýja lykilorðinu.')</script>");
                        Session["CurrentUserName"] = "";
                        Session["CurrentAccessLevel"] = 0;
                        Session["ComingFromPage"] = "";
                        gl.SetGlobalValue("LoginMsg", "Lykilorðinu þínu hefur verið breytt. Þú verður að skrá þig aftur og nú með nýja lykilorðinu.");
                        Response.Redirect("AthleticsLogon.aspx");
                    }
                }
            }
        }
    }
}