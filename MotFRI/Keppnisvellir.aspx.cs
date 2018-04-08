using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class Keppnisvellir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeppnisvellirText.Text = "Valinn völlur " + KeppnisvellirGridView.SelectedValue.ToString();
              
        }
        
        protected void NyrVollur_Click(object sender, EventArgs e)
        {
            KeppnisvellirText.Text = "Hér þarf að skrá nýjan völl";
        }

        

        
    }
}