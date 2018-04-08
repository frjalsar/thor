using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MotFRI
{
    public partial class ApplyForUserID : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                RequestedUserID.Text = "";
                Name.Text = "";
                EmailAddress.Text = "";
                EmailAddressAgain.Text = "";
                TelephoneNo.Text = "";
                Club_1.Text = "";
                Club_2.Text = "";
                Club_3.Text = "";
                SendApplication.Visible = true;
            }
            else
            {
                RequestedUserID.Text = RequestedUserID.Text.ToUpper();
            }

        }

        protected void SendApplication_Click(object sender, EventArgs e)
        {
            Global gl = new Global();
            bool EmailAdrIsOk;
            string EmailTxt = "";
            string EmailValidationText = "";

            if (RequestedUserID.Text == "")
            {
                ErrorMsg.Text = "Þú verður að fylla út umbeðna notendakennið";
                return;
            }
            if (Name.Text == "")
            {
                ErrorMsg.Text = "Þú verður að fylla út Nafnið";
                return;
            }
            if ((EmailAddress.Text == "") || (EmailAddress.Text != EmailAddressAgain.Text))
            {
                ErrorMsg.Text = "Þú verður að fylla út tölvupóstfangið þitt tvisvar sinnum. Það verður að vera alveg eins í bæði skiptin";
                return;
            }
            EmailTxt = EmailAddress.Text;
            EmailAdrIsOk = gl.ValidEmailAddress(EmailTxt);

            if (EmailAdrIsOk == false)
            {
                ErrorMsg.Text = "Tölvupóstfangið verður að vera rétt byggt upp. Dæmi: 'jon@vinna.is'";
                return;
            }
            if (Club_1.Text == "")
            {
                ErrorMsg.Text = "Þú verður að fylla út a.m.k. eitt félag sem þú tengist";
                return;
            }

            RequestedUserID.Text = RequestedUserID.Text.ToUpper();

            ObjectParameter MsgOut;
            MsgOut = new ObjectParameter("MessageOut", typeof(global::System.String));

            AthleticsEntities1 AthlEnt = new AthleticsEntities1();            

            AthlEnt.SaveApplicationForAccess(RequestedUserID.Text, Name.Text, EmailAddress.Text, EmailAddressAgain.Text, TelephoneNo.Text, 
                Club_1.Text, Club_2.Text, Club_3.Text, MsgOut);

            ErrorMsg.Text = MsgOut.Value.ToString();
            
            if (ErrorMsg.Text == "")
            {
                RequestedUserID.ReadOnly = true;
                Name.ReadOnly = true;
                EmailAddress.ReadOnly = true;
                EmailAddressAgain.ReadOnly = true;
                TelephoneNo.ReadOnly = true;
                Club_1.ReadOnly = true;
                Club_2.ReadOnly = true;
                Club_3.ReadOnly = true;

                ErrorMsg.Text = "Staðfesting og lykilorð verður sent á tölvupóstfang " + EmailAddress.Text + " fljótlega.";
                SendApplication.Visible = false;
                Response.Write("<script language=javascript>alert('Staðfesting og lykilorð verður sent bráðlega.')</script>");              
            }  
        }
    }
}