using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class Statistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Special Icelandic Characters
            //
            //Example: http://thor.fri.is/Statistics.aspx?club=%c3%81&y=2015
            //
            //This will fill out the Club field with Á (Ármann) and the dates with 01.01.2015 to 31.12.2015
            //
            //Á = %c3%81
            //É = %c3%89
            //Ý = %c3%8d
            //Ó = %c3%93
            //Ú = %c3%9a
            //Ý = %c3%9d
            //Þ = %c3%9e
            //Æ = %c3%86
            //Ö = %c3%96
            //Ð = %c3%90
            //á = %c3%a1
            //é = %c3%a9
            //í = %c3%ad
            //ó = %c3%b3
            //ú = %c3%ba
            //ý = %c3%bd
            //þ = %c3%be
            //æ = %c3%a6
            //ö = %c3%b6
            //ð = %c3%b0



            string FromQueryString;
            if (!IsPostBack)
            {
                DateFr.Text = "01-01-1900";
                DateTo.Text = "31-12-2099";
                AgeFr.Text = "0";
                AgeTo.Text = "999";
                SelectedClubToSubmit.Text = "%";
                CompetitionGrCode.Text = "%";
                Gend.Text = "1";
                OutInd.Text = "0";
                LeagalWind.Checked = true;

                FromQueryString = Request.QueryString.Get("club");
                if ((FromQueryString != "") && (FromQueryString != null))
                {
                    SelectedClub.Text = FromQueryString;
                    SelectedClubToSubmit.Text = FromQueryString;
                }
                FromQueryString = Request.QueryString.Get("y");
                if ((FromQueryString == "") || (FromQueryString == null))
                {
                    FromQueryString = Request.QueryString.Get("year");
                }
                if ((FromQueryString != "") && (FromQueryString != null))
                {
                    DateFr.Text = "01-01-" + FromQueryString;
                    DateTo.Text = "31-12-" + FromQueryString;
                    SelectYear.SelectedValue = FromQueryString;
                }
                FromQueryString = Request.QueryString.Get("g");  //Gender
                if ((FromQueryString == "") || (FromQueryString == null))
                {
                    FromQueryString = Request.QueryString.Get("k");  //Kyn or Gender
                }
                if ((FromQueryString != "") && (FromQueryString != null))
                {
                    if ((FromQueryString.ToUpper() == "F") || (FromQueryString.ToUpper().Substring(0, 2) == "KO"))
                    {
                        Gender.SelectedValue = "2";
                    }
                    else
                    {
                        Gender.SelectedValue = "1";
                    }
                }
                FromQueryString = Request.QueryString.Get("o");  //Outdoors/indoors
                if ((FromQueryString == "") || (FromQueryString == null))
                {
                    FromQueryString = Request.QueryString.Get("oi");  //Outdoors/indoors
                }
                if ((FromQueryString != "") && (FromQueryString != null))
                {
                    if (FromQueryString.ToUpper().Substring(0, 1) == "U")
                    {
                        OutOrIndoors.SelectedValue = "0";
                    }
                    else
                    {
                        OutOrIndoors.SelectedValue = "1";
                    }
                }
                FromQueryString = Request.QueryString.Get("a");  //AgeGroup
                if ((FromQueryString != "") && (FromQueryString != null))
                {
                    AgeGroup.SelectedValue = "0";
                    AgeFr.Text = "0";
                    AgeTo.Text = "999";

                    switch (FromQueryString)
                    {
                        case "20":
                            AgeGroup.SelectedValue = "1";
                            AgeFr.Text = "20";
                            AgeTo.Text = "22";
                            break;
                        case "18":
                            AgeGroup.SelectedValue = "2";
                            AgeFr.Text = "18";
                            AgeTo.Text = "19";
                            break;
                        case "16":
                            AgeGroup.SelectedValue = "3";
                            AgeFr.Text = "16";
                            AgeTo.Text = "17";
                            break;
                        case "15":
                            AgeGroup.SelectedValue = "4";
                            AgeFr.Text = "15";
                            AgeTo.Text = "15";
                            break;
                        case "14":
                            AgeGroup.SelectedValue = "5";
                            AgeFr.Text = "14";
                            AgeTo.Text = "14";
                            break;
                        case "13":
                            AgeGroup.SelectedValue = "6";
                            AgeFr.Text = "13";
                            AgeTo.Text = "13";
                            break;
                        case "12":
                            AgeGroup.SelectedValue = "7";
                            AgeFr.Text = "12";
                            AgeTo.Text = "12";
                            break;
                    }
                }
            }
            else
            {
                if (SelectYear.SelectedValue.ToString() != LastSelectedYear.Text)
                {
                    if (SelectYear.SelectedValue == "9999")
                    {
                        DateFr.Text = "01-01-1900";
                        DateTo.Text = "31-12-2099";
                    }
                    else
                    {
                        DateFr.Text = "01-01-" + SelectYear.SelectedValue;
                        DateTo.Text = "31-12-" + SelectYear.SelectedValue;
                    }
                }
                LastSelectedYear.Text = SelectYear.SelectedValue.ToString();

                if (Gender.SelectedValue.ToString() != Gend.Text)
                {
                    Gre.Text = "N/A";
                }
                Gend.Text = Gender.SelectedValue.ToString();

                if (OutOrIndoors.SelectedValue.ToString() != OutInd.Text)
                {
                    Gre.Text = "N/A";
                }
                OutInd.Text = OutOrIndoors.SelectedValue.ToString();

                if (AgeGroup.SelectedValue != LastSelectedAgeGroup.Text)
                {
                    switch (AgeGroup.SelectedValue)
                    {
                        case "0":
                            AgeFr.Text = "0";
                            AgeTo.Text = "999";
                            break;
                        case "1":
                            AgeFr.Text = "20";
                            AgeTo.Text = "22";
                            break;
                        case "2":
                            AgeFr.Text = "18";
                            AgeTo.Text = "19";
                            break;
                        case "3":
                            AgeFr.Text = "16";
                            AgeTo.Text = "17";
                            break;
                        case "4":
                            AgeFr.Text = "15";
                            AgeTo.Text = "15";
                            break;
                        case "5":
                            AgeFr.Text = "14";
                            AgeTo.Text = "14";
                            break;
                        case "6":
                            AgeFr.Text = "13";
                            AgeTo.Text = "13";
                            break;
                        case "7":
                            AgeFr.Text = "12";
                            AgeTo.Text = "12";
                            break;

                    }
                }
                LastSelectedAgeGroup.Text = AgeGroup.SelectedValue.ToString();

                if (LeagalWind.Checked)
                {
                    WindFrom.Text = "-500";
                    WindTo.Text = "2";
                }
                else
                {
                    WindFrom.Text = "-5000";
                    WindTo.Text = "5000";
                }

                if (ForeignCitizens.Checked)
                {
                    Foreigner.Text = "2";
                }
                else
                {
                    Foreigner.Text = "0";
                }

                if (AllEventsToSelect.SelectedValue != LastSelectedAllEvents.Text)
                {
                    string CombinedKey = AllEventsToSelect.SelectedValue.ToString();
                    int inx = CombinedKey.IndexOf(";");

                    if (inx != -1)
                    {
                        Flo.Text = " ";
                        Gre.Text = CombinedKey.Substring(0, inx);

                        CombinedKey = CombinedKey.Substring(inx + 1);
                        inx = CombinedKey.IndexOf(";");
                        if (inx != -1)
                        {
                            CombinedKey = CombinedKey.Substring(inx + 1);
                            inx = CombinedKey.IndexOf(";");

                            if (inx > 0)
                            {
                                Flo.Text = CombinedKey.Substring(0, inx);

                            }
                        }
                    }
                }
                LastSelectedAllEvents.Text = AllEventsToSelect.SelectedValue.ToString();

                if (SelectedClub.Text == "")
                {
                    SelectedClubToSubmit.Text = "%";
                }
                else
                {
                    SelectedClubToSubmit.Text = SelectedClub.Text;
                }

            }

        }
    }
}