using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class CompetitorsPrPlace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_Competition AthlComp = new Athl_Competition();
            
            CompCode.Text = Request.QueryString.Get("CompCode");
            if ((CompCode.Text == "") || (CompCode.Text == null))
            {
                CompCode.Text = gl.GetCompetitionCode();
            }
            SelectedClub.Text = Request.QueryString.Get("Club");
            SelectedPlace.Text = Request.QueryString.Get("Place");
            SelectedGender.Text = Request.QueryString.Get("Gender");
            if ((CompCode.Text != "") && (CompCode.Text != null))
            {
                AthlComp = AthlCRUD.GetCompetitionRec(CompCode.Text);
                CompetitionName.Text = AthlComp.Name;
            }
            SelectionDescription.Text = "";
            if ((SelectedClub.Text != "") && (SelectedClub.Text != null))
            {
                if (SelectedGender.Text == "0")
                {
                    SelectionDescription.Text = "Karlar og konur " + SelectedClub.Text + " í " + SelectedPlace.Text + ". sæti";
                }
                else
                    if (SelectedGender.Text == "1")
                {
                    SelectionDescription.Text = "Karlar " + SelectedClub.Text + " í " + SelectedPlace.Text + ". sæti";
                }
                else
                {
                    SelectionDescription.Text = "Konur " + SelectedClub.Text + " í " + SelectedPlace.Text + ". sæti";
                }
            }
        }
    }
}