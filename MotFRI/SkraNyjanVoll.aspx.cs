using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class SkraNyjanVoll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Click(object sender, EventArgs e)
        {
            MotADO ado = new MotADO();
            string UtiEdaInni = Convert.ToString(UtiInni.SelectedValue);
            ado.InsertVoll(HeitiVallar.Text, Stadur.Text, UtiEdaInni, FjBrauta.Text, FjBeinnaBrauta.Text);
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Keppnisvellir.aspx");
        }
    }
}