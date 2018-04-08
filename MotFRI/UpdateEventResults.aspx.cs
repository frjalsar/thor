using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MotFRI
{
    public partial class UpdateEventResults : System.Web.UI.Page
    {
        //static string HeatNumberFilter = "%";
        string HeatNumberFilter = "%";
        Int32 ElectrTiming = 0;
        enum CompetitionStatus
        {
            ÓsamþykktAfFRÍ,
            SamþykktAfFRÍ,
            OpiðFyrirSkráningu,
            LokaðFyrirSkráningu,
            StendurYfir,
            KeppniLokið
        };

        // AccessLevelText = "0" : Not logged in - Aðeins fyrirspurnir
        // AccessLevelText = "1" : Club Representative 
        // AccessLevelText = "2" : Administrator
        // AccessLevelText = "3" : Can edit entry sheets
        // AccessLevelText = "4" : Competition Administrator
        enum AccLevel
        {
            NotLoggedIn,
            ClubRepresentative,
            Administrator,
            CanEditEntrySheets,
            CompetitionAdministrator
        };

        protected void Page_Load(object sender, EventArgs e)
        {

            Global gl = new Global();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            string CurrPageAndHeatID = "xx";
            Athl_Competition AthlComp = new Athl_Competition();
            Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();
            var CurrAccLevlVar = Session["CurrentAccessLevel"];
            if (CurrAccLevlVar == null)
            {
                string CurrUsrN = gl.GetCurrUsrName();
                string CurrAccLevl = gl.GetCurrAccLev();
                if ((CurrUsrN == null) || (CurrAccLevl == null))
                {
                    string msg = "Sessionin þín er útrunnin. Þú verður að ská þig inn aaftur";
                    Response.Write("<script>alert('" + msg + "')</script>");
                }
                Session["CurrentAccessLevel"] = CurrAccLevl;
                Session["CurrentUserName"] = CurrUsrN;
            }

            RetriveRunnersFromPrevRnd.Visible = false;
            if (Session["CurrentAccessLevel"].ToString() == "0")
            {

                SaveValues.Visible = false;
                AddCompetitor.Visible = false;
                SeedCompetitors.Visible = false;
                FillOutHeights.Visible = false;
                EventStatusDropDown.Enabled = false;
                ReadLynxFiles.Visible = false;
            }
            string CurrAccLev = Session["CurrentAccessLevel"].ToString();
            Session["CurrentAccessLevel"] = CurrAccLev;
            string CompCode = gl.GetCompetitionCode();
            //string EventLineNo = gl.GetCompetitionEventNo();
            string EventLineNo = Request.QueryString.Get("Event");
            Int32 EventLin = Convert.ToInt32(EventLineNo);
            Int32 NoOfHeats;
            AthleticsEntities1 AthlEntities = new AthleticsEntities1();
            System.Data.Objects.ObjectParameter OutNoOfHeats = new System.Data.Objects.ObjectParameter("OutNoOfHeats", "0");
            AthlEntities.ReturnNoOfHeatsInEvent(CompCode, EventLin, OutNoOfHeats);
            NoOfHeats = Convert.ToInt32(OutNoOfHeats.Value);
            //AthlEvent = AthlCRUD.GetAthlEvent(CompCode, )
            AthlEvent = AthlCRUD.GetCompetitionEvent(CompCode, EventLin);
            gl.SetCompetitionEventName(AthlEvent.heitigreinar);
            gl.SetGlobalValue("EvType", AthlEvent.tegundgreinar.ToString());
            gl.SetGlobalValue("EvCloserType", AthlEvent.nanaritegundargreining.ToString());
            AthlComp = AthlCRUD.GetCompetitionRec(CompCode);

            //Staða Móts: 0: Ósamþykkt af FRÍ, 1: Samþykkt af FRÍ, 2: Opið fyrir skráningu,3: Lokað fyrir skráningu, 4 : Keppni stendur yfir,5: Keppni lokið

            //if (AthlComp.CompetitionFinalized == 1) 
            if (AthlComp.Staða_móts != 4)  // 4 = Keppni stendur yfir 
            {
                SaveValues.Visible = false;
                AddCompetitor.Visible = false;
                SeedCompetitors.Visible = false;
                FillOutHeights.Visible = false;
                EventStatusDropDown.Enabled = false;
                ReadLynxFiles.Visible = false;
            }
            else
                if ((Session["CurrentAccessLevel"].ToString() == "2") ||
                    (Session["CurrentAccessLevel"].ToString() == "4") ||
                    (gl.UserCanUpdateMeet(CompCode, Session["CurrentUserName"].ToString()) == 1))  //0 = No, 1 = Yes
            {

                string CurrAccL = Session["CurrentAccessLevel"].ToString();
                Session["CurrentAccessLevel"] = CurrAccL;
                SaveValues.Visible = true;
                AddCompetitor.Visible = true;
                SeedCompetitors.Visible = true;
                //  FillOutHeights.Visible = true;
                EventStatusDropDown.Enabled = true;
                if (AthlEvent.tegundgreinar == 1)
                {
                    ReadLynxFiles.Visible = true;
                }
            }
            if ((AthlComp.Staða_móts != 5) &&
                (gl.UserCanUpdateMeet(CompCode, Session["CurrentUserName"].ToString()) == 1))
            {
                SaveValues.Visible = true;
                AddCompetitor.Visible = true;
                SeedCompetitors.Visible = true;
                EventStatusDropDown.Enabled = true;
                if (AthlEvent.tegundgreinar == 1)
                {
                    ReadLynxFiles.Visible = true;
                }
                if ((AthlEvent.tegundgreinar == 1) && //Hlaup
                    ((AthlEvent.ridill == 2) || (AthlEvent.ridill == 3) || (AthlEvent.ridill == 4) || (AthlEvent.ridill == 5)))
                {
                    RetriveRunnersFromPrevRnd.Visible = true;
                }

            }
            if ((AthlComp.Staða_móts > 1) && (AthlComp.Staða_móts < 5) && (Session["CurrentAccessLevel"].ToString() == "1"))
            {
                AddCompetitor.Visible = true;
            }
            if (IsPostBack)
            {
                UpdateDatabase();
            }

            MsgBox.Text = gl.GetGlobalValue("MsgInUpdEvResult");
            if (MsgBox.Text != "")
            {
                gl.SetGlobalValue("MsgInUpdEvResult", "");
            }
            if (!IsPostBack)
            {
                SelectTabOrder.SelectedValue = "0";
                SelectTabOrder.SelectedIndex = 0;

                CurrPageAndHeatID = DateTime.Now.ToLongTimeString();
                //MessageBox.Text = DateTime.Now.ToLongTimeString();

                if (NoOfHeats < 2)
                {
                    FilterOnHeatLabel.Visible = false;
                    FilterOnSelectedHeadDropDownList.Visible = false;
                    HeatNumberFilter = "%";
                    CurrPageAndHeat.Text = "0";
                }
                else
                {
                    for (int ix = 1; ix <= NoOfHeats; ix++)
                    {
                        ListItem HeatInfo = new ListItem();
                        HeatInfo.Text = "Aðeins riðill " + ix.ToString();
                        HeatInfo.Value = ix.ToString();
                        FilterOnSelectedHeadDropDownList.Items.Add(HeatInfo);
                    }
                    FilterOnHeatLabel.Visible = true;
                    FilterOnSelectedHeadDropDownList.Visible = true;
                    if (AthlEvent.tegundgreinar == 1)  //Track Event
                    {
                        FilterOnSelectedHeadDropDownList.SelectedIndex = 0;
                        HeatNumberFilter = "%";
                    }
                    else
                    {
                        FilterOnSelectedHeadDropDownList.SelectedIndex = 0; //Var 1
                        HeatNumberFilter = "%"; // var "1"
                    }
                }
                string QryStr = Request.QueryString.Get("HeatNoFilter");
                if (QryStr != null)
                {
                    HeatNumberFilter = QryStr;
                    if (HeatNumberFilter == "%")
                    {
                        FilterOnSelectedHeadDropDownList.SelectedIndex = 0;
                    }
                    else
                    {
                        FilterOnSelectedHeadDropDownList.SelectedIndex = Convert.ToInt32(HeatNumberFilter);
                    }

                }
                CurrPageAndHeat.Text = HeatNumberFilter;
                gl.SetGlobalValue(CurrPageAndHeatID, HeatNumberFilter);
                SelectedHeatFilter.Text = HeatNumberFilter;
                EventStatusDropDown.SelectedValue = AthlEvent.stadakeppni.ToString();
                SavedEventStatus.Text = AthlEvent.stadakeppni.ToString();

                EventName.Text = AthlEvent.heitigreinar;
            }
            else  //Is Postback
            {
                HeatNumberFilter = gl.GetGlobalValue(CurrPageAndHeatID);

                if (FilterOnSelectedHeadDropDownList.SelectedValue != HeatNumberFilter)
                {
                    HeatNumberFilter = FilterOnSelectedHeadDropDownList.SelectedValue;
                    CurrPageAndHeatID = CurrPageAndHeatID + ";" + HeatNumberFilter;
                    SelectedHeatFilter.Text = HeatNumberFilter;
                    if (HeatNumberFilter == "0")
                    {
                        HeatNumberFilter = "%";
                    }
                    CurrPageAndHeat.Text = HeatNumberFilter;
                }
            }
            if (AthlEvent.krefstvindmaelis == 1)
            {
                switch (AthlEvent.tegundgreinar)
                {
                    case 1:  //Spretthlaup utanhúss
                        FillPage(CompCode, EventLin, true, "TRACK", AthlEvent.fjoldiumferda, AthlEvent.tegundgreinar, HeatNumberFilter);
                        break;
                    case 2:  //Langstökk, Þrístökk utanhúss
                        FillPage(CompCode, EventLin, true, "TECHNICAL", AthlEvent.fjoldiumferda, AthlEvent.tegundgreinar, HeatNumberFilter);
                        break;
                }
            }
            else
            {
                switch (AthlEvent.nanaritegundargreining)
                {
                    case 1: //Hlaup
                    case 8: //Boðhlaup
                        {
                            FillPage(CompCode, EventLin, false, "TRACK", AthlEvent.fjoldiumferda, AthlEvent.tegundgreinar, HeatNumberFilter);
                            break;
                        }
                    case 2: //Götuhlaup ?
                        {
                            EventName.Text = EventName.Text + " - Götuhlaup ekki hægt að skrá hérna";
                            break;
                        }
                    case 4: //Hástökk, Stangarstökk
                        {
                            FillPage(CompCode, EventLin, false, "HJ/PV", AthlEvent.fjoldiumferda, AthlEvent.tegundgreinar, HeatNumberFilter);
                            break;
                        }
                    case 5: //Stökk
                    case 6: //Köst
                    case 10: //Beggja handa köst
                        {
                            FillPage(CompCode, EventLin, false, "TECHNICAL", AthlEvent.fjoldiumferda, AthlEvent.tegundgreinar, HeatNumberFilter);
                            break;
                        }
                    case 7: //Fjölþraut
                        {
                            FillPage(CompCode, EventLin, false, "MULTIEVENT", AthlEvent.fjoldiumferda, AthlEvent.tegundgreinar, HeatNumberFilter);
                            break;
                        }
                }
            }
        }

        protected void FillPage(string CompCode, Int32 EventNo, bool WindMetered, string EventType, Int32 NoOfAttempts,
            Int32 EventTypeInt, string HeatNumberFilter)
        {
            Int32 LabelNo = 0;
            Int32 TextBoxNo = 0;
            Int32 CheckBoxNo = 0;
            Global gl = new Global();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitorsInCompetition CompetitorsInCompRec = new Athl_CompetitorsInCompetition();
            Athl_CompetitionEvents CompetitionEventRec = new Athl_CompetitionEvents();
            Athl_Competition CompetitionRec = new Athl_Competition();
            var CompetitorsInEventDataset = AthlEnt.ReturnCompetitorsInEvent(CompCode, EventNo, HeatNumberFilter);
            decimal CurrRoundValueDecimal;
            string CurrRoundValue = "";
            string CurrWindValue = "";
            Int32 NoOfLines = 0;
            Int32 CurrHeatNo = -1;
            Int32 AddToTabIndex = 0;
            System.Data.Objects.ObjectParameter PersBestOutdoors = new System.Data.Objects.ObjectParameter("PersonalBestOutdoors", typeof(string));
            System.Data.Objects.ObjectParameter PersBestIndoors = new System.Data.Objects.ObjectParameter("PersonalBestIndoors", typeof(string));
            System.Data.Objects.ObjectParameter PersBestWind = new System.Data.Objects.ObjectParameter("PersonalBestWind", typeof(string));
            System.Data.Objects.ObjectParameter YearBestOutdoors = new System.Data.Objects.ObjectParameter("YearBestOutdoors", typeof(string));
            System.Data.Objects.ObjectParameter YearBestIndoors = new System.Data.Objects.ObjectParameter("YearBestIndoors", typeof(string));
            System.Data.Objects.ObjectParameter YearBestWind = new System.Data.Objects.ObjectParameter("YearBestWind", typeof(string));

            gl.SetEventTypeAndWindMetered(EventType, WindMetered);

            CompetitionRec = AthlCRUD.GetCompetitionRec(CompCode);

            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Lína", "50px", "right");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Eyða", "35px", "right");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Rásn.", "50px", "right");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "", "10px", "");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Nafn", "210px", "");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "F.ár", "50px", "");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Félag", "80px", "");

            if (EventType == "TRACK")
            {
                //TabOrderIndexArray = PopulateTabIndexArray(false, NoOfCompetitors, 2);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Ársbesta", "80px", "right");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Persónulegt met", "80px", "right");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "", "5px", "right");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Riðill", "50px", "right");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Braut", "50px", "right");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Úrsl", "100px", "right");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "", "10px", "");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Rafm.", "50px", "");
                if (WindMetered == true)
                {
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Vindur", "80px", "right");
                }
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Árangur", "80px", "right");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Nánari röð", "100px", "right");
                PH1.Controls.Add(new LiteralControl("<br />"));

            }
            else
            {
                if (EventType == "HJ/PV")
                {
                    //TabOrderIndexArray = PopulateTabIndexArray(false, NoOfCompetitors, 25);
                    Athl_HeightsInHJandPV AthlHeightsInHJandPVRec = new Athl_HeightsInHJandPV();
                    AthlHeightsInHJandPVRec = AthlCRUD.GetHeightsInHJandPVRec(CompCode, EventNo, -1);


                    //  if ((Session["CurrentAccessLevel"] == "2") ||
                    //       (Session["CurrentAccessLevel"] == "3") ||
                    if (gl.UserCanUpdateMeet(CompCode, Session["CurrentUserName"].ToString()) == 1)
                    {
                        string CurrAcL = Session["CurrentAccessLevel"].ToString();
                        Session["CurrentAccessLevel"] = CurrAcL;
                        if (CompetitionRec.Staða_móts == (int)CompetitionStatus.StendurYfir)
                        {
                            FillOutHeights.Visible = true;
                        }
                    }

                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Röð", "20px", "right");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "5px", "");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Árang.", "50px", "right");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "5px", "");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Úrsl", "40px", "right");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "13px", "right");


                    for (int ixx = 1; ixx <= 25; ixx++)
                    {
                        LabelNo = LabelNo + 1;
                        if ((ixx == 5) || (ixx == 10) || (ixx == 15) || (ixx == 20))
                        {
                            AddLabel(LabelNo, "    " + ixx.ToString(), "30px", "");
                        }
                        else
                        {
                            AddLabel(LabelNo, "    " + ixx.ToString(), "29px", "");
                        }
                    }
                    PH1.Controls.Add(new LiteralControl("<br />"));

                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "50px", "right");  //Lína
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "35px", "right");  //Eyða
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "50px", "right");  //Rásn
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "210px", "");   //Nafn
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "50px", "");    //F.ár
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "80px", "");   //Félag

                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "20px", "right");  //Röð
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "5px", "");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "50px", "right"); //Árangur
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "40px", "right");  //Úrsl
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "8px", "right");

                    for (int ixx = 1; ixx <= 25; ixx++)
                    {
                        string CurrentHeight = "";
                        switch (ixx)
                        {
                            case 1:
                                CurrentHeight = AthlHeightsInHJandPVRec.C1__hæð;
                                break;
                            case 2:
                                CurrentHeight = AthlHeightsInHJandPVRec.C2__hæð;
                                break;
                            case 3:
                                CurrentHeight = AthlHeightsInHJandPVRec.C3__hæð;
                                break;
                            case 4:
                                CurrentHeight = AthlHeightsInHJandPVRec.C4__hæð;
                                break;
                            case 5:
                                CurrentHeight = AthlHeightsInHJandPVRec.C5__hæð;
                                break;
                            case 6:
                                CurrentHeight = AthlHeightsInHJandPVRec.C6__hæð;
                                break;
                            case 7:
                                CurrentHeight = AthlHeightsInHJandPVRec.C7__hæð;
                                break;
                            case 8:
                                CurrentHeight = AthlHeightsInHJandPVRec.C8__hæð;
                                break;
                            case 9:
                                CurrentHeight = AthlHeightsInHJandPVRec.C9__hæð;
                                break;
                            case 10:
                                CurrentHeight = AthlHeightsInHJandPVRec.C10__hæð;
                                break;
                            case 11:
                                CurrentHeight = AthlHeightsInHJandPVRec.C11__hæð;
                                break;
                            case 12:
                                CurrentHeight = AthlHeightsInHJandPVRec.C12__hæð;
                                break;
                            case 13:
                                CurrentHeight = AthlHeightsInHJandPVRec.C13__hæð;
                                break;
                            case 14:
                                CurrentHeight = AthlHeightsInHJandPVRec.C14__hæð;
                                break;
                            case 15:
                                CurrentHeight = AthlHeightsInHJandPVRec.C15__hæð;
                                break;
                            case 16:
                                CurrentHeight = AthlHeightsInHJandPVRec.C16__hæð;
                                break;
                            case 17:
                                CurrentHeight = AthlHeightsInHJandPVRec.C17__hæð;
                                break;
                            case 18:
                                CurrentHeight = AthlHeightsInHJandPVRec.C18__hæð;
                                break;
                            case 19:
                                CurrentHeight = AthlHeightsInHJandPVRec.C19__hæð;
                                break;
                            case 20:
                                CurrentHeight = AthlHeightsInHJandPVRec.C20__hæð;
                                break;
                            case 21:
                                CurrentHeight = AthlHeightsInHJandPVRec.C21__hæð;
                                break;
                            case 22:
                                CurrentHeight = AthlHeightsInHJandPVRec.C22__hæð;
                                break;
                            case 23:
                                CurrentHeight = AthlHeightsInHJandPVRec.C23__hæð;
                                break;
                            case 24:
                                CurrentHeight = AthlHeightsInHJandPVRec.C24__hæð;
                                break;
                            case 25:
                                CurrentHeight = AthlHeightsInHJandPVRec.C25__hæð;
                                break;

                        }

                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "2px", "");
                        AddTextBox(NoOfLines, ixx, CurrentHeight, "24px", "right", 0, false, false);
                    }
                    AddTextBoxForEndOfLine(0);
                    PH1.Controls.Add(new LiteralControl("<br />"));

                }
                else
                {
                    if ((EventType == "TECHNICAL") || (EventType == "MULTIEVENT"))
                    {

                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "Röð", "50px", "right");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "10px", "");

                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "Árangur", "80px", "right");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "Úrsl", "100px", "right");
                        for (int ixx = 1; ixx <= NoOfAttempts; ixx++)
                        {
                            LabelNo = LabelNo + 1;
                            AddLabel(LabelNo, "Umf. " + ixx.ToString(), "65px", "right");
                            if (WindMetered == true)
                            {

                                LabelNo = LabelNo + 1;
                                AddLabel(LabelNo, "Vind " + ixx.ToString(), "40px", "right");
                            }
                        }
                        PH1.Controls.Add(new LiteralControl("<br />"));

                    }
                    else  //Multi Events
                    {
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "Röð", "50px", "right");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "Stig", "80px", "right");
                        PH1.Controls.Add(new LiteralControl("<br />"));

                    }
                }
            }

            foreach (var CompInEvRec in CompetitorsInEventDataset)
            {
                NoOfLines = NoOfLines + 1;
                gl.SetArr1Value(NoOfLines, Convert.ToString(CompInEvRec.lina));
                gl.SetBibNoArrayElement(NoOfLines, CompInEvRec.rasnumer);
                gl.SetHeatNoElement(NoOfLines, CompInEvRec.ridillnumer);
                gl.SetLaneOrOrder(NoOfLines, CompInEvRec.stokkkastrod);

                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, Convert.ToString(NoOfLines), "50px", "right");
                LabelNo = LabelNo + 1;
                if (CompInEvRec.rasnumer > 0)
                {
                    AddEditableCheckBox(CompInEvRec.rasnumer, CompInEvRec.lina, false, "35px");
                }
                TextBoxNo = 0;
                CheckBoxNo = 0;

                if (EventType == "TRACK")
                {

                    if ((CompInEvRec.rasnumer != 0) || (CompInEvRec.nafn != ""))
                    {
                        // AddLabel(LabelNo, Convert.ToString(CompInEvRec.rasnumer), "50px", "right");
                        AddLabelForBibNo(LabelNo, Convert.ToString(CompInEvRec.rasnumer), "50px", "right");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "10px", "");
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.nafn.Length > 22)
                        {
                            AddLabel(LabelNo, CompInEvRec.nafn.Substring(0, 22), "210px", "");
                        }
                        else
                        {
                            AddLabel(LabelNo, CompInEvRec.nafn, "210px", "");
                        }
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, Convert.ToString(CompInEvRec.faedingarar), "50px", "");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.felag, "80px", "");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.bestiaranguriartexti, "80px", "right");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.personulegtmettexti, "80px", "right");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "5px", "right");
                        TextBoxNo = TextBoxNo + 1;
                        AddTextBoxForHeatLaneEtc(NoOfLines, TextBoxNo, Convert.ToString(CompInEvRec.ridillnumer), "50px", "right");
                        TextBoxNo = TextBoxNo + 1;
                        AddTextBoxForHeatLaneEtc(NoOfLines, TextBoxNo, Convert.ToString(CompInEvRec.stokkkastrod), "50px", "right");
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.urslitarod < 10000)
                        {
                            AddLabel(LabelNo, CompInEvRec.urslitarodtexti, "100px", "right");
                        }
                        else
                        {
                            AddLabel(LabelNo, "", "100px", "right");
                        }
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, " ", "10px", "right");
                        CheckBoxNo = CheckBoxNo + 1;
                        AddCheckBox(NoOfLines, CheckBoxNo, Convert.ToBoolean(CompInEvRec.rafmagnstimataka), "50px");
                        if (WindMetered == true)
                        {
                            if (CompInEvRec.ridillnumer != CurrHeatNo)
                            {
                                TextBoxNo = TextBoxNo + 1;
                                AddTextBox(NoOfLines, TextBoxNo, gl.FormatWind(Convert.ToDecimal(CompInEvRec.vindur)),
                                    "79px", "right", NoOfLines * 10 + 1, true, false);
                                CurrHeatNo = CompInEvRec.ridillnumer;
                            }
                            else
                            {
                                LabelNo = LabelNo + 1;
                                AddLabel(LabelNo, " ", "82px", "right");
                            }
                        }

                        TextBoxNo = TextBoxNo + 1;
                        if (CompInEvRec.arangur == "")
                        {
                            if (CompInEvRec.rasnumer > 0)
                            {
                                AddTextBox(NoOfLines, TextBoxNo, CompInEvRec.athugasemd, "80px", "right", (NoOfLines * 10) + 1, false, false);
                            }
                            else
                            {
                                AddTextBox(NoOfLines, TextBoxNo, CompInEvRec.athugasemd, "80px", "right", (NoOfLines * 10) + 1, false, true);
                            }
                        }
                        else
                        {
                            if (CompInEvRec.rasnumer > 0)
                            {
                                AddTextBox(NoOfLines, TextBoxNo, CompInEvRec.arangur, "80px", "right", (NoOfLines * 10) + 1, false, false);
                            }
                            else
                            {
                                AddTextBox(NoOfLines, TextBoxNo, CompInEvRec.athugasemd, "80px", "right", (NoOfLines * 10) + 1, false, true);
                            }
                        }
                        if (CompInEvRec.rasnumer > 0)
                        {
                            LabelNo = LabelNo + 1;
                            AddLabel(LabelNo, " ", "30px", "right");

                            TextBoxNo = TextBoxNo + 1;
                            if (CompInEvRec.nanarirod > 0)
                            {
                                AddTextBox(NoOfLines, TextBoxNo, Convert.ToString(CompInEvRec.nanarirod), "60px", "right", (NoOfLines * 10) + 1, false, false);
                            }
                            else
                            {
                                AddTextBox(NoOfLines, TextBoxNo, "", "60px", "right", (NoOfLines * 10) + 1, false, false);
                            }
                            LabelNo = LabelNo + 1;
                            AddLabelWithColorRed(LabelNo, CompInEvRec.PerformaceRemarks, "40px", "right");

                        }
                    }
                }
                else
                {
                    if (EventType == "HJ/PV")
                    {
                        Athl_HeightsInHJandPV AthlHeightsInHJandPVRec = new Athl_HeightsInHJandPV();
                        AthlHeightsInHJandPVRec = AthlCRUD.GetHeightsInHJandPVRec(CompCode, EventNo, CompInEvRec.rasnumer);

                        LabelNo = LabelNo + 1;
                        //AddLabel(LabelNo, CompInEvRec.rasnumer.ToString(), "50px", "right");  //Rásn
                        AddLabelForBibNo(LabelNo, Convert.ToString(CompInEvRec.rasnumer), "50px", "right");

                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "10px", "");
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.nafn.Length > 22)
                        {
                            AddLabel(LabelNo, CompInEvRec.nafn.Substring(0, 22), "210px", "");
                        }
                        else
                        {
                            AddLabel(LabelNo, CompInEvRec.nafn, "210px", "");
                        }
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.faedingarar.ToString(), "50px", "");    //F.ár
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.felag, "80px", "");   //Félag
                        TextBoxNo = TextBoxNo + 1;
                        AddTextBoxForHeatLaneEtc(NoOfLines, TextBoxNo, CompInEvRec.stokkkastrod.ToString(), "20px", "right");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "5px", "");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.arangur, "40px", "right"); //Árangur
                        LabelNo = LabelNo + 1;
                        AddLabelWithColorRed(LabelNo, CompInEvRec.PerformaceRemarks, "30px", "right");
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.urslitarod < 9999)
                        {
                            AddLabel(LabelNo, CompInEvRec.urslitarodtexti, "20px", "right");  //Úrsl 
                        }
                        else
                        {
                            AddLabel(LabelNo, "", "20px", "right");
                        }
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "5px", "right");

                        AddToTabIndex = 0;

                        for (int ixx = 1; ixx <= 25; ixx++)
                        {
                            if (SelectTabOrder.SelectedIndex == 1) //Tab down the column
                            {
                                AddToTabIndex = ixx * 1000;
                            }

                            string CurrentHeight = "";
                            switch (ixx)
                            {
                                case 1:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C1__hæð;
                                    break;
                                case 2:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C2__hæð;
                                    break;
                                case 3:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C3__hæð;
                                    break;
                                case 4:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C4__hæð;
                                    break;
                                case 5:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C5__hæð;
                                    break;
                                case 6:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C6__hæð;
                                    break;
                                case 7:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C7__hæð;
                                    break;
                                case 8:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C8__hæð;
                                    break;
                                case 9:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C9__hæð;
                                    break;
                                case 10:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C10__hæð;
                                    break;
                                case 11:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C11__hæð;
                                    break;
                                case 12:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C12__hæð;
                                    break;
                                case 13:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C13__hæð;
                                    break;
                                case 14:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C14__hæð;
                                    break;
                                case 15:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C15__hæð;
                                    break;
                                case 16:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C16__hæð;
                                    break;
                                case 17:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C17__hæð;
                                    break;
                                case 18:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C18__hæð;
                                    break;
                                case 19:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C19__hæð;
                                    break;
                                case 20:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C20__hæð;
                                    break;
                                case 21:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C21__hæð;
                                    break;
                                case 22:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C22__hæð;
                                    break;
                                case 23:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C23__hæð;
                                    break;
                                case 24:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C24__hæð;
                                    break;
                                case 25:
                                    CurrentHeight = AthlHeightsInHJandPVRec.C25__hæð;
                                    break;
                            }

                            LabelNo = LabelNo + 1;
                            AddLabel(LabelNo, "", "2px", "");
                            if (AthlHeightsInHJandPVRec.Rásnúmer > 0)
                            {
                                AddTextBox(NoOfLines, ixx, CurrentHeight, "24px", "right", (NoOfLines + AddToTabIndex), false, false);
                            }
                            else
                            {
                                AddTextBox(NoOfLines, ixx, CurrentHeight, "24px", "right", (NoOfLines + AddToTabIndex), false, false);
                            }
                        }
                    }
                    if ((EventType == "TECHNICAL") || (EventType == "MULTIEVENT"))
                    {

                        // AddLabel(LabelNo, Convert.ToString(CompInEvRec.rasnumer), "50px", "right");
                        AddLabelForBibNo(LabelNo, Convert.ToString(CompInEvRec.rasnumer), "50px", "right");

                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "10px", "");
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.nafn.Length > 22)
                        {
                            AddLabel(LabelNo, CompInEvRec.nafn.Substring(0, 22), "210px", "");
                        }
                        else
                        {
                            AddLabel(LabelNo, CompInEvRec.nafn, "210px", "");
                        }
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, Convert.ToString(CompInEvRec.faedingarar), "50px", "");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.felag, "80px", "");
                        TextBoxNo = TextBoxNo + 1;
                        AddTextBoxForHeatLaneEtc(NoOfLines, TextBoxNo, Convert.ToString(CompInEvRec.stokkkastrod), "50px", "right");
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.arangur == "")
                        {
                            AddLabel(LabelNo, CompInEvRec.athugasemd, "45px", "right");
                        }
                        else
                        {
                            AddLabel(LabelNo, CompInEvRec.arangur, "45px", "right");
                        }
                        LabelNo = LabelNo + 1;
                        AddLabelWithColorRed(LabelNo, CompInEvRec.PerformaceRemarks, "35px", "right");
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.urslitarod < 10000)
                        {
                            AddLabel(LabelNo, CompInEvRec.urslitarodtexti, "100px", "right");
                        }
                        else
                        {
                            AddLabel(LabelNo, "", "100px", "right");
                        }
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "15px", "right");
                        AddToTabIndex = 0;
                        for (int ixx = 1; ixx <= NoOfAttempts; ixx++)
                        {
                            if (SelectTabOrder.SelectedIndex == 1) //Tab down the column
                            {
                                AddToTabIndex = ixx * 1000;
                            }
                            switch (ixx)
                            {
                                case 1:
                                    if (CompInEvRec.tilraun1 == 0)
                                    {
                                        CurrRoundValue = "";
                                        CurrWindValue = "";
                                        CurrRoundValue = ReturnMerking(CompInEvRec.merking1);
                                    }
                                    else
                                    {
                                        CurrRoundValueDecimal = (decimal)CompInEvRec.tilraun1;
                                        CurrRoundValue = CurrRoundValueDecimal.ToString("0.00");
                                        CurrWindValue = gl.FormatWind(Convert.ToDecimal(CompInEvRec.vindur1));
                                    }
                                    break;
                                case 2:
                                    if (CompInEvRec.tilraun2 == 0)
                                    {
                                        CurrRoundValue = "";
                                        CurrWindValue = "";
                                        CurrRoundValue = ReturnMerking(CompInEvRec.merking2);
                                    }
                                    else
                                    {
                                        CurrRoundValueDecimal = (decimal)CompInEvRec.tilraun2;
                                        CurrRoundValue = CurrRoundValueDecimal.ToString("0.00");
                                        CurrWindValue = gl.FormatWind(Convert.ToDecimal(CompInEvRec.vindur2));
                                    }
                                    break;
                                case 3:
                                    if (CompInEvRec.tilraun3 == 0)
                                    {
                                        CurrRoundValue = "";
                                        CurrWindValue = "";
                                        CurrRoundValue = ReturnMerking(CompInEvRec.merking3);
                                    }
                                    else
                                    {
                                        CurrRoundValueDecimal = (decimal)CompInEvRec.tilraun3;
                                        CurrRoundValue = CurrRoundValueDecimal.ToString("0.00");
                                        CurrWindValue = gl.FormatWind(Convert.ToDecimal(CompInEvRec.vindur3));
                                    }
                                    break;
                                case 4:
                                    if (CompInEvRec.tilraun4 == 0)
                                    {
                                        CurrRoundValue = "";
                                        CurrWindValue = "";
                                        CurrRoundValue = ReturnMerking(CompInEvRec.merking4);
                                    }
                                    else
                                    {
                                        CurrRoundValueDecimal = (decimal)CompInEvRec.tilraun4;
                                        CurrRoundValue = CurrRoundValueDecimal.ToString("0.00");
                                        CurrWindValue = gl.FormatWind(Convert.ToDecimal(CompInEvRec.vindur4));
                                    }
                                    break;
                                case 5:
                                    if (CompInEvRec.tilraun5 == 0)
                                    {
                                        CurrRoundValue = "";
                                        CurrWindValue = "";
                                        CurrRoundValue = ReturnMerking(CompInEvRec.merking5);
                                    }
                                    else
                                    {
                                        CurrRoundValueDecimal = (decimal)CompInEvRec.tilraun5;
                                        CurrRoundValue = CurrRoundValueDecimal.ToString("0.00");
                                        CurrWindValue = gl.FormatWind(Convert.ToDecimal(CompInEvRec.vindur5));
                                    }
                                    break;
                                case 6:
                                    if (CompInEvRec.tilraun6 == 0)
                                    {
                                        CurrRoundValue = "";
                                        CurrWindValue = "";
                                        CurrRoundValue = ReturnMerking(CompInEvRec.merking6);
                                    }
                                    else
                                    {
                                        CurrRoundValueDecimal = (decimal)CompInEvRec.tilraun6;
                                        CurrRoundValue = CurrRoundValueDecimal.ToString("0.00");
                                        CurrWindValue = gl.FormatWind(Convert.ToDecimal(CompInEvRec.vindur6));
                                    }
                                    break;
                            }
                            TextBoxNo = TextBoxNo + 1;
                            if (CompInEvRec.rasnumer > 0)
                            {
                                AddTextBox(NoOfLines, TextBoxNo, CurrRoundValue, "60px", "right", (NoOfLines + AddToTabIndex), false, false);
                                if (WindMetered == true)
                                {
                                    TextBoxNo = TextBoxNo + 1;
                                    AddTextBox(NoOfLines, TextBoxNo, CurrWindValue, "40px", "right", (NoOfLines + AddToTabIndex), false, false);
                                }
                            }
                            else
                            {
                                AddTextBox(NoOfLines, TextBoxNo, CurrRoundValue, "60px", "right", (NoOfLines + AddToTabIndex), false, true);
                                if (WindMetered == true)
                                {
                                    TextBoxNo = TextBoxNo + 1;
                                    AddTextBox(NoOfLines, TextBoxNo, CurrWindValue, "40px", "right", (NoOfLines + AddToTabIndex), false, true);
                                }

                            }
                        }
                    }

                }
                AddTextBoxForEndOfLine(NoOfLines);
                PH1.Controls.Add(new LiteralControl("<br />"));
            }
            PH1.Controls.Add(new LiteralControl("<br />"));
            PH1.Controls.Add(new LiteralControl("<br />"));
            switch (EventType)
            {
                case "TRACK":
                    PH1.Controls.Add(new LiteralControl("Í reitina þarf að slá inn tíma hlauparans og munið að setja kommu (,) en ekki punkt á sekúndubrotunum.<br />"));
                    PH1.Controls.Add(new LiteralControl("Það þarf ekki að slá inn tvípuknt (:) á eftir mínútum. Sem dæmi má slá inn <b>210,17 </b>fyrir tímann <b>2:10,17</b>.<br />"));
                    PH1.Controls.Add(new LiteralControl("Ef keppandi mætti ekki til leiks skal slá inn í dálkinn hans DNS, ef hann kláraði ekki þá DNF og ef hann var dæmdur úr leik þá DQ.<br />"));
                    break;
                case "TECHNICAL":
                    PH1.Controls.Add(new LiteralControl("Í reitina þarf að slá inn árangur og munið að setja kommu (,) en ekki punkt á milli metra og sentimetra.<br />"));
                    PH1.Controls.Add(new LiteralControl("Ef keppandi sleppir skal slá inn S (eða P) í dálkinn fyrir umferðina og ef um ógilt stökk eða kast er að ræða skal slá inn X.<br /><br />"));
                    PH1.Controls.Add(new LiteralControl("Ef keppandi mætti ekki til leiks skal slá inn í fyrsta dálkinn hans DNS, ef hann var dæmdur úr leik þá DQ.<br />"));
                    break;
                case "HJ/PV":
                    PH1.Controls.Add(new LiteralControl("Í reitina fyrir neðan hverja hæð þarf að skrá inn tilraunir stökkvarans. Ef stökkið tókst þá skal skrá o (lítill bókstafur O) en x ef um fall var að ræða.<br />"));
                    PH1.Controls.Add(new LiteralControl("Ef keppandi sleppir tilraun skal slá inn bandstrik (-). Hver keppandi fær mest þrár tilraunir við hverja hæð og þess vegna mega mest vera þrjú tákn í hverjum reit.<br />"));
                    PH1.Controls.Add(new LiteralControl("Ef keppandi mætti ekki til leiks setur kerfið sjálft inn DNS. Ef keppandi felldi byrjunarhæð sína setur kerfið inn NH.<br />"));
                    break;

            }
            gl.SetNoOfArrayElements(NoOfLines);

        }

        protected void AddLabel(Int32 LblNo, string LabelText, string LabelWidth, string AlignText)
        {
            Label Lbl = new Label();
            Lbl.ID = "Lbl" + Convert.ToString(LblNo);
            Lbl.Width = new System.Web.UI.WebControls.Unit(LabelWidth);
            if (AlignText != "")
            {
                Lbl.Style["text-align"] = AlignText; //right, left
            }
            Lbl.Text = LabelText;
            PH1.Controls.Add(Lbl);
        }

        protected void AddLabelForBibNo(Int32 LblNo, string BibNoText, string LabelWidth, string AlignText)
        {
            Label Lbl = new Label();
            Lbl.ID = "BibNo" + Convert.ToString(LblNo);
            Lbl.Width = new System.Web.UI.WebControls.Unit(LabelWidth);
            if (AlignText != "")
            {
                Lbl.Style["text-align"] = AlignText; //right, left
            }
            Lbl.Text = BibNoText;
            PH1.Controls.Add(Lbl);

        }

        protected void AddLabelWithColorRed(Int32 LblNo, string LabelText, string LabelWidth, string AlignText)
        {
            Label Lbl = new Label();
            Lbl.ID = "Lbl" + Convert.ToString(LblNo);
            Lbl.Width = new System.Web.UI.WebControls.Unit(LabelWidth);
            if (AlignText != "")
            {
                Lbl.Style["text-align"] = AlignText; //right, left
            }
            Lbl.ForeColor = Color.Red;
            Lbl.Font.Bold = true;
            Lbl.Text = LabelText;
            PH1.Controls.Add(Lbl);
        }

        protected void AddTextBox(Int32 LineNo, Int32 TxtBoxNo, string TextBoxText, string TextBoxWidth,
            string AlignText, Int32 TabIndx, bool WindTextBox, bool SetReadOnly)
        {
            string TextBoxHeatNo = "";
            if (HeatNumberFilter != "%")
            {
                TextBoxHeatNo = "_" + HeatNumberFilter;
            }
            TextBox Txb = new TextBox();
            if (WindTextBox == true)
            {
                Txb.ID = "Wind_" + Convert.ToString(LineNo) + "_" + Convert.ToString(TxtBoxNo) + TextBoxHeatNo;
            }
            else
            {
                Txb.ID = "Txb_" + Convert.ToString(LineNo) + "_" + Convert.ToString(TxtBoxNo) + TextBoxHeatNo;
            }
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
            PH1.Controls.Add(Txb);
        }

        protected void AddTextBoxForHeatLaneEtc(Int32 LineNo, Int32 TxtBoxHeatLaneNo, string TextBoxHeatLaneText,
            string TextBoxHeatLaneWidth, string AlignText)
        {
            string TextBoxHeatNo = "";
            if (HeatNumberFilter != "%")
            {
                TextBoxHeatNo = "_" + HeatNumberFilter;
            }
            TextBox TxbHeatLane = new TextBox();
            TxbHeatLane.ID = "HeatLaneOrder_" + Convert.ToString(LineNo) + "_" + Convert.ToString(TxtBoxHeatLaneNo) + TextBoxHeatNo;
            TxbHeatLane.Width = new System.Web.UI.WebControls.Unit(TextBoxHeatLaneWidth);
            if (AlignText != "")
            {
                TxbHeatLane.Style["text-align"] = AlignText; //right, left
            }
            TxbHeatLane.Text = TextBoxHeatLaneText;
            PH1.Controls.Add(TxbHeatLane);
        }

        protected void AddTextBoxForEndOfLine(Int32 LineNo)
        {
            TextBox TxbEOL = new TextBox();
            TxbEOL.ID = "EndOfLine" + Convert.ToString(LineNo);
            TxbEOL.Text = "EOL " + LineNo.ToString();
            TxbEOL.Visible = false;
            PH1.Controls.Add(TxbEOL);
        }

        protected void AddCheckBox(Int32 LineNo, Int32 ChkBoxNo, bool CheckBoxValue, string CheckBoxWidth)
        {
            CheckBox Chkb = new CheckBox();
            Chkb.ID = "Chkb_" + Convert.ToString(LineNo) + "_" + Convert.ToString(ChkBoxNo);
            Chkb.Width = new System.Web.UI.WebControls.Unit(CheckBoxWidth);
            Chkb.Checked = CheckBoxValue;
            Chkb.Enabled = false;
            PH1.Controls.Add(Chkb);
        }

        protected void AddEditableCheckBox(Int32 BibNo, Int32 LinNo, bool CheckBoxValue, string CheckBoxWidth)
        {
            CheckBox Chkb = new CheckBox();
            Chkb.ID = "Del_Competitor" + Convert.ToString(LinNo) + "_" + Convert.ToString(BibNo);
            Chkb.Width = new System.Web.UI.WebControls.Unit(CheckBoxWidth);
            Chkb.Checked = CheckBoxValue;
            Chkb.Enabled = true;
            Chkb.Style["text-align"] = "center";
            PH1.Controls.Add(Chkb);
        }

        protected void SaveValues_Click(object sender, EventArgs e)
        {
            UpdateDatabase();
        }

        protected void UpdateDatabase()
        {
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Global gl = new Global();
            Athl_CompetitorsInEvent AthlCompInComp = new Athl_CompetitorsInEvent();
            Athl_CompetitionEvents AthlCompEventRec = new Athl_CompetitionEvents();
            Athl_Competition AthlCompetitionRec = new Athl_Competition();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            string CompCode = gl.GetCompetitionCode();
            //string EventLineNo = gl.GetCompetitionEventNo();
            string EventLineNo = Request.QueryString.Get("Event");
            Int32 EventLin = Convert.ToInt32(EventLineNo);
            AthlCompEventRec = AthlCRUD.GetCompetitionEvent(CompCode, EventLin);
            AthlCompetitionRec = AthlCRUD.GetCompetitionRec(CompCode);
            Int32 FjLin = gl.GetNoOfArrayElements();
            string EventType = gl.GetEventType();
            string CurrValue = "";
            Int32 NewHeatNo = 0;
            Int32 NewLaneOrderNo = 0;
            bool res = false;
            bool NeedToDeleteCompetitor;
            string TextBoxID;
            string CheckBoxID;
            bool ElectricalTiming = false;
            string[] TextBoxParts = new string[10];
            string[] TextBoxValues = new string[50];
            string[] TextBoxIDs = new string[50];
            Int32 LineNo = -1;
            Int32 TextBoxNo;
            Int32 BibNo = 0;
            bool NeedToUpdateResultOrder = false;
            //NeedToSortByHeatAndLanes = false;

            string NewEventStatus = Convert.ToString(EventStatusDropDown.SelectedValue);
            if (NewEventStatus != SavedEventStatus.Text)
            {
                AthlEnt.UpdateEventStatus(CompCode, EventLin, Convert.ToInt32(NewEventStatus));
                string CurrUsr = gl.GetCurrUsrName();
                AthlEnt.InsertToLogFile(CurrUsr, "Lin=" + EventLin.ToString(), CompCode, "EVSTATUS",
                    "NEW=" + NewEventStatus.ToString());
                SavedEventStatus.Text = NewEventStatus;
                if ((NewEventStatus == "2") && (AthlCompEventRec.thrautargrein == 1) && (AthlCompEventRec.nanaritegundargreining != 7))
                {
                    AthlEnt.ProcessMultiEventCompetitors(CompCode, Convert.ToInt32(EventLineNo));
                    Int32 LineNoOfMultiEv = 0;
                    System.Data.Objects.ObjectParameter LineNoOfMultiEvParameter = new System.Data.Objects.ObjectParameter("LineNoOfMultiEvent", "0");
                    //AthlEntities.ReturnNoOfHeatsInEvent(CompCode, EventLin, OutNoOfHeats);
                    AthlEnt.ReturnLineNumberOfMultiEvent(CompCode, Convert.ToInt32(EventLineNo), LineNoOfMultiEvParameter);
                    LineNoOfMultiEv = Convert.ToInt32(LineNoOfMultiEvParameter.Value);
                    AthlEnt.UpdateOrderAndPointsByResults(CompCode, LineNoOfMultiEv, 2);
                }
                else
                    if (NewEventStatus == "2")
                {
                    AthlEnt.UpdateOrderAndPointsByResults(CompCode, Convert.ToInt32(EventLineNo), AthlCompEventRec.tegundgreinar);
                }
                AthlEnt.CalculatePointsInEvent(CompCode, Convert.ToInt32(EventLineNo));
            }

            for (int ixx = 0; ixx < 40; ixx++)
            {
                TextBoxValues[ixx] = "";
                TextBoxIDs[ixx] = "";
            }
            NeedToDeleteCompetitor = false;
            foreach (Control item in PH1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox t1 = (TextBox)PH1.FindControl(item.ID);
                    TextBoxID = t1.ID + "___XXXX";
                    if (TextBoxID.Substring(0, 9) == "EndOfLine")
                    {
                        if ((BibNo > 0) || ((BibNo == 0) && (EventType == "HJ/PV")))
                        {
                            if (NeedToDeleteCompetitor == false)
                            {
                                if (UpdateRecord(BibNo, TextBoxValues, TextBoxIDs, ElectricalTiming, NewHeatNo, NewLaneOrderNo) == true)
                                {
                                    NeedToUpdateResultOrder = true;
                                }
                            }
                        }
                        NewHeatNo = -2;
                        NewLaneOrderNo = 0;
                        for (int ixx = 0; ixx < 30; ixx++)
                        {
                            TextBoxValues[ixx] = "";
                            TextBoxIDs[ixx] = "";
                        }
                        BibNo = 0;
                    }
                    if (TextBoxID.Substring(0, 3) == "Hea")
                    {
                        CurrValue = t1.Text;
                        TextBoxParts = TextBoxID.Split('_');
                        LineNo = Convert.ToInt32(TextBoxParts[1]);
                        //BibNo = gl.GetBibNoArrayElement(LineNo);
                        TextBoxNo = Convert.ToInt32(TextBoxParts[2]);
                        if (TextBoxNo == 1)
                        {
                            if (EventType == "TRACK")
                            {
                                res = Int32.TryParse(CurrValue, out NewHeatNo);
                                if (res == false)
                                {
                                    NewHeatNo = 0;
                                }
                            }
                            else
                            {
                                NewHeatNo = -2;
                                res = Int32.TryParse(CurrValue, out NewLaneOrderNo);
                                if (res == false)
                                {
                                    NewLaneOrderNo = 0;
                                }
                            }
                        }
                        if (TextBoxNo == 2)
                        {
                            res = Int32.TryParse(CurrValue, out NewLaneOrderNo);
                            if (res == false)
                            {
                                NewLaneOrderNo = 0;
                            }
                        }
                    }
                    else
                    {
                        if ((TextBoxID.Substring(0, 3) == "Txb") || (TextBoxID.Substring(0, 3) == "Win"))
                        {
                            CurrValue = t1.Text;
                            TextBoxParts = TextBoxID.Split('_');
                            LineNo = Convert.ToInt32(TextBoxParts[1]);
                            // BibNo = gl.GetBibNoArrayElement(LineNo);

                            TextBoxNo = Convert.ToInt32(TextBoxParts[2]);
                            TextBoxValues[TextBoxNo] = CurrValue;
                            TextBoxIDs[TextBoxNo] = TextBoxID;
                        }
                    }
                }
                if (item is CheckBox)
                {
                    CheckBox C1 = (CheckBox)PH1.FindControl(item.ID);
                    CheckBoxID = C1.ID;
                    if (CheckBoxID.Substring(0, 4) == "Del_")  //"Del_Competitor_108 for BibNo 108
                    {
                        if (C1.Checked)
                        {
                            //string DebugTxt = CheckBoxID.Length.ToString();
                            //DebugTxt = DebugTxt + " - " + CheckBoxID.Substring(15, (CheckBoxID.Length - 15));
                            TextBoxParts = CheckBoxID.Split('_');
                            //BibNo = Convert.ToInt32(CheckBoxID.Substring(15, (CheckBoxID.Length - 15)));
                            // BibNo = Convert.ToInt32(TextBoxParts[2]);
                            // AthlEnt.DeleteCompetitorInEvent(CompCode, Convert.ToInt32(EventLineNo), BibNo);
                            NeedToDeleteCompetitor = true;
                            NeedToUpdateResultOrder = true;
                        }
                    }
                }
                if (item is Label)
                {
                    Label Lbl1 = (Label)PH1.FindControl(item.ID);
                    string LableID = Lbl1.ID;
                    if (LableID.Substring(0, 3) == "Bib") //Label for BibNo
                    {
                        BibNo = Convert.ToInt32(Lbl1.Text.ToString());
                        if (NeedToDeleteCompetitor == true)
                        {
                            AthlEnt.DeleteCompetitorInEvent(CompCode, Convert.ToInt32(EventLineNo), BibNo);
                            NeedToDeleteCompetitor = false;
                        }
                    }
                }
            }
            if (NeedToUpdateResultOrder == true)
            {
                Int32 EventTypeInteger = 0;
                if (EventType == "TRACK")
                {
                    EventTypeInteger = 1;
                }
                else
                {
                    EventTypeInteger = 2;
                }
                AthlEnt.UpdateOrderAndPointsByResults(CompCode, Convert.ToInt32(EventLineNo), EventTypeInteger);

                if ((AthlCompEventRec.thrautargrein == 1) && (AthlCompEventRec.nanaritegundargreining != 7))
                {
                    AthlEnt.ProcessMultiEventCompetitors(CompCode, Convert.ToInt32(EventLineNo));
                    Int32 LineNoOfMultiEv = 0;
                    System.Data.Objects.ObjectParameter LineNoOfMultiEvParameter = new System.Data.Objects.ObjectParameter("LineNoOfMultiEvent", "0");
                    //AthlEntities.ReturnNoOfHeatsInEvent(CompCode, EventLin, OutNoOfHeats);
                    AthlEnt.ReturnLineNumberOfMultiEvent(CompCode, Convert.ToInt32(EventLineNo), LineNoOfMultiEvParameter);
                    LineNoOfMultiEv = Convert.ToInt32(LineNoOfMultiEvParameter.Value);
                    AthlEnt.UpdateOrderAndPointsByResults(CompCode, LineNoOfMultiEv, 2);
                }
                if ((AthlCompEventRec.stigagrein == 1) && (AthlCompetitionRec.tegundstigakeppni == 5))
                {
                    AthlEnt.CalculatePointsInEvent(CompCode, Convert.ToInt32(EventLineNo));
                }

                string HeatNoFilt;
                HeatNoFilt = CurrPageAndHeat.Text;
                if ((HeatNoFilt != "0") && (HeatNoFilt != "%"))
                {
                    Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNo + "&HeatNoFilter=" + HeatNoFilt);
                }
                else
                {
                    Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNo + "&HeatNoFilter=" + HeatNoFilt);

                    //                    Response.Redirect("UpdateEventResults.aspx");
                }

            }

        }

        private string ReturnMerking(Int32 Merking)
        {
            string ReturnValue = "";
            switch (Merking)
            {
                case 1:
                    ReturnValue = "P";
                    break;
                case 2:
                    ReturnValue = "X";
                    break;
                case 3:
                    ReturnValue = "DNS";
                    break;
                case 4:
                    ReturnValue = "DNF";
                    break;
                case 5:
                    ReturnValue = "DQ";
                    break;
                case 6:
                    ReturnValue = "FS";
                    break;
            }
            return ReturnValue;
        }

        private bool UpdateRecord(Int32 BibNo, string[] TextBoxValues, string[] TextBoxIDs, bool ElectricalTiming, Int32 NewHeatNo, Int32 NewLaneNo)
        {
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitorsInEvent AthlCompetitorsInEventRec = new Athl_CompetitorsInEvent();
            Athl_HeightsInHJandPV AthlHeightsInCurrentHJandPVRec = new Athl_HeightsInHJandPV();
            Athl_HeightsInHJandPV AthlHeightsForCompetitor = new Athl_HeightsInHJandPV();
            Athl_CompetitionEvents AthlCompEvent = new Athl_CompetitionEvents();
            Athl_Events AthlEventRec = new Athl_Events();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Global gl = new Global();
            string EventType;
            bool WindMetered;
            string CompCode;
            Int32 CompetitionEventLineNo;
            bool NeedToUpdate = false;
            decimal CurrDecimalValue = 0;
            decimal WindInHeat = 0;
            decimal BestResult = 0;
            decimal BestWind = 0;
            string AlphaCharacters;
            decimal SortOrder1 = 0;
            decimal SortOrder2 = 0;
            //Int32 ElTimeInt = 0;
            //Boolean res = false;
            string UrslitaRodTexti = "";
            Int32 SamaSaetiOgNaestiAUndan = 0;
            Int32 TotNoOfRounds;
            Int32 IAAFPts = 0;

            EventType = gl.GetEventType();
            WindMetered = gl.GetWindMetered();
            CompCode = gl.GetCompetitionCode();
            //CompetitionEventLineNo = Convert.ToInt32(gl.GetCompetitionEventNo());
            string EventLineNo = Request.QueryString.Get("Event");
            CompetitionEventLineNo = Convert.ToInt32(EventLineNo);
            AthlCompEvent = AthlCRUD.GetCompetitionEvent(CompCode, CompetitionEventLineNo);

            Int32 OutDoorsOrIndoors = ConvertStringToInt32(gl.GetOutdorrsOrIndoors());
            //string OutdoorsOrIndoorsString = gl.GetOutdorrsOrIndoors();

            //// OutDoorsOrIndoors = Convert.ToInt32(gl.GetOutdorrsOrIndoors());  XXXXX
            //bool WasOk = Int32.TryParse(OutdoorsOrIndoorsString, out OutDoorsOrIndoors);
            //if (WasOk == false)
            //{
            //    OutDoorsOrIndoors = 1;
            //}

            AthlEventRec = AthlCRUD.GetAthlEvent(AthlCompEvent.grein, AthlCompEvent.kyn, AthlCompEvent.flokkur,
                //Convert.ToInt32(gl.GetOutdorrsOrIndoors()));
                OutDoorsOrIndoors);

            //gl.SetEventTypeInteger(AthlCompEvent.tegundgreinar); Ekki nota þetta lengur
            TotNoOfRounds = AthlCompEvent.fjoldiumferda;

            if ((BibNo == 0) && (EventType == "HJ/PV"))
            {
                AthlHeightsInCurrentHJandPVRec = CheckHJPVHeights(CompCode, CompetitionEventLineNo, -1, TextBoxValues);
                gl.SetCurrentHeightsForHJorPV(AthlHeightsInCurrentHJandPVRec);
                return false;
            }
            AthlCompetitorsInEventRec = AthlCRUD.GetCompetitorInEvent(CompCode, BibNo, CompetitionEventLineNo);
            //if ((NewHeatNo != AthlCompetitorsInEventRec.ridillnumer) || (NewLaneNo != AthlCompetitorsInEventRec.stokkkastrod))
            //{
            //    NeedToSortByHeatAndLanes = true;
            //}
            if (AthlCompetitorsInEventRec == null)
            {
                return false;
            }
            if (AthlCompetitorsInEventRec.rasnumer.Equals(null))
            {
                return false;
            }
            ElectricalTiming = Convert.ToBoolean(AthlCompetitorsInEventRec.rafmagnstimataka);
            if (WindMetered == true)
            {
                switch (EventType)
                {
                    case "TRACK":

                        ElectrTiming = AthlCompetitorsInEventRec.rafmagnstimataka;

                        if (TextBoxIDs[3].Substring(0, 3) == "Win")
                        {
                            WindInHeat = gl.ReturnEnteredWind(TextBoxValues[3], out AlphaCharacters);
                            gl.SetWindInHeat(WindInHeat);
                            AthlEnt.UpdateWindInHeat(CompCode, CompetitionEventLineNo, AthlCompetitorsInEventRec.ridillnumer, WindInHeat);
                            AthlCompetitorsInEventRec = AthlCRUD.GetCompetitorInEvent(CompCode, BibNo, CompetitionEventLineNo);
                            ElectrTiming = IsItElectricalTiming(TextBoxValues[4]);
                            CurrDecimalValue = gl.ReturnNumbersOnly(TextBoxValues[4], out AlphaCharacters);
                        }
                        else
                        {
                            ElectrTiming = IsItElectricalTiming(TextBoxValues[3]);
                            CurrDecimalValue = gl.ReturnNumbersOnly(TextBoxValues[3], out AlphaCharacters); //Convert.ToDecimal(TextBoxValues[1]);
                        }
                        AthlCompetitorsInEventRec.rafmagnstimataka = Convert.ToByte(ElectrTiming);
                        ElectricalTiming = Convert.ToBoolean(AthlCompetitorsInEventRec.rafmagnstimataka);
                        if (AthlCompetitorsInEventRec.rafmagnstimataka == 1)
                        {
                            CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 100)) / 100;
                        }
                        else
                        {
                            CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 10)) / 10;
                        }
                        if (AthlCompetitorsInEventRec.timi != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.timi = CurrDecimalValue;
                            AthlCompetitorsInEventRec.arangur = gl.FormatTimeInTrackRace(CurrDecimalValue, ElectricalTiming); //TextBoxValues[1];
                            NeedToUpdate = true;
                        }
                        if ((AthlCompetitorsInEventRec.ridillnumer != NewHeatNo) || (AthlCompetitorsInEventRec.stokkkastrod != NewLaneNo) || (ElectrTiming != AthlCompetitorsInEventRec.rafmagnstimataka))
                        {
                            NeedToUpdate = true;
                        }
                        AthlCompetitorsInEventRec.seria = "";
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking1 = 0;
                            AthlCompetitorsInEventRec.athugasemd = "";
                            AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "DQ":
                                    AthlCompetitorsInEventRec.merking1 = 5;
                                    AthlCompetitorsInEventRec.athugasemd = "DQ";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "DNF":
                                    AthlCompetitorsInEventRec.merking1 = 4;
                                    AthlCompetitorsInEventRec.athugasemd = "DNF";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "DNS":
                                    AthlCompetitorsInEventRec.merking1 = 3;
                                    AthlCompetitorsInEventRec.athugasemd = "DNS";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "FS":
                                    AthlCompetitorsInEventRec.merking1 = 6;
                                    AthlCompetitorsInEventRec.athugasemd = "FS";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "X":
                                    AthlCompetitorsInEventRec.merking1 = 2;
                                    break;
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking1 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        break;

                    case "TECHNICAL":
                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[2], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);

                        BestResult = CurrDecimalValue;
                        AthlCompetitorsInEventRec.athugasemd = "";
                        AthlCompetitorsInEventRec.seria = "";
                        string Ser = "";
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking1 = 0;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking1 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "DQ":
                                    AthlCompetitorsInEventRec.merking1 = 5;
                                    AthlCompetitorsInEventRec.athugasemd = "DQ";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "DNF":
                                    AthlCompetitorsInEventRec.merking1 = 4;
                                    AthlCompetitorsInEventRec.athugasemd = "DNF";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "DNS":
                                    AthlCompetitorsInEventRec.merking1 = 3;
                                    AthlCompetitorsInEventRec.athugasemd = "DNS";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "FS":
                                    AthlCompetitorsInEventRec.merking1 = 6;
                                    AthlCompetitorsInEventRec.athugasemd = "FS";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "X":
                                    AthlCompetitorsInEventRec.merking1 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking1 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun1 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun1 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        if ((AthlCompetitorsInEventRec.ridillnumer != NewHeatNo) || (AthlCompetitorsInEventRec.stokkkastrod != NewLaneNo))
                        {
                            NeedToUpdate = true;
                        }
                        CurrDecimalValue = gl.ReturnEnteredWind(TextBoxValues[3], out AlphaCharacters);
                        CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 10)) / 10;
                        if (AthlCompetitorsInEventRec.vindur1 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.vindur1 = CurrDecimalValue;
                            NeedToUpdate = true;
                            BestWind = CurrDecimalValue;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, AthlCompetitorsInEventRec.tilraun1, AthlCompetitorsInEventRec.merking1, AthlCompetitorsInEventRec.vindur1, 1, true, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[4], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking2 = 0;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking2 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking2 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking2 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun2 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun2 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        CurrDecimalValue = gl.ReturnEnteredWind(TextBoxValues[5], out AlphaCharacters);
                        CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 10)) / 10;
                        if (AthlCompetitorsInEventRec.vindur2 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.vindur2 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun2 > BestResult)
                        {
                            BestResult = AthlCompetitorsInEventRec.tilraun2;
                            BestWind = AthlCompetitorsInEventRec.vindur2;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, AthlCompetitorsInEventRec.tilraun2, AthlCompetitorsInEventRec.merking2, AthlCompetitorsInEventRec.vindur2, 2, true, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[6], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking3 = 0;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking3 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking3 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking3 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun3 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun3 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        CurrDecimalValue = gl.ReturnEnteredWind(TextBoxValues[7], out AlphaCharacters);
                        CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 10)) / 10;
                        if (AthlCompetitorsInEventRec.vindur3 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.vindur3 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun3 > BestResult)
                        {
                            BestResult = AthlCompetitorsInEventRec.tilraun3;
                            BestWind = AthlCompetitorsInEventRec.vindur3;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, AthlCompetitorsInEventRec.tilraun3, AthlCompetitorsInEventRec.merking3, AthlCompetitorsInEventRec.vindur3, 3, true, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[8], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking4 = 0;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking4 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking4 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking4 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun4 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun4 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        CurrDecimalValue = gl.ReturnEnteredWind(TextBoxValues[9], out AlphaCharacters);
                        CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 10)) / 10;
                        if (AthlCompetitorsInEventRec.vindur4 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.vindur4 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun4 > BestResult)
                        {
                            BestResult = AthlCompetitorsInEventRec.tilraun4;
                            BestWind = AthlCompetitorsInEventRec.vindur4;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, AthlCompetitorsInEventRec.tilraun4, AthlCompetitorsInEventRec.merking4, AthlCompetitorsInEventRec.vindur4, 4, true, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[10], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking5 = 0;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking5 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking5 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking5 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun5 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun5 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        CurrDecimalValue = gl.ReturnEnteredWind(TextBoxValues[11], out AlphaCharacters);
                        CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 10)) / 10;
                        if (AthlCompetitorsInEventRec.vindur5 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.vindur5 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun5 > BestResult)
                        {
                            BestResult = AthlCompetitorsInEventRec.tilraun5;
                            BestWind = AthlCompetitorsInEventRec.vindur5;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, AthlCompetitorsInEventRec.tilraun5, AthlCompetitorsInEventRec.merking5, AthlCompetitorsInEventRec.vindur5, 5, true, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[12], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking6 = 0;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking6 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking6 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking6 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun6 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun6 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        CurrDecimalValue = gl.ReturnEnteredWind(TextBoxValues[13], out AlphaCharacters);
                        CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 10)) / 10;
                        if (AthlCompetitorsInEventRec.vindur6 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.vindur6 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun6 > BestResult)
                        {
                            BestResult = AthlCompetitorsInEventRec.tilraun6;
                            BestWind = AthlCompetitorsInEventRec.vindur6;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, AthlCompetitorsInEventRec.tilraun6, AthlCompetitorsInEventRec.merking6, AthlCompetitorsInEventRec.vindur6, 6, true, TotNoOfRounds);
                        AthlCompetitorsInEventRec.seria = Ser;

                        if (BestResult > 0)
                        {
                            AthlCompetitorsInEventRec.arangur = (BestResult + 0.001M).ToString("#.###");
                            AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.arangur.Substring(0, AthlCompetitorsInEventRec.arangur.Length - 1);
                            AthlCompetitorsInEventRec.metrar = BestResult;
                            AthlCompetitorsInEventRec.vindur = Math.Ceiling((BestWind * 10)) / 10;
                        }
                        else
                        {
                            AthlCompetitorsInEventRec.metrar = 0;
                            AthlCompetitorsInEventRec.vindur = 0;
                        }
                        break;
                }
            }
            else  //Not Wind Metered
            {
                switch (EventType)
                {
                    case "TRACK":
                        if (TextBoxValues[3] == "")
                        {
                            CurrDecimalValue = 0;
                            AlphaCharacters = "";
                        }
                        else
                        {
                            CurrDecimalValue = gl.ReturnNumbersOnly(TextBoxValues[3], out AlphaCharacters); //Convert.ToDecimal(TextBoxValues[1]);
                            if (AthlCompetitorsInEventRec.rafmagnstimataka == 1)
                            {
                                CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 100)) / 100;
                            }
                            else
                            {
                                CurrDecimalValue = Math.Ceiling((CurrDecimalValue * 10)) / 10;
                            }
                        }
                        if (AthlCompetitorsInEventRec.timi != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.timi = CurrDecimalValue;
                            if (CurrDecimalValue == 0)
                            {
                                AthlCompetitorsInEventRec.arangur = "";
                            }
                            else
                            {
                                AthlCompetitorsInEventRec.arangur = gl.FormatTimeInTrackRace(AthlCompetitorsInEventRec.timi, ElectricalTiming);
                            }
                            NeedToUpdate = true;
                        }
                        if ((AthlCompetitorsInEventRec.ridillnumer != NewHeatNo) || (AthlCompetitorsInEventRec.stokkkastrod != NewLaneNo))
                        {
                            NeedToUpdate = true;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking1 = 0;
                            AthlCompetitorsInEventRec.athugasemd = "";
                            AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "DQ":
                                    AthlCompetitorsInEventRec.merking1 = 5;
                                    AthlCompetitorsInEventRec.athugasemd = "DQ";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "DNF":
                                    AthlCompetitorsInEventRec.merking1 = 4;
                                    AthlCompetitorsInEventRec.athugasemd = "DNF";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "DNS":
                                    AthlCompetitorsInEventRec.merking1 = 3;
                                    AthlCompetitorsInEventRec.athugasemd = "DNS";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "FS":
                                    AthlCompetitorsInEventRec.merking1 = 6;
                                    AthlCompetitorsInEventRec.athugasemd = "FS";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "X":
                                    AthlCompetitorsInEventRec.merking1 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking1 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        AthlCompetitorsInEventRec.vindur = gl.GetWindInHeat();
                        if (TextBoxValues[4] == "")
                        {
                            CurrDecimalValue = 0;
                            AlphaCharacters = "";
                        }
                        else
                        {
                            Int32 CloserSortingOrder;
                            CurrDecimalValue = gl.ReturnNumbersOnly(TextBoxValues[4], out AlphaCharacters);
                            CloserSortingOrder = Convert.ToInt32(decimal.Floor(CurrDecimalValue));
                            if (AthlCompetitorsInEventRec.nanarirod != CloserSortingOrder)
                            {
                                AthlCompetitorsInEventRec.nanarirod = CloserSortingOrder;
                                NeedToUpdate = true;
                            }

                        }
                        break;

                    case "TECHNICAL":
                        for (int ixx = 1; ixx <= 13; ixx++)
                        {
                            TextBoxValues[ixx] = TextBoxValues[ixx].Trim();
                            if (TextBoxValues[ixx] == "")
                            {
                                TextBoxValues[ixx] = "0";
                            }
                            if (TextBoxValues[ixx] == "-")
                            {
                                TextBoxValues[ixx] = "P";
                            }
                        }
                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[2], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        BestResult = CurrDecimalValue;
                        AthlCompetitorsInEventRec.athugasemd = "";
                        string Ser = "";
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking1 = 0;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking1 = 0;
                            AthlCompetitorsInEventRec.athugasemd = "";
                            AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "DQ":
                                    AthlCompetitorsInEventRec.merking1 = 5;
                                    AthlCompetitorsInEventRec.athugasemd = "DQ";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "DNF":
                                    AthlCompetitorsInEventRec.merking1 = 4;
                                    AthlCompetitorsInEventRec.athugasemd = "DNF";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "DNS":
                                    AthlCompetitorsInEventRec.merking1 = 3;
                                    AthlCompetitorsInEventRec.athugasemd = "DNS";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "FS":
                                    AthlCompetitorsInEventRec.merking1 = 6;
                                    AthlCompetitorsInEventRec.athugasemd = "FS";
                                    AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.athugasemd;
                                    break;
                                case "X":
                                    AthlCompetitorsInEventRec.merking1 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking1 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun1 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun1 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        if ((AthlCompetitorsInEventRec.ridillnumer != NewHeatNo) || (AthlCompetitorsInEventRec.stokkkastrod != NewLaneNo))
                        {
                            NeedToUpdate = true;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, CurrDecimalValue, AthlCompetitorsInEventRec.merking1, 0, 1, false, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[3], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking2 = 0;
                        }
                        if (CurrDecimalValue > BestResult)
                        {
                            BestResult = CurrDecimalValue;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking2 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking2 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking2 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun2 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun2 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, CurrDecimalValue, AthlCompetitorsInEventRec.merking2, 0, 2, false, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[4], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking3 = 0;
                        }
                        if (CurrDecimalValue > BestResult)
                        {
                            BestResult = CurrDecimalValue;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking3 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking3 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking3 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun3 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun3 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, CurrDecimalValue, AthlCompetitorsInEventRec.merking3, 0, 3, false, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[5], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking4 = 0;
                        }
                        if (CurrDecimalValue > BestResult)
                        {
                            BestResult = CurrDecimalValue;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking4 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking4 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking4 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun4 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun4 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, CurrDecimalValue, AthlCompetitorsInEventRec.merking4, 0, 4, false, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[6], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking5 = 0;
                        }
                        if (CurrDecimalValue > BestResult)
                        {
                            BestResult = CurrDecimalValue;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking5 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking5 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking5 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun5 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun5 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, CurrDecimalValue, AthlCompetitorsInEventRec.merking5, 0, 5, false, TotNoOfRounds);

                        CurrDecimalValue = gl.ReturnNumbersOnlyUnder100(TextBoxValues[7], out AlphaCharacters, AthlEventRec.Hámarkgildi_tæknigreinar);
                        if ((CurrDecimalValue == 0) && (AlphaCharacters == ""))
                        {
                            AthlCompetitorsInEventRec.merking6 = 0;
                        }
                        if (CurrDecimalValue > BestResult)
                        {
                            BestResult = CurrDecimalValue;
                        }
                        if ((CurrDecimalValue == 0) && (AlphaCharacters != ""))
                        {
                            AthlCompetitorsInEventRec.merking6 = 0;

                            // ,P - Sleppir,X - Ógilt,DNS - Mætti ekki,DNF - Kláraði ekki,DQ - Dæmd(ur) úr leik
                            switch (AlphaCharacters.ToUpper())
                            {
                                case "X":
                                    AthlCompetitorsInEventRec.merking6 = 2;
                                    break;
                                case "-":
                                case "P":
                                case "S":
                                    AthlCompetitorsInEventRec.merking6 = 1;
                                    break;
                            }
                            NeedToUpdate = true;
                        }
                        if (AthlCompetitorsInEventRec.tilraun6 != CurrDecimalValue)
                        {
                            AthlCompetitorsInEventRec.tilraun6 = CurrDecimalValue;
                            NeedToUpdate = true;
                        }
                        Ser = BuildSeriesForTechnicalEvent(Ser, CurrDecimalValue, AthlCompetitorsInEventRec.merking6, 0, 6, false, TotNoOfRounds);

                        AthlCompetitorsInEventRec.seria = Ser;
                        if (BestResult > 0)
                        {
                            AthlCompetitorsInEventRec.arangur = (BestResult + 0.001M).ToString("#.###");
                            AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.arangur.Substring(0, AthlCompetitorsInEventRec.arangur.Length - 1);
                            AthlCompetitorsInEventRec.metrar = BestResult;
                        }
                        else
                        {
                            AthlCompetitorsInEventRec.arangur = "";
                            AthlCompetitorsInEventRec.vindur = 0;
                            AthlCompetitorsInEventRec.metrar = 0;
                        }
                        if (AthlCompEvent.nanaritegundargreining == 10) //Kast beggja handa
                        {
                            decimal BothRightAndLeft = 0;
                            decimal LeftRig = 0;
                            if (AthlCompetitorsInEventRec.tilraun1 > AthlCompetitorsInEventRec.tilraun2)
                            {
                                LeftRig = AthlCompetitorsInEventRec.tilraun1;
                            }
                            else
                            {
                                LeftRig = AthlCompetitorsInEventRec.tilraun2;
                            }
                            if (AthlCompetitorsInEventRec.tilraun3 > LeftRig)
                            {
                                LeftRig = AthlCompetitorsInEventRec.tilraun3;
                            }
                            BothRightAndLeft = LeftRig;
                            LeftRig = 0;
                            if (AthlCompetitorsInEventRec.tilraun4 > AthlCompetitorsInEventRec.tilraun5)
                            {
                                LeftRig = AthlCompetitorsInEventRec.tilraun4;
                            }
                            else
                            {
                                LeftRig = AthlCompetitorsInEventRec.tilraun5;
                            }
                            if (AthlCompetitorsInEventRec.tilraun6 > LeftRig)
                            {
                                LeftRig = AthlCompetitorsInEventRec.tilraun6;
                            }
                            BothRightAndLeft = BothRightAndLeft + LeftRig;
                            if (BothRightAndLeft > 0)
                            {
                                AthlCompetitorsInEventRec.arangur = (BothRightAndLeft + 0.001M).ToString("#.###");
                                AthlCompetitorsInEventRec.arangur = AthlCompetitorsInEventRec.arangur.Substring(0, AthlCompetitorsInEventRec.arangur.Length - 1);
                                AthlCompetitorsInEventRec.metrar = BothRightAndLeft;
                            }
                            else
                            {
                                AthlCompetitorsInEventRec.arangur = "";
                                AthlCompetitorsInEventRec.vindur = 0;
                                AthlCompetitorsInEventRec.metrar = 0;
                            }
                        }

                        break;
                    case "HJ/PV":
                        Int32 HeightCleared = 0;
                        Int32 ClearedInInAttemptNo = 0;
                        Int32 TotalNoOfFailures = 0;
                        Int32 NoOfFailures = 0;
                        string OldSeriesText = "";
                        string OldPerformanceText = "";
                        string OldHeightsText = "";
                        string NewHeightsText = "";
                        bool ClearedThisHeight = false;
                        string CurrentHeight = "";
                        AthlHeightsInCurrentHJandPVRec = gl.GetCurrentHeightsForHJorPV();
                        AthlHeightsForCompetitor = AthlCRUD.GetHeightsInHJandPVRec(CompCode, CompetitionEventLineNo, BibNo);
                        OldSeriesText = AthlCompetitorsInEventRec.seria;
                        AthlCompetitorsInEventRec.seria = "";
                        OldPerformanceText = AthlCompetitorsInEventRec.arangur;
                        OldHeightsText = ReturnHeightsText(AthlHeightsForCompetitor);

                        for (int ixx = 1; ixx <= 25; ixx++)
                        {
                            switch (ixx)
                            {
                                case 1:
                                    AthlHeightsForCompetitor.C1__hæð = ValidateHeights(TextBoxValues[1], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C1__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C1__hæð);
                                    break;
                                case 2:
                                    AthlHeightsForCompetitor.C2__hæð = ValidateHeights(TextBoxValues[2], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C2__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C2__hæð);

                                    break;
                                case 3:
                                    AthlHeightsForCompetitor.C3__hæð = ValidateHeights(TextBoxValues[3], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C3__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C3__hæð);
                                    break;
                                case 4:
                                    AthlHeightsForCompetitor.C4__hæð = ValidateHeights(TextBoxValues[4], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C4__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C4__hæð);
                                    break;
                                case 5:
                                    AthlHeightsForCompetitor.C5__hæð = ValidateHeights(TextBoxValues[5], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C5__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C5__hæð);
                                    break;
                                case 6:
                                    AthlHeightsForCompetitor.C6__hæð = ValidateHeights(TextBoxValues[6], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C6__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C6__hæð);
                                    break;
                                case 7:
                                    AthlHeightsForCompetitor.C7__hæð = ValidateHeights(TextBoxValues[7], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C7__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C7__hæð);
                                    break;
                                case 8:
                                    AthlHeightsForCompetitor.C8__hæð = ValidateHeights(TextBoxValues[8], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C8__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C8__hæð);
                                    break;
                                case 9:
                                    AthlHeightsForCompetitor.C9__hæð = ValidateHeights(TextBoxValues[9], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C9__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C9__hæð);
                                    break;
                                case 10:
                                    AthlHeightsForCompetitor.C10__hæð = ValidateHeights(TextBoxValues[10], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C10__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C10__hæð);
                                    break;
                                case 11:
                                    AthlHeightsForCompetitor.C11__hæð = ValidateHeights(TextBoxValues[11], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C11__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C11__hæð);
                                    break;
                                case 12:
                                    AthlHeightsForCompetitor.C12__hæð = ValidateHeights(TextBoxValues[12], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C12__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C12__hæð);
                                    break;
                                case 13:
                                    AthlHeightsForCompetitor.C13__hæð = ValidateHeights(TextBoxValues[13], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C13__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C13__hæð);
                                    break;
                                case 14:
                                    AthlHeightsForCompetitor.C14__hæð = ValidateHeights(TextBoxValues[14], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C14__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C14__hæð);
                                    break;
                                case 15:
                                    AthlHeightsForCompetitor.C15__hæð = ValidateHeights(TextBoxValues[15], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C15__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C15__hæð);
                                    break;
                                case 16:
                                    AthlHeightsForCompetitor.C16__hæð = ValidateHeights(TextBoxValues[16], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C16__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C16__hæð);
                                    break;
                                case 17:
                                    AthlHeightsForCompetitor.C17__hæð = ValidateHeights(TextBoxValues[17], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C17__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C17__hæð);
                                    break;
                                case 18:
                                    AthlHeightsForCompetitor.C18__hæð = ValidateHeights(TextBoxValues[18], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C18__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C18__hæð);
                                    break;
                                case 19:
                                    AthlHeightsForCompetitor.C19__hæð = ValidateHeights(TextBoxValues[19], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C19__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C19__hæð);
                                    break;
                                case 20:
                                    AthlHeightsForCompetitor.C20__hæð = ValidateHeights(TextBoxValues[20], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C20__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C20__hæð);
                                    break;
                                case 21:
                                    AthlHeightsForCompetitor.C21__hæð = ValidateHeights(TextBoxValues[21], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C21__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C21__hæð);
                                    break;
                                case 22:
                                    AthlHeightsForCompetitor.C22__hæð = ValidateHeights(TextBoxValues[22], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C22__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C22__hæð);
                                    break;
                                case 23:
                                    AthlHeightsForCompetitor.C23__hæð = ValidateHeights(TextBoxValues[23], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C23__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C23__hæð);
                                    break;
                                case 24:
                                    AthlHeightsForCompetitor.C24__hæð = ValidateHeights(TextBoxValues[24], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C24__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C24__hæð);
                                    break;
                                case 25:
                                    AthlHeightsForCompetitor.C25__hæð = ValidateHeights(TextBoxValues[25], out ClearedInInAttemptNo,
                                        out NoOfFailures, out ClearedThisHeight);
                                    CurrentHeight = AthlHeightsInCurrentHJandPVRec.C25__hæð;
                                    AthlCompetitorsInEventRec.seria = BuildSeriesForHJandPV(AthlCompetitorsInEventRec.seria, CurrentHeight, AthlHeightsForCompetitor.C25__hæð);
                                    break;
                            }
                            NewHeightsText = ReturnHeightsText(AthlHeightsForCompetitor);
                            if (ClearedThisHeight == true)
                            {
                                HeightCleared = Convert.ToInt32(CurrentHeight);
                                AthlHeightsForCompetitor.Raðsvæði_2__yfir_í_tilraun_ = ClearedInInAttemptNo;
                            }
                            TotalNoOfFailures = TotalNoOfFailures + NoOfFailures;
                        }
                        if (HeightCleared > 0)
                        {
                            AthlHeightsForCompetitor.Árangur = gl.FormatHeightInHJorPV(HeightCleared);
                        }
                        else
                        {
                            if (TotalNoOfFailures > 0)
                            {
                                AthlHeightsForCompetitor.Árangur = "NH";
                                AthlHeightsForCompetitor.Raðsvæði_2__yfir_í_tilraun_ = TotalNoOfFailures;
                            }
                            else
                            {
                                AthlHeightsForCompetitor.Árangur = "DNS";
                                AthlHeightsForCompetitor.Raðsvæði_2__yfir_í_tilraun_ = 5;
                            }
                        }

                        AthlHeightsForCompetitor.Raðsvæði_1__1000___hæð_ = 1000 - HeightCleared;
                        AthlHeightsForCompetitor.Raðsvæði_3__fjöldi_falla_ = TotalNoOfFailures;
                        Int32 CurrLineNo = AthlHeightsForCompetitor.Lína;
                        if (CurrLineNo == 0)
                        {
                            CurrLineNo = BibNo;
                        }
                        if (NewHeightsText != OldHeightsText)
                        {
                            AthlEnt.UpdateHeightsInHJandPV(CompCode, CompetitionEventLineNo, CurrLineNo, BibNo,
                                AthlHeightsForCompetitor.Árangur, 0, 0, 0,
                                AthlHeightsForCompetitor.C1__hæð, AthlHeightsForCompetitor.C2__hæð, AthlHeightsForCompetitor.C3__hæð,
                                AthlHeightsForCompetitor.C4__hæð, AthlHeightsForCompetitor.C5__hæð, AthlHeightsForCompetitor.C6__hæð,
                                AthlHeightsForCompetitor.C7__hæð, AthlHeightsForCompetitor.C8__hæð, AthlHeightsForCompetitor.C9__hæð,
                                AthlHeightsForCompetitor.C10__hæð, AthlHeightsForCompetitor.C11__hæð, AthlHeightsForCompetitor.C12__hæð,
                                AthlHeightsForCompetitor.C13__hæð, AthlHeightsForCompetitor.C14__hæð, AthlHeightsForCompetitor.C15__hæð,
                                AthlHeightsForCompetitor.C16__hæð, AthlHeightsForCompetitor.C17__hæð, AthlHeightsForCompetitor.C18__hæð,
                                AthlHeightsForCompetitor.C19__hæð, AthlHeightsForCompetitor.C20__hæð, AthlHeightsForCompetitor.C21__hæð,
                                AthlHeightsForCompetitor.C22__hæð, AthlHeightsForCompetitor.C23__hæð, AthlHeightsForCompetitor.C24__hæð,
                                AthlHeightsForCompetitor.C25__hæð, AthlHeightsForCompetitor.Raðsvæði_1__1000___hæð_,
                                AthlHeightsForCompetitor.Raðsvæði_2__yfir_í_tilraun_, AthlHeightsForCompetitor.Raðsvæði_3__fjöldi_falla_,
                                AthlHeightsForCompetitor.IAAF_Stig, AthlHeightsForCompetitor.Unglingastig, "", "", "", "", "", "");
                        }
                        if ((AthlCompetitorsInEventRec.seria != OldSeriesText) || (AthlCompetitorsInEventRec.arangur != OldPerformanceText))
                        {
                            AthlCompetitorsInEventRec.arangur = AthlHeightsForCompetitor.Árangur;
                            decimal HeightClearedDecimal = HeightCleared / 100.0m;
                            AthlCompetitorsInEventRec.metrar = HeightClearedDecimal;

                            if (AthlCompEvent.thrautargrein == 1)
                            {
                                System.Data.Objects.ObjectParameter MultiEventPointsParameter;
                                MultiEventPointsParameter = new System.Data.Objects.ObjectParameter("MultiEventPoints", typeof(global::System.Int32));
                                MultiEventPointsParameter.Value = DBNull.Value;

                                Int32 RetVal = 0;
                                RetVal = AthlEnt.CalculateMultiEventPointsFromResult(AthlCompetitorsInEventRec.mot,
                                    AthlCompetitorsInEventRec.greinarnumer, AthlCompetitorsInEventRec.lina, HeightClearedDecimal,
                                    MultiEventPointsParameter);
                                AthlCompetitorsInEventRec.thrautarstig = Convert.ToInt32(MultiEventPointsParameter.Value);
                            }

                            OutDoorsOrIndoors = ConvertStringToInt32(gl.GetOutdorrsOrIndoors());
                            //string OutdoorsOrIndoors4String = gl.GetOutdorrsOrIndoors();

                            //bool WasOk4 = Int32.TryParse(OutdoorsOrIndoors4String, out OutDoorsOrIndoors4);
                            //if (WasOk4 == false)
                            //{
                            //    OutDoorsOrIndoors4 = 1;
                            //}




                            IAAFPts = AthlCRUD.RetIAAFPoints(AthlCompEvent.grein, AthlCompEvent.kyn, AthlCompEvent.flokkur,
                                //Convert.ToInt32(gl.GetOutdorrsOrIndoors()), 
                                OutDoorsOrIndoors,
                                AthlCompetitorsInEventRec.metrar, 0);

                            Int32 YouthPts = new Int32();
                            Int32 AgeToUse = new Int32();
                            AgeToUse = AthlCompEvent.dagsetning.Year - AthlCompetitorsInEventRec.faedingarar;
                            if ((AthlCompEvent.aldurtil > 0) && (AthlCompEvent.aldurtil < 100))
                            {
                                AgeToUse = AthlCompEvent.aldurtil;
                            }
                            //GAMALT: 
                            //YouthPts = AthlCRUD.RetYouthPoints(AthlCompEvent.grein, AthlCompEvent.kyn, AthlCompEvent.flokkur,
                            //  //Convert.ToInt32(gl.GetOutdorrsOrIndoors()), 
                            //  OutDoorsOrIndoors,
                            //  AthlCompetitorsInEventRec.arangur,
                            //  //AthlCompEvent.dagsetning.Year - AthlCompetitorsInEventRec.faedingarar, 
                            //  AgeToUse,
                            //  AthlCompetitorsInEventRec.rafmagnstimataka);

                            System.Data.Objects.ObjectParameter PointsOut = new System.Data.Objects.ObjectParameter("PointsOut", "0");
                            AthlEnt.ReturnUnglingastig(AthlCompEvent.grein, AthlCompEvent.kyn, AthlEventRec.Flokkur,
                                OutDoorsOrIndoors, AgeToUse, AthlCompetitorsInEventRec.arangur,
                                PointsOut);
                            YouthPts = gl.TryConvertStringToInt32(PointsOut.Value.ToString());
                            AthlCompetitorsInEventRec.Unglingastig = YouthPts;

                            AthlCompetitorsInEventRec.PerformaceRemarks = ReturnPerfRemarks(AthlCompetitorsInEventRec.arangur, AthlCompetitorsInEventRec.personulegmet,
                                  AthlCompetitorsInEventRec.bestiaranguriar, AthlCompEvent.krefstvindmaelis, AthlCompetitorsInEventRec.vindur, EventType);

                            AthlEnt.UpdCompetitorInEvent(CompCode, CompetitionEventLineNo, BibNo, AthlCompetitorsInEventRec.timi,
                                HeightClearedDecimal, 0, AthlCompetitorsInEventRec.arangur, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                AthlCompetitorsInEventRec.seria,
                                0, 0, 0, 0, 0, 0, 0, AthlHeightsForCompetitor.Raðsvæði_1__1000___hæð_, AthlHeightsForCompetitor.Raðsvæði_2__yfir_í_tilraun_,
                                AthlHeightsForCompetitor.Raðsvæði_3__fjöldi_falla_, "", 0, "",
                                //NewHeatNo, 
                                AthlCompetitorsInEventRec.ridillnumer,
                                NewLaneNo, IAAFPts, AthlCompetitorsInEventRec.thrautarstig, AthlCompetitorsInEventRec.Unglingastig,
                                AthlCompetitorsInEventRec.PerformaceRemarks);
                        }
                        //NeedToUpdate = true;
                        return true;
                        break;

                    case "MULTIEVENT":
                        EventName.Text = EventName.Text + " EKKI TILBÚIÐ";
                        break;
                }

            }

            if (NeedToUpdate)
            {
                decimal ResultValue = 0;

                if (EventType == "TRACK")
                {
                    if (AthlCompetitorsInEventRec.timi == 0)
                    {
                        SortOrder1 = 999999;
                    }
                    else
                    {
                        SortOrder1 = AthlCompetitorsInEventRec.timi;
                    }

                    ResultValue = AthlCompetitorsInEventRec.timi;
                }
                else
                {
                    if (EventType == "HJ/PV")
                    {
                        SortOrder1 = 1000 - (AthlCompetitorsInEventRec.metrar + AthlHeightsForCompetitor.Raðsvæði_2__yfir_í_tilraun_);
                        SortOrder2 = AthlHeightsForCompetitor.Raðsvæði_3__fjöldi_falla_;
                    }
                    else
                    {
                        decimal[] AttemptsArray = new decimal[6];
                        AttemptsArray[0] = AthlCompetitorsInEventRec.tilraun1 + 1;
                        AttemptsArray[1] = AthlCompetitorsInEventRec.tilraun2 + 1;
                        AttemptsArray[2] = AthlCompetitorsInEventRec.tilraun3 + 1;
                        AttemptsArray[3] = AthlCompetitorsInEventRec.tilraun4 + 1;
                        AttemptsArray[4] = AthlCompetitorsInEventRec.tilraun5 + 1;
                        AttemptsArray[5] = AthlCompetitorsInEventRec.tilraun6 + 1;
                        Array.Sort(AttemptsArray);
                        SortOrder1 = (AttemptsArray[5] * 10000) +
                            AttemptsArray[4] +
                            (AttemptsArray[3] / 10000);
                        SortOrder1 = -SortOrder1;
                        SortOrder2 = (AttemptsArray[2] * 10000) +
                            AttemptsArray[1] +
                            (AttemptsArray[0] / 10000);
                        SortOrder2 = -SortOrder2;

                        ResultValue = AthlCompetitorsInEventRec.metrar;
                    }
                }
                if ((AthlCompetitorsInEventRec.arangur == "0") || (AthlCompetitorsInEventRec.arangur == ".00") || (AthlCompetitorsInEventRec.arangur == ",00"))
                {
                    AthlCompetitorsInEventRec.arangur = "";
                }

                if (AthlEventRec.Tegund_greinar == 1)  //Track Event
                {
                    OutDoorsOrIndoors = ConvertStringToInt32(gl.GetOutdorrsOrIndoors());
                    IAAFPts = AthlCRUD.RetIAAFPoints(AthlEventRec.Grein, AthlEventRec.Kyn, AthlEventRec.Flokkur,
                        //Convert.ToInt32(gl.GetOutdorrsOrIndoors()),
                        OutDoorsOrIndoors,
                        AthlCompetitorsInEventRec.timi, 0);
                }
                else
                {
                    OutDoorsOrIndoors = ConvertStringToInt32(gl.GetOutdorrsOrIndoors());
                    //string OutdoorsOrIndoors2String = gl.GetOutdorrsOrIndoors();

                    //bool WasOk2 = Int32.TryParse(OutdoorsOrIndoors2String, out OutDoorsOrIndoors2);
                    //if (WasOk2 == false)
                    //{
                    //    OutDoorsOrIndoors2 = 1;
                    //}
                    IAAFPts = AthlCRUD.RetIAAFPoints(AthlEventRec.Grein, AthlEventRec.Kyn, AthlEventRec.Flokkur,
                        //Convert.ToInt32(gl.GetOutdorrsOrIndoors()),
                        OutDoorsOrIndoors,
                       0, AthlCompetitorsInEventRec.metrar);
                }
                if (AthlCompEvent.thrautargrein == 1)
                {
                    System.Data.Objects.ObjectParameter MultiEventPointsParameter;
                    MultiEventPointsParameter = new System.Data.Objects.ObjectParameter("MultiEventPoints", typeof(global::System.Int32));
                    MultiEventPointsParameter.Value = DBNull.Value;

                    Int32 RetVal = 0;
                    RetVal = AthlEnt.CalculateMultiEventPointsFromResult(AthlCompetitorsInEventRec.mot,
                        AthlCompetitorsInEventRec.greinarnumer, AthlCompetitorsInEventRec.lina, ResultValue,
                        MultiEventPointsParameter);
                    AthlCompetitorsInEventRec.thrautarstig = Convert.ToInt32(MultiEventPointsParameter.Value);
                }
                if (NewHeatNo == -2)
                {
                    NewHeatNo = AthlCompetitorsInEventRec.ridillnumer;
                }

                //Int32 OutDoorsOrIndoors3 = 0;
                //string OutdoorsOrIndoors3String = gl.GetOutdorrsOrIndoors();

                //bool WasOk3 = Int32.TryParse(OutdoorsOrIndoors3String, out OutDoorsOrIndoors3);
                //if (WasOk3 == false)
                //{
                //    OutDoorsOrIndoors3 = 1;
                //}

                OutDoorsOrIndoors = ConvertStringToInt32(gl.GetOutdorrsOrIndoors());

                Int32 YouthPts = new Int32();
                Int32 AgeToUse = new Int32();
                AgeToUse = AthlCompEvent.dagsetning.Year - AthlCompetitorsInEventRec.faedingarar;
                if ((AthlCompEvent.aldurtil > 0) && (AthlCompEvent.aldurtil < 100))
                {
                    AgeToUse = AthlCompEvent.aldurtil;
                }

                //GAMALT:
                //YouthPts = AthlCRUD.RetYouthPoints(AthlCompEvent.grein, AthlCompEvent.kyn, AthlCompEvent.flokkur,
                //  //Convert.ToInt32(gl.GetOutdorrsOrIndoors()), 
                //  OutDoorsOrIndoors,
                //  AthlCompetitorsInEventRec.arangur,
                //  //AthlCompEvent.dagsetning.Year - AthlCompetitorsInEventRec.faedingarar, 
                //  AgeToUse,
                //  AthlCompetitorsInEventRec.rafmagnstimataka);

                //AthlCompetitorsInEventRec.Unglingastig = YouthPts;

                System.Data.Objects.ObjectParameter PointsOut = new System.Data.Objects.ObjectParameter("PointsOut", "0");
                AthlEnt.ReturnUnglingastig(AthlCompEvent.grein, AthlCompEvent.kyn, AthlEventRec.Flokkur,
                    OutDoorsOrIndoors, AgeToUse, AthlCompetitorsInEventRec.arangur,
                    PointsOut);
                YouthPts = gl.TryConvertStringToInt32(PointsOut.Value.ToString());
                AthlCompetitorsInEventRec.Unglingastig = YouthPts;


                AthlCompetitorsInEventRec.PerformaceRemarks = ReturnPerfRemarks(AthlCompetitorsInEventRec.arangur, AthlCompetitorsInEventRec.personulegmet,
                      AthlCompetitorsInEventRec.bestiaranguriar, AthlCompEvent.krefstvindmaelis, AthlCompetitorsInEventRec.vindur, EventType);

                //  AthlCompetitorsInEventRec.rafmagnstimataka = Convert.ToByte(ElectrTiming);

                AthlCRUD.UpdCompetitorInEvent(AthlCompetitorsInEventRec.mot, AthlCompetitorsInEventRec.greinarnumer,
                    AthlCompetitorsInEventRec.rasnumer, AthlCompetitorsInEventRec.timi, AthlCompetitorsInEventRec.metrar,
                    AthlCompetitorsInEventRec.vindur, AthlCompetitorsInEventRec.arangur,
                    AthlCompetitorsInEventRec.tilraun1, AthlCompetitorsInEventRec.vindur1,
                    AthlCompetitorsInEventRec.tilraun2, AthlCompetitorsInEventRec.vindur2,
                    AthlCompetitorsInEventRec.tilraun3, AthlCompetitorsInEventRec.vindur3,
                    AthlCompetitorsInEventRec.tilraun4, AthlCompetitorsInEventRec.vindur4,
                    AthlCompetitorsInEventRec.tilraun5, AthlCompetitorsInEventRec.vindur5,
                    AthlCompetitorsInEventRec.tilraun6, AthlCompetitorsInEventRec.vindur6,
                    AthlCompetitorsInEventRec.seria,
                    AthlCompetitorsInEventRec.rafmagnstimataka,
                    AthlCompetitorsInEventRec.merking1, AthlCompetitorsInEventRec.merking2,
                    AthlCompetitorsInEventRec.merking3, AthlCompetitorsInEventRec.merking4, AthlCompetitorsInEventRec.merking5,
                    AthlCompetitorsInEventRec.merking6, SortOrder1, SortOrder2, AthlCompetitorsInEventRec.nanarirod, UrslitaRodTexti, SamaSaetiOgNaestiAUndan,
                    AthlCompetitorsInEventRec.athugasemd, NewHeatNo, NewLaneNo, IAAFPts,
                    AthlCompetitorsInEventRec.thrautarstig, AthlCompetitorsInEventRec.Unglingastig, AthlCompetitorsInEventRec.PerformaceRemarks);


                return true;
            }
            return false;
        }

        private Int32 IsItElectricalTiming(string p)
        {
            Int32 LengthOfTxt;
            Int32 PosOfComma;
            Int32 ThisIsElectricalTimingText = 0;

            LengthOfTxt = p.Length;
            if (LengthOfTxt > 1)
            {
                PosOfComma = 0;
                for (int ixx = 0; ixx < LengthOfTxt; ixx++)
                {
                    if (p.Substring(ixx, 1) == ",")
                    {
                        PosOfComma = ixx + 1;
                    }
                }

                if (PosOfComma == (LengthOfTxt - 2))
                {
                    ThisIsElectricalTimingText = 1;
                }
            }

            return (ThisIsElectricalTimingText);
        }

        private string ReturnPerfRemarks(string PerfText, decimal PersBestDecimal, decimal YearBestDecimal, Int32 RequiresWindMetering,
            decimal WindReading, string EventType)
        {

            string PerformaceRemarksOut = "";
            if (PerfText != "")
            {
                decimal CurrentPerformanceDecimal = 0;
                Global gl = new Global();

                CurrentPerformanceDecimal = gl.ParseTextAndReturnDec(PerfText);
                if (EventType == "TRACK")
                {
                    if (((RequiresWindMetering == 1) && (WindReading <= 2)) ||
                     (RequiresWindMetering == 0))
                    {
                        if ((CurrentPerformanceDecimal <= PersBestDecimal) && (PersBestDecimal > 0) && (CurrentPerformanceDecimal > 0))
                        {
                            PerformaceRemarksOut = "Pb.";
                            if (CurrentPerformanceDecimal == PersBestDecimal)
                            {
                                PerformaceRemarksOut = "=Pb.";
                            }
                        }
                        else
                            if ((CurrentPerformanceDecimal <= YearBestDecimal) && (YearBestDecimal > 0) && (CurrentPerformanceDecimal > 0))
                        {
                            PerformaceRemarksOut = "Sb.";
                            if (CurrentPerformanceDecimal == YearBestDecimal)
                            {
                                PerformaceRemarksOut = "=Sb.";
                            }
                        }
                    }
                }
                else
                {
                    if (((RequiresWindMetering == 1) && (WindReading <= 2)) ||
                     (RequiresWindMetering == 0))
                    {
                        if ((CurrentPerformanceDecimal >= PersBestDecimal) && (PersBestDecimal > 0))
                        {
                            PerformaceRemarksOut = "Pb.";
                            if (CurrentPerformanceDecimal == PersBestDecimal)
                            {
                                PerformaceRemarksOut = "=Pb.";
                            }
                        }
                        else
                            if ((CurrentPerformanceDecimal >= YearBestDecimal) && (YearBestDecimal > 0))
                        {
                            PerformaceRemarksOut = "Sb.";
                            if (CurrentPerformanceDecimal == YearBestDecimal)
                            {
                                PerformaceRemarksOut = "=Sb.";
                            }
                        }
                    }
                }
            }
            return PerformaceRemarksOut;
        }

        private string BuildSeriesForTechnicalEvent(string OldSeries, decimal NewValue, Int32 Markings, decimal WindReading, Int32 RoundNo, bool WindMetered, Int32 NoOfRounds)
        {
            Global gl = new Global();
            string NewSeries = OldSeries;
            string NewValueText = "";
            if (RoundNo <= NoOfRounds)
            {
                WindReading = Math.Ceiling((WindReading * 10)) / 10;
                if (NewValue != 0)
                {
                    if (NewValue == -1)
                    {
                        NewValueText = "P";
                    }
                    else
                    {
                        if (NewValue == -2)
                        {
                            NewValueText = "X";
                        }
                        else
                        {
                            NewValueText = gl.FormatResultMeters(NewValue);
                            if (WindMetered == true)
                            {
                                NewValueText = NewValueText + "/" + gl.FormatWind(WindReading);
                            }
                        }
                    }
                }
                if ((NewValueText == "") && (Markings > 0))
                {
                    switch (Markings)
                    {
                        case 1:
                            NewValueText = "P";
                            break;
                        case 2:
                            NewValueText = "X";
                            break;
                    }

                }
                if (NewSeries == "")
                {
                    NewSeries = NewValueText;
                }
                else
                {
                    NewSeries = NewSeries + " - " + NewValueText;
                }
            }
            return NewSeries;
        }

        private Athl_HeightsInHJandPV CheckHJPVHeights(string CompCode, Int32 EventNo, Int32 BibNo, string[] TextBoxValues)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_HeightsInHJandPV AthlHeightsInHJandPVRec = new Athl_HeightsInHJandPV();
            AthlHeightsInHJandPVRec = AthlCRUD.GetHeightsInHJandPVRec(CompCode, EventNo, BibNo);
            if (
                (TextBoxValues[1] != AthlHeightsInHJandPVRec.C1__hæð) ||
                (TextBoxValues[2] != AthlHeightsInHJandPVRec.C2__hæð) ||
                (TextBoxValues[3] != AthlHeightsInHJandPVRec.C3__hæð) ||
                (TextBoxValues[4] != AthlHeightsInHJandPVRec.C4__hæð) ||
                (TextBoxValues[5] != AthlHeightsInHJandPVRec.C5__hæð) ||
                (TextBoxValues[6] != AthlHeightsInHJandPVRec.C6__hæð) ||
                (TextBoxValues[7] != AthlHeightsInHJandPVRec.C7__hæð) ||
                (TextBoxValues[8] != AthlHeightsInHJandPVRec.C8__hæð) ||
                (TextBoxValues[9] != AthlHeightsInHJandPVRec.C9__hæð) ||
                (TextBoxValues[10] != AthlHeightsInHJandPVRec.C10__hæð) ||
                (TextBoxValues[11] != AthlHeightsInHJandPVRec.C11__hæð) ||
                (TextBoxValues[12] != AthlHeightsInHJandPVRec.C12__hæð) ||
                (TextBoxValues[13] != AthlHeightsInHJandPVRec.C13__hæð) ||
                (TextBoxValues[14] != AthlHeightsInHJandPVRec.C14__hæð) ||
                (TextBoxValues[15] != AthlHeightsInHJandPVRec.C15__hæð) ||
                (TextBoxValues[16] != AthlHeightsInHJandPVRec.C16__hæð) ||
                (TextBoxValues[17] != AthlHeightsInHJandPVRec.C17__hæð) ||
                (TextBoxValues[18] != AthlHeightsInHJandPVRec.C18__hæð) ||
                (TextBoxValues[10] != AthlHeightsInHJandPVRec.C19__hæð) ||
                (TextBoxValues[20] != AthlHeightsInHJandPVRec.C20__hæð) ||
                (TextBoxValues[21] != AthlHeightsInHJandPVRec.C21__hæð) ||
                (TextBoxValues[22] != AthlHeightsInHJandPVRec.C22__hæð) ||
                (TextBoxValues[23] != AthlHeightsInHJandPVRec.C23__hæð) ||
                (TextBoxValues[24] != AthlHeightsInHJandPVRec.C24__hæð) ||
                (TextBoxValues[25] != AthlHeightsInHJandPVRec.C25__hæð))
            {
                AthlEnt.UpdateHeightsInHJandPV(CompCode, EventNo, AthlHeightsInHJandPVRec.Lína, BibNo, "", 0, 0, 0,
                    TextBoxValues[1], TextBoxValues[2], TextBoxValues[3], TextBoxValues[4], TextBoxValues[5], TextBoxValues[6],
                    TextBoxValues[7], TextBoxValues[8], TextBoxValues[9], TextBoxValues[10], TextBoxValues[11], TextBoxValues[12],
                    TextBoxValues[13], TextBoxValues[14], TextBoxValues[15], TextBoxValues[16], TextBoxValues[17], TextBoxValues[18],
                    TextBoxValues[19], TextBoxValues[20], TextBoxValues[21], TextBoxValues[22], TextBoxValues[23], TextBoxValues[24],
                    TextBoxValues[25], 0, 0, 0, 0, 0,
                    AthlHeightsInHJandPVRec.OpeningHeight, AthlHeightsInHJandPVRec.FirstIncreaseBy,
                    AthlHeightsInHJandPVRec.FirstLimit, AthlHeightsInHJandPVRec.SecondIncreaseBy,
                    AthlHeightsInHJandPVRec.SecondLimit, AthlHeightsInHJandPVRec.ThirdIncreaseBy);
                AthlHeightsInHJandPVRec = AthlCRUD.GetHeightsInHJandPVRec(CompCode, EventNo, BibNo);
            }
            return AthlHeightsInHJandPVRec;
        }

        protected void BackToEvents_Click(object sender, EventArgs e)
        {
            Global gl = new Global();

            //Response.Redirect("NyttMot.aspx?Comp=" + gl.GetCompetitionCode());
            Response.Redirect("SelectedCompetitionEvents.aspx");
        }

        protected string ValidateHeights(string Attempts, out Int32 NoOfAttemptCleared, out Int32 NoOfFails, out bool HasClearance)
        {
            string ValidCharacters = "xo-";
            string OutputValue = "";
            NoOfAttemptCleared = 0;
            NoOfFails = 0;
            HasClearance = false;
            if (Attempts == "")
            {
                return Attempts;
            }
            Int32 MaxLengthToCheck = Attempts.Length - 1;
            if (MaxLengthToCheck > 2)
            {
                MaxLengthToCheck = 2;
            }
            Attempts = Attempts.ToLower();
            for (int ixx = 0; ixx <= MaxLengthToCheck; ixx++)
            {
                string CurrentCharacter = Attempts.Substring(ixx, 1);
                if (ValidCharacters.IndexOf(CurrentCharacter) != -1)
                {
                    OutputValue = OutputValue + CurrentCharacter;
                    switch (CurrentCharacter)
                    {
                        case "x":
                            NoOfFails = NoOfFails + 1;
                            break;
                        case "o":
                            NoOfAttemptCleared = ixx + 1;
                            HasClearance = true;
                            break;
                    }
                }
            }
            return OutputValue;
        }

        protected void ShowResultOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("RaceResults.aspx?Event=" + Request.QueryString.Get("Event"));
        }

        protected string BuildSeriesForHJandPV(string OldSeries, string NewHeight, string FailsOrClearance)
        {
            string NewSeries = OldSeries;
            if (FailsOrClearance != "")
            {
                if ((FailsOrClearance == "-") && (OldSeries == ""))
                {
                    //Do nothing - competitor has not started yet
                }
                else
                {
                    if (NewSeries != "")
                    {
                        NewSeries = NewSeries + " ";
                    }
                    NewSeries = NewSeries + NewHeight + "/" + FailsOrClearance;
                }
            }
            return NewSeries;
        }

        //Int32[,] PopulateTabIndexArray(bool FieldEvWithMax8InLastRounds, Int32 NoOfLines, Int32 NoOfColumns)
        //{
        //    Int32[,] TabOrderIndexArrayLocal = new Int32[500, 7];
        //    Int32 RowNo = 0;
        //    Int32 ColNo = 0;
        //    Int32 TabIndex = 1;
        //    if (SelectTabOrder.SelectedIndex == 0)  //Lines
        //    {
        //        for (RowNo = 1; RowNo <= NoOfLines; RowNo++)
        //        {
        //            for (ColNo = 1; ColNo <= NoOfColumns; ColNo++)
        //                if (FieldEvWithMax8InLastRounds)
        //                {
        //                    if ((RowNo < 8) || ((RowNo >= 8) && (ColNo < 3)))
        //                    {
        //                        TabOrderIndexArrayLocal[RowNo, ColNo] = TabIndex;
        //                    }
        //                    else
        //                    {
        //                        TabOrderIndexArrayLocal[RowNo, ColNo] = 10000;
        //                    }
        //                    TabIndex = TabIndex + 1;

        //                }
        //                else
        //                {
        //                    TabOrderIndexArrayLocal[RowNo, ColNo] = TabIndex;
        //                    TabIndex = TabIndex + 1;
        //                }
        //        }
        //    }
        //    else  //Columns
        //    {
        //        for (ColNo = 1; ColNo <= NoOfColumns; ColNo++)
        //        {
        //            for (RowNo = 1; RowNo <= NoOfLines; RowNo++)
        //            {
        //                if (FieldEvWithMax8InLastRounds)
        //                {
        //                    if ((RowNo < 8) || ((RowNo >= 8) && (ColNo < 3)))
        //                    {
        //                        TabOrderIndexArrayLocal[RowNo, ColNo] = TabIndex;
        //                    }
        //                    else
        //                    {
        //                        TabOrderIndexArrayLocal[RowNo, ColNo] = 10000;
        //                    }
        //                    TabIndex = TabIndex + 1;

        //                }
        //                else
        //                {
        //                    TabOrderIndexArrayLocal[RowNo, ColNo] = TabIndex;
        //                    TabIndex = TabIndex + 1;
        //                }
        //            }
        //        }
        //    }

        //    return TabOrderIndexArrayLocal;


        //}

        protected Int32 ConvertStringToInt32(string TextIn)
        {
            Int32 IntToReturn;
            bool ConvertsOK = Int32.TryParse(TextIn, out IntToReturn);
            if (ConvertsOK == false)
            {
                IntToReturn = -1;
            }
            return IntToReturn;
        }

        protected string ReturnHeightsText(Athl_HeightsInHJandPV HeightsRec)
        {
            string HeightOutText = "";
            HeightOutText = HeightsRec.C1__hæð.ToString() + ";" +
    HeightsRec.C2__hæð.ToString() + ";" +
    HeightsRec.C3__hæð.ToString() + ";" +
    HeightsRec.C4__hæð.ToString() + ";" +
    HeightsRec.C5__hæð.ToString() + ";" +
    HeightsRec.C6__hæð.ToString() + ";" +
    HeightsRec.C7__hæð.ToString() + ";" +
    HeightsRec.C8__hæð.ToString() + ";" +
    HeightsRec.C9__hæð.ToString() + ";" +
    HeightsRec.C10__hæð.ToString() + ";" +
    HeightsRec.C11__hæð.ToString() + ";" +
    HeightsRec.C12__hæð.ToString() + ";" +
    HeightsRec.C13__hæð.ToString() + ";" +
    HeightsRec.C14__hæð.ToString() + ";" +
    HeightsRec.C15__hæð.ToString() + ";" +
    HeightsRec.C16__hæð.ToString() + ";" +
    HeightsRec.C17__hæð.ToString() + ";" +
    HeightsRec.C18__hæð.ToString() + ";" +
    HeightsRec.C19__hæð.ToString() + ";" +
    HeightsRec.C20__hæð.ToString() + ";" +
    HeightsRec.C21__hæð.ToString() + ";" +
    HeightsRec.C22__hæð.ToString() + ";" +
    HeightsRec.C23__hæð.ToString() + ";" +
    HeightsRec.C24__hæð.ToString() + ";" +
    HeightsRec.C25__hæð.ToString();
            return HeightOutText;

        }

        protected void PrintResults_Click(object sender, EventArgs e)
        {
            Global gl = new Global();
            gl.SetGlobalValue("HeatNumberFilter", SelectedHeatFilter.Text);
            Response.Redirect("PrintEventResults.aspx?Event=" + Request.QueryString.Get("Event") + "&HeatNoFilter=" + CurrPageAndHeat.Text);
        }


        protected void FillOutHeights_Click(object sender, EventArgs e)
        {
            Response.Redirect("HeightsInHJandPV.aspx?Event=" + Request.QueryString.Get("Event"));

        }

        protected void AddCompetitor_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCompetitorsToEvent.aspx?Event=" + Request.QueryString.Get("Event"));
        }

        protected void SeedCompetitors_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectSeedingMethod.aspx?Event=" + Request.QueryString.Get("Event"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Global gl = new Global();
            string TegGr = gl.GetGlobalValue("EvType");
            if (TegGr == "1") //Track
            {
                Page page = (Page)HttpContext.Current.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                }
                string url = "http://82.221.94.225/ResultSheets/SheetForTrack.aspx?CompCode=" + gl.GetCompetitionCode() + "&EventLineNo=" + Request.QueryString.Get("Event");
                string script;
                script = @"window.open(""{0}"", ""{1}"");";
                script = String.Format(script, url, "_blank"); //, windowFeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
            else
                if (TegGr == "2")
            {
                string CloserType = gl.GetGlobalValue("EvCloserType");
                if (CloserType == "4")  //HJ or PV
                {
                    Page page = (Page)HttpContext.Current.Handler;
                    if (page == null)
                    {
                        throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                    }
                    string url = "http://82.221.94.225/ResultSheets/SheetForHJandPV.aspx?CompCode=" + gl.GetCompetitionCode() + "&EventLineNo=" + Request.QueryString.Get("Event");
                    string script;
                    script = @"window.open(""{0}"", ""{1}"");";
                    script = String.Format(script, url, "_blank"); //, windowFeatures);
                    ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
                }
                else
                {
                    Page page = (Page)HttpContext.Current.Handler;
                    if (page == null)
                    {
                        throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                    }
                    string url = "http://82.221.94.225/ResultSheets/SheetForField.aspx?CompCode=" + gl.GetCompetitionCode() + "&EventLineNo=" + Request.QueryString.Get("Event");
                    string script;
                    script = @"window.open(""{0}"", ""{1}"");";
                    //}
                    script = String.Format(script, url, "_blank"); //, windowFeatures);
                    ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
                }
            }
        }
        //gl.SetGlobalValue("EvType", AthlEvent.tegundgreinar.ToString());
        //gl.SetGlobalValue("EvCloserType", AthlEvent.nanaritegundargreining.ToString());

        //if (HLAUP)
        //Response.Redirect("RitarabladHlaup.aspx?Event=" + Request.QueryString.Get("Event"));
        //Hér þarf að kalla á skýrsluna sem prentar ritrarablað fyrir greinina
        //http://82.221.94.225/ResultSheets/SheetForTrack.aspx?CompCode=GOUMOT2015&EventLineNo=10000


        protected void LiveUpdate_Click(object sender, EventArgs e)
        {
            // Response.Redirect("EventProgressLiveUpdate.aspx?EventLineNo=" + xx + "&EventName=" + xx);
        }

        protected void ReadLynxFiles_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReadLynxFiles.aspx?EventLineNo=" + Request.QueryString.Get("Event"));
        }

        protected void RetriveRunnersFromPrevRnd_Click(object sender, EventArgs e)
        {
            Athl_CompetitorsInCompetition AthlCompetitorInComp = new Athl_CompetitorsInCompetition();
            Athl_CompetitionEvents AthlCompEvent = new Athl_CompetitionEvents();
            Athl_CompetitorsInEvent CompInEvThisRnd = new Athl_CompetitorsInEvent();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            // System.Data.Objects.ObjectParameter IgnoreError = new System.Data.Objects.ObjectParameter("IgnoreError", typeof(Int32));
            Int32 IgnoreError = 0;
            Int32[] BibOfRetrievedRunners = new Int32[100];
            decimal[] TimeOfRunnerInPrevRound = new decimal[100];
            string[] ResultOfRunnerInPrevRound = new string[100];
            Int32[] ThousandthOfASecond = new Int32[100];
            Int32 LineNoOfPrvRnd = 0;
            Int32 NoOfRunnersRetrieved = 0;
            Global gl = new Global();
            string CompCode;
            CompCode = gl.GetCompetitionCode();
            string EventLineNo = Request.QueryString.Get("Event");
            Int32 EventLin = Convert.ToInt32(EventLineNo);
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            var SelectedCompetitors = AthlEnt.RetrieveRunnersFromPreviousRound(CompCode, EventLin, IgnoreError);
            //Int32 ErrNo = 0;
            foreach (var SelComp in SelectedCompetitors.ToList())
            {
                string ErrMessage = SelComp.ErrMsg.ToString();
                string ErrCode = SelComp.ErrCode.Value.ToString();
                if (ErrMessage == "")
                {
                    Int32 BibNo = Convert.ToInt32(SelComp.BibNo.Value.ToString());
                    decimal TimeInPrev = gl.ParseTextAndReturnDec(SelComp.TimeInHeat); // Convert.ToDecimal(SelComp.TimeInHeat.ToString());
                    Int32 Thous = Convert.ToInt32(SelComp.ThousandsOfASec.Value.ToString());
                    NoOfRunnersRetrieved = NoOfRunnersRetrieved + 1;
                    BibOfRetrievedRunners[NoOfRunnersRetrieved] = BibNo;
                    TimeOfRunnerInPrevRound[NoOfRunnersRetrieved] = TimeInPrev;
                    ResultOfRunnerInPrevRound[NoOfRunnersRetrieved] = SelComp.TimeInHeat.ToString();
                    ThousandthOfASecond[NoOfRunnersRetrieved] = Thous;
                    LineNoOfPrvRnd = Convert.ToInt32(SelComp.LineNoFromPrevRound.Value.ToString());
                }
                else
                {
                    string msg = "Error:" + ErrCode + " - " + ErrMessage;
                    Response.Write("<script>alert('" + msg + "')</script>");
                    return;
                }
            }
            if (NoOfRunnersRetrieved > 0)
            {
                for (int Ix = 1; Ix <= NoOfRunnersRetrieved; Ix++)
                {
                    AthlCompetitorInComp = AthlCRUD.GetCompetitorInComp(CompCode, BibOfRetrievedRunners[Ix]);
                    CompInEvThisRnd = AthlCRUD.InitCompetitorInEvent();
                    AthlCompEvent = AthlCRUD.GetCompetitionEvent(CompCode, EventLin);

                    AthlCRUD.RegisterCompetitorInEvent(CompCode, AthlCompEvent,
                        BibOfRetrievedRunners[Ix], AthlCompetitorInComp, 1, Ix);
                    CompInEvThisRnd = AthlCRUD.GetCompetitorInEvent(CompCode, BibOfRetrievedRunners[Ix], EventLin);
                    CompInEvThisRnd.bestiaranguriar = TimeOfRunnerInPrevRound[Ix];
                    CompInEvThisRnd.bestiaranguriartexti = ResultOfRunnerInPrevRound[Ix];
                    CompInEvThisRnd.nanarirod = ThousandthOfASecond[Ix];
                    AthlEnt.UpdateCompInEvPbSb(CompCode, EventLin, BibOfRetrievedRunners[Ix],
                        CompInEvThisRnd.personulegmet, CompInEvThisRnd.personulegtmettexti,
                       (TimeOfRunnerInPrevRound[Ix] * 1000) + ThousandthOfASecond[Ix] + CompInEvThisRnd.urslitarod, ResultOfRunnerInPrevRound[Ix]);
                    AthlEnt.InsertToLogFile(gl.GetCurrUsrName(), "Keppendur sóttir úr fyrri umf.", CompCode, AthlCompetitorInComp.keppendanumer, AthlCompEvent.grein + ";" +
                        AthlCompEvent.kyn.ToString() + AthlCompEvent.flokkur);
                }

                gl.SetGlobalValue("MsgInUpdEvResult", "Muna eftir að raða keppendum í riðla og á brautir!");
                Response.Redirect("UpdateEventResults.aspx?Code=" + CompCode + "&Event=" + EventLin.ToString());

            }
        }
    }
}