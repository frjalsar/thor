using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MotFRI
{
    public partial class PrintEventReults : System.Web.UI.Page
    {
        static protected Int32 LineWidth = -1;
        static Athl_CompetitionEvents AthlCompEventRec = new Athl_CompetitionEvents();
        static Athl_Competition AthlCompetition = new Athl_Competition();

        protected void Page_Load(object sender, EventArgs e)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Global gl = new Global();
            DateTime FirstD;
            DateTime LastD;
            DateTime CurrentDateAndTime = DateTime.Now;
            string EventDateTim;
            string CompCode = gl.GetCompetitionCode();
            //string EventLineNo = gl.GetCompetitionEventNo();
            if (CompCode == "")
            {
                CompCode = Request.QueryString.Get("Code");
            }
            string EventLineNo = Request.QueryString.Get("Event");
            string HeatNumberFilter = "";
            CompetitionVenue.Text = gl.GetCompetitionVenue();
            Int32 EvNo = Convert.ToInt32(EventLineNo);
            AthlCompEventRec = AthlCRUD.GetCompetitionEvent(CompCode, EvNo);
            //if (!IsPostBack)
            //{
            //    HeatNoFilter.Text = gl.GetGlobalValue("HeatNumberFilter");
            //}
            //CompetitionName.Text = gl.GetCompetitionName();
            AthlCompetition = AthlCRUD.GetCompetitionRec(CompCode);
            CompetitionName.Text = AthlCompetition.Name;
            
            EventDateTim = gl.ReturnEventDateAndTime(CompCode, EvNo);

            if (AthlCompetition.tungumal == 0)  //Icelandic
            {
                CompetitionEventName.Text = AthlCompEventRec.heitigreinar + " - " + EventDateTim;
            }
            else
            {
                CompetitionEventName.Text = AthlCompEventRec.heitigreinar + " - " + AthlCompEventRec.ensktheitigreinar + " - " + EventDateTim;
            }

            //string QryStr = Request.QueryString.Get("HeatNoFilter");
            //if (QryStr != null)
            //{
            //    HeatNumberFilter = QryStr;
            //    HeatNoFilter.Text = QryStr;
            //    if (HeatNumberFilter != "%")
            //    {
            //        CompetitionEventName.Text = CompetitionEventName.Text + " - Riðill " + HeatNumberFilter; ;
            //    }
            //}

            DateAndTime.Text = "Pr.: " + CurrentDateAndTime.ToString();

            var CompetitionDatesRec = AthlEnt.ReturnCompetitionDates(CompCode);
            foreach (var CompDatesRec in CompetitionDatesRec)  //There is only one record here
            {
                FirstD = Convert.ToDateTime(CompDatesRec.FirstDate);
                LastD = Convert.ToDateTime(CompDatesRec.LastDate);
                if (FirstD == LastD)
                {
                    CompetitionDates.Text = FirstD.ToString("d");            
                }
                else
                {
                    CompetitionDates.Text = FirstD.ToString("d") + " - " + LastD.ToString("d");
                }
            }


            FillPage(CompCode, EvNo, AthlCompEventRec, HeatNumberFilter);

               
        }
        protected void FillPage(string CompCode, Int32 EventNo, Athl_CompetitionEvents AthlCompEventRec, string HeatNoFilt)
        {
            Int32 LabelNo = 0;
            string LblWidhtForSeries = "";
            Global gl = new Global();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Athl_Competition AthlCompetitionRec = new Athl_Competition();
            AthlCompetitionRec = AthlCRUD.GetCompetitionRec(CompCode);

            PH.Controls.Add(new LiteralControl("<span style = 'font-family:Arial;font-size:12pt'>"));

            if ((HeatNoFilt == "0") || (HeatNoFilt == ""))
            {
                HeatNoFilt = "%";
            }
            else
            {
                //LabelNo = LabelNo + 1;
                //AddLabel(LabelNo, "Riðill " + HeatNoFilt, "200px", "left", false, false);
                //PH.Controls.Add(new LiteralControl("<br /><br />"));
            }
            var CompetitorsInEventDataset = AthlEnt.CompetitorsInEvResultOrder(CompCode, EventNo, HeatNoFilt);
            
            LabelNo = LabelNo + 1;
            if (AthlCompetitionRec.tungumal == 0)  //Icelandic
            {
                AddLabel(LabelNo, "Sæti", "50px", "right", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Árangur", "80px", "right", false, false);
                if ((AthlCompetitionRec.tegundstigakeppni > 0) && (AthlCompEventRec.stigagrein == 1))
                {
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Stig", "50px", "right", false, false);  //"80px"
                }
                if (AthlCompetitionRec.Reikna_IAAF_stig == 1)
                {
                    AddLabel(LabelNo, "IAAF Stig", "50px", "right", false, false);  //"80px"
                }
                if (AthlCompEventRec.thrautargrein == 1)
                {
                    if (AthlCompetitionRec.Reikna_unglingastig == 1)
                    {
                        AddLabel(LabelNo, "Ungl.stig", "50px", "right", false, false);  //"60px"
                    }
                    else
                        AddLabel(LabelNo, "Stig", "50px", "right", false, false);       //"60px"
                }
                if (AthlCompEventRec.krefstvindmaelis == 1)
                {
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "", false, false);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Vindur", "50px", "", false, false);
                }
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "", "10px", "", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "", "10px", "", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Uppl.", "40px", "left", false, false);  //"70px"
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Nafn", "250px", "", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "F.ár", "50px", "", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Félag", "80px", "", false, false);
                if (AthlCompEventRec.tegundgreinar == 1)  //Track
                {
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Ri.", "20px", "right", false, false);  //Riðill "50px"
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Br", "20px", "right", false, false);   //Braut "50px"
                }
                else
                {
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Röð", "20px", "right", false, false);  //Field Event
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "", false, false);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Sería", "200px", "left", false, false); //"500px"
                }
            }
            else //Language English or Both
            {
                AddLabel(LabelNo, "Place", "50px", "right", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Mark", "80px", "right", false, false);
                if ((AthlCompetitionRec.tegundstigakeppni > 0) && (AthlCompEventRec.stigagrein == 1))
                {
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Points", "50px", "right", false, false);  //"80px"
                }
                if (AthlCompetitionRec.Reikna_IAAF_stig == 1)
                {
                    AddLabel(LabelNo, "IAAF pts", "50px", "right", false, false);  //"80px"
                }
                if (AthlCompEventRec.thrautargrein == 1)
                {
                    if (AthlCompetitionRec.Reikna_unglingastig == 1)
                    {
                        AddLabel(LabelNo, "Youth pts", "50px", "right", false, false);  //"60px"
                    }
                    else
                        AddLabel(LabelNo, "Points", "50px", "right", false, false);     //"60px"
                }
                if (AthlCompEventRec.krefstvindmaelis == 1)
                {
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "", false, false);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Wind", "50px", "", false, false);
                }
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "", "10px", "", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "", "10px", "", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Info", "40px", "left", false, false);  //"70px"
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Name", "250px", "", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Year", "50px", "", false, false);
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Club", "80px", "", false, false);
                if (AthlCompEventRec.tegundgreinar == 1)  //Track
                {
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Ht.", "20px", "right", false, false);  //Heat "50px"
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Ln.", "20px", "right", false, false);  //Lane "50px"
                }
                else
                {
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Order", "20px", "right", false, false);  //Field Event
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "", false, false);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "Series", "200px", "left", false, false);  //"500px"
                }

            }
            PH.Controls.Add(new LiteralControl("<br />"));

            foreach (var CompInEvRec in CompetitorsInEventDataset)
            {
                if (((CompInEvRec.rasnumer != 0) || (CompInEvRec.nafn != "")))
                {
                    LineWidth = 0;
                    LabelNo = LabelNo + 1;
                    if (CompInEvRec.urslitarod < 9999)
                    {
                        AddLabel(LabelNo, CompInEvRec.urslitarodtexti, "50px", "right", false, false);
                    }
                    else
                    {
                        AddLabel(LabelNo, "", "50px", "right", false, false);
                    }
                    LabelNo = LabelNo + 1;
                    if (CompInEvRec.arangur != "")
                    {
                        AddLabel(LabelNo, CompInEvRec.arangur, "80px", "right", true, false);
                    }
                    else
                    {
                        AddLabel(LabelNo, CompInEvRec.athugasemd, "80px", "right", true, false);
                    }
                    if ((AthlCompetitionRec.tegundstigakeppni > 0) && (AthlCompEventRec.stigagrein == 1))
                    {            
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.stig > 0)
                        {
                            Int32 PointsInt = Convert.ToInt32(CompInEvRec.stig);
                            Decimal PointsDec = PointsInt;
                            if (CompInEvRec.stig == PointsDec)
                            {
                                AddLabel(LabelNo, PointsInt.ToString(), "50px", "right", false, false); //"50px"
                            }
                            else
                            {
                                //AddLabel(LabelNo, CompInEvRec.stig.ToString(), "80px", "right", false, false);
                                AddLabel(LabelNo, string.Format("{0:N2}", CompInEvRec.stig), "50px", "right", false, false);  //"80px"
                            }
                        }
                        else
                        {
                            AddLabel(LabelNo, "", "50px", "right", false, false);  //"80px"
                        }
                    }
                    if (AthlCompetitionRec.Reikna_IAAF_stig == 1)
                    {
                        if (CompInEvRec.IAAF_Stig > 0)
                        {
                            AddLabel(LabelNo, CompInEvRec.IAAF_Stig.ToString(), "50px", "right", false, false);  //"80px"
                        }
                        else
                        {
                            AddLabel(LabelNo, "", "50px", "right", false, false);    //"80px"               
                        }
                    }
                    if (AthlCompEventRec.thrautargrein == 1)
                    {
                        //REDDING !!!
                        if ((CompCode == "M-00000017") && 
                            (AthlCompEventRec.kyn == 2) && 
                            (AthlCompEventRec.aldurtil == 15) && 
                            (AthlCompEventRec.grein != "FIMMTUNG"))
                        {
                            AddLabel(LabelNo, CompInEvRec.Unglingastig.ToString(), "60px", "right", false, false);
                        }
                        else
                        if (CompInEvRec.thrautarstig > 0)
                        {
                            AddLabel(LabelNo, CompInEvRec.thrautarstig.ToString(), "60px", "right", false, false);
                        }
                        else
                        {
                            if (CompInEvRec.Unglingastig > 0)
                            {
                                AddLabel(LabelNo, CompInEvRec.Unglingastig.ToString(), "60px", "right", false, false);
                            }
                            else
                            AddLabel(LabelNo, "", "60px", "right", false, false);
                        }
                    }
                    if (AthlCompEventRec.krefstvindmaelis == 1)
                    {
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "10px", "", false, false);
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.arangur != "")
                        {
                            AddLabel(LabelNo, gl.FormatWind(Convert.ToDecimal(CompInEvRec.vindur)), "50px", "", false, false);
                        }
                        else
                        {
                            AddLabel(LabelNo, "", "50px", "", false, false);
                        }
                    }
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "", false, false);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, CompInEvRec.PerformaceRemarks, "40px", "left", false, false);  //"70px"
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "", false, false);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, CompInEvRec.nafn, "250px", "", true, false);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, CompInEvRec.faedingarar.ToString(), "50px", "", false, false);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, CompInEvRec.felag, "80px", "", false, false);
                    if (AthlCompEventRec.tegundgreinar == 1)  //Track
                    {
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.ridillnumer.ToString(), "20px", "right", false, false);  //"50px"
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.stokkkastrod.ToString(), "20px", "right", false, false);  //"50px"
                        if (CompInEvRec.seria != "")
                        {
                            PH.Controls.Add(new LiteralControl("<br />"));
                            LabelNo = LabelNo + 1;
                            AddLabel(LabelNo, "", "90px", "Left", false, true);
                            LabelNo = LabelNo + 1;
                            AddLabel(LabelNo, CompInEvRec.seria, "600px", "Left", false, true);
                        }
                    }
                    else
                    {
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.stokkkastrod.ToString(), "20px", "right", false, false);  //Technical (Field Event)
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "10px", "", false, false);
                        LabelNo = LabelNo + 1;
                        //LblWidhtForSeries = Convert.ToString(1150 - LineWidth) + "px";
                        //AddLabel(LabelNo, CompInEvRec.seria, LblWidhtForSeries, "", false, true);   //"500px"
                        AddLabel(LabelNo, CompInEvRec.seria, "200px", "", false, true);
                    }
                    PH.Controls.Add(new LiteralControl("<br />"));
                }
            }
            PH.Controls.Add(new LiteralControl("</span>"));           
        }

        protected void AddLabel(Int32 LblNo, string LabelText, string LabelWidth, string AlignText, bool SetFontBold, bool SetFontSmall)
        {
            string LblWidthStripped = LabelWidth;
            LblWidthStripped = LblWidthStripped.Replace("p", "").Replace("x", "");
            Label Lbl = new Label();
            Lbl.ID = "Lbl" + Convert.ToString(LblNo);
            Lbl.Width = new System.Web.UI.WebControls.Unit(LabelWidth);
            //string FontName = "Arial";
            //Lbl.Font = new System.Drawing.FontFamily(FontName);
                //Arial);  //NamesConverter(); //new System.Web.UI.WebControls.
            if (AlignText != "")
            {
                Lbl.Style["text-align"] = AlignText; //right, left
            }
            //Lbl.Style["Font-Names"] = "Courier";
            //Font-Names="Arial"
            Lbl.Text = LabelText;
            Lbl.Font.Bold = SetFontBold;
            if (SetFontSmall == true)
            {
                Lbl.Font.Size = FontUnit.XSmall; //new System.Web.UI.WebControls.Unit(SetFontSize);
            }
            LineWidth = LineWidth + Convert.ToInt32(LblWidthStripped);
            PH.Controls.Add(Lbl);
        }

        //protected void AddTextBox(Int32 LineNo, Int32 TxtBoxNo, string TextBoxText, string TextBoxWidth, string AlignText, Int32 TabIndx, bool WindTextBox)
        //{
        //    TextBox Txb = new TextBox();
        //    if (WindTextBox == true)
        //    {
        //        Txb.ID = "Wind_" + Convert.ToString(LineNo) + "_" + Convert.ToString(TxtBoxNo);
        //    }
        //    else
        //    {
        //        Txb.ID = "Txb_" + Convert.ToString(LineNo) + "_" + Convert.ToString(TxtBoxNo);
        //    }
        //    Txb.Width = new System.Web.UI.WebControls.Unit(TextBoxWidth);
        //    if (AlignText != "")
        //    {
        //        Txb.Style["text-align"] = AlignText; //right, left
        //    }
        //    if (TabIndx > 0)
        //    {
        //        Txb.TabIndex = Convert.ToInt16(TabIndx);
        //    }
        //    Txb.Text = TextBoxText;
        //    PH.Controls.Add(Txb);
        //}

        //protected void AddCheckBox(Int32 LineNo, Int32 ChkBoxNo, bool CheckBoxValue, string CheckBoxWidth)
        //{
        //    CheckBox Chkb = new CheckBox();
        //    Chkb.ID = "Chkb_" + Convert.ToString(LineNo) + "_" + Convert.ToString(ChkBoxNo);
        //    Chkb.Width = new System.Web.UI.WebControls.Unit(CheckBoxWidth);
        //    Chkb.Checked = CheckBoxValue;
        //    //Chkb.Enabled = false;
        //    PH1.Controls.Add(Chkb);
        //}


    }
}