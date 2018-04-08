using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.SqlClient;

namespace MotFRI
{
    public partial class SelectSeedingMethod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SelBest8.Visible = false;

                Global gl = new Global();
                
                AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                Athl_CompetitionEvents CompEventRec = new Athl_CompetitionEvents();
                AthleticsEntities1 AthlEnt1 = new AthleticsEntities1();
                string EventLineNoText = Request.QueryString.Get("Event");

                CompEventRec = AthlCRUD.GetCompetitionEvent(gl.GetCompetitionCode(), Convert.ToInt32(EventLineNoText));  //gl.GetCompetitionEventNo()));

                if (gl.GetEventType() == "TRACK")
                {
                    SelectSeedingTypeTrack.Visible = true;
                    SelectSeedingTypeField.Visible = false;

                    if (CompEventRec.fjoldiibrauta < 1)
                    {
                        throw new InvalidOperationException(string.Format(
                     "Þú verður að fylla út fjölda brauta í hlaupi '{0}'",
                     CompEventRec.heitigreinar));
                    }
                }
                else
                {
                    SelectSeedingTypeTrack.Visible = false;
                    SelectSeedingTypeField.Visible = true;
                    Int32 IsEventReadyForFinal3Rounds = 0;
                    ObjectParameter readyReturnValue = new ObjectParameter("readyReturnValue", 0);
                    //AthlEnt1.ReadyForSelectFirst8ForFinal(gl.GetCompetitionCode(),
                    //      Convert.ToInt32(gl.GetCompetitionEventNo()), readyReturnValue);
                    AthlEnt1.ReadyForSelectFirst8ForFinal(gl.GetCompetitionCode(),
                          Convert.ToInt32(EventLineNoText), readyReturnValue);

                    IsEventReadyForFinal3Rounds = Convert.ToInt32(readyReturnValue.Value);
                    if (IsEventReadyForFinal3Rounds == 1)
                    {
                        SelBest8.Visible = true;
                    }
                }
                EventName.Text = gl.GetCompetitonEventName();
            }
        }

        protected void SelSeeding_Click(object sender, EventArgs e)
        {
            Global gl = new Global();
            if (gl.GetEventType() == "TRACK")
            {
                gl.SetSeedingMethodSelected(SelectSeedingTypeTrack.SelectedValue);
            }
            else
            {
                gl.SetSeedingMethodSelected(SelectSeedingTypeField.SelectedValue);
            }
            string EventLineNoText = Request.QueryString.Get("Event");

            //gl.SeedCompetitors(gl.GetCompetitionCode(), Convert.ToInt32(gl.GetCompetitionEventNo()), gl.GetSeedingMethodSelected());
            gl.SeedCompetitors(gl.GetCompetitionCode(), Convert.ToInt32(EventLineNoText), gl.GetSeedingMethodSelected());

            Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNoText);
            
        }

        protected void SelBest8_Click(object sender, EventArgs e)
        {
            AthleticsEntities1 AthlEnt1 = new AthleticsEntities1();
            Global gl = new Global();
            string EventLineNoText = Request.QueryString.Get("Event");
            AthlEnt1.SelectFirst8ForFinal(gl.GetCompetitionCode(), Convert.ToInt32(EventLineNoText)); //gl.GetCompetitionEventNo()));
            Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNoText);

        }
    }
}