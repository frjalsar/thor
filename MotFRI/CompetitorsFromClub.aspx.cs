using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class CompetitorsFromClub : System.Web.UI.Page
    {
        static bool PageHasBeenLoaded = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (PageHasBeenLoaded == false)
            {
                Global gl = new Global();
                string CompCode = "";
                CompCode = gl.GetCompetitionCode();
                LoadCompetitorsPage();
                AddTextBox(7777, 8888, 9999, "InOnIn", "Competition is " + CompCode, "200px", "left", 666, false, false);
                PH2.Controls.Add(new LiteralControl("<br />"));
                PageHasBeenLoaded = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {
              //  CheckAllControls();
                UpdateRegistration();
            }

            if (IsPostBack == false)  //InitialPageLoad
            {
                //LoadCompetitorsPage();

            }
            //ClubCode.Text = gl.GetGlobalValue("SelectedClubCode") + " - " + gl.GetGlobalValue("SelectedClubName");



            else //Not Postback
            {

            }

        }
        protected void LoadCompetitorsPage()
        {
            AthleticsEntities1 Athl = new AthleticsEntities1();
            //EventsPrAgeGroupInCompetition_Result EvePrAgeGr = new EventsPrAgeGroupInCompetition_Result();

            Global gl = new Global();
            string CompCode = "";
            CompCode = gl.GetCompetitionCode();
            Int32 CurrentAccessLevel = 0;
            string ClubList = "";
            Int32 CurrLblNo = 0;
            Int32 CurrChkBoxNo = 0;
            Int32 CurrLineNo = 0;
            Int32 CurrTabIndex = 0;
            Int32 NoEventsInAgeGr = 0;
            string CurrAgeGroupCode = "";

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
                var AgeGroupsAndEvents = Athl.AgeGroupsAndEvents(CompCode, 0).ToList();
                var CompetitorsPrClubPrAgeGroup = Athl.CompetitorsAndRegistrationInCompetition2(CompCode, CurrentUserName).ToList();
                foreach (var AgeGAndEv in AgeGroupsAndEvents)
                {
                    CurrLblNo = CurrLblNo + 1;
                    CurrAgeGroupCode = AgeGAndEv.AgeGr;
                    AddLabel(CurrLblNo, AgeGAndEv.AgeGroupName, "550px", "left", "x-large");
                    NoEventsInAgeGr = Convert.ToInt32(AgeGAndEv.NoOfEvents.ToString());

                    for (int IEvNo = 1; IEvNo <= NoEventsInAgeGr; IEvNo++)
                    {
                        CurrLblNo = CurrLblNo + 1;
                        switch (IEvNo)
                        {
                            case 1:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine01.ToString(), AgeGAndEv.Event01.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 2:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine02.ToString(), AgeGAndEv.Event02.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 3:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine03.ToString(), AgeGAndEv.Event03.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 4:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine04.ToString(), AgeGAndEv.Event04.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 5:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine05.ToString(), AgeGAndEv.Event05.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 6:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine06.ToString(), AgeGAndEv.Event06.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 7:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine07.ToString(), AgeGAndEv.Event07.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 8:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine08.ToString(), AgeGAndEv.Event08.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 9:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine09.ToString(), AgeGAndEv.Event09.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 10:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine10.ToString(), AgeGAndEv.Event10.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 11:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine11.ToString(), AgeGAndEv.Event11.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 12:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine12.ToString(), AgeGAndEv.Event12.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 13:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine13.ToString(), AgeGAndEv.Event13.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 14:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine14.ToString(), AgeGAndEv.Event14.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 15:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine15.ToString(), AgeGAndEv.Event15.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 16:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine16.ToString(), AgeGAndEv.Event16.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 17:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine17.ToString(), AgeGAndEv.Event17.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 18:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine18.ToString(), AgeGAndEv.Event18.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 19:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine19.ToString(), AgeGAndEv.Event19.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 20:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine20.ToString(), AgeGAndEv.Event20.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 21:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine21.ToString(), AgeGAndEv.Event21.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 22:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine22.ToString(), AgeGAndEv.Event22.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 23:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine23.ToString(), AgeGAndEv.Event23.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 24:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine24.ToString(), AgeGAndEv.Event24.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 25:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine25.ToString(), AgeGAndEv.Event25.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 26:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine26.ToString(), AgeGAndEv.Event26.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 27:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine27.ToString(), AgeGAndEv.Event27.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 28:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine28.ToString(), AgeGAndEv.Event28.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 29:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine29.ToString(), AgeGAndEv.Event29.ToString(), "49px", "center", 0, true, false);
                                break;
                            case 30:
                                AddTextBox(CurrLineNo, CurrLblNo, IEvNo, AgeGAndEv.EventLine30.ToString(), AgeGAndEv.Event30.ToString(), "49px", "center", 0, true, false);
                                break;
                        }
                    }
                    PH2.Controls.Add(new LiteralControl("<br />"));

                    foreach (var CompPrClub in CompetitorsPrClubPrAgeGroup)
                    {
                        if (CompPrClub.AgeGroup == AgeGAndEv.AgeGr)
                        {
                            CurrChkBoxNo = 0;
                            Int32 CompBibNo = Convert.ToInt32(CompPrClub.BibNo);
                            string CompName = CompPrClub.Name.ToString();
                            string CompClub = CompPrClub.Club.ToString();
                            string CompAgeGr = CompPrClub.AgeGroup.ToString();
                            string CompAge = CompPrClub.Age.ToString();
                            string CompBirthYear = CompPrClub.BirthYear.ToString();
                            string DateOfBirthText = CompPrClub.BirthDateText;
                            if (DateOfBirthText == "")
                            {
                                DateOfBirthText = CompBirthYear;
                            }
                            CurrLineNo = CurrLineNo + 1;
                            CurrLblNo = CurrLblNo + 1;
                            AddTextBox(CurrLineNo, CurrLblNo, 0, "BibNo", CompBibNo.ToString(), "40px", "right", 0, true, true);
                            CurrLblNo = CurrLblNo + 1;
                            AddLabel(CurrLblNo, "", "10px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabel(CurrLblNo, CompName, "250px", "left", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabel(CurrLblNo, "", "10px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabel(CurrLblNo, DateOfBirthText, "100px", "right", "");
                            //CurrLblNo = CurrLblNo + 1;
                            //AddLabel(CurrLblNo, CompBirthYear, "40px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabel(CurrLblNo, CompAge, "40px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabel(CurrLblNo, "", "10px", "right", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabel(CurrLblNo, CompClub, "80px", "left", "");
                            CurrLblNo = CurrLblNo + 1;
                            AddLabel(CurrLblNo, "", "8px", "right", "");
                            //     CurrLblNo = CurrLblNo + 1;
                            //     AddLabel(CurrLblNo, CompAgeGr, "65px", "right", "");

                            for (int Ickb = 1; Ickb <= NoEventsInAgeGr; Ickb++)
                            {
                                CurrChkBoxNo = CurrChkBoxNo + 1;
                                CurrTabIndex = CurrTabIndex + 1;
                                switch (Ickb)
                                {
                                    case 1:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve01.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 2:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve02.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 3:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve03.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 4:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve04.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 5:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve05.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 6:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve06.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 7:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve07.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 8:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve08.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 9:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve09.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 10:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve10.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 11:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve11.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 12:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve12.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 13:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve13.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 14:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve14.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 15:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve15.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 16:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve16.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 17:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve17.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 18:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve18.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 19:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve19.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 20:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve20.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 21:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve21.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 22:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve22.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 23:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve23.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 24:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve24.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 25:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve25.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 26:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve26.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 27:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve27.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 28:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve28.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 29:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve29.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                    case 30:
                                        AddCheckBox(CurrLineNo, CurrChkBoxNo, Ickb, CompPrClub.Eve30.ToString(), "51px", CurrTabIndex, false);
                                        break;
                                }
                            }
                            PH2.Controls.Add(new LiteralControl("<br />"));
                            //Message.Text = "No of Controls: " + PH2.Controls.Count.ToString();

                        }
                    }
                }
            }
        }

        protected void AddLabel(Int32 LblNo, string LabelText, string LabelWidth, string AlignText, string FontSizeText)
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
            PH2.Controls.Add(Lbl);
        }

        protected void AddTextBox(Int32 LineNo, Int32 TxtBoxNo, Int32 IndexNo, string TextBoxIDPrefix, string TextBoxText, string TextBoxWidth,
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
            PH2.Controls.Add(Txb);
        }

        protected void AddCheckBox(Int32 LineNo, Int32 CheckBoxNo, Int32 ChkBoxIndexNo, string CheckBoxValue, string CheckBoxWidth, Int32 TabIndx, bool SetReadOnly)
        {
            CheckBox ChkBx = new CheckBox();
            ChkBx.ID = "ChkBx_" + CheckBoxValue + "_" + Convert.ToString(ChkBoxIndexNo) + "_" + Convert.ToString(LineNo) + "_" + Convert.ToString(CheckBoxNo);
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

            PH2.Controls.Add(ChkBx);
        }

        protected void UpdateRegistration()
        {
            Global gl = new Global();
            string TextBoxID = "";
            string CheckBoxID = "";
            Int32 CurrentBibNo = 0;
            string CurrValue = "";
            string[] TextBoxNameParts = new string[10];
            string[] CheckBoxNameParts = new string[10];
            Int32[] EventLineArr = new Int32[30];
            Int32 CurrentChkBoxIndex;
            bool WasRegistredForEvent;
            bool IsRegisteredForEvent;

            foreach (Control item in PH2.Controls)
            {
                if (item is TextBox)
                {
                    TextBox t1 = (TextBox)PH2.FindControl(item.ID);
                    CurrValue = t1.Text;
                    TextBoxID = t1.ID + "___XXXX";
                    TextBoxNameParts = TextBoxID.Split('_');
                    if (TextBoxID.Substring(0, 9) == "Txb_BibNo")
                    {
                        CurrentBibNo = gl.TryConvertStringToInt32(CurrValue);

                    }
                    Int32 WrkIndex = gl.TryConvertStringToInt32(TextBoxNameParts[1]);
                    Int32 WrkEventLineNo = gl.TryConvertStringToInt32(TextBoxNameParts[2]);
                    if ((WrkIndex > 0) && (WrkEventLineNo > 0))
                    {
                        EventLineArr[WrkIndex] = WrkEventLineNo;
                    }

                }
                if (item is CheckBox)
                {
                    CheckBox cb = (CheckBox)PH2.FindControl(item.ID);
                    CheckBoxID = cb.ID;
                    CheckBoxNameParts = CheckBoxID.Split('_');
                    if (CheckBoxNameParts[1] == "true")
                    {
                        WasRegistredForEvent = true;
                    }
                    else
                    {
                        WasRegistredForEvent = false;
                    }
                    CurrentChkBoxIndex = gl.TryConvertStringToInt32(CheckBoxNameParts[3]);
                    IsRegisteredForEvent = cb.Checked;
                    if ((WasRegistredForEvent == false) && (IsRegisteredForEvent == true))
                    {

                        //if (AthlCompetitorInComp.rasnumer == 0)
                        //{
                        //    AthlCompetitorInComp = AthlCompCRUD.GetCompetitorInComp(gl.GetCompetitionCode(), gl.GetBibNumber());
                        //}

                        //AthlCompetitorInEvRec = AthlCompCRUD.GetCompetitorInEvent(gl.GetCompetitionCode(), gl.GetBibNumber(), EventLine);
                        //if (AthlCompetitorInEvRec == null)
                        //{
                        //    AthlCompetitorInEvRec = AthlCompCRUD.InitCompetitorInEvent();
                        //}
                        //if (AthlCompetitorInEvRec.rasnumer == null)
                        //{
                        //    AthlCompetitorInEvRec.rasnumer = 0;
                        //}
                        //if (AthlCompetitorInEvRec.rasnumer == 0)
                        //{
                        //    AthlCompEvent = AthlCompCRUD.GetCompetitionEvent(gl.GetCompetitionCode(), EventLine);

                        //    AthlCompCRUD.RegisterCompetitorInEvent(gl.GetCompetitionCode(), AthlCompEvent, gl.GetBibNumber(), AthlCompetitorInComp,
                        //        1, 999);
                        //}




                    }

                }
            }

        }

        protected void CheckAllControls()
        {
            Control control = null;
            string ControlID = "";
            string ctrlname = Page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = this.Page.FindControl(ctrlname);
                if (control != null)
                {
                    ControlID = control.ID;
                }
            }
            else
            {
                string ctrlStr = String.Empty;
                //Control c = null;

                foreach (var ContInPage in Page.Request.Form)
                {
                    //ControlID = ContInPage.ID;
                    ControlID = Convert.ToString(ContInPage);

                }
                //foreach (string ctl in Page.Request.Form)
                //{
                //    if (c is System.Web.UI.WebControls.CheckBox)
                //    {
                //        control = c;
                //        break;
                //    }
                //}
                //ControlID = c.ID;
                ////#endregion
            }

            // Check if ID is of Checkbox
            if (ControlID == "checkboxID")
            {
                ControlID = ControlID + "Fannst hér";
                //     if(ControlID.Checked == true)
                //    {
                /// YOUR CODE HERE
                //  }
            }
        }
        protected void SaveRegistration_Click(object sender, EventArgs e)
        {

            UpdateRegistration();

        }
    }
}