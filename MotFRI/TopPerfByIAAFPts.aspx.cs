using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class TopPerfByIAAFPts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
            Global gl = new Global();
            CompCode.Text = gl.GetCompetitionCode();
            string CompCodeText = Request.QueryString.Get("Code");
            if ((CompCodeText != "") && (CompCodeText != null))
            {
                CompCodeText = CompCodeText.ToUpper();  
                Athl_Competition AthlComp = new Athl_Competition();
                AthlComp = AthlCompCRUD.GetCompetitionRec(CompCodeText);
                CompCode.Text = CompCodeText;                
                gl.SetCompetitionCode(CompCodeText);
                gl.SetCompetitionName(AthlComp.Name);
                TopIAAFPoints.DataBind();
            }

            CompName.Text = gl.GetCompetitionName();

        }
    }
}