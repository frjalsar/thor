using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class RelayTeamNames : System.Web.UI.Page
    {

        public string StringOfBibNumbers;
        public string StringOfLegNumers;

        protected void Page_Load(object sender, EventArgs e)
        {
            Global Gl = new Global();

            if (!IsPostBack)
            {
                string LoginUserID = Session["CurrentUserName"].ToString();
                if ((LoginUserID != null) && (LoginUserID != ""))
                {

                    Athl_CompetitionEvents AthlCompEv = new Athl_CompetitionEvents();
                    AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                    CompCode.Text = Gl.GetCompetitionCode();
                    CompName.Text = Gl.GetCompetitionName();
                    EventLineNo.Text = Gl.GetGlobalValue("EventLineNo");
                    CompetitorBibNo.Text = Gl.GetGlobalValue("CompetitorBibNo");
                    Club.Text = Gl.GetGlobalValue("SelectedClub");
                    AthlCompEv = AthlCRUD.GetCompetitionEvent(CompCode.Text, Convert.ToInt32(EventLineNo.Text));
                    EventName.Text = AthlCompEv.heitigreinar;
                    RelayTeamName.Text = Gl.GetGlobalValue("RelayTeamName");
                }
                else
                {
                    CompCode.Text = "";
                    CompName.Text = "";
                    EventLineNo.Text = "";
                    CompetitorBibNo.Text = "";
                    Club.Text = "";
                    Vista.Visible = false;

                    ErrorMsg.Text = "Notandi er ekki lengur skráður inn";
                    return;

                }
            }

        }

        protected void Clear1_Click(object sender, EventArgs e)
        {

        }

        protected void Clear2_Click(object sender, EventArgs e)
        {

        }

        protected void Clear3_Click(object sender, EventArgs e)
        {

        }

        protected void Clear4_Click(object sender, EventArgs e)
        {

        }

        protected void ClearAll_Click(object sender, EventArgs e)
        {

        }

        protected void Vista_Click(object sender, EventArgs e)
        {
            Int32 i;
            DropDownList SelLeg = new DropDownList();
            Int32 SelectedLegNo;
            Int32 SelectedBibNo;
            Global gl = new Global();
            GridViewRow CurrRow;
            Int32 SelCellIndex;

            ErrorMsg.Text = "";

            StringOfBibNumbers = "";
            StringOfLegNumers = "";
            string[] RelayTeamNamesArr = new string[5];

            for (i = 1;i < 5; i++)
            {
                RelayTeamNamesArr[i] = "";
            }
           
            for (i = 0; i < ClubRunnersGridView.Rows.Count; i++)
            {

                SelLeg = (DropDownList)ClubRunnersGridView.Rows[i].FindControl("SelectLeg");
                string SelV = SelLeg.SelectedValue.ToString();

                if (SelLeg.SelectedValue.ToString() != "0")
                {
                    CurrRow = ClubRunnersGridView.Rows[i];
                    SelCellIndex = gl.GetColumnIndexByName(CurrRow, "rasnumer");
                    SelectedLegNo = Convert.ToInt32(SelLeg.SelectedValue.ToString());
                    SelectedBibNo = Convert.ToInt32(CurrRow.Cells[SelCellIndex].Text);

                    if (RelayTeamNamesArr[SelectedLegNo] != "")
                    {
                        ErrorMsg.Text = string.Format("Það eru a.m.k. tveir hlauparar skráðir á sprett númer {0}", SelectedLegNo.ToString());
                        return;
                    }
                    StringOfBibNumbers = StringOfBibNumbers + ";" + SelectedBibNo;
                    StringOfLegNumers = StringOfLegNumers + ";" + SelectedLegNo;
                    RelayTeamNamesArr[SelectedLegNo] = SelectedLegNo.ToString();
                 }
            }
            if (StringOfBibNumbers != "")
            {
                StringOfBibNumbers = StringOfBibNumbers.Substring(1) + ";0;0;0;0;0";
                StringOfLegNumers = StringOfLegNumers.Substring(1) + ";0;0;0;0;0";

                string[] BibNoArr = StringOfBibNumbers.Split(';');
                string[] LegNoArr = StringOfLegNumers.Split(';');

                Int32 EvLineNo = Convert.ToInt32(EventLineNo.Text);
                Int32 CompetitorBibNoInt = Convert.ToInt32(CompetitorBibNo.Text);
                AthleticsEntities1 AthlEnt = new AthleticsEntities1();
                AthlEnt.SetRunnersInRelay(CompCode.Text, EvLineNo, CompetitorBibNoInt, BibNoArr[0], LegNoArr[0],
                    BibNoArr[1], LegNoArr[1], BibNoArr[2], LegNoArr[2], BibNoArr[3], LegNoArr[3]);
                Response.Redirect("RelayTeamNames.aspx");
            }

        }

        protected void ClubRunnersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList SelLeg = new DropDownList();
            GridViewRow CurrRow;
            CurrRow = e.Row;
            Global gl = new Global();
            Int32 ColNoRelayLegNo;
            Int32 ColNoRunnersName;
            Int32 ColNoBibNumbers;


            ColNoRelayLegNo = gl.GetColumnIndexByName(CurrRow, "spretturnr");
            ColNoRunnersName = gl.GetColumnIndexByName(CurrRow, "nafn");
            ColNoBibNumbers = gl.GetColumnIndexByName(CurrRow, "rasnumer");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string LegNoOfCompetitor = e.Row.Cells[ColNoRelayLegNo].Text;
                if (LegNoOfCompetitor != "0")
                {
                    SelLeg = (DropDownList)CurrRow.FindControl("SelectLeg");
                    SelLeg.SelectedValue = e.Row.Cells[ColNoRelayLegNo].Text;
                    switch (LegNoOfCompetitor)
                    {
                        case "1":
                            Runner1.Text = HttpUtility.HtmlDecode(e.Row.Cells[ColNoRunnersName].Text);
                            BibNo1.Text = e.Row.Cells[ColNoBibNumbers].Text;
                            break;
                        case "2":
                            Runner2.Text = HttpUtility.HtmlDecode(e.Row.Cells[ColNoRunnersName].Text);
                            BibNo2.Text = e.Row.Cells[ColNoBibNumbers].Text;
                            break;
                        case "3":
                            Runner3.Text = HttpUtility.HtmlDecode(e.Row.Cells[ColNoRunnersName].Text);
                            BibNo3.Text = e.Row.Cells[ColNoBibNumbers].Text;
                            break;
                        case "4":
                            Runner4.Text = HttpUtility.HtmlDecode(e.Row.Cells[ColNoRunnersName].Text);
                            BibNo4.Text = e.Row.Cells[ColNoBibNumbers].Text;
                            break;
                    }

                }
            }
            e.Row.Cells[ColNoRelayLegNo].Text = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterRelay.aspx?Club=" + Club.Text);
        }
    }
}