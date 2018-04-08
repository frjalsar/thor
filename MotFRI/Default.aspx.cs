using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;



namespace MotFRI
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string YearNow = DateTime.Now.Year.ToString();
                VeljaAr.SelectedValue = YearNow;
            }


            //        Þetta var í aspx-inu
            //                   <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Motalisti.aspx">Mótalisti (Stored Procedure)</asp:HyperLink>
            //    <br />

            //    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Keppnisvellir.aspx">Keppnisvellir</asp:HyperLink>
            //    <br />
            //    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CompetitionSetup.aspx">Skrá nýtt mót</asp:HyperLink><br />
            //<br />
            //    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/CompetitorsInCompetition.aspx">Keppendur móts</asp:HyperLink><br />

            // AccessLevelText = "0" : Not logged in - Aðeins fyrirspurnir
            // AccessLevelText = "1" : Club Representative 
            // AccessLevelText = "2" : Administrator
            // AccessLevelText = "3" : Can edit entry sheets
            // AccessLevelText = "4" : Competition Administrator

            InsertNewCompetitionHyperlink.Visible = false;
            var CurrAccLevlVar = Session["CurrentAccessLevel"];
            if (CurrAccLevlVar != null)
            {
                string AccessLevelText = Session["CurrentAccessLevel"].ToString();
                if ((AccessLevelText == "1") || (AccessLevelText == "2"))
                {
                   InsertNewCompetitionHyperlink.Visible = true;
                }
            }

            Global Gl = new Global();
            Gl.SetCompetitionCode("");


        }

        protected void MotArsins_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global Gl = new Global();

            string CompetitionCode = MotArsins.SelectedValue.ToString();
            Gl.SetCompetitionCode(CompetitionCode);

            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
            Athl_Competition AthlComp = new Athl_Competition();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthlComp = AthlCompCRUD.GetCompetitionRec(CompetitionCode);
            Gl.SetGlobalValue("CompetitionName", AthlComp.Name);
            Gl.SetGlobalValue("EnglishCompetitionName", AthlComp.ensktheitiamoti);
            Gl.SetOutdoorsOrIndoors(AthlComp.OutdoorsOrIndoors.ToString());

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

            //Response.Redirect("NyttMot.aspx?Comp=" + CompetitionCode);
            // Response.Redirect("NyttMot.aspx"); 
            Int32 CompetitionHasBeenFinalized = 0;

            System.Data.Objects.ObjectParameter CompetitionStatus = new System.Data.Objects.ObjectParameter("CompetitionStatus", "0");
            ///AthlEnt.ReturnNoOfHeatsInEvent(CompCode, EventLin, OutNoOfHeats);
            AthlEnt.CompetitionIsFinalized(CompetitionCode, CompetitionStatus);

            CompetitionHasBeenFinalized = Convert.ToInt32(CompetitionStatus.Value);

            if (CompetitionHasBeenFinalized == 1)
            {
                Response.Redirect("SelectedCompetitionResults.aspx?Code=" + CompetitionCode);
            }
            else
            {
                Response.Redirect("SelectedCompetitionEvents.aspx?Code=" + CompetitionCode);
            }
        }

        
    }
}
