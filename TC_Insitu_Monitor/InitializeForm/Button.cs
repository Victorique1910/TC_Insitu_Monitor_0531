using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor
{
    public partial class TC_Insitu_Monitor
    {
        private void InitializeFlowLayoutPanel_IndexMain()
        {
            Button_Setting.BackColor = Color.LightGreen;
            Button_StartReStart.BackColor = Color.LightGreen;
            Button_Pause.BackColor = Color.LightGreen;
            Button_Stop.BackColor = Color.LightGreen;
            Button_Precheck.BackColor = Color.LightGreen;
            Button_Pretest.BackColor = Color.LightGreen;
            Button_9points.BackColor = Color.LightGreen;
            Button_ReportAnalysis.BackColor = Color.LightGreen;
            Button_Screen.BackColor = Color.LightGreen;
            Button_Cal.BackColor = Color.LightGreen;
            Button_Loss.BackColor = Color.LightGreen;
        }

        private void InitializeFlowLayoutPanel_FillMain()
        {
            Button_TCCondition.BackColor = Color.PeachPuff;
            Button_FailCriteria.BackColor = Color.PeachPuff;
            Button_SampleRate.BackColor = Color.PeachPuff;
            Button_Chamber.BackColor = Color.PeachPuff;
            Button_StopCriteria.BackColor = Color.PeachPuff;
            Button_CorrespondentTemperature.BackColor = Color.PeachPuff;
        }

        private void InitializeFlowLayoutPanel_Automatic()
        {
            Button_InitialSetting.BackColor = Color.PeachPuff;
            Button_FormalContiuneTest.BackColor = Color.PeachPuff;
        }

        private void InitializeFlowLayoutPanel_ScreenMain()
        {
            Button_Main.BackColor = Color.Violet;
            Button_Trend.BackColor = Color.Violet;
        }
    }
}
