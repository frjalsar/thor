using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;
using System.Web.UI.HtmlControls;


namespace MotFRI
{


    public partial class UpdCompEvents : System.Web.UI.Page
    {
        static Int32[] EventLineArr = new Int32[1000];
        static string[] DateArr = new string[1000];
        static string[] TimeArr = new string[1000];
        static string[] CompetitionDateArr = new string[4];
        static string[] EventDescriptionArr = new string[1000];
        static DateTime[] CompetitionDateArrWithYear = new DateTime[4];
        static Int32[] NoOfLanesArr = new Int32[1000];
        static Int32[] NoOfRoundsArr = new Int32[1000];
        static Int32[] AgeFromsArr = new Int32[1000];
        static Int32[] AgeToArr = new Int32[1000];
        static Int32[] NoOfCompetitorsArr = new Int32[1000];

        static int NoOfDatesinCompetition;
        static Int32 NoOfEventLines = 0;
        static bool ArraysHaveBeenPopulated = false;
        static string CompDate1;


        protected void Page_Load(object sender, EventArgs e)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Global gl = new Global();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_Competition AthlComp = new Athl_Competition();
            Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();
            if (!IsPostBack)
            {
                var CurrAccLevlVar = Session["CurrentAccessLevel"];
                if (CurrAccLevlVar == null)
                {
                    string CurrUsrN = gl.GetCurrUsrName();
                    string CurrAccLevl = gl.GetCurrAccLev();
                    Session["CurrentAccessLevel"] = CurrAccLevl;
                    Session["CurrentUserName"] = CurrUsrN;

                }
                // AccessLevelText = "0" : Not logged in - Aðeins fyrirspurnir
                // AccessLevelText = "1" : Club Representative 
                // AccessLevelText = "2" : Administrator
                // AccessLevelText = "3" : Can edit entry sheets
                // AccessLevelText = "4" : Competition Administrator

                NoOfEventLines = 0;
                CompCode.Text = gl.GetCompetitionCode();

                string WrkCompCode = Request.QueryString.Get("Code");
                if ((WrkCompCode != "") && (WrkCompCode != null))
                {
                    CompCode.Text = WrkCompCode;
                    gl.SetCompetitionCode(CompCode.Text);
                }
                AthlComp = AthlCRUD.GetCompetitionRec(CompCode.Text);

                //                ÓsamþykktAfFRÍ,
                //                SamþykktAfFRÍ,
                //                OpiðFyrirSkráningu,
                //                LokaðFyrirSkráningu,
                //                StendurYfir,
                //                KeppniLokið
                bool ShowSaveValuesButton = false;
                if ((Session["CurrentAccessLevel"].ToString() == "2") || (Session["CurrentAccessLevel"].ToString() == "3") ||
                    (Session["CurrentAccessLevel"].ToString() == "4") ||
                    (gl.UserCanUpdateMeet(CompCode.Text, Session["CurrentUserName"].ToString()) == 1))  //2 = Admin)
                {
                    ShowSaveValuesButton = true;
                }
                if (AthlComp.Staða_móts == 5)
                {
                    ShowSaveValuesButton = false;
                }
                SaveValues.Visible = ShowSaveValuesButton;

                CompName.Text = AthlComp.Name;
                CompDate1 = string.Format("{0:dd.MM.yyyy}", AthlComp.Date);
                for (int ix = 0; ix < 4; ix++)
                {
                    CompetitionDateArr[ix] = null;
                    CompetitionDateArrWithYear[ix] = Convert.ToDateTime("1900-01-01");
                }
                CompetitionDateArr[0] = ReturnIslWeekDayAndDDMM(AthlComp.Date);
                CompetitionDateArrWithYear[0] = AthlComp.Date; //ToString("yyyy-MM-dd");  //string.Format("{0:YYYY.MM.dd}", AthlComp.Date);
                NoOfDatesinCompetition = 1;
                if (AthlComp.Date2 > Convert.ToDateTime("1900-01-01"))
                {
                    CompetitionDateArr[1] = ReturnIslWeekDayAndDDMM(AthlComp.Date2);
                    CompetitionDateArrWithYear[1] = AthlComp.Date2; //ToString("yyyy-MM-dd"); // string.Format("{0:YYYY.MM.dd}", AthlComp.Date2);
                    NoOfDatesinCompetition = 2;
                    if (AthlComp.Date3 > Convert.ToDateTime("1900-01-01"))
                    {
                        CompetitionDateArr[2] = ReturnIslWeekDayAndDDMM(AthlComp.Date3);
                        CompetitionDateArrWithYear[2] = AthlComp.Date3;   //ToString("yyyy-MM-dd");  // string.Format("{0:YYYY.MM.dd}", AthlComp.Date3);
                        NoOfDatesinCompetition = 3;
                        if (AthlComp.dagsetning4 > Convert.ToDateTime("1900-01-01"))
                        {
                            CompetitionDateArr[3] = ReturnIslWeekDayAndDDMM(AthlComp.dagsetning4);
                            CompetitionDateArrWithYear[3] = AthlComp.dagsetning4; //ToString("yyyy-MM-dd");  //string.Format("{0:YYYY.MM.dd}", AthlComp.dagsetning4);
                            NoOfDatesinCompetition = 4;
                        }
                    }
                }
            }
        }

