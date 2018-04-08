using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class RegisterCompetitorInEvent : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            if (!IsPostBack)
            {
                
                CompetitionCode.Text = gl.GetCompetitionCode();
                CompetitorCode.Text = gl.GetSelCode();
                CompetitorName.Text = gl.GetSelNafn();
                Gender.Text = gl.GetSelKyn();
                if (Gender.Text == "1")
                {
                    CompetitorGender.Text = "Karl";
                }
                else
                {
                    CompetitorGender.Text = "Kona";
                }
                CompetitorYearOfBirth.Text = gl.GetSelFar();
                CompetitorClub.Text = gl.GetSelFel();
                CompetitorAge.Text = gl.GetSelAld();
                BibNo.Text = gl.GetBibNumber().ToString();
                CompetitionName.Text = gl.GetCompetitionName();
              
            }
            else
            {
                Int32 EventAge;
                Int32 CompAge;
                bool res = int.TryParse(gl.GetSelAld(), out CompAge);
                if (res == false)
                {
                    CompAge = 0;
                }
                res = int.TryParse(CompetitorAge.Text, out EventAge);
            	if (res == false)
	            {
	                CompetitorAge.Text = gl.GetSelAld();
	            }
                else
                {
                    if (EventAge < CompAge)
                    {
                        CompetitorAge.Text = gl.GetSelAld();
                    }
                }
            }
        }

        protected bool CompetitorAlreadyRegistered(string CompetitionCode, Int32 EventLineNo, string CompetitorCode)
        {
            return false;
        }
        protected void RegEventsForComp_Click(object sender, EventArgs e)
        {
            Athl_CompetitorsInEvent AthlCompetitorInEvRec = new Athl_CompetitorsInEvent();
            Athl_CompetitionEvents AthlCompEvent = new Athl_CompetitionEvents();
            Athl_CompetitorsInCompetition AthlCompetitorInComp = new Athl_CompetitorsInCompetition();
            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            Int32 i;
            CheckBox SelectedEvent;
            GridViewRow CurrRow;
            Int32 SelCellIndex = -1;
            Global gl = new Global();
            Int32 EventLine;
            bool Res;
            string CurrentUserID =  Session["CurrentUserName"].ToString();

            AthlCompetitorInComp = AthlCompCRUD.InitCompetitorInComp();
            for (i = 0; i < AvailableEvents.Rows.Count; i++)
            {
                SelectedEvent = (CheckBox)AvailableEvents.Rows[i].FindControl("ValinChk");
                if (SelectedEvent.Checked)
                {
                    CurrRow = AvailableEvents.Rows[i];
                    if (SelCellIndex < 0)
                    {
                        SelCellIndex = gl.GetColumnIndexByName(CurrRow, "EventLine");
                    }
                    Res = int.TryParse(CurrRow.Cells[SelCellIndex].Text, out EventLine);
                    if (Res == false)
                    {
                        EventLine = -1;
                    }
                    else
                    {
                        if (AthlCompetitorInComp.rasnumer == 0)
                        {
                            AthlCompetitorInComp = AthlCompCRUD.GetCompetitorInComp(gl.GetCompetitionCode(), gl.GetBibNumber());
                        }

                        AthlCompetitorInEvRec =  AthlCompCRUD.GetCompetitorInEvent(gl.GetCompetitionCode(), gl.GetBibNumber(), EventLine);
                        if (AthlCompetitorInEvRec == null)
                        {
                            AthlCompetitorInEvRec = AthlCompCRUD.InitCompetitorInEvent();
                        }
                        if (AthlCompetitorInEvRec.rasnumer == null)
                        {
                            AthlCompetitorInEvRec.rasnumer = 0;
                        }
                        if (AthlCompetitorInEvRec.rasnumer == 0)
                        {
                            AthlCompEvent = AthlCompCRUD.GetCompetitionEvent(gl.GetCompetitionCode(), EventLine);
                             
                            AthlCompCRUD.RegisterCompetitorInEvent(gl.GetCompetitionCode(), AthlCompEvent, gl.GetBibNumber(), AthlCompetitorInComp,
                                1, 999);
                            AthlEnt.InsertToLogFile(CurrentUserID, "Skráning í grein (b)", gl.GetCompetitionCode(), AthlCompetitorInComp.keppendanumer, AthlCompEvent.grein + ";" +
                                AthlCompEvent.kyn.ToString() + AthlCompEvent.flokkur);
                        }
                        else
                            if (AthlCompetitorInEvRec.rasnumer > 0)
                        {
                            AthlEnt.DeleteCompetitorInEvent(AthlCompetitorInEvRec.mot, AthlCompetitorInEvRec.greinarnumer, AthlCompetitorInEvRec.rasnumer);
                        }
                            
 
                    }
                }
                //else
                //{
                //   if
                //}
            }

          //  Response.Redirect("~/NyttMot.aspx?Comp=" + gl.GetCompetitionCode());

            Response.Redirect("CompetitorsForUser.aspx");
        }

        protected void AvailableEvents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
//            string Cmdn = e.CommandName;
 //           string CmdArg = e.CommandArgument.ToString();


            //GridViewRow CurrRow;
   //         var CurrRow = e.CommandSource;
            Global gl = new Global();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow CurrentRow = AvailableEvents.Rows[index];
           // string CompCode = AvailableEvents.DataKeys[0].Value.ToString();
            int IndexEventNo = gl.GetColumnIndexByName(CurrentRow, "EventLine");
            int EvLineNo = Convert.ToInt32(CurrentRow.Cells[IndexEventNo].Text);
            AthleticsEntities1 AthlEnt1 = new AthleticsEntities1();
            Int32 CurrBibno = Convert.ToInt32(gl.GetBibNumber());
            AthlEnt1.ToggleGuestForCompetitorInEvent(CompetitionCode.Text, CurrBibno, EvLineNo);

            Response.Redirect("RegisterCompetitorInEvent.aspx");
        }

    }
}