using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class SelectedCompetitionEvents : System.Web.UI.Page
    {
        string AccessLevelText = "";
        Int32 ColNoForPrizeCeremony = 0;
        bool SetPrizeCermonyColumnVisible = false;
        Int32 ColNoForHeitiHTMLSkrar = 0;
        Int32 ColNoForPDFResults = 0;
        Int32 ColNoForStigagrein = 0;
        bool SetPDFResultsVisible = false;
        string WrkFileName = "";


        // AccessLevelText = "0" : Not logged in - Aðeins fyrirspurnir
        // AccessLevelText = "1" : Club Representative 
        // AccessLevelText = "2" : Administrator
        // AccessLevelText = "3" : Can edit entry sheets
        // AccessLevelText = "4" : Competition Administrator

        protected void Page_Load(object sender, EventArgs e)
        {
            Athl_Competition AthlComp = new Athl_Competition();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();

            Global Gl = new Global();
            string CompCodeText = Request.QueryString.Get("Code");
            CompCode.Text = Gl.GetCompetitionCode();
            ExportLynxFiles.Visible = false;

            if ((CompCodeText != "") && (CompCodeText != null))
            {
                //CompCodeText = HttpUtility.HtmlDecode(CompCodeText);  //Má gera þetta? 
                CompCodeText = CompCodeText.ToUpper();

                AthlComp = AthlCompCRUD.GetCompetitionRec(CompCodeText);
                if (AthlComp.tungumal > 0) //Not Icelandic
                {
                    CompEventsLbl.Text = "Keppnisgreinar móts - Competition Events";
                }
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
                    //Session["CurrentAccessLevel"] = "";
                }
            }
            Competitors.NavigateUrl = "~/SelectedCompetitionCompetitors.aspx?Code=" + CompCode.Text;
            CompetitionResults.NavigateUrl = "~/SelectedCompetitionResults.aspx?Code=" + CompCode.Text;
            PointsStanding.NavigateUrl = "~/Points.aspx?Code=" + CompCode.Text;
            PointsStandingMIYngri.NavigateUrl = "~/PointsStandingMIYngri.aspx?Code=" + CompCode.Text;

            AthlComp = AthlCompCRUD.GetCompetitionRec(CompCode.Text);
            string CompDates = AthlComp.Date.ToShortDateString();
            if (AthlComp.Date2 > Convert.ToDateTime("1900-01-01"))
            {
                if (AthlComp.Date3 > Convert.ToDateTime("1900-01-01"))
                {
                    CompDates = CompDates + ", " + AthlComp.Date2.ToShortDateString() + " og " +
                        AthlComp.Date3.ToShortDateString();
                }
                else
                {
                    CompDates = CompDates + " og " + AthlComp.Date2.ToShortDateString();
                }
            }
            CompetitionName.Text = AthlComp.Name + " - " + CompDates + " - " + AthlComp.Location;

            // if (AthlComp.tegundstigakeppni == 5) //Meistaramót Íslands m IAAF stigum
            if ((AthlComp.tegundstigakeppni == 1) || (AthlComp.tegundstigakeppni == 2) || (AthlComp.tegundstigakeppni == 5)) //Mi yngri, bikar eða Meistaramót Íslands m IAAF stigum
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
            LinkToPage.Text = "http://thor.fri.is/SelectedCompetitionEvents.aspx?Code=" + CompCode.Text;
            // LinkToPage.Text = "http://thor.fri.is/SelectedCompetitionEvents.aspx?Code=" + HttpUtility.HtmlEncode(CompCode.Text);
            if (Session["CurrentAccessLevel"] == null)
            {
                Session["CurrentAccessLevel"] = "0";
            }
            AccessLevelText = Session["CurrentAccessLevel"].ToString();
            UpdCompEvents.Visible = false;
            CompSetup.Visible = false;
            if (AccessLevelText == "0")
            {
                YourCompetitors.Visible = false;
                YourCompetitorsLabel.Visible = false;
            }
            else
                if ((AccessLevelText != "1") || (Gl.UserCanUpdateMeet(AthlComp.Code, Session["CurrentUserName"].ToString()) == 1))
            {
                if ((AthlComp.Staða_móts == 2) || (AthlComp.Staða_móts == 3) || (AthlComp.Staða_móts == 4))
                {  //2 = Opið fyrir skráningu, 3 = Lokað fyrir skráningu, 4 = Stendur yfir
                    ExportLynxFiles.Visible = true;
                    UpdCompEvents.Visible = true;
                }

            }
            if (Session["CurrentUserName"] == null)
            {
                Session["CurrentUserName"] = "";
            }

            if (Gl.UserCanUpdateMeet(AthlComp.Code, Session["CurrentUserName"].ToString()) == 1)  //2 = Admin
            {
                UpdCompEvents.Visible = true;
                CompSetup.Visible = true;
                CompSetup.NavigateUrl = "~/CompetitionSetup.aspx?Code=" + AthlComp.Code;
            }

            if (!IsPostBack)
            { 
                EventDateFilterDropDown.Items.Clear();
            Int32 NoOfDates = 0;
            ListItem newItem = new ListItem();
            newItem = new ListItem();
            newItem.Text = "Allir dagar";
            newItem.Value = "%";
            EventDateFilterDropDown.Items.Add(newItem);
            //AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            var DatesInCompetition = AthlEnt.ReturnCompetitionEventDates(Gl.GetCompetitionCode());
            foreach (var result in DatesInCompetition)
            {
                NoOfDates = NoOfDates + 1;
                newItem = new ListItem();
                newItem.Text = Convert.ToString(result); // "Dagur 1"; // Convert.ToString(result.Dags);
                newItem.Value = Convert.ToString(result) + "%"; // "D1%"; // Convert.ToString(result.Dags) + "%";
                EventDateFilterDropDown.Items.Add(newItem);
            }

            if (NoOfDates <= 1)
            {
                EventDateFilterLabel.Visible = false;
                EventDateFilterDropDown.Visible = false;
            }
            else
            {
                EventDateFilterLabel.Visible = true;
                EventDateFilterDropDown.Visible = true;
            }
        }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string HeitiG = "";
            Global gl = new Global();

            if (e.CommandName == "DeleteCompEvent")  //DELETE THE COMPETITION EVENT  - NOT USED ANY MORE
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];
                string CompCode = (string)this.GridView3.DataKeys[index]["mot"];
                Int32 EventLineNo = (Int32)this.GridView3.DataKeys[index]["greinnumer"];

                if ((CompCode != "") & (EventLineNo > 0))
                {
                    AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                    Athl_CompetitionEvents AthlCompetitionEvent = new Athl_CompetitionEvents();
                    AthlCompetitionEvent = AthlCompCRUD.GetCompetitionEvent(CompCode, EventLineNo);
                    AthlCompCRUD.DeleteCompetitonEvent(AthlCompetitionEvent);
                    GridView3.DataBind();
                }
            }

            if (e.CommandName == "ShowCompetitors")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];
                string CompCode = (string)this.GridView3.DataKeys[index]["mot"];
                Int32 EventLineNo = (Int32)this.GridView3.DataKeys[index]["greinnumer"];

                //gl.SetCompetitionEventNo(GridView3.DataKeys[index].Values[1].ToString());
                string EventLineNoText = GridView3.DataKeys[index].Values[1].ToString();

                index = GetColumnIndexByName(CurrentRow, "heitigreinar");
                HeitiG = CurrentRow.Cells[index].Text;
                HeitiG = HttpUtility.HtmlDecode(HeitiG);
                gl.SetCompetitionEventName(HeitiG);

                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if (AccessLevelText == "0")
                {
                    Response.Redirect("ComppetitorsInEv.aspx?Code=" + gl.GetCompetitionCode() + "&LineNo=" + EventLineNoText);
                }
                else
                {
                    Response.Redirect("CompetitorsInEvent.aspx?Event=" + EventLineNoText);
                }
            }

            if (e.CommandName == "EnterEventResults")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];

                string CompCode = (string)this.GridView3.DataKeys[index]["mot"];
                Int32 EventLineNo = (Int32)this.GridView3.DataKeys[index]["greinnumer"];

                //gl.SetCompetitionEventNo(GridView3.DataKeys[index].Values[1].ToString());
                string EventLineNoText = GridView3.DataKeys[index].Values[1].ToString();

                index = GetColumnIndexByName(CurrentRow, "heitigreinar");
                HeitiG = CurrentRow.Cells[index].Text;
                HeitiG = HttpUtility.HtmlDecode(HeitiG);
                gl.SetCompetitionEventName(HeitiG);

                Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNoText);

            }

            if (e.CommandName == "ShowResults")
            {
                string EventCode = "";
                string Gend;
                string EventStatus = "";
                string EventLineNoText = "";
                Int32 index;

                index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];
                EventLineNoText = GridView3.DataKeys[index].Values[1].ToString();
                //gl.SetCompetitionEventNo(EventLineNoText);                

                index = GetColumnIndexByName(CurrentRow, "heitigreinar");
                HeitiG = CurrentRow.Cells[index].Text;
                HeitiG = HttpUtility.HtmlDecode(HeitiG);
                gl.SetCompetitionEventName(HeitiG);
                index = GetColumnIndexByName(CurrentRow, "greinnumer");
                EventCode = CurrentRow.Cells[index].Text;
                //gl.SetSelectedEventCode(EventCode);
                gl.SetSelectedEventCode(EventLineNoText);

                index = GetColumnIndexByName(CurrentRow, "kyn");
                if (CurrentRow.Cells[index].Text.Substring(0, 1) == "B")
                {
                    gl.SetSelectedGender("0");
                }
                else
                {
                    Gend = CurrentRow.Cells[index].Text;
                    if (Gend == "Karl")
                    {
                        gl.SetSelectedGender("1");
                    }
                    else
                    {
                        gl.SetSelectedGender("2");
                    }

                }
                index = GetColumnIndexByName(CurrentRow, "StaðaKeppni");
                EventStatus = CurrentRow.Cells[index].Text;
                Global Gl = new Global();
                Athl_CompetitionEvents AthlCompEv = new Athl_CompetitionEvents();
                AthleticCompetitionCRUD AthlCRU = new AthleticCompetitionCRUD();
                Int32 EvNumInt = Gl.TryConvertStringToInt32(EventLineNoText);
                AthlCompEv = AthlCRU.GetCompetitionEvent(Gl.GetCompetitionCode(), EvNumInt);
                if ((EventStatus == "Stendur yfir") && (AthlCompEv.tegundgreinar == 2) && (AthlCompEv.nanaritegundargreining != 7)) //Field Event and not MultiEv
                {
                    Response.Redirect("EventProgressLiveUpdate.aspx?EventLineNo=" +
                          EventLineNoText + "&EventName=" + HeitiG);
                }
                else
                {
                    Response.Redirect("PrintEventResults.aspx?Event=" + EventLineNoText);
                }
            }

            if (e.CommandName == "PrizeCeremonyFinished")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];

                string CompCode = (string)this.GridView3.DataKeys[index]["mot"];
                Int32 EventLineNo = (Int32)this.GridView3.DataKeys[index]["greinnumer"];
                string EventLineNoText = GridView3.DataKeys[index].Values[1].ToString();
                EventLineNo = Convert.ToInt32(EventLineNoText);

                AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                Athl_CompetitionEvents CompEvent = new Athl_CompetitionEvents();
                if ((CompCode != "") && (EventLineNo > 0))
                {
                    CompEvent = AthlCompCRUD.GetCompetitionEvent(CompCode, EventLineNo);
                    AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                    AthlEnt.UpdateEventDateTimeAndStatus(CompCode, EventLineNo, CompEvent.dagsetning, CompEvent.timi,
                          CompEvent.stadakeppni, 2);  //Set Prize Ceremony has finished for this Event
                    Int32 ColNoForPrizeCeremonyStatus = 0;
                    ColNoForPrizeCeremonyStatus = GetColumnIndexByName(CurrentRow, "tilkynnaverdlaunaafhendingu");
                    CurrentRow.Cells[ColNoForPrizeCeremonyStatus].Text = "Lokið";
                }
            }
        }

        int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                {
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                }
                if (cell.ContainingField is TemplateField)
                {
                    if (((TemplateField)cell.ContainingField).HeaderText.Equals(columnName))
                        break;
                }
                if (cell.ContainingField is HyperLinkField)
                {
                    // System.Web.UI.WebControls.HyperLink h = new HyperLink();

                    // h = cell.ContainingField;
                    string CtrlTxt = cell.Text;
                    CtrlTxt = cell.ContainingField.AccessibleHeaderText;

                    //if (((HyperLinkField)cell.ContainingField).AccessibleHeaderText.Equals(columnName))
                    if (CtrlTxt == columnName)
                    {
                        break;
                    }
                }
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }



        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CompC = "";
            string HeitiG = "";
            Int32 index;

            Global Gl = new Global();
            GridViewRow Row = GridView3.SelectedRow;

            //HeitiG = Row.Cells[0].Text;
            //HeitiG = Row.Cells[1].Text;
            //HeitiG = Row.Cells[2].Text;
            //HeitiG = Row.Cells[3].Text;
            //HeitiG = Row.Cells[4].Text;
            //HeitiG = Row.Cells[5].Text;
            //HeitiG = Row.Cells[6].Text;
            //HeitiG = Row.Cells[7].Text;
            //HeitiG = Row.Cells[8].Text;
            //HeitiG = GridView3.DataKeys[GridView3.SelectedRow.RowIndex].Values[0].ToString();
            HeitiG = GridView3.DataKeys[GridView3.SelectedRow.RowIndex].Values[1].ToString();
            CompC = Gl.GetCompetitionCode();

            index = GetColumnIndexByName(Row, "heitigreinar");

            HeitiG = Row.Cells[index].Text;
            HeitiG = HttpUtility.HtmlDecode(HeitiG);

            Gl.SetCompetitionCode(CompC);
            index = GetColumnIndexByName(Row, "greinnumer");
            //Gl.SetCompetitionEventNo(GridView3.DataKeys[GridView3.SelectedRow.RowIndex].Values[1].ToString());
            string EventLineNoText = GridView3.DataKeys[GridView3.SelectedRow.RowIndex].Values[1].ToString();
            Gl.SetCompetitionEventName(HeitiG);  //Row.Cells[4].Text);
            index = GetColumnIndexByName(Row, "kyn");
            if (Row.Cells[index].Text.Substring(0, 1) == "B")
            {
                Gl.SetSelectedGender("0");
            }
            else
            {
                Gl.SetSelectedGender(Row.Cells[index].Text);
            }
            Gl.SetSelectedEventCode(Row.Cells[5].Text);
            CompCode5.Text = Gl.GetCompetitionCode();
            CompCode5.Text = HttpUtility.HtmlDecode(CompCode5.Text);

            //EvenNo.Text = Gl.GetCompetitionEventNo();
            EvenNo.Text = EventLineNoText;
            SelGender.Text = Gl.GetSelectedGender();
            EventCode.Text = Gl.GetSelectedEventCode();
            EventCode.Text = HttpUtility.HtmlDecode(EventCode.Text);
            Response.Redirect("EventResults.aspx?Event=" + EventLineNoText);

        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string[] WordsArray;
            string AgeGr;
            Int32 Gend;
            Int32 AgeFr;
            Int32 AgeTo;
            Int32 ColNo;
            Int32 TegundStigak = 0;

            string EventLineNoText = "";
            bool UserIsSignedin = false;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "0") || (AccessLevelText == "") || (AccessLevelText == null))
                {
                    UserIsSignedin = false;
                    //ModifyDateTime.Visible = false;
                    SetPrizeCermonyColumnVisible = false;
                }
                else
                {
                    UserIsSignedin = true;
                    AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                    Athl_Competition AthlComp = new Athl_Competition();
                    if (CompCode.Text != "")
                    {
                        AthlComp = AthlCompCRUD.GetCompetitionRec(CompCode.Text);
                        if (AthlComp.DisplayColumnForPrizeCeremony == 1)
                        {
                            SetPrizeCermonyColumnVisible = true;
                        }
                        TegundStigak = AthlComp.tegundstigakeppni;
                    }
                }
                ColNoForHeitiHTMLSkrar = GetColumnIndexByName(e.Row, "heitihtmlskrar");
                ColNoForPrizeCeremony = GetColumnIndexByName(e.Row, "tilkynnaverdlaunaafhendingu");
                ColNoForPDFResults = ColNoForPrizeCeremony + 3;
                ColNoForStigagrein = GetColumnIndexByName(e.Row, "stigagrein");

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int RowIx = e.Row.RowIndex;
                EventLineNoText = GridView3.DataKeys[e.Row.RowIndex].Values[1].ToString();

                ColNo = GetColumnIndexByName(e.Row, "ShowCompetitors");
                HyperLink hp = new HyperLink();

                hp.Text = "Keppendur"; //e.Row.Cells[ColNo].Text;

                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "0") || (AccessLevelText == "") || (AccessLevelText == null))
                {
                    hp.NavigateUrl = "CompetitorsInEv.aspx?Code=" + CompCode.Text + "&LineNo=" + EventLineNoText;
                }
                else
                {
                    hp.NavigateUrl = "UpdateEventResults.aspx?Event=" + EventLineNoText;
                }

                e.Row.Cells[ColNo].Controls.Add(hp);

            }

            if (TegundStigak == 0)
            {
                e.Row.Cells[ColNoForStigagrein].Visible = false;
            }
            if (SetPrizeCermonyColumnVisible == false)
            {
                e.Row.Cells[ColNoForPrizeCeremony].Visible = false;
                e.Row.Cells[ColNoForPrizeCeremony + 1].Visible = false;
            }
            //WrkFileName =  @"C:/Pdfs/" + CompCode.Text + @"/" + e.Row.Cells[ColNoForHeitiHTMLSkrar].Text;

            //WrkFileName = @"~/" + e.Row.Cells[ColNoForHeitiHTMLSkrar].Text;

            String path = Server.MapPath(@"~/" + e.Row.Cells[ColNoForHeitiHTMLSkrar].Text); //MyResources/PassedFileName.pdf");
            //if (File.Exists(path))
            e.Row.Cells[ColNoForHeitiHTMLSkrar].Visible = false;


            //if (File.Exists(WrkFileName))


            ////if (File.Exists(path))
            ////{
            ////    e.Row.Cells[ColNoForPDFResults].Visible = true;

            ////    HyperLink hp = new HyperLink();
            ////    hp.Text = ".";
            ////    hp.NavigateUrl = path; // WrkFileName;
            ////    e.Row.Cells[ColNoForPDFResults].Controls.Add(hp);
            ////}
            ////else
            ////{
            e.Row.Cells[ColNoForPDFResults].Visible = false;
            ////}


        }

    }
}