        protected void EventsGW_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int ColEventLine = 0;
            int ColDate = 0;
            int ColTime = 0;
            int ColDateDropDown = 0;
            int ColNoOfLanes = 0;
            int ColNoOfRounds = 0;
            int ColNoAgeFrom = 0;
            int ColNoAgeTo = 0;
            int ColNoTypeOfEvent = 0;
            int ColNoCloserType = 0;
            int ColNoEventDescription = 0;
            int ColNoNoOfCompetitors = 0;

            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            GridViewRow CurrRow;
            CurrRow = e.Row;
            Global gl = new Global();

            ColEventLine = gl.GetColumnIndexByName(CurrRow, "lina");
            ColDate = gl.GetColumnIndexByName(CurrRow, "dagsetning");
            ColTime = gl.GetColumnIndexByName(CurrRow, "timi");
            ColDateDropDown = gl.GetColumnIndexByName(CurrRow, "DropDDate");
            ColNoOfLanes = gl.GetColumnIndexByName(CurrRow, "fjoldiibrauta");
            ColNoOfRounds = gl.GetColumnIndexByName(CurrRow, "fjoldiumferda");
            ColNoAgeFrom = gl.GetColumnIndexByName(CurrRow, "aldurfra");
            ColNoAgeTo = gl.GetColumnIndexByName(CurrRow, "aldurtil");
            ColNoTypeOfEvent = gl.GetColumnIndexByName(CurrRow, "tegundgreinar");
            ColNoEventDescription = gl.GetColumnIndexByName(CurrRow, "heitigreinar");
            ColNoCloserType = gl.GetColumnIndexByName(CurrRow, "nanaritegundargreining");
            ColNoNoOfCompetitors = gl.GetColumnIndexByName(CurrRow, "NoOfCompetitorsInEv");

