using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;


namespace MotFRI
{
    public partial class FyrirSjonvarp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                        Global Gl = new Global();

            if (IsPostBack)
            {
                Gl.SetGlobalValue("TVEventStat", EventStat.SelectedValue.ToString());
                Gl.SetGlobalValue("TVGenderFilter", GenderFilter.SelectedValue.ToString());
                Gl.SetGlobalValue("TVSelectedDate", SelectDay.SelectedValue.ToString());
            }
            if (IsPostBack == false)
            {
                AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            
                var CompCodeForScoreboard = new ObjectParameter("compCodeForScorb", typeof(global::System.String));
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            int RetValue = 0;

            RetValue = AthlEnt.ReturnCompetitionForScoreb(CompCodeForScoreboard);
            CompCode.Text = CompCodeForScoreboard.Value.ToString();
            Athl_Competition AthlCompetitionRecord = new Athl_Competition();
            AthlCompetitionRecord = AthlCRUD.GetCompetitionRec(CompCode.Text);
            CompName.Text = AthlCompetitionRecord.Name;        


            Int32 NoOfDates = 0;
            ListItem newItem = new ListItem();
            newItem = new ListItem();
            var DatesInCompetition = AthlEnt.ReturnCompetitionEventDates(CompCode.Text);
            foreach (var result in DatesInCompetition)
            {
                NoOfDates = NoOfDates + 1;
                newItem = new ListItem();
                newItem.Text = Convert.ToString(result); // "Dagur 1"; // Convert.ToString(result.Dags);
                newItem.Value = Convert.ToString(result); // +"%"; // "D1%"; // Convert.ToString(result.Dags) + "%";
                SelectDay.Items.Add(newItem);
            }


            if (NoOfDates <= 1)
            {
                SelectDay.Visible = false;
                SelDayLabel.Visible = false;

            }
            else
            {
                SelectDay.Visible = true;
                SelDayLabel.Visible = true;
            }
                
            string SelectedEventStatus = Gl.GetGlobalValue("TVEventStat");
            string SelectedGenderFilter = Gl.GetGlobalValue("TVGenderFilter");
            string SelectedEventDate = Gl.GetGlobalValue("TVSelectedDate");
                if ((SelectedEventStatus == "") && (SelectedGenderFilter == "") && (SelectedEventDate == ""))
                {
                    Gl.SetGlobalValue("TVEventStat", EventStat.SelectedValue.ToString());
                    Gl.SetGlobalValue("TVGenderFilter", GenderFilter.SelectedValue.ToString());
                    Gl.SetGlobalValue("TVSelectedDate", SelectDay.SelectedValue.ToString());

                }
                else
                {
                    EventStat.SelectedValue = SelectedEventStatus;
                    GenderFilter.SelectedValue = SelectedGenderFilter;
                    SelectDay.SelectedValue = SelectedEventDate;
                }

        }
    }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Global gl = new Global();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow CurrentRow;
                CurrentRow = e.Row;
                int ColNoOfSelTV = gl.GetColumnIndexByName(CurrentRow, "ValFyrirSjonvarp");
                int ValueOfValTV = Convert.ToInt32(CurrentRow.Cells[ColNoOfSelTV].Text);

                if (ValueOfValTV == 0)
                {
                    CurrentRow.Cells[ColNoOfSelTV].Text = "";
                }
                else
                    if (ValueOfValTV == 1)
                    {
                        CurrentRow.Cells[ColNoOfSelTV].Text = "Keppendur";
                    }
                    else
                        if (ValueOfValTV == 2)
                        {
                            CurrentRow.Cells[ColNoOfSelTV].Text = "Úrslit";
                        }
                        else
                            if (ValueOfValTV == 3)
                            {
                                CurrentRow.Cells[ColNoOfSelTV].Text = "Staðan nú";

                            }
                            else
                                if (ValueOfValTV == 11)
                                {
                                    CurrentRow.Cells[ColNoOfSelTV].Text = "Keppendur R1";
                                }
                                else
                                    if (ValueOfValTV == 12)
                                    {
                                        CurrentRow.Cells[ColNoOfSelTV].Text = "Keppendur R2";
                                    }
                                    else
                                        if (ValueOfValTV == 21)
                                        {
                                            CurrentRow.Cells[ColNoOfSelTV].Text = "Úrslit R1";
                                        }
                                        else
                                        {
                                            if (ValueOfValTV == 22)
                                            {
                                                CurrentRow.Cells[ColNoOfSelTV].Text = "Úrslit R2";
                                            }
                                        }

            }
        }

        }
}