using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class AthleticsLogon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // AccessLevelText = "0" : Not logged in - Aðeins fyrirspurnir
            // AccessLevelText = "1" : Club Representative 
            // AccessLevelText = "2" : Administrator
            // AccessLevelText = "3" : Can edit entry sheets
            // AccessLevelText = "4" : Competition Administrator

            Global gl = new Global();

            if (!IsPostBack)
            {
                string ComingFromPage = "";
                string ComingFromPage2 = "";
                ComingFromPage = Request.UrlReferrer.ToString();
                if (ComingFromPage == "")
                {
                    ComingFromPage = "";
                }
                if (ComingFromPage.Length > 0)
                {
                    for (Int32 ii = 0; ii < ComingFromPage.Length; ii++)
                    {
                        if (ComingFromPage.Substring(ii, 1) == "/")
                        {
                            ComingFromPage2 = ComingFromPage.Substring(ii + 1);
                        }
                    }
                }
                Session["ComingFromPage"] = ComingFromPage2;
                Session["CurrentAccessLevel"] = "0";
                Session["AccessLevelText"] = "Aðeins fyrirspurnir";
                if (Session["CurrentUserName"] == null)
                {
                    UserName.Text = "";
                }
                else
                {
                    UserName.Text = Session["CurrentUserName"].ToString();
                }
                if (UserName.Text != "")
                {
                    UserName.Text = Session["CurrentUserName"].ToString();
                    AccessLevel.Text = gl.GetGlobalValue("AccessLevelText");
                    LogInButton.Visible = false;
                    LogOutButton.Visible = true;
                }
                else
                {
                    if (Session["CurrentUserName"] != null)
                    {
                        UserName.Text = Session["CurrentUserName"].ToString();
                        LogInButton.Visible = true;
                        LogOutButton.Visible = false;
                    }
                }
                
                Message.Text = gl.GetGlobalValue("LoginMsg");
                gl.SetGlobalValue("LoginMsg", "");                      
                UserName.Focus();
 
            } //IsPostback
            else
            {
                bool PasswordWasChanged = false;
                bool UserNameWasChanged = false;
                if (Password.Text != "")
                {
                    if (SavedPassword.Text != Password.Text)
                    {
                        PasswordWasChanged = true;
                    }
                    SavedPassword.Text = Password.Text;
                }
                Password.Text = "";

                if (UserName.Text != "")
                {
                    if (Session["CurrentUserName"].ToString() != UserName.Text)
                    {
                        UserNameWasChanged = true;
                    }
                    Session["CurrentUserName"] = UserName.Text;
                    if ((UserNameWasChanged == true) || (PasswordWasChanged == true))
                    {
                        ValidateLoginInfo(UserName.Text, SavedPassword.Text);
                    }
                }

                if (Session["CurrentUserName"].ToString() != "")
                {
                    UserName.Text = Session["CurrentUserName"].ToString();
                    AccessLevel.Text = gl.GetGlobalValue("AccessLevelText");
                    LogInButton.Visible = false;
                    LogOutButton.Visible = true;
                }
                else
                {
                    UserName.Text = Session["CurrentUserName"].ToString();
                    LogInButton.Visible = true;
                    LogOutButton.Visible = false;
                }
            }
        }

        protected void ValidateLoginInfo(string UserN, string PassW)
        {
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Global gl = new Global();
            Int32 CurrentAccessLevel = 0;
            if ((UserN == "") && (PassW == ""))
            {
                Message.Text = "Þú verður að fylla út bæði Notanda og lykilorð.";
                AccessToClubs.Visible = false;
            }
            else
            {
                CurrentAccessLevel = AthlCRUD.CheckUserNameAndPw(UserN, PassW);
                if (CurrentAccessLevel == -1)
                {
                    Message.Text = "Notandi finnst ekki eða lykilorðið er rangt.";
                    AccessToClubs.Visible = false;
                    Session["CurrentUserName"] = "";
                    Session["CurrentAccesslevel"] = "";
                }
                else
                {   //Queries Only,Club Representative,Administrator,Selected Competition Only
                    Session["CurrentUserName"] = UserN;
                    gl.SetGlobalValue("CurrentUserName", UserN);
                    Session["CurrentAccesslevel"] = CurrentAccessLevel.ToString();
                    gl.SetGlobalValue("CurrAcccessLevel", CurrentAccessLevel.ToString());
                    gl.SetCurrUsrName(UserN);
                    gl.SetCurrAccLev(CurrentAccessLevel.ToString());                  
                    AccessLevel.Text = "";
                    switch (CurrentAccessLevel)
                    {
                        case 0:
                            AccessLevel.Text = "Aðeins fyrirspurnir";
                            AccessToClubs.Visible = false;
                            break;
                        case 1:
                            AccessLevel.Text = "Fulltrúi félags";
                            AccessToClubs.Visible = true;
                            break;
                        case 2:
                            AccessLevel.Text = "Administrator";
                            AccessToClubs.Visible = false;
                            break;
                        case 3:
                            AccessLevel.Text = "Ritari í móti";
                            AccessToClubs.Visible = false;
                            break; 
                    }
                    gl.SetGlobalValue("CurrentAccessLevel", CurrentAccessLevel.ToString());
                    gl.SetGlobalValue("AccessLevelText", AccessLevel.Text);

                    string ComingFromPage = "";
                    ComingFromPage = Session["ComingFromPage"].ToString();
                    if (ComingFromPage != "")
                    {
                        if (ComingFromPage == "ChangePassword.aspx")
                        {
                            Response.Redirect("Default.aspx");
                        }
                        else
                        {
                            Response.Redirect(ComingFromPage);
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }

                }

            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            //AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Global gl = new Global();
            //Int32 CurrentAccessLevel = 0;
            //string ComingFromPage = "";

            if ((UserName.Text == "") || (SavedPassword.Text == ""))
            {
               // Message.Text = "Þú verður að fylla út bæði Notanda og lykilorð.";
            }
            else
            {
                ValidateLoginInfo(UserName.Text, SavedPassword.Text);

                //CurrentAccessLevel = AthlCRUD.CheckUserNameAndPw(UserName.Text, SavedPassword.Text);
                //if (CurrentAccessLevel == -1)
                //{
                //    Message.Text = "Notandi finnst ekki eða lykilorðið er rangt.";
                //}
                //else
                //{
                //    Session["CurrentUserName"] = UserName.Text;
                //    Session["CurrentAccesslevel"] = CurrentAccessLevel;
                //    AccessLevel.Text = "";
                //    switch (CurrentAccessLevel)
                //    {
                //        case 0:
                //            AccessLevel.Text = "Aðeins fyrirspurnir";
                //            break;
                //        case 1:
                //            AccessLevel.Text = "Fulltrúi félags";
                //            break;
                //        case 2:
                //            AccessLevel.Text = "Administrator";
                //            break;
                //    }
                //    gl.SetGlobalValue("AccessLevelText", AccessLevel.Text);
                //ComingFromPage = Session["ComingFromPage"].ToString();
                //if (ComingFromPage != "")
                //{
                //    Response.Redirect("Motalisti.aspx");
                //}

            }
        }

        //  } }

        protected void LogOutButton_Click(object sender, EventArgs e)
        {
            Global gl = new Global();
            string ComingFromPage = "";
            Session["CurrentUserName"] = "";
            Session["CurrentAccessLevel"] = 0;
            gl.SetCurrUsrName("");
            gl.SetCurrAccLev("");                  
            ComingFromPage = Session["ComingFromPage"].ToString();
            if ((ComingFromPage != "") && (ComingFromPage != null))
            {
                Response.Redirect(ComingFromPage);
            }
            else
            {
                Response.Redirect("Motalisti.aspx");
            }


        }

        protected void BackButton_Click(object sender, EventArgs e)
        {

            //Global gl = new Global();
            string ComingFromPage = "";
            //Session["CurrentUserName"] = "";
            //Session["CurrentAccessLevel"] = 0;
            ComingFromPage = Session["ComingFromPage"].ToString();
            if (ComingFromPage != "")
            {
                Response.Redirect(ComingFromPage);
            }
            else
            {
                Response.Redirect("Motalisti.aspx");
            }
            
        }

        protected void ApplyForAccess_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplyForUserID.aspx");
        }

        protected void PwChangeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
        }
    }
}