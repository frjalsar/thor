using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class HeightsInHJandPV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global gl = new Global();
                //EventName.Text = gl.GetCompetitonEventName();
                string CompCode = gl.GetCompetitionCode();
                //Int32 EventLineNo = Convert.ToInt32(gl.GetCompetitionEventNo());
                string EventLineNoText = Request.QueryString.Get("Event");
                Int32 EventLineNo = Convert.ToInt32(EventLineNoText);
                AthleticCompetitionCRUD AthlCRUD = new AthleticCompetitionCRUD();
                Athl_HeightsInHJandPV AthlHeighs = new Athl_HeightsInHJandPV();
                Athl_CompetitionEvents AthlCompEv = new Athl_CompetitionEvents();
                AthlCompEv = AthlCRUD.GetCompetitionEvent(CompCode, EventLineNo);
                EventName.Text = AthlCompEv.heitigreinar;
                AthlHeighs = AthlCRUD.GetHeightsInHJandPVRec(CompCode, EventLineNo, -1);
                OpeningHeight.Text = AthlHeighs.OpeningHeight;
                FirstIncreaseBy.Text = AthlHeighs.FirstIncreaseBy;
                FirstLimit.Text = AthlHeighs.FirstLimit;
                SecondIncreaseBy.Text = AthlHeighs.SecondIncreaseBy;
                SecondLimit.Text = AthlHeighs.SecondLimit;
                ThirdIncreaseBy.Text = AthlHeighs.ThirdIncreaseBy;
            }
        }

        protected void CalculateButton_Click(object sender, EventArgs e)
        {
            string[] HeightsArray = new string[26];
            Int32 OpeningHeightInCm = ReturnCentimetres(OpeningHeight.Text);
            Int32 FirstIncreaseByInCm = ReturnCentimetres(FirstIncreaseBy.Text);
            Int32 FirstLimitInCm = ReturnCentimetres(FirstLimit.Text);
            Int32 SecondIncreaseByInCm = ReturnCentimetres(SecondIncreaseBy.Text);
            Int32 SecondLimitInCm = ReturnCentimetres(SecondLimit.Text);
            Int32 ThirdIncreaseByInCm = ReturnCentimetres(ThirdIncreaseBy.Text);
            if (SecondLimitInCm == 0)
            {
                SecondLimitInCm = 9999;
                ThirdIncreaseByInCm = 9999;
            }
            Int32 NextHeight = 0;
            bool FirstLimithHasBeenReached = false;
            bool SecondLimintHasBeenReached = false;
            NextHeight = OpeningHeightInCm;
            
            HeightsArray[1] = OpeningHeightInCm.ToString();
            Height01.Text = OpeningHeightInCm.ToString();

            for (int i = 2; i <= 25; i++)
            {

                if (FirstLimithHasBeenReached == false)
                {
                    NextHeight = NextHeight + FirstIncreaseByInCm;
                }
                else
                {
                    if (SecondLimintHasBeenReached == false)
                    {
                        NextHeight = NextHeight + SecondIncreaseByInCm;
                    }
                    else
                    { 
                        NextHeight = NextHeight + ThirdIncreaseByInCm;
                    }
                }
                HeightsArray[i] = NextHeight.ToString();
                switch (i)
                {
                    case 1: Height01.Text = NextHeight.ToString();
                        break;
                    case 2: Height02.Text = NextHeight.ToString();
                        break;
                    case 3: Height03.Text = NextHeight.ToString();
                        break;
                    case 4: Height04.Text = NextHeight.ToString();
                        break;
                    case 5: Height05.Text = NextHeight.ToString();
                        break;
                    case 6: Height06.Text = NextHeight.ToString();
                        break;
                    case 7: Height07.Text = NextHeight.ToString();
                        break;
                    case 8: Height08.Text = NextHeight.ToString();
                        break;
                    case 9: Height09.Text = NextHeight.ToString();
                        break;
                    case 10: Height10.Text = NextHeight.ToString();
                        break;
                    case 11: Height11.Text = NextHeight.ToString();
                        break;
                    case 12: Height12.Text = NextHeight.ToString();
                        break;
                    case 13: Height13.Text = NextHeight.ToString();
                        break;
                    case 14: Height14.Text = NextHeight.ToString();
                        break;
                    case 15: Height15.Text = NextHeight.ToString();
                        break;
                    case 16: Height16.Text = NextHeight.ToString();
                        break;
                    case 17: Height17.Text = NextHeight.ToString();
                        break;
                    case 18: Height18.Text = NextHeight.ToString();
                        break;
                    case 19: Height19.Text = NextHeight.ToString();
                        break;
                    case 20: Height20.Text = NextHeight.ToString();
                        break;
                    case 21: Height21.Text = NextHeight.ToString();
                        break;
                    case 22: Height22.Text = NextHeight.ToString();
                        break;
                    case 23: Height23.Text = NextHeight.ToString();
                        break;
                    case 24: Height24.Text = NextHeight.ToString();
                        break;
                    case 25: Height25.Text = NextHeight.ToString();
                        break;
                }
                if (FirstLimithHasBeenReached == false)
                {
                    if (NextHeight >= FirstLimitInCm)
                    {
                        FirstLimithHasBeenReached = true;
                    }
                }
                if (SecondLimintHasBeenReached == false)
                {
                    if (NextHeight >= SecondLimitInCm)
                    {
                        SecondLimintHasBeenReached = true;
                    }
                }
            }           

        }

        protected Int32 ReturnCentimetres(string ValueIn)
        {
            if (ValueIn == "")
            {
                return 0;
            }
            ValueIn = ValueIn.Replace(",","").Replace(".","");
            Int32 ValueInCentimeters;
            ValueInCentimeters = Convert.ToInt32(ValueIn);
            return ValueInCentimeters;
        }

        protected void SaveHeights_Click(object sender, EventArgs e)
        {
            Global gl = new Global();
            string CompCode = gl.GetCompetitionCode();
            //Int32 EventLineNo = Convert.ToInt32(gl.GetCompetitionEventNo());
            string EventLineNoText = Request.QueryString.Get("Event");
            Int32 EventLineNo = Convert.ToInt32(EventLineNoText);
            AthleticsEntities1 AthlEnt = new AthleticsEntities1();
            Int32 OpeningHeightInCm = ReturnCentimetres(OpeningHeight.Text);
            Int32 FirstIncreaseByInCm = ReturnCentimetres(FirstIncreaseBy.Text);
            Int32 FirstLimitInCm = ReturnCentimetres(FirstLimit.Text);
            Int32 SecondIncreaseByInCm = ReturnCentimetres(SecondIncreaseBy.Text);
            Int32 SecondLimitInCm = ReturnCentimetres(SecondLimit.Text);
            Int32 ThirdIncreaseByInCm = ReturnCentimetres(ThirdIncreaseBy.Text);

            AthlEnt.UpdateHeightsInHJandPV(CompCode, EventLineNo, -1, -1, "", 0, 0, 0, 
                Height01.Text,
                Height02.Text,
                Height03.Text,
                Height04.Text,
                Height05.Text,
                Height06.Text,
                Height07.Text,
                Height08.Text,
                Height09.Text,
                Height10.Text,
                Height11.Text,
                Height12.Text,
                Height13.Text,
                Height14.Text,
                Height15.Text,
                Height16.Text,
                Height17.Text,
                Height18.Text,
                Height19.Text,
                Height20.Text,
                Height21.Text,
                Height22.Text,
                Height23.Text,
                Height24.Text,
                Height25.Text,
                0,0,0,0,0,
                OpeningHeightInCm.ToString(),
                FirstIncreaseByInCm.ToString(),
                FirstLimitInCm.ToString(),
                SecondIncreaseByInCm.ToString(),
                SecondLimitInCm.ToString(),
                ThirdIncreaseByInCm.ToString());

            Response.Redirect("UpdateEventResults.aspx?Event=" + EventLineNoText);
                
        }
    }
}