using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;


namespace MotFRI
{
    public class AthleticCompetitionCRUD
    {
        private AthleticsEntities1 AthlCompetitionRecord = new AthleticsEntities1();
        private AthleticsEntities1 AthlCompetitorInCompRecord = new AthleticsEntities1();
        private AthleticsEntities1 AthlEventInCompetitionRecord = new AthleticsEntities1();
        private AthleticsEntities1 AthlEventInCompRecord = new AthleticsEntities1();
        private AthleticsEntities1 AthlEvent = new AthleticsEntities1();
        private AthleticsEntities1 AthlCompetitorRecord = new AthleticsEntities1();
        private AthleticsEntities1 AthlCompetitorInEvent = new AthleticsEntities1();
        private AthleticsEntities1 AthlHeightsInHJandPV = new AthleticsEntities1();
        private AthleticsEntities1 VenuesRec = new AthleticsEntities1();

        public string InsertNewRec(Athl_Competition AthlComp)
        {
            string NewCode = "";
            MotADO MotAD = new MotADO();
            NewCode = MotAD.SkilaNaestaMotanr();

            AthlComp.Code = NewCode;
            AthlCompetitionRecord.AddToAthl_Competition(AthlComp);

            AthlCompetitionRecord.SaveChanges();

            return (NewCode);
        }

        public Athl_Competition GetCompetitionRec(string CompetitionCode)
        {

            using (AthleticsEntities1 AthlCompRec = new AthleticsEntities1())
            {
                Athl_Competition AthComp = (from AthlC in AthlCompetitionRecord.Athl_Competition
                                            where AthlC.Code == CompetitionCode
                                            select AthlC).First();
                return (AthComp);
            }

        }

        public void UpdateRec(Athl_Competition AthlComp)
        {
            string CompetitionCode = AthlComp.Code;

            using (AthleticsEntities1 AthlCompRec = new AthleticsEntities1())
            {
                Athl_Competition AthlComp2 = (from AthlC in AthlCompetitionRecord.Athl_Competition
                                              where AthlC.Code == CompetitionCode
                                              select AthlC).First();

                AthlComp2.Name = AthlComp.Name;
                AthlComp2.ensktheitiamoti = AthlComp.ensktheitiamoti;
                AthlComp2.Date = AthlComp.Date;
                AthlComp2.keppnisvollur = AthlComp.keppnisvollur;
                AthlComp2.Location = AthlComp.Location; ;
                AthlComp2.OutdoorsOrIndoors = AthlComp.OutdoorsOrIndoors;
                AthlComp2.Organizer = AthlComp.Organizer;
                AthlComp2.Judge = AthlComp.Judge;
                AthlComp2.skraningargjaldprgrein = AthlComp.skraningargjaldprgrein;
                AthlComp2.Skráningargjld_f__boðhlaup = AthlComp.Skráningargjld_f__boðhlaup;
                AthlComp2.Reikna_unglingastig = AthlComp.Reikna_unglingastig;
                AthlComp2.tegundstigakeppni = AthlComp.tegundstigakeppni;
                AthlComp2.Staða_móts = AthlComp.Staða_móts;

                AthlComp2.Skráningargj__yngri_en_18_ára = AthlComp.Skráningargj__yngri_en_18_ára;
                AthlComp2.Skráningargj__f_boðhl_y_18_ára = AthlComp.Skráningargj__f_boðhl_y_18_ára;
                AthlComp2.Reikna_IAAF_stig = AthlComp.Reikna_IAAF_stig;
                AthlComp2.CompetitonType = AthlComp.CompetitonType;
                AthlCompetitionRecord.SaveChanges();

            }


        }


        public string ReturnNextCompetitionCode()
        {

            SqlConnection objConn = new SqlConnection();
            string ConnStrToUse = AthlCompetitionRecord.Connection.ToString();
            objConn.ConnectionString = AthlCompetitionRecord.Connection.ToString();
            SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 1 Code FROM [Athletics].[dbo].[Athl$Competition] WHERE Code LIKE 'M-%' order by Code DESC", objConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Comp");
            DataTable dt = ds.Tables["Comp"];
            string NextCompCode = "";

            foreach (DataRow row in dt.Rows)
            {
                NextCompCode = Convert.ToString(row[1]);
            }

            if (NextCompCode == String.Empty)
            {
                NextCompCode = "M-00000001";
            }
            else
            {
                Int32 Numb = Convert.ToInt32(NextCompCode.Substring(2));
                Numb = Numb + 1;
                string NumberPartOfCode = Convert.ToString(Numb);
                NextCompCode = "M-" + "0000000000".Substring(1, 8 - NumberPartOfCode.Length) + NumberPartOfCode;
            }

            return (NextCompCode);
        }

        public Athl_Competition InitAthlComp()
        {
            DateTime EmptyDate;
            DateTime.TryParse("1753-01-01 00:00:00.000", out EmptyDate);


            Athl_Competition AthlCompe = new Athl_Competition();


            AthlCompe.Code = "";
            AthlCompe.Name = "";
            AthlCompe.Location = "";

            AthlCompe.Date = EmptyDate;

            AthlCompe.Date2 = EmptyDate;
            AthlCompe.Date3 = EmptyDate;
            AthlCompe.Organizer = "";
            AthlCompe.Director = "";
            AthlCompe.Judge = "";
            AthlCompe.OutdoorsOrIndoors = 0;
            AthlCompe.CompetitonType = 0;
            AthlCompe.fjolditilstiga = 0;
            AthlCompe.stigfyrir1saeti = 0;
            AthlCompe.stigfyrir2saeti = 0;
            AthlCompe.allirfastig = 0;
            AthlCompe.tegundstigakeppni = 0;
            AthlCompe.fjoldibrauta = 0;
            AthlCompe.skraningargjaldprgrein = 0;
            AthlCompe.undirskriftgjaldkera = "";
            AthlCompe.rafmagnstimataka = 1;
            AthlCompe.heitiiafrekaskra = "";
            AthlCompe.fjoldiumferdaitaeknigreinum = 0;
            AthlCompe.staðurgreinainnanbæjarfélags = 0;
            AthlCompe.dagsetning4 = EmptyDate;
            AthlCompe.Tími = EmptyDate;
            AthlCompe.Skráningargjld_f__boðhlaup = 0;
            AthlCompe.vantardagogmanud = 0;
            AthlCompe.lengdikm = 0;
            AthlCompe.systurhlaup1 = "";
            AthlCompe.systurhlaup2 = "";
            AthlCompe.systurhlaup3 = "";
            AthlCompe.hlaupmotifyrra = "";
            AthlCompe.vidbotvtimatoku1 = 0;
            AthlCompe.greiniafrekaskra = "";
            AthlCompe.bokadiafrekaskra = 0;
            AthlCompe.vidbotvtimatoku = "";
            AthlCompe.endanlegurslitskrad = 0;
            AthlCompe.raesitimi = EmptyDate;
            AthlCompe.synaathugasemd = 0;
            AthlCompe.oldungaflokkar = 0;
            AthlCompe.tungumal = 0;
            AthlCompe.synafelag = 0;
            AthlCompe.synaheitisveitar = 0;
            AthlCompe.taknhlaupsvidinnlestur = "";
            AthlCompe.ensktheitiamoti = "";
            AthlCompe.heitihtmlsidu = "";
            AthlCompe.aldursflokkamot = 0;
            AthlCompe.makeppauppfyrirsig = 0;
            AthlCompe.ritarablodmedislmeti = 0;
            AthlCompe.synaland = 0;
            AthlCompe.athugasemdaurslitabladi = "";
            AthlCompe.Reikna_unglingastig = 0;
            AthlCompe.dagssídastuppfaert = EmptyDate;
            AthlCompe.timisídastuppfaert = EmptyDate;
            AthlCompe.bodhlaupmedmismsprettum = 0;
            AthlCompe.textividgesti = "";
            AthlCompe.synanettotima = 0;
            AthlCompe.greiniafrekaskraflogutimi = "";
            AthlCompe.notafelagafkeppendaspjaldi = 0;
            AthlCompe.lokamotivafrekaskrar = 0;
            AthlCompe.landistadfelags = 0;
            AthlCompe.ekkibirtaiafrekaskra = 0;
            AthlCompe.slodaurslitmots = "";
            AthlCompe.synamillitima1 = 0;
            AthlCompe.synamillitima2 = 0;
            AthlCompe.heitialista = "";
            AthlCompe.keppnisvollur = "";
            AthlCompe.floguhlaup = 0;
            AthlCompe.nanaritegund = 0;
            AthlCompe.hlaupaseria = "";
            AthlCompe.flokkurhlaups = "";
            AthlCompe.tegundmots = "";
            AthlCompe.synamillitima3 = 0;
            AthlCompe.teljarialltaf1 = 1;
            AthlCompe.Stofna_nýjan_v_innles_á_millit = 0;
            AthlCompe.Millitímar_teknir_eftir = "";
            AthlCompe.Nota_aðeins_ársbesta_við_röðun = 0;
            AthlCompe.Tímabil_ársbesta_frá_mótsdags = "";
            AthlCompe.Reikna_IAAF_stig = 0;
            AthlCompe.Má_prenta_úrslit_frá_Scheduler = 0;
            AthlCompe.synamillitima4 = 0;
            AthlCompe.synamillitima5 = 0;
            AthlCompe.Með_rásnúmeri = 0;
            AthlCompe.Sleppa_í_afhendingu_rásnúmera = 0;


            return (AthlCompe);
        }

