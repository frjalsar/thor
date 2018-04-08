using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace MotFRI
{
    public partial class CompetitorsAchievements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Int32 WrkFirstYear;
                Int32 WrkLastYear;
                Int32 WrkNoOfRecords;

                AthleticsEntities1 AthlEnt1 = new AthleticsEntities1();
                string CompetitorCd = Request.QueryString.Get("CompetitorCode");
                CompetitorsCode.Text = CompetitorCd;
                ObjectParameter pCompetitorName = new ObjectParameter("Name", "");
                ObjectParameter pClub = new ObjectParameter("Club", "");
                ObjectParameter pYearOfBirth = new ObjectParameter("YearOfBirth", "0");
                ObjectParameter pFirstYear = new ObjectParameter("FirstYear", "0");
                ObjectParameter pLastYear = new ObjectParameter("LastYear", "0");
                ObjectParameter pNoOfRecords = new ObjectParameter("NoOfRecords", "0");

                AthlEnt1.CompetitorInfo(CompetitorCd, pCompetitorName, pClub, pYearOfBirth, pFirstYear, pLastYear, pNoOfRecords);
                CompetitorsName.Text = pCompetitorName.Value.ToString();
                CompetitorsClub.Text = pClub.Value.ToString();
                YearOfBirth.Text = pYearOfBirth.Value.ToString();
                WrkFirstYear = Convert.ToInt32(pFirstYear.Value.ToString());
                WrkLastYear = Convert.ToInt32(pLastYear.Value.ToString());
                WrkNoOfRecords = Convert.ToInt32(pNoOfRecords.Value.ToString());
                if (WrkNoOfRecords == 0)
                {
                    RecordsHeader.Visible = false;
                }
                else
                {
                    RecordsHeader.Visible = true;
                }

                int Indx = 0;
                for (int ix = WrkFirstYear; ix <= WrkLastYear; ix++)
                {
                    YearFrom.Items.Insert(Indx, ix.ToString());
                    YearTo.Items.Insert(Indx, ix.ToString());
                    Indx = Indx + 1;
                }
                YearFrom.SelectedIndex = 0;
                YearTo.SelectedIndex = Indx - 1;

                YearFromControl.Text = YearFrom.SelectedValue;
                YearToControl.Text = YearTo.SelectedValue;
                OutdoorsIndoors.SelectedIndex = 2;
                OutdoorsIndoorsControl.Text = "%";
            }
            else
            {
                YearFromControl.Text = YearFrom.SelectedValue;
                YearToControl.Text = YearTo.SelectedValue;
                OutdoorsIndoorsControl.Text = OutdoorsIndoors.SelectedValue;
            }
        }
    }
}