using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class CompetitorsForUser : System.Web.UI.Page
    {
        string AccessLevelText;

        // AccessLevelText = "0" : Not logged in - Aðeins fyrirspurnir
        // AccessLevelText = "1" : Club Representative 
        // AccessLevelText = "2" : Administrator
        // AccessLevelText = "3" : Can edit entry sheets
        // AccessLevelText = "4" : Competition Administrator

        //Staða móts 0 : Ósamþykkt
        //           1 : Samþykkt
        //           2 : Opið fyrir skráningu
        //           3 : Lokað fyrir skráningu
        //           4 : Keppni stendur yfir
        //           5 : Lokið

        Athl_Competition AthlComp = new Athl_Competition();

        AthleticsEntities1 AthlEnt = new AthleticsEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {

            Global Gl = new Global();
            CompCode.Text = Gl.GetCompetitionCode();
            string SelectedClubText = Gl.GetGlobalValue("ShowForClub");
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            AthlComp = AthlCRUD.GetCompetitionRec(CompCode.Text);
            CompetitionName.Text = AthlComp.Name + " - " + AthlComp.Date.ToShortDateString() + " - " + AthlComp.Location;
            AccessLevelText = Session["CurrentAccessLevel"].ToString();
            CurrentUserName.Text = Session["CurrentUserName"].ToString();
            ObjectParameter CurrAccLevelParam;
            CurrAccLevelParam = new ObjectParameter("CurrentAccessLevel", typeof(global::System.String));
            ObjectParameter CanEnterScoresParam;
            CanEnterScoresParam = new ObjectParameter("CanEnterScores", typeof(global::System.String));
           // ObjectParameter CurrAccLevelParam = new ObjectParameter("CurrentAccessLevel", "");
           // ObjectParameter CanEnterScoresParam = new ObjectParameter("CanEnterScores", "");
            AthlEnt.GetAccessLevel(CurrentUserName.Text, CompCode.Text, CurrAccLevelParam, CanEnterScoresParam);
            string CompetitionAccessLevel = CurrAccLevelParam.Value.ToString();
            var CurrAccLevlVar = Session["CurrentAccessLevel"];
            if (CurrAccLevlVar == null)
            {
                string CurrUsrN = Gl.GetCurrUsrName();
                string CurrAccLevl = Gl.GetCurrAccLev();
                if ((CurrUsrN == null) || (CurrAccLevl == null))
                {
                    string msg = "Sessionin þín er útrunnin. Þú verður að ská þig inn aaftur";
                    Response.Write("<script>alert('" + msg + "')</script>");
                }
                Session["CurrentAccessLevel"] = CurrAccLevl;
                Session["CurrentUserName"] = CurrUsrN;
            }
            //StaðaM 3=Lokað f skrán, 5=Lokið
            //CurrAccL 2=Admin,3=Can Edit Sheets, 4=Comp Admin
            string CurrAccLev = Session["CurrentAccessLevel"].ToString();
            Session["CurrentAccessLevel"] = CurrAccLev;

            //string UserAcclevelToCompetition = "";
            //ObjectParameter NewUserAccessTxt;
            //NewUserAccessTxt = new ObjectParameter("NewUserAccessTxt", typeof(global::System.String));

            //AthlEnt.GetUserAccessToCompetition(AthlComp.Code, CurrentUserName.Text, NewUserAccessTxt);
            //UserAcclevelToCompetition = NewUserAccessTxt.ToString();

            if ((AthlComp.Staða_móts == 2) || ((CurrAccLev == "2") && (AthlComp.Staða_móts != 5)) || 
                 ((CurrAccLev == "3" ) && (AthlComp.Staða_móts != 5)) ||
                 ((CurrAccLev == "4") && (AthlComp.Staða_móts != 5)) ||
                 ((CompetitionAccessLevel == "2") && (AthlComp.Staða_móts != 5)))  
            {
                NewRegistration.Visible = true;
                NewRegLabel.Visible = true;
                NewCompetitors.Visible = true;
                CompWithRegLabel.Visible = true;
            }
            else
            {
                NewRegistration.Visible = false;
                NewRegLabel.Visible = false;
                NewCompetitors.Visible = false;
                CompWithRegLabel.Visible = false;                    
            }
            if (!IsPostBack)
            {
                ListItem newItem = new ListItem();
                newItem = new ListItem();
                newItem.Text = "*Öll þín félög";
                newItem.Value = "%" + CurrentUserName.Text;
                SelectClubDropDown.Items.Add(newItem);
            }
            else
            {
                RegisterRelayTeams.NavigateUrl = "~/RegisterRelay.aspx?Club=" + SelectClubDropDown.SelectedValue.ToString();
            }
            if ((SelectedClubText != null) && (SelectedClubText != ""))
            {
                SelectClubDropDown.SelectedValue = SelectedClubText;
                Gl.SetGlobalValue("ShowForClub", "");
            }
  
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //StaðaM 3=Lokað f skrán, 5=Lokið
            //CurrAccL 2=Admin,3=Can Edit Sheets, 4=Comp Admin

            string UserAcclevelToCompetition = "";
            ObjectParameter NewUserAccessTxt;
            NewUserAccessTxt = new ObjectParameter("NewUserAccessTxt", typeof(global::System.String));

            AthlEnt.GetUserAccessToCompetition(CompCode.Text, CurrentUserName.Text, NewUserAccessTxt);
            UserAcclevelToCompetition = NewUserAccessTxt.Value.ToString();

            AccessLevelText = Session["CurrentAccessLevel"].ToString();
            if ((AthlComp.Staða_móts != 2) && ((AccessLevelText == "0") || (AccessLevelText == "1")))
            {
                if ((UserAcclevelToCompetition != "3") && (UserAcclevelToCompetition != "4"))
                {
                    ErrorMessage.Text = "Lokað er fyrir skráningu á þetta mót";
                    return;
                }                                         
            }
            ErrorMessage.Text = "";
            if (e.CommandName == "DeleteCompetitorInComp")
            {
                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "1") || (AccessLevelText == "2"))
                {
                    Global gl = new Global();
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow CurrentRow = GridView1.Rows[index];
                    string CompCode = GridView1.DataKeys[0].Value.ToString();
                    int IndexOfBibno = gl.GetColumnIndexByName(CurrentRow, "rasnumer");
                    int BibNo = Convert.ToInt32(CurrentRow.Cells[IndexOfBibno].Text);

                    if ((CompCode != "") & (BibNo > 0))
                    {
                        ObjectParameter NoOfRegistrations = new ObjectParameter("NoOfRegistrations", typeof(global::System.Int32));
                        AthlEnt.NoOfRegistrationsInEvetns(CompCode, BibNo, NoOfRegistrations);

                        Int32 NoOfRegistrationsInEv = Convert.ToInt32(NoOfRegistrations.Value.ToString());
                        if (NoOfRegistrationsInEv > 0)
                        {
                            ErrorMessage.Text = "Þú getur ekki eytt keppandanum vegna þess að hann er ennþá skráður í grein(ar).";
                        }
                        else
                        {
                            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                            Athl_CompetitorsInCompetition Competitor = new Athl_CompetitorsInCompetition();
                            Competitor = AthlCompCRUD.GetCompetitorInComp(CompCode, BibNo);
                            AthlCompCRUD.DeleteCompetitorInCompetition(Competitor);
                            GridView1.DataBind();
                            string CurrentUser = Session["CurrentUserName"].ToString();
                            AthlEnt.InsertToLogFile(CurrentUser, "Afskráning í mót", CompCode, Competitor.keppendanumer, "");
                        }
                    }
                }
            }

            if (e.CommandName == "RegisterEvents")
            {
                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "1") || (AccessLevelText == "2"))
                {
                    Global gl = new Global();
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow CurrentRow = GridView1.Rows[index];
                    string CompCode = GridView1.DataKeys[0].Value.ToString();
                    int IndexOfBibno = gl.GetColumnIndexByName(CurrentRow, "rasnumer");
                    int BibNo = Convert.ToInt32(CurrentRow.Cells[IndexOfBibno].Text);

                    if ((CompCode != "") & (BibNo > 0))
                    {
                        AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                        Athl_CompetitorsInCompetition Competitor = new Athl_CompetitorsInCompetition();
                        Competitor = AthlCompCRUD.GetCompetitorInComp(CompCode, BibNo);
                        Int32 YearOfBirth;
                        YearOfBirth = Competitor.faedingarar;
                        Int32 CompetitorAge = gl.GetCompetitionYear() - YearOfBirth;

                        gl.SetSelComp(CompCode, Competitor.kennitala, Competitor.nafn, Competitor.kyn.ToString(),
                            Competitor.faedingarar.ToString(), Competitor.felag, CompetitorAge.ToString(), "0");
                        gl.SetBibNumber(BibNo);

                        Response.Redirect("RegisterCompetitorInEvent.aspx");
                    }
                }
            }
            if (e.CommandName == "SetGuestYesNo")
            {
                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "1") || (AccessLevelText == "2"))
                {
                    Global gl = new Global();
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow CurrentRow = GridView1.Rows[index];
                    string CompCode = GridView1.DataKeys[0].Value.ToString();
                    int IndexOfBibno = gl.GetColumnIndexByName(CurrentRow, "rasnumer");
                    int BibNo = Convert.ToInt32(CurrentRow.Cells[IndexOfBibno].Text);

                    if ((CompCode != "") & (BibNo > 0))
                    {
                        AthleticsEntities1 AthlEnt1 = new AthleticsEntities1();
                        AthlEnt1.ToggleGuestForCompetitorInCompetition(CompCode, BibNo);
                        gl.SetGlobalValue("ShowForClub", SelectClubDropDown.SelectedValue.ToString());
                        Response.Redirect("CompetitorsForUser.aspx");
                    }
                }
            }

        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            AccessLevelText = Session["CurrentAccessLevel"].ToString();
            if ((AccessLevelText == "0") || (AccessLevelText == "") || (AccessLevelText == null))
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }

        
    
}