        public Athl_CompetitorsInCompetition InitCompetitorInComp()
        {
            DateTime EmptyDate;
            DateTime.TryParse("1753-01-01 00:00:00.000", out EmptyDate);

            Athl_CompetitorsInCompetition CompetitorInComp = new Athl_CompetitorsInCompetition();

            CompetitorInComp.mot = "";
            CompetitorInComp.rasnumer = 0;
            CompetitorInComp.keppendanumer = "";
            CompetitorInComp.nafn = "";
            CompetitorInComp.felag = "";
            CompetitorInComp.felaginnansambands = "";
            CompetitorInComp.kyn = 0;
            CompetitorInComp.kennitala = "";
            CompetitorInComp.faedingardagur = EmptyDate;
            CompetitorInComp.faedingarar = 0;
            CompetitorInComp.aldurkeppanda = 0;
            CompetitorInComp.aldursflokkur = 0;
            CompetitorInComp.leitarnafn = "";
            CompetitorInComp.land = "";
            CompetitorInComp.keppnisflokkurihlaupi = "";
            CompetitorInComp.timinumeriskur = 0;
            CompetitorInComp.timi = "";
            CompetitorInComp.rodimark = 0;
            CompetitorInComp.rodiflokki = 0;
            CompetitorInComp.heitisveitar = "";
            CompetitorInComp.ogreittthatttokugjald = 0;
            CompetitorInComp.skraning = 0;
            CompetitorInComp.vegalengd = "";
            CompetitorInComp.keppnisflokkur = 0;
            CompetitorInComp.skogerd = 0;
            CompetitorInComp.staerdtbols = 0;
            CompetitorInComp.athugasemd = "";
            CompetitorInComp.numerflogu = "";
            CompetitorInComp.starttimiklst = "";
            CompetitorInComp.lokatimiklst = "";
            CompetitorInComp.nettotimi = "";
            CompetitorInComp.kaupaedaleigjaflogu = 0;
            CompetitorInComp.bruttotimi = "";
            CompetitorInComp.bodhlaupssveit = 0;
            CompetitorInComp.fyrirlidi = 0;
            CompetitorInComp.millitimi1klst = "";
            CompetitorInComp.millitimi1brutto = "";
            CompetitorInComp.millitimi1netto = "";
            CompetitorInComp.millitimi2klst = "";
            CompetitorInComp.millitimi2brutto = "";
            CompetitorInComp.millitimi2netto = "";
            CompetitorInComp.byssutimihlaupara = "";
            CompetitorInComp.millitimi3klst = "";
            CompetitorInComp.millitimi3brutto = "";
            CompetitorInComp.millitimi3netto = "";
            CompetitorInComp.fradrvmargrarashopa = 0;
            CompetitorInComp.millitimi4klst = "";
            CompetitorInComp.millitimi4brutto = "";
            CompetitorInComp.millitimi4netto = "";
            CompetitorInComp.millitimi5klst = "";
            CompetitorInComp.millitimi5brutto = "";
            CompetitorInComp.millitimi5netto = "";
            CompetitorInComp.millitimi6klst = "";
            CompetitorInComp.millitimi6brutto = "";
            CompetitorInComp.millitimi6netto = "";
            CompetitorInComp.rasnumeraskyrslu = 0;
            CompetitorInComp.wrkheimili = "";
            CompetitorInComp.wrkstadur = "";
            CompetitorInComp.vantarbol = 0;
            CompetitorInComp.gestur = 0;
            CompetitorInComp.skraningardags = EmptyDate;
            CompetitorInComp.skraningartimi = EmptyDate;
            CompetitorInComp.skradaf = "";
            CompetitorInComp.wrkemail = "";
            CompetitorInComp.flokkurhlaups = "";
            CompetitorInComp.StaerdBolsTexti = "";
            CompetitorInComp.Með_medalíu = 0;
            CompetitorInComp.numergodgerdafelags = 0;
            CompetitorInComp.numervaraflogu = "";
            CompetitorInComp.wrkheimilifangland = "";
            CompetitorInComp.idegauserid = "";
            CompetitorInComp.Má_senda_e_mail = 0;
            CompetitorInComp.Netskráning = 0;
            CompetitorInComp.Fyrirtæki = "";
            CompetitorInComp.Innlesið_nafn = "";
            CompetitorInComp.Innlesið_fæðingarár = 0;
            CompetitorInComp.Innlesið_félag = "";
            CompetitorInComp.Úthlutað_rásnúmer = 0;
            CompetitorInComp.Skráningardags__rásnúmers = EmptyDate;
            CompetitorInComp.Skráningartími_rásnúmers = EmptyDate;
            CompetitorInComp.Skráð_í_vél = "";
            CompetitorInComp.Skipta_yfir_í_vegalengd = "";
            CompetitorInComp.wrkSimi = "";
            CompetitorInComp.wrkFarsimi = "";
            CompetitorInComp.wrkÞjóðerni = "";
            CompetitorInComp.greinnumer = 0;
            CompetitorInComp.Announcer_Remarks = "";
            CompetitorInComp.Finish_Line_Description = "";
            CompetitorInComp.Hlaupahópur = "";


            return (CompetitorInComp);
        }


        public Athl_CompetitorsInCompetition GetCompetitorInComp(string CompetitionCode, Int32 BibNo)
        {

            using (AthleticsEntities1 AthlCompetitorInCompRec = new AthleticsEntities1())
            {
                Athl_CompetitorsInCompetition AthCompetorInComp = (from AthlCompeInComp in AthlCompetitorInCompRecord.Athl_CompetitorsInCompetition
                                                                   where (AthlCompeInComp.mot == CompetitionCode) &&
                                                                   (AthlCompeInComp.rasnumer == BibNo)
                                                                   select AthlCompeInComp).First();
                return (AthCompetorInComp);
            }

        }

        public void InsertCompetitorInCompetition(Athl_CompetitorsInCompetition CompetitorInComp)
        {
            AthlCompetitorInCompRecord.AddToAthl_CompetitorsInCompetition(CompetitorInComp);
            AthlCompetitorInCompRecord.SaveChanges();

        }

        public void DeleteCompetitorInCompetition(Athl_CompetitorsInCompetition CompetitorInComp)
        {

            try
            {
                AthlCompetitorInCompRecord.DeleteObject(CompetitorInComp);
                AthlCompetitorInCompRecord.SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw new InvalidOperationException(string.Format(
                    "Gat ekki eytt keppanda með rásnúmeri '{0}' í móti {1}",
                    CompetitorInComp.rasnumer.ToString(),
                    CompetitorInComp.mot));
            }

        }

