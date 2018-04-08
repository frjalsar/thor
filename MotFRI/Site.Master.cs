using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
            // Þetta var í SiteMaster skránni
            //
            //    <div class="loginDisplay">
            //    <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
            //        <AnonymousTemplate>
            //            [ <a href="~/Account/Login.aspx" ID="HeadLoginStatus" runat="server">Log In</a> ]
            //        </AnonymousTemplate>
            //        <LoggedInTemplate>
            //            Welcome <span class="bold"><asp:LoginName ID="HeadLoginName" runat="server" /></span>!
            //            [ <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out" LogoutPageUrl="~/"/> ]
            //        </LoggedInTemplate>
            //    </asp:LoginView>
            //</div>


    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CurrentUserName"] == null)
            {
                Session["CurrentUserName"] = "";
                Session["ComingFromPage"] = "";
                Session["CurrentAccessLevel"] = "0";
                Session["AccessLevelText"] = "";

            }
            //if (!IsPostBack)
            //{
                Global gl = new Global();
                if (Session["CurrentUserName"].ToString() == "")
                {
                    LoginHyperlink.Text = "Innskráning";
                }
                else
                {
                    LoginHyperlink.Text = "Innskráður notandi: " + Session["CurrentUserName"].ToString(); 
                }
                
          //  }
                        
        }
    }
}
