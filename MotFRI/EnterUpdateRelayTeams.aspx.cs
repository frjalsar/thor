using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class EnterUpdateRelayTeams : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            CompName.Text = gl.GetCompetitionName();
            CompCode.Text = gl.GetCompetitionCode();

            EventLineNo.Text = Request.QueryString.Get("Event");

            if (!IsPostBack)
            {
                Athl_CompetitionEvents AthlEv = new Athl_CompetitionEvents();
                AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                AthlEv = AthlCRUD.GetCompetitionEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text));

                CompName.Text = AthlEv.heitigreinar;

                string CurrentUserName = Session["CurrentUserName"].ToString();
                AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                ListItem newItem = new ListItem();
                
                var ClubsForCurrUsr = AthlEnt.ClubsForUser(CurrentUserName);
                foreach (var ClubRec in ClubsForCurrUsr)
                {
                    newItem = new ListItem();
                    newItem.Text = Convert.ToString(ClubRec.Club) + " - " + Convert.ToString(ClubRec.NameOfClub);
                    newItem.Value = Convert.ToString(ClubRec.Club);
                    SelectClubDropDownL.Items.Add(newItem);
                }

                newItem = new ListItem();
                newItem.Text = "*Öll félög - Fyrir blandaðar sveitir";
                newItem.Value = "%";
                SelectClubDropDownL.Items.Add(newItem);

                string TeamBibNo = Request.QueryString.Get("TeamBibNo");

                Leg1.Text = "";
                Bib1.Text = "";
                Name1.Text = "";
                Yob1.Text = "";
                Age1.Text = "";
                Leg2.Text = "";
                Bib2.Text = "";
                Name2.Text = "";
                Yob2.Text = "";
                Age2.Text = "";
                Leg3.Text = "";
                Bib3.Text = "";
                Name3.Text = "";
                Yob3.Text = "";
                Age3.Text = "";
                Leg4.Text = "";
                Bib4.Text = "";
                Name4.Text = "";
                Yob4.Text = "";
                Age4.Text = "";


                if ((TeamBibNo != "") && (TeamBibNo != null))
                {
                    Int32 TeamBibNoInt = 0;
                    if (Int32.TryParse(TeamBibNo, out TeamBibNoInt))
                    {
                        Int32 LegNo = 0;
                        var TeamComp = AthlEnt.GetRelayTeamMembers(CompCode.Text, Convert.ToInt32(EventLineNo.Text), TeamBibNoInt);
                        foreach (var TeamCompetitor in TeamComp)
                        {
                            LegNo = LegNo + 1;
                            switch (LegNo)
                            {
                                case 1:
                                    TeamName.Text = TeamCompetitor.NameOfTeam;
                                    Leg1.Text = TeamCompetitor.Sprettur.ToString();
                                    Bib1.Text = TeamCompetitor.Rásnúmer_keppanda.ToString();
                                    Name1.Text = TeamCompetitor.Nafn_keppanda;
                                    Yob1.Text = TeamCompetitor.YearOfBirth.ToString();
                                    Age1.Text = TeamCompetitor.AgeOfComp.ToString();
                                    break;
                                case 2:
                                    Leg2.Text = TeamCompetitor.Sprettur.ToString();
                                    Bib2.Text = TeamCompetitor.Rásnúmer_keppanda.ToString();
                                    Name2.Text = TeamCompetitor.Nafn_keppanda;
                                    Yob2.Text = TeamCompetitor.YearOfBirth.ToString();
                                    Age2.Text = TeamCompetitor.AgeOfComp.ToString();
                                    break;
                                case 3:
                                    Leg3.Text = TeamCompetitor.Sprettur.ToString();
                                    Bib3.Text = TeamCompetitor.Rásnúmer_keppanda.ToString();
                                    Name3.Text = TeamCompetitor.Nafn_keppanda;
                                    Yob3.Text = TeamCompetitor.YearOfBirth.ToString();
                                    Age3.Text = TeamCompetitor.AgeOfComp.ToString();
                                    break;
                                case 4:
                                    Leg4.Text = TeamCompetitor.Sprettur.ToString();
                                    Bib4.Text = TeamCompetitor.Rásnúmer_keppanda.ToString();
                                    Name4.Text = TeamCompetitor.Nafn_keppanda;
                                    Yob4.Text = TeamCompetitor.YearOfBirth.ToString();
                                    Age4.Text = TeamCompetitor.AgeOfComp.ToString();
                                    break;

                            }

                        }


                    }

                }

            }
            else
            {

            }

        }
    }
}