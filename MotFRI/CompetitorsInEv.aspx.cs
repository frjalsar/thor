using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class CompetorsInEv : System.Web.UI.Page
    {
        static Int32 CompetitionLanguage = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            if (IsPostBack == false)
            {
                string CompCodeText = Request.QueryString.Get("Code");
                string EventLineNoText = Request.QueryString.Get("LineNo");
                if ((CompCodeText != "") && (EventLineNoText != ""))
                {
                    AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();

                    CompCode.Text = CompCodeText;
                    EventLineNo.Text = EventLineNoText;
                    Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();
                    Athl_Competition AthlComp = new Athl_Competition();
                    AthlComp = AthlCRUD.GetCompetitionRec(CompCode.Text);
                    CompetitionLanguage = AthlComp.tungumal;
                    CompetitionName.Text = AthlComp.Name;

                    Int32 EventLin = Convert.ToInt32(EventLineNo.Text);
                    AthlEvent = AthlCRUD.GetCompetitionEvent(CompCode.Text, EventLin);
                    if (AthlComp.tungumal == 0)
                    {
                        EventName.Text = AthlEvent.heitigreinar;
                    }
                    else
                    {
                        EventName.Text = AthlEvent.heitigreinar + " - " + AthlEvent.ensktheitigreinar;
                    }
                    if (AthlComp.tungumal == 0) 
                    {
                        if (AthlEvent.sigurvegariifyrra != "")
                        {
                            AdditionalInfo1.Text = "Sigurv. í fyrra: " + AthlEvent.sigurvegariifyrra + " " + AthlEvent.arangurifyrra;
                        }
                        if (AthlEvent.motsmetshafi != "")
                        {
                            MeetRec1.Text = "Mótsmet: " + AthlEvent.motsmet + " " + AthlEvent.motsmetshafi + ", " + AthlEvent.felagmotsmetshafa; // +" " + AthlEvent.dagsetningmotsmets.ToString("yyyy");
                        }
                        if (AthlEvent.motsmetshafi2 != "")
                        {
                            MeetRec2.Text = "Mótsmet: " + AthlEvent.motsmet2 + " " + AthlEvent.motsmetshafi2 + ", " + AthlEvent.felagmotsmetshafa2;// +" " + AthlEvent.dagsetningmotsmets2.ToString("yyyy");
                        }
                        if (AthlEvent.motsmetshafi3 != "")
                        {
                            MeetRec3.Text = "Mótsmet: " + AthlEvent.motsmet3 + " " + AthlEvent.motsmetshafi3 + ", " + AthlEvent.felagmotsmetshafa3; // +" " + AthlEvent.dagsetningmotsmets3.ToString("yyyy");
                        }
                    }
                    if (AthlComp.tungumal == 1) 
                    {
                        if (AthlEvent.sigurvegariifyrra != "")
                        {
                            AdditionalInfo1.Text = "Winner last year " + AthlEvent.sigurvegariifyrra + " " + AthlEvent.arangurifyrra;
                        }
                        if (AthlEvent.motsmetshafi != "")
                        {
                            MeetRec1.Text = "Games Record: " + AthlEvent.motsmet + " " + AthlEvent.motsmetshafi + ", " + AthlEvent.felagmotsmetshafa; // +" " + AthlEvent.dagsetningmotsmets.ToString("yyyy");
                        }
                        if (AthlEvent.motsmetshafi2 != "")
                        {
                            MeetRec2.Text = "Games Record: " + AthlEvent.motsmet2 + " " + AthlEvent.motsmetshafi2 + ", " + AthlEvent.felagmotsmetshafa2; // +" " + AthlEvent.dagsetningmotsmets2.ToString("yyyy");
                        }
                        if (AthlEvent.motsmetshafi3 != "")
                        {
                            MeetRec3.Text = "Games Record: " + AthlEvent.motsmet3 + " " + AthlEvent.motsmetshafi3 + ", " + AthlEvent.felagmotsmetshafa3; // +" " + AthlEvent.dagsetningmotsmets3.ToString("yyyy");
                        }
                    }
                    if (AthlComp.tungumal == 2)
                    {
                        if (AthlEvent.sigurvegariifyrra != "")
                        {
                            AdditionalInfo1.Text = "Sigurv í fyrra/Winner last year: " + AthlEvent.sigurvegariifyrra + " " + AthlEvent.arangurifyrra;
                        }
                        if (AthlEvent.motsmetshafi != "")
                        {
                            MeetRec1.Text = "Mótsmet/Games Record: " + AthlEvent.motsmet + " " + AthlEvent.motsmetshafi + ", " + AthlEvent.felagmotsmetshafa; // +" " + AthlEvent.dagsetningmotsmets.ToString("yyyy");
                        }
                        if (AthlEvent.motsmetshafi2 != "")
                        {
                            MeetRec2.Text = "Mótsme/Games Record: " + AthlEvent.motsmet2 + " " + AthlEvent.motsmetshafi2 + ", " + AthlEvent.felagmotsmetshafa2; // +" " + AthlEvent.dagsetningmotsmets2.ToString("yyyy");
                        }
                        if (AthlEvent.motsmetshafi3 != "")
                        {
                            MeetRec3.Text = "Mótsmet/Games Record: " + AthlEvent.motsmet3 + " " + AthlEvent.motsmetshafi3 + ", " + AthlEvent.felagmotsmetshafa3; // +" " + AthlEvent.dagsetningmotsmets3.ToString("yyyy");
                        }
                    }
                    if (AthlEvent.tegundgreinar == 1)  //Track Event
                    {
                        TrackEventGridView.Visible = true;
                        FieldEventGridView.Visible = false;
                    }
                    else
                    {
                        TrackEventGridView.Visible = false;
                        FieldEventGridView.Visible = true;
                    }
                }

            }
        }

        protected void TrackEventGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ColNo = 0;
            Global gl = new Global();


            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (CompetitionLanguage > 1) //English
                {
                    ColNo = gl.GetColumnIndexByName(e.Row, "rasnumer");
                    e.Row.Cells[ColNo].Text = "Bib No.";
                    ColNo = gl.GetColumnIndexByName(e.Row, "ridillnumer");
                    e.Row.Cells[ColNo].Text = "Heat No.";
                    ColNo = gl.GetColumnIndexByName(e.Row, "stokkkastrod");
                    e.Row.Cells[ColNo].Text = "Lane No.";
                    ColNo = gl.GetColumnIndexByName(e.Row, "nafn");
                    e.Row.Cells[ColNo].Text = "Name";
                    ColNo = gl.GetColumnIndexByName(e.Row, "felag");
                    e.Row.Cells[ColNo].Text = "Club/Country";
                    ColNo = gl.GetColumnIndexByName(e.Row, "faedingarar");
                    e.Row.Cells[ColNo].Text = "Year of birth";                   
                }

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int RowIx = e.Row.RowIndex;

                ColNo = gl.GetColumnIndexByName(e.Row, "rasnumer");
                int BibNo = Convert.ToInt32(e.Row.Cells[ColNo].Text);
                if (BibNo == 0)
                {
                    e.Row.Cells[ColNo].Text = "";
                    ColNo = gl.GetColumnIndexByName(e.Row, "ridillnumer");
                    e.Row.Cells[ColNo].Text = "";
                    ColNo = gl.GetColumnIndexByName(e.Row, "stokkkastrod");
                    e.Row.Cells[ColNo].Text = "";
                    ColNo = gl.GetColumnIndexByName(e.Row, "faedingarar");
                    e.Row.Cells[ColNo].Text = "";
                }
            }

        }

        protected void ListOfEvents_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectedCompetitionEvents.aspx?Code=" + CompCode.Text);
        }

        protected void FieldEventGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ColNo = 0;
            Global gl = new Global();


            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (CompetitionLanguage > 1) //English
                {
                    ColNo = gl.GetColumnIndexByName(e.Row, "stokkkastrod");
                    e.Row.Cells[ColNo].Text = "Order";
                    ColNo = gl.GetColumnIndexByName(e.Row, "rasnumer");
                    e.Row.Cells[ColNo].Text = "Bib No.";
                    ColNo = gl.GetColumnIndexByName(e.Row, "nafn");
                    e.Row.Cells[ColNo].Text = "Name";
                    ColNo = gl.GetColumnIndexByName(e.Row, "felag");
                    e.Row.Cells[ColNo].Text = "Club/Country";
                    ColNo = gl.GetColumnIndexByName(e.Row, "faedingarar");
                    e.Row.Cells[ColNo].Text = "Year of birth";
                }

            }

        }
    }
}