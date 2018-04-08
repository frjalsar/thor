using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Objects;


namespace MotFRI
{
    public partial class SelectCompetitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global Gl = new Global();

                CurrentUser.Text = Session["CurrentUserName"].ToString();
                CompetitionCode.Text = Gl.GetCompetitionCode();
                SearchOrInsertMode.Text = "S";
            }            

        }

        protected void SelectCompetitorGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

            Global Gl = new Global();

            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitorsInCompetition AthlCompetitorInComp = new Athl_CompetitorsInCompetition();
            string CompetitorCode;
            bool WasOkToMove = false;
            string ErrorMessageText = "";

            GridViewRow CurrRow = SelectCompetitorGridView.SelectedRow;
            Int32 IndexForCompetitorCode = Gl.GetColumnIndexByName(CurrRow, "CompetitorCode");
            CompetitorCode = CurrRow.Cells[IndexForCompetitorCode].Text;
            string ChangeToClub = "";
            if (RegistrationAction.SelectedValue.ToString() == "1") //(ChangeClubsCheckBox.Checked == true)
            {
                ChangeToClub = SelectedClub.SelectedValue.ToString();
            }
            else
            {
                ChangeToClub = "";
            }

            AthlCompetitorInComp = AthlCompCRUD.CopyCompetitorToCompetition(CompetitorCode, Gl.GetCompetitionCode(), Gl.GetCompetitionYear(), 
                ChangeToClub, out WasOkToMove, out ErrorMessageText);
            if (WasOkToMove == false)
            {
                ErrorMsgBox.Text = ErrorMessageText;
            }
            //AthlCompetitorInComp = AthlCompCRUD.InitCompetitorInComp();

            //AthlCompetitorInComp.mot = Gl.GetCompetitionCode();
            //AthlCompetitorInComp.rasnumer = Gl.ReturnNextBibno(AthlCompetitorInComp.mot);
            //string KeppNo = SelectCompetitorGridView.SelectedRow.Cells[1].Text;
            //KeppNo = HttpUtility.HtmlDecode(KeppNo);
            //AthlCompetitorInComp.keppendanumer = KeppNo;
            //if (SelectCompetitorGridView.SelectedRow.Cells[2].Text == "Karl")
            //{
            //    AthlCompetitorInComp.kyn = 1;
            //}
            //else
            //{
            //    AthlCompetitorInComp.kyn = 2;
            //}
            //AthlCompetitorInComp.kennitala = SelectCompetitorGridView.SelectedRow.Cells[3].Text;
            //string Name = SelectCompetitorGridView.SelectedRow.Cells[4].Text;
            //Name = HttpUtility.HtmlDecode(Name);
            //AthlCompetitorInComp.nafn = Name;
            //AthlCompetitorInComp.faedingarar = Convert.ToInt16(SelectCompetitorGridView.SelectedRow.Cells[5].Text);
            //AthlCompetitorInComp.aldurkeppanda = Convert.ToInt32(SelectCompetitorGridView.SelectedRow.Cells[6].Text);
            //string Club = SelectCompetitorGridView.SelectedRow.Cells[7].Text;
            //Club = HttpUtility.HtmlDecode(Club);
            //AthlCompetitorInComp.felag = Club;
            ////string NoOfPerf = SelectCompetitorGridView.SelectedRow.Cells[8].Text;
            //AthlCompetitorInComp.faedingardagur = Convert.ToDateTime(SelectCompetitorGridView.SelectedRow.Cells[9].Text);
            //string Land = SelectCompetitorGridView.SelectedRow.Cells[10].Text;
            //Land = HttpUtility.HtmlDecode(Land);
            //AthlCompetitorInComp.land = Land;
 
            AthlCompCRUD.InsertCompetitorInCompetition(AthlCompetitorInComp);

            //Gl.SetSelComp(CompCode, Kennitala, Name, Gender, YearOfBirth, Club, Age, NoOfPerf);

            Response.Redirect("~/CompetitorsForUser.aspx");

           
        }

        protected void InsertSelected_Click(object sender, EventArgs e)
        {

            if (RegistrationAction.SelectedValue.ToString() == "0")
            {
                string strMsg = "Þú verður að tilgreina hvort þú viljir flytja valda keppendur yfir í félagið þitt!";
                Response.Write("<script>alert('" + strMsg + "')</script>");
                        //string Jascript = "<script language="\""javascript\" type=\"text/javascript\">alert('" + strMsg + "');</script>";
                        //Response.Write(Jascript);
                return;
            }
            Int32 i;
            Int32 SelCellIndex;
            Int32 CompetitionYear;
            string CompetitorCode;
            CheckBox SelectedCompetitor;
            Global gl = new Global();
            string CompCode = gl.GetCompetitionCode();
            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitorsInCompetition AthlCompetitorInComp = new Athl_CompetitorsInCompetition();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();

            GridViewRow CurrRow;
            bool WasOkToMove = false;
            string Kennit = "";
            string CurrentClub = "";
            string ErrorMsgText;
            string UseThisClub = SelectedClub.SelectedValue.ToString();
            string MoveToClub;
            if (RegistrationAction.SelectedValue.ToString() == "2") //(ChangeClubsCheckBox.Checked == true)
            {
                MoveToClub = UseThisClub;
            }
            else
            {
                MoveToClub = "";
            }
            CompetitionYear = Convert.ToInt32(gl.GetCompetitionYear());
            for (i = 0; i < SelectCompetitorGridView.Rows.Count; i++)  //Mens Events
            {

                SelectedCompetitor = (CheckBox)SelectCompetitorGridView.Rows[i].FindControl("ValinChk");
                if (SelectedCompetitor.Checked)
                {
                    ((CheckBox)SelectCompetitorGridView.Rows[i].FindControl("ValinChk")).Checked = false;

                    CurrRow = SelectCompetitorGridView.Rows[i];
                    SelCellIndex = gl.GetColumnIndexByName(CurrRow, "Félag");
                    CurrentClub = CurrRow.Cells[SelCellIndex].Text;
                    SelCellIndex = gl.GetColumnIndexByName(CurrRow, "CompetitorCode");
                    CompetitorCode = CurrRow.Cells[SelCellIndex].Text;

                    if ((CompetitorCode == "") || (CompetitorCode == "&nbsp;"))
                    { 
                        SelCellIndex = gl.GetColumnIndexByName(CurrRow, "Kennitala");
                        Kennit = CurrRow.Cells[SelCellIndex].Text;
                        if (Kennit != "")
                        {
                            string ClubForNewCompetitor = UseThisClub;
                            if (MoveToClub != "")
                            {
                                ClubForNewCompetitor = MoveToClub;
                            }
                            ObjectParameter NewCompetitorNo;
                            NewCompetitorNo = new ObjectParameter("CompetitorCodeOut", "");

                            AthlEnt.RegisterCompetitorFromNatReg(Kennit, ClubForNewCompetitor, NewCompetitorNo);
                            CompetitorCode = NewCompetitorNo.Value.ToString();
                        }
                    }

                    AthlCompetitorInComp = AthlCompCRUD.CopyCompetitorToCompetition(CompetitorCode, CompCode, CompetitionYear, MoveToClub, out WasOkToMove, out ErrorMsgText);

                    if ((WasOkToMove == false) && ((ErrorMsgText == "") == false))
                    {
                        Response.Write("<script>alert('" + ErrorMsgText + "')</script>");
                        return;
                    }
                    AthlCompCRUD.InsertCompetitorInCompetition(AthlCompetitorInComp);
                }
            }

            Response.Redirect("SelectedCompetitionCompetitors.aspx?Code=" + CompCode);

        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
 
            NameForDS.Text = "X" + NafnText.Text;
            KennitForDS.Text = "X" + KennitalaText.Text;
            YearOfBirthForDS.Text = "X" + FaedArText.Text;
            if (SearchNatReg.Checked == true)
            {
                SearchInNattRegForDS.Text = "X1";
            }
            else
            {
                SearchInNattRegForDS.Text = "X0";
            }

            SelectCompetitorGridView.DataBind();
        }      
    
    }
}