using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class RecordsPersonalBestSeasonBests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();

            if (!IsPostBack)
            {
                CompCode.Text = gl.GetCompetitionCode();


                string CompCodeText = Request.QueryString.Get("Code");

                if ((CompCodeText != "") && (CompCodeText != null))
                {
                    CompCodeText = CompCodeText.ToUpper();
                    gl.SetCompetitionCode(CompCodeText);
                    CompCode.Text = CompCodeText;
                }

                AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                Athl_Competition AthlComp = new Athl_Competition();
                AthlComp = AthlCRUD.GetCompetitionRec(CompCode.Text);
                CompetitionName.Text = AthlComp.Name;
                PlaceAndDate.Text = AthlComp.Location + ", " + AthlComp.Date.ToShortDateString();
            }

        }

        protected void RecordsPBsAndSBs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ColNoLineType = 0;
            int ColNoName = 0;
            int ColNoClub = 0;
            int ColNoYearOfBirth = 0;
            int ColNoRemarks = 0;
            int ColNoPerformance = 0;
            int ColNoEventName = 0;
            int ColNoNoOfRecords = 0;
            int ColNoNoOfPbsAndSbs = 0;

            int NoOfRecords = 0;
            int NoOfPbsAndSbs = 0;
            int FirstLine = 1;

            //<asp:BoundField DataField="Name" HeaderText="Nafn" SortExpression="Name" />
            //<asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" />
            //<asp:BoundField DataField="YearOfBirth" HeaderText="F.ár" SortExpression="YearOfBirth" />
            //<asp:BoundField DataField="Remarks" HeaderText="Athugasemd" SortExpression="Remarks" />
            //<asp:BoundField DataField="Performance" HeaderText="Árangur" SortExpression="Performance" />
            //<asp:BoundField DataField="EventName" He


            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            GridViewRow CurrRow;
            CurrRow = e.Row;
            Global gl = new Global();

            ColNoLineType = gl.GetColumnIndexByName(CurrRow, "LineType");
            ColNoName = gl.GetColumnIndexByName(CurrRow, "Name");
            ColNoClub = gl.GetColumnIndexByName(CurrRow, "Club");
            ColNoYearOfBirth = gl.GetColumnIndexByName(CurrRow, "YearOfBirth");
            ColNoRemarks = gl.GetColumnIndexByName(CurrRow, "Remarks");  
            ColNoPerformance = gl.GetColumnIndexByName(CurrRow, "Performance");
            ColNoEventName = gl.GetColumnIndexByName(CurrRow, "EventName");
            ColNoNoOfRecords = gl.GetColumnIndexByName(CurrRow, "NoOfRecords");
            ColNoNoOfPbsAndSbs = gl.GetColumnIndexByName(CurrRow, "NoOfPbsAndSbs");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (FirstLine == 1)
                {
                    NoOfRecords = Convert.ToInt32(e.Row.Cells[ColNoNoOfRecords].Text);
                    NoOfPbsAndSbs = Convert.ToInt32(e.Row.Cells[ColNoNoOfPbsAndSbs].Text);
                    FirstLine = 0;
                    if ((NoOfRecords > 0) || (NoOfPbsAndSbs > 0))
                    {
                        if (NoOfRecords > 0)
                        {
                            InfoLine.Text = "Fjöldi meta: " + NoOfRecords.ToString();
                            if (NoOfPbsAndSbs > 0)
                            {
                                InfoLine.Text = InfoLine.Text + ", Fjöldi Pb/Sb: " + NoOfPbsAndSbs.ToString();
                            }
                        }
                        else
                        {
                            InfoLine.Text = "Fjöldi Pb/Sb: " + NoOfPbsAndSbs.ToString();
                        }
                    }
                }

                if (e.Row.Cells[ColNoLineType].Text == "1")  //Record = 1, Pb or Sb = 2
                {
                    e.Row.Cells[ColNoLineType].Text = "Met";
                    e.Row.Cells[ColNoName].Font.Bold = true;
                    e.Row.Cells[ColNoClub].Font.Bold = true;
                    e.Row.Cells[ColNoYearOfBirth].Font.Bold = true;
                    e.Row.Cells[ColNoRemarks].Font.Bold = true;
                    e.Row.Cells[ColNoRemarks].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[ColNoPerformance].Font.Bold = true;
                    e.Row.Cells[ColNoEventName].Font.Bold = true;

                }
                else
                {
                    e.Row.Cells[ColNoLineType].Text = null;
                }
                e.Row.Cells[ColNoNoOfRecords].Text = null;
                e.Row.Cells[ColNoNoOfPbsAndSbs].Text = null;
            }
            else
            {
                e.Row.Cells[ColNoLineType].Text = null;
                e.Row.Cells[ColNoNoOfRecords].Text = null;
                e.Row.Cells[ColNoNoOfPbsAndSbs].Text = null;
            }
        }
    
    }
}