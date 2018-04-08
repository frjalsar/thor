using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class CoachPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Global gl = new Global();
            CompCode.Text = gl.GetCompetitionCode();
            CurrentUser.Text = Session["CurrentUserName"].ToString();
        }

        protected void CoachesPageGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            int ColNoEvent = 0;
            int ColNoLineType = 0;
            int ColNoCallRoom = 0;
            int ColNoEventDate = 0;
            int ColNoEventTime = 0;
            int ColNoBibNo = 0;
            int ColNoCompetitor = 0;
            int ColNoYoB = 0;
            int ColNoClub = 0;
            int ColNoHeatNo = 0;
            int ColNoLaneNo = 0;
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            GridViewRow CurrRow;
            CurrRow = e.Row;
            Global gl = new Global();

            ColNoEvent = gl.GetColumnIndexByName(CurrRow, "EventName");
            ColNoCallRoom = gl.GetColumnIndexByName(CurrRow, "CallRoomTime");
            ColNoEventDate = gl.GetColumnIndexByName(CurrRow, "EventDate");
            ColNoEventTime = gl.GetColumnIndexByName(CurrRow, "EventTime");
            ColNoLineType = gl.GetColumnIndexByName(CurrRow, "LineType");
            ColNoBibNo = gl.GetColumnIndexByName(CurrRow, "BibNo");
            ColNoCompetitor = gl.GetColumnIndexByName(CurrRow, "CompetitorName");
            ColNoYoB = gl.GetColumnIndexByName(CurrRow, "CompetitorYearOfBirth");
            ColNoClub = gl.GetColumnIndexByName(CurrRow, "CompetitorClub");
            ColNoHeatNo = gl.GetColumnIndexByName(CurrRow, "HeatNumber");
            ColNoLaneNo = gl.GetColumnIndexByName(CurrRow, "LaneOrOrder");

            //<asp:BoundField DataField="EventName" HeaderText="Grein" SortExpression="EventName" />
            //<asp:BoundField DataField="CallRoomTime" HeaderText="Nafnakall" SortExpression="CallRoomTime" DataFormatString="{0:t}" />
            //<asp:BoundField DataField="EventDate" HeaderText="Dagsetn." SortExpression="EventDate" DataFormatString="{0:d}" />
            //<asp:BoundField DataField="EventTime" HeaderText="Tími greinar" SortExpression="EventTime" DataFormatString="{0:t}" />
            //<asp:BoundField DataField="BibNo" HeaderText="Rásnr." SortExpression="BibNo" />
            //<asp:BoundField DataField="CompetitorName" HeaderText="Keppandi" SortExpression="CompetitorName" />
            //<asp:BoundField DataField="CompetitorYearOfBirth" HeaderText="F.ár" SortExpression="CompetitorYearOfBirth" />
            //<asp:BoundField DataField="CompetitorClub" HeaderText="Félag" SortExpression="CompetitorClub" />
            //<asp:BoundField DataField="HeatNumber" HeaderText="Riðill" SortExpression="HeatNumber" />
            //<asp:BoundField DataField="LaneOrOrder" HeaderText="Braut/Röð" SortExpression="LaneOrOrder" />
            //<asp:BoundField DataField="LineType" HeaderText="LineType" SortExpression="LineType" />
            //<asp:BoundField DataField="EventLineNo" HeaderText="EventLineNo" SortExpression="EventLineNo" Visible="False" />
            //<asp:BoundField DataField="EventType" HeaderText="EventType" SortExpression="EventType" Visible="False" />
            //<asp:BoundField DataField="CloserEventType" HeaderText="CloserEventType" SortExpression="CloserEventType" Visible="False" />


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //AthlEnt.InsertToLogFile("Vindur í línu ", e.Row.Cells[ColNoWind].Text);

                if (e.Row.Cells[ColNoLineType].Text == "1")  //Event Name and Date
                {
                    e.Row.Cells[ColNoCompetitor].Text = e.Row.Cells[ColNoEvent].Text;
                    e.Row.Cells[ColNoCompetitor].Font.Bold = true;
                    e.Row.Cells[ColNoCompetitor].Font.Italic = true;
                    e.Row.Cells[ColNoCompetitor].Font.Size = 10;
//                    e.Row.Cells[ColNoEvent].ColumnSpan = 6;
                    e.Row.Cells[ColNoBibNo].Text = null;
                    e.Row.Cells[ColNoEvent].Text = null;
                    e.Row.Cells[ColNoYoB].Text = null;
                    e.Row.Cells[ColNoClub].Text = null;
                    e.Row.Cells[ColNoHeatNo].Text = null;
                    e.Row.Cells[ColNoLaneNo].Text = null;
                    e.Row.Cells[ColNoLineType].Text = null;
                }
                else
                {
                    //e.Row.Cells[4].Text = e.Row.Cells[ColNoName].Text;
                    e.Row.Cells[ColNoLineType].Text = null;
                    e.Row.Cells[ColNoEvent].Text = null;
                    e.Row.Cells[ColNoCallRoom].Text = null;
                    e.Row.Cells[ColNoEventDate].Text = null;
                    e.Row.Cells[ColNoEventTime].Text = null;
                 //   e.Row.Cells[ColNoEvent]
                }
            }
            else
            {
//                e.Row.Cells[ColNoEventName].Text = null;
//                e.Row.Cells[ColNoEventDate].Text = null;
                e.Row.Cells[ColNoLineType].Text = null;

            }

        }
        
    }        
    
}