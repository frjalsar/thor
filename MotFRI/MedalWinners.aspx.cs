using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class MedalWinners : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            if (!IsPostBack)
            {
                CompCode.Text = gl.GetCompetitionCode();
                if (CompCode.Text == "")
                {
                    CompCode.Text = Request.QueryString.Get("Code");
                }
                CompName.Text = gl.GetCompetitionName();
                ClubCode.Text = Request.QueryString.Get("Club");
                TypeOfMedal.Text = Request.QueryString.Get("Type");
                Int32 NoOfDates = 0;
                ListItem newItem = new ListItem();
                newItem = new ListItem();
                newItem.Text = "Allir dagar";
                newItem.Value = "%";
                SelectDayDropDownList.Items.Add(newItem);
                var DatesInCompetition = AthlEnt.ReturnCompetitionEventDates(CompCode.Text);
                foreach (var result in DatesInCompetition)
                {
                    NoOfDates = NoOfDates + 1;
                    newItem = new ListItem();
                    newItem.Text = Convert.ToString(result); // "Dagur 1"; // Convert.ToString(result.Dags);
                    newItem.Value = Convert.ToString(result) +"%"; // "D1%"; // Convert.ToString(result.Dags) + "%";
                    SelectDayDropDownList.Items.Add(newItem);
                }

                if (NoOfDates <= 1)
                {
                    SelectDayLbl.Visible = false;
                    SelectDayDropDownList.Visible = false;
                }
                else
                {
                    SelectDayLbl.Visible = true;
                    SelectDayDropDownList.Visible = true;
                }
            }
        }
    }
}