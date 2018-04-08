using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class EventProgressLiveUpdate : System.Web.UI.Page
    {
        public string EventName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Global gl = new Global();
            
            EventName = Request.QueryString.Get("EventName");
            EventName = HttpUtility.HtmlDecode(EventName);
            //CompAndEventName.Text = gl.GetCompetitionName() + " - " + EventName;
            CompAndEventName.Text = EventName;
            CompCode.Text = gl.GetCompetitionCode();
            if (CompCode.Text == "")
            {
                CompCode.Text = Request.QueryString.Get("Code");
            }
            //EventLineNo.Text = gl.GetCompetitionEventNo();
            EventLineNo.Text = Request.QueryString.Get("EventLineNo");

            if (!IsPostBack)
            {
                Int32 EventLin;
                EventLin = Convert.ToInt32(EventLineNo.Text);
                Int32 NoOfHeats;
                AthleticsEntities1 AthlEntities = new AthleticsEntities1();
                System.Data.Objects.ObjectParameter OutNoOfHeats = new System.Data.Objects.ObjectParameter("OutNoOfHeats", "0");
                AthlEntities.ReturnNoOfHeatsInEvent(CompCode.Text, EventLin, OutNoOfHeats);
                NoOfHeats = Convert.ToInt32(OutNoOfHeats.Value);
                if (NoOfHeats < 2)
                {
                    FilterOnHeatLabel.Visible = false;
                    FilterOnSelectedHeadDropDownList.Visible = false;
                    //HeatNumberFilter = "%";
                    //CurrPageAndHeat.Text = "0";
                }
                else
                {
                    //NOT READY!
                    //for (int ix = 1; ix <= NoOfHeats; ix++)
                    //{
                    //    ListItem HeatInfo = new ListItem();
                    //    HeatInfo.Text = "Aðeins riðill " + ix.ToString();
                    //    HeatInfo.Value = ix.ToString();
                    //    FilterOnSelectedHeadDropDownList.Items.Add(HeatInfo);
                    //}
                    //FilterOnHeatLabel.Visible = true;
                    //FilterOnSelectedHeadDropDownList.Visible = true;
                    //FilterOnSelectedHeadDropDownList.SelectedIndex = 1;
                    FilterOnHeatLabel.Visible = false;
                    FilterOnSelectedHeadDropDownList.Visible = false;

                    ////HeatNumberFilter = "1";
                }
            }


        }

        protected void RefreshButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EventProgressLiveUpdate.aspx?EventLineNo=" + EventLineNo.Text + 
                "&EventName=" + EventName);
            //?EventLineNo=830000&EventName=Hástökk%20pilta%2016-17%20ára
        }

        protected void EventStatusGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Int32 ColNo = 0;
            Int32 ColNoOfName = 0;
            string EvStatusInDataset = "";
            string WrkValue = "";            
            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ColNo = GetColumnIndexByName(e.Row, "StatusOfEvent");
                EvStatusInDataset = e.Row.Cells[ColNo].Text;
                EvStatusInDataset = HttpUtility.HtmlDecode(EvStatusInDataset); 
                if (EvStatusInDataset != EventStatus.Text)
                {
                    EventStatus.Text = EvStatusInDataset;
                }
                e.Row.Cells[ColNo].Text = "";
            }
                ColNo = GetColumnIndexByName(e.Row, "NextInLine");
                WrkValue = e.Row.Cells[ColNo].Text;
                if (WrkValue == "1")
                {
                    ColNoOfName = GetColumnIndexByName(e.Row, "Name");
                    e.Row.Cells[ColNoOfName].BackColor = System.Drawing.Color.Red;
                }
                e.Row.Cells[ColNo].Text = "";
                ColNo = GetColumnIndexByName(e.Row, "Waiting");
                            WrkValue = e.Row.Cells[ColNo].Text;

                            if (WrkValue == "1")
                            {
                                ColNoOfName = GetColumnIndexByName(e.Row, "Name");
                                e.Row.Cells[ColNoOfName].BackColor = System.Drawing.Color.Yellow;

                            }
                e.Row.Cells[ColNo].Text = "";            
                ColNo = GetColumnIndexByName(e.Row, "StatusOfEvent");
                e.Row.Cells[ColNo].Text = "";

        }

        int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }


    }
}