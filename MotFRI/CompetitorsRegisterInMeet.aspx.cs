using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class CompetitorsInCompetition : System.Web.UI.Page
    {
        string CurrAccessLev = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // User Access: 
            //0 : Queries Only,
            //1 : Club Representative,
            //2 : Administrator,
            //3 : Selected Competition Only
            //case 0:
            //      AccessLevel.Text = "Aðeins fyrirspurnir";
            //      AccessToClubs.Visible = false;
            //      break;
            //  case 1:
            //      AccessLevel.Text = "Fulltrúi félags";
            //      AccessToClubs.Visible = true;
            //      break;
            //  case 2:
            //      AccessLevel.Text = "Administrator";
            //      AccessToClubs.Visible = false;
            //      break;

            ////< asp:TableRow >

            ////     < asp:TableCell > &nbsp;</ asp:TableCell >

            ////         < asp:TableCell > Tegund íþróttafélaga </ asp:TableCell >

            ////              < asp:TableCell >< asp:DropDownList ID = "TypeOfClub" runat = "server" Width = "200px" >

            ////             < asp:ListItem Value = "1" > Íþróttafélög </ asp:ListItem >

            ////                    < asp:ListItem Value = "3" > Skokkklúbbar </ asp:ListItem >

            ////                           < asp:ListItem Value = "4" > Fyrirtæki </ asp:ListItem >

            ////                                  < asp:ListItem Value = "5" > Skólar </ asp:ListItem >

            ////                                         < asp:ListItem Value = "6" > Lönd </ asp:ListItem >

            ////                                                < asp:ListItem Value = "7" > Annað </ asp:ListItem >

            ////                                                       < asp:ListItem Value = "8" > Ófélagsbundin(n) </ asp:ListItem >

            ////                                                              < asp:ListItem Value = "0" > Öll félög </ asp:ListItem >

            ////                                                                     </ asp:DropDownList ></ asp:TableCell >

            ////                                                                            </ asp:TableRow >
      //      < asp:DropDownList ID = "SelectClub" runat = "server"
      //              DataSourceID = "SelectClubFromDB" DataTextField = "Name" DataValueField = "Félag" ></ asp:DropDownList ></ asp:TableCell >



            if (!IsPostBack)
            {

                PerformanceYears.SelectedIndex = 2;
                PerformanceYears02.SelectedIndex = 2;
                Global gl = new Global();
                CurrentUserName.Text = Session["CurrentUserName"].ToString();
                ListItem newItem = new ListItem();
                newItem = new ListItem();
                newItem.Text = "*Öll þín félög";
                newItem.Value = "%" + CurrentUserName.Text;
                SelUserClub.Items.Add(newItem);
                if (CurrentUserName.Text == "")
                {
                    CurrentUserName.Text = "--NONE--";
                }
                CompCode.Text = gl.GetCompetitionCode();
                CurrAccessLev = Session["CurrentAccessLevel"].ToString();
                if (CurrAccessLev == "")
                {
                    CurrAccessLev = "0";
                }
                Int32 CurrentAccessLevel = Convert.ToInt32(CurrAccessLev);
                switch (CurrentAccessLevel)
                {
                    case 0:
                        SelectionTable01.Visible = false;
                        SelectionTable02.Visible = false;
                        ShowCompetitors.Visible = false;
                        //AccessToClubs.Visible = false;
                        Message.Text = "Þú hefur ekki aðgang að þessum hluta kerfisins";
                        break;

                    case 1:
                        SelectionTable01.Visible = false;
                        SelectionTable02.Visible = true;
                        //AccessToClubs.Visible = true;
                        break;

                    case 2:
                        SelectionTable01.Visible = true;
                        SelectionTable02.Visible = false;
                        //AccessToClubs.Visible = false;
                        break;
                }
            }
            else  //IsPostback
            {
                CurrAccessLev = Session["CurrentAccessLevel"].ToString();
                if (CurrAccessLev == "")
                {
                    CurrAccessLev = "0";
                }
                //Int32 CurrentAccessLevel = Convert.ToInt32(CurrAccessLev);                
                //if (CurrAccessLev == "1")
                //{
                //    SelectedClubOrUser.Text = "USER:" + Session["CurrentUserName"].ToString();
                //    SelectedGender.Text = SelectGender02.SelectedValue;
                //    SelectedPerformanceYears.Text = PerformanceYears02.SelectedValue;
                //    SelectedAgeFrom.Text = AgeFrom02.Text;
                //    SelectedAgeTo.Text = AgeTo02.Text;
                //}
                //if (CurrAccessLev == "2")
                //{
                //    SelectedClubOrUser.Text = "CLUB:" + SelectClub.SelectedValue;
                //    SelectedGender.Text = SelectGender.SelectedValue;
                //    SelectedPerformanceYears.Text = PerformanceYears.SelectedValue;
                //    SelectedAgeFrom.Text = AgeFrom.Text;
                //    SelectedAgeTo.Text = AgeTo.Text;
                //}
           //     if (CurrAccessLev == "1")
           //     {
           //         SelectedClub.Text = SelUserClub.SelectedValue.ToString();
          //      }
          //      else
          //      {
          //          SelectedClub.Text = SelectClub.SelectedValue.ToString();
          //      }
                SelectedClub.Text = SelUserClub.SelectedValue.ToString();


            }
        }

        protected void SelectedCompetitors_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Global gl = new Global();
                Int32 index;
                //Int32 IndexForSelect;
                //Boolean YesNo;

                index = gl.GetColumnIndexByName(e.Row, "Already Registered");
                //IndexForSelect = 0; // gl.GetColumnIndexByName(e.Row, "Velja");
                if (e.Row.Cells[index].Text == "1")
                {
                    e.Row.Cells[index].Text = "Já";
                    //YesNo = true;
                    //e.Row.Cells[IndexForSelect].Text = YesNo.ToString();
                    //((CheckBox)e.Row[IndexForSelect].Text)).Cheked = false;
                    //((CheckBox)NormalEventsMenGrid.Rows[i].FindControl("ValinChk")).Checked = false; 


                    //e.Row.Cells[IndexForSelect].Text = true.ToString();
                }
                else
                {
                    e.Row.Cells[index].Text = "Nei";
                    //YesNo = false;
                    //e.Row.Cells[IndexForSelect].Text = YesNo.ToString();

                    //e.Row.Cells[IndexForSelect].Text = false.ToString();

                }
            }


            //if (e.Row.Cells[9].Text == "1")  //Event Name and Date
            //{

            //    //e.Row.BackColor = System.Drawing.Color.CadetBlue;
            //    //e.Row.ForeColor = System.Drawing.Color.Green;

            //    e.Row.Cells[0].Text = e.Row.Cells[7].Text;
            //    e.Row.Cells[0].Font.Bold = true;
            //    e.Row.Cells[0].Font.Italic = true;
            //    e.Row.Cells[0].Font.Size = 14;
            //    e.Row.Cells[0].ColumnSpan = 6;
            //    e.Row.Cells[1].Text = e.Row.Cells[8].Text;
            //    e.Row.Cells[1].Font.Bold = true;
            //    e.Row.Cells[1].Font.Italic = true;
            //    e.Row.Cells[1].Font.Size = 14;
            //    e.Row.Cells[2].Text = null;
            //    e.Row.Cells[3].Text = null;
            //    e.Row.Cells[4].Text = null;
            //    e.Row.Cells[5].Text = null;
            //    e.Row.Cells[6].Text = null;
            //    e.Row.Cells[7].Text = null;
            //    e.Row.Cells[8].Text = null;
            //    e.Row.Cells[9].Text = null;

            //}
            //else
            //{
            //    e.Row.Cells[7].Text = " ";
            //    e.Row.Cells[8].Text = " ";
            //    e.Row.Cells[9].Text = " ";

            //}


        }

        protected void InsertCompetitors_Click(object sender, EventArgs e)
        {
            Int32 i;
            Int32 SelCellIndex;
            Int32 CompetitionYear;
            string MoveToClub = "";
            bool WasOkToMove;
            string ErrorText;
            string CompetitorCode;
            CheckBox SelectedEvent;
            Global gl = new Global();
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            AthleticCompetitionCRUD AthlCompCRUD = new AthleticCompetitionCRUD();
            Athl_CompetitorsInCompetition AthlCompetitorInComp = new Athl_CompetitorsInCompetition();
            GridViewRow CurrRow;

            CompetitionYear = gl.GetCompetitionYear();
            for (i = 0; i < SelectedCompetitors.Rows.Count; i++)  //Mens Events
            {
                SelectedEvent = (CheckBox)SelectedCompetitors.Rows[i].FindControl("ValinChk");
                if (SelectedEvent.Checked)
                {
                    ((CheckBox)SelectedCompetitors.Rows[i].FindControl("ValinChk")).Checked = false;

                    //AthlCompetitorInComp = AthlCompCRUD.InitCompetitorInComp();

                    //AthlCompetitorInComp.mot = gl.GetCompetitionCode();
                    //AthlCompetitorInComp.rasnumer = gl.ReturnNextBibno(AthlCompetitorInComp.mot);
                    CurrRow = SelectedCompetitors.Rows[i];
                    SelCellIndex = gl.GetColumnIndexByName(CurrRow, "CompetitorCode");
                    CompetitorCode = CurrRow.Cells[SelCellIndex].Text;

                    //string KeppNo = SelectedCompetitors.SelectedRow.Cells[SelRowIndex].Text;

                    CompetitorCode = CurrRow.Cells[SelCellIndex].Text;

                    ObjectParameter IsRegisteredParameter;
                    IsRegisteredParameter = new ObjectParameter("IsRegistered", typeof(global::System.Int32));

                    AthlEnt.CompetitorIsAlreadyRegisteredInCompetition(CompCode.Text, CompetitorCode, IsRegisteredParameter);

                    Int32 CompetitorIsAlreadyRegistered = Convert.ToInt32(IsRegisteredParameter.Value.ToString());
                    if (CompetitorIsAlreadyRegistered == 0)
                    {
                        AthlCompetitorInComp = AthlCompCRUD.CopyCompetitorToCompetition(CompetitorCode, CompCode.Text, CompetitionYear, MoveToClub, out WasOkToMove, out ErrorText);
                    }
//CopyCompetitorToCompetition(string CompetitorCode, string CompetitonCode, Int32 CompetitionYear, string MoveToClub, 
 //           out bool WasOkToMove, out string ReasonText)
                    //KeppNo = HttpUtility.HtmlDecode(KeppNo);
                    //AthlCompetitorInComp.keppendanumer = KeppNo;
                    //if (SelectedCompetitors.SelectedRow.Cells[2].Text == "Karl")
                    //{
                    //    AthlCompetitorInComp.kyn = 1;
                    //}
                    //else
                    //{
                    //    AthlCompetitorInComp.kyn = 2;
                    //}
                    //AthlCompetitorInComp.kennitala = SelectedCompetitors.SelectedRow.Cells[3].Text;
                    //string Name = SelectedCompetitors.SelectedRow.Cells[4].Text;
                    //Name = HttpUtility.HtmlDecode(Name);
                    //AthlCompetitorInComp.nafn = Name;
                    //AthlCompetitorInComp.faedingarar = Convert.ToInt16(SelectedCompetitors.SelectedRow.Cells[5].Text);
                    //AthlCompetitorInComp.aldurkeppanda = Convert.ToInt32(SelectedCompetitors.SelectedRow.Cells[6].Text);
                    //string Club = SelectedCompetitors.SelectedRow.Cells[7].Text;
                    //Club = HttpUtility.HtmlDecode(Club);
                    //AthlCompetitorInComp.felag = Club;
                    ////string NoOfPerf = SelectCompetitorGridView.SelectedRow.Cells[8].Text;
                    //AthlCompetitorInComp.faedingardagur = Convert.ToDateTime(SelectedCompetitors.SelectedRow.Cells[9].Text);
                    //string Land = SelectedCompetitors.SelectedRow.Cells[10].Text;
                    //Land = HttpUtility.HtmlDecode(Land);
                    //AthlCompetitorInComp.land = Land;

                    AthlCompCRUD.InsertCompetitorInCompetition(AthlCompetitorInComp);
                    AthlEnt.InsertToLogFile(CurrentUserName.Text, "Skráning í mót", CompCode.Text, AthlCompetitorInComp.keppendanumer, "");

                    //       AthlCompEvents = AthlCompCRUD.InitCompetitionEvent();
                    //       AthlCompEvents.mot = gl.GetCompetitionCode();
                }
            }
            SelectedCompetitors.DataBind();

            //Response.Redirect("NyttMot.aspx?Comp=" + gl.GetCompetitionCode());
        }

        protected void ShowCompetitors_Click(object sender, EventArgs e)
        {
            //SelectClub.DataBind();

              //<asp:ControlParameter ControlID="SelectedGender" Name="GenderFilter" />
              //        <asp:ControlParameter ControlID="SelectedPerFormanceYears" Name="NoOfYearsToSelect" />
              //        <asp:ControlParameter ControlID="SelectedAgeFrom" Name="AgeFrom" />
              //        <asp:ControlParameter ControlID="SelectedAgeTo" Name="AgeTo" />
              //        <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode"/>
              //        <asp:ControlParameter ControlID="SelectedClub" Name="SelectedClub" PropertyName="Text" Type="String" />
              //    </SelectParameters>

            ////SelectedClub.Text = "USER:" + Session["CurrentUserName"].ToString();

            SelectedGender.Text = SelectGender02.SelectedValue;
            SelectedPerformanceYears.Text = PerformanceYears02.SelectedValue;
            SelectedAgeFrom.Text = AgeFrom02.Text;
            SelectedAgeTo.Text = AgeTo02.Text;


            //          DebugTxt.Text = "G: " + SelectedGender.Text + " - " +
            //          "Yrs: " + SelectedPerformanceYears.Text + " - " +
            //          "AgeF : " + SelectedAgeFrom.Text + " - " + 
            //          "AgeT : " + SelectedAgeTo.Text + " - " + 
            //          "Comp : " + CompCode.Text + " - " + 
            //          "SelecCl : " + SelectedClub.Text;

            SelectedCompetitors.DataBind();
        }

        protected void ClubsCompetitors_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClubCompetitorsWithReg.aspx");
        }

    }
}