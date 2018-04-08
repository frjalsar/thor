using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class PointsStandingMIYngri : System.Web.UI.Page
    {
        static string[] CompDatesArr = new string[10];

        static Athl_Competition AthlCompRec = new Athl_Competition();
        static AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
        static int NoOfAgeGroupsInArr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global gl = new Global();
                AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                CompCode.Text = gl.GetCompetitionCode();
                CompetitionName.Text = gl.GetCompetitionName();
                AthlCompRec = AthlCRUD.GetCompetitionRec(CompCode.Text);

                CompDatesArr[1] = "";
                CompDatesArr[2] = "";
                CompDatesArr[3] = "";

                SelectDay.Items.Clear();
                Int32 NoOfDates = 0;
                ListItem newItem = new ListItem();
                newItem = new ListItem();
                newItem.Text = "Allir dagar";
                newItem.Value = "%";
                SelectDay.Items.Add(newItem);
                var DatesInCompetition = AthlEnt.ReturnCompetitionEventDates(CompCode.Text);
                foreach (var result in DatesInCompetition)
                {
                    NoOfDates = NoOfDates + 1;
                    newItem = new ListItem();
                    newItem.Text = Convert.ToString(result); // "Dagur 1"; // Convert.ToString(result.Dags);
                    newItem.Value = Convert.ToString(result) + "%"; // "D1%"; // Convert.ToString(result.Dags) + "%";
                    SelectDay.Items.Add(newItem);
                    if (NoOfDates < 4)
                    {
                        CompDatesArr[NoOfDates] = Convert.ToString(result);
                    }
                }

                if (NoOfDates <= 1)
                {
                    SelectDayLabel.Visible = false;
                    SelectDay.Visible = false;
                }
                else
                {
                    SelectDayLabel.Visible = true;
                    SelectDay.Visible = true;
                }
                Int32 NoOfAgeGroups = 0;
                NoOfAgeGroupsInArr = 0;

                SelAgeFrom.Items.Clear();
                SelAgeTo.Items.Clear();
                var CompetitonAgeFromAndAgeTo = AthlEnt.ReturnCompAgeFromAndAgeTo(CompCode.Text);

                foreach (var AgeFrAndAgeTo in CompetitonAgeFromAndAgeTo)
                {
                    NoOfAgeGroups = NoOfAgeGroups + 1;
                    NoOfAgeGroupsInArr = NoOfAgeGroupsInArr + 1;
                    newItem = new ListItem();
                    newItem.Text = Convert.ToString(AgeFrAndAgeTo.AgeFromText);
                    newItem.Value = Convert.ToString(AgeFrAndAgeTo.AgeFromValue);
                    //AgeGroupFrom[NoOfAgeGroupsInArr] = newItem.Value.ToString();
                    SelAgeFrom.Items.Add(newItem);
                    newItem = new ListItem();
                    newItem.Text = Convert.ToString(AgeFrAndAgeTo.AgeToText);
                    newItem.Value = Convert.ToString(AgeFrAndAgeTo.AgeToValue);
                    //AgeGroupTo[NoOfAgeGroupsInArr] = newItem.Value.ToString();
                    SelAgeTo.Items.Add(newItem);
                }
                if (NoOfAgeGroups <= 1)
                {
                    SelAgeFrom.Visible = false;
                    AgeFromLabel.Visible = false;
                    SelAgeTo.Visible = false;
                    AgeToLabel.Visible = false;

                }
                //LastSelectedAgeGrFrom = 1;






            }
        }

        protected void PointsStanding_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string SelectedDay1 = "";
            string SelectedDay2 = "";
            string SelectedDay3 = "";
            if (SelectDay.SelectedValue.ToString() == "%")
            {
                SelectedDay1 = CompDatesArr[1];
                SelectedDay2 = CompDatesArr[2];
                SelectedDay3 = CompDatesArr[3];
            }
            else
            {
                SelectedDay1 = SelectDay.SelectedValue.ToString();
                SelectedDay1 = SelectedDay1.Substring(0, SelectedDay1.Length - 1);
            }

            int ColRecordType = 0;
            int ColUtskyring = 0;
            int ColAgeGroupDescr = 0;
            int ColFelag = 0;
            int ColNoPointsDay1 = 0;
            int ColNoPointsDay2 = 0;
            int ColNoPointsDay3 = 0;
            int ColSamtalsStig = 0;
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            GridViewRow CurrRow;
            CurrRow = e.Row;
            Global gl = new Global();
            ColRecordType = gl.GetColumnIndexByName(CurrRow, "RecordType");
            ColUtskyring = gl.GetColumnIndexByName(CurrRow, "Utskyring");
            ColAgeGroupDescr = gl.GetColumnIndexByName(CurrRow, "AgeGroupDescription");
            ColFelag = gl.GetColumnIndexByName(CurrRow, "HeitiFelags");
            ColSamtalsStig = gl.GetColumnIndexByName(CurrRow, "TotalPoints");
            ColNoPointsDay1 = gl.GetColumnIndexByName(CurrRow, "PointsDay1");
            ColNoPointsDay2 = gl.GetColumnIndexByName(CurrRow, "PointsDay2");
            ColNoPointsDay3 = gl.GetColumnIndexByName(CurrRow, "PointsDay3");
            
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (AthlCompRec.tungumal > 0) //English or Icelandic and English
                {
                    e.Row.Cells[ColFelag].Text = "Country";
                    e.Row.Cells[ColNoPointsDay1].Text = "Points " + SelectedDay1;
                    e.Row.Cells[ColNoPointsDay2].Text = "Points " + SelectedDay2;
                    e.Row.Cells[ColNoPointsDay3].Text = "Points " + SelectedDay3;
                    e.Row.Cells[ColSamtalsStig].Text = "Total Points";
                }
                else
                {
                    e.Row.Cells[ColNoPointsDay1].Text = "Stig " + SelectedDay1;
                    e.Row.Cells[ColNoPointsDay2].Text = "Stig " + SelectedDay2;
                    e.Row.Cells[ColNoPointsDay3].Text = "Stig " + SelectedDay3;
                    e.Row.Cells[ColSamtalsStig].Text = "Samtals stig";
                }
                if (SelectedDay2 == "")
                {
                    e.Row.Cells[ColNoPointsDay2].Text = null;
                    e.Row.Cells[ColNoPointsDay3].Text = null;
                    e.Row.Cells[ColSamtalsStig].Text = null;
                }
                else
                {
                    if (SelectedDay3 == "")
                    {
                        e.Row.Cells[ColNoPointsDay3].Text = null;
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AthlEnt.InsertToLogFile("Vindur í línu ", e.Row.Cells[ColNoWind].Text);

                if (e.Row.Cells[ColRecordType].Text == "1")  //Description 
                {
                    e.Row.Cells[ColFelag].Text = e.Row.Cells[ColAgeGroupDescr].Text + " - " + e.Row.Cells[ColUtskyring].Text;
                    e.Row.Cells[ColFelag].Font.Bold = true;
                    e.Row.Cells[ColFelag].Font.Italic = true;
                    e.Row.Cells[ColFelag].Font.Size = 14;
                    e.Row.Cells[ColFelag].ColumnSpan = 4;
                    e.Row.Cells[ColRecordType].Text = null;
                    e.Row.Cells[ColUtskyring].Text = null;
                    e.Row.Cells[ColAgeGroupDescr].Text = null;
                    e.Row.Cells[ColNoPointsDay1].Text = null;
                    e.Row.Cells[ColNoPointsDay2].Text = null;
                    e.Row.Cells[ColNoPointsDay3].Text = null;
                    e.Row.Cells[ColSamtalsStig].Text = null;
                }
                else
                {
                    e.Row.Cells[ColRecordType].Text = null;
                    e.Row.Cells[ColUtskyring].Text = null;
                    e.Row.Cells[ColAgeGroupDescr].Text = null;
                    if (SelectedDay2 == "")
                    {
                        e.Row.Cells[ColSamtalsStig].Text = null;
                        e.Row.Cells[ColNoPointsDay2].Text = null;
                        e.Row.Cells[ColNoPointsDay3].Text = null;
                    }
                    else
                    {
                        if (SelectedDay3 == "")
                        {
                            e.Row.Cells[ColNoPointsDay3].Text = null;
                        }
                    }
                }
            }
            else
            {
                e.Row.Cells[ColRecordType].Text = null;
                e.Row.Cells[ColUtskyring].Text = null;
                e.Row.Cells[ColAgeGroupDescr].Text = null;
            }

        }


    }


}