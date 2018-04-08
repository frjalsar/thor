using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class SelectedCompetitionCopetitors : System.Web.UI.Page
    {
        string AccessLevelText = "";
        Global gl = new Global();
            
        protected void Page_Load(object sender, EventArgs e)
        {
            Athl_Competition AthlComp = new Athl_Competition();
            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();

            Global Gl = new Global();

            string CompCodeText = Request.QueryString.Get("Code");
            if ((CompCodeText != "") && (CompCodeText != null))
            {
                CompCodeText = CompCodeText.ToUpper();

                AthlComp = AthlCompCRUD.GetCompetitionRec(CompCodeText);
                if (AthlComp.Code != "")
                {
                    Gl.SetCompetitionCode(AthlComp.Code);
                    CompCode.Text = AthlComp.Code;
                    Gl.SetGlobalValue("CompetitionName", AthlComp.Name);
                    Gl.SetGlobalValue("EnglishCompetitionName", AthlComp.ensktheitiamoti);
                    Gl.SetOutdoorsOrIndoors(AthlComp.OutdoorsOrIndoors.ToString());
                    
                    Gl.SetCompetitionName(AthlComp.Name);
                    Gl.SetCompetionYear(AthlComp.Date.Year);
                    if (AthlComp.keppnisvollur != "")
                    {
                        Gl.SetCompetitionVenue(AthlComp.keppnisvollur + ", " + AthlComp.Location);
                    }
                    else
                    {
                        Gl.SetCompetitionVenue(AthlComp.Location);
                    }
                    //Session["CurrentAccessLevel"] = "";
                }

            }

            CompCode.Text = Gl.GetCompetitionCode();

            Events.NavigateUrl = "~/SelectedCompetitionEvents.aspx?Code=" + CompCode.Text;
            CompetitionResults.NavigateUrl = "~/SelectedCompetitionResults.aspx?Code=" + CompCode.Text;
            PointsStanding.NavigateUrl = "~/Points.aspx?Code=" + CompCode.Text;
            PointsStandingMIYngri.NavigateUrl = "~/PointsStandingMIYngri.aspx?Code=" + CompCode.Text;



            if ((CompCode.Text == "") || (CompCode.Text == null))
            {
                Response.Redirect("Default.aspx");
            }

            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            AthlComp = AthlCRUD.GetCompetitionRec(CompCode.Text);
            Gl.SetCompetitionName(AthlComp.Name);
            CompetitionName.Text = AthlComp.Name + " - " + AthlComp.Date.ToShortDateString() + " - " + AthlComp.Location;
            if ((AthlComp.tegundstigakeppni == 1) || (AthlComp.tegundstigakeppni == 2) || (AthlComp.tegundstigakeppni == 5)) //Yngri, bikar eða Meistaramót Íslands m IAAF stigum
            {
                PointsStanding.Visible = true;
            }
            else
            {
                PointsStanding.Visible = false;
            }
            if (AthlComp.tegundstigakeppni == 3)
            {
                PointsStandingMIYngri.Visible = true;
            }
            else
            {
                PointsStandingMIYngri.Visible = false;
            }
            if (Session["CurrentAccessLevel"] == null)
            {
                Session["CurrentAccessLevel"] = "0";
            }
            AccessLevelText = Session["CurrentAccessLevel"].ToString();

            if (!IsPostBack)
            {
                LinkToPage.Text = "http://thor.fri.is/SelectedCompetitionCompetitors.aspx?Code=" + CompCode.Text;

                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if (AccessLevelText == "0")
                {
                    //AddCompeitors.Visible = false;
                    YourCompetitors.Visible = false;
                    YourCompetitorsLabel.Visible = false;
                    CoachPage.Visible = false;
                    CoachPageLabel.Visible = false;
                }
            }
  
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
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
                        AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                        Athl_CompetitorsInCompetition Competitor = new Athl_CompetitorsInCompetition();
                        Competitor = AthlCompCRUD.GetCompetitorInComp(CompCode, BibNo);
                        AthlCompCRUD.DeleteCompetitorInCompetition(Competitor);
                        GridView1.DataBind();
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

        }

   
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //AccessLevelText = Session["CurrentAccessLevel"].ToString();
            //if ((AccessLevelText == "0") || (AccessLevelText == "") || (AccessLevelText == null))
            //{
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            //}
        }

        
    }
}