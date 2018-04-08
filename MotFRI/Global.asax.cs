using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Net;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Objects;
using System.Text;
using System.Globalization;


namespace MotFRI
{
    public class Global : System.Web.HttpApplication
    {
        protected static string CurrentSessionID = "";
        protected static string SelectedCompetitionCode = "";
        protected static Int32 SelectedCompetitionYear = 0;
        protected static string SelectedEventNo = "";
        protected static string SelectedEventName = "";
        protected static string SelectedGender = "";
        protected static string SelectedEventCode = "";
        protected static string SelectedCompetitionName = "";
        protected static string SelectedCompetitionVenue = "";
        protected static string SelectedKennitala = "";
        protected static string SelectedDate = "";
        protected static string SelCode = "";
        protected static string SelKennit = "";
        protected static string SelNafn = "";
        protected static string SelKyn = "";
        protected static string SelFaedar = "";
        protected static string SelFelag = "";
        protected static string SelAldur = "";
        protected static string SelFjAfreka = "";
        protected static Int32 BibNumber = 0;
        protected static string OutOrIndoors = "";
        protected static bool WindMetered = false;
        protected static string EventType = "";
        protected static decimal WindInHeat = 0;
        protected static Int32 EventTypeInteger = 0;
        protected static string SeedingMethodSelected = "";
        protected static string[] Arr1 = new string[500];
        protected static string[] Arr2 = new string[500];
        protected static Int32 NoOfArrElements;
        //protected static Int32[] BibNoArray = new Int32[500];
        //protected static Int32[] HeatNoArray = new Int32[500];
        //protected static Int32[] LaneOrOrderNoArray = new Int32[500];
        protected static Int32[] BibNoArray = new Int32[500];
        protected static Int32[] HeatNoArray = new Int32[500];
        protected static Int32[] LaneOrOrderNoArray = new Int32[500];
        //protected static Int32 NoOfArrElements;
        //protected static Athl_HeightsInHJandPV AthlCurrentHeightsForHJorPV = new Athl_HeightsInHJandPV();
        protected static Athl_HeightsInHJandPV AthlCurrentHeightsForHJorPV = new Athl_HeightsInHJandPV();
        protected static string CurrAccLev = "";
        protected static string CurrUsrName = "";
 
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup


        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            //if (System.Web.HttpContext.Current.Request.Cookies["MotFRI"] != null)
            //{
            //    HttpCookie myCookie = new HttpCookie("MotFRI");
            //    myCookie.Expires = DateTime.Now.AddDays(-1d);
            //    Response.Cookies.Add(myCookie);
            //}
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            HttpSessionState ThisSessionState = HttpContext.Current.Session;
            CurrentSessionID = ThisSessionState.SessionID;


            //HttpCookie MotFRICookies = new HttpCookie("MotFRI");
            //MotFRICookies["BibNumber"] = "";
            //MotFRICookies["CompetitionCode"] = "";
            //MotFRICookies["EventType"] = "";
            //MotFRICookies["EventTypeInteger"] = "";
            //MotFRICookies["OutOrIndoors"] = "";
            //MotFRICookies["SelAldur"] = "";
            //MotFRICookies["SelCode"] = "";
            //MotFRICookies["SelectedCompetitionName"] = "";
            //MotFRICookies["SelectedCompetitionVenue"] = "";
            //MotFRICookies["SelectedCompetitionYear"] = "";
            //MotFRICookies["SelectedDate"] = "";
            //MotFRICookies["SelectedEventCode"] = "";
            //MotFRICookies["SelectedEventName"] = "";
            //MotFRICookies["SelectedEventNo"] = "";
            //MotFRICookies["SelectedGender"] = "";
            //MotFRICookies["SelectedKennitala"] = "";
            //MotFRICookies["SelFaedar"] = "";
            //MotFRICookies["SelFelag"] = "";
            //MotFRICookies["SelFjAfreka"] = "";
            //MotFRICookies["SelKennit"] = "";
            //MotFRICookies["SelKyn"] = "";
            //MotFRICookies["SelNafn"] = "";
            //MotFRICookies["WindInHeat"] = "";
            //MotFRICookies["WindM"] = "";
            //MotFRICookies["WindMetered"] = "";
            //Response.Cookies.Add(MotFRICookies);
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        public static string MyHtmlEncode(string text)
        {
            char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();
            StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

            foreach (char c in chars)
            {
                int value = Convert.ToInt32(c);
                if (value > 127)
                    result.AppendFormat("&#{0};", value);
                else
                    result.Append(c);
            }

            return result.ToString();
        }
        public void SetGlobalValue(string IDCode, string NewValue)
        {

            //string CurrentSessionID = ""; // Session.SessionID;
            HttpCookie MotFRICookies = new HttpCookie(IDCode);
            //string NewValue2 = MyHtmlEncode(NewValue); //Convert.ToBase64String(NewValue);

            //var bytes = Encoding.UTF8.GetBytes(NewValue);
            string NewValue2 = "";
            //for (int ix = 0; ix < bytes.Length;ix++ )
            //{
            //    NewValue2 = NewValue2 + bytes[ix].ToString() + ",";
            //}

            //for (int ix = 0; ix < NewValue.Length;ix++ )
            //{
            //    string CurrentCharacter = NewValue.Substring(ix, 1);
            //    byte Character = Convert.ToByte(CurrentCharacter);
            //    NewValue2 = NewValue2 + Character.ToString() + ",";
            //}

            int AsciiValue;
            foreach (char c in NewValue)
            {
                AsciiValue = Convert.ToInt32(c);
                NewValue2 = NewValue2 + AsciiValue.ToString() + ",";
            }

            if (NewValue2.Length > 0)
            {
                NewValue2 = NewValue2.Substring(0, NewValue2.Length - 1);
            }

            //string NewValue3 = Convert.ToBase64String(bytes);

            // var e = Encoding.GetEncoding("iso-8859-1");
            // var s = e.GetString(new byte[] { 189 });

            // string NewValue2 = "";
            // byte AsciiValue;
            // for (int ix=0;ix < NewValue.Length;ix++)

            // {
            //     string WrkString = NewValue.Substring(ix, 1);
            ////     var s = e.GetString(new byte[] { 189 });

            //   //  AsciiValue = Convert.ToByte(WrkString);
            //   //  NewValue2 = NewValue2 + AsciiValue.ToString() + ",";
            // } 
            //byte[] ascii = Encoding.ASCII.GetBytes(NewValue);
            //foreach (Byte b in ascii)
            //{
            //    NewValue2 += b.ToString() + ",";
            //}
            //if (NewValue2.Length > 0)
            //{
            //    NewValue2 = NewValue2.Substring(0, NewValue2.Length - 1);
            //}
            //for (int ix = 0; ix < NewValue.Length; ix++)
            //{
            //   // NewValue2 = NewValue2 + bytes[ix].ToString() + ",";
            //    WrkString = Val.Substring(0, CommaPos);
            //    AsciiValue = Convert.ToInt32(WrkString);
            //    char character = (char)AsciiValue;
            //    Val2 = Val2 + character.ToString();

            //}
            //MotFRICookies[IDCode] = NewValue2; //HttpUtility.HtmlDecode(NewValue);         
            //MotFRICookies[IDCode] = HttpUtility.HtmlDecode(NewValue);
            MotFRICookies[IDCode] = NewValue2;
            MotFRICookies.Expires = DateTime.Now.AddDays(1d);
            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies);

            //HttpSessionState ss = HttpContext.Current.Session;
            ////ss.SessionID
            //HttpContext.Current.Session[IDCode] = NewValue;
            //if (System.Web.HttpContext.Current.Request.Cookies["MotFRI"] != null)
            //{
            //   HttpCookie MotFRICookies = System.Web.HttpContext.Current.Request.Cookies["MotFRI"];


