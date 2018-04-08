using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class ClubCompetitorsWithReg : System.Web.UI.Page
    {
        Global gl = new Global();
        
        decimal TotalAmtPrAgeGroup = 0;
        Int32 NoOfCompetitorsPrAgeGroup = 0;
        decimal TotalAmtPrClub = 0;
        decimal TotalAmtPrClubUnder18 = 0;
        Int32 NoOfCompetitorsPrClub = 0;
        Int32 NoOfCompetitorsPrClubUnder18 = 0;
        decimal AmountPrIndividualEvent = 0;
        decimal AmountPrRelayTeam = 0;
        decimal AmountPrIndividualUnder18 = 0;
        decimal AmountPrRelayTeamUnder18 = 0;
        Int32 NoOfRegistrationsIndividuals = 0;
        Int32 NoOfRegistrationsRelayTeams = 0;
        Int32 NoOfRegistrationsIndividualsUnder18 = 0;
        Int32 NoOfRegistrationsRelayTeamsUnder18 = 0;
        Int32 TotalNoOfRegistrationsIndividuals = 0;
        Int32 TotalNoOfRegistrationsRelayTeams = 0;
        Int32 TotalNoOfRegistrationsIndividualsUnder18 = 0;
        Int32 TotalNoOfRegistrationsRelayTeamsUnder18 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UpdateDatabase();
            }
            else
            {
                AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                Athl_Competition AthlCompetitionRec = new Athl_Competition();
                string CompCode = gl.GetCompetitionCode();
                AthlCompetitionRec = AthlCRUD.GetCompetitionRec(CompCode);
                if ((AthlCompetitionRec.Staða_móts == 2) || (AthlCompetitionRec.Staða_móts == 4))
                {
                    SaveValues.Visible = true;
                    SaveAndReturn.Visible = true;
                }
                else
                {
                    SaveValues.Visible = false;
                    SaveAndReturn.Visible = false;
                }
                    
            }

            LoadCompetitorsPage();

        }

        protected void SaveValues_Click(object sender, EventArgs e)
        {
            UpdateDatabase();
        }

        protected void UpdateDatabase()
        {
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Global gl = new Global();
            Athl_CompetitorsInEvent AthlCompInEvent = new Athl_CompetitorsInEvent();
            Athl_CompetitorsInEvent AthlCompInEventForDelete = new Athl_CompetitorsInEvent();
            Athl_CompetitionEvents CompEvent = new Athl_CompetitionEvents();
            Athl_CompetitorsInCompetition CompetitorsInComp = new Athl_CompetitorsInCompetition();
            CompetitorsInComp.rasnumer = -1;
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            string CompCode = gl.GetCompetitionCode();
            Int32 EventLin = 0;
            Int32 FjLin = gl.GetNoOfArrayElements();
            Int32 BibNumber = 0;
            string EventType = gl.GetEventType();
            string[] CheckBoxParts = new string[10];
            string[] CheckBoxValues = new string[50];
            string[] TextBoxIDs = new string[50];
            bool NeedToRefreshPage = false;
            string CurrentUserID = Session["CurrentUserName"].ToString();

            foreach (Control item in PH1.Controls)
            {
                if (item is CheckBox)
                {

                    CheckBox Cb = (CheckBox)PH1.FindControl(item.ID);
                    string CheckBoxID = Cb.ID;
                    CheckBoxParts = CheckBoxID.Split('_');  //Chkb_BibNo_EventLineNo_TickedOrNot
                    bool CheckboxIsTicked = Convert.ToBoolean(Cb.Checked);
                    bool CheckBoxWasTicked = CheckBoxParts[3] == "1";
                    if ((CheckboxIsTicked == true) && (CheckBoxWasTicked == false))
                    {
                        BibNumber = Convert.ToInt32(CheckBoxParts[1]);
                        EventLin = Convert.ToInt32(CheckBoxParts[2]);
                        AthlCompInEvent = AthlCRUD.InitCompetitorInEvent();
                        CompEvent = AthlCRUD.GetCompetitionEvent(CompCode, EventLin);
                        if (CompetitorsInComp.rasnumer != BibNumber)
                        {
                            CompetitorsInComp = AthlCRUD.GetCompetitorInComp(CompCode, BibNumber);
                        }
                        AthlCRUD.RegisterCompetitorInEvent(CompCode, CompEvent, BibNumber, CompetitorsInComp, 0, 9999);
                        AthlEnt.InsertToLogFile(CurrentUserID, "Skráning í grein", CompCode, CompetitorsInComp.keppendanumer, CompEvent.grein + ";" + CompEvent.kyn.ToString() + ";" +
                            CompEvent.flokkur);
                        NeedToRefreshPage = true;
                    }
                    if ((CheckboxIsTicked == false) && (CheckBoxWasTicked == true))
                    {
                        BibNumber = Convert.ToInt32(CheckBoxParts[1]);
                        EventLin = Convert.ToInt32(CheckBoxParts[2]);
                        if (CompetitorsInComp.rasnumer != BibNumber)
                        {
                            CompetitorsInComp = AthlCRUD.GetCompetitorInComp(CompCode, BibNumber);
                        }
                        CompEvent = AthlCRUD.GetCompetitionEvent(CompCode, EventLin);
                        AthlCompInEventForDelete = AthlCRUD.GetCompetitorInEvent(CompCode, BibNumber, EventLin);
                        AthlCRUD.DeleteCompetitorInEvRec(AthlCompInEventForDelete);
                        AthlEnt.InsertToLogFile(CurrentUserID, "Afskráning í grein", CompCode, CompetitorsInComp.keppendanumer, CompEvent.grein + ";" + CompEvent.kyn.ToString() + ";" +
                                                    CompEvent.flokkur);
                      
                        NeedToRefreshPage = true;
                    }
                }
            }
            if (NeedToRefreshPage == true)
            {
                Response.Redirect("ClubCompetitorsWithReg.aspx");
            }
        }

        protected void BackToEvents_Click(object sender, EventArgs e)
        {
            Global gl = new Global();

            //Response.Redirect("NyttMot.aspx?Comp=" + gl.GetCompetitionCode());
            Response.Redirect("CompetitorsForUser.aspx");
        }

        protected void LoadCompetitorsPage()
        {
            AthleticsEntities1 Athl = new AthleticsEntities1();

            Global gl = new Global();
            string CompCode = "";
            CompCode = gl.GetCompetitionCode();
            Athl_Competition AthlComp = new Athl_Competition();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            AthlComp = AthlCRUD.GetCompetitionRec(CompCode);
            Int32 CurrentAccessLevel = 0;
            string ClubList = "";
            Int32 CurrLblNo = 0;
            Int32 CurrChkBoxNo = 0;
            Int32 CurrLineNo = 0;
            Int32 CurrTabIndex = 0;
            Int32 NoEventsInAgeGr = 0;
            string CurrAgeGroupCode = "";
            Int32[] EventLineArr = new Int32[50];
            AmountPrIndividualEvent = AthlComp.skraningargjaldprgrein;
            AmountPrRelayTeam = AthlComp.Skráningargjld_f__boðhlaup;
            AmountPrIndividualUnder18 = AthlComp.Skráningargj__yngri_en_18_ára;
            AmountPrRelayTeamUnder18 = AthlComp.Skráningargj__f_boðhl_y_18_ára;
            string RegistrationInfo;
            if (AmountPrIndividualUnder18 == 0)
            {
                AmountPrIndividualUnder18 = AmountPrIndividualEvent;
            }
            if (AmountPrRelayTeamUnder18 == 0)
            {
                AmountPrRelayTeamUnder18 = AmountPrRelayTeam;
            }

            CompetitonName.Text = gl.GetGlobalValue("CompetitionName");

            string CurrentAccessLevelStr = "0";
            string CurrentUserName = Session["CurrentUserName"].ToString();
            if (Session["CurrentAccessLevel"] != null)
            {
                CurrentAccessLevelStr = Session["CurrentAccessLevel"].ToString();
            }
            if (CurrentAccessLevelStr != "")
            {
                CurrentAccessLevel = Convert.ToInt32(CurrentAccessLevelStr);
            }
            if ((CurrentUserName == "") || (CurrentAccessLevel == 0))
            {
                Message.Text = "Þú verður að skrá þig inn - ekki er hægt að birta keppendur þína fyrr.";
                return;
            }
            else
            {
                if (CurrentAccessLevel == 2)
                {
                    ClubCode.Text = "Administrator: Keppendur frá öllum félögum";
                }
                else
                {
                    if (CurrentAccessLevel == 1)
                    {
                        var ClubsForUsr = Athl.ClubsForUser(CurrentUserName);
                        foreach (var ClubInUserClubs in ClubsForUsr)
                        {
                            if (ClubList == "")
                            {
                                ClubList = ClubInUserClubs.Club.ToString();
                            }
                            else
                            {
                                ClubList = ClubList + ", " + ClubInUserClubs.Club.ToString();
                            }
                        }

                        ClubCode.Text = ClubList;
                    }
                }
                var AgeGroupsAndEvents = Athl.CompetitionAgeGroupsAndEvents(CompCode, 0).ToList();  //Athuga með að birta boðhlaupssveitirnar!!!
                var CompetitorsPrClubPrAgeGroup = Athl.CompetitorsAndRegistrationInCompetition2(CompCode, CurrentUserName).ToList();
                //  CurrLblNo = CurrLblNo + 10000;
                TotalAmtPrClub = 0;
                TotalAmtPrClubUnder18 = 0;
                NoOfCompetitorsPrClub = 0;
                NoOfCompetitorsPrClubUnder18 = 0;
                foreach (var AgeGAndEv in AgeGroupsAndEvents)
                {
                    CurrLblNo = CurrLblNo + 1;
                    CurrAgeGroupCode = AgeGAndEv.AgeGr;
                    AddLabelWithFontSize(CurrLblNo, AgeGAndEv.AgeGroupName, "500px", "left", "x-large");
                    NoEventsInAgeGr = Convert.ToInt32(AgeGAndEv.NoOfEvents.ToString());

                    for (int Ixxx = 1; Ixxx <= 30; Ixxx++)
                    {
                        EventLineArr[Ixxx] = 0;
                    }

                    for (int IEvNo = 1; IEvNo <= NoEventsInAgeGr; IEvNo++)
                    {
                        CurrLblNo = CurrLblNo + 1;
                        switch (IEvNo)
                        {
                            case 1:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine01.ToString(), AgeGAndEv.Event01.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine01);
                                break;
                            case 2:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine02.ToString(), AgeGAndEv.Event02.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine02);
                                break;
                            case 3:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine03.ToString(), AgeGAndEv.Event03.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine03);
                                break;
                            case 4:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine04.ToString(), AgeGAndEv.Event04.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine04);
                                break;
                            case 5:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine05.ToString(), AgeGAndEv.Event05.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine05);
                                break;
                            case 6:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine06.ToString(), AgeGAndEv.Event06.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine06);
                                break;
                            case 7:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine07.ToString(), AgeGAndEv.Event07.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine07);
                                break;
                            case 8:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine08.ToString(), AgeGAndEv.Event08.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine08);
                                break;
                            case 9:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine09.ToString(), AgeGAndEv.Event09.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine09);
                                break;
                            case 10:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine10.ToString(), AgeGAndEv.Event10.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine10);
                                break;
                            case 11:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine11.ToString(), AgeGAndEv.Event11.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine11);
                                break;
                            case 12:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine12.ToString(), AgeGAndEv.Event12.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine12);
                                break;
                            case 13:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine13.ToString(), AgeGAndEv.Event13.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine13);
                                break;
                            case 14:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine14.ToString(), AgeGAndEv.Event14.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine14);
                                break;
                            case 15:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine15.ToString(), AgeGAndEv.Event15.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine15);
                                break;
                            case 16:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine16.ToString(), AgeGAndEv.Event16.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine16);
                                break;
                            case 17:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine17.ToString(), AgeGAndEv.Event17.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine17);
                                break;
                            case 18:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine18.ToString(), AgeGAndEv.Event18.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine18);
                                break;
                            case 19:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine19.ToString(), AgeGAndEv.Event19.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine19);
                                break;
                            case 20:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine20.ToString(), AgeGAndEv.Event20.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine20);
                                break;
                            case 21:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine21.ToString(), AgeGAndEv.Event21.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine21);
                                break;
                            case 22:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine22.ToString(), AgeGAndEv.Event22.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine22);
                                break;
                            case 23:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine23.ToString(), AgeGAndEv.Event23.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine23);
                                break;
                            case 24:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine24.ToString(), AgeGAndEv.Event24.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine24);
                                break;
                            case 25:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine25.ToString(), AgeGAndEv.Event25.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine25);
                                break;
                            case 26:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine26.ToString(), AgeGAndEv.Event26.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine26);
                                break;
                            case 27:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine27.ToString(), AgeGAndEv.Event27.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine27);
                                break;
                            case 28:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine28.ToString(), AgeGAndEv.Event28.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine28);
                                break;
                            case 29:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine29.ToString(), AgeGAndEv.Event29.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine29);
                                break;
                            case 30:
                                AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine30.ToString(), AgeGAndEv.Event30.ToString(), "49px", "center", 0, true, false);
                                EventLineArr[IEvNo] = Convert.ToInt32(AgeGAndEv.EventLine30);
                                break;
                        }
                    }
                    PH1.Controls.Add(new LiteralControl("<br />"));

                    TotalAmtPrAgeGroup = 0;
                    NoOfCompetitorsPrAgeGroup = 0;
                    NoOfRegistrationsIndividuals = 0;
                    NoOfRegistrationsIndividualsUnder18 = 0;

                    foreach (var CompPrClub in CompetitorsPrClubPrAgeGroup)
                    {
                        if (CompPrClub.AgeGroup == AgeGAndEv.AgeGr)
                        {
                            NoOfCompetitorsPrAgeGroup = NoOfCompetitorsPrAgeGroup + 1;
                            NoOfCompetitorsPrClub = NoOfCompetitorsPrClub + 1;
                            CurrChkBoxNo = 0;
                            Int32 CompBibNo = Convert.ToInt32(CompPrClub.BibNo);
                            string CompName = CompPrClub.Name.ToString();
                            string CompClub = CompPrClub.Club.ToString();
                            string CompAgeGr = CompPrClub.AgeGroup.ToString();
                            string CompAge = CompPrClub.Age.ToString();
                            if (Convert.ToInt32(CompAge) < 18)
                            {
                                NoOfCompetitorsPrClubUnder18 = NoOfCompetitorsPrClubUnder18 + 1;
                            }
                            string CompBirthYear = CompPrClub.BirthYear.ToString();
                            string DateOfBirthText = CompPrClub.BirthDateText;
                            if (DateOfBirthText == "")
                            {
                                DateOfBirthText = CompBirthYear;
                            }
                            CurrLineNo = CurrLineNo + 1;
                            CurrLblNo = CurrLblNo + 1;
                            AddTextBoxWithBorderStyle(CurrLineNo, CurrLblNo, 0, "BibNo", CompBibNo.ToString(), "40px", "right", 0, true, true);
                            CurrLblNo = CurrLblNo + 1;
                            AddLabelWithFontSize(CurrLblNo, "", "10px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabelWithFontSize(CurrLblNo, CompName, "200px", "left", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabelWithFontSize(CurrLblNo, "", "10px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabelWithFontSize(CurrLblNo, DateOfBirthText, "100px", "right", "");
                            //CurrLblNo = CurrLblNo + 1;
                            //AddLabelWithFontSize(CurrLblNo, CompBirthYear, "40px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabelWithFontSize(CurrLblNo, CompAge, "40px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabelWithFontSize(CurrLblNo, "", "10px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabelWithFontSize(CurrLblNo, CompClub, "80px", "left", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabelWithFontSize(CurrLblNo, "", "8px", "right", "");
                            //     CurrLblNo = CurrLblNo + 1;
                            //     AddLabelWithFontSize(CurrLblNo, CompAgeGr, "65px", "right", "");

                            for (int Ickb = 1; Ickb <= NoEventsInAgeGr; Ickb++)
                            {
                                CurrChkBoxNo = CurrChkBoxNo + 1;
                                CurrTabIndex = CurrTabIndex + 1;
                                switch (Ickb)
                                {
                                    case 1:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[1], CompPrClub.Eve01.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve01.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType01));
                                        break;
                                    case 2:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[2], CompPrClub.Eve02.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve02.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType02));
                                        break;
                                    case 3:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[3], CompPrClub.Eve03.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve03.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType03));
                                        break;
                                    case 4:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[4], CompPrClub.Eve04.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve04.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType04));
                                        break;
                                    case 5:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[5], CompPrClub.Eve05.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve05.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType05));
                                        break;
                                    case 6:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[6], CompPrClub.Eve06.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve06.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType06));
                                        break;
                                    case 7:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[7], CompPrClub.Eve07.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve07.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType07));
                                        break;
                                    case 8:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[8], CompPrClub.Eve08.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve08.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType08));
                                        break;
                                    case 9:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[9], CompPrClub.Eve09.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve09.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType09));
                                        break;
                                    case 10:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[10], CompPrClub.Eve10.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve10.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType10));
                                        break;
                                    case 11:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[11], CompPrClub.Eve11.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve11.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType11));
                                        break;
                                    case 12:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[12], CompPrClub.Eve12.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve12.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType12));
                                        break;
                                    case 13:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[13], CompPrClub.Eve13.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve13.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType13));
                                        break;
                                    case 14:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[14], CompPrClub.Eve14.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve14.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType14));
                                        break;
                                    case 15:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[15], CompPrClub.Eve15.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve15.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType15));
                                        break;
                                    case 16:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[16], CompPrClub.Eve16.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve16.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType16));
                                        break;
                                    case 17:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[17], CompPrClub.Eve17.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve17.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType17));
                                        break;
                                    case 18:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[18], CompPrClub.Eve18.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve18.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType18));
                                        break;
                                    case 19:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[19], CompPrClub.Eve19.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve19.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType19));
                                        break;
                                    case 20:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[20], CompPrClub.Eve20.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve20.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType20));
                                        break;
                                    case 21:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[21], CompPrClub.Eve21.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve21.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType21));
                                        break;
                                    case 22:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[22], CompPrClub.Eve22.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve22.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType22));
                                        break;
                                    case 23:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[23], CompPrClub.Eve23.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve23.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType23));
                                        break;
                                    case 24:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[24], CompPrClub.Eve24.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve24.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType24));
                                        break;
                                    case 25:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[25], CompPrClub.Eve25.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve25.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType25));
                                        break;
                                    case 26:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[26], CompPrClub.Eve26.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve26.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType26));
                                        break;
                                    case 27:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[27], CompPrClub.Eve27.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve27.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType27));
                                        break;
                                    case 28:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[28], CompPrClub.Eve28.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve28.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType28));
                                        break;
                                    case 29:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[29], CompPrClub.Eve29.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve29.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType29));
                                        break;
                                    case 30:
                                        AddCheckBox(CompPrClub.BibNo.ToString(), EventLineArr[30], CompPrClub.Eve30.ToString(), "51px", CurrTabIndex, false);
                                        UpdateTotals(CompPrClub.Eve30.ToString(), Convert.ToInt32(AgeGAndEv.AgeGroupAgeTo), Convert.ToInt32(AgeGAndEv.DetailedEventType30));
                                        break;
                                }
                            }
                            PH1.Controls.Add(new LiteralControl("<br />"));
                            //Message.Text = "No of Controls: " + PH1.Controls.Count.ToString(); 

                        }

                    }
                    CurrLineNo = CurrLineNo + 1;
                    RegistrationInfo = "";
                    if (NoOfRegistrationsIndividuals > 0)
                    {
                        RegistrationInfo = " Samtals skráningar: " + string.Format("{0:N0}", NoOfRegistrationsIndividuals);

                    }
                    if (NoOfRegistrationsIndividualsUnder18 > 0)
                    {
                        RegistrationInfo = RegistrationInfo + " " +
                            " Samtals skráningar 17 og yngri: " + string.Format("{0:N0}", NoOfRegistrationsIndividualsUnder18);
                    }
                    AddTextBoxWithBorderStyle(CurrLineNo, CurrLineNo, CurrLineNo, "TotPrAgeGr", "Samtals keppendur: " +
                        string.Format("{0:N0}", NoOfCompetitorsPrAgeGroup) + RegistrationInfo + " Samtals skráningargjöld: " +
                        string.Format("{0:N2}", TotalAmtPrAgeGroup), "1000px", "left", CurrLineNo, true, true);
                    PH1.Controls.Add(new LiteralControl("<br />"));
                    PH1.Controls.Add(new LiteralControl("<br />"));
                }
                PH1.Controls.Add(new LiteralControl("<br />"));

                CurrLineNo = CurrLineNo + 1;
                RegistrationInfo = "";
                if (TotalNoOfRegistrationsIndividualsUnder18 == 0)
                {
                    AddTextBoxWithBorderStyle(CurrLineNo, CurrLineNo, CurrLineNo, "TotClub1", "Samtals keppendur: " +
string.Format("{0:N0}", NoOfCompetitorsPrClub) +
" Fjöldi skráninga: " + string.Format("{0:N0}", TotalNoOfRegistrationsIndividuals) +
" Samtals skráningargjöld: " + string.Format("{0:N2}", TotalAmtPrClub), "1000px", "left", CurrLineNo, true, true);
                    PH1.Controls.Add(new LiteralControl("<br />"));
                }
                else
                {
                    AddTextBoxWithBorderStyle(CurrLineNo, CurrLineNo, CurrLineNo, "TotClubU18", "Samtals keppendur 17 og yngri: " +
    string.Format("{0:N0}", NoOfCompetitorsPrClubUnder18) +
    " Fjöldi skráninga: " + string.Format("{0:N0}", TotalNoOfRegistrationsIndividualsUnder18) +
    " Samtals skráningargjöld: " + string.Format("{0:N2}", TotalAmtPrClubUnder18), "1000px", "left", CurrLineNo, true, true);
                    PH1.Controls.Add(new LiteralControl("<br />"));
                    RegistrationInfo = RegistrationInfo + " 18 ára og eldri";

                    if (TotalNoOfRegistrationsIndividuals > 0)
                    {
                        //RegistrationInfo = " Samtals skráningar: " + string.Format("{0:N0}", TotalNoOfRegistrationsIndividuals);
                        AddTextBoxWithBorderStyle(CurrLineNo, CurrLineNo, CurrLineNo, "TotClub", "Samtals keppendur" + RegistrationInfo + ": " +
    string.Format("{0:N0}", NoOfCompetitorsPrClub - NoOfCompetitorsPrClubUnder18) +
    " Fjöldi skráninga: " + string.Format("{0:N0}", TotalNoOfRegistrationsIndividuals) +
    " Samtals skráningargjöld: " + string.Format("{0:N2}", TotalAmtPrClub - TotalAmtPrClubUnder18), "1000px", "left", CurrLineNo, true, true);
                        PH1.Controls.Add(new LiteralControl("<br />"));

                        AddTextBoxWithBorderStyle(CurrLineNo, CurrLineNo, CurrLineNo, "TotClub2", "Heildarfjöldi keppenda:" +
    string.Format("{0:N0}", NoOfCompetitorsPrClub) +
    " Fjöldi skráninga: " + string.Format("{0:N0}", TotalNoOfRegistrationsIndividuals + TotalNoOfRegistrationsIndividualsUnder18) +
    " Samtals skráningargjöld: " + string.Format("{0:N2}", TotalAmtPrClub), "1000px", "left", CurrLineNo, true, true);
                        PH1.Controls.Add(new LiteralControl("<br />"));

                    }
                }
                //if (TotalNoOfRegistrationsIndividualsUnder18 > 0)
                //{
                //    RegistrationInfo = RegistrationInfo + " " +
                //        " Samtals skráningar 17 og yngri: " + string.Format("{0:N0}", TotalNoOfRegistrationsIndividualsUnder18);
                //}

                //AddTextBoxWithBorderStyle(CurrLineNo, CurrLineNo, CurrLineNo, "TotPrAgeGr", "Samtals keppendur: " +
                //    string.Format("{0:N0}", NoOfCompetitorsPrClub) + " Samtals skráningargjöld: " + RegistrationInfo +
                //    string.Format("{0:N2}", TotalAmtPrClub - Tot), "1000px", "left", CurrLineNo, true, true);
                //PH1.Controls.Add(new LiteralControl("<br />"));

            }
        }

        private void UpdateTotals(string RegisteredInEvent, int EventAgeTo, int DetailedEventType)
        {            
            if (RegisteredInEvent == "1")
                if (DetailedEventType == 8)  //Relay
                {
                    if ((EventAgeTo < 18) && (EventAgeTo > 0))
                    {
                        TotalAmtPrAgeGroup = TotalAmtPrAgeGroup + AmountPrRelayTeamUnder18;
                        TotalAmtPrClub = TotalAmtPrClub + AmountPrRelayTeamUnder18;
                        TotalAmtPrClubUnder18 = TotalAmtPrClubUnder18 + AmountPrRelayTeamUnder18;
                        NoOfRegistrationsRelayTeamsUnder18 = NoOfRegistrationsRelayTeamsUnder18 + 1;
                        TotalNoOfRegistrationsRelayTeamsUnder18 = TotalNoOfRegistrationsRelayTeamsUnder18 + 1;
                    }
                    else
                    {
                        TotalAmtPrAgeGroup = TotalAmtPrAgeGroup + AmountPrRelayTeam;
                        TotalAmtPrClub = TotalAmtPrClub + AmountPrRelayTeam;
                        NoOfRegistrationsRelayTeams = NoOfRegistrationsRelayTeams + 1;
                        TotalNoOfRegistrationsRelayTeams = TotalNoOfRegistrationsRelayTeams + 1;
                    }
                }
                else
                {
                    if ((EventAgeTo < 18) && (EventAgeTo > 0))
                    {
                        TotalAmtPrAgeGroup = TotalAmtPrAgeGroup + AmountPrIndividualUnder18;
                        TotalAmtPrClub = TotalAmtPrClub + AmountPrIndividualUnder18;
                        TotalAmtPrClubUnder18 = TotalAmtPrClubUnder18 + AmountPrIndividualUnder18;
                        NoOfRegistrationsIndividualsUnder18 = NoOfRegistrationsIndividualsUnder18 + 1;
                        TotalNoOfRegistrationsIndividualsUnder18 = TotalNoOfRegistrationsIndividualsUnder18 + 1;
                    }
                    else
                    {
                        TotalAmtPrAgeGroup = TotalAmtPrAgeGroup + AmountPrIndividualEvent;
                        TotalAmtPrClub = TotalAmtPrClub + AmountPrIndividualEvent;
                        NoOfRegistrationsIndividuals = NoOfRegistrationsIndividuals + 1;
                        TotalNoOfRegistrationsIndividuals = TotalNoOfRegistrationsIndividuals + 1;
                    }
                }

        }

        protected void AddCheckBox(string BibNo, Int32 EventLineNo, string CheckBoxValue, string CheckBoxWidth, Int32 TabIndx, bool SetReadOnly)
        {
            CheckBox ChkBx = new CheckBox();
            ChkBx.ID = "ChkBx_" + BibNo + "_" +
                Convert.ToInt32(EventLineNo) + "_" +
                CheckBoxValue; // +"_" + Convert.ToString(ChkBoxIndexNo) + "_" + Convert.ToString(LineNo) + "_" + Convert.ToString(CheckBoxNo);
            ChkBx.Width = new System.Web.UI.WebControls.Unit(CheckBoxWidth);
            if (TabIndx > 0)
            {
                ChkBx.TabIndex = Convert.ToInt16(TabIndx);
            }
            if (SetReadOnly == true)
            {
                ChkBx.Enabled = false;
            }
            else
            {
                ChkBx.Enabled = true;
            }
            if (CheckBoxValue == "1")
            {
                ChkBx.Checked = true;
            }
            else
            {
                ChkBx.Checked = false;
            }
            ChkBx.Style["text-align"] = "center";
            ChkBx.BorderStyle = BorderStyle.Groove;
            ChkBx.BorderWidth = new System.Web.UI.WebControls.Unit("1");

            PH1.Controls.Add(ChkBx);
        }

        protected void AddTextBoxWithBorderStyle(Int32 LineNo, Int32 TxtBoxNo, Int32 IndexNo, string TextBoxIDPrefix, string TextBoxText, string TextBoxWidth,
            string AlignText, Int32 TabIndx, bool SetReadOnly, bool BorderStyleNone)
        {
            TextBox Txb = new TextBox();
            Txb.ID = "Txb_" + IndexNo.ToString() + "_" + TextBoxIDPrefix + Convert.ToString(LineNo) + "_" + Convert.ToString(TxtBoxNo);
            Txb.Width = new System.Web.UI.WebControls.Unit(TextBoxWidth);
            if (AlignText != "")
            {
                Txb.Style["text-align"] = AlignText; //right, left
            }
            if (TabIndx > 0)
            {
                Txb.TabIndex = Convert.ToInt16(TabIndx);
            }
            Txb.ReadOnly = SetReadOnly;
            Txb.Text = TextBoxText;
            if (BorderStyleNone == true)
            {
                Txb.BorderStyle = BorderStyle.None;
            }
            PH1.Controls.Add(Txb);
        }

        protected void AddLabelWithFontSize(Int32 LblNo, string LabelText, string LabelWidth, string AlignText, string FontSizeText)
        {
            Label Lbl = new Label();
            Lbl.ID = "Lbl" + Convert.ToString(LblNo);
            Lbl.Width = new System.Web.UI.WebControls.Unit(LabelWidth);
            if (AlignText != "")
            {
                Lbl.Style["text-align"] = AlignText; //right, left
            }
            if (FontSizeText != "")
            {
                Lbl.Style["font-size"] = FontSizeText;  //ts.Style.Add("font-size", "xx-large");
            }
            Lbl.Text = LabelText;
            PH1.Controls.Add(Lbl);
        }

        protected void Back2_Click(object sender, EventArgs e)
        {
//            Global gl = new Global();
            Response.Redirect("CompetitorsForUser.aspx");

        }

        protected void SaveAndReturn_Click(object sender, EventArgs e)
        {
            UpdateDatabase();
            Response.Redirect("CompetitorsForUser.aspx");

        }

    }
}