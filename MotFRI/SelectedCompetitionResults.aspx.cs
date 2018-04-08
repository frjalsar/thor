using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class SelectedCompetition : System.Web.UI.Page
    {
        protected string[] AgeGroupFrom = new string[50];
        protected string[] AgeGroupTo = new string[50];
        protected int NoOfAgeGroupsInArr = 0;
        protected int LastSelectedAgeGrFrom = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Athl_Competition AthlComp = new Athl_Competition();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();

            if (IsPostBack)
            {
                if (SelAgeFrom.SelectedIndex != LastSelectedAgeGrFrom)
                {
                    //Athuga þetta
                   // LastSelectedAgeGrFrom = SelAgeFrom.SelectedIndex;
                   // SelAgeTo.SelectedIndex = LastSelectedAgeGrFrom;
                }

            }
            else
            {
                Global Gl = new Global();
                CompCode.Text = Gl.GetCompetitionCode();

                string CompCodeText = Request.QueryString.Get("Code");

                if ((CompCodeText != "") && (CompCodeText != null))
                {
                    //CompCodeText = HttpUtility.HtmlDecode(CompCodeText);
                    CompCodeText = CompCodeText.ToUpper();
                    AthlComp = AthlCRUD.GetCompetitionRec(CompCodeText);

                    Int32 NoOfDates = 0;                    
                    SelectDay.Items.Clear();
                    ListItem newItem = new ListItem();

                    newItem = new ListItem();
                    newItem.Text = "Allir dagar";
                    newItem.Value = "%";
                    SelectDay.Items.Add(newItem);
                    //AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                    var DatesInCompetition = AthlEnt.ReturnCompetitionEventDates(Gl.GetCompetitionCode());
                    foreach (var result in DatesInCompetition)
                    {
                        NoOfDates = NoOfDates + 1;
                        newItem = new ListItem();
                        newItem.Text = Convert.ToString(result); // "Dagur 1"; // Convert.ToString(result.Dags);
                        DateTime CompD = Convert.ToDateTime(result.ToString());
                        newItem.Value = CompD.ToString("yyyy.MM.dd");
                            //Convert.ToString(result) + "%"; // "D1%"; // Convert.ToString(result.Dags) + "%";
                        SelectDay.Items.Add(newItem);
                    }
                    if (NoOfDates <= 1)
                    {
                        SelectDay.Visible = false;
                        SelectDayLabel.Visible = false;
                    }

                    Int32 NoOfAgeGroups = 0;
                    NoOfAgeGroupsInArr = 0;                   
                    SelAgeFrom.Items.Clear();
                    SelAgeTo.Items.Clear();
                    var CompetitonAgeFromAndAgeTo = AthlEnt.ReturnCompAgeFromAndAgeTo(CompCodeText);

                    foreach (var AgeFrAndAgeTo in CompetitonAgeFromAndAgeTo)
                    {
                        NoOfAgeGroups = NoOfAgeGroups + 1;
                        NoOfAgeGroupsInArr = NoOfAgeGroupsInArr + 1;
                        newItem = new ListItem();
                        newItem.Text = Convert.ToString(AgeFrAndAgeTo.AgeFromText);
                        newItem.Value = Convert.ToString(AgeFrAndAgeTo.AgeFromValue);
                        AgeGroupFrom[NoOfAgeGroupsInArr] = newItem.Value.ToString();
                        SelAgeFrom.Items.Add(newItem);
                        newItem = new ListItem();
                        newItem.Text = Convert.ToString(AgeFrAndAgeTo.AgeToText);
                        newItem.Value = Convert.ToString(AgeFrAndAgeTo.AgeToValue);
                        AgeGroupTo[NoOfAgeGroupsInArr] = newItem.Value.ToString();
                        SelAgeTo.Items.Add(newItem);
                    }
                    if (NoOfAgeGroups <= 1)
                    {
                        SelAgeFrom.Visible = false;
                        AgeFromLabel.Visible = false;
                        SelAgeTo.Visible = false;
                        AgeToLabel.Visible = false;

                    }
                    LastSelectedAgeGrFrom = 1;
                    if (AthlComp.Code != "")
                    {
                        Gl.SetCompetitionCode(CompCodeText);
                        CompCode.Text = CompCodeText;
                        Gl.SetGlobalValue("CompetitionName", AthlComp.Name);
                        Gl.SetGlobalValue("EnglishCompetitionName", AthlComp.ensktheitiamoti);
                        Gl.SetOutdoorsOrIndoors(AthlComp.OutdoorsOrIndoors.ToString());

                        Gl.SetCompetitionCode(AthlComp.Code);
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
                        if (AthlComp.CompetitonType == 0) //Road Race
                        {
                            SelMaxLinesLabel.Visible = false;
                            MaxNoOfLines.Visible = false;
                            MaxNoOfLines.Text = "99999";
                            SelectDayLabel.Visible = false;
                            SelectDay.Visible = false;
                            SelectDay.Text = "%";
                        }
                        // Session["CurrentAccessLevel"] = "";
                    }
                }

                LinkToPage.Text = "http://thor.fri.is/SelectedCompetitionResults.aspx?Code=" + CompCode.Text;

                Events.NavigateUrl = "~/SelectedCompetitionEvents.aspx?Code=" + CompCode.Text;
                Competitors.NavigateUrl = "~/SelectedCompetitionCompetitors.aspx?Code=" + CompCode.Text;
                MedalTable.NavigateUrl = "~/MedalTable.aspx?Code=" + CompCode.Text;
                PointsStanding.NavigateUrl = "~/Points.aspx?Code=" + CompCode.Text;
                PointsStandingMIYngri.NavigateUrl = "~/PointsStandingMIYngri.aspx?Code=" + CompCode.Text;
                TopPerfByIAAFPts.NavigateUrl = "~/TopPerfByIAAFPts.aspx?Code=" + CompCode.Text;
                RecordsPbsAndSbs.NavigateUrl = "~/RecordsPersonalBestSeasonBests.aspx?Code=" + CompCode.Text;

                if ((CompCode.Text == "") || (CompCode.Text == null))
                {
                    Response.Redirect("Default.aspx");
                }

                AthlComp = AthlCRUD.GetCompetitionRec(CompCode.Text);
                CompetitionName.Text = AthlComp.Name + " - " + AthlComp.Date.ToShortDateString() + " - " + AthlComp.Location;

                //   if (AthlComp.tegundstigakeppni == 5) //Meistaramót Íslands m IAAF stigum
                if ((AthlComp.tegundstigakeppni == 1) || (AthlComp.tegundstigakeppni == 2) || (AthlComp.tegundstigakeppni == 5)) //Aldurfl, Bikar eða Meistaramót Íslands m IAAF stigum

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
                string AccessLevelText = "";
                if (Session["CurrentAccessLevel"] == null)
                {
                    AccessLevelText = "0";
                }
                else
                {
                    AccessLevelText = Session["CurrentAccessLevel"].ToString();
                }
                if (AccessLevelText == "0")
                {
                    YourCompetitors.Visible = false;
                    YourCompetitorsLabel.Visible = false;
                }

                if (AthlComp.Reikna_IAAF_stig == 1)
                {
                    TopPerfByIAAFPts.Visible = true;
                }
                else
                {
                    TopPerfByIAAFPts.Visible = false;
                }
                if ((AthlComp.CompetitonType == 0) || (AthlComp.Date.Year < 2015))
                {
                    RecordsPbsAndSbs.Visible = false;
                }
            }

        }

        protected void CompetitionResultsDataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ColNoPlace = 0;
            int ColNoResult = 0;
            int ColNoWind = 0;
            int ColNoName = 0;
            int ColNoHyperlink = 0;
            int ColNoYoB = 0;
            int ColNoClub = 0;
            int ColNoSeries = 0;
            int ColNoEventName = 0;
            int ColNoEventDate = 0;
            int ColNoLineType = 0;
            int ColNoCompetitorCode = 0;
            int ColNoPoints = 0;
            
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            GridViewRow CurrRow;
            CurrRow = e.Row;
            Global gl = new Global();

            //select Place, Result, Wind, Name, Club, YearOfBirth, Series, EventName, convert(varchar, EventDate, 104) as EventDate, 
            //LineType, EventNo,  SortOrder, NeedsWindMeter, WindText, EntryNo, CompetitorCode
            //from @CompetitionResults order by EventNo, LineType, SortOrder, EntryNo, Place

            ColNoPlace = gl.GetColumnIndexByName(CurrRow, "Place");
            ColNoResult = gl.GetColumnIndexByName(CurrRow, "Result");
            ColNoWind = gl.GetColumnIndexByName(CurrRow, "WindText");
            ColNoPoints = gl.GetColumnIndexByName(CurrRow, "Points");
            ColNoName = gl.GetColumnIndexByName(CurrRow, "Name");
            ColNoHyperlink = ColNoName + 1;
            ColNoYoB = gl.GetColumnIndexByName(CurrRow, "YearOfBirth");
            ColNoClub = gl.GetColumnIndexByName(CurrRow, "Club");
            ColNoSeries = gl.GetColumnIndexByName(CurrRow, "Series");
            ColNoEventName = gl.GetColumnIndexByName(CurrRow, "EventName");
            ColNoEventDate = gl.GetColumnIndexByName(CurrRow, "EventDate");
            ColNoLineType = gl.GetColumnIndexByName(CurrRow, "LineType");
            ColNoCompetitorCode = gl.GetColumnIndexByName(CurrRow, "CompetitorCode");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AthlEnt.InsertToLogFile("Vindur í línu ", e.Row.Cells[ColNoWind].Text);

                if (e.Row.Cells[ColNoLineType].Text == "1")  //Event Name and Date
                {
                    e.Row.Cells[ColNoPlace].Text = e.Row.Cells[ColNoEventName].Text + " - " + e.Row.Cells[ColNoEventDate].Text; //EventName and EventDate
                    e.Row.Cells[ColNoPlace].Font.Bold = true;
                    e.Row.Cells[ColNoPlace].Font.Italic = true;
                    e.Row.Cells[ColNoPlace].Font.Size = 14;
                    e.Row.Cells[ColNoPlace].ColumnSpan = 6;
                    e.Row.Cells[ColNoResult].Text = null;
                    e.Row.Cells[ColNoWind].Text = null;
                    e.Row.Cells[ColNoName].Text = null;
                    e.Row.Cells[ColNoYoB].Text = null;
                    e.Row.Cells[ColNoClub].Text = null;
                    e.Row.Cells[ColNoSeries].Text = null;
                    e.Row.Cells[ColNoLineType].Text = null;
                    e.Row.Cells[ColNoEventName].Text = null;
                    e.Row.Cells[ColNoEventDate].Text = null;
                    e.Row.Cells[ColNoCompetitorCode].Text = null;
                    e.Row.Cells[ColNoPoints].Text = null;
                }
                else
                {
                    //e.Row.Cells[4].Text = e.Row.Cells[ColNoName].Text;
                    e.Row.Cells[ColNoLineType].Text = null;
                    e.Row.Cells[ColNoEventName].Text = null;
                    e.Row.Cells[ColNoEventDate].Text = null;
                    e.Row.Cells[ColNoCompetitorCode].Text = null;
                    e.Row.Cells[ColNoName].Text = null;

                }
            }
            else
            {
                e.Row.Cells[ColNoEventName].Text = null;
                e.Row.Cells[ColNoEventDate].Text = null;
                e.Row.Cells[ColNoLineType].Text = null;

            }

        }

        
    }
}