        public Athl_CompetitionEvents InitCompetitionEvent()
        {
            DateTime EmptyDate;
            DateTime.TryParse("1753-01-01 00:00:00.000", out EmptyDate);

            Athl_CompetitionEvents CompetitionEvent = new Athl_CompetitionEvents();

            CompetitionEvent.mot = "";
            CompetitionEvent.lina = 0;
            CompetitionEvent.grein = "";
            CompetitionEvent.kyn = 0;
            CompetitionEvent.flokkur = "";
            CompetitionEvent.ridill = 0;
            CompetitionEvent.numerridils = 0;
            CompetitionEvent.dagsetning = EmptyDate;
            CompetitionEvent.timi = EmptyDate;
            CompetitionEvent.stadur = "";
            CompetitionEvent.nafnakall = EmptyDate;
            CompetitionEvent.timi2ridils = EmptyDate;
            CompetitionEvent.heitigreinar = "";
            CompetitionEvent.ensktheitigreinar = "";
            CompetitionEvent.tegundgreinar = 0;
            CompetitionEvent.rafmagnstimataka = 1;  //Was 0
            CompetitionEvent.krefstvindmaelis = 0;
            CompetitionEvent.nanaritegundargreining = 0;
            CompetitionEvent.thrautargrein = 0;
            CompetitionEvent.Sería_með_stigum_í_þrautargr_ = 0;
            CompetitionEvent.fjoldiibrauta = 0;
            CompetitionEvent.fjoldiumferda = 0;
            CompetitionEvent.aldurfra = 0;
            CompetitionEvent.aldurtil = 0;
            CompetitionEvent.stigagrein = 0;
            CompetitionEvent.fjolditilstiga = 0;
            CompetitionEvent.stigfyrir1saeti = 0;
            CompetitionEvent.stigfyrir2saeti = 0;
            CompetitionEvent.handvirkstigaskraning = 0;
            CompetitionEvent.teljagreinistigautreikningi = 0;
            CompetitionEvent.Röð_kepp__sem_fær_fyrst_stig = 0;
            CompetitionEvent.skraningargjald = 0;
            CompetitionEvent.urslitkomin = 0;
            CompetitionEvent.birtaiurslitum = 0;
            CompetitionEvent.stadakeppni = 0;
            CompetitionEvent.takngreinar = "";
            CompetitionEvent.rodiurslitum = 0;
            CompetitionEvent.urridliaundankoma = 0;
            CompetitionEvent.hamarkfjoldikeppenda = 0;
            CompetitionEvent.sigurvegariifyrra = "";
            CompetitionEvent.arangurifyrra = "";
            CompetitionEvent.motsmetshafi = "";
            CompetitionEvent.motsmet = "";
            CompetitionEvent.dagsetningmotsmets = EmptyDate;
            CompetitionEvent.stadurmotsmets = "";
            CompetitionEvent.felagmotsmetshafa = "";
            CompetitionEvent.rodiafrekaskra = 0;
            CompetitionEvent.rodithraut = 0;
            CompetitionEvent.motsmetshafi2 = "";
            CompetitionEvent.motsmet2 = "";
            CompetitionEvent.dagsetningmotsmets2 = EmptyDate;
            CompetitionEvent.stadurmotsmets2 = "";
            CompetitionEvent.felagmotsmetshafa2 = "";
            CompetitionEvent.tharfadradakeppendum = 0;
            CompetitionEvent.athugasemd = "";
            CompetitionEvent.athugasemdaritarablad = "";
            CompetitionEvent.heitihtmlskrar = "";
            CompetitionEvent.htmlskramyndudthann = EmptyDate;
            CompetitionEvent.htmlskramyndudkl = EmptyDate;
            CompetitionEvent.eventnamefisi = "";
            CompetitionEvent.Númer_hlaupagreinar_f__Lynx = 0;
            CompetitionEvent.tilkynnaurslit = 0;
            CompetitionEvent.tilkynnaverdlaunaafhendingu = 0;
            CompetitionEvent.ekkiprentaritarablod = 0;
            CompetitionEvent.Innlesið_heiti_greinar = "";
            CompetitionEvent.motsmetshafi3 = "";
            CompetitionEvent.motsmet3 = "";
            CompetitionEvent.dagsetningmotsmets3 = EmptyDate;
            CompetitionEvent.stadurmotsmets3 = "";
            CompetitionEvent.felagmotsmetshafa3 = "";


            return CompetitionEvent;
        }

        public Athl_CompetitionEvents GetCompetitionEvent(string CompetitionCode, Int32 LineNo)
        {

            using (AthleticsEntities1 AthlCompetitionEventRec = new AthleticsEntities1())
            {
                Athl_CompetitionEvents AthlCompEvent = (from AthlCompEventR in AthlEventInCompetitionRecord.Athl_CompetitionEvents
                                                        where (AthlCompEventR.mot == CompetitionCode) &&
                                                                   (AthlCompEventR.lina == LineNo)
                                                        select AthlCompEventR).First();
                return (AthlCompEvent);
            }

        }

        public Int32 ReturnNextEventinCompLineNo(string CompCode)
        {
            Int32 NextLinNoInt = 0;
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            var LastLineNo = AthlEnt.ReturnLastEventinCompLineNo(CompCode);

            foreach (var result in LastLineNo)
            {
                NextLinNoInt = Convert.ToInt32(result.Value) + 10000;
            }
            return (NextLinNoInt);

        }

        public void ReturnAgeGroupInfo(string AgeGroupCode, out Int32 Gender, out Int32 AgeFrom, out Int32 AgeTo,
              out string AgeGroupName1, out string AgeGroupName2)
        {
            Gender = 0;
            AgeFrom = 0;
            AgeTo = 0;
            AgeGroupName1 = "";
            AgeGroupName2 = "";
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            var AgeGrResult = AthlEnt.ReturnAgeGroupInfo2(AgeGroupCode);
            foreach (var result in AgeGrResult)
            {
                Gender = result.Kyn;
                AgeFrom = result.Aldur_frá;
                AgeTo = result.Aldur_Til;
                AgeGroupName1 = result.Heiti;
                AgeGroupName2 = result.Þágufallsheiti;

            }

        }

        //public void ReturnEventRelatedInfo(string EventCode, Int32 Gender, string Group, Int32 OutdoorsIndoors,
        //    out Int32 EventType, out Int32 DetailedEventType, out Int32 OrderInStatistics)
        //{

        //}


        public void InsertCompEventInCompetition(Athl_CompetitionEvents CompetitionEvent)
        {
            AthlEventInCompetitionRecord.AddToAthl_CompetitionEvents(CompetitionEvent);
            AthlEventInCompetitionRecord.SaveChanges();

        }

       

        public void DeleteCompetitonEvent(Athl_CompetitionEvents CompetitionEvent)
        {

            try
            {
                AthlEventInCompetitionRecord.DeleteObject(CompetitionEvent);
                AthlEventInCompetitionRecord.SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw new InvalidOperationException(string.Format(
                "Gat ekki eytt keppnisgrein móts með línu númeri '{0}' í móti {1}",
                CompetitionEvent.lina.ToString(),
                CompetitionEvent.mot));

            }
        }