            if ((e.Row.RowType == DataControlRowType.DataRow) && (ArraysHaveBeenPopulated == false))
            {
                NoOfEventLines = NoOfEventLines + 1;
                EventLineArr[NoOfEventLines] = Convert.ToInt32(e.Row.Cells[ColEventLine].Text);
                TimeArr[NoOfEventLines] = e.Row.Cells[ColTime].Text;
                DropDownList ddl = (DropDownList)e.Row.FindControl("DropDDate");
                int ElementNo = 0;
                foreach (string colName in CompetitionDateArr)
                {
                    ElementNo = ElementNo + 1;
                    if (ElementNo <= NoOfDatesinCompetition)
                    {
                        ddl.Items.Add(new ListItem(colName));
                    }
                }
                ddl.SelectedValue = HttpUtility.HtmlDecode(e.Row.Cells[ColDate].Text);
                DateArr[NoOfEventLines] = HttpUtility.HtmlDecode(e.Row.Cells[ColDate].Text);
                NoOfLanesArr[NoOfEventLines] = gl.TryConvertStringToInt32(e.Row.Cells[ColNoOfLanes].Text);
                NoOfRoundsArr[NoOfEventLines] = gl.TryConvertStringToInt32(e.Row.Cells[ColNoOfRounds].Text);
                AgeFromsArr[NoOfEventLines] = gl.TryConvertStringToInt32(e.Row.Cells[ColNoAgeFrom].Text);
                AgeToArr[NoOfEventLines] = gl.TryConvertStringToInt32(e.Row.Cells[ColNoAgeTo].Text);
                NoOfCompetitorsArr[NoOfEventLines] = gl.TryConvertStringToInt32(e.Row.Cells[ColNoNoOfCompetitors].Text);
                EventDescriptionArr[NoOfEventLines] = HttpUtility.HtmlDecode(e.Row.Cells[ColNoEventDescription].Text);
                string TypeOfEv = e.Row.Cells[ColNoTypeOfEvent].Text;
                string CloserType = HttpUtility.HtmlDecode(e.Row.Cells[ColNoCloserType].Text);
                if (TypeOfEv == "Hlaup")
                {
                    TextBox NoOfRndsTextBox = (TextBox)e.Row.FindControl("NoOfRounds");
                    NoOfRndsTextBox.Text = "";
                    NoOfRndsTextBox.Enabled = false;
                }
                else
                {
                    TextBox NoOfLanesTextBox = (TextBox)e.Row.FindControl("NoOfLanes");
                    NoOfLanesTextBox.Text = "";
                    NoOfLanesTextBox.Enabled = false;
                    if (CloserType == "Hástökk - Stangarstökk")
                    {
                        TextBox NoOfRndsTextBox = (TextBox)e.Row.FindControl("NoOfRounds");
                        NoOfRndsTextBox.Text = "";
                        NoOfRndsTextBox.Enabled = false;
                    }
                }

            }
            e.Row.Cells[ColEventLine].Text = null;
            e.Row.Cells[ColDate].Text = null;
            e.Row.Cells[ColTime].Text = null;
            e.Row.Cells[ColNoOfRounds].Text = null;
            e.Row.Cells[ColNoOfLanes].Text = null;
            e.Row.Cells[ColNoAgeFrom].Text = null;
            e.Row.Cells[ColNoAgeTo].Text = null;
            e.Row.Cells[ColNoTypeOfEvent].Text = null;
            e.Row.Cells[ColNoEventDescription].Text = null;
            e.Row.Cells[ColNoCloserType].Text = null;

        }

        protected string ReturnIslWeekDayAndDDMM(DateTime DateInput)
        {
            string DateOutput;
            DateOutput = DateInput.ToString("dddd", new CultureInfo("is-IS")).Substring(0, 3);
            DateOutput = DateOutput.ToUpper().Substring(0, 1) + DateOutput.Substring(1, 2);
            DateOutput = DateOutput + " " + string.Format("{0:dd.MM}", DateInput);
            return DateOutput;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Athl_CompetitionEvents AthlCompEvRec = new Athl_CompetitionEvents();
            Global gl = new Global();
            for (int Ixx = 1; Ixx <= NoOfEventLines; Ixx++)
            {
                CheckBox DeleteEv = (CheckBox)EventsGW.Rows[Ixx - 1].FindControl("DeleteChk");
                if (DeleteEv.Checked)
                {
                    if (NoOfCompetitorsArr[Ixx] == 0)
                    {
                        AthlEnt.DeleteCompetitionEvent(CompCode.Text, EventLineArr[Ixx]);
                    }
                    else
                    {
                        string msg = string.Format("Það er ekki hægt að eyða grein -{0}- vegna þess að það eru keppendur skráðir í hana.",
                              EventDescriptionArr[Ixx]);
                        Response.Write("<script>alert('" + msg + "')</script>");

                        return;
                    }
                }
                else
                {
                    DropDownList DropDownListDate = (DropDownList)EventsGW.Rows[Ixx - 1].FindControl("DropDDate");
                    string DDDataValueField = DropDownListDate.DataValueField.ToString();
                    string DDValue = DropDownListDate.SelectedValue.ToString();
                    TextBox TimeTxt = (TextBox)EventsGW.Rows[Ixx - 1].FindControl("TimiEd");
                    TextBox NoOfRoundsTxt = (TextBox)EventsGW.Rows[Ixx - 1].FindControl("NoOfRounds");
                    TextBox NoOfLanesTxt = (TextBox)EventsGW.Rows[Ixx - 1].FindControl("NoOfLanes");
                    TextBox AgeFromTxt = (TextBox)EventsGW.Rows[Ixx - 1].FindControl("AgeFrom");
                    TextBox AgeToTxt = (TextBox)EventsGW.Rows[Ixx - 1].FindControl("AgeTo");
                    TextBox EventDescrTxt = (TextBox)EventsGW.Rows[Ixx - 1].FindControl("EventDescription");


                    Int32 NoOfRounds = gl.TryConvertStringToInt32(NoOfRoundsTxt.Text);
                    Int32 NoOfLanes = gl.TryConvertStringToInt32(NoOfLanesTxt.Text);
                    Int32 AgeFrom = gl.TryConvertStringToInt32(AgeFromTxt.Text);
                    Int32 AgeTo = gl.TryConvertStringToInt32(AgeToTxt.Text);

                    bool Bool1 = DropDownListDate.SelectedValue.ToString() != DateArr[Ixx];
                    string NewTimeText = gl.ValidateTimeFromText(TimeTxt.Text);

                    bool Bool2 = NewTimeText != TimeArr[Ixx]; //TimeTxt.Text
                    bool Bool3 = NoOfRounds != NoOfRoundsArr[Ixx];
                    bool Bool4 = NoOfLanes != NoOfLanesArr[Ixx];
                    bool Bool5 = AgeFrom != AgeFromsArr[Ixx];
                    bool Bool6 = AgeTo != AgeToArr[Ixx];
                    bool Bool7 = EventDescrTxt.Text != EventDescriptionArr[Ixx];

                    if ((DropDownListDate.SelectedValue.ToString() != DateArr[Ixx]) || (NewTimeText != TimeArr[Ixx]) || // (TimeTxt.Text != TimeArr[Ixx]) ||
                        (NoOfRounds != NoOfRoundsArr[Ixx]) || (NoOfLanes != NoOfLanesArr[Ixx]) ||
                        (AgeFrom != AgeFromsArr[Ixx]) || (AgeTo != AgeToArr[Ixx]) ||
                        (EventDescrTxt.Text != EventDescriptionArr[Ixx]))
                    {

                        Int32 IndexOfNewDate = Array.IndexOf(CompetitionDateArr, DropDownListDate.SelectedItem.ToString());

                        DateTime NewTimeForEv = System.DateTime.Parse("1754-01-01 " + NewTimeText); // TimeTxt.Text);

                        AthlEnt.UpdateEventDateTimeEtc(CompCode.Text, EventLineArr[Ixx], CompetitionDateArrWithYear[IndexOfNewDate], NewTimeForEv,
                            NoOfLanes, NoOfRounds, AgeFrom, AgeTo, EventDescrTxt.Text);
                    }
                }
            }
            Response.Redirect("UpdCompEvents.aspx");
        }

        protected void BackToEvents_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectedCompetitionEvents.aspx");
        }
    }
}