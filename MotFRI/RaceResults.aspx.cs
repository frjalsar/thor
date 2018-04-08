using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class EventRes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            if (Session["CurrentAccessLevel"] == "0")
            {
                SaveRaceResultOrder.Visible = false;
            }
          
            string CompCode = gl.GetCompetitionCode();
            //string EventLineNo = gl.GetCompetitionEventNo();
            string EventLineNo = Request.QueryString.Get("Event");
            Int32 EventLin = Convert.ToInt32(EventLineNo);
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD(); ;
            Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();

            AthlEvent = AthlCRUD.GetCompetitionEvent(CompCode, EventLin);

            EventName.Text = AthlEvent.heitigreinar;

            FillPage(CompCode, EventLin, AthlEvent);


        }
        protected void FillPage(string CompCode, Int32 EventNo, Athl_CompetitionEvents AthlEvent)
        {
            Int32 LabelNo = 0;
            Int32 TextBoxNo = 0;
            Global gl = new Global();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            // var CompetitorsInEventDataset = AthlEnt.CompetitorsInEventInResultOrder1(CompCode, EventNo, "%");
            var CompetitorsInEventDataset = AthlEnt.CompetitorsInEvResultOrder(CompCode, EventNo, "%");
            Int32 NoOfLines = 0;
            string FirstInCloserOrder = "";
            Int32 IndexOfDash;

            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Lína", "50px", "right");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Rásn.", "50px", "right");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "", "10px", "");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Nafn", "220px", "");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "F.ár", "50px", "");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Félag", "80px", "");
            LabelNo = LabelNo + 1;
            if (AthlEvent.tegundgreinar == 1)  //Track
            {
                AddLabel(LabelNo, "Riðill", "50px", "right");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Braut", "50px", "right");
                LabelNo = LabelNo + 1;
            }
            else
            {
                AddLabel(LabelNo, "Röð", "20px", "right");
                LabelNo = LabelNo + 1;
            }
            AddLabel(LabelNo, "Úrsl.röð", "70px", "right");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Nánari röð", "70px", "right");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "Árangur", "80px", "right");
            LabelNo = LabelNo + 1;
            AddLabel(LabelNo, "", "10px", "");
            if (AthlEvent.krefstvindmaelis == 1)
            {
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Vindur", "80px", "right");
            }
            if (AthlEvent.tegundgreinar == 2)  //Technical (Field Event)
            {
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "", "10px", "");
                LabelNo = LabelNo + 1;
                AddLabel(LabelNo, "Sería", "500px", "left");
            }
            PH1.Controls.Add(new LiteralControl("<br />"));

            foreach (var CompInEvRec in CompetitorsInEventDataset)
            {
                if ((CompInEvRec.rasnumer != 0) || (CompInEvRec.nafn != ""))
                {
                    NoOfLines = NoOfLines + 1;
                    if (CompInEvRec.urslitarod < 9999)
                    {
                        IndexOfDash = CompInEvRec.urslitarodtexti.IndexOf("-");
                        if (IndexOfDash != -1)
                        {
                            FirstInCloserOrder = CompInEvRec.urslitarodtexti.Substring(0, IndexOfDash);
                        }
                        else
                        {
                            FirstInCloserOrder = CompInEvRec.urslitarodtexti;
                        }
                    }
                    else
                    {
                        FirstInCloserOrder = "";
                    }
                    //gl.SetArr1Value(NoOfLines, Convert.ToString(CompInEvRec.nanarirod));
                    gl.SetBibNoArrayElement(NoOfLines, CompInEvRec.rasnumer);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, Convert.ToString(NoOfLines), "50px", "right");
                    LabelNo = LabelNo + 1;
                    TextBoxNo = 0;
                    AddLabel(LabelNo, Convert.ToString(CompInEvRec.rasnumer), "50px", "right");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, CompInEvRec.nafn, "220px", "");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, Convert.ToString(CompInEvRec.faedingarar), "50px", "");
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, CompInEvRec.felag, "80px", "");
                    if (AthlEvent.tegundgreinar == 1)
                    {
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, Convert.ToString(CompInEvRec.ridillnumer), "50px", "right");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, Convert.ToString(CompInEvRec.stokkkastrod), "50px", "right");
                    }
                    else 
                    {
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, Convert.ToString(CompInEvRec.stokkkastrod), "20px", "right");
                    }
                    LabelNo = LabelNo + 1;
                    if (CompInEvRec.urslitarod < 10000)
                    {
                        AddLabel(LabelNo, CompInEvRec.urslitarodtexti, "60px", "right"); //urslitarodtexti
                    }
                    else
                    {
                        AddLabel(LabelNo, "", "60px", "right");
                    }
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, " ", "10px", "right");
                    TextBoxNo = TextBoxNo + 1;
                    //AddTextBox(NoOfLines, TextBoxNo, CompInEvRec.nanarirod.ToString(), "60px", "right", (NoOfLines * 10) + 1, false);                    
                    AddTextBox(NoOfLines, TextBoxNo, FirstInCloserOrder, "60px", "right", (NoOfLines * 10) + 1, false);
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "right");
                    LabelNo = LabelNo + 1;
                    if (CompInEvRec.arangur == "")
                    {
                        AddLabel(LabelNo, CompInEvRec.athugasemd, "80px", "right");
                    }
                    else
                    {
                        AddLabel(LabelNo, CompInEvRec.arangur, "80px", "right");
                    }
                    LabelNo = LabelNo + 1;
                    AddLabel(LabelNo, "", "10px", "");
                    if (AthlEvent.krefstvindmaelis == 1)
                    {
                        LabelNo = LabelNo + 1;
                        if (CompInEvRec.arangur != "")
                        {
                            AddLabel(LabelNo, gl.FormatWind(Convert.ToDecimal(CompInEvRec.vindur)), "80px", "right");
                        }
                        else
                        {
                            AddLabel(LabelNo, "", "80px", "right");
                        }
                    }
                    if (AthlEvent.tegundgreinar == 2)  //Technical (Field Event)
                    {
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, "", "10px", "");
                        LabelNo = LabelNo + 1;
                        AddLabel(LabelNo, CompInEvRec.seria, "500px", "left");
                    }

                }
                PH1.Controls.Add(new LiteralControl("<br />"));
            }
            PH1.Controls.Add(new LiteralControl("<br />"));
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

        protected void AddTextBox(Int32 LineNo, Int32 TxtBoxNo, string TextBoxText, string TextBoxWidth, string AlignText, Int32 TabIndx, bool WindTextBox)
        {
            TextBox Txb = new TextBox();
            if (WindTextBox == true)
            {
                Txb.ID = "Wind_" + Convert.ToString(LineNo) + "_" + Convert.ToString(TxtBoxNo);
            }
            else
            {
                Txb.ID = "Txb_" + Convert.ToString(LineNo) + "_" + Convert.ToString(TxtBoxNo);
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
            Txb.Text = TextBoxText;
            PH1.Controls.Add(Txb);
        }

        protected void AddCheckBox(Int32 LineNo, Int32 ChkBoxNo, bool CheckBoxValue, string CheckBoxWidth)
        {
            CheckBox Chkb = new CheckBox();
            Chkb.ID = "Chkb_" + Convert.ToString(LineNo) + "_" + Convert.ToString(ChkBoxNo);
            Chkb.Width = new System.Web.UI.WebControls.Unit(CheckBoxWidth);
            Chkb.Checked = CheckBoxValue;
            //Chkb.Enabled = false;
            PH1.Controls.Add(Chkb);
        }

        protected void SaveRaceResultOrder_Click(object sender, EventArgs e)
        {
            Global gl = new Global();
            string CompCode = gl.GetCompetitionCode();
            //Int32 EventLineNo = Convert.ToInt32(gl.GetCompetitionEventNo());
            string EventLineNoText = Request.QueryString.Get("Event");
            Int32 EventLineNo = Convert.ToInt32(EventLineNoText);
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitorsInEvent AthlCompetitorsInEventRec = new Athl_CompetitorsInEvent();
            Athl_CompetitionEvents AthlCompEvent = new Athl_CompetitionEvents();
            string TextBoxID;
            string CurrValue;
            Int32 CurrValueInt;
            bool NeedToUpdateOrderByResults = false;
            string[] ControlNameParts;
            Int32 LineNoForTextBox;
            Int32 BibNo = 0;

            CurrValue = "";
            LineNoForTextBox = -1;
            AthlCompEvent = AthlCRUD.GetCompetitionEvent(CompCode, EventLineNo);

            foreach (Control item in PH1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox t1 = (TextBox)PH1.FindControl(item.ID);
                    TextBoxID = t1.ID;
                    CurrValue = t1.Text;
                    ControlNameParts = TextBoxID.Split('_');
                    LineNoForTextBox = Convert.ToInt32(ControlNameParts[1]);
                    BibNo = gl.GetBibNoArrayElement(LineNoForTextBox);
                    bool res = Int32.TryParse(CurrValue, out CurrValueInt);
                    if (res == false)
                    {
                        CurrValueInt = 0;
                    }
                    if (CurrValueInt.ToString() != gl.GetArr1Value(LineNoForTextBox))
                    {
                        AthlCompetitorsInEventRec = AthlCRUD.GetCompetitorInEvent(CompCode, BibNo, EventLineNo);
                        
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
                            AthlCompetitorsInEventRec.rafmagnstimataka, AthlCompetitorsInEventRec.merking1, AthlCompetitorsInEventRec.merking2,
                            AthlCompetitorsInEventRec.merking3, AthlCompetitorsInEventRec.merking4, AthlCompetitorsInEventRec.merking5,
                            AthlCompetitorsInEventRec.merking6, AthlCompetitorsInEventRec.sortorder1, AthlCompetitorsInEventRec.sortorder2,
                            CurrValueInt, "", AthlCompetitorsInEventRec.samasaetiognaestiaundan,
                            AthlCompetitorsInEventRec.athugasemd, AthlCompetitorsInEventRec.ridillnumer, 
                            AthlCompetitorsInEventRec.stokkkastrod, AthlCompetitorsInEventRec.IAAF_Stig,
                            AthlCompetitorsInEventRec.thrautarstig, AthlCompetitorsInEventRec.Unglingastig, 
                            AthlCompetitorsInEventRec.PerformaceRemarks);
                        NeedToUpdateOrderByResults = true;
                    }

                }
                //if (item is CheckBox)
                //{
                //    CheckBox c1 = (CheckBox)PH1.FindControl(item.ID);
                //    CheckBoxID = c1.ID;
                //    CurrChecked = (Boolean)(c1.Checked);
                //    ControlNameParts = CheckBoxID.Split('_');
                //    LineNoForCheckBox = Convert.ToInt32(ControlNameParts[1]);
                //}
                //if ((LineNoForTextBox > 0) && (LineNoForCheckBox > 0) && (LineNoForTextBox == LineNoForCheckBox))
                //{
                //    if (CurrChecked == true)
                //    {
                //        CurrCheckedInteger = 1;
                //    }
                //    else
                //    {
                //        CurrCheckedInteger = 0;
                //    }
                //    string Test = gl.GetArr1Value(LineNoForTextBox);
                //    Test = gl.GetArr2Value(LineNoForCheckBox);
                //    Test = CurrChecked.ToString();
                //    bool res = Int32.TryParse(CurrValue, out CurrValueInt);
                //    if (res == false)
                //    {
                //        CurrValueInt = 0;
                //    }
                //    if ((CurrValueInt.ToString() != gl.GetArr1Value(LineNoForTextBox)) || (CurrCheckedInteger.ToString() != gl.GetArr2Value(LineNoForCheckBox)))
                //    {
                //        AthlCompetitorsInEventRec = AthlCRUD.GetCompetitorInEvent(CompCode, BibNo, EventLineNo);
                //        AthlCRUD.UpdCompetitorInEvent(AthlCompetitorsInEventRec.mot, AthlCompetitorsInEventRec.greinarnumer,
                //            AthlCompetitorsInEventRec.rasnumer, AthlCompetitorsInEventRec.timi, AthlCompetitorsInEventRec.metrar,
                //            AthlCompetitorsInEventRec.vindur, AthlCompetitorsInEventRec.arangur,
                //            AthlCompetitorsInEventRec.tilraun1, AthlCompetitorsInEventRec.vindur1,
                //            AthlCompetitorsInEventRec.tilraun2, AthlCompetitorsInEventRec.vindur2,
                //            AthlCompetitorsInEventRec.tilraun3, AthlCompetitorsInEventRec.vindur3,
                //            AthlCompetitorsInEventRec.tilraun4, AthlCompetitorsInEventRec.vindur4,
                //            AthlCompetitorsInEventRec.tilraun5, AthlCompetitorsInEventRec.vindur5,
                //            AthlCompetitorsInEventRec.tilraun6, AthlCompetitorsInEventRec.vindur6,
                //            AthlCompetitorsInEventRec.seria,
                //            AthlCompetitorsInEventRec.rafmagnstimataka, AthlCompetitorsInEventRec.merking1, AthlCompetitorsInEventRec.merking2,
                //            AthlCompetitorsInEventRec.merking3, AthlCompetitorsInEventRec.merking4, AthlCompetitorsInEventRec.merking5,
                //            AthlCompetitorsInEventRec.merking6, AthlCompetitorsInEventRec.sortorder1, AthlCompetitorsInEventRec.sortorder2,
                //            CurrValueInt, "", CurrCheckedInteger,
                //            AthlCompetitorsInEventRec.athugasemd);
                //        NeedToUpdateOrderByResults = true;
                //    }
                //    LineNoForCheckBox = -1;
                //    LineNoForTextBox = -1;
                //}
            }
   //         if (NeedToUpdateOrderByResults == true)
           // {
            //    gl.UpdateResultOrderForEvent(CompCode, EventLineNo, AthlCompEvent.tegundgreinar, "%");
//                AthlCRUD.UpdateResultOrder(CompCode, EventLineNo, AthlCompEvent.tegundgreinar);
     //       }
            Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNoText);
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            string EventLineNoText = Request.QueryString.Get("Event");
            Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNoText);
        }


    }
}