using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class MedalTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Global gl = new Global();

            if (!IsPostBack)
            {

                CompCode.Text = gl.GetCompetitionCode();
                CompetitionName.Text = gl.GetCompetitionName();
                Int32 NoOfDates = 0;
                ListItem newItem = new ListItem();
                newItem = new ListItem();
                newItem.Text = "Allir dagar";
                newItem.Value = "%";
                SelectDateDropDownList.Items.Add(newItem);
                //AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                var DatesInCompetition = AthlEnt.ReturnCompetitionEventDates(CompCode.Text);
                foreach (var result in DatesInCompetition)
                {
                    NoOfDates = NoOfDates + 1;
                    newItem = new ListItem();
                    newItem.Text = Convert.ToString(result); // "Dagur 1"; // Convert.ToString(result.Dags);
                    newItem.Value = Convert.ToString(result) + "%"; // "D1%"; // Convert.ToString(result.Dags) + "%";
                    SelectDateDropDownList.Items.Add(newItem);
                }

                if (NoOfDates <= 1)
                {
                    DateFilterLabel.Visible = false;
                    SelectDateDropDownList.Visible = false;
                }
                else
                {
                    DateFilterLabel.Visible = true;
                    SelectDateDropDownList.Visible = true;
                }

            }
        }

       
    }
}