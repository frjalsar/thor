using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MotFRI
{

    public partial class NyttMot : System.Web.UI.Page
    {
        string AccessLevelText = "";
        Int32 ColNoForPrizeCeremony = 0;
        bool SetPrizeCermonyColumnVisible = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Global Gl = new Global();

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
                    }
                    Dagsetning.Text = InnslDags;
                    GamlaDags.Text = InnslDags;
                    HeitiMots.Focus();
                }

            }
            else  //Not Postback - Page Load
            {
                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "0") || (AccessLevelText == "") || (AccessLevelText == null))
                {
                    NewCompetitorButton.Visible = false;
                    YourCompetitors.Visible = false;
                    AddNewCompetitorsBtn.Visible = false;
                    SetupEvents.Visible = false;
                    Vista.Visible = false;
                    HeitiMots.ReadOnly = true;
                    EnsktHeiti.ReadOnly = true;
                    Dagsetning.ReadOnly = true;
                    //Vellir.ReadOnly = true;
                    Stadur.ReadOnly = true;
                    UtiEdaInni.ReadOnly = true;
                    Motshaldari.ReadOnly = true;
                    Yfirdomari.ReadOnly = true;
                    SkranGjaldPrGrein.ReadOnly = true;
                    SkranGjPrBodhl.ReadOnly = true;
                    SkranGjPrGreinU18.ReadOnly = true;
                    SkranGjPrBodhlU18.ReadOnly = true;
                }

                if (Gl.GetCompetitionCode() != "")
                {
                    {
                        CompetitionCode.Text = Gl.GetCompetitionCode();
                        CompCode2.Text = CompetitionCode.Text;
                        CompCode4.Text = CompetitionCode.Text;
                        CompCode7.Text = CompetitionCode.Text;                        

                        AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                        Athl_Competition AthlComp = new Athl_Competition();
                        AthlComp = AthlCompCRUD.GetCompetitionRec(Gl.GetCompetitionCode());
                        HeitiMots.Text = AthlComp.Name;
                        EnsktHeiti.Text = AthlComp.ensktheitiamoti;
                        Dagsetning.Text = AthlComp.Date.ToShortDateString();
                        Gl.SetGlobalValue("CompetitionName", AthlComp.Name);
                        Gl.SetGlobalValue("EnglishCompetitionName", AthlComp.ensktheitiamoti);
                        Gl.SetOutdoorsOrIndoors(AthlComp.OutdoorsOrIndoors.ToString());

                        Int16 VenueIndex = 0;
                        System.Data.Objects.ObjectParameter Indx = new System.Data.Objects.ObjectParameter("Indx", typeof(Int32));
                        
                        AthleticsEntities1 AthlEnt = new AthleticsEntities1();

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
                        if (AthlComp.CompetitonType == 0)
                        {
                            CompetitionEventsHdr.Text = HeitiMots.Text + " - Keppnisgreinar hlaups:";
                        }
                        else
                        {
                            CompetitionEventsHdr.Text = HeitiMots.Text + " - Keppnisgreinar móts:";
                        } if (AthlComp.OutdoorsOrIndoors == 0)
                        {
                            UtiEdaInni.Text = "Utanhúss";
                        }
                        else
                        {
                            UtiEdaInni.Text = "Innanhúss";
                        }
                        Motshaldari.Text = AthlComp.Organizer;
                        Yfirdomari.Text = AthlComp.Judge;
                        SkranGjaldPrGrein.Text = string.Format("{0:N2}", AthlComp.skraningargjaldprgrein);    //AthlComp.skraningargjaldprgrein.ToString();
                        SkranGjPrBodhl.Text = string.Format("{0:N2}", AthlComp.Skráningargjld_f__boðhlaup); //.ToString();
                        SkranGjPrGreinU18.Text = string.Format("{0:N2}", AthlComp.Skráningargj__yngri_en_18_ára);
                        SkranGjPrBodhlU18.Text = string.Format("{0:N2}", AthlComp.Skráningargj__f_boðhl_y_18_ára);
                        if (AthlComp.Reikna_unglingastig == 1)
                        {
                            ReiknaUnglStig.Text = "Já";
                        }
                        else
                        {
                            ReiknaUnglStig.Text = "Nei";
                        }
                        if (AthlComp.Reikna_IAAF_stig == 1)
                        {
                            ReiknaIAAFStig.Text = "Já";
                        }
                        else
                        {
                            ReiknaIAAFStig.Text = "Nei";
                        }

                        Gl.SetCompetitionCode(AthlComp.Code);
                        Gl.SetCompetitionName(AthlComp.Name);
                        Gl.SetCompetionYear(AthlComp.Date.Year);
                        if (AthlComp.keppnisvollur != "")
                        {
                            Gl.SetCompetitionVenue(AthlComp.keppnisvollur + ", " + AthlComp.Location);
                        }
                        else
                        {
                            Gl.SetCompetitionVenue(AthlComp.Location);
                        }

                        Int32 NoOfDates = 0;
                        ListItem newItem = new ListItem();
                        newItem = new ListItem();
                        newItem.Text = "Allir dagar";
                        newItem.Value = "%";
                        EventDateFilterDropDown.Items.Add(newItem);
                        //AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                        var DatesInCompetition = AthlEnt.ReturnCompetitionEventDates(Gl.GetCompetitionCode());
                        foreach (var result in DatesInCompetition)
                        {
                            NoOfDates = NoOfDates + 1;
                            newItem = new ListItem();
                            newItem.Text = Convert.ToString(result); // "Dagur 1"; // Convert.ToString(result.Dags);
                            newItem.Value = Convert.ToString(result) + "%"; // "D1%"; // Convert.ToString(result.Dags) + "%";
                            EventDateFilterDropDown.Items.Add(newItem);
                        }


                        if (NoOfDates <= 1)
                        {
                            EventDateFilterLabel.Visible = false;
                            EventDateFilterDropDown.Visible = false;
                        }
                        else
                        {
                            EventDateFilterLabel.Visible = true;
                            EventDateFilterDropDown.Visible = true;
                        }

                    }
                    HeitiMots.Focus();
                    NyrKeppandiTbl.Visible = false;

                }
                else
                {
                    string SelNafn;
                    SelNafn = Gl.GetSelNafn();
                    if (SelNafn != "")
                    {
                        KennitText.Text = Gl.GetSelKt();
                        NafnText.Text = SelNafn;
                        FaedArText.Text = Gl.GetSelFar();
                        FelagText.Text = Gl.GetSelFel();

                        Gl.SetSelComp("", "", "", "", "", "", "", "");

                    }
                }
            }


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
            AthlComp.skraningargjaldprgrein = Convert.ToDecimal(SkranGjaldPrGrein.Text);
            AthlComp.Skráningargjld_f__boðhlaup = Convert.ToDecimal(SkranGjPrBodhl.Text);
            AthlComp.Skráningargj__yngri_en_18_ára = Convert.ToDecimal(SkranGjPrGreinU18.Text);
            AthlComp.Skráningargj__f_boðhl_y_18_ára = Convert.ToDecimal(SkranGjPrBodhlU18.Text);
            AthlComp.Reikna_unglingastig = Convert.ToByte(reiknaungl);
            AthlComp.Reikna_IAAF_stig = Convert.ToByte(reiknaIAAF);
            AthlComp.tegundstigakeppni = tegstigak;
            AthlComp.CompetitonType = 1;  //Hlaup = 0, Mót = 1

            if (CompetitionCode.Text == "")
            {
                CompetitionCode.Text = AthlCompCRUD.InsertNewRec(AthlComp);
            }
            else
            {
                AthlCompCRUD.UpdateRec(AthlComp);
            }

            Gl.SetCompetitionCode(AthlComp.Code);
            Gl.SetCompetitionName(AthlComp.Name);
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CompC = "";
            string HeitiG = "";
            Int32 index;

            Global Gl = new Global();
            GridViewRow Row = GridView3.SelectedRow;

            //HeitiG = Row.Cells[0].Text;
            //HeitiG = Row.Cells[1].Text;
            //HeitiG = Row.Cells[2].Text;
            //HeitiG = Row.Cells[3].Text;
            //HeitiG = Row.Cells[4].Text;
            //HeitiG = Row.Cells[5].Text;
            //HeitiG = Row.Cells[6].Text;
            //HeitiG = Row.Cells[7].Text;
            //HeitiG = Row.Cells[8].Text;
            //HeitiG = GridView3.DataKeys[GridView3.SelectedRow.RowIndex].Values[0].ToString();
            HeitiG = GridView3.DataKeys[GridView3.SelectedRow.RowIndex].Values[1].ToString();
            CompC = Gl.GetCompetitionCode();

            index = GetColumnIndexByName(Row, "heitigreinar");

            HeitiG = Row.Cells[index].Text;
            HeitiG = HttpUtility.HtmlDecode(HeitiG);

            Gl.SetCompetitionCode(CompC);
            index = GetColumnIndexByName(Row, "greinnumer");
            //Gl.SetCompetitionEventNo(GridView3.DataKeys[GridView3.SelectedRow.RowIndex].Values[1].ToString());
            string EventLineNoText = GridView3.DataKeys[GridView3.SelectedRow.RowIndex].Values[1].ToString();
            Gl.SetCompetitionEventName(HeitiG);  //Row.Cells[4].Text);
            index = GetColumnIndexByName(Row, "kyn");
            if (Row.Cells[index].Text.Substring(0, 1) == "B")
            {
                Gl.SetSelectedGender("0");
            }
            else
            {
                Gl.SetSelectedGender(Row.Cells[index].Text);
            }
            Gl.SetSelectedEventCode(Row.Cells[5].Text);
            CompCode5.Text = Gl.GetCompetitionCode();
            CompCode5.Text = HttpUtility.HtmlDecode(CompCode5.Text);

            //EvenNo.Text = Gl.GetCompetitionEventNo();
            EvenNo.Text = EventLineNoText;
            SelGender.Text = Gl.GetSelectedGender();
            EventCode.Text = Gl.GetSelectedEventCode();
            EventCode.Text = HttpUtility.HtmlDecode(EventCode.Text);
            Response.Redirect("EventResults.aspx?Event=" + EventLineNoText);
        }

        //protected Int16 ReturnIndexOfVenue(string VenueName)
        //{

            
        //    Int16 RetCode = 0;
        //    SqlConnection conn = new SqlConnection("Data Source=Localhost;Initial Catalog=Athletics;Integrated Security=True");
        //    SqlCommand cmd = new SqlCommand("IndexCompetitionVenue", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlParameter paramReturnValue = new SqlParameter();
        //    paramReturnValue.ParameterName = "@Venue";
        //    cmd.Parameters.Add("@Venue", SqlDbType.VarChar).Value = VenueName;
        //    paramReturnValue.ParameterName = "@Indx";
        //    paramReturnValue.SqlDbType = SqlDbType.Int;
        //    cmd.Parameters.Add(paramReturnValue);
        //    cmd.Parameters["@Indx"].Direction = ParameterDirection.Output;
        //    conn.Open();

        //    RetCode = Convert.ToInt16(cmd.ExecuteScalar());
        //    string VenueIxText = paramReturnValue.Value.ToString();
        //    conn.Close();
        //    if (Convert.ToInt32(RetCode) == 0)
        //    {
        //        Int16 VenueIx = Int16.Parse(VenueIxText);
        //        return VenueIx;
        //    }
        //    else
        //    {
        //        return 0;
        //    }

        //}

        protected void CompetitionResultsDataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ColNoPlace = 0;
            int ColNoResult = 0;
            int ColNoWind = 0;
            int ColNoName = 0;
            int ColNoHyperlink = 0;
            int ColNoYoB = 0;
            int ColNoClub = 0;
            int ColNoSeries = 0;
            int ColNoEventName = 0;
            int ColNoEventDate = 0;
            int ColNoLineType = 0;
            int ColNoCompetitorCode = 0;
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            GridViewRow CurrRow;
            CurrRow = e.Row;            
            Global gl = new Global();

 //select Place, Result, Wind, Name, Club, YearOfBirth, Series, EventName, convert(varchar, EventDate, 104) as EventDate, 
 //LineType, EventNo,  SortOrder, NeedsWindMeter, WindText, EntryNo, CompetitorCode
 //from @CompetitionResults order by EventNo, LineType, SortOrder, EntryNo, Place

            ColNoPlace = gl.GetColumnIndexByName(CurrRow, "Place");
            ColNoResult = gl.GetColumnIndexByName(CurrRow, "Result");
            ColNoWind = gl.GetColumnIndexByName(CurrRow, "WindText");
            ColNoName = gl.GetColumnIndexByName(CurrRow, "Name");
            ColNoHyperlink = ColNoName + 1;
            ColNoYoB = gl.GetColumnIndexByName(CurrRow, "YearOfBirth");
            ColNoClub = gl.GetColumnIndexByName(CurrRow, "Club");
            ColNoSeries = gl.GetColumnIndexByName(CurrRow, "Series");
            ColNoEventName = gl.GetColumnIndexByName(CurrRow, "EventName");
            ColNoEventDate = gl.GetColumnIndexByName(CurrRow, "EventDate");
            ColNoLineType = gl.GetColumnIndexByName(CurrRow, "LineType");
            ColNoCompetitorCode = gl.GetColumnIndexByName(CurrRow, "CompetitorCode");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AthlEnt.InsertToLogFile("Vindur í línu ", e.Row.Cells[ColNoWind].Text);

                if (e.Row.Cells[ColNoLineType].Text == "1")  //Event Name and Date
                {
                    e.Row.Cells[ColNoPlace].Text = e.Row.Cells[ColNoEventName].Text + " - " + e.Row.Cells[ColNoEventDate].Text; //EventName and EventDate
                    e.Row.Cells[ColNoPlace].Font.Bold = true;
                    e.Row.Cells[ColNoPlace].Font.Italic = true;
                    e.Row.Cells[ColNoPlace].Font.Size = 14;
                    e.Row.Cells[ColNoPlace].ColumnSpan = 6;
                    e.Row.Cells[ColNoResult].Text = null;
                    e.Row.Cells[ColNoWind].Text = null;
                    e.Row.Cells[ColNoName].Text = null;
                    e.Row.Cells[ColNoYoB].Text = null;
                    e.Row.Cells[ColNoClub].Text = null;
                    e.Row.Cells[ColNoSeries].Text = null;
                    e.Row.Cells[ColNoLineType].Text = null;
                    e.Row.Cells[ColNoEventName].Text = null;
                    e.Row.Cells[ColNoEventDate].Text = null;
                    e.Row.Cells[ColNoCompetitorCode].Text = null;
                }
                else
                {
                    //e.Row.Cells[4].Text = e.Row.Cells[ColNoName].Text;
                    e.Row.Cells[ColNoLineType].Text = null;
                    e.Row.Cells[ColNoEventName].Text = null;
                    e.Row.Cells[ColNoEventDate].Text = null;
                    e.Row.Cells[ColNoCompetitorCode].Text = null;
                    e.Row.Cells[ColNoName].Text = null;

                }
            }
            else
            {
                e.Row.Cells[ColNoEventName].Text = null;
                e.Row.Cells[ColNoEventDate].Text = null;
                e.Row.Cells[ColNoLineType].Text = null;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            NyrKeppandiTbl.Visible = true;
            NyrKeppandiTbl.Focus();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteCompetitorInComp")
            {
                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "1") || (AccessLevelText == "2"))
                {
                    Global gl = new Global();
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow CurrentRow = GridView1.Rows[index];
                    string CompCode = GridView1.DataKeys[0].Value.ToString();
                    int IndexOfBibno = gl.GetColumnIndexByName(CurrentRow, "rasnumer");
                    int BibNo = Convert.ToInt32(CurrentRow.Cells[IndexOfBibno].Text);

                    if ((CompCode != "") & (BibNo > 0))
                    {
                        AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                        Athl_CompetitorsInCompetition Competitor = new Athl_CompetitorsInCompetition();
                        Competitor = AthlCompCRUD.GetCompetitorInComp(CompCode, BibNo);
                        AthlCompCRUD.DeleteCompetitorInCompetition(Competitor);
                        GridView1.DataBind();
                    }
                }
            }

            if (e.CommandName == "RegisterEvents")
            {
                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "1") || (AccessLevelText == "2"))
                {
                    Global gl = new Global();
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow CurrentRow = GridView1.Rows[index];
                    string CompCode = GridView1.DataKeys[0].Value.ToString();
                    int IndexOfBibno = gl.GetColumnIndexByName(CurrentRow, "rasnumer");
                    int BibNo = Convert.ToInt32(CurrentRow.Cells[IndexOfBibno].Text);

                    if ((CompCode != "") & (BibNo > 0))
                    {
                        AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                        Athl_CompetitorsInCompetition Competitor = new Athl_CompetitorsInCompetition();
                        Competitor = AthlCompCRUD.GetCompetitorInComp(CompCode, BibNo);
                        Int32 YearOfBirth;
                        YearOfBirth = Competitor.faedingarar;
                        Int32 CompetitorAge = gl.GetCompetitionYear() - YearOfBirth;

                        gl.SetSelComp(CompCode, Competitor.kennitala, Competitor.nafn, Competitor.kyn.ToString(),
                            Competitor.faedingarar.ToString(), Competitor.felag, CompetitorAge.ToString(), "0");
                        gl.SetBibNumber(BibNo);

                        Response.Redirect("RegisterCompetitorInEvent.aspx");
                    }
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

            Response.Redirect("CompetitionEvents.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selecedcell = GridView1.SelectedRow.Cells[0].Text;
            selecedcell = GridView1.SelectedRow.Cells[1].Text;
            selecedcell = GridView1.SelectedRow.Cells[2].Text;
            selecedcell = GridView1.SelectedRow.Cells[3].Text;

            selecedcell = GridView1.SelectedRow.Cells[4].Text;

        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string[] WordsArray;
            string AgeGr;
            Int32 Gend;
            Int32 AgeFr;
            Int32 AgeTo;
            Int32 ColNo;
                                              
            string EventLineNoText = "";
            bool UserIsSignedin = false;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "0") || (AccessLevelText == "") || (AccessLevelText == null))
                {
                    UserIsSignedin = false;
                    //ModifyDateTime.Visible = false;
                    SetPrizeCermonyColumnVisible = false;
                }
                else
                {
                    UserIsSignedin = true;
                    AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                    Athl_Competition AthlComp = new Athl_Competition();
                    if (CompCode4.Text != "")
                    {
                        AthlComp = AthlCompCRUD.GetCompetitionRec(CompetitionCode.Text);
                        if (AthlComp.DisplayColumnForPrizeCeremony == 1)
                        {
                            SetPrizeCermonyColumnVisible = true;
                        }
                    }
                }
                ColNoForPrizeCeremony = GetColumnIndexByName(e.Row, "tilkynnaverdlaunaafhendingu");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int RowIx = e.Row.RowIndex;
                EventLineNoText = GridView3.DataKeys[e.Row.RowIndex].Values[1].ToString();
                
                ColNo = GetColumnIndexByName(e.Row, "ShowCompetitors");
                HyperLink hp = new HyperLink();
                
                hp.Text = "Keppendur"; //e.Row.Cells[ColNo].Text;
                hp.NavigateUrl = "UpdateEventResults.aspx?Event=" + EventLineNoText; 
               
                e.Row.Cells[ColNo].Controls.Add(hp);

                ColNo = GetColumnIndexByName(e.Row, "Aldursflokkur");
                AgeGr = e.Row.Cells[ColNo].Text;
                WordsArray = AgeGr.Split(';');
                Gend = Convert.ToInt32(WordsArray[0]);
                AgeFr = Convert.ToInt32(WordsArray[1]);
                AgeTo = Convert.ToInt32(WordsArray[2]);

                switch (Gend)
                {
                    case 1:
                        if ((AgeFr == 0) & ((AgeTo == 0) | (AgeTo == 999)))
                        {
                            AgeGr = "Karlar";
                        }
                        else
                        {
                            if (AgeFr == 0)
                            {
                                if (AgeTo > 22)
                                {
                                    AgeGr = "Karlar";
                                }
                                else
                                {
                                    AgeGr = "Piltar " + AgeTo.ToString() + " og yngri";
                                }
                            }
                            else
                            {
                                if (AgeTo == 999)
                                {
                                    AgeGr = "Karlar " + AgeFr.ToString() + " og eldri";
                                }
                                else
                                {
                                    if (AgeFr == AgeTo)
                                    {
                                        AgeGr = "Piltar " + AgeFr.ToString();
                                    }
                                    else
                                    {
                                        AgeGr = "Piltar " + AgeFr.ToString() + "-" + AgeTo.ToString();
                                    }
                                }
                            }

                        }
                        break;
                    case 2:
                        if ((AgeFr == 0) && ((AgeTo == 0) || (AgeTo == 999)))
                        {
                            AgeGr = "Konur";
                        }
                        else
                        {
                            if (AgeFr == 0)
                            {
                                if (AgeTo > 22)
                                {
                                    AgeGr = "Konur";
                                }
                                else
                                {
                                    AgeGr = "Stúlkur " + AgeTo.ToString() + " og yngri";
                                }
                            }
                            else
                            {
                                if (AgeTo == 999)
                                {
                                    AgeGr = "Konur " + AgeFr.ToString() + " og eldri";
                                }
                                else
                                {
                                    if (AgeFr == AgeTo)
                                    {
                                        AgeGr = "Stúlkur " + AgeFr.ToString();
                                    }
                                    else
                                    {
                                        AgeGr = "Stúlkur " + AgeFr.ToString() + "-" + AgeTo.ToString();
                                    }
                                }
                            }
                        }
                        break;
                }
                //e.Row.Cells[ColNo].Text = AgeGr;
            }
            if (SetPrizeCermonyColumnVisible == false)
            {
                e.Row.Cells[ColNoForPrizeCeremony].Visible = false;
                e.Row.Cells[ColNoForPrizeCeremony + 1].Visible = false;
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string HeitiG = "";
            Global gl = new Global();

            if (e.CommandName == "DeleteCompEvent")  //DELETE THE COMPETITION EVENT  - NOT USED ANY MORE
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];
                string CompCode = (string)this.GridView3.DataKeys[index]["mot"];
                Int32 EventLineNo = (Int32)this.GridView3.DataKeys[index]["greinnumer"];

                if ((CompCode != "") & (EventLineNo > 0))
                {
                    AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                    Athl_CompetitionEvents AthlCompetitionEvent = new Athl_CompetitionEvents();
                    AthlCompetitionEvent = AthlCompCRUD.GetCompetitionEvent(CompCode, EventLineNo);
                    AthlCompCRUD.DeleteCompetitonEvent(AthlCompetitionEvent);
                    GridView3.DataBind();
                }
            }

            if (e.CommandName == "ShowCompetitors")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];
                string CompCode = (string)this.GridView3.DataKeys[index]["mot"];
                Int32 EventLineNo = (Int32)this.GridView3.DataKeys[index]["greinnumer"];

                //gl.SetCompetitionEventNo(GridView3.DataKeys[index].Values[1].ToString());
                string EventLineNoText = GridView3.DataKeys[index].Values[1].ToString();

                index = GetColumnIndexByName(CurrentRow, "heitigreinar");
                HeitiG = CurrentRow.Cells[index].Text;
                HeitiG = HttpUtility.HtmlDecode(HeitiG);
                gl.SetCompetitionEventName(HeitiG);

                Response.Redirect("CompetitorsInEvent.aspx?Event=" + EventLineNoText);
            }

            if (e.CommandName == "EnterEventResults")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];

                string CompCode = (string)this.GridView3.DataKeys[index]["mot"];
                Int32 EventLineNo = (Int32)this.GridView3.DataKeys[index]["greinnumer"];

                //gl.SetCompetitionEventNo(GridView3.DataKeys[index].Values[1].ToString());
                string EventLineNoText = GridView3.DataKeys[index].Values[1].ToString();

                index = GetColumnIndexByName(CurrentRow, "heitigreinar");
                HeitiG = CurrentRow.Cells[index].Text;
                HeitiG = HttpUtility.HtmlDecode(HeitiG);
                gl.SetCompetitionEventName(HeitiG);

                Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNoText);

            }

            if (e.CommandName == "ShowResults")
            {
                string EventCode = "";
                string Gend;
                string EventStatus = "";
                string EventLineNoText = "";
                Int32 index;

                index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];
                EventLineNoText = GridView3.DataKeys[index].Values[1].ToString();
                //gl.SetCompetitionEventNo(EventLineNoText);                

                index = GetColumnIndexByName(CurrentRow, "heitigreinar");
                HeitiG = CurrentRow.Cells[index].Text;
                HeitiG = HttpUtility.HtmlDecode(HeitiG);
                gl.SetCompetitionEventName(HeitiG);
                index = GetColumnIndexByName(CurrentRow, "greinnumer");
                EventCode = CurrentRow.Cells[index].Text;
                //gl.SetSelectedEventCode(EventCode);
                gl.SetSelectedEventCode(EventLineNoText);

                index = GetColumnIndexByName(CurrentRow, "kyn");
                if (CurrentRow.Cells[index].Text.Substring(0, 1) == "B")
                {
                    gl.SetSelectedGender("0");
                }
                else
                {
                    Gend = CurrentRow.Cells[index].Text;
                    if (Gend == "Karl")
                    {
                        gl.SetSelectedGender("1");
                    }
                    else
                    {
                        gl.SetSelectedGender("2");
                    }

                }
                index = GetColumnIndexByName(CurrentRow, "StaðaKeppni");
                EventStatus = CurrentRow.Cells[index].Text;

                if (EventStatus == "Stendur yfir")
                {
                    Response.Redirect("EventProgressLiveUpdate.aspx?EventLineNo=" +
                          EventLineNoText + "&EventName=" + HeitiG);
                }
                else
                {
                    Response.Redirect("PrintEventResults.aspx?Event=" + EventLineNoText);
                }
            }

            if (e.CommandName == "PrizeCeremonyFinished")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow CurrentRow = GridView3.Rows[index];

                string CompCode = (string)this.GridView3.DataKeys[index]["mot"];
                Int32 EventLineNo = (Int32)this.GridView3.DataKeys[index]["greinnumer"];
                string EventLineNoText = GridView3.DataKeys[index].Values[1].ToString();
                EventLineNo = Convert.ToInt32(EventLineNoText);

                AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
                Athl_CompetitionEvents CompEvent = new Athl_CompetitionEvents();
                if ((CompCode != "") && (EventLineNo > 0))
                {
                    CompEvent = AthlCompCRUD.GetCompetitionEvent(CompCode, EventLineNo);
                    AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                    AthlEnt.UpdateEventDateTimeAndStatus(CompCode, EventLineNo, CompEvent.dagsetning, CompEvent.timi,
                          CompEvent.stadakeppni, 2);  //Set Prize Ceremony has finished for this Event
                    Int32 ColNoForPrizeCeremonyStatus = 0;
                    ColNoForPrizeCeremonyStatus = GetColumnIndexByName(CurrentRow, "tilkynnaverdlaunaafhendingu");
                    CurrentRow.Cells[ColNoForPrizeCeremonyStatus].Text = "Lokið";
                }
            }
        }

        int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                {
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                }
                if (cell.ContainingField is TemplateField)
                {
                    if (((TemplateField)cell.ContainingField).HeaderText.Equals(columnName))
                        break;
                }
                if (cell.ContainingField is HyperLinkField)
                {
                    // System.Web.UI.WebControls.HyperLink h = new HyperLink();

                    // h = cell.ContainingField;
                    string CtrlTxt = cell.Text;
                    CtrlTxt = cell.ContainingField.AccessibleHeaderText;

                    //if (((HyperLinkField)cell.ContainingField).AccessibleHeaderText.Equals(columnName))
                    if (CtrlTxt == columnName)
                    {
                        break;
                    }
                }
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompetitorsInCompetition.aspx");
        }

        protected void CompCode4_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Points_Click(object sender, EventArgs e)
        {
            Response.Redirect("PointsStanding.aspx");
        }

        protected void YourCompetitors_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClubCompetitorsWithReg.aspx");
        }

        protected void SearchForCompetitorsBtn_Click(object sender, EventArgs e)
        {
            Global Gl = new Global();
            Gl.SetGlobalValue("SelectedKennitala", KennitText.Text);
            Gl.SetGlobalValue("SelectedName", NafnText.Text);
            Gl.SetGlobalValue("SelectedYearOfBirth", FaedArText.Text);
            Response.Redirect("~/SelectCompetitor.aspx");

        }
        protected void ModifyDateTime_Click(object sender, EventArgs e)
        {
            //EKKI NOTAÐ LENGUR - Var notað í GridViw3
            bool RefreshPage = false;
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Global gl = new Global();

            string CompCode = gl.GetCompetitionCode();
            string NewDateText = "";
            string NewTimeText = "";
            Int32 CurrentRowNo = 0;
            Int32 CurrentEventLineNo = 0;
            Int32 NoOfEvents = GridView3.Rows.Count;
            DateTime NewDate;
            DateTime NewTime;
            DateTime EmptyDateTime = DateTime.ParseExact("17530101 00:00:00", "yyyyMMdd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            for (CurrentRowNo = 0; CurrentRowNo < NoOfEvents; CurrentRowNo++)
            {
                CurrentEventLineNo = (Int32)this.GridView3.DataKeys[CurrentRowNo]["greinnumer"];
                TextBox EventDateTextBox = (TextBox)GridView3.Rows[CurrentRowNo].FindControl("DagsGreinar");
                NewDateText = EventDateTextBox.Text;
                TextBox EventTimeTextBox = (TextBox)GridView3.Rows[CurrentRowNo].FindControl("TimiGreinar");
                NewTimeText = EventTimeTextBox.Text;
                NewDate = gl.TryConvertStringToDate(NewDateText);
                NewTime = gl.TryConvertStringToTime(NewTimeText);
                if ((NewDate > EmptyDateTime) && (NewTime > EmptyDateTime))
                {
                    AthlEvent = AthlCRUD.GetCompetitionEvent(CompCode, CurrentEventLineNo);
                    if ((NewDate.ToString("d") != AthlEvent.dagsetning.ToString("d")) || (NewTime.ToShortTimeString() != AthlEvent.timi.ToShortTimeString()))
                    {
                        AthlEvent.dagsetning = NewDate;
                        AthlEvent.timi = NewTime;
                        AthlEnt.UpdateEventDateTimeAndStatus(CompCode, CurrentEventLineNo, NewDate, NewTime, AthlEvent.stadakeppni,
                            AthlEvent.tilkynnaverdlaunaafhendingu);
                        RefreshPage = true;
                    }
               }
            }
            if (RefreshPage == true)
            {
                Response.Redirect("NyttMot.aspx" + "?Comp=" + CompCode);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
                 AccessLevelText = Session["CurrentAccessLevel"].ToString();
                 if ((AccessLevelText == "0") || (AccessLevelText == "") || (AccessLevelText == null))
                 {
                     e.Row.Cells[0].Visible = false;
                     e.Row.Cells[1].Visible = false;
                 }
        }


    }

}