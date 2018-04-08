using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace MotFRI
{
    public partial class ReadLynxFiles : System.Web.UI.Page
    {
        public Int32 WindReadingRequired = 0;
        public decimal WindReading = 0;
        public Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();
        public Athl_Competition AthlCompetitionRec = new Athl_Competition();

        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();


            if (!IsPostBack)
            {
                AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                CompCode.Text = gl.GetCompetitionCode();
                CompetitionName.Text = gl.GetCompetitionName();
                EventName.Text = gl.GetCompetitonEventName();
                EventLineNo.Text = Request.QueryString.Get("EventLineNo");
                AthlEvent = AthlCRUD.GetCompetitionEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text));
                AthlCompetitionRec = AthlCRUD.GetCompetitionRec(CompCode.Text);
                Int32 TrackEventNoForLynx = AthlEvent.Númer_hlaupagreinar_f__Lynx + 1000;
                WindReadingRequired = AthlEvent.krefstvindmaelis;
                LifFilePrefix.Text = TrackEventNoForLynx.ToString().Substring(1, 3);
                SelectFileInfo.Text = LifFilePrefix.Text + " : ";
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            string line;
            AthleticCompetitionCRUD AthlCRUD2 = new AthleticCompetitionCRUD();

            HttpFileCollection File_Collection = Request.Files;
            string BibNos = "";
            string RunnerLanes = "";
            Global gl = new Global();
            string Times = "";
            Int32 LineNo = 0;
            Int32 CurrentHeatNo = 1;
            string ImportedHeatNo = "";
            string ImportedWind = "";
            WindReading = 0;
            EventLineNo.Text = Request.QueryString.Get("EventLineNo");
            AthlEvent = AthlCRUD2.GetCompetitionEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text));
            Int32 TrackEventNoForLynx = AthlEvent.Númer_hlaupagreinar_f__Lynx + 1000;
            WindReadingRequired = AthlEvent.krefstvindmaelis;

            foreach (string File_Uploader in File_Collection)
            {
                HttpPostedFile Posted_File = File_Collection[File_Uploader];
                string FileNameWithoutPath = "";
                Int32 LastSlashPos = 0;
                for (int ix = 0; ix < Posted_File.FileName.Length; ix++)
                {
                    if (Posted_File.FileName.Substring(ix, 1) == @"\")
                    {
                        LastSlashPos = ix;
                    }
                }
                if (LastSlashPos > 0)
                {
                    FileNameWithoutPath = Posted_File.FileName.Substring(LastSlashPos + 1);
                }
                else
                {
                    FileNameWithoutPath = Posted_File.FileName;
                }
                if (FileNameWithoutPath.Substring(0, 3) == LifFilePrefix.Text)
                {
                    if (Posted_File.ContentLength > 0)
                    {

                        System.IO.StreamReader file = new System.IO.StreamReader(Posted_File.InputStream);
                        while ((line = file.ReadLine()) != null)
                        {  //Example: 5,0,1,"100M Karla R1 w:-0,1",-1.0,M/S S,,,,100,18:34:25.1698  - Með vindi
                           //Example: 2,2,1,60M S15 R1,,,,,,60,10:01:00.4149                        - Án vinds

                            string[] Fields = line.Split(',');
                            if (LineNo == 0)
                            {
                                string EventNameText = Fields[3]; //Example: "60m p12 R2" or "100M S13 R1/2" or "4X100M S13"
                                string HeatText = EventNameText.Split(' ').Last();
                                if (HeatText.StartsWith("R"))
                                {
                                    string[] HeatTextArr = HeatText.Split('/');
                                    HeatText = HeatTextArr[0].Substring(1);
                                    CurrentHeatNo = gl.TryConvertStringToInt32(HeatText);
                                }
                                else
                                {
                                    CurrentHeatNo = 1;
                                }
                                if (WindReadingRequired == 1)
                                {
                                    if (Fields.Length == 12)
                                    {
                                        WindReading = gl.TryConvertStringToDecimal(Fields[5]);
                                    } 
                                    else
                                    {
                                        WindReading = gl.TryConvertStringToDecimal(Fields[4]);
                                    }
                                    //WindReading = gl.TryConvertStringToDecimal(Fields[4]);
                                    WindReading = WindReading / 10;
                                }
                                else
                                {
                                    WindReading = 0;
                                }
                            }
                            else
                            {
                                Int32 Rank = 0;
                                bool SuccessFul = Int32.TryParse(Fields[0], out Rank);
                                if (SuccessFul)
                                {
                                    BibNos = BibNos + "," + Fields[1];
                                    Times = Times + "," + Fields[6];
                                    RunnerLanes = RunnerLanes + "," + Fields[2];
                                }
                                else
                                {
                                    BibNos = BibNos + "," + Fields[1];
                                    Times = Times + "," + Fields[0];
                                    RunnerLanes = RunnerLanes + "," + Fields[2];

                                }
                                //6,2,2,60m p12 R2,-1.0,M/S W,,,,60,11:39:33.7908
                                //1,179,4,Finnsson,Birnir Vagn,UFA,8.942,UFA,8.942,,,11:39:33.791,,,,8.942,8.942
                                //2,4,5,Eysteinsson,Dagur,AFTURE,9.266,AFTURE,0.324,,,11:39:33.791,,,,0.324,0.324
                                //3,156,3,Olafsson,Karl Hakon,IR,9.702,IR,0.436,,,11:39:33.791,,,,0.436,0.436
                                //4,142,2,Ingolfsson,Ari,HST,10.242,HST,0.540,,,11:39:33.791,,,,0.540,0.540
                                //DNS,188,6,Sigurdarson,Starkadur,UFA,,UFA,,,,11:39:33.791,,,,,
                                //DNS,125,7,Khorchai,Jens Johnny,HSK-SELFOS,,HSK-SELFOS,,,,11:39:33.791,,,,,
                            }
                            LineNo = LineNo + 1;
                        }
                        file.Close();

                        if (BibNos.Substring(0, 1) == ",")
                        {
                            BibNos = BibNos.Substring(1);
                            Times = Times.Substring(1);
                            RunnerLanes = RunnerLanes.Substring(1);
                        }
                        //TextBox1.Text = BibNos;
                        //TextBox2.Text = Times;

                        System.Data.Objects.ObjectParameter MsgOut = new System.Data.Objects.ObjectParameter("MessageOut", typeof(string));
                        AthlEnt.UpdateTimesInTrackEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text), BibNos, Times, RunnerLanes, CurrentHeatNo, WindReading, MsgOut);
                        if (MsgOut.Value.ToString() == "")
                        {
                            Int32 EventTypeInteger = 1;
                            AthlEnt.UpdateOrderAndPointsByResults(CompCode.Text, Convert.ToInt32(EventLineNo.Text), EventTypeInteger);

                            if ((AthlEvent.thrautargrein == 1) && (AthlEvent.nanaritegundargreining != 7))
                            {
                                AthlEnt.ProcessMultiEventCompetitors(CompCode.Text, Convert.ToInt32(EventLineNo.Text));
                                Int32 LineNoOfMultiEv = 0;
                                System.Data.Objects.ObjectParameter LineNoOfMultiEvParameter = new System.Data.Objects.ObjectParameter("LineNoOfMultiEvent", "0");
                                //AthlEntities.ReturnNoOfHeatsInEvent(CompCode, EventLin, OutNoOfHeats);
                                AthlEnt.ReturnLineNumberOfMultiEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text), LineNoOfMultiEvParameter);
                                LineNoOfMultiEv = Convert.ToInt32(LineNoOfMultiEvParameter.Value);
                                AthlEnt.UpdateOrderAndPointsByResults(CompCode.Text, LineNoOfMultiEv, 2);
                            }
                            if ((AthlEvent.stigagrein == 1) && (AthlCompetitionRec.tegundstigakeppni == 5))
                            {
                                AthlEnt.CalculatePointsInEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text));
                            }
                            Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNo.Text);
                        }
                        else
                        {
                            string msg = MsgOut.Value.ToString();
                            Response.Write("<script>alert('" + msg + "')</script>");
                        }
                    }
                }
                else
                {
                    string msg = string.Format("Þú verður að velja .lif skrá sem byrjar á {0}", LifFilePrefix.Text);
                    Response.Write("<script>alert('" + msg + "')</script>");

                }


            }
        }

    }

}


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Data.Objects;

//namespace MotFRI
//{
//    public partial class ReadLynxFiles : System.Web.UI.Page
//    {
//        public Int32 WindReadingRequired = 0;
//        public decimal WindReading = 0;
//        public Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();
//        public Athl_Competition AthlCompetitionRec = new Athl_Competition();

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            Global gl = new Global();
//            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();


//            if (!IsPostBack)
//            {
//                CompCode.Text = gl.GetCompetitionCode();
//                CompetitionName.Text = gl.GetCompetitionName();
//                EventName.Text = gl.GetCompetitonEventName();
//                EventLineNo.Text = Request.QueryString.Get("EventLineNo");
//                AthlEvent = AthlCRUD.GetCompetitionEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text));
//                AthlCompetitionRec = AthlCRUD.GetCompetitionRec(CompCode.Text);
//                Int32 TrackEventNoForLynx = AthlEvent.Númer_hlaupagreinar_f__Lynx + 1000;
//                WindReadingRequired = AthlEvent.krefstvindmaelis;          
//                LifFilePrefix.Text = TrackEventNoForLynx.ToString().Substring(1, 3);
//                SelectFileInfo.Text = "Veldu .lif skrá sem byrjar á " + LifFilePrefix.Text + ": ";
//            }
//        }

//        protected void Button1_Click1(object sender, EventArgs e)
//        {
//            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
//            string line;

//            HttpFileCollection File_Collection = Request.Files;
//            string BibNos = "";
//            string Times = "";
//            Int32 LineNo = 0;
//            WindReading = 0;

//            foreach (string File_Uploader in File_Collection)
//            {
//                HttpPostedFile Posted_File = File_Collection[File_Uploader];
//                string FileNameWithoutPath = "";
//                Int32 LastSlashPos = 0;
//                for (int ix = 0; ix < Posted_File.FileName.Length; ix++)
//                {
//                    if (Posted_File.FileName.Substring(ix, 1) == @"\")
//                    {
//                        LastSlashPos = ix;
//                    }
//                }
//                if (LastSlashPos > 0)
//                {
//                    FileNameWithoutPath = Posted_File.FileName.Substring(LastSlashPos + 1);
//                }
//                else
//                {
//                    FileNameWithoutPath = Posted_File.FileName;
//                }
//                if (FileNameWithoutPath.Substring(0, 3) == LifFilePrefix.Text)
//                {
//                    if (Posted_File.ContentLength > 0)
//                    {

//                        System.IO.StreamReader file = new System.IO.StreamReader(Posted_File.InputStream);
//                        while ((line = file.ReadLine()) != null)
//                        {
//                            string[] Fields = line.Split(',');
//                            if (LineNo == 0)
//                            {
//                                if (WindReadingRequired == 1)
//                                {
//                                    WindReading = Convert.ToDecimal(Fields[4]);
//                                }
//                            }
//                            else
//                            {
//                                Int32 Rank = 0;
//                                bool SuccessFul = Int32.TryParse(Fields[0], out Rank);
//                                if (SuccessFul)
//                                {
//                                    BibNos = BibNos + "," + Fields[1];
//                                    Times = Times + "," + Fields[6];
//                                }
//                                else
//                                {
//                                    BibNos = BibNos + "," + Fields[1];
//                                    Times = Times + "," + Fields[0];

//                                }
//                                //6,2,2,60m p12 R2,-1.0,M/S W,,,,60,11:39:33.7908
//                                //1,179,4,Finnsson,Birnir Vagn,UFA,8.942,UFA,8.942,,,11:39:33.791,,,,8.942,8.942
//                                //2,4,5,Eysteinsson,Dagur,AFTURE,9.266,AFTURE,0.324,,,11:39:33.791,,,,0.324,0.324
//                                //3,156,3,Olafsson,Karl Hakon,IR,9.702,IR,0.436,,,11:39:33.791,,,,0.436,0.436
//                                //4,142,2,Ingolfsson,Ari,HST,10.242,HST,0.540,,,11:39:33.791,,,,0.540,0.540
//                                //DNS,188,6,Sigurdarson,Starkadur,UFA,,UFA,,,,11:39:33.791,,,,,
//                                //DNS,125,7,Khorchai,Jens Johnny,HSK-SELFOS,,HSK-SELFOS,,,,11:39:33.791,,,,,
//                            }
//                            LineNo = LineNo + 1;
//                        }
//                        file.Close();

//                        if (BibNos.Substring(0, 1) == ",")
//                        {
//                            BibNos = BibNos.Substring(1);
//                            Times = Times.Substring(1);
//                        }
//                        //TextBox1.Text = BibNos;
//                        //TextBox2.Text = Times;

//                        System.Data.Objects.ObjectParameter MsgOut = new System.Data.Objects.ObjectParameter("MessageOut", typeof(string));
//                        AthlEnt.UpdateTimesInTrackEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text), BibNos, Times, MsgOut);
//                        if (MsgOut.Value.ToString() == "")
//                        {
//                            Int32 EventTypeInteger = 1;
//                            AthlEnt.UpdateOrderAndPointsByResults(CompCode.Text, Convert.ToInt32(EventLineNo.Text), EventTypeInteger);

//                            if ((AthlEvent.thrautargrein == 1) && (AthlEvent.nanaritegundargreining != 7))
//                            {
//                                AthlEnt.ProcessMultiEventCompetitors(CompCode.Text, Convert.ToInt32(EventLineNo.Text));
//                                Int32 LineNoOfMultiEv = 0;
//                                System.Data.Objects.ObjectParameter LineNoOfMultiEvParameter = new System.Data.Objects.ObjectParameter("LineNoOfMultiEvent", "0");
//                                //AthlEntities.ReturnNoOfHeatsInEvent(CompCode, EventLin, OutNoOfHeats);
//                                AthlEnt.ReturnLineNumberOfMultiEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text), LineNoOfMultiEvParameter);
//                                LineNoOfMultiEv = Convert.ToInt32(LineNoOfMultiEvParameter.Value);
//                                AthlEnt.UpdateOrderAndPointsByResults(CompCode.Text, LineNoOfMultiEv, 2);
//                            }
//                            if ((AthlEvent.stigagrein == 1) && (AthlCompetitionRec.tegundstigakeppni == 5))
//                            {
//                                AthlEnt.CalculatePointsInEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text));
//                            }
//                            Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNo.Text);
//                        }
//                        else
//                        {
//                            string msg = MsgOut.Value.ToString();
//                            Response.Write("<script>alert('" + msg + "')</script>");
//                        }
//                    }
//                }
//                else
//                {
//                    string msg = string.Format("Þú verður að velja .lif skrá sem byrjar á {0}", LifFilePrefix.Text);
//                    Response.Write("<script>alert('" + msg + "')</script>");

//                }


//            }
//        }

//    }

//}
