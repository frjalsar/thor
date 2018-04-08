using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Objects;
using System.Web.UI.WebControls;



namespace MotFRI
{


    public partial class RegisterRelay : System.Web.UI.Page
    {
        static int ColNoEventLineNo = 0;
        static int ColNoLineType = 0;
        static int ColNoEventName = 0;
        static int ColNoEventDate = 0;
        static int ColNoEventTime = 0;
        static int ColNoNewTeamButton = 0;
        static int ColNoTeamName = 0;
        static int ColNoClub = 0;
        static int ColNoRunners = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();

            if (!IsPostBack)
            {
                CompetitionName.Text = gl.GetCompetitionName();
                CompCode.Text = gl.GetCompetitionCode();

                LoginUserID.Text = Session["CurrentUserName"].ToString();
                if ((LoginUserID.Text != null) && (LoginUserID.Text != ""))
                {

                    string RequestedClub = Request.QueryString.Get("Club");
                    if ((RequestedClub != "") && (RequestedClub != null))
                    {
                        ClubDropDown.SelectedValue = RequestedClub;
                    }
                }
            }
            else
            {
                RelaysGrid.DataBind();
            }
        }

        protected void RelaysGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

                    //        <asp:LinkButton ID="NewTeam" Text="Nýskr" runat="server" CommandName="InsertNewTeam" 
                    //CommandArgument='<%# Eval("EventLineNo") %>' ></asp:LinkButton>


            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
  
            GridViewRow CurrRow;
            CurrRow = e.Row;
            Global gl = new Global();