        public Athl_CompetitionEvents CopyKeyValuesBtwEvents(Athl_CompetitionEvents CompetitionEventsOld)
        {
            Athl_CompetitionEvents CompetitionEventsNew = new Athl_CompetitionEvents();

            CompetitionEventsNew = InitCompetitionEvent();
            CompetitionEventsNew.mot = CompetitionEventsOld.mot;
            CompetitionEventsNew.grein = CompetitionEventsOld.grein;
            CompetitionEventsNew.kyn = CompetitionEventsOld.kyn;
            CompetitionEventsNew.flokkur = CompetitionEventsOld.flokkur;
            CompetitionEventsNew.heitigreinar = CompetitionEventsOld.heitigreinar;
            CompetitionEventsNew.dagsetning = CompetitionEventsOld.dagsetning;
            CompetitionEventsNew.aldurtil = CompetitionEventsOld.aldurfra;
            CompetitionEventsNew.aldurtil = CompetitionEventsOld.aldurtil;
            CompetitionEventsNew.tegundgreinar = CompetitionEventsOld.tegundgreinar;
            CompetitionEventsNew.nanaritegundargreining = CompetitionEventsOld.nanaritegundargreining;
            CompetitionEventsNew.skraningargjald = CompetitionEventsOld.skraningargjald;
            CompetitionEventsNew.stadakeppni = CompetitionEventsOld.stadakeppni;
            CompetitionEventsNew.takngreinar = CompetitionEventsOld.takngreinar;
            CompetitionEventsNew.rodiafrekaskra = CompetitionEventsOld.rodiafrekaskra;
            CompetitionEventsNew.fjoldiibrauta = CompetitionEventsOld.fjoldiibrauta;
            CompetitionEventsNew.tharfadradakeppendum = 1;
            if (CompetitionEventsNew.tegundgreinar == 1)
            {
                CompetitionEventsNew.rafmagnstimataka = 1;
            }
            else
            {
                CompetitionEventsNew.rafmagnstimataka = 0;
                CompetitionEventsNew.fjoldiumferda = 6;
            }
            CompetitionEventsNew.krefstvindmaelis = CompetitionEventsOld.krefstvindmaelis;

            return (CompetitionEventsNew);

        }


        public Athl_Events GetAthlEvent(string EventCode, Int32 Gender, string Group, Int32 OutdoorsIndoors)
        {

            using (AthleticsEntities1 AthlEventRec = new AthleticsEntities1())
            {
                Athl_Events AthlEventRecord = (from AthlEv in AthlEventRec.Athl_Events
                                               where (AthlEv.Grein == EventCode) &&
                                               (AthlEv.Kyn == Gender) &&
                                               (AthlEv.Flokkur == Group) &&
                                               (AthlEv.Úti_Inni == OutdoorsIndoors)
                                               select AthlEv).First();
                return (AthlEventRecord);
            }

        }

        public Athl_Competitors GetAthlCompetitor(string CompetitorCode)
        {
            using (AthleticsEntities1 AthlCompetitor = new AthleticsEntities1())
            {
                Athl_Competitors AthlCompetitorRec = (from AthlComp in AthlCompetitor.Athl_Competitors
                                                      where (AthlComp.Númer == CompetitorCode)
                                                      select AthlComp).First();
                return (AthlCompetitorRec);
            }
        }


        public Athl_CompetitorsInCompetition CopyCompetitorToCompetition(string CompetitorCode, string CompetitonCode, Int32 CompetitionYear, string MoveToClub, 
            out bool WasOkToMove, out string ReasonText)
        {
            Athl_Competitors AthlCompetitorRec = new Athl_Competitors();
            Athl_CompetitorsInCompetition AthlCompetitorInCompRec = new Athl_CompetitorsInCompetition();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
           
            Global gl = new Global();
            //DateTime TimeNow;
            //DateTime.TryParse("1754-01-01 00:00:00.000", out TimeNow);
            //TimeNow = TimeNow + DateTime.Now.TimeOfDay;
            DateTime TodaysDate = DateTime.Now;
            DateTime TimeNow;
            DateTime.TryParse("1754-01-01 00:00:00.000", out TimeNow);
            string CurrentUser = gl.GetGlobalValue("CurrentUserName");
            string CompetitionCode = gl.GetCompetitionCode();
            Int32 NoOfPerformancesThisYear = 0;
            Int32 NoOfPerformancesLastYear = 0;
            //System.Data.Objects.ObjectParameter NoOfPerfParam = new System.Data.Objects.ObjectParameter("NoOfAfrekThisYear", typeof(Int32));    
            System.Data.Objects.ObjectParameter NoOfPerfParam = new System.Data.Objects.ObjectParameter("NoOfPerfForYear", typeof(Int32));

            WasOkToMove = true;
            ReasonText = "";
            AthlCompetitorInCompRec = InitCompetitorInComp();
            AthlCompetitorRec = GetAthlCompetitor(CompetitorCode);
            if (AthlCompetitorRec.Félag != MoveToClub)
            {
                if (MoveToClub != "")
                {
                    if (AthlCompetitorRec.Fæðingarár > 0)
                    {
                        if (AthlCompetitorRec.Fæðingarár >= (TodaysDate.Year - 14))
                        {
                            AthlEnt.PerfornancesForCompetitorFoundInYear(CompetitorCode, TodaysDate.Year, NoOfPerfParam);
                            NoOfPerformancesThisYear = Convert.ToInt32(NoOfPerfParam.Value.ToString());
                            if (NoOfPerformancesThisYear > 0)
                            {
                                WasOkToMove = false;
                                ReasonText = "Keppandi " + CompetitorCode + " " + AthlCompetitorRec.Nafn + " á afrek á árinu " + TodaysDate.Year.ToString();
                                return AthlCompetitorInCompRec;
                            }
                            AthlEnt.InsertToLogFile(CurrentUser, "Keppandi " + CompetitorCode + " " + AthlCompetitorRec.Nafn + " skiptir um félag. Úr " + AthlCompetitorRec.Félag + " í " + MoveToClub,
                                CompetitionCode, CompetitorCode, "");                                  
                            AthlEnt.ModifyCompetitorsClub(CompetitorCode, MoveToClub);
                            AthlCompetitorRec.Félag = MoveToClub;
                            ReasonText = "Move was ok";
                        }
                        else //Competitor is older than 14 years old
                        {
                            AthlEnt.PerfornancesForCompetitorFoundInYear(CompetitorCode, TodaysDate.Year, NoOfPerfParam);
                            NoOfPerformancesThisYear = Convert.ToInt32(NoOfPerfParam.Value.ToString());
                            if (NoOfPerformancesThisYear > 0)
                            {
                                WasOkToMove = false;
                                ReasonText = "Keppandi " + CompetitorCode + " " + AthlCompetitorRec.Nafn + " á afrek á árinu " + TodaysDate.Year.ToString();
                                return AthlCompetitorInCompRec;
                            }
                            AthlEnt.PerfornancesForCompetitorFoundInYear(CompetitorCode, (TodaysDate.Year - 1), NoOfPerfParam);
                            NoOfPerformancesLastYear = Convert.ToInt32(NoOfPerfParam.Value.ToString());
                            if (NoOfPerformancesLastYear > 0)
                            {
                                WasOkToMove = false;
                                ReasonText = "Keppandi " + CompetitorCode + " " + AthlCompetitorRec.Nafn + " á afrek á árinu " + (TodaysDate.Year - 1).ToString() + ". Þarf formleg félagaskipti.";
                                return AthlCompetitorInCompRec;
                            }
                            AthlEnt.InsertToLogFile(CurrentUser, "Keppandi " + CompetitorCode + " " + AthlCompetitorRec.Nafn + " skiptir um félag. Úr " + AthlCompetitorRec.Félag + " í " + MoveToClub,
                              CompetitionCode, CompetitorCode, "");                                  
                            AthlEnt.ModifyCompetitorsClub(CompetitorCode, MoveToClub);
                            AthlCompetitorRec.Félag = MoveToClub;
                            ReasonText = "Move was ok";
                        }
                    }
                    else
                    {
                        WasOkToMove = false;
                        ReasonText = "Vantar fæðingarár á keppanda " + CompetitorCode;
                        return AthlCompetitorInCompRec;
                    }
                }
            }

            AthlCompetitorInCompRec.mot = CompetitonCode;
            AthlCompetitorInCompRec.rasnumer = gl.ReturnNextBibno(CompetitonCode);
            AthlCompetitorInCompRec.keppendanumer = AthlCompetitorRec.Númer;
            AthlCompetitorInCompRec.kennitala = AthlCompetitorRec.Kennitala;
            AthlCompetitorInCompRec.nafn = AthlCompetitorRec.Nafn;
            AthlCompetitorInCompRec.faedingarar = AthlCompetitorRec.Fæðingarár;
            AthlCompetitorInCompRec.aldurkeppanda = CompetitionYear - AthlCompetitorInCompRec.faedingarar;
            AthlCompetitorInCompRec.felag = AthlCompetitorRec.Félag;            
            AthlCompetitorInCompRec.kyn = AthlCompetitorRec.Kyn;
            AthlCompetitorInCompRec.land = AthlCompetitorRec.Land;
            AthlCompetitorInCompRec.felaginnansambands = AthlCompetitorRec.Félag_innan_sambands;
            AthlCompetitorInCompRec.faedingardagur = AthlCompetitorRec.Fæðingardagur;
            AthlCompetitorInCompRec.skraningardags = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            AthlCompetitorInCompRec.skraningartimi = TimeNow; //TodaysDate;


            return (AthlCompetitorInCompRec);

        }

