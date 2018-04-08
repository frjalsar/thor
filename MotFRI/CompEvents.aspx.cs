using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;


namespace MotFRI
{
    public partial class CompEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Global gl = new Global();
            if (IsPostBack == true)
            {
            }
            else
            {
                CompCode4.Text = gl.GetCompetitionCode();
                Int32 NoOfDates = 0;
                ListItem newItem = new ListItem();
                newItem = new ListItem();
                newItem.Text = "Allir dagar";
                newItem.Value = "%";
                EventDateFilterDropDown.Items.Add(newItem);
                //AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                var DatesInCompetition = AthlEnt.ReturnCompetitionEventDates(gl.GetCompetitionCode());
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

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string[] WordsArray;
            string AgeGr;
            Int32 Gend;
            Int32 AgeFr;
            Int32 AgeTo;
            Int32 ColNo;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ColNo = GetColumnIndexByName(e.Row, "Aldursflokkur");
                AgeGr = e.Row.Cells[ColNo].Text;
                WordsArray = AgeGr.Split(';');
                Gend = Convert.ToInt32(WordsArray[0]);
                AgeFr = Convert.ToInt32(WordsArray[1]);
                AgeTo = Convert.ToInt32(WordsArray[2]);

                switch (Gend)
                {
                    case 1:
                        if ((AgeFr == 0) & ((AgeTo == 0) | (AgeTo == 999)))
                        {
                            AgeGr = "Karlar";
                        }
                        else
                        {
                            if (AgeFr == 0)
                            {
                                if (AgeTo > 22)
                                {
                                    AgeGr = "Karlar";
                                }
                                else
                                {
                                    AgeGr = "Piltar " + AgeTo.ToString() + " og yngri";
                                }
                            }
                            else
                            {
                                if (AgeTo == 999)
                                {
                                    AgeGr = "Karlar " + AgeFr.ToString() + " og eldri";
                                }
                                else
                                {
                                    if (AgeFr == AgeTo)
                                    {
                                        AgeGr = "Piltar " + AgeFr.ToString();
                                    }
                                    else
                                    {
                                        AgeGr = "Piltar " + AgeFr.ToString() + "-" + AgeTo.ToString();
                                    }
                                }
                            }

                        }
                        break;
                    case 2:
                        if ((AgeFr == 0) && ((AgeTo == 0) || (AgeTo == 999)))
                        {
                            AgeGr = "Konur";
                        }
                        else
                        {
                            if (AgeFr == 0)
                            {
                                if (AgeTo > 22)
                                {
                                    AgeGr = "Konur";
                                }
                                else
                                {
                                    AgeGr = "Stúlkur " + AgeTo.ToString() + " og yngri";
                                }
                            }
                            else
                            {
                                if (AgeTo == 999)
                                {
                                    AgeGr = "Konur " + AgeFr.ToString() + " og eldri";
                                }
                                else
                                {
                                    if (AgeFr == AgeTo)
                                    {
                                        AgeGr = "Stúlkur " + AgeFr.ToString();
                                    }
                                    else
                                    {
                                        AgeGr = "Stúlkur " + AgeFr.ToString() + "-" + AgeTo.ToString();
                                    }
                                }
                            }
                        }
                        break;
                }
                e.Row.Cells[ColNo].Text = AgeGr;
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

                Response.Redirect("CompetitorsInEvent.aspx?Event=" + EventLineNoText);


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
                Int32 index;

                index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];
                //gl.SetCompetitionEventNo(GridView3.DataKeys[index].Values[1].ToString());
                string EventLineNoText = GridView3.DataKeys[index].Values[1].ToString();

                index = GetColumnIndexByName(CurrentRow, "heitigreinar");
                HeitiG = CurrentRow.Cells[index].Text;
                HeitiG = HttpUtility.HtmlDecode(HeitiG);
                gl.SetCompetitionEventName(HeitiG);
                index = GetColumnIndexByName(CurrentRow, "greinnumer");
                EventCode = CurrentRow.Cells[index].Text;
                gl.SetSelectedEventCode(EventCode);

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
                Response.Redirect("PrintEventResults.aspx?Event=" + EventLineNoText);
            }

        }

        int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }


        protected void Points_Click(object sender, EventArgs e)
        {

        }

        protected void ModifyDateTime_Click(object sender, EventArgs e)
        {
            bool RefreshPage = false;
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Global gl = new Global();

            string CompCode = gl.GetCompetitionCode();
            string NewDateText = "";
            string NewTimeText = "";
            Int32 CurrentRowNo = 0;
            Int32 CurrentEventLineNo = 0;
            Int32 NoOfEvents = GridView3.Rows.Count;
            DateTime NewDate;
            DateTime NewTime;
            DateTime EmptyDateTime = DateTime.ParseExact("17530101 00:00:00", "yyyyMMdd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            for (CurrentRowNo = 0; CurrentRowNo < NoOfEvents; CurrentRowNo++)
            {
                CurrentEventLineNo = (Int32)this.GridView3.DataKeys[CurrentRowNo]["greinnumer"];
                TextBox EventDateTextBox = (TextBox)GridView3.Rows[CurrentRowNo].FindControl("DagsGreinar");
                NewDateText = EventDateTextBox.Text;
                TextBox EventTimeTextBox = (TextBox)GridView3.Rows[CurrentRowNo].FindControl("TimiGreinar");
                NewTimeText = EventTimeTextBox.Text;
                NewDate = gl.TryConvertStringToDate(NewDateText);
                NewTime = gl.TryConvertStringToTime(NewTimeText);
                if ((NewDate > EmptyDateTime) && (NewTime > EmptyDateTime))
                {
                    AthlEvent = AthlCRUD.GetCompetitionEvent(CompCode, CurrentEventLineNo);
                    if ((NewDate.ToString("d") != AthlEvent.dagsetning.ToString("d")) || (NewTime.ToShortTimeString() != AthlEvent.timi.ToShortTimeString()))
                    {
                        AthlEvent.dagsetning = NewDate;
                        AthlEvent.timi = NewTime;
                        AthlEnt.UpdateEventDateTimeAndStatus(CompCode, CurrentEventLineNo, NewDate, NewTime, AthlEvent.stadakeppni,
                             AthlEvent.tilkynnaverdlaunaafhendingu);
                        RefreshPage = true;
                    }
               }
            }
            if (RefreshPage == true)
            {
                Response.Redirect("CompEvents.aspx");
            }
        }

        

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}