using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class AddCompetitorsToEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Athl_CompetitionEvents AthlCompEvent = new Athl_CompetitionEvents();
                AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                Global gl = new Global();
                CompCode.Text = gl.GetCompetitionCode();
                //Int32 EvLineNo = Convert.ToInt32(gl.GetCompetitionEventNo());
                //EventLineNo.Text = gl.GetCompetitionEventNo();
                string EventLineNoText = Request.QueryString.Get("Event");
                Int32 EvLineNo = Convert.ToInt32(EventLineNoText);
                EventLineNo.Text = EventLineNoText;
                AthlCompEvent = AthlCRUD.GetCompetitionEvent(CompCode.Text, EvLineNo);
                EventName.Text = AthlCompEvent.heitigreinar;
                Gender.Text = AthlCompEvent.kyn.ToString();
                if (AthlCompEvent.aldurtil == 0)
                {
                    AgeTo.Text = "999";
                }
                else
                {
                    AgeTo.Text = AthlCompEvent.aldurtil.ToString();
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateEventResults.aspx?Event=" + Request.QueryString.Get("Event"));
        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            Global gl = new Global();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitionEvents AthlCompEvent = new Athl_CompetitionEvents();
            Athl_CompetitorsInCompetition AthlCompInCompetition = new Athl_CompetitorsInCompetition();
            //Int32 EvLineNo = Convert.ToInt32(gl.GetCompetitionEventNo());
            string EventLineNoText = Request.QueryString.Get("Event");
            Int32 EvLineNo = Convert.ToInt32(EventLineNoText);
            GridViewRow CurrRow;
            Int32 Index = 0;
            bool Res = false;
            Int32 BibNo = 0;
            AthlCompEvent = AthlCRUD.GetCompetitionEvent(CompCode.Text, EvLineNo);
            CheckBox SelectedCandidate;
            for (int i = 0; i < Candidates.Rows.Count; i++)
            {
                SelectedCandidate = (CheckBox)Candidates.Rows[i].FindControl("ValinChk");
                if (SelectedCandidate.Checked)
                {
                    CurrRow = Candidates.Rows[i];
                    Index = gl.GetColumnIndexByName(CurrRow, "rasnumer");
                    Res = int.TryParse(CurrRow.Cells[Index].Text, out BibNo);
                    if (Res == false)
                    {
                        BibNo = -1;
                    }
                    if (BibNo > 0)
                    {
                        AthlCompInCompetition = AthlCRUD.GetCompetitorInComp(CompCode.Text, BibNo);
                        AthlCRUD.RegisterCompetitorInEvent(CompCode.Text, AthlCompEvent, BibNo, AthlCompInCompetition, 1, 999);
                    }
                }
            }
            Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNoText);
        }
    }
}