        public Athl_CompetitorsInEvent CopyFromCompetitorInEvent(Athl_CompetitorsInEvent FromCompetitorInEvent)
        {
            Athl_CompetitorsInEvent CompetitorInEventOut = new Athl_CompetitorsInEvent();
            CompetitorInEventOut = FromCompetitorInEvent;
            return CompetitorInEventOut;
        }

        public Athl_CompetitorsInEvent InitCompetitorInEvent()
        {
            DateTime EmptyDateTime;
            DateTime.TryParse("1753-01-01 00:00:00.000", out EmptyDateTime);

            Athl_CompetitorsInEvent AthlCompetitorInEvent = new Athl_CompetitorsInEvent();

            AthlCompetitorInEvent.mot = "";
            AthlCompetitorInEvent.greinarnumer = 0;
            AthlCompetitorInEvent.lina = 0;
            AthlCompetitorInEvent.grein = "";
            AthlCompetitorInEvent.kyn = 0;
            AthlCompetitorInEvent.flokkur = "";
            AthlCompetitorInEvent.ridill = 0;
            AthlCompetitorInEvent.numerridilsekkinotad = 0;
            AthlCompetitorInEvent.dagsetninggreinar = EmptyDateTime;
            AthlCompetitorInEvent.timigreinar = EmptyDateTime;
            AthlCompetitorInEvent.rasnumer = 0;
            AthlCompetitorInEvent.leitarnafn = "";
            AthlCompetitorInEvent.ridillnumer = 0;
            AthlCompetitorInEvent.stokkkastrod = 0;
            AthlCompetitorInEvent.nafn = "";
            AthlCompetitorInEvent.faedingarar = 0;
            AthlCompetitorInEvent.felag = "";
            AthlCompetitorInEvent.timi = 0;
            AthlCompetitorInEvent.metrar = 0;
            AthlCompetitorInEvent.vindur = 0;
            AthlCompetitorInEvent.arangur = "";
            AthlCompetitorInEvent.rafmagnstimataka = 0;
            AthlCompetitorInEvent.thrautarstig = 0;
            AthlCompetitorInEvent.urslitarod = 0;
            AthlCompetitorInEvent.samasaetiognaestiaundan = 0;
            AthlCompetitorInEvent.nanarirod = 0;
            AthlCompetitorInEvent.stig = 0;
            AthlCompetitorInEvent.stadakeppni = 0;
            AthlCompetitorInEvent.urslitarodtexti = "";
            AthlCompetitorInEvent.IAAF_Stig = 0;
            AthlCompetitorInEvent.tilraun1 = 0;
            AthlCompetitorInEvent.vindur1 = 0;
            AthlCompetitorInEvent.merking1 = 0;
            AthlCompetitorInEvent.tilraun2 = 0;
            AthlCompetitorInEvent.vindur2 = 0;
            AthlCompetitorInEvent.merking2 = 0;
            AthlCompetitorInEvent.tilraun3 = 0;
            AthlCompetitorInEvent.vindur3 = 0;
            AthlCompetitorInEvent.merking3 = 0;
            AthlCompetitorInEvent.tilraun4 = 0;
            AthlCompetitorInEvent.vindur4 = 0;
            AthlCompetitorInEvent.merking4 = 0;
            AthlCompetitorInEvent.tilraun5 = 0;
            AthlCompetitorInEvent.vindur5 = 0;
            AthlCompetitorInEvent.merking5 = 0;
            AthlCompetitorInEvent.tilraun6 = 0;
            AthlCompetitorInEvent.vindur6 = 0;
            AthlCompetitorInEvent.merking6 = 0;
            AthlCompetitorInEvent.seria = "";
            AthlCompetitorInEvent.athugasemd = "";
            AthlCompetitorInEvent.handvirkathugasemd = 0;
            AthlCompetitorInEvent.nullarangur = 0;
            AthlCompetitorInEvent.bestiaranguriar = 0;
            AthlCompetitorInEvent.personulegmet = 0;
            AthlCompetitorInEvent.bestiaranguriartexti = "";
            AthlCompetitorInEvent.personulegtmettexti = "";
            AthlCompetitorInEvent.rodiundanurslitum = 0;
            AthlCompetitorInEvent.qualification = "";
            AthlCompetitorInEvent.gestur = 0;
            AthlCompetitorInEvent.handvirkstig = 0;
            AthlCompetitorInEvent.rasnumeraskyrslu = 0;
            AthlCompetitorInEvent.Unglingastig = 0;
            AthlCompetitorInEvent.PerformaceRemarks = "";

            return AthlCompetitorInEvent;

        }


        public Athl_CompetitorsInEvent GetCompetitorInEvent(string CompetitionCode, Int32 BibNo, Int32 EventLineNo)
        {
            Athl_CompetitorsInEvent EmptyAthlCompetitorInEv = new Athl_CompetitorsInEvent();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            EmptyAthlCompetitorInEv = AthlCRUD.InitCompetitorInEvent();

            using (AthleticsEntities1 AthlCompetitorInEvent = new AthleticsEntities1())
            {                
                Athl_CompetitorsInEvent AthlCompetitorInEv = new Athl_CompetitorsInEvent();
                AthlCompetitorInEv = EmptyAthlCompetitorInEv;
                AthlCompetitorInEv = (from AthlCompInEventR in AthlCompetitorInEvent.Athl_CompetitorsInEvent
                                                              where (AthlCompInEventR.mot == CompetitionCode) &&
                                                                    (AthlCompInEventR.greinarnumer == EventLineNo) &&
                                                                    (AthlCompInEventR.rasnumer == BibNo)
                                                              select AthlCompInEventR).FirstOrDefault();
                return (AthlCompetitorInEv);
            }
            //return (EmptyAthlCompetitorInEv);

        }

