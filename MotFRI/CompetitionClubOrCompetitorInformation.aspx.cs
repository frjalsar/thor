using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class CompetitionClubOrCompetitorInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if  (!IsPostBack)
            {
                Global gl = new Global();
                CompCode.Text = gl.GetCompetitionCode();
                string CompCodeText = Request.QueryString.Get("Code");
                if ((CompCodeText != null) && (CompCodeText.Length > 1))
                {
                    CompCode.Text = CompCodeText;
                    gl.SetCompetitionCode(CompCode.Text);
                }
                Athl_Competition CompetitionRec = new Athl_Competition();
                AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                CompetitionRec = AthlCRUD.GetCompetitionRec(CompCode.Text);
                CompetitionName.Text = CompetitionRec.Name;

                ClubCode.Text = " ";
                string SelClub = Request.QueryString.Get("Club");
                if ((SelClub != null) && (SelClub.Length > 0))
                {
                    ClubCode.Text = SelClub;
                    PageInfoText.Text = "Upplýsingar um félag: " + ClubCode.Text;
                }

                SelectedBibNo.Text = "0";
                string SelBib = Request.QueryString.Get("BibNo");
                if ((SelBib != null) && (SelBib.Length > 0))
                {
                    SelectedBibNo.Text = SelBib;
                    PageInfoText.Text = "Upplýsingar um keppanda með rásnúmer: " + SelectedBibNo.Text;
                }

            }
        }
    }
}