            //    switch (IDCode)
            //    {
            //        case "BibNumber":
            //            HttpCookie MotFRICookies01 = new HttpCookie("MotFRIBibNumber");
            //            MotFRICookies01["BibNumber"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies01);
            //            break;
            //        case "CompetitionCode":
            //            HttpCookie MotFRICookies02 = new HttpCookie("MotFRICompetitionCode");
            //            MotFRICookies02["CompetitionCode"] = NewValue;
            //            MotFRICookies02.Expires = DateTime.Now.AddDays(1d);
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies02);
            //            break;
            //        case "EventType":
            //            HttpCookie MotFRICookies03 = new HttpCookie("MotFRIEventType");
            //            MotFRICookies03["EventType"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies03);
            //            break;
            //        case "EventTypeInteger":
            //            HttpCookie MotFRICookies04 = new HttpCookie("MotFRIEventTypeInteger");
            //            MotFRICookies04["EventTypeInteger"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies04);
            //            break;
            //        case "OutOrIndoors":
            //            HttpCookie MotFRICookies05 = new HttpCookie("MotFRIOutOrIndoors");
            //            MotFRICookies05["OutOrIndoors"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies05);
            //            break;
            //        case "SelAdldur":
            //            HttpCookie MotFRICookies06 = new HttpCookie("MotFRISelAldur");
            //            MotFRICookies06["SelAldur"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies06);
            //            break;
            //        case "SelCode":
            //            HttpCookie MotFRICookies07 = new HttpCookie("MotFRISelCode");
            //            MotFRICookies07["SelCode"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies07);
            //            break;
            //        case "SelectedCompetitionName":
            //            HttpCookie MotFRICookies08 = new HttpCookie("MotFRISelectedCompetition");
            //            MotFRICookies08["CompetitionName"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies08);
            //            break;
            //        case "SelectedCompetitionVenue":
            //            HttpCookie MotFRICookies09 = new HttpCookie("MotFRISelectedCompetitionVenue");
            //            MotFRICookies09["SelectedCompetitionVenue"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies09);
            //            break;
            //        case "SelectedCompetitionYear":
            //            HttpCookie MotFRICookies10 = new HttpCookie("MotFRISelectedCompetitionYear");
            //            MotFRICookies10["SelectedCompetitionYear"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies10);
            //            break;
            //        case "SelectedDate":
            //            HttpCookie MotFRICookies11 = new HttpCookie("MotFRISelectedDate");
            //            MotFRICookies11["SelectedDate"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies11);
            //            break;
            //        case "SelectedEventCode":
            //            HttpCookie MotFRICookies12 = new HttpCookie("MotFRISelectedEventCode");
            //            MotFRICookies12["SelectedEventCode"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies12);
            //            break;
            //        case "SelectedEventName":
            //            HttpCookie MotFRICookies13 = new HttpCookie("MotFRISelectedEventName");
            //            MotFRICookies13["SelectedEventName"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies13);
            //            break;
            //        case "SelectedEventNo":
            //            HttpCookie MotFRICookies14 = new HttpCookie("MotFRISelectedEventNo");
            //            MotFRICookies14["SelectedEventNo"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies14);
            //            break;
            //        case "SelectedGender":
            //            HttpCookie MotFRICookies15 = new HttpCookie("MotFRISelectedGender");
            //            MotFRICookies15["SelectedGender"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies15);
            //            break;
            //        case "SelectedKennitala":
            //            HttpCookie MotFRICookies16 = new HttpCookie("MotFRISelectedKennitala");
            //            MotFRICookies16["SelectedKennitala"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies16);
            //            break;
            //        case "SelFaedar":
            //            HttpCookie MotFRICookies17 = new HttpCookie("MotFRISelFaedar");
            //            MotFRICookies17["SelFaedar"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies17);
            //            break;
            //        case "SelFelag":
            //            HttpCookie MotFRICookies18 = new HttpCookie("MotFRISelFelag");
            //            MotFRICookies18["SelFelag"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies18);
            //            break;
            //        case "SelFjAfreka":
            //            HttpCookie MotFRICookies19 = new HttpCookie("MotFRISelFjAfreka");
            //            MotFRICookies19["SelFjAfreka"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies19);
            //            break;
            //        case "SelKennit":
            //            HttpCookie MotFRICookies20 = new HttpCookie("MotFRISelKennit");
            //            MotFRICookies20["SelKennit"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies20);
            //            break;
            //        case "SelKyn":
            //            HttpCookie MotFRICookies21 = new HttpCookie("MotFRISelKyn");
            //            MotFRICookies21["SelKyn"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies21);
            //            break;
            //        case "SelNafn":
            //            HttpCookie MotFRICookies22 = new HttpCookie("MotFRISelNafn");
            //            MotFRICookies22["SelNafn"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies22);
            //            break;
            //        case "WindInHeat":
            //            HttpCookie MotFRICookies23 = new HttpCookie("MotFRIWindInHeat");
            //            MotFRICookies23["WindInHeat"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies23);
            //            break;
            //        case "WindM":
            //            HttpCookie MotFRICookies24 = new HttpCookie("MotFRIWindM");
            //            MotFRICookies24["WindM"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies24);
            //            break;
            //        case "WindMetered":
            //            HttpCookie MotFRICookies25 = new HttpCookie("MotFRIWindMetered");
            //            MotFRICookies25["WindMetered"] = NewValue;
            //            System.Web.HttpContext.Current.Response.Cookies.Add(MotFRICookies25);
            //            break;
            //    }

            ////Debug!!

            //    int loop1, loop2;
            //    HttpCookieCollection MyCookieColl;
            //    HttpCookie MyCookie;
            //    string CookieNamesAndValues = "";

            //    MyCookieColl = System.Web.HttpContext.Current.Request.Cookies; //Request.Cookies;

            //    // Capture all cookie names into a string array.
            //    String[] arr1 = MyCookieColl.AllKeys;

            //    // Grab individual cookie objects by cookie name.
            //    for (loop1 = 0; loop1 < arr1.Length; loop1++)
            //    {
            //        MyCookie = MyCookieColl[arr1[loop1]];
            //        //Response.Write("Cookie: " + MyCookie.Name + "<br>");
            //        //Response.Write("Secure:" + MyCookie.Secure + "<br>");
            //        CookieNamesAndValues = CookieNamesAndValues + " Cookie: " + MyCookie.Name + " value: ";

            //        //Grab all values for single cookie into an object array.
            //        String[] arr2 = MyCookie.Values.AllKeys;

            //        //Loop through cookie Value collection and print all values.
            //        for (loop2 = 0; loop2 < arr2.Length; loop2++)
            //        {
            //            //Response.Write("Value" + loop2 + ": " + Server.HtmlEncode(arr2[loop2]) + "<br>");
            //            CookieNamesAndValues = CookieNamesAndValues + Server.HtmlEncode(arr2[loop2]) + "---";
            //        }
            //    }

        }

        public string GetGlobalValue(string IDCode)
        {

            //string CurrentSessionID = ""; // Session.SessionID;
            if (System.Web.HttpContext.Current.Request.Cookies[IDCode] != null)
            {
                HttpCookie MotFRICookies = System.Web.HttpContext.Current.Request.Cookies[IDCode];
                //           var bytes = Encoding.UTF8.GetBytes(NewValue);
                //Encoding.UTF8.
                string Val = MotFRICookies.Value;

                //string StringOut = "";
                //int value;
                //foreach (char c in Val)
                //{
                //    value = Convert.ToInt32(c);
                //    StringOut += value.ToString("X");
                //    // or finalValue = String.Format("{0}{1:X}", finalValue, value);
                //}
                //return StringOut;

                //string Val3 = Server.HtmlEncode(MotFRICookies.Value);
                // Val = Server.HtmlDecode(MotFRICookies.Value);
                //string Val2 = Server.HtmlEncode(MotFRICookies.Value);
                if (Val.Length > IDCode.Length)
                {
                    Val = Val.Substring(IDCode.Length + 1);
                }
                else
                {
                    Val = "";
                }
                ////int startIndex = mainString.IndexOf(subString);
                //// str.Substring(0, Math.Min(length, str.Length)); if ()
                //string Val3 = "";

                if (Val.Length == 0)
                {
                    return Val;
                }
                else
                {
                    string Val3 = "";
                    string WrkString;
                    Int32 CommaPos = 0;
                    Int32 AsciiValue = 0;
                    byte[] AscVal = new byte[1];
                    var e = Encoding.GetEncoding("iso-8859-1");
                    //var e = Encoding.GetEncoding("utf-32");
                    do
                    {
                        CommaPos = Val.IndexOf(",");
                        if (CommaPos > 0)
                        {
                            WrkString = Val.Substring(0, CommaPos);
                            AsciiValue = Convert.ToInt32(WrkString);
                            //WrkString = MyCharacterConversion(AsciiValue);
                            //Val3 = Val3 + WrkString;
                            //char character = (char)AsciiValue;
                            //Val3 = Val3 + character.ToString();
                            AscVal[0] = (byte)AsciiValue;
                            var s = e.GetString(AscVal);
                            Val3 = Val3 + s.ToString();

                            Val = Val.Substring(CommaPos + 1);
                        }
                        else
                        {
                            //AsciiValue = Convert.ToInt32(Val);
                            //char character = (char)AsciiValue;
                            //Val3 = Val3 + character.ToString();
                            AsciiValue = Convert.ToInt32(Val);
                            AscVal[0] = (byte)AsciiValue;
                            var s = e.GetString(AscVal);
                            Val3 = Val3 + s.ToString();
                            Val = "";
                        }

                    } while (Val.Length > 0);
                    return Val3.Trim();
                }
                //return System.Web.HttpContext.Current.Request.Cookies[IDCode].Value;
            }
            else
            {
                return "";
            }


            //HttpSessionState ss = HttpContext.Current.Session;
            //return (HttpContext.Current.Session[IDCode].ToString());
            //if (System.Web.HttpContext.Current.Request.Cookies["MotFRI"] != null)
            //{
            //    switch (IDCode)
            //    {
            //        case "BibNumber":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRIBibNumber"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRIBibNumber"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "CompetitionCode":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRICompetitionCode"] != null)
            //            {
            //                HttpCookie MotFRICookies02 = System.Web.HttpContext.Current.Request.Cookies["MotFRICompetitionCode"];
            //                string Val = MotFRICookies02.Value;
            //                Val = Server.HtmlEncode(MotFRICookies02.Value);
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRICompetitionCode"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "EventType":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRIEventType"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRIEventType"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "EventTypeInteger":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRIEventTypeInteger"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRIEventTypeInteger"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "OutOrIndoors":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRIOutOrIndoors"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRIOutOrIndoors"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelAldur":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelAldur"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelAldur"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelCode":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelCode"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelCode"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelectedCompitionName":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedCompetitionName"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedCompetitionName"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelectedCompetitionVenue":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedCompetitionVenue"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedCompetitionVenue"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelectedCompetitionYear":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedCompetitionYear"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedCompetitionYear"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelectedDate":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedDate"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedDate"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelectedEventCode":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedEventCode"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedEventCode"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelectedEventName":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedEventName"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedEventName"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelectedEventNo":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedEventNo"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedEventNo"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelectedGender":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedGender"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedGender"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelectedKennitala":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedKennitala"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelectedKennitala"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelFaedar":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelFaedar"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelFaedar"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelFelag":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelFelag"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelFelag"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelFjAfreka":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelFjAfreka"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelFjAfreka"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelKennit":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelKennit"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelKennit"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelKyn":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelKyn"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelKyn"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "SelNafn":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRISelNafn"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRISelNafn"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "WindInHeat":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRIWindInHeat"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRIWindInHeat"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "WindM":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRIWindM"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRIWindM"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;
            //        case "WindMetered":
            //            if (System.Web.HttpContext.Current.Request.Cookies["MotFRIWindMetered"] != null)
            //            {
            //                return System.Web.HttpContext.Current.Request.Cookies["MotFRIWindMetered"].Value;
            //            }
            //            else
            //            {
            //                return "";
            //            }
            //            break;

            //    //}
            //}

            return "";
        }


        public void SetCompetitionCode(string CompCode)
        {
            //Session["CompetitionCode"] = CompCode; //SelectedCompetitonCode = CompCode;
            //SelectedCompetitionCode = CompCode;
            SetGlobalValue("CompetitionCode", CompCode);

        }
        public string GetCompetitionCode()
        {
            //return (SelectedCompetitionCode);
            //return Session["CompetitionCode"].ToString();
            return GetGlobalValue("CompetitionCode");
        }

        //public void SetCompetitionEventNo(string EventCode)
        //{
        //    //SelectedEventNo = EventCode;
        //    //Session["SelectedEventNo"] = EventCode;
        //    SetGlobalValue("SelectedEventNo", EventCode);

        //}
        //public string GetCompetitionEventNo()
        //{
        //    //return (SelectedEventNo);
        //    //return Session["SelectedEventNo"].ToString();
        //    return GetGlobalValue("SelectedEventNo");
        //}

        public void SetCompetitionEventName(string CompEventName)
        {
            //SelectedEventName = CompEventName;
            //Session["SelectedEventName"] = CompEventName;
            SetGlobalValue("SelectedEventName", CompEventName);

        }
        public string GetCompetitonEventName()
        {
            //return (SelectedEventName);
            //return Session["SelectedEventName"].ToString();
            return GetGlobalValue("SelectedEventName");
        }

        public void SetSelectedGender(string SelGender)
        {
            //SelectedGender = SelGender;
            //Session["SelectedGender"] = SelGender;
            SetGlobalValue("SelectedGender", SelGender);
        }

        public string GetSelectedGender()
        {
            //return SelectedGender;
            //return Session["SelectedGender"].ToString();
            return GetGlobalValue("SelectedGender");
        }

        public void SetSelectedEventCode(string SelEventCode)
        {
            //SelectedEventCode = SelEventCode;
            //Session["SelectedEventCode"] = SelEventCode;
            SetGlobalValue("SelectedEventCode", SelEventCode);
        }

        public string GetSelectedEventCode()
        {
            //return SelectedEventCode;
            //return Session["SelectedEventCode"].ToString();
            return GetGlobalValue("SelectedEventCode");
        }

        public void SetCompetitionName(string CompName)
        {
            //SelectedCompetitionName = CompName;
            //Session["SelectedCompetitionName"] = CompName;
            SetGlobalValue("SelectedCompetitionName", CompName);
        }

        public string GetCompetitionName()
        {
            //return SelectedCompetitionName;
            //return Session["SelectedCompetitionName"].ToString();
            return GetGlobalValue("SelectedCompetitionName");
        }

        public void SetCompetitionVenue(string CompVenue)
        {
            //SelectedCompetitionVenue = CompVenue;
            //Session["SelectedCompetitionVenue"] = CompVenue;
            SetGlobalValue("SelectedCompetitionVenue", CompVenue);
        }

        public string GetCompetitionVenue()
        {
            //return SelectedCompetitionVenue;
            //return Session["SelectedCompetitionVenue"].ToString();
            return GetGlobalValue("SelectedCompetitionVenue");
        }
        public void SetSelectedKennitala(string Kennit)
        {
            //SelectedKennitala = Kennit;
            //Session["SelectedKennitala"] = Kennit;
            SetGlobalValue("SelectedKennitala", Kennit);
        }

        public string GetSelectedKennitala()
        {
            //return SelectedKennitala;
            //return Session["SelectedKennitala"].ToString();
            return GetGlobalValue("SelectedKennitala");
        }

        public void SetOutdoorsOrIndoors(string OutOrIn)
        {
            //OutOrIndoors = OutOrIn;
            //Session["OutOrIndoors"] = OutOrIn;
            SetGlobalValue("OutOrIndoors", OutOrIn);
        }

        public void SetSelectedDate(string SelDate)
        {
            //SelectedDate = SelDate;
            //Session["SelectedDate"] = SelDate;
            SetGlobalValue("SelectedDate", SelDate);
        }

        public void SetArr1Value(Int32 ix, string ValueIn)
        {
            Arr1[ix] = ValueIn;
        }

        public void SetArr2Value(Int32 ix, string ValueIn)
        {
            Arr2[ix] = ValueIn;
        }

        public void SetNoOfArrayElements(Int32 NoOfElem)
        {
            NoOfArrElements = NoOfElem;
        }

        public Int32 GetNoOfArrayElements()
        {
            return NoOfArrElements;
        }

        public string GetOutdorrsOrIndoors()
        {
            //return OutOrIndoors;
            //return Session["OutOrIndoors"].ToString();
            return GetGlobalValue("OutOrIndoors");
        }


        public void SetSelComp(string Cod, string Kt, string Naf, string Ky, string Fae, string Fel, string Ald, string FjAfr)
        {
            //SelCode = Cod;
            //SelKennit = Kt;
            //SelNafn = Naf;
            //SelKyn = Ky;
            //SelFaedar = Fae;
            //SelFelag = Fel;
            //SelAldur = Ald;
            //SelFjAfreka = FjAfr;
            //Session["SelCode"] = Cod;
            //Session["SelKennit"] = Kt;
            //Session["SelNafn"] = Naf;
            //Session["SelKyn"] = Ky;
            //Session["SelFaedar"] = Fae;
            //Session["SelFelag"] = Fel;
            //Session["SelAldur"] = Ald;
            //Session["SelFjAfreka"] = FjAfr;
            SetGlobalValue("SelCode", Cod);
            SetGlobalValue("SelKennit", Kt);
            SetGlobalValue("SelNafn", Naf);
            SetGlobalValue("SelKyn", Ky);
            SetGlobalValue("SelFaedar", Fae);
            SetGlobalValue("SelFelag", Fel);
            SetGlobalValue("SelAldur", Ald);
            SetGlobalValue("SelFjAfreka", FjAfr);

        }

        public string GetSelCode()
        {
            //return SelCode;
            //return Session["SelCode"].ToString();
            return GetGlobalValue("SelCode");
        }

        public string GetSelKt()
        {
            //return SelKennit;
            //return Session["SelKennit"].ToString();
            return GetGlobalValue("SelKennit");
        }

        public string GetSelNafn()
        {
            //return SelNafn;
            //return Session["SelNafn"].ToString();
            return GetGlobalValue("SelNafn");
        }

        public string GetSelKyn()
        {
            //return SelKyn;
            //return Session["SelKyn"].ToString();
            return GetGlobalValue("SelKyn");
        }

        public string GetSelFar()
        {
            //return SelFaedar;
            //return Session["SelFaedar"].ToString();
            return GetGlobalValue("SelFaedar");
        }

        public string GetSelFel()
        {
            //return SelFelag;
            //return Session["SelFelag"].ToString();
            return GetGlobalValue("SelFelag");
        }

        public string GetSelAld()
        {
            //return SelAldur;
            //return Session["SelAldur"].ToString();
            return GetGlobalValue("SelAldur");
        }

        public string GetSelFjAfreka()
        {
            //return SelFjAfreka;
            //return Session["SelFjAfreka"].ToString();
            return GetGlobalValue("SelFjAfreka");
        }

        public string GetSelectedDate()
        {
            //return SelectedDate;
            //return Session["SelectedDate"].ToString();
            return GetGlobalValue("SelectedDate");
        }

        public string GetArr1Value(Int32 ix)
        {
            return Arr1[ix];
        }

        public string GetArr2Value(Int32 ix)
        {
            return Arr2[ix];
        }


        public int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                {
                    string xxx = ((BoundField)cell.ContainingField).DataField.ToString();

                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                }
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }

        public Int32 ReturnNextBibno(string CompetitionCode)
        {
            int LastBibNo = 0;
            string LastBibNoString = "";
            int RetCode = 0;
            ObjectParameter LastB = new ObjectParameter("LastBib", "0");

            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            RetCode = AthlEnt.LastBibNo(CompetitionCode, LastB);
            if (RetCode == -1)
            {
                LastBibNoString = LastB.Value.ToString();
                LastBibNo = Convert.ToInt32(LastBibNoString);
                LastBibNo = LastBibNo + 1;
                return (LastBibNo);

            }
            else
            {
                return (1);
            }
                
            
            //SqlConnection conn = new SqlConnection("Data Source=Localhost;Initial Catalog=Athletics;Integrated Security=True");
            //SqlCommand cmd = new SqlCommand("LastBibNo", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter paramReturnValue = new SqlParameter();
            //paramReturnValue.ParameterName = "@CompCode";
            //cmd.Parameters.Add("@CompCode", SqlDbType.VarChar).Value = CompetitionCode;
            //paramReturnValue.ParameterName = "@LastBib";
            //paramReturnValue.SqlDbType = SqlDbType.Int;
            //cmd.Parameters.Add(paramReturnValue);
            //cmd.Parameters["@LastBib"].Direction = ParameterDirection.Output;
            //conn.Open();

            //RetCode = Convert.ToInt16(cmd.ExecuteScalar());
            //if ((paramReturnValue.Value == null) || (paramReturnValue.Value == ""))
            //{
            //    LastBibNo = 0;
            //}
            //else
            //{
            //    LastBibNo = Convert.ToInt16(paramReturnValue.Value);
            //}
            //conn.Close();
            //if (Convert.ToInt32(RetCode) == 0)
            //{
            //    return (Int16)(LastBibNo + 1);
            //}
            //else
            //{
            //    return 1;
            //}
        }

        public void SetCompetionYear(Int32 CompetitionYear)
        {
            //SelectedCompetitionYear = CompetitionYear;
            //Session["SelectedCompetitionYear"] = CompetitionYear.ToString();
            SetGlobalValue("SelectedCompetitionYear", CompetitionYear.ToString());
        }

        public Int32 GetCompetitionYear()
        {
            //return SelectedCompetitionYear;
            //return Convert.ToInt32(Session["SelectedCompetitionYear"].ToString());    
            string Val = GetGlobalValue("SelectedCompetitionYear");
            return Convert.ToInt32(Val);
        }

        public void SetBibNumber(Int32 BibNo)
        {
            //BibNumber = BibNo;
            //Session["BibNumber"] = BibNo.ToString();
            SetGlobalValue("BibNumber", BibNo.ToString());
        }

        public Int32 GetBibNumber()
        {
            //return (BibNumber);
            //return Convert.ToInt32(Session["BibNumber"].ToString());
            return Convert.ToInt32(GetGlobalValue("BibNumber"));
        }

        public void SetEventTypeAndWindMetered(string EvType, bool WindM)
        {
            //EventType = EvType;
            //WindMetered = WindM;
            //Session["EventType"] = EvType;
            //Session["WindM"] = WindM.ToString();
            SetGlobalValue("EventType", EvType);
            SetGlobalValue("WindMetered", WindM.ToString());
        }

        public string GetEventType()
        {
            //return EventType;
            //return Session["EventType"].ToString();
            return GetGlobalValue("EventType");
        }

        public bool GetWindMetered()
        {
            //return WindMetered;
            //return Convert.ToBoolean(Session["WindMetered"].ToString());
            return Convert.ToBoolean(GetGlobalValue("WindMetered"));
        }

        public void SetEventTypeInteger(Int32 EvTypeInteger)
        {
            //EventTypeInteger = EvTypeInteger;
            //Session["EventTypeInteger"] = EvTypeInteger.ToString();
            SetGlobalValue("EventTypeInteger", EvTypeInteger.ToString());
        }

        public Int32 GetEventTypeInteger()
        {
            //return EventTypeInteger;
            //return Convert.ToInt32(Session["EventTypeInteger"].ToString());
            return Convert.ToInt32(GetGlobalValue("EventTypeInteger"));
        }
        public void SetBibNoArrayElement(Int32 Ix, Int32 BibNo)
        {
            BibNoArray[Ix] = BibNo;
        }
        public Int32 GetBibNoArrayElement(Int32 ix)
        {
            return BibNoArray[ix];
        }

        public void SetHeatNoElement(Int32 Ix, Int32 HeatNo)
        {
            HeatNoArray[Ix] = HeatNo;

        }

        public Int32 GetHeatNoElement(Int32 Ix)
        {
            return HeatNoArray[Ix];
        }

        public void SetLaneOrOrder(Int32 Ix, Int32 LaneOrOrderNo)
        {
            LaneOrOrderNoArray[Ix] = LaneOrOrderNo;

        }

        public Int32 GetLaneOrOrder(Int32 Ix)
        {
            return LaneOrOrderNoArray[Ix];
        }

        public void SetWindInHeat(decimal Wind)
        {
            //WindInHeat = Wind;
            //Session["WindInHeat"] = Wind.ToString();
            SetGlobalValue("WindInHeat", Wind.ToString());
        }

        public decimal GetWindInHeat()
        {
            //return WindInHeat;
            //return Convert.ToDecimal(Session["WindInHeat"].ToString());
            string Val = GetGlobalValue("WindInHeat");
            decimal WindInH = 0;
            bool Res = decimal.TryParse(Val, out WindInH);
            if (Res == false)
            {
                WindInH = 0;
            }
            return WindInH; //Convert.ToDecimal(GetGlobalValue("WindInHeat"));
        }

        public void SetCurrentHeightsForHJorPV(Athl_HeightsInHJandPV CurrentHeightsForHJorPV)
        {
            AthlCurrentHeightsForHJorPV = CurrentHeightsForHJorPV;
        }

        public void SetCurrAccLev(string CurrAccLevin)
        {
            CurrAccLev = CurrAccLevin;
        }
        public string GetCurrAccLev()
        {
            return CurrAccLev;
        }
        public void SetCurrUsrName(string CurrUsrNameIn)
        {
            CurrUsrName = CurrUsrNameIn;
        }
        public string GetCurrUsrName()
        {
            return CurrUsrName;
        }

        public Athl_HeightsInHJandPV GetCurrentHeightsForHJorPV()
        {
            return AthlCurrentHeightsForHJorPV;
        }

        public decimal ReturnNumbersOnlyUnder100(string InputString, out string AlphaCharacters, string MaxValueForFieldEv)
        {

            decimal MaxValue = 0;
            if (MaxValueForFieldEv == "")
            {
                MaxValue = 999999;
            }
            else
            {
                MaxValue = Convert.ToDecimal(MaxValueForFieldEv);
            }

            decimal ValueToReturn = ReturnNumbersOnly(InputString, out AlphaCharacters);
            if (ValueToReturn > 100)
                do
                {
                    ValueToReturn = ValueToReturn / 10;
                    ValueToReturn = Math.Floor(ValueToReturn * 100) / 100;
                } while (ValueToReturn > 100);

            ValueToReturn = Math.Abs(ValueToReturn);

            if (ValueToReturn > MaxValue)
            {
                ValueToReturn = ValueToReturn / 10;
                if (ValueToReturn > MaxValue)
                {
                    ValueToReturn = ValueToReturn / 10;
                }
            }
            ValueToReturn = Math.Floor(ValueToReturn * 100) / 100;
            return ValueToReturn;
        }

        public decimal ReturnEnteredWind(string InputString, out string AlphaCharacters)
        {

            decimal ValueToReturn = ReturnNumbersOnly(InputString, out AlphaCharacters);
            if ((ValueToReturn > 100) && (Math.Floor(ValueToReturn) == ValueToReturn))
            {
                ValueToReturn = ValueToReturn / 100;
            }
            //ValueToReturn = Math.Abs(ValueToReturn);
            return ValueToReturn;
        }


        public decimal ReturnNumbersOnly(string InputString, out string AlphaCharacters)
        {
            bool CommaFound = false;
            AlphaCharacters = "";
            decimal NumberToReturn = 0;
            if (InputString == "")
            {
                return NumberToReturn;
            }
            string OutputString = "";
            string ValidNumerals = "0123456789,-";
            string ValidCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÝÞÆÖabcdefghijklmnopqrstuvwxyzáéíóúýþæöð";

            for (int i = 0; i < InputString.Length; i++)
            {
                if (ValidNumerals.IndexOf(InputString.Substring(i, 1)) != -1)
                {
                    OutputString = OutputString + InputString.Substring(i, 1);

                    if (InputString.Substring(i, 1) == ",")
                    {
                        if (CommaFound == true)
                        {
                            OutputString = OutputString.Substring(0, OutputString.Length - 1);
                        }
                        CommaFound = true;
                    }
                }
                if (ValidCharacters.IndexOf(InputString.Substring(i, 1)) != -1)
                {
                    AlphaCharacters = AlphaCharacters + InputString.Substring(i, 1);
                }
            }
            if (OutputString == "")
            {
                NumberToReturn = 0;
            }
            else
            {
                if (OutputString == "-")
                {
                    OutputString = "P";
                    NumberToReturn = 0;
                    return NumberToReturn;
                }
                NumberToReturn = Convert.ToDecimal(OutputString);
            }
            NumberToReturn = Math.Ceiling((NumberToReturn * 100)) / 100;
            return NumberToReturn;
        }

        public string FormatWind(decimal WindIn)
        {
            string OutputValue = "";
            if (WindIn >= 0)
            {
                WindIn = Math.Ceiling(WindIn * 10) / 10; // Math.Round(WindIn, 1, MidpointRounding.AwayFromZero).ToString();
                OutputValue = (WindIn + 0.01M).ToString("0.##");
                OutputValue = OutputValue.Substring(0, OutputValue.Length - 1);
                return "+" + OutputValue;
            }
            else
            {
                WindIn = Math.Floor(WindIn * 10) / 10;
                OutputValue = (WindIn - 0.01M).ToString("0.##");
                OutputValue = OutputValue.Substring(0, OutputValue.Length - 1);
                return OutputValue;
            }
        }

        public string FormatTimeInTrackRace(decimal TimeIn, bool ElectronicTiming)
        {
            string OutputValue = "";
            if (ElectronicTiming == true)
            {
                //TimeIn = Math.Floor((TimeIn + 0.009999M) * 100M) / 100;
                OutputValue = (TimeIn + 0.001M).ToString("#.###");
            }
            else
            {
                //TimeIn = Math.Ceiling((TimeIn + 0.09999M) * 100M) / 100;
                OutputValue = (TimeIn + 0.01M).ToString("#.##");
            }
            OutputValue = OutputValue.Substring(0, OutputValue.Length - 1);
            if (TimeIn > 99.99M)
            {
                if (TimeIn > 999.99M)
                {
                    OutputValue = OutputValue.Substring(0, 2) + ":" + OutputValue.Substring(2);
                }
                else
                {
                    OutputValue = OutputValue.Substring(0, 1) + ":" + OutputValue.Substring(1);
                }
            }

            //if (ElectronicTiming == false)
            //{
            //    OutputValue = OutputValue.Substring(0, OutputValue.Length - 1);
            //}
            return OutputValue;
        }

        public string FormatResultMeters(decimal MetersIn)
        {
            decimal Meters2 = Math.Floor(MetersIn * 1000M) / 1000;
            string OutputValue = (Meters2 + 0.001M).ToString("#.###");
            OutputValue = OutputValue.Substring(0, OutputValue.Length - 1);
            return OutputValue;
        }

        public string FormatHeightInHJorPV(Int32 HeightIn)
        {
            decimal HeightInDecimal = (Convert.ToDecimal(HeightIn) / 100) + 100.001M;
            string FormattedHeight = HeightInDecimal.ToString("#.###");
            FormattedHeight = FormattedHeight.Substring(2);
            FormattedHeight = FormattedHeight.Substring(0, 4);
            return FormattedHeight;

        }

        //public void UpdateResultOrderForEvent(string CompetitionCode, Int32 EventLineNo, Int32 EventType1Track2Technical, string HeatNoFilt)
        //{
        //    AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
        //    AthleticsEntities1 AthlEnt = new AthleticsEntities1();
        //    Int32[] BibNoArray = new Int32[1000];
        //    decimal[,] SortOrdFields = new decimal[1000, 4];
        //    string[] ResultTextArray = new string[1000];
        //    Int32[] SamePlaceAsOneAbove = new Int32[1000];
        //    Int32 NoOfLines = 0;
        //    Int32 LastUpdatedOrder;
        //    bool DifferenceInSortOrders;
        //    //AthlCRUD.UpdateResultOrder(CompetitionCode, EventLineNo, EventType1Track2Technical);

        //    var CompetitorsInEventDataset = AthlEnt.CompetitorsInEventInResultOrder4(CompetitionCode, EventLineNo, HeatNoFilt);

        //    NoOfLines = 0;
        //    foreach (var CompInEvRec in CompetitorsInEventDataset)
        //    {
        //        NoOfLines = NoOfLines + 1;
        //        BibNoArray[NoOfLines] = CompInEvRec.rasnumer;
        //        SortOrdFields[NoOfLines, 1] = CompInEvRec.sortorder1;
        //        SortOrdFields[NoOfLines, 2] = CompInEvRec.sortorder2;
        //        SortOrdFields[NoOfLines, 3] = Convert.ToDecimal(CompInEvRec.nanarirod);
        //        SamePlaceAsOneAbove[NoOfLines] = CompInEvRec.samasaetiognaestiaundan;
        //    }
        //    LastUpdatedOrder = 1;

        //    if (NoOfLines == 1)
        //    {
        //        UpdateOrder(1, 1, CompetitionCode, EventLineNo, BibNoArray);
        //    }
        //    else
        //    {
        //        for (int i = 2; i <= NoOfLines; i++)
        //        {
        //            DifferenceInSortOrders = false;
        //            //if (i == NoOfLines) //Last Line being processed
        //            //{
        //            //    DifferenceInSortOrders = true;
        //            //}
        //            if ((SortOrdFields[i, 1] != SortOrdFields[i - 1, 1]) ||
        //                (SortOrdFields[i, 2] != SortOrdFields[i - 1, 2]) ||
        //                (SortOrdFields[i, 3] != SortOrdFields[i - 1, 3]))
        //            {
        //                DifferenceInSortOrders = true;
        //            }
        //            //if ((SamePlaceAsOneAbove[i] == 0) && (EventType1Track2Technical == 1))
        //            //{
        //            //    DifferenceInSortOrders = true;
        //            //}
        //            if (DifferenceInSortOrders == true)
        //            {
        //                UpdateOrder(LastUpdatedOrder, i - 1, CompetitionCode, EventLineNo, BibNoArray);
        //                LastUpdatedOrder = i;
        //            }
        //        }
        //        UpdateOrder(LastUpdatedOrder, NoOfLines, CompetitionCode, EventLineNo, BibNoArray);
        //    }
        //}

        private void UpdateOrder(int CurrentResultOrder, int i, string CompCode, Int32 EventLineNo, Int32[] BibNoArr)
        {
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Athl_CompetitorsInEvent AthlCompetitorsInEventRec = new Athl_CompetitorsInEvent();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();

            string ResultText;

            for (int Ix = CurrentResultOrder; Ix <= i; Ix++)
            {
                AthlCompetitorsInEventRec = AthlCRUD.GetCompetitorInEvent(CompCode, BibNoArr[Ix], EventLineNo);
                if (CurrentResultOrder == i)
                {
                    ResultText = CurrentResultOrder.ToString();
                }
                else
                {
                    ResultText = CurrentResultOrder.ToString() + "-" + i.ToString();
                }
                AthlEnt.UpdCompetitorInEvent(AthlCompetitorsInEventRec.mot, AthlCompetitorsInEventRec.greinarnumer, AthlCompetitorsInEventRec.rasnumer,
                    AthlCompetitorsInEventRec.timi, AthlCompetitorsInEventRec.metrar, AthlCompetitorsInEventRec.vindur, AthlCompetitorsInEventRec.arangur,
                    AthlCompetitorsInEventRec.tilraun1, AthlCompetitorsInEventRec.vindur1,
                    AthlCompetitorsInEventRec.tilraun2, AthlCompetitorsInEventRec.vindur2,
                    AthlCompetitorsInEventRec.tilraun3, AthlCompetitorsInEventRec.vindur3,
                    AthlCompetitorsInEventRec.tilraun4, AthlCompetitorsInEventRec.vindur4,
                    AthlCompetitorsInEventRec.tilraun5, AthlCompetitorsInEventRec.vindur5,
                    AthlCompetitorsInEventRec.tilraun6, AthlCompetitorsInEventRec.vindur6, AthlCompetitorsInEventRec.seria,
                    AthlCompetitorsInEventRec.rafmagnstimataka, AthlCompetitorsInEventRec.merking1, AthlCompetitorsInEventRec.merking2,
                    AthlCompetitorsInEventRec.merking3, AthlCompetitorsInEventRec.merking4,
                    AthlCompetitorsInEventRec.merking5, AthlCompetitorsInEventRec.merking6, AthlCompetitorsInEventRec.sortorder1,
                    AthlCompetitorsInEventRec.sortorder2, AthlCompetitorsInEventRec.nanarirod, ResultText,
                    AthlCompetitorsInEventRec.samasaetiognaestiaundan, AthlCompetitorsInEventRec.athugasemd,
                    AthlCompetitorsInEventRec.ridillnumer, AthlCompetitorsInEventRec.stokkkastrod, AthlCompetitorsInEventRec.IAAF_Stig,
                    AthlCompetitorsInEventRec.thrautarstig, AthlCompetitorsInEventRec.Unglingastig, 
                    AthlCompetitorsInEventRec.PerformaceRemarks);
            }

        }

        public string ReturnEventDateAndTime(string CompCode, Int32 EventLineNo)
        {
            string EventDateAndTime = "";
            Athl_CompetitionEvents AthlCompEventRec = new Athl_CompetitionEvents();
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            AthlCompEventRec = AthlCRUD.GetCompetitionEvent(CompCode, EventLineNo);
            EventDateAndTime = AthlCompEventRec.dagsetning.ToString("d") + " kl. " + AthlCompEventRec.timi.ToShortTimeString();
            return EventDateAndTime;
        }

        public string GetIpAddress()  // Get IP Address
        {
            string ip = "";            
            //IPHostEntry ipEntry = Dns.GetHostEntry(GetCompCode());
            //IPAddress[] addr = ipEntry.AddressList;
            //ip = addr[2].ToString();
            return ip;
        }
        public static string GetCompCode()  // Get Computer Name
        {
            string strHostName = "";
            strHostName = Dns.GetHostName();
            return strHostName;
        }
                  
        public void SetSeedingMethodSelected(string SelectedValue)
        {
            SeedingMethodSelected = SelectedValue;
            //Session["SeedingMethodSelected"] = SelectedValue;
        }

        public string GetSeedingMethodSelected()
        {
            return SeedingMethodSelected;
            //return Session["SeedingMethodSelected"].ToString();
        }

        public void SeedCompetitors(string CompCode, Int32 EventLineNo, string SeedingMethod)
        //SeedingMethod:
        //"PERFORMANCEBASED"           //  1 : AccordingToYearsBestOrPersonalBest();
        //"RANDOM"                     //  2 : Random();
        //"MANUAL"                     //  3 : Manual();
        //"BESTHEATLAST"               //  4 : BestHeatLast();
        //"200MAND400MINDOORS"         //  5 : TwoAndFourHunderedIndoorsBestHeatLast();
        //"ALPHABETICAL"               //  6 : AlphabeticalOrder();
        //EUROPEAN TEAM CHAMPIONSHIPS  //  7 : Use records in table Brautarskipt, stökk-, kaströð
        //"CANCEL"
        {

            if (SeedingMethod == "CANCEL")
            {
                return;
            }

            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitorsInEvent AthlCompetitorInEvent = new Athl_CompetitorsInEvent();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            //var ResultDataset = AthlEnt.SeedAthletesInCompEvent(CompCode, EventLineNo, SeedingMethod);
            Int32 RetValue = AthlEnt.SortAthletesInCompEvent(CompCode, EventLineNo, SeedingMethod);
            if (RetValue == -1)  //?
            {
                int NoOfHeatsInEvent;
                ObjectParameter NoOfH = new ObjectParameter("OutNoOfHeats", "0");
                //NoOfH.Value = "0";

                RetValue = AthlEnt.ReturnNoOfHeatsInEvent(CompCode, EventLineNo, NoOfH);
                if (RetValue == -1)
                {
                    string NoOfHeats = NoOfH.Value.ToString();
                    NoOfHeatsInEvent = Convert.ToInt32(NoOfHeats);
                    if (NoOfHeatsInEvent > 1)
                    {
                        for (int Ix = 2; Ix <= NoOfHeatsInEvent; Ix++)
                        {
                            var LineNoOfFirstDataSet = AthlEnt.ReturnLineNoOfFirstCompetitorInHeat(CompCode, EventLineNo, Ix);
                            foreach (var LinNoElem in LineNoOfFirstDataSet)
                            {
                                Int32 LineOfNextCompetitorLine;
                                LineOfNextCompetitorLine = Convert.ToInt32(LinNoElem.Value.ToString());
                                AthlCompetitorInEvent = AthlCRUD.InitCompetitorInEvent();
                                AthlCompetitorInEvent.mot = CompCode;
                                AthlCompetitorInEvent.greinarnumer = EventLineNo;
                                AthlCompetitorInEvent.lina = LineOfNextCompetitorLine - 10;
                                AthlCRUD.InsertNewCompetitorInEvRec(AthlCompetitorInEvent);
                            }


                        }
                    }

                }

            }
        }

        private Int32[,] SetUpSeedingOrder(string SeedingMethod, Int32 EventType, Int32 NoOfCompetitorsInEv, Int32 NoOfLanes)
        {
            Int32[,] HeatAndLaneArray = new Int32[500, 2];
            Global gl = new Global();
            Int32 NoOfHeats = 0;
            decimal MaxNoOfCompetitorsInHeat = 0;
            Int32 NoOfCompetitorsinHeat = 0;
            Int32 NoOfRunnersInHeat = 9999;
            Int32 NoOfCompetitorsInThisHeat = 0;
            decimal CompetitorRounding = 0;
            decimal CumulativeCompetitorRounding = 0;
            decimal CenterLane = 0;
            Int32 CurrHeatNo = 1;
            Int32 CurrLaneNo = 0;
            Int32 LaneIndex = 0;
            string Direction = "";

            if (EventType == 1)
            {
                if (NoOfLanes == 0)
                {
                    throw new InvalidOperationException(string.Format(
                     "Þú þarft að fylla út fjölda brauta í hlaupi '{0}'",
                     gl.GetCompetitonEventName()));
                }
                NoOfHeats = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(NoOfCompetitorsInEv) / Convert.ToDecimal(NoOfLanes)));
                MaxNoOfCompetitorsInHeat = Convert.ToDecimal(NoOfCompetitorsInEv) / Convert.ToDecimal(NoOfHeats);
                NoOfCompetitorsinHeat = Convert.ToInt32(Math.Floor(MaxNoOfCompetitorsInHeat));
                NoOfCompetitorsInThisHeat = NoOfCompetitorsinHeat;
                CompetitorRounding = MaxNoOfCompetitorsInHeat - NoOfCompetitorsinHeat;
                CenterLane = Math.Floor(Convert.ToDecimal(NoOfLanes / 2));
            }
            else
            {
                NoOfHeats = 1;
                MaxNoOfCompetitorsInHeat = NoOfCompetitorsInEv;
                NoOfLanes = NoOfCompetitorsInEv;
            }
            for (int CurrLine = 1; CurrLine <= NoOfCompetitorsInEv; CurrLine++)
            {
                switch (SeedingMethod)
                {
                    case "RANDOM":
                    case "ALPHABETICAL":
                        if (EventType == 1)  //Track
                        {
                            if (NoOfRunnersInHeat >= NoOfCompetitorsInThisHeat)
                            {
                                if (NoOfRunnersInHeat < 9999)
                                {
                                    CurrHeatNo = CurrHeatNo + 1;
                                }
                                NoOfRunnersInHeat = 0;
                                NoOfCompetitorsInThisHeat = NoOfCompetitorsinHeat;
                                CumulativeCompetitorRounding = CumulativeCompetitorRounding + CompetitorRounding;
                                if (CumulativeCompetitorRounding > 1)
                                {
                                    CumulativeCompetitorRounding = CumulativeCompetitorRounding - 1;
                                    NoOfCompetitorsInThisHeat = NoOfCompetitorsInThisHeat + 1;
                                }
                                CurrLaneNo = FindFirstLaneNo(CenterLane, NoOfLanes, NoOfCompetitorsInThisHeat);
                            }
                            else
                            {
                                CurrLaneNo = CurrLaneNo + 1;
                            }
                            NoOfRunnersInHeat = NoOfRunnersInHeat + 1;
                        }
                        else
                        {
                            CurrLaneNo = CurrLaneNo + 1;
                        }
                        break;
                    case "PERFORMANCEBASED":
                        if (CurrLine == 1)
                        {
                            CurrHeatNo = 0;
                            LaneIndex = 1;
                            Direction = "UP";
                        }
                        ReturnCorrectHeatAndLaneNo(CurrLine, NoOfHeats, NoOfLanes, MaxNoOfCompetitorsInHeat, Direction, CurrHeatNo, LaneIndex, out CurrHeatNo, out CurrLaneNo, out LaneIndex, out Direction);
                        break;
                    case "BESTHEATLAST":
                    case "200MAND400MINDOORS":
                        throw new InvalidOperationException(string.Format(
                          "Röðun '{0}' er ekki tilbúin", SeedingMethod));
                        break;
                }
                HeatAndLaneArray[CurrLine, 0] = CurrHeatNo;
                HeatAndLaneArray[CurrLine, 1] = CurrLaneNo;

            }
            return HeatAndLaneArray;
        }

        private void ReturnCorrectHeatAndLaneNo(int CurrLine, int NoOfHeats, int NoOfLanes, decimal MaxNoOfRunnersPrHeat, string OldDirection, Int32 OldHeatNo, Int32 OldLaneIndex,
            out Int32 CurrH, out Int32 CurrL, out Int32 LaneIndex, out string Direction)
        {
            string LaneIndexCharacter = "";

            Direction = OldDirection;
            CurrH = OldHeatNo;
            CurrL = 0;
            LaneIndex = OldLaneIndex;

            if (OldDirection == "UP")
            {
                if (CurrH == NoOfHeats)
                {
                    Direction = "DOWN";
                    LaneIndex = LaneIndex + 1;
                }
                else
                {
                    CurrH = CurrH + 1;
                }
            }
            else
            {
                if (CurrH == 1)
                {
                    Direction = "UP";
                    LaneIndex = LaneIndex + 1;
                    if ((CurrLine == 1) && (MaxNoOfRunnersPrHeat < NoOfLanes))
                    {
                        LaneIndex = LaneIndex + Convert.ToInt32(Math.Ceiling(NoOfLanes - MaxNoOfRunnersPrHeat) / 2);
                    }
                }
                else
                {
                    CurrH = CurrH - 1;
                }
            }
            switch (NoOfLanes)
            {
                case 4:
                    LaneIndexCharacter = "2314".Substring(LaneIndex - 1, 1);
                    break;
                case 5:
                    LaneIndexCharacter = "34251".Substring(LaneIndex - 1, 1);
                    break;
                case 6:
                    LaneIndexCharacter = "342516".Substring(LaneIndex - 1, 1);
                    break;
                case 7:
                    LaneIndexCharacter = "4536271".Substring(LaneIndex - 1, 1);
                    break;
                case 8:
                    LaneIndexCharacter = "45362718".Substring(LaneIndex - 1, 1);
                    break;
                case 9:
                    LaneIndexCharacter = "564738291".Substring(LaneIndex - 1, 1);
                    break;
                case 10:
                    LaneIndexCharacter = "564738291A".Substring(LaneIndex - 1, 1);
                    break;
                case 11:
                    LaneIndexCharacter = "6758493A2B1".Substring(LaneIndex - 1, 1);
                    break;
                case 12:
                    LaneIndexCharacter = "6758493A2B1C".Substring(LaneIndex - 1, 1);
                    break;
                case 13:
                    LaneIndexCharacter = "78695A4B3C2D1".Substring(LaneIndex - 1, 1);
                    break;
                case 14:
                    LaneIndexCharacter = "78695A4B3C2D1E".Substring(LaneIndex - 1, 1);
                    break;
                case 15:
                    LaneIndexCharacter = "897A6B5C4D3E2F1".Substring(LaneIndex - 1, 1);
                    break;
                case 16:
                    LaneIndexCharacter = "897A6B5C4D3E2F1G".Substring(LaneIndex - 1, 1);
                    break;
                case 17:
                    LaneIndexCharacter = "9A8B7C6D5E4F3G2H1".Substring(LaneIndex - 1, 1);
                    break;
                case 18:
                    LaneIndexCharacter = "9A8B7C6D5E4F3G2H1I".Substring(LaneIndex - 1, 1);
                    break;
                case 19:
                    LaneIndexCharacter = "AB9C8D7E6F5G4H3I2J1".Substring(LaneIndex - 1, 1);
                    break;
                case 20:
                    LaneIndexCharacter = "AB9C8D7E6F5G4H3I2J1K".Substring(LaneIndex - 1, 1);
                    break;
                case 21:
                    LaneIndexCharacter = "BCAD9E8F7G6H5I4J3K2L1".Substring(LaneIndex - 1, 1);
                    break;
                case 22:
                    LaneIndexCharacter = "BCAD9E8F7G6H5I4J3K2L1M".Substring(LaneIndex - 1, 1);
                    break;
                case 23:
                    LaneIndexCharacter = "CDBEAF9G8H7I6J5K4L3M2N1O".Substring(LaneIndex - 1, 1);
                    break;
                case 24:
                    LaneIndexCharacter = "CDBEAF9G8H7I6J5K4L3M2N1O".Substring(LaneIndex - 1, 1);
                    break;
                case 25:
                    LaneIndexCharacter = "DECFBGAH9I8J7K6L5M4N3O2P1".Substring(LaneIndex - 1, 1);
                    break;
                case 26:
                    LaneIndexCharacter = "DECFBGAH9I8J7K6L5M4N3O2P1Q".Substring(LaneIndex - 1, 1);
                    break;
                case 27:
                    LaneIndexCharacter = "EFDGCHBIAJ9K8L7M6N5O4P3Q2R1".Substring(LaneIndex - 1, 1);
                    break;
                case 28:
                    LaneIndexCharacter = "EFDGCHBIAJ9K8L7M6N5O4P3Q2R1S".Substring(LaneIndex - 1, 1);
                    break;
                case 29:
                    LaneIndexCharacter = "FGEHDICJBKAL9M8N7O6P5Q4R3S2T1".Substring(LaneIndex - 1, 1);
                    break;
                case 30:
                    LaneIndexCharacter = "FGEHDICJBKAL9M8N7O6P5Q4R3S2T1U".Substring(LaneIndex - 1, 1);
                    break;
                //  4 : BrautaRöðun := '2314';
                //  5 : BrautaRöðun := '34251';
                //  6 : BrautaRöðun := '342516';
                //  7 : BrautaRöðun := '4536271';
                //  8 : BrautaRöðun := '45362718';
                //  9 : BrautaRöðun := '564738291';
                // 10 : BrautaRöðun := '564738291A';
                // 11 : BrautaRöðun := '6758493A2B1';
                // 12 : BrautaRöðun := '6758493A2B1C';
                // 13 : BrautaRöðun := '78695A4B3C2D1';
                // 14 : BrautaRöðun := '78695A4B3C2D1E';
                // 15 : BrautaRöðun := '897A6B5C4D3E2F1';
                // 16 : BrautaRöðun := '897A6B5C4D3E2F1G';
                // 17 : BrautaRöðun := '9A8B7C6D5E4F3G2H1';
                // 18 : BrautaRöðun := '9A8B7C6D5E4F3G2H1I';
                // 19 : BrautaRöðun := 'AB9C8D7E6F5G4H3I2J1';
                // 20 : BrautaRöðun := 'AB9C8D7E6F5G4H3I2J1K';
                // 21 : BrautaRöðun := 'BCAD9E8F7G6H5I4J3K2L1';
                // 22 : BrautaRöðun := 'BCAD9E8F7G6H5I4J3K2L1M';
                // 23 : BrautaRöðun := 'CDBEAF9G8H7I6J5K4L3M2N1';
                // 24 : BrautaRöðun := 'CDBEAF9G8H7I6J5K4L3M2N1O';
                // 25 : BrautaRöðun := 'DECFBGAH9I8J7K6L5M4N3O2P1';
                // 26 : BrautaRöðun := 'DECFBGAH9I8J7K6L5M4N3O2P1Q';
                // 27 : BrautaRöðun := 'EFDGCHBIAJ9K8L7M6N5O4P3Q2R1';
                // 28 : BrautaRöðun := 'EFDGCHBIAJ9K8L7M6N5O4P3Q2R1S';
                // 29 : BrautaRöðun := 'FGEHDICJBKAL9M8N7O6P5Q4R3S2T1';
                // 30 : BrautaRöðun := 'FGEHDICJBKAL9M8N7O6P5Q4R3S2T1U';
            }

            switch (LaneIndexCharacter)
            {
                case "1":
                    CurrL = 1;
                    break;
                case "2":
                    CurrL = 2;
                    break;
                case "3":
                    CurrL = 3;
                    break;
                case "4":
                    CurrL = 4;
                    break;
                case "5":
                    CurrL = 5;
                    break;
                case "6":
                    CurrL = 6;
                    break;
                case "7":
                    CurrL = 7;
                    break;
                case "8":
                    CurrL = 8;
                    break;
                case "9":
                    CurrL = 9;
                    break;
                case "A":
                    CurrL = 10;
                    break;
                case "B":
                    CurrL = 11;
                    break;
                case "C":
                    CurrL = 12;
                    break;
                case "D":
                    CurrL = 13;
                    break;
                case "E":
                    CurrL = 14;
                    break;
                case "F":
                    CurrL = 15;
                    break;
                case "G":
                    CurrL = 16;
                    break;
                case "H":
                    CurrL = 17;
                    break;
                case "I":
                    CurrL = 18;
                    break;
                case "J":
                    CurrL = 19;
                    break;
                case "K":
                    CurrL = 20;
                    break;
                case "L":
                    CurrL = 21;
                    break;
                case "M":
                    CurrL = 22;
                    break;
                case "N":
                    CurrL = 23;
                    break;
                case "O":
                    CurrL = 24;
                    break;
                case "P":
                    CurrL = 25;
                    break;
                case "Q":
                    CurrL = 26;
                    break;
                case "R":
                    CurrL = 27;
                    break;
                case "S":
                    CurrL = 28;
                    break;
                case "T":
                    CurrL = 29;
                    break;
                case "U":
                    CurrL = 30;
                    break;
            }

        }

        private int FindFirstLaneNo(decimal CenterLane, int NoOfLanes, decimal NoOfCompetitorsInHeat)
        {
            Int32 StartingLaneNo = 0;
            StartingLaneNo = Convert.ToInt32(CenterLane - Convert.ToDecimal(Math.Floor((Convert.ToDecimal(NoOfCompetitorsInHeat - 1)) / 2)));
            return StartingLaneNo;
        }

        public int TryConvertStringToInt32(string ValueIn)
        {
            bool res;
            Int32 ValueToReturn = 0;
            res = Int32.TryParse(ValueIn, out ValueToReturn);
            if (res == false)
            {
                ValueToReturn = 0;
            }
            return ValueToReturn;
        }
        public Decimal TryConvertStringToDecimal(string ValueIn)
        {
            bool res;
            decimal ValueToReturn = 0;
            res = Decimal.TryParse(ValueIn, out ValueToReturn);
            if (res == false)
            {
                ValueToReturn = 0;
            }
            return ValueToReturn;
        }

        public DateTime TryConvertStringToDate(string DateStringIn)
        {
            bool res;
            Int32 Day = 0;
            Int32 Month = 0;
            Int32 Year = 0;
            string[] DateParts = new string[10];
            DateTime RetDate;
            if (DateStringIn.IndexOf(".") > 0)
            {
                DateParts = DateStringIn.Split('.');
            }
            if (DateStringIn.IndexOf("/") > 0)
            {
                DateParts = DateStringIn.Split('/');
            }
            res = Int32.TryParse(DateParts[0], out Day);
            res = Int32.TryParse(DateParts[1], out Month);
            res = Int32.TryParse(DateParts[2], out Year);
            if ((Day == 0) || (Month == 0) || (Day > 31) || (Month > 12))
            {
                RetDate = DateTime.ParseExact("17530101 00:00:00", "yyyyMMdd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                return (RetDate);
            }
            if (Year < 100)
            {
                Year = Year + 2000;
            }
            string WrkDate = Year.ToString() + (Month + 100).ToString().Substring(1, 2) + (Day + 100).ToString().Substring(1, 2);
            RetDate = DateTime.ParseExact(WrkDate + " 00:00:00", "yyyyMMdd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            return (RetDate);
        }

        public decimal ParseTextAndReturnDec(string TextIn)
        {
            Char chr;
            decimal ValueOut;
            string TextInCleaned = "";

            for (int i = 0; i < TextIn.Length; i++)
            {

                if (TextIn[i].ToString() == ",")
                {
                    TextInCleaned += ","; // TextIn[i];
                }
                else
                {
                    if ((Char.IsDigit(chr = TextIn[i])))
                        TextInCleaned += chr;
                }
            }
            bool Res = decimal.TryParse(TextInCleaned, out ValueOut);
            if (Res == false)
            {
                ValueOut = 0;
            }

            return ValueOut;
        }

        public DateTime TryConvertStringToTime(string TimeStringIn)
        {
            bool res;
            Int32 Hours = 0;
            Int32 Minutes = 0;
            Int32 Seconds = 0;
            Int32 TimeInteger = 0;
            Decimal WrkDecimal;
            string[] TimeParts = new string[10];
            for (int Ix = 0; Ix < 3; Ix++)
            {
                TimeParts[Ix] = "0";
            }
            DateTime RetTime;
            if (TimeStringIn.IndexOf(":") > 0)
            {
                string[] TimeParts2 = new string[10];
                TimeParts2 = TimeStringIn.Split(':');
                int TpLen = TimeParts2.Length;
                for (int Ix = 0; Ix < TpLen; Ix++)
                {
                    TimeParts[Ix] = TimeParts2[Ix];
                }
            }
            if (TimeStringIn.IndexOf(".") > 0)
            {
                string[] TimeParts2 = new string[10];
                TimeParts2 = TimeStringIn.Split('.');
                int TpLen = TimeParts2.Length;
                for (int Ix = 0; Ix < TpLen; Ix++)
                {
                    TimeParts[Ix] = TimeParts2[Ix];
                }
            }
            if ((TimeParts[0] == "0") && (TimeParts[1] == "0") && (TimeParts[2] == "0") && (TimeStringIn != ""))
            {
                res = Int32.TryParse(TimeStringIn, out TimeInteger);
                if (res == true)
                {
                    WrkDecimal = TimeInteger;
                    if (WrkDecimal > 10000)
                    {
                        Hours = Convert.ToInt32(Math.Truncate((WrkDecimal / 10000.0m)));
                        WrkDecimal = WrkDecimal - (Hours * 10000);
                        if (WrkDecimal > 100)
                        {
                            Minutes = Convert.ToInt32(Math.Truncate((WrkDecimal / 100.0m)));
                            WrkDecimal = WrkDecimal - (Minutes * 100);
                        }
                        if (WrkDecimal > 0)
                        {
                            Seconds = Convert.ToInt32(WrkDecimal);
                        }
                    }
                    else
                        if (WrkDecimal > 100)
                        {
                            Hours = Convert.ToInt32(Math.Truncate((WrkDecimal / 100.0m)));
                            WrkDecimal = WrkDecimal - (Hours * 100);
                            if (WrkDecimal > 0)
                            {
                                Minutes = Convert.ToInt32(WrkDecimal);
                            }

                        }
                        else
                        {
                            if (WrkDecimal > 0)
                            {
                                Hours = Convert.ToInt32(WrkDecimal);
                            }
                        }
                    TimeParts[0] = Hours.ToString();
                    TimeParts[1] = Minutes.ToString();
                    TimeParts[2] = Seconds.ToString();
                }
            }
            for (int Ix = 0; Ix < 3; Ix++)
            {
                if (TimeParts[Ix] != "")
                {
                    switch (Ix)
                    {
                        case 0:
                            res = Int32.TryParse(TimeParts[0], out Hours);
                            break;
                        case 1:
                            res = Int32.TryParse(TimeParts[1], out Minutes);
                            break;
                        case 2:
                            res = Int32.TryParse(TimeParts[2], out Seconds);
                            break;
                    }
                }
            }
            if ((Hours > 24) || (Minutes > 60) || (Seconds > 60))
            {
                RetTime = DateTime.ParseExact("17530101 00:00:00", "yyyyMMdd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                return (RetTime);
            }
            string WrkTime = (Hours + 100).ToString().Substring(1, 2) + ":" + (Minutes + 100).ToString().Substring(1, 2) +
                  ":" + (Seconds + 100).ToString().Substring(1, 2);
            RetTime = DateTime.ParseExact("17540101 " + WrkTime, "yyyyMMdd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            return (RetTime);
        }

        internal bool ValidEmailAddress(string emailAddress)
        {

            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    return true;
                }
            }
            return false;
        }

        public Int32 UserCanUpdateMeet(string CompetitionCode, string CurrentUserID)
        {
            Int32 UserCanUpd = 0;
            ObjectParameter CanUpdateParm = new ObjectParameter("CanUpdate", "0");

            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthlEnt.CanUserUpdateMeet(CompetitionCode, CurrentUserID, CanUpdateParm);

            UserCanUpd = Convert.ToInt32(CanUpdateParm.Value.ToString());
            return (UserCanUpd);

        }

        public string ValidateTimeFromText(string TimeTxtIn)
        {
            string TimeTxtOut;
            TimeTxtOut = "";

            foreach (char c in TimeTxtIn)
            {
                if (char.IsDigit(c))
                    TimeTxtOut = TimeTxtOut + c;
            }
            switch (TimeTxtOut.Length)
            {
                case 0:
                    TimeTxtOut = "00:00";
                    break;
                case 1:
                    TimeTxtOut = "0" + TimeTxtOut + ":00";
                    break;
                case 2:
                    TimeTxtOut = TimeTxtOut + ":00";
                    break;
                case 3:
                    TimeTxtOut = "0" + TimeTxtOut.Substring(0, 1) + ":" + TimeTxtOut.Substring(1, 2);
                    break;
                default:
                    TimeTxtOut = TimeTxtOut.Substring(0, 2) + ":" + TimeTxtOut.Substring(2, 2);
                    break;
            }
            return (TimeTxtOut);
        }
     
    }
}
