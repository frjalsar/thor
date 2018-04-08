using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class ClubCompetirosWithReg2 : System.Web.UI.Page
    {
        static int ColLineType = 0;
        static int ColAgeGroupDescription = 0;
        static int ColBibNo = 0;
        static int ColCompetitorName = 0;
        static int ColCompetitorYearOfBirth = 0;
        static int ColCompetitorAge = 0;
        static int ColCompetitorClub = 0;
        static int ColEvent01 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();

            if (!IsPostBack)
            {
                CompCode.Text = gl.GetCompetitionCode();
                CompetitionName.Text = gl.GetCompetitionName();
                //SelectClub.SelectedValue = "ÍR";
            }

        }

        protected void ClubCompetitorsWithReg2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //string SelectedDay1 = "";
            //string SelectedDay2 = "";
            //string SelectedDay3 = "";
            //if (SelectDay.SelectedValue.ToString() == "%")
            //{
            //    SelectedDay1 = CompDatesArr[1];
            //    SelectedDay2 = CompDatesArr[2];
            //    SelectedDay3 = CompDatesArr[3];
            //}
            //else
            //{
            //    SelectedDay1 = SelectDay.SelectedValue.ToString();
            //    SelectedDay1 = SelectedDay1.Substring(0, SelectedDay1.Length - 1);
            //}


            //AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            if ((ColAgeGroupDescription == 0) && (ColBibNo == 0))
            {
                GridViewRow CurrRow;
                CurrRow = e.Row;
                Global gl = new Global();
                ColLineType = gl.GetColumnIndexByName(CurrRow, "LineType");
                ColAgeGroupDescription = gl.GetColumnIndexByName(CurrRow, "AgeGroupDescription");
                ColBibNo = gl.GetColumnIndexByName(CurrRow, "BibNo");
                ColCompetitorName = gl.GetColumnIndexByName(CurrRow, "CompetitorName");
                ColCompetitorYearOfBirth = gl.GetColumnIndexByName(CurrRow, "CompetitorYearOfBirth");
                ColCompetitorAge = gl.GetColumnIndexByName(CurrRow, "CompetitorAge");
                ColCompetitorClub = gl.GetColumnIndexByName(CurrRow, "CompetitorAge");
                ColEvent01 = gl.GetColumnIndexByName(CurrRow, "Event01");
            }

            if (e.Row.Cells[ColLineType].Text == "1")  //Age Group Description 
            {
                e.Row.Cells[ColBibNo].Text = e.Row.Cells[ColAgeGroupDescription].Text;
                e.Row.Cells[ColCompetitorName].Text = null;
                e.Row.Cells[ColCompetitorYearOfBirth].Text = null;
                e.Row.Cells[ColCompetitorAge].Text = null;
                e.Row.Cells[ColCompetitorClub].Text = null;
                e.Row.Cells[ColBibNo].ColumnSpan = 5;
            }
            e.Row.Cells[ColLineType].Text = null;
            e.Row.Cells[ColAgeGroupDescription].Text = null;

        }
    }
}