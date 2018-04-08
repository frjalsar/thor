using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class TestInnsl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Sjá: http://www.codeproject.com/Articles/37207/Editable-Gridview-with-Textbox-CheckBox-Radio-Butt
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StdTextBox.Text = "TextCh með UpdButt";
        }

        protected void UpdBtn(object sender, EventArgs e)
        {
            string WrkText = "";
            TextBox WrkTextBox = (TextBox)StandardTextGridView.Rows[3].FindControl("TxtFirstName");
            WrkText = WrkTextBox.Text;
            GridViewRow Ro = null;
            Ro = StandardTextGridView.Rows[3];
            WrkText = WrkText + " þarf að fara í record með code = " + Ro.Cells[0].Text;
            StdTextBox.Text = HttpUtility.HtmlDecode(WrkText);



            //StdTextBox.Text = "TextCh: ";
            //TextBox tx1 = (TextBox)StandardTextGridView.Rows[3].FindControl("DescriptionTextBox");
            

            //Ro = StandardTextGridView.Rows[1];
            //WrkText = WrkText + ", " + Ro.Cells[0].Text;
            //WrkText = WrkText + ", " + Ro.Cells[1].Text;
            //WrkText = WrkText + ", " + Ro.Cells[2].Text;
            //WrkText = WrkText + ", " + Ro.Cells[3].Text;

            //Ro = StandardTextGridView.Rows[2];
            //WrkText = WrkText + ", " + Ro.Cells[0].Text;
            //WrkText = WrkText + ", " + Ro.Cells[1].Text;
            //WrkText = WrkText + ", " + Ro.Cells[2].Text;
            //WrkText = WrkText + ", " + Ro.Cells[3].Text;

            //WrkText = WrkText + ", " + Ro.Cells[1].Text;
            //WrkText = WrkText + ", " + Ro.Cells[2].Text;
            //WrkText = WrkText + ", " + Ro.Cells[3].Text;

            //WrkText = WrkText + " TextBox1= " + (TextBox)StandardTextGridView.Rows[1].FindControl("TextBox1");
            

            
            
        }

        protected void StandardTextGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text == "FV")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
                if (e.Row.Cells[0].Text == "FF")
                {
                    e.Row.BackColor = System.Drawing.Color.Crimson;
                    //e.Row.Cells[0].Text = e.Row.Cells[1].Text;
                    //e.Row.Cells[1].Text = e.Row.Cells[1].Text + " og svo kemur viðbótin";
                    e.Row.Cells[1].Text = "Hér kemur viðbótin sem er sett inn á binding tíma";
                    //e.Row.Cells[3].Text = "";
                    e.Row.Cells[1].ColumnSpan = 3;

                }
            }
        }
    }
}