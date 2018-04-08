using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using System.Transactions;




namespace MotFRI
{
    public partial class CompetitionEvents : System.Web.UI.Page
    {
        public string LastAgeGroupMen;
        public string LastAgeGroupWomen;

        // Sjá betur í http://www.aspsnippets.com/Articles/Select-and-delete-multiple-rows-in-ASP.Net-Gridview-control.aspx
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global gl = new Global();
                OutdoorsOrIndoors.Text = gl.GetOutdorrsOrIndoors();
                DateForEvents.Text = gl.GetSelectedDate();
                
                
            }
            else
            {
                Global gl = new Global();
                OutdoorsOrIndoors.Text = gl.GetOutdorrsOrIndoors();    
            }
        }
        
        protected void CompetitionEventsMale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
               DataRowView drv = e.Row.DataItem as DataRowView;
               if (e.Row.RowType == DataControlRowType.DataRow)
               {
                   if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                   {
                       RadioButtonList rbtnl = (RadioButtonList)e.Row.FindControl("SelectRounds");
                       rbtnl.SelectedValue = drv[5].ToString();
                       CheckBoxList chkb = (CheckBoxList)e.Row.FindControl("MaleSel");
                       chkb.SelectedValue = drv[6].ToString();
                   }
               }
       }

        protected void StandardTextGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Global gl = new Global();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string CurrAgeGr = e.Row.Cells[1].Text;
                if (CurrAgeGr != LastAgeGroupMen)
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[1].Font.Italic = true;
                    e.Row.Cells[1].Font.Size = 14;
                    LastAgeGroupMen = CurrAgeGr;
                }
                else
                {
                    e.Row.Cells[1].Text = "";
                }
                string[] SplitTextArr = e.Row.Cells[7].Text.Split(';');
                e.Row.Cells[5].Text = SplitTextArr[1];
                e.Row.Cells[6].Text = SplitTextArr[2];
                gl.SetArr1Value(NormalEventsMenGrid.Rows.Count, SplitTextArr[0]); //e.Row.Cells[7].Text);  //Save Age Group Code
                e.Row.Cells[7].Text = "";  //Do not show Age Group Code

            }
        }

        protected void NormalEventsWomenGridiew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Global gl = new Global();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string CurrAgeGr = e.Row.Cells[1].Text;
                if (CurrAgeGr != LastAgeGroupWomen)
                {
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[1].Font.Italic = true;
                    e.Row.Cells[1].Font.Size = 14;
                    LastAgeGroupWomen = CurrAgeGr;
                }
                else
                {
                    e.Row.Cells[1].Text = "";
                }

                string[] SplitTextArr = e.Row.Cells[7].Text.Split(';');
                e.Row.Cells[5].Text = SplitTextArr[1];
                e.Row.Cells[6].Text = SplitTextArr[2];
                gl.SetArr2Value(NormalEventsWomenGrid.Rows.Count, SplitTextArr[0]); //e.Row.Cells[7].Text);  //Save Age Group Code
                e.Row.Cells[7].Text = "";  //Do not show Age Group Code                            
            }
        }
        
        protected void VistaButton_Click(object sender, EventArgs e)
        {
            Global gl = new Global();
            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
            Athl_Competition AthlComp = new Athl_Competition();
            Athl_Venues AthlVenueRec = new Athl_Venues();
            Athl_CompetitionEvents AthlCompEvents = new Athl_CompetitionEvents();
            Athl_CompetitionEvents AthlCompEvents2 = new Athl_CompetitionEvents();
            Athl_CompetitionEvents AthlCompEvents3 = new Athl_CompetitionEvents();
            Athl_CompetitionEvents AthlCompEvents4 = new Athl_CompetitionEvents();
            Athl_Events AthlEvents = new Athl_Events();
                    
            Message.Text = "";

            string WrkText = "";
            string HeitiAldursflokks = "";
            string HeitiAldursflokks2 = "";
            string WrkCompetitionEventName = "";
            CheckBox SelectedEvent;
            Int16 i = 0;
            Int32 Gend = 0;
            Int32 AgeFr = 0;
            Int32 AgeTo = 0;
            Int32 LastLineNo = -1;
            Int32 UtiInniInt;
            UtiInniInt = Convert.ToInt32(gl.GetOutdorrsOrIndoors());

            
            AthlComp = AthlCompCRUD.GetCompetitionRec(gl.GetCompetitionCode());
            AthlVenueRec = AthlCompCRUD.GetVenueRec(AthlComp.keppnisvollur);

            DropDownList UmfDropD;

           // using (TransactionScope scope = new TransactionScope())


            for (i = 0; i < NormalEventsMenGrid.Rows.Count; i++)  //Mens Events
            {
                SelectedEvent = (CheckBox)NormalEventsMenGrid.Rows[i].FindControl("ValinChk");
                if (SelectedEvent.Checked)
                {

                    ((CheckBox)NormalEventsMenGrid.Rows[i].FindControl("ValinChk")).Checked = false; 

                    AthlCompEvents = AthlCompCRUD.InitCompetitionEvent();
                    AthlCompEvents.mot = gl.GetCompetitionCode();
                    if (LastLineNo == -1)
                    {
                        LastLineNo = AthlCompCRUD.ReturnNextEventinCompLineNo(gl.GetCompetitionCode());
                    }
                    else
                    {
                        LastLineNo = LastLineNo + 10000;
                    }
                    AthlCompEvents.lina = LastLineNo;
                    WrkText = NormalEventsMenGrid.DataKeys[i].Value.ToString();
                    string[] KeyParts = WrkText.Split(';');
                    AthlCompEvents.grein = KeyParts[0];
                    AthlCompEvents.kyn = Convert.ToInt32(KeyParts[1]); //Male or female
                    AthlCompEvents.flokkur = KeyParts[2];
                    WrkText = NormalEventsMenGrid.Rows[i].Cells[2].Text;  //Name of Event
                    WrkCompetitionEventName = HttpUtility.HtmlDecode(WrkText);
                    AthlCompEvents.heitigreinar = WrkText;
                    AthlCompEvents.dagsetning = Convert.ToDateTime(DateForEvents.Text); //gl.GetSelectedDate());
                    WrkText = gl.GetArr1Value(i);  //Age Group Code
                    AthlCompCRUD.ReturnAgeGroupInfo(WrkText,
                          out Gend,
                          out AgeFr,
                          out AgeTo,
                          out HeitiAldursflokks,
                          out HeitiAldursflokks2);

                    AthlCompEvents.kyn = Gend;
                    AthlCompEvents.aldurfra = AgeFr;
                    AthlCompEvents.aldurtil = AgeTo;
                    WrkCompetitionEventName = WrkCompetitionEventName + " " + HeitiAldursflokks2;
                    AthlCompEvents.heitigreinar = WrkCompetitionEventName;
                    AthlEvents = AthlCompCRUD.GetAthlEvent(AthlCompEvents.grein, AthlCompEvents.kyn, AthlCompEvents.flokkur, UtiInniInt);
                    AthlCompEvents.tegundgreinar = AthlEvents.Tegund_greinar;
                    AthlCompEvents.nanaritegundargreining = AthlEvents.Nánari_tegundargreining;
                    AthlCompEvents.skraningargjald = AthlComp.skraningargjaldprgrein;
                    AthlCompEvents.stadakeppni = 0;
                    AthlCompEvents.takngreinar = AthlCompEvents.grein + "," + AthlCompEvents.kyn.ToString() + "," + AthlCompEvents.aldurfra.ToString();
                    AthlCompEvents.rodiafrekaskra = (Int32)AthlEvents.Röð_í_afrekaskrá;
                    AthlCompEvents.krefstvindmaelis = AthlEvents.Krefst_vindmælis;
                    AthlCompEvents.tharfadradakeppendum = 1;
                    //if (AthlEvents.Tegund_greinar == 1)
                    //{
                    //    if (AthlEvents.Teg__hlaups_v__fj__brauta == 1)  //Bein braut
                    //    {
                    //        AthlCompEvents.fjoldiibrauta = AthlVenueRec.Fjöldi_beinna_brauta;
                    //        if (AthlCompEvents.fjoldiibrauta == 0)
                    //        {
                    //            AthlCompEvents.fjoldiibrauta = 6;
                    //        }
                    //    }
                    //    if (AthlEvents.Teg__hlaups_v__fj__brauta == 2)  //200m, 300m, 400m
                    //    {
                    //        AthlCompEvents.fjoldiibrauta = AthlVenueRec.Fj__hringbrauta_spretthlaup;
                    //        if (AthlCompEvents.fjoldiibrauta == 0)
                    //        {
                    //            AthlCompEvents.fjoldiibrauta = 6;
                    //        }
                    //    }
                    //    if (AthlEvents.Teg__hlaups_v__fj__brauta == 3)  //Millivegalengdir
                    //    {
                    //        AthlCompEvents.fjoldiibrauta = AthlVenueRec.Fj__hringbrauta_spretthlaup * 2;
                    //        if (AthlCompEvents.fjoldiibrauta == 0)
                    //        {
                    //            AthlCompEvents.fjoldiibrauta = 12;
                    //        }
                    //    }
                    //    if (AthlEvents.Teg__hlaups_v__fj__brauta == 4)  //Langhlaup
                    //    {
                    //        AthlCompEvents.fjoldiibrauta = 20;
                    //    }
                    //}
                    AthlCompEvents.fjoldiumferda = 6;

                    if (AthlCompEvents.tegundgreinar == 1)  //Track Event
                    {
                        AthlCompEvents.rafmagnstimataka = 1;
                        //AthlEvents.Greinahópur: 
                        //0 = Spretthlaup bein braut
                        //1 = Spretthlaup hringbraut
                        //2 = Millivegalengir
                        //3 = Langhlaup
                        //4 = Grindahlaup bein braut
                        //5 = Grindahlaup hringbraut
                        //6 = Hindrun
                        //7 = Boðhlaup
                        //8 = Götuhlaup
                        //9 = Víðavangshlaup
                        //10 = Stökk
                        //11 = Köst
                        //12 = Fjölþrautir
                        //13 = Annað

                        if ((AthlEvents.Greinahópur == 0) || (AthlEvents.Greinahópur == 4))  //Short sprints and hurdles
                        {
                            AthlCompEvents.fjoldiibrauta = AthlVenueRec.Fjöldi_beinna_brauta;
                            if (AthlCompEvents.fjoldiibrauta == 0)
                            {
                                AthlCompEvents.fjoldiibrauta = 6;
                            }
                        }
                        else
                        {
                            if ((AthlEvents.Greinahópur == 1) || (AthlEvents.Greinahópur == 5) || (AthlEvents.Greinahópur == 7)) //Long sprints, long hurdles, relays
                            {
                                AthlCompEvents.fjoldiibrauta = AthlVenueRec.Fj__hringbrauta_spretthlaup;
                                if (AthlCompEvents.fjoldiibrauta == 0)
                                {
                                    AthlCompEvents.fjoldiibrauta = 6;
                                }
                            }
                            else
                            {
                                if (AthlEvents.Greinahópur == 2)  //Middle distances
                                {
                                    AthlCompEvents.fjoldiibrauta = AthlVenueRec.Fj__hingbrauta_millivegalengd;
                                    if (AthlCompEvents.fjoldiibrauta == 0)
                                    {
                                        AthlCompEvents.fjoldiibrauta = 12;
                                    }
                                }
                                else
                                {
                                AthlCompEvents.fjoldiibrauta = AthlVenueRec.Fj__hringbrauta_langhlaup;  //Long distance, steeple chase 
                                    if (AthlCompEvents.fjoldiibrauta == 0)
                                    {
                                        AthlCompEvents.fjoldiibrauta = 20;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        AthlCompEvents.fjoldiibrauta = 0;
                        AthlCompEvents.fjoldiumferda = 6;
                    }
                
                    UmfDropD = (DropDownList)NormalEventsMenGrid.Rows[i].FindControl("UmferdDropDownList");
                    WrkText = UmfDropD.SelectedValue.ToString();
                    switch (WrkText)
                    {
                        case "Úrslit":
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            break;

                        case "Undanúrslit og úrslit":
                            AthlCompEvents.heitigreinar = WrkCompetitionEventName + " - Undanúrslit";
                            AthlCompEvents.ridill = 4; //Semi-Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            AthlCompEvents2 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents2.lina = LastLineNo;
                            AthlCompEvents2.heitigreinar = WrkCompetitionEventName + " - Úrslit";
                            AthlCompEvents2.ridill = 5; //Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents2);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents2, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            break;

                        case "Riðlakeppni, undanúrslit og úrslit":
                            AthlCompEvents.heitigreinar = WrkCompetitionEventName + " - Riðlakeppni";
                            AthlCompEvents.ridill = 2; //Heats
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            AthlCompEvents3 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents3.lina = LastLineNo;
                            AthlCompEvents3.heitigreinar = WrkCompetitionEventName + " - Undanúrslit";
                            AthlCompEvents3.ridill = 4; //Semi-Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents3);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents3, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            AthlCompEvents2 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents2.lina = LastLineNo;
                            AthlCompEvents2.heitigreinar = WrkCompetitionEventName + " - Úrslit";
                            AthlCompEvents2.ridill = 5; //Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents2);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents2, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);
                            break;

                        case "Forkeppni, riðlakeppni, undanúrslit og úrslit":
                            AthlCompEvents.heitigreinar = WrkCompetitionEventName + " - Forkeppni";
                            AthlCompEvents.ridill = 1; //Preliminaries
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            AthlCompEvents4 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents4.lina = LastLineNo;
                            AthlCompEvents4.heitigreinar = WrkCompetitionEventName + " - Riðlakeppni";
                            AthlCompEvents4.ridill = 2; //Heats
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents4);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents4, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            AthlCompEvents3 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents3.lina = LastLineNo;
                            AthlCompEvents3.heitigreinar = WrkCompetitionEventName + " - Undanúrslit";
                            AthlCompEvents3.ridill = 4; //Semi-Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents3);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents3, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);


                            AthlCompEvents2 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents2.lina = LastLineNo;
                            AthlCompEvents2.heitigreinar = WrkCompetitionEventName + " - Úrslit";
                            AthlCompEvents2.ridill = 5; //Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents2);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents2, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);
                            break;
                    }


                }
            }

            for (i = 0; i < NormalEventsWomenGrid.Rows.Count; i++)  //Mens Events
            {
                SelectedEvent = (CheckBox)NormalEventsWomenGrid.Rows[i].FindControl("ValinChk");
                if (SelectedEvent.Checked)
                {
                    ((CheckBox)NormalEventsWomenGrid.Rows[i].FindControl("ValinChk")).Checked = false;

                    AthlCompEvents = AthlCompCRUD.InitCompetitionEvent();
                    AthlCompEvents.mot = gl.GetCompetitionCode();
                    if (LastLineNo == -1)
                    {
                        LastLineNo = AthlCompCRUD.ReturnNextEventinCompLineNo(gl.GetCompetitionCode());
                    }
                    else
                    {
                        LastLineNo = LastLineNo + 10000;
                    }
                    AthlCompEvents.lina = LastLineNo;
                    WrkText = NormalEventsWomenGrid.DataKeys[i].Value.ToString();
                    string[] KeyParts = WrkText.Split(';');
                    AthlCompEvents.grein = KeyParts[0];
                    AthlCompEvents.kyn = Convert.ToInt32(KeyParts[1]); //Male
                    AthlCompEvents.flokkur = KeyParts[2];
                    WrkText = NormalEventsWomenGrid.Rows[i].Cells[2].Text;  //Name of Event
                    WrkCompetitionEventName = HttpUtility.HtmlDecode(WrkText);
                    AthlCompEvents.heitigreinar = WrkText;
                    AthlCompEvents.dagsetning = Convert.ToDateTime(DateForEvents.Text); //gl.GetSelectedDate());
                    WrkText = gl.GetArr2Value(i);  //Age Group Code
                    AthlCompCRUD.ReturnAgeGroupInfo(WrkText,
                          out Gend,
                          out AgeFr,
                          out AgeTo,
                          out HeitiAldursflokks,
                          out HeitiAldursflokks2);

                    AthlCompEvents.kyn = Gend;
                    AthlCompEvents.aldurfra = AgeFr;
                    AthlCompEvents.aldurtil = AgeTo;
                    WrkCompetitionEventName = WrkCompetitionEventName + " " + HeitiAldursflokks2;
                    AthlCompEvents.heitigreinar = WrkCompetitionEventName;
                    AthlCompEvents.heitigreinar = WrkCompetitionEventName;
                    AthlComp = AthlCompCRUD.GetCompetitionRec(AthlCompEvents.mot);
                    AthlEvents = AthlCompCRUD.GetAthlEvent(AthlCompEvents.grein, AthlCompEvents.kyn, AthlCompEvents.flokkur, UtiInniInt);
                    AthlCompEvents.tegundgreinar = AthlEvents.Tegund_greinar;
                    AthlCompEvents.nanaritegundargreining = AthlEvents.Nánari_tegundargreining;
                    AthlCompEvents.skraningargjald = AthlComp.skraningargjaldprgrein;
                    AthlCompEvents.stadakeppni = 0;
                    AthlCompEvents.takngreinar = AthlCompEvents.grein + "," + AthlCompEvents.kyn.ToString() + "," + AthlCompEvents.aldurfra.ToString();
                    AthlCompEvents.rodiafrekaskra = (Int32)AthlEvents.Röð_í_afrekaskrá;
                    AthlCompEvents.tharfadradakeppendum = 1;
                    AthlCompEvents.krefstvindmaelis = AthlEvents.Krefst_vindmælis;
                    AthlCompEvents.fjoldiibrauta = 6;
                    AthlCompEvents.fjoldiumferda = 6;

                    UmfDropD = (DropDownList)NormalEventsWomenGrid.Rows[i].FindControl("UmferdDropDownList");
                    WrkText = UmfDropD.SelectedValue.ToString();
                    switch (WrkText)
                    {
                        case "Úrslit":
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);
                            break;

                        case "Undanúrslit og úrslit":
                            AthlCompEvents.heitigreinar = WrkCompetitionEventName + " - Undanúrslit";
                            AthlCompEvents.ridill = 4; //Semi-Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            AthlCompEvents2 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents2.lina = LastLineNo;
                            AthlCompEvents2.heitigreinar = WrkCompetitionEventName + " - Úrslit";
                            AthlCompEvents2.ridill = 5; //Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents2);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents2, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);
                            break;

                        case "Riðlakeppni, undanúrslit og úrslit":
                            AthlCompEvents.heitigreinar = WrkCompetitionEventName + " - Riðlakeppni";
                            AthlCompEvents.ridill = 2; //Heats
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);


                            AthlCompEvents3 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents3.lina = LastLineNo;
                            AthlCompEvents3.heitigreinar = WrkCompetitionEventName + " - Undanúrslit";
                            AthlCompEvents3.ridill = 4; //Semi-Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents3);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents3, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            AthlCompEvents2 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents2.lina = LastLineNo;
                            AthlCompEvents2.heitigreinar = WrkCompetitionEventName + " - Úrslit";
                            AthlCompEvents2.ridill = 5; //Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents2);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents2, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);
                            break;

                        case "Forkeppni, riðlakeppni, undanúrslit og úrslit":
                            AthlCompEvents.heitigreinar = WrkCompetitionEventName + " - Forkeppni";
                            AthlCompEvents.ridill = 1; //Preliminaries
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            AthlCompEvents4 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents4.lina = LastLineNo;
                            AthlCompEvents4.heitigreinar = WrkCompetitionEventName + " - Riðlakeppni";
                            AthlCompEvents4.ridill = 2; //Heats
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents4);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents4, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            AthlCompEvents3 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents3.lina = LastLineNo;
                            AthlCompEvents3.heitigreinar = WrkCompetitionEventName + " - Undanúrslit";
                            AthlCompEvents3.ridill = 4; //Semi-Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents3);
                           // EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents3, UtiInniInt);
                           // AthlCompCRUD.InsertEventInComp(EventsInComp);


                            AthlCompEvents2 = AthlCompCRUD.CopyKeyValuesBtwEvents(AthlCompEvents);
                            LastLineNo = LastLineNo + 10000;
                            AthlCompEvents2.lina = LastLineNo;
                            AthlCompEvents2.heitigreinar = WrkCompetitionEventName + " - Úrslit";
                            AthlCompEvents2.ridill = 5; //Final
                            AthlCompCRUD.InsertCompEventInCompetition(AthlCompEvents2);
                            //EventsInComp = AthlCompCRUD.CopyFromCompetitionEvent(AthlCompEvents2, UtiInniInt);
                            //AthlCompCRUD.InsertEventInComp(EventsInComp);

                            break;
                    }

                }
            }

            Response.Redirect("CompetitionSetup.aspx?Code=" + gl.GetCompetitionCode());
            //string navigateurl = "javascript:history.go(-1);";
            //Response.Redirect(navigateurl);
        }

        protected void SaveValues2_Click(object sender, EventArgs e)
        {
            VistaButton_Click(sender, e);
        }

    }
}