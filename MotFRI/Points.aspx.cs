using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{

    public partial class Points : System.Web.UI.Page
    {
        public int ColNoOfTeamsInComp = -1;
        public Int32 NoOfTeams = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
            Athl_Competition AthlComp = new Athl_Competition();

            string CompCodeText = Request.QueryString.Get("Code");
            if (!string.IsNullOrEmpty(CompCodeText))
            {
                CompCode.Text = CompCodeText;
                gl.SetCompetitionCode(CompCodeText);
                AthlComp = AthlCompCRUD.GetCompetitionRec(CompCodeText);
                gl.SetCompetitionName(AthlComp.Name);
                CompetitionName.Text = gl.GetCompetitionName();
                CompetitionName.Text = AthlComp.Name;
            }
            else
            {
                CompCode.Text = gl.GetCompetitionCode();
                CompetitionName.Text = gl.GetCompetitionName();
                AthlComp = AthlCompCRUD.GetCompetitionRec(CompCode.Text);
            }
            if (AthlComp.tegundstigakeppni == 1)
            {
                Response.Redirect("~/PointsStandingMIYngri.aspx?Code=" + CompCode.Text);
            }
            
        }

        protected void TotalPoints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ColRecordType = 0;
            int ColPoints = 0;
            int ColClub = 0;
            int ColClubName = 0;
            int ColLina = 0;
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            GridViewRow CurrRow;
            CurrRow = e.Row;
            Global gl = new Global();

 //           SELECT RecordType, Club, Points, Heitifelags,
 //          Utskyring as Lina FROM @PointsTable ORDER BY RecordType ASC, Points DESC

            ColRecordType = gl.GetColumnIndexByName(CurrRow, "RecordType");
            ColPoints = gl.GetColumnIndexByName(CurrRow, "Points");
            ColClub = gl.GetColumnIndexByName(CurrRow, "Club");
            ColClubName = gl.GetColumnIndexByName(CurrRow, "Heitifelags");
            ColLina = gl.GetColumnIndexByName(CurrRow, "Lina");
            if (ColNoOfTeamsInComp < 0)
            {
                ColNoOfTeamsInComp = gl.GetColumnIndexByName(CurrRow, "NoOfTeamsInComp");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AthlEnt.InsertToLogFile("Vindur í línu ", e.Row.Cells[ColNoWind].Text);

                if ((ColNoOfTeamsInComp > 0) && (NoOfTeams < 0))
                {
                    NoOfTeams = Convert.ToInt32(e.Row.Cells[ColNoOfTeamsInComp].Text);
                }
                if (e.Row.Cells[ColRecordType].Text == "1")  //Description 
                {
                    e.Row.Cells[ColPoints].Text = e.Row.Cells[ColLina].Text; //Description
                    e.Row.Cells[ColPoints].Font.Bold = true;
                    e.Row.Cells[ColPoints].Font.Italic = true;
                    e.Row.Cells[ColPoints].Font.Size = 14;
                    e.Row.Cells[ColPoints].ColumnSpan = 2;
                    e.Row.Cells[ColClub].Text = null;
                    e.Row.Cells[ColClubName].Text = null;
                    e.Row.Cells[ColRecordType].Text = null;
                    e.Row.Cells[ColLina].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 1].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 2].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 3].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 4].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 5].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 6].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 7].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 8].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 9].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp + 10].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp].Text = null;
                }
                else
                {
                    //e.Row.Cells[4].Text = e.Row.Cells[ColNoName].Text;
                    e.Row.Cells[ColRecordType].Text = null;
                    e.Row.Cells[ColLina].Text = null;
                    e.Row.Cells[ColNoOfTeamsInComp].Text = null;

                }
            }
            else
            {
                e.Row.Cells[ColRecordType].Text = null;
                e.Row.Cells[ColLina].Text = null;
                e.Row.Cells[ColNoOfTeamsInComp].Text = null;
            }
            if ((NoOfTeams > 0) && (NoOfTeams < 10))
            {
                for (int ixx = NoOfTeams + 1; ixx <= 10; ixx++)
                {
                    e.Row.Cells[ColNoOfTeamsInComp + ixx].Text = null;
                }
            }
        }        
    }
}