        public void RegisterCompetitorInEvent(string CompetitionCode, Athl_CompetitionEvents AthlCompEvent, Int32 BibNo,
              Athl_CompetitorsInCompetition CompetitorInComp, Int32 HeatNo, Int32 LaneOrOrder)
        {
            Athl_CompetitorsInEvent AthlCompetitorsInEvRecord = new Athl_CompetitorsInEvent();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Global gl = new Global();
            Int32 IndoorsOrOutdoors = 0;
            string PerformanceTextOutdoors = "";
            string PerformanceTextIndoors = "";
            decimal PerformanceDecOutdoors = 0;
            decimal PerformanceDecIndoors = 0;
            string BestPerformanceIsOutdoors = "Unknown";
            //string PersonalBestOutdoors = "";
            //var PersonalBestIndoors = "";
            //var PersonalBestWind = "";
            //var YearBestOutdoors = "";
            //var YearBestIndoors = "";
            //var YearBestWind = "";

            //System.Data.Objects.ObjectParameter PersBestOutdoors = new System.Data.Objects.ObjectParameter("PersonalBestOutdoors", typeof(string));
            //System.Data.Objects.ObjectParameter PersBestIndoors = new System.Data.Objects.ObjectParameter("PersonalBestIndoors", typeof(string));
            //System.Data.Objects.ObjectParameter PersBestWind = new System.Data.Objects.ObjectParameter("PersonalBestWind", typeof(string));
            //System.Data.Objects.ObjectParameter YearBestOutdoors = new System.Data.Objects.ObjectParameter("YearBestOutdoors", typeof(string));
            //System.Data.Objects.ObjectParameter YearBestIndoors = new System.Data.Objects.ObjectParameter("YearBestIndoors", typeof(string));
            //System.Data.Objects.ObjectParameter YearBestWind = new System.Data.Objects.ObjectParameter("YearBestWind", typeof(string));

            AthlCompetitorsInEvRecord = InitCompetitorInEvent();
            AthlCompetitorsInEvRecord.mot = CompetitionCode;
            AthlCompetitorsInEvRecord.greinarnumer = AthlCompEvent.lina;
            AthlCompetitorsInEvRecord.lina = RetNextLineNoInEvent(CompetitionCode, AthlCompEvent.lina);
            AthlCompetitorsInEvRecord.grein = AthlCompEvent.grein;
            AthlCompetitorsInEvRecord.flokkur = AthlCompEvent.flokkur;
            AthlCompetitorsInEvRecord.kyn = CompetitorInComp.kyn;
            AthlCompetitorsInEvRecord.rasnumer = BibNo;
            AthlCompetitorsInEvRecord.ridillnumer = HeatNo;
            AthlCompetitorsInEvRecord.stokkkastrod = LaneOrOrder;
            AthlCompetitorsInEvRecord.nafn = CompetitorInComp.nafn;
            AthlCompetitorsInEvRecord.faedingarar = CompetitorInComp.faedingarar;
            AthlCompetitorsInEvRecord.felag = CompetitorInComp.felag;
            AthlCompetitorsInEvRecord.gestur = CompetitorInComp.gestur;
            AthlCompetitorsInEvRecord.rafmagnstimataka = AthlCompEvent.rafmagnstimataka;


            //AthlEnt.ReturnPersBestYearBest(CompetitorInComp.keppendanumer, AthlCompEvent.grein, AthlCompEvent.flokkur,
            //      PersBestOutdoors, PersBestIndoors, PersBestWind, YearBestOutdoors, YearBestIndoors, YearBestWind);

            //PerformanceTextOutdoors = PersBestOutdoors.Value.ToString();
            //PerformanceDecOutdoors = gl.ParseTextAndReturnDec(PerformanceTextOutdoors);
            //PerformanceTextIndoors = PersBestIndoors.Value.ToString();
            //PerformanceDecIndoors = gl.ParseTextAndReturnDec(PerformanceTextIndoors);

            //if (AthlCompEvent.tegundgreinar == 1)  //Track
            //{
            //    if (PerformanceDecIndoors == 0)
            //    {
            //        if (PerformanceDecOutdoors == 0)
            //        {
            //            AthlCompetitorsInEvRecord.personulegmet = 0;
            //            AthlCompetitorsInEvRecord.personulegtmettexti = "";
            //            BestPerformanceIsOutdoors = "Neither";
            //        }
            //        else
            //        {
            //            AthlCompetitorsInEvRecord.personulegmet = PerformanceDecOutdoors;
            //            AthlCompetitorsInEvRecord.personulegtmettexti = PerformanceTextOutdoors;
            //            BestPerformanceIsOutdoors = "True";
            //        }
            //    }
            //    else
            //        if (PerformanceDecOutdoors == 0)
            //        {
            //            AthlCompetitorsInEvRecord.personulegmet = PerformanceDecIndoors;
            //            AthlCompetitorsInEvRecord.personulegtmettexti = PerformanceTextIndoors;
            //            BestPerformanceIsOutdoors = "False";
            //        }
            //        else
            //            if (PerformanceDecIndoors == PerformanceDecOutdoors)
            //            {
            //                AthlCompetitorsInEvRecord.personulegmet = PerformanceDecIndoors;
            //                AthlCompetitorsInEvRecord.personulegtmettexti = PerformanceTextIndoors;
            //                BestPerformanceIsOutdoors = "Both";
            //            }
            //            else
            //            if (PerformanceDecIndoors < PerformanceDecOutdoors)
            //            {
            //                AthlCompetitorsInEvRecord.personulegmet = PerformanceDecIndoors;
            //                AthlCompetitorsInEvRecord.personulegtmettexti = PerformanceTextIndoors;
            //                BestPerformanceIsOutdoors = "False";
            //            }
            //            else
            //            {
            //                AthlCompetitorsInEvRecord.personulegmet = PerformanceDecOutdoors;
            //                AthlCompetitorsInEvRecord.personulegtmettexti = PerformanceTextOutdoors;
            //                BestPerformanceIsOutdoors = "True";
            //            }
            //}
            //else //Field Event
            //{

            //}
            //if ((gl.GetOutdorrsOrIndoors() == "0") && (BestPerformanceIsOutdoors == "False"))
            //{
            //    AthlCompetitorsInEvRecord.personulegtmettexti = AthlCompetitorsInEvRecord.personulegtmettexti + "(i)";
            //}
            //if ((gl.GetOutdorrsOrIndoors() == "1") && (BestPerformanceIsOutdoors == "True"))
            //{
            //    AthlCompetitorsInEvRecord.personulegtmettexti = AthlCompetitorsInEvRecord.personulegtmettexti + "(ú)";
            //}
            System.Data.Objects.ObjectParameter PersonalBest = new System.Data.Objects.ObjectParameter("PersonalBest", typeof(string));
            System.Data.Objects.ObjectParameter PBSuffix = new System.Data.Objects.ObjectParameter("PBSuffix", typeof(string));
            System.Data.Objects.ObjectParameter SeasonBest = new System.Data.Objects.ObjectParameter("SeasonBest", typeof(string));
            System.Data.Objects.ObjectParameter SBSuffix = new System.Data.Objects.ObjectParameter("SBSuffix", typeof(string));

            IndoorsOrOutdoors = gl.TryConvertStringToInt32(gl.GetOutdorrsOrIndoors());
            AthlEnt.ReturnPersBestYearBestForEvent(CompetitorInComp.keppendanumer, AthlCompEvent.grein, AthlCompEvent.flokkur,
                IndoorsOrOutdoors, PersonalBest, PBSuffix, SeasonBest, SBSuffix);

            AthlCompetitorsInEvRecord.bestiaranguriartexti = SeasonBest.Value.ToString();
            AthlCompetitorsInEvRecord.bestiaranguriar = gl.TryConvertStringToDecimal(AthlCompetitorsInEvRecord.bestiaranguriartexti);
            AthlCompetitorsInEvRecord.bestiaranguriartexti = AthlCompetitorsInEvRecord.bestiaranguriartexti + SBSuffix.Value.ToString();

            AthlCompetitorsInEvRecord.personulegtmettexti = PersonalBest.Value.ToString();
            AthlCompetitorsInEvRecord.personulegmet = gl.TryConvertStringToDecimal(AthlCompetitorsInEvRecord.personulegtmettexti);
            AthlCompetitorsInEvRecord.personulegtmettexti = AthlCompetitorsInEvRecord.personulegtmettexti + PBSuffix.Value.ToString();


            //PerformanceTextOutdoors = YearBestOutdoors.Value.ToString();
            //PerformanceDecOutdoors = gl.ParseTextAndReturnDec(PerformanceTextOutdoors);
            //PerformanceTextIndoors = YearBestIndoors.Value.ToString();
            //PerformanceDecIndoors = gl.ParseTextAndReturnDec(PerformanceTextIndoors);

            //if (AthlCompEvent.tegundgreinar == 1)  //Track
            //{
            //    if (PerformanceDecIndoors == 0)
            //    {
            //        if (PerformanceDecIndoors == 0)
            //        {
            //            AthlCompetitorsInEvRecord.bestiaranguriar = 0;
            //            AthlCompetitorsInEvRecord.bestiaranguriartexti = "";
            //            BestPerformanceIsOutdoors = "Neither";
            //        }
            //        else
            //        {
            //            AthlCompetitorsInEvRecord.bestiaranguriar = PerformanceDecOutdoors;
            //            AthlCompetitorsInEvRecord.bestiaranguriartexti = PerformanceTextOutdoors;
            //            BestPerformanceIsOutdoors = "True";
            //        }
            //    }
            //    else
            //        if (PerformanceDecOutdoors == 0)
            //        {
            //            AthlCompetitorsInEvRecord.bestiaranguriar = PerformanceDecIndoors;
            //            AthlCompetitorsInEvRecord.bestiaranguriartexti = PerformanceTextIndoors;
            //            BestPerformanceIsOutdoors = "False";
            //        }
            //        else
            //            if (PerformanceDecIndoors == PerformanceDecOutdoors)
            //            {
            //                AthlCompetitorsInEvRecord.bestiaranguriar = PerformanceDecIndoors;
            //                AthlCompetitorsInEvRecord.bestiaranguriartexti = PerformanceTextIndoors;
            //                BestPerformanceIsOutdoors = "Both";
            //            }
            //            else
            //                if (PerformanceDecIndoors < PerformanceDecOutdoors)
            //                {
            //                    AthlCompetitorsInEvRecord.bestiaranguriar = PerformanceDecIndoors;
            //                    AthlCompetitorsInEvRecord.bestiaranguriartexti = PerformanceTextIndoors;
            //                    BestPerformanceIsOutdoors = "False";
            //                }
            //                else
            //                {
            //                    AthlCompetitorsInEvRecord.bestiaranguriar = PerformanceDecOutdoors;
            //                    AthlCompetitorsInEvRecord.bestiaranguriartexti = PerformanceTextOutdoors;
            //                    BestPerformanceIsOutdoors = "True";
            //                }
            //}

            //if ((gl.GetOutdorrsOrIndoors() == "0") && (BestPerformanceIsOutdoors == "False"))
            //{
            //    AthlCompetitorsInEvRecord.bestiaranguriartexti = AthlCompetitorsInEvRecord.bestiaranguriartexti + "(i)";
            //}
            //if ((gl.GetOutdorrsOrIndoors() == "1") && (BestPerformanceIsOutdoors == "True"))
            //{
            //    AthlCompetitorsInEvRecord.bestiaranguriartexti = AthlCompetitorsInEvRecord.bestiaranguriartexti + "(ú)";
            //}

            AthlCompetitorsInEvRecord.rafmagnstimataka = AthlCompEvent.rafmagnstimataka;
            AthlCompetitorsInEvRecord.PerformaceRemarks = "";

            //AthlEnt.ReturnPersonalBestAndYearBest
            //AthlEnt.ReturnPersonalBestAndYearBest1
            //AthlEnt.ReturnPersonalBestAndYearBest(CompetitionCode, AthlCompEvent.grein, AthlCompEvent.flokkur );
            //AthlEnt.ReturnPersonalBestAndYearBest1()
            //foreach (var result in ResultSet)
            //{
            //    //NextLinNoInt = Convert.ToInt32(result.Value) + 10000;
            //    PersonalBestOutdoors = Convert.ToString(result.value);

            //}


            //AthlEnt.ReturnPersonalBestAndYearBest(CompetitionCode, AthlCompEvent.grein,AthlCompEvent.flokkur, 
            //    Output1, Output2, Output3, Output4, Output5, Output6);
            //PerformanceText = Output1.ToString();
            //PerformanceDec = Decimal.TryParse(Output1.ToString();
            //AthlCompetitorsInEvRecord.personulegmet = Output1.ToString();

            InsertNewCompetitorInEvRec(AthlCompetitorsInEvRecord);

        }

