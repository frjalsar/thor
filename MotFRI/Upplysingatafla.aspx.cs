using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace MotFRI
{
    public partial class Upplysingatafla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global Gl = new Global();

            if (IsPostBack)
            {
                Gl.SetGlobalValue("ScorebEventStat", EventStat.SelectedValue.ToString());
                Gl.SetGlobalValue("ScorebGenderFilter", GenderFilter.SelectedValue.ToString());
                Gl.SetGlobalValue("ScorebSelectedDate", SelectDay.SelectedValue.ToString());
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

                string SelectedEventStatus = Gl.GetGlobalValue("ScorebEventStat");
                string SelectedGenderFilter = Gl.GetGlobalValue("ScorebGenderFilter");
                string SelectedEventDate = Gl.GetGlobalValue("ScorebSelectedDate");
                if ((SelectedEventStatus == "") && (SelectedGenderFilter == "") && (SelectedEventDate == ""))
                {
                    Gl.SetGlobalValue("ScorebEventStat", EventStat.SelectedValue.ToString());
                    Gl.SetGlobalValue("ScorebGenderFilter", GenderFilter.SelectedValue.ToString());
                    Gl.SetGlobalValue("ScorebSelectedDate", SelectDay.SelectedValue.ToString());

                }
                else
                {
                    EventStat.SelectedValue = SelectedEventStatus;
                    GenderFilter.SelectedValue = SelectedGenderFilter;
                    SelectDay.SelectedValue = SelectedEventDate;
                }

            }

        }

        protected void ScoreboardGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Global gl = new Global();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow CurrentRow;
                CurrentRow = e.Row;
                int ColNoOfSelForLEDStudio = gl.GetColumnIndexByName(CurrentRow, "Val_fyrir_LedStudio");
                int ValueOfValFyrirLEDStudio = Convert.ToInt32(CurrentRow.Cells[ColNoOfSelForLEDStudio].Text);

                if (ValueOfValFyrirLEDStudio == 0)
                {
                    CurrentRow.Cells[ColNoOfSelForLEDStudio].Text = "";
                }
                else
                    if (ValueOfValFyrirLEDStudio == 1)
                    {
                        CurrentRow.Cells[ColNoOfSelForLEDStudio].Text = "Keppendur";
                    }
                    else
                        if (ValueOfValFyrirLEDStudio == 2)
                        {
                            CurrentRow.Cells[ColNoOfSelForLEDStudio].Text = "Úrslit";
                        }
                        else
                            if (ValueOfValFyrirLEDStudio == 3)
                            {
                                CurrentRow.Cells[ColNoOfSelForLEDStudio].Text = "Staðan nú";

                            }
                            else
                                if ((ValueOfValFyrirLEDStudio > 9) && (ValueOfValFyrirLEDStudio < 30))
                                {
                                    CurrentRow.Cells[ColNoOfSelForLEDStudio].Text = "Keppendur " + (ValueOfValFyrirLEDStudio - 10).ToString();
                                }
                                else
                                    if ((ValueOfValFyrirLEDStudio > 30) && (ValueOfValFyrirLEDStudio < 50))
                                    {
                                        CurrentRow.Cells[ColNoOfSelForLEDStudio].Text = "Úrslit " + (ValueOfValFyrirLEDStudio - 30).ToString();
                                    }

            }
        }
    }
}