            if ((ColNoEventLineNo + ColNoLineType) == 0)
            {
                ColNoEventLineNo = gl.GetColumnIndexByName(CurrRow, "EventLineNo");
                ColNoLineType = gl.GetColumnIndexByName(CurrRow, "LineType");
                ColNoEventName = gl.GetColumnIndexByName(CurrRow, "EventName");
                ColNoEventDate = gl.GetColumnIndexByName(CurrRow, "EventDate");
                ColNoEventTime = gl.GetColumnIndexByName(CurrRow, "EventTime");
                ColNoNewTeamButton = ColNoEventTime + 1;
                ColNoTeamName = gl.GetColumnIndexByName(CurrRow, "RelayTeamName");
                ColNoClub = gl.GetColumnIndexByName(CurrRow, "Club");
                ColNoRunners = gl.GetColumnIndexByName(CurrRow, "Runners");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lnkNames = (LinkButton)e.Row.FindControl("NameOfRunners");
                LinkButton lnk = (LinkButton)e.Row.FindControl("LineComm");
  
                if (e.Row.Cells[ColNoLineType].Text == "1")  //Event Name and Date
                {
                    e.Row.Cells[ColNoEventName].Font.Bold = true;
                    e.Row.Cells[ColNoEventDate].Font.Bold = true;
                    e.Row.Cells[ColNoEventTime].Font.Bold = true;
                    e.Row.Cells[ColNoRunners].Text = null;
                    e.Row.Cells[ColNoTeamName].Text = null;
                    e.Row.Cells[ColNoClub].Text = null;
                    e.Row.Cells[ColNoRunners].Text = null;
                    lnk.Text = "Nýskrá";
                    lnk.CommandName = "NewTeam";
                    lnkNames.Text = "";
                    lnkNames.CommandName = "";
                    //e.Row.Cells[ColNoNewTeamButton].Text = "Nýskrá";
                }
                else  //Relay Team and Runners
                {
                    e.Row.Cells[ColNoEventName].Text = null;
                    e.Row.Cells[ColNoEventDate].Text = null;
                    e.Row.Cells[ColNoEventTime].Text = null;
                    lnk.Text = "Eyða";
                    lnk.CommandName = "DeleteTeam";
                    lnkNames.Text = "Nöfn";
                    lnkNames.CommandName = "Runners";
                    //e.Row.Cells[ColNoNewTeamButton].Text = "Eyða";
                }
            }
            e.Row.Cells[ColNoLineType].Text = null;
            e.Row.Cells[ColNoEventLineNo].Text = null;
        }


        protected void RelaysGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Global Gl = new Global();

            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            string CommandN = e.CommandName;
            string CommandArg = e.CommandArgument.ToString();
            string[] EventNoAndBib = CommandArg.Split(';');

            if (CommandN == "DeleteTeam")
            {
                //                <script type="text/javascript">
                //    function myTestFunction()
                //    {
                //        if (confirm('Do you want to proceed ?'))
                //        {
                //            return true;
                //        }
                //        else
                //        {
                //            return false;
                //        }
                //    }
                //</script>

                //            ScriptManager.RegisterStartupScript(this, typeof(string), "confirm",
                //            "myTestFunction();", true);
                //Hér vantar confirm  Yes No !!!

                AthlEnt.DeleteCompetitorInEvent(CompCode.Text, Convert.ToInt32(EventNoAndBib[0]), Convert.ToInt32(EventNoAndBib[1]));
                //Eyða þarf Skipan boðhlaupssveitar hér
                AthlEnt.DeleteCompetitorInComp(CompCode.Text, CommandArg[1]);
                Response.Redirect("RegisterRelay.aspx");
            }

            if (CommandN == "Runners")
            {
                Gl.SetCompetitionCode(CompCode.Text);
                Gl.SetGlobalValue("EventLineNo", EventNoAndBib[0]);
                Gl.SetGlobalValue("SelectedClub", ClubDropDown.SelectedValue.ToString());
                Gl.SetGlobalValue("RelayTeamName", EventNoAndBib[2]);
                Gl.SetGlobalValue("CompetitorBibNo", EventNoAndBib[1]);
                Response.Redirect("RelayTeamNames.aspx");

            }

                Int32 EventLineNo = Convert.ToInt32(EventNoAndBib[0]);   //e.CommandArgument);
            AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitionEvents AthlEvent = new Athl_CompetitionEvents();
            Athl_Competition AthlCompetition = new Athl_Competition();
            Athl_CompetitorsInCompetition AthlCompInComp = new Athl_CompetitorsInCompetition();
            Athl_CompetitorsInEvent AthlCompInEv = new Athl_CompetitorsInEvent();
            
           
            var RelayInfoDs =  AthlEnt.PrepareToRegisterNewRelayTeam(
                  CompCode.Text, EventLineNo, ClubDropDown.SelectedValue.ToString());
            
            ///     NewBibNoOut, GenderOut, TeamNameOut, YearOfBirthOut);
            Int32 NewBibNo = 0;
            Int32 Gender = 0;
            string TeamName = "";
            Int32 YearOfBirth = 0;
            Int32 LastLineNoInEvent = 0;

            foreach (var RelayInfoRec in RelayInfoDs)
            {
                NewBibNo = Convert.ToInt32(RelayInfoRec.NewBibNoOut.ToString());
                Gender = Convert.ToInt32(RelayInfoRec.GenderOut.ToString());
                TeamName = RelayInfoRec.TeamNameOut.ToString();
                YearOfBirth = Convert.ToInt32(RelayInfoRec.YearOfBirthOut.ToString());
                LastLineNoInEvent = Convert.ToInt32(RelayInfoRec.LastLineNoInEvent.ToString());
            }


            AthlEvent = AthlCRUD.GetCompetitionEvent(CompCode.Text, EventLineNo);
            AthlCompetition = AthlCRUD.GetCompetitionRec(CompCode.Text);

            AthlCompInComp = AthlCRUD.InitCompetitorInComp();
            AthlCompInComp.mot = CompCode.Text;
            AthlCompInComp.rasnumer = NewBibNo;
            AthlCompInComp.kyn = Gender;
            AthlCompInComp.nafn = TeamName;
            AthlCompInComp.faedingarar = YearOfBirth;
            AthlCompInComp.felag = ClubDropDown.SelectedValue.ToString();
            AthlCompInComp.aldurkeppanda = AthlCompetition.Date.Year - YearOfBirth;
            AthlCompInComp.bodhlaupssveit = 1;
            AthlCRUD.InsertCompetitorInCompetition(AthlCompInComp);

            AthlCompInEv = AthlCRUD.InitCompetitorInEvent();
            
            AthlCompInEv.mot = CompCode.Text;
            AthlCompInEv.greinarnumer = EventLineNo;
            AthlCompInEv.rasnumer = NewBibNo;
            AthlCompInEv.nafn = TeamName;
            AthlCompInEv.faedingarar = YearOfBirth;
            AthlCompInEv.felag = ClubDropDown.SelectedValue.ToString();
            AthlCRUD.RegisterCompetitorInEvent(CompCode.Text, AthlEvent, NewBibNo, AthlCompInComp, 0, 0);

            Response.Redirect("RegisterRelay.aspx?Club=" + ClubDropDown.SelectedValue.ToString());

            
        }
    }
}