        public void InsertNewCompetitorInEvRec(Athl_CompetitorsInEvent AthlCompInEv)
        {
            AthlCompetitorInEvent.AddToAthl_CompetitorsInEvent(AthlCompInEv);
            AthlCompetitorInEvent.SaveChanges();
        }

        public void DeleteCompetitorInEvRec(Athl_CompetitorsInEvent CompetitorInEv)
        {
            try
            {
                AthlCompetitorInEvent.Attach(CompetitorInEv);
                AthlCompetitorInEvent.DeleteObject(CompetitorInEv);
                AthlCompetitorInEvent.SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw new InvalidOperationException(string.Format(
                    "Gat ekki eytt keppanda í grein með rásnúmeri '{0}' í móti {1}",
                    CompetitorInEv.rasnumer.ToString(),
                    CompetitorInEv.mot));
            }
        }

        public Int32 RetNextLineNoInEvent(string CompCode, Int32 EventLineNo)
        {
            Int32 NextLinNoInt = 0;
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            var LastLineNo = AthlEnt.ReturnLastLineInEvent(CompCode, EventLineNo);

            foreach (var result in LastLineNo)
            {
                NextLinNoInt = Convert.ToInt32(result.Value) + 10000;
            }
            return (NextLinNoInt);

        }

        public void UpdCompetitorInEvent(string CompCode, Int32 EventLineNo, Int32 BibNo, decimal Time, decimal Meters, decimal Wind,
            string ResultText, decimal Attempt1, decimal Wind1, decimal Attempt2, decimal Wind2, decimal Attempt3, decimal Wind3,
            decimal Attempt4, decimal Wind4, decimal Attempt5, decimal Wind5, decimal Attempt6, decimal Wind6, string Series, Int32 ElectricalTiming,
            Int32 Merking1, Int32 Merking2, Int32 Merking3, Int32 Merking4, Int32 Merking5, Int32 Merking6, decimal Sortorder1, decimal Sortorder2,
            Int32 NanariRod, string UrslitaRodTexti, Int32 SamaSaetiOgNaestiAUndan, string Athugasemd, Int32 HeatNo, Int32 LaneOrOrder,
            Int32 IAAFPointsIn, Int32 MultiEventPointsIn, Int32 YouthPointsIn, string PerformanceRemarksIn)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthlEnt.UpdCompetitorInEvent(CompCode, EventLineNo, BibNo, Time, Meters, Wind, ResultText, Attempt1, Wind1, Attempt2, Wind2,
                Attempt3, Wind3, Attempt4, Wind4, Attempt5, Wind5, Attempt6, Wind6, Series, ElectricalTiming, Merking1, Merking2, Merking3,
                Merking4, Merking5, Merking6, Sortorder1, Sortorder2, NanariRod, UrslitaRodTexti, SamaSaetiOgNaestiAUndan, Athugasemd,
                HeatNo, LaneOrOrder, IAAFPointsIn, MultiEventPointsIn, YouthPointsIn, PerformanceRemarksIn);
        }

        public Athl_HeightsInHJandPV GetHeightsInHJandPVRec(string CompetitionCode, Int32 EventLineNo, Int32 BibNo)
        {

            using (AthleticsEntities1 AthlHeightsInHJandPV = new AthleticsEntities1())
            {

                try
                {
                    Athl_HeightsInHJandPV HeightsInHJandPV = (from HeightsInHJPV in AthlHeightsInHJandPV.Athl_HeightsInHJandPV
                                                              where (HeightsInHJPV.Mót == CompetitionCode) &&
                                                              (HeightsInHJPV.Greinarnúmer == EventLineNo) &&
                                                              (HeightsInHJPV.Rásnúmer == BibNo)
                                                              select HeightsInHJPV).First();

                    return (HeightsInHJandPV);
                }
                catch
                {
                    AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                    Athl_HeightsInHJandPV EmptyHeights = new Athl_HeightsInHJandPV();
                    EmptyHeights = AthlCRUD.InitHeightsInHJandPV();
                    return EmptyHeights;
                }
            }
        }

        public Athl_HeightsInHJandPV InitHeightsInHJandPV()
        {
            Athl_HeightsInHJandPV HeightsInHJandPVRec = new Athl_HeightsInHJandPV();

            HeightsInHJandPVRec.Mót = "";
            HeightsInHJandPVRec.Greinarnúmer = 0;
            HeightsInHJandPVRec.Lína = 0;
            HeightsInHJandPVRec.Rásnúmer = 0;
            HeightsInHJandPVRec.Árangur = "";
            HeightsInHJandPVRec.Úrslitaröð = 0;
            HeightsInHJandPVRec.Nánari_röð = 0;
            HeightsInHJandPVRec.Sama_sæti_og_næsti_á_undan = 0;
            HeightsInHJandPVRec.C1__hæð = "";
            HeightsInHJandPVRec.C2__hæð = "";
            HeightsInHJandPVRec.C3__hæð = "";
            HeightsInHJandPVRec.C4__hæð = "";
            HeightsInHJandPVRec.C5__hæð = "";
            HeightsInHJandPVRec.C6__hæð = "";
            HeightsInHJandPVRec.C7__hæð = "";
            HeightsInHJandPVRec.C8__hæð = "";
            HeightsInHJandPVRec.C9__hæð = "";
            HeightsInHJandPVRec.C10__hæð = "";
            HeightsInHJandPVRec.C11__hæð = "";
            HeightsInHJandPVRec.C12__hæð = "";
            HeightsInHJandPVRec.C13__hæð = "";
            HeightsInHJandPVRec.C14__hæð = "";
            HeightsInHJandPVRec.C15__hæð = "";
            HeightsInHJandPVRec.C16__hæð = "";
            HeightsInHJandPVRec.C17__hæð = "";
            HeightsInHJandPVRec.C18__hæð = "";
            HeightsInHJandPVRec.C19__hæð = "";
            HeightsInHJandPVRec.C20__hæð = "";
            HeightsInHJandPVRec.C21__hæð = "";
            HeightsInHJandPVRec.C22__hæð = "";
            HeightsInHJandPVRec.C23__hæð = "";
            HeightsInHJandPVRec.C24__hæð = "";
            HeightsInHJandPVRec.C25__hæð = "";
            HeightsInHJandPVRec.Raðsvæði_1__1000___hæð_ = 0;
            HeightsInHJandPVRec.Raðsvæði_2__yfir_í_tilraun_ = 0;
            HeightsInHJandPVRec.Raðsvæði_3__fjöldi_falla_ = 0;
            HeightsInHJandPVRec.IAAF_Stig = 0;
            HeightsInHJandPVRec.Unglingastig = 0;
            HeightsInHJandPVRec.OpeningHeight = "";
            HeightsInHJandPVRec.FirstIncreaseBy = "";
            HeightsInHJandPVRec.FirstLimit = "";
            HeightsInHJandPVRec.SecondIncreaseBy = "";
            HeightsInHJandPVRec.SecondLimit = "";
            HeightsInHJandPVRec.ThirdIncreaseBy = "";
            return HeightsInHJandPVRec;
        }

        public void DeleteHeAthlHeightsInHJandPV(Athl_HeightsInHJandPV AthlHeightsInHJ)
        {

            try
            {
                AthlHeightsInHJandPV.DeleteObject(AthlHeightsInHJ);
                AthlHeightsInHJandPV.SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw new InvalidOperationException(string.Format(
                    "Gat ekki eytt hæð í hástökki eða stöng  með rásnúmeri '{0}' í móti {1}",
                    AthlHeightsInHJ.Rásnúmer.ToString(),
                    AthlHeightsInHJ.Mót));
            }

        }

        public void ReturnLastLineNosForHJPV(string CompetitionCode, Int32 EventLineNo, out Int32 LastLineCompetitorInEv, out Int32 LastLineInHeights)
        {
            System.Data.Objects.ObjectParameter LastLineComp = new System.Data.Objects.ObjectParameter("LastLineNoOfCompetitorsInEvent", typeof(Int32));
            System.Data.Objects.ObjectParameter LastLineHeight = new System.Data.Objects.ObjectParameter("LastLineNoOfHeights", typeof(Int32));

            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthlEnt.ReturnLastLineNosForHJPV(CompetitionCode, EventLineNo, LastLineComp, LastLineHeight);

            if (LastLineComp.Value.ToString() != "")
            {
                LastLineCompetitorInEv = Convert.ToInt32(LastLineComp.Value);
            }
            else
            {
                LastLineCompetitorInEv = 0;
            }
            if (LastLineHeight.Value.ToString() != "")
            {
                LastLineInHeights = Convert.ToInt32(LastLineHeight.Value);
            }
            else
            {
                LastLineInHeights = 0;
            }
        }
        
        public Int32 RetIAAFPoints(string EventCode, Int32 GenderInt, string Flokkur, Int32 OutdoorsIndoors, decimal TimeResult, decimal MeterResult)         
        {
            Int32 IAAFPoints = 0;

            System.Data.Objects.ObjectParameter IAAFPointsParameter = new System.Data.Objects.ObjectParameter("Points", typeof(Int32));

            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthlEnt.ReturnIAAFPoints(EventCode, GenderInt, Flokkur, OutdoorsIndoors, TimeResult, MeterResult, IAAFPointsParameter);

            if (IAAFPointsParameter.Value.ToString() != "")
            {
                IAAFPoints = Convert.ToInt32(IAAFPointsParameter.Value);
            }
            else
            {
                IAAFPoints = 0;
            }
            return IAAFPoints;
        }

        public Int32 RetYouthPoints(string EventCode, Int32 GenderInt, string Flokkur, Int32 OutdoorsIndoors, string PerformText,
              Int32 CompetitorsAge, Int32 ElectricTiming)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Int32 YouthPts = 0;
            System.Data.Objects.ObjectParameter YouthPointsParameter = new System.Data.Objects.ObjectParameter("PointsOut", typeof(Int32));


            AthlEnt.ReturnYouthPoints(EventCode, GenderInt, Flokkur, OutdoorsIndoors, PerformText, CompetitorsAge, ElectricTiming,
                  YouthPointsParameter);

            if (YouthPointsParameter.Value.ToString() != "")
            {
                YouthPts = Convert.ToInt32(YouthPointsParameter.Value);
            }
            else
            {
                YouthPts = 0;
            }
            return YouthPts;
        }




        public Int32 CheckUserNameAndPw(string UserName, string PassWrd)         
        {
            Int32 AccessLevel = 0;

            System.Data.Objects.ObjectParameter ReturnValue = new System.Data.Objects.ObjectParameter("ReturnValue", typeof(Int32));

            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthlEnt.CheckUserIDAndPassword(UserName, PassWrd, ReturnValue);
            
            if (ReturnValue.Value.ToString() != "")
            {
                AccessLevel = Convert.ToInt32(ReturnValue.Value);
            }
            else
            {
                AccessLevel = 0;
            }
            return AccessLevel;
        }

        public void SortCompetitorsInEventByHeatAndLane(string CompetitionCode, Int32 EventLineNo)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthlEnt.SortCompetitorsInEventByHeatAndLane(CompetitionCode, EventLineNo);
        }

        public Athl_Events GetAtlEvents(string EventCode, Int32 Gender, string AgeGroup, Int32 OutDoorsIndoors)
        {

            using (AthleticsEntities1 AthlEvents = new AthleticsEntities1())
            {

                try
                {
                    Athl_Events AthlEv = (from AthlEvRec in AthlEvents.Athl_Events
                                                              where (AthlEvRec.Grein == EventCode) &&
                                                              (AthlEvRec.Kyn == Gender) &&
                                                              (AthlEvRec.Flokkur == AgeGroup) &&
                                                              (AthlEvRec.Úti_Inni == OutDoorsIndoors)
                                                              select AthlEvRec).First();

                    return (AthlEv);
                }
                catch
                {
                    throw new InvalidOperationException(string.Format(
                    "Gat ekki fundið grein {0} {1} {2} {3} ",
                    EventCode, Gender, AgeGroup, OutDoorsIndoors));
                }
            }
        }

        public Athl_Venues GetVenueRec(string VenueName)
        {

            using (AthleticsEntities1 VenueRecords = new AthleticsEntities1())
            {
                try
                {
                    Athl_Venues AthlVen = (from AthlVenR in VenueRecords.Athl_Venues
                                          where (AthlVenR.Heiti == VenueName)                                           
                                          select AthlVenR).First();

                    return (AthlVen);
                }
                catch
                {
                    throw new InvalidOperationException(string.Format(
                    "Gat ekki fundið völl {0}",
                    VenueName));
                }
            }
        }

    }

}