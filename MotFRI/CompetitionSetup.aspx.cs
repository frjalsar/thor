using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data.Objects;
using System.Globalization;

namespace MotFRI
{
    public partial class CompetitionSetup : System.Web.UI.Page
    {
        static Athl_Competition AthlComp = new Athl_Competition();
        static Athl_Venues AthlVenueRec = new Athl_Venues();
        static bool NewCompetition = false;
        static Int32 NoOfDays;
        public string AccessLevelText;
        public int AccessLevelint;
        public int CanEnterScores;
        public string CurrentUserID;
        public string CurrCompCode;
        public Global gl = new Global();
        public AthleticsEntities1 AthlEnt = new AthleticsEntities1();
        protected void Page_Load(object sender, EventArgs e)
        {
            CopyEventsFromPrevComp.Visible = false;  //DEBUG

            if (Session["CurrentUserName"] == null)
            {
                CurrentUserID = "";
            }
            else
            {
                CurrentUserID = Session["CurrentUserName"].ToString();
            }
            CurrCompCode = gl.GetCompetitionCode();
            string CompCodeText = Request.QueryString.Get("Code");
            NewCompetition = false;
            if ((CompCodeText != "") && (CompCodeText != null))
            {
                gl.SetCompetitionCode(CompCodeText);
                CurrCompCode = CompCodeText;
                if (CurrCompCode == "NYTT")
                {
                    NewCompetition = true;
                }
            }

            System.Data.Objects.ObjectParameter CurrAccessLevel = new System.Data.Objects.ObjectParameter("CurrentAccessLevel", "0");
            System.Data.Objects.ObjectParameter CurrCanEnterScores = new System.Data.Objects.ObjectParameter("CanEnterScores", "0");

            AthlEnt.GetAccessLevel(CurrentUserID, CurrCompCode, CurrAccessLevel, CurrCanEnterScores);
            AccessLevelint = Convert.ToInt32(CurrAccessLevel.Value);
            CanEnterScores = Convert.ToInt32(CurrCanEnterScores.Value);

            //--AccessLevel
            //--
            //--0 = Queries Only
            //--1 = Club Representative
            //--2 = Administrator
            //--3 = Selected Competition Only
            //
            //--    CanEnterScores 0 = No, 1 = Yes

            if (IsPostBack)
            {
                Message.Text = "";
                DateTime DateValue;
                string ValinnV = Convert.ToString(Vellir.SelectedValue);
                string InnslDags = Dagsetning.Text;
                if (ValinnV != GamliVollur.Text)
                {
                    MotADO ado = new MotADO();

                    ado.GeymaVallarupplys(ValinnV);
                    Stadur.Text = ado.ValinnStadur;
                    UtiEdaInni.Text = ado.ValidUtiInni;

                    if (UtiEdaInni.Text == "0")
                    {
                        UtiEdaInni.Text = "Utanhúss";
                    }
                    if (UtiEdaInni.Text == "1")
                    {
                        UtiEdaInni.Text = "Innanhúss";
                    }
                    if (UtiEdaInni.Text == "2")
                    {
                        UtiEdaInni.Text = "Utan- eða Innanhúss";
                    }


                    GamliVollur.Text = ValinnV.ToString();
                }


                if (InnslDags != GamlaDags.Text)
                {

                    string[] separators = { ".", "/", " " };
                    string[] words = InnslDags.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    if (words[0].Length == 1)
                    {
                        words[0] = "0" + words[0];
                    }
                    if (words[1].Length == 1)
                    {
                        words[1] = "0" + words[1];
                    }
                    if (words[2].Length == 1)
                    {
                        words[2] = "200" + words[2];
                    }
                    else if (words[2].Length == 2)
                    {
                        words[2] = "20" + words[2];
                    }

                    InnslDags = words[0] + "." + words[1] + "." + words[2];
                    // if (words[0].Length >  
                    if (InnslDags.Length == 8)
                    {

                        InnslDags = InnslDags.Substring(0, 2) + "." + InnslDags.Substring(2, 2) + "." + InnslDags.Substring(4, 4);
                        HeitiMots.Focus();
                    }
                    else
                        if (InnslDags.Length == 7)
                    {
                        InnslDags = "0" + InnslDags.Substring(0, 1) + "." + InnslDags.Substring(1, 2) + "." + InnslDags.Substring(3, 4);
                    }
                    else
                            if (InnslDags.Length == 6)
                    {
                        InnslDags = InnslDags.Substring(0, 2) + "." + InnslDags.Substring(2, 2) + ".20" + InnslDags.Substring(4, 2);
                    }
                    else
                                if (InnslDags.Length == 5)
                    {
                        InnslDags = "0" + InnslDags.Substring(0, 1) + "." + InnslDags.Substring(1, 2) + ".20" + InnslDags.Substring(3, 2);
                    }
                    else
                                    if (InnslDags.Length == 4)
                    {
                        InnslDags = InnslDags.Substring(0, 2) + "." + InnslDags.Substring(2, 2) + "." + DateTime.Now.ToString("yyyy");
                    }
                    else
                                        if (InnslDags.Length == 3)
                    {
                        InnslDags = "0" + InnslDags.Substring(0, 1) + "." + InnslDags.Substring(1, 2) + "." + DateTime.Now.ToString("yyyy");
                    }
                    else
                                            if (InnslDags.Length == 2)
                    {
                        InnslDags = InnslDags + "." + DateTime.Now.ToString("MM.yyyy");
                    }
                    else
                                                if (InnslDags.Length == 1)
                    {
                        InnslDags = "0" + InnslDags + "." + DateTime.Now.ToString("MM.yyyy");
                    }

                    if (!DateTime.TryParse(InnslDags, out DateValue))
                    {
                        Message.Text = "Dagsetning " + InnslDags + " er ekki í lagi.";
                    }
                    else
                    {
                        InnslDags = DateValue.ToShortDateString();
                        NoOfDays = 1;
                    }
                    Dagsetning.Text = InnslDags;
                    GamlaDags.Text = InnslDags;
                    HeitiMots.Focus();
                }
                if (CompetitionDate2.Text != "")
                {
                    if (!DateTime.TryParse(CompetitionDate2.Text, out DateValue))
                    {
                        Message.Text = "Dagsetning 2 " + CompetitionDate2.Text + " er ekki í lagi.";
                    }
                    else
                    {
                        AthlComp.Date2 = DateValue;
                        NoOfDays = 2;
                    }
                }
                else
                {
                    AthlComp.Date2 = Convert.ToDateTime("1753-01-01 00:00:00.000");
                    CompetitionDate3.Text = "";
                    CompetitionDate4.Text = "";
                }
                if (CompetitionDate3.Text != "")
                {
                    if (!DateTime.TryParse(CompetitionDate3.Text, out DateValue))
                    {
                        Message.Text = "Dagsetning 3 " + CompetitionDate3.Text + " er ekki í lagi.";
                    }
                    else
                    {
                        AthlComp.Date3 = DateValue;
                        NoOfDays = 3;
                    }
                }
                else
                {
                    AthlComp.Date3 = Convert.ToDateTime("1753-01-01 00:00:00.000");
                    CompetitionDate4.Text = "";
                }
                if (CompetitionDate4.Text != "")
                {
                    if (!DateTime.TryParse(CompetitionDate4.Text, out DateValue))
                    {
                        Message.Text = "Dagsetning 4 " + CompetitionDate4.Text + " er ekki í lagi.";
                    }
                    else
                    {
                        AthlComp.dagsetning4 = DateValue;
                        NoOfDays = 4;
                    }
                }
                else
                {
                    AthlComp.dagsetning4 = Convert.ToDateTime("1753-01-01 00:00:00.000");
                }
                AthlComp.tegundstigakeppni = gl.TryConvertStringToInt32(TegundStigakeppni.SelectedValue.ToString());

            }
            else  //Not Postback - Page Load
            {

                //AccessLevelText = Session["CurrentAccessLevel"].ToString();
                //if ((AccessLevelText == "0") || (AccessLevelText == "") || (AccessLevelText == null))
                if ((AccessLevelint != 1) && (AccessLevelint != 2)) //Access is NOT Club Repesentative and NOT Administrator, Competition Creator or CompetitionUpdateAccess = Administrator
                {
                    //NewCompetitorButton.Visible = false;
                    //YourCompetitors.Visible = false;
                    //AddNewCompetitorsBtn.Visible = false;
                    SetupEvents.Visible = false;
                    //  Vista.Visible = false;
                    HeitiMots.ReadOnly = true;
                    EnsktHeiti.ReadOnly = true;
                    Dagsetning.ReadOnly = true;
                    Vellir.Enabled = false;
                    Stadur.ReadOnly = true;
                    UtiEdaInni.ReadOnly = true;
                    Motshaldari.ReadOnly = true;
                    Yfirdomari.ReadOnly = true;
                    SkranGjaldPrGrein.ReadOnly = true;
                    SkranGjPrBodhl.ReadOnly = true;
                    SkranGjPrGreinU18.ReadOnly = true;
                    SkranGjPrBodhlU18.ReadOnly = true;
                    CompStatus.Enabled = false;
                    TegundStigakeppni.Enabled = false;
                    ReiknaIAAFStig.Enabled = false;
                    ReiknaUnglStig.Enabled = false;
                    Message.Text = "Þú hefur ekki leyfi til að breyta uppsetningu mótsins";
                    Vista.Visible = false;
                }
                else
                {
                    Message.Text = "";
                }

                if ((gl.GetCompetitionCode() != "") && (gl.GetCompetitionCode() != "NYTT") && (NewCompetition == false))
                {
                    {
                        //SetupEvents.Visible = true;

                        CompetitionCode.Text = gl.GetCompetitionCode();
                        //CompCode2.Text = CompetitionCode.Text;
                        //CompCode4.Text = CompetitionCode.Text;
                        //CompCode7.Text = CompetitionCode.Text;

                        AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                        AthlComp = AthlCompCRUD.GetCompetitionRec(gl.GetCompetitionCode());
                        HeitiMots.Text = AthlComp.Name;
                        EnsktHeiti.Text = AthlComp.ensktheitiamoti;
                        Dagsetning.Text = AthlComp.Date.ToShortDateString();
                        gl.SetGlobalValue("CompetitionName", AthlComp.Name);
                        gl.SetGlobalValue("EnglishCompetitionName", AthlComp.ensktheitiamoti);
                        UtiEdaInni.Text = AthlComp.OutdoorsOrIndoors.ToString();
                        gl.SetOutdoorsOrIndoors(UtiEdaInni.Text);

                        Int16 VenueIndex = 0;
                        System.Data.Objects.ObjectParameter Indx = new System.Data.Objects.ObjectParameter("Indx", typeof(Int32));

                        AthlEnt.IndexCompetitionVenue(AthlComp.keppnisvollur, Indx);
                        if (Int16.TryParse(Indx.Value.ToString(), out VenueIndex) == false)
                        {
                            VenueIndex = 0;
                        }
                        //VenueIndex = ReturnIndexOfVenue(AthlComp.keppnisvollur);

                        if (VenueIndex > 0)
                        {
                            Vellir.SelectedIndex = VenueIndex;
                        }

                        Stadur.Text = AthlComp.Location;
                        if (UtiEdaInni.Text == "0")
                        {
                            UtiEdaInni.Text = "Utanhúss";
                        }
                        if (UtiEdaInni.Text == "1")
                        {
                            UtiEdaInni.Text = "Innanhúss";
                        }

                        CompStatus.SelectedIndex = AthlComp.Staða_móts;
                        TegundStigakeppni.SelectedValue = AthlComp.tegundstigakeppni.ToString();
                        Motshaldari.Text = AthlComp.Organizer;
                        Yfirdomari.Text = AthlComp.Judge;
                        Motsstjori.Text = AthlComp.Director;
                        SkranGjaldPrGrein.Text = string.Format("{0:N2}", AthlComp.skraningargjaldprgrein);    //AthlComp.skraningargjaldprgrein.ToString();
                        SkranGjPrBodhl.Text = string.Format("{0:N2}", AthlComp.Skráningargjld_f__boðhlaup); //.ToString();
                        SkranGjPrGreinU18.Text = string.Format("{0:N2}", AthlComp.Skráningargj__yngri_en_18_ára);
                        SkranGjPrBodhlU18.Text = string.Format("{0:N2}", AthlComp.Skráningargj__f_boðhl_y_18_ára);
                        //if (AthlComp.Reikna_unglingastig == 1)
                        //{
                        //    ReiknaUnglStig.Text = "Já";
                        //}
                        //else
                        //{
                        //    ReiknaUnglStig.Text = "Nei";
                        //}
                        ReiknaUnglStig.SelectedValue = AthlComp.Reikna_unglingastig.ToString();
                        //if (AthlComp.Reikna_IAAF_stig == 1)
                        //{
                        //    ReiknaIAAFStig.Text = "Já";
                        //}
                        //else
                        //{
                        //    ReiknaIAAFStig.Text = "Nei";
                        //}
                        ReiknaIAAFStig.SelectedValue = AthlComp.Reikna_IAAF_stig.ToString();

                        gl.SetCompetitionCode(AthlComp.Code);
                        gl.SetCompetitionName(AthlComp.Name);
                        gl.SetCompetionYear(AthlComp.Date.Year);
                        if (AthlComp.keppnisvollur != "")
                        {
                            gl.SetCompetitionVenue(AthlComp.keppnisvollur + ", " + AthlComp.Location);
                        }
                        else
                        {
                            gl.SetCompetitionVenue(AthlComp.Location);
                        }

                        Int32 NoOfDates = 0;
                        ListItem newItem = new ListItem();
                        newItem = new ListItem();
                        newItem.Text = "Allir dagar";
                        newItem.Value = "%";
                        //AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                        var DatesInCompetition = AthlEnt.ReturnCompetitionEventDates(gl.GetCompetitionCode());
                        foreach (var result in DatesInCompetition)
                        {
                            NoOfDates = NoOfDates + 1;
                            newItem = new ListItem();
                            newItem.Text = Convert.ToString(result); // "Dagur 1"; // Convert.ToString(result.Dags);
                            newItem.Value = Convert.ToString(result) + "%"; // "D1%"; // Convert.ToString(result.Dags) + "%";
                        }
                        if (AthlComp.Date2 > Convert.ToDateTime("1900.01.01"))
                        {
                            CompetitionDate2.Visible = true;
                            CompetitionDate2.Text = AthlComp.Date2.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                        }
                        if (AthlComp.Date3 > Convert.ToDateTime("1900.01.01"))
                        {
                            CompetitionDate3.Visible = true;
                            CompetitionDate3.Text = AthlComp.Date3.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                        }
                        if (AthlComp.dagsetning4 > Convert.ToDateTime("1900.01.01"))
                        {
                            CompetitionDate4.Visible = true;
                            CompetitionDate4.Text = AthlComp.dagsetning4.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                        }

                    }
                    HeitiMots.Focus();

                }
                else
                {
                    string SelNafn;
                    SelNafn = gl.GetSelNafn();
                    SetupEvents.Visible = false;
                    CopyEventsFromPrevComp.Visible = false;
                    UpdateEventSetup.Visible = false;
                    CompStatus.SelectedIndex = 1;
                    Vista.Text = "Stofna";
                }
            }

        }

        protected void SetupEvents_Click(object sender, EventArgs e)
        {
            if ((Stadur.Text == "") || (UtiEdaInni.Text == ""))
            {
                Message.Text = "Þú verður að velja keppnisvöll!";
                return;
            }

            Global gl = new Global();

            if (UtiEdaInni.Text == "Utanhúss")
            {
                gl.SetOutdoorsOrIndoors("0");
            }
            else
            {
                gl.SetOutdoorsOrIndoors("1");
            }

            gl.SetSelectedDate(Dagsetning.Text);

            Response.Redirect("CompetitionEvents.aspx?Code=" + CompetitionCode.Text);

        }

        protected void VistaNyttMot_Click(object sender, EventArgs e)
        {
            Global Gl = new Global();
            bool reiknaungl = false;
            bool reiknaIAAF = false;
            DateTime Dags;
            bool dagsOk = false;
            Int16 tegstigak = -1;
            string valinnv = "";
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();

            if (HeitiMots.Text == String.Empty)
            {
                Message.Text = "Þú verður að fylla út heiti mótsins";
                return;
            }
            if (EnsktHeiti.Text == String.Empty)
            {
                Message.Text = "Þú ættir að fylla út enska heiti mótsins";
                //return;
            }
            dagsOk = DateTime.TryParse(Dagsetning.Text, out Dags);
            if (!dagsOk)
            {
                Message.Text = "Dagsetning er ekki í lagi.";
                return;
            }
            valinnv = Convert.ToString(Vellir.SelectedValue);
            if (valinnv == String.Empty)
            {
                Message.Text = "Þú verður að velja keppnisvöllinn";
                return;
            }
            if (Motshaldari.Text == String.Empty)
            {
                Message.Text = "Þú verður að fylla út upplýsingar um mótshaldarann";
                return;
            }
            if (Yfirdomari.Text == String.Empty)
            {
                Message.Text = "Þú ættir að fylla út heiti yfirdómarans";
                //return;
            }
            if (SkranGjaldPrGrein.Text == String.Empty)
            {
                SkranGjaldPrGrein.Text = "0";
            }
            if (SkranGjPrBodhl.Text == String.Empty)
            {
                SkranGjPrBodhl.Text = "0";
            }
            if (ReiknaUnglStig.SelectedValue == "1")
            {
                reiknaungl = true;
            }
            else
            {
                reiknaungl = false;
            }
            if (ReiknaIAAFStig.SelectedValue == "1")
            {
                reiknaIAAF = true;
            }
            else
            {
                reiknaIAAF = false;
            }
            tegstigak = Convert.ToInt16(TegundStigakeppni.SelectedValue.ToString());

            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
            Athl_Competition AthlComp = new Athl_Competition();
            if (CompetitionCode.Text == "")
            {
                AthlComp = AthlCompCRUD.InitAthlComp();
            }
            else
            {
                AthlComp = AthlCompCRUD.GetCompetitionRec(CompetitionCode.Text);
            }

            AthlComp.Name = HeitiMots.Text;
            AthlComp.ensktheitiamoti = EnsktHeiti.Text;
            AthlComp.Date = Dags;
            dagsOk = DateTime.TryParse(CompetitionDate2.Text, out Dags);
            if (dagsOk)
            {
                AthlComp.Date2 = Dags;
            }
            dagsOk = DateTime.TryParse(CompetitionDate3.Text, out Dags);
            if (dagsOk)
            {
                AthlComp.Date3 = Dags;
            }
            dagsOk = DateTime.TryParse(CompetitionDate4.Text, out Dags);
            if (dagsOk)
            {
                AthlComp.dagsetning4 = Dags;
            }
            AthlComp.keppnisvollur = valinnv;
            AthlComp.Location = Stadur.Text;
            if (UtiEdaInni.Text == "Utanhúss")
            {
                AthlComp.OutdoorsOrIndoors = 0;
            }
            else
            {
                AthlComp.OutdoorsOrIndoors = 1;
            }
            AthlComp.Organizer = Motshaldari.Text.ToUpper();
            AthlComp.Judge = Yfirdomari.Text;
            AthlComp.Director = Motsstjori.Text;
            AthlComp.skraningargjaldprgrein = Convert.ToDecimal(SkranGjaldPrGrein.Text);
            AthlComp.Skráningargjld_f__boðhlaup = Convert.ToDecimal(SkranGjPrBodhl.Text);
            AthlComp.Skráningargj__yngri_en_18_ára = Convert.ToDecimal(SkranGjPrGreinU18.Text);
            AthlComp.Skráningargj__f_boðhl_y_18_ára = Convert.ToDecimal(SkranGjPrBodhlU18.Text);
            AthlComp.Reikna_unglingastig = Convert.ToByte(reiknaungl);
            AthlComp.Reikna_IAAF_stig = Convert.ToByte(reiknaIAAF);
            AthlComp.tegundstigakeppni = tegstigak;
            AthlComp.Staða_móts = Convert.ToInt32(CompStatus.SelectedIndex.ToString());
            AthlComp.CompetitonType = 1;  //Hlaup = 0, Mót = 1
            AthlVenueRec = AthlCRUD.GetVenueRec(valinnv);
            if ((CompetitionCode.Text == "") || (CompetitionCode.Text == "NYTT"))
            {
                CompetitionCode.Text = AthlCompCRUD.InsertNewRec(AthlComp);
                gl.SetCompetitionCode(CompetitionCode.Text);
                AthlEnt.InsertCompUpdAccess(CompetitionCode.Text, CurrentUserID, 1, 1);
                SetupEvents.Visible = true;
            }
            else
            {
                AthlCompCRUD.UpdateRec(AthlComp);
            }

            Gl.SetCompetitionCode(AthlComp.Code);
            Gl.SetCompetitionName(AthlComp.Name);
            Response.Redirect("CompetitionSetup.aspx?Code=" + AthlComp.Code);
        }

        protected void UpdateEventSetup_Click(object sender, EventArgs e)
        {
            if (AthlComp.Code == "")
            {
                Message.Text = "Þú verður fyrst að stofna mótið áður en þú getur breytt tímaseðlinum.";
            }
            else
              Response.Redirect("UpdCompEvents.aspx?Code=" + AthlComp.Code);
        }

        protected void CopyEventsFromPrevComp_Click(object sender, EventArgs e)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            var CompetitionDatesFromEvents = AthlEnt.ReturnCompetitionDates(AthlComp.Code);
            Int32 NoOfDates = 0;
            DateTime LastCompetitionEventDate;
            LastCompetitionEventDate = Convert.ToDateTime("1900-01-01");
            foreach (var CompD in CompetitionDatesFromEvents)
            {
                if (CompD.FirstDate != null)
                {
                    NoOfDates = NoOfDates + 1;
                    LastCompetitionEventDate = Convert.ToDateTime(CompD.FirstDate.ToString());
                }
            }
            if ((NoOfDates > 0) && (LastCompetitionEventDate > Convert.ToDateTime("1900-01-01")))
            {
                Message.Text = "Ekki er hægt að afrita greinar frá öðru móti vegna þess að það eru " +
                    "þegar keppnisgreinar komnar fyrir þetta mót.";

            }
            else
            {
                Response.Redirect("CopyEventsFromExisting.aspx?Code=" + 
                    AthlComp.Code + "&InOut=" + AthlComp.OutdoorsOrIndoors.ToString());
            }
        }
    }
}