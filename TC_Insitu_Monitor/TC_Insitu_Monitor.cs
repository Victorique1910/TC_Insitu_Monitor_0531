using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TC_Insitu_Monitor.BLL;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor
{
    public partial class TC_Insitu_Monitor : Form
    {
        //COM3: R  COM4: T  
        readonly List<string> comports = new List<string>();
        readonly Counter counter = new Counter();
        readonly Initial initial = new Initial();
        //readonly Mail mail = new Mail();

        public TC_Insitu_Monitor(string[] args)
        {
            InitialLoad(args);
            InitializeComponent();
            InitailUI();            
        }
        #region InitialLoad
        private void InitialLoad(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                foreach (var arg in args)
                {
                    comports.Add(arg);
                }
            }
            else
            {
                comports.Add("COM12");
            }
        }
        #endregion
        #region InitializeComponent
        #region 1.flowLayoutPanel_Setting
        private void Button_Common_Click(object sender, EventArgs e)
        {
            tabControl_Setting.SelectedTab = tabPage_Common;
        }
        private void Button_Impedance_Click(object sender, EventArgs e)
        {
            tabControl_Setting.SelectedTab = tabPage_Impedance;
        }
        private void Button_Temperature_Click(object sender, EventArgs e)
        {
            tabControl_Setting.SelectedTab = tabPage_Temperature;
        }
        private void Button_Email_Click(object sender, EventArgs e)
        {
            tabControl_Setting.SelectedTab = tabPage_Email;
        }
        private void Button_Automatic_Click(object sender, EventArgs e)
        {
            tabControl_Setting.SelectedTab = tabPage_Automatic;
        }
        private void Button_Location_Click(object sender, EventArgs e)
        {
            tabControl_Setting.SelectedTab = tabPage_Location;
        }
        #endregion
        #region 2.flowLayoutPanel_FillMain
        private void Button_TCCondition_Click(object sender, EventArgs e)
        {
            tabControl_Common.SelectedTab = tabPage_TCCondition;
            InitializeFlowLayoutPanel_FillMain();
            Button_TCCondition.BackColor = Color.DarkOrange;
        }
        private void Button_FailCriteria_Click(object sender, EventArgs e)
        {
            tabControl_Common.SelectedTab = tabPage_FailCriteria;
            InitializeFlowLayoutPanel_FillMain();            
            Button_FailCriteria.BackColor = Color.DarkOrange;            
        }
        private void Button_SampleRate_Click(object sender, EventArgs e)
        {
            tabControl_Common.SelectedTab = tabPage_SampleRate;
            InitializeFlowLayoutPanel_FillMain();           
            Button_SampleRate.BackColor = Color.DarkOrange;           
        }
        private void Button_Chamber_Click(object sender, EventArgs e)
        {
            tabControl_Common.SelectedTab = tabPage_Chamber;
            InitializeFlowLayoutPanel_FillMain();           
            Button_Chamber.BackColor = Color.DarkOrange;            
        }
        private void Button_StopCriteria_Click(object sender, EventArgs e)
        {
            tabControl_Common.SelectedTab = tabPage_StopCriteria;
            InitializeFlowLayoutPanel_FillMain();           
            Button_StopCriteria.BackColor = Color.DarkOrange;            
        }
        private void Button_CorrespondentTemperature_Click(object sender, EventArgs e)
        {
            tabControl_Common.SelectedTab = tabPage_CorrespondentTemperature;
            InitializeFlowLayoutPanel_FillMain();            
            Button_CorrespondentTemperature.BackColor = Color.DarkOrange;
        }
        #endregion
        #region 3.flowLayoutPanel_Automatic
        private void Button_InitialSetting_Click(object sender, EventArgs e)
        {
            tabControl_Automatic.SelectedTab = tabPage_InitialSetting;
            InitializeFlowLayoutPanel_Automatic();
            Button_InitialSetting.BackColor = Color.DarkOrange;            
        }
        private void Button_FormalContiuneTest_Click(object sender, EventArgs e)
        {
            tabControl_Automatic.SelectedTab = tabPage_FormalContinueTest;
            InitializeFlowLayoutPanel_Automatic();
            Button_FormalContiuneTest.BackColor = Color.DarkOrange;
        }
        #endregion
        #region flowLayoutPanel_ScreenMain
        private void Button_Main_Click(object sender, EventArgs e)
        {
            tabControl_Screen.SelectedTab = tabPage_Main;
            InitializeFlowLayoutPanel_ScreenMain();
            Button_Main.BackColor = Color.Purple;           
        }
        private void Button_Trend_Click(object sender, EventArgs e)
        {
            tabControl_Screen.SelectedTab = tabPage_Trend;
            InitializeFlowLayoutPanel_ScreenMain();           
            Button_Trend.BackColor = Color.Purple;
        }
        #endregion
        #region flowLayoutPanel_MainRight
        private void Button_TemperatureOK_Click(object sender, EventArgs e)
        {
            tabControl_Temperature.SelectedTab = tabPage_TemperatureFornt;
            string port = ComboBox_TemperatureChannel.SelectedItem.ToString();
            DataTable dataTable = initial.FormalTestDataTable;
            if(dataTable != null)
            {
                for (int rowCount = 0; rowCount < dataTable.Rows.Count; rowCount++)
                {
                    if(dataTable.Rows[rowCount]["Port"].ToString() == port)
                    {
                        dataTable.Rows[rowCount]["DUTName"] = textBox_TemperatureName.Text;
                        dataTable.Rows[rowCount]["TemperatureType"] = ComboBox_TemperatureType.SelectedItem.ToString();
                    }                    
                }
            }
            initial.SaveFormalTest(dataTable);
            DataGridView_TemperatureFornt.DataSource = initial.FormalTestDataTable;
        }
        private void Button_TemperatureClear_Click(object sender, EventArgs e)
        {
            tabControl_Temperature.SelectedTab = tabPage_TemperatureFornt;
            string port = ComboBox_TemperatureChannel.SelectedItem.ToString();
            ComboBox_TemperatureType.SelectedIndex = ComboBox_TemperatureType.Items.Count-1;
            textBox_TemperatureName.Text = "";

            DataTable dataTable = initial.FormalTestDataTable;
            if(dataTable != null)
            {
                for (int rowCount = 0; rowCount < dataTable.Rows.Count; rowCount++)
                {
                    if(dataTable.Rows[rowCount]["Port"].ToString() == port)
                    {
                        dataTable.Rows[rowCount]["DUTName"] = textBox_TemperatureName.Text;
                        dataTable.Rows[rowCount]["TemperatureType"] = ComboBox_TemperatureType.SelectedItem.ToString();
                    }                    
                }
            }
            initial.SaveFormalTest(dataTable);
            DataGridView_TemperatureFornt.DataSource = initial.FormalTestDataTable;
        }
        private void Button_TemperatureForntAdd_Click(object sender, EventArgs e)
        {
            tabControl_Temperature.SelectedTab = tabPage_TemperatureBack;
        }
        private void Button_TemperatureForntEdit_Click(object sender, EventArgs e)
        {
            tabControl_Temperature.SelectedTab = tabPage_TemperatureBack;
        }
        #endregion
        #region flowLayoutPanel_ImpedanceForntRight
        private void Button_ImpedanceOK_Click(object sender, EventArgs e)
        {
            tabControl_Impedance.SelectedTab = tabPage_ImpedanceFornt;
        }
        private void Button_ImpedanceForntAdd_Click(object sender, EventArgs e)
        {
            tabControl_Impedance.SelectedTab = tabPage_ImpedanceBack;
        }
        private void Button_ImpedanceForntEdit_Click(object sender, EventArgs e)
        {
            tabControl_Impedance.SelectedTab = tabPage_ImpedanceBack;
        }
        #endregion
        private void Button_PrecheckGraphBack_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Setting;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Setting.BackColor = Color.ForestGreen;
        }
        private void Button_PretestChartBack_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Setting;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Setting.BackColor = Color.ForestGreen;
        }
        private void Button_9pointsChartBack_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Setting;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Setting.BackColor = Color.ForestGreen;
        }
        private void Button_LossChartBack_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Setting;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Setting.BackColor = Color.ForestGreen;
        }
        private void Button_CalChartBack_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Setting;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Setting.BackColor = Color.ForestGreen;
        }
        private void Button_Exit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        private void CheckBox_BySettingCriteria_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void CheckBox_ByDPAT_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void ComboBox_FailCondition_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox_FailCondition2.SelectedItem = ComboBox_FailCondition.SelectedItem;
            if (ComboBox_FailCondition.SelectedItem.ToString() == "Absolute")
            {
                textBox_Precentage.Hide();
                textBox_Precentage2.Hide();
            }
            else if (ComboBox_FailCondition.SelectedItem.ToString() == "Relative")
            {
                textBox_Precentage.Show();
                textBox_Precentage2.Show();
            }
        }
        private void ComboBox_FailCondition2_SelectedValueChanged(object sender, EventArgs e)
        {            
            if(ComboBox_FailCondition2.SelectedItem.ToString() == "Absolute")
            {               
                textBox_Precentage2.Hide();
            }
            else if(ComboBox_FailCondition2.SelectedItem.ToString() == "Relative")
            {               
                textBox_Precentage2.Show();
            }
        }
        private void ComboBox_Method_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_Method2.SelectedItem = ComboBox_Method.SelectedItem;
            if (ComboBox_Method.SelectedItem.ToString() == "Between")
            {
                textBox_ImpedanceDelta.Hide();
                textBox_ImpedanceDelta2.Hide();
                textBox_ImpedanceDeltaHighLimit.Show();
                textBox_ImpedanceDeltaHighLimit2.Show();
                label_Between.Show();
                label_Between2.Show();
                textBox_ImpedanceDeltaLowLimit.Show();
                textBox_ImpedanceDeltaLowLimit2.Show();
            }
            else if (ComboBox_Method.SelectedItem.ToString() != "Between")
            {
                textBox_ImpedanceDelta.Show();
                textBox_ImpedanceDelta2.Show();
                textBox_ImpedanceDeltaHighLimit.Hide();
                textBox_ImpedanceDeltaHighLimit2.Hide();
                label_Between.Hide();
                label_Between2.Hide();
                textBox_ImpedanceDeltaLowLimit.Hide();
                textBox_ImpedanceDeltaLowLimit2.Hide();
            }
        }
        private void CheckBox_BySettingCriteria2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void CheckBox_ByDPAT2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void ComboBox_Method2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Method2.SelectedItem.ToString() == "Between")
            {
                textBox_ImpedanceDelta2.Hide();
                textBox_ImpedanceDeltaHighLimit2.Show();
                label_Between2.Show();
                textBox_ImpedanceDeltaLowLimit2.Show();
            }
            else if (ComboBox_Method2.SelectedItem.ToString() != "Between")
            {
                textBox_ImpedanceDelta2.Show();
                textBox_ImpedanceDeltaHighLimit2.Hide();
                label_Between2.Hide();
                textBox_ImpedanceDeltaLowLimit2.Hide();
            }
        }
        #endregion
        #region InitailUI
        private void InitailUI()
        {
            DelegeteSetting();                
            InitialdataGridView_ImpedanceFornt();
            InitialdataGridView_TemperatureFornt();
            InitialDataGridView_Main();
            InitialDataGridView_Cal();

            InitailTCCondition();
            InitailSampleRate();
            InitialChamber();
            InitialStopCriteria();
            InitialCorrespondentTemperature();
            InitialEmail();
            InitialFailCritiria();
            InitialTemperature();
            InitialImpedance();
            InitailCalChart();
        }
       
        private void InitialdataGridView_ImpedanceFornt()
        {
            DataGridView_ImpedanceFornt.DataSource = initial.FormalTestDataTable;
            InitializedataGridView(DataGridView_ImpedanceFornt);
            DataGridView_ImpedanceFornt.Columns["ID"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["DUTName"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["Chain"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["ImpedanceChannel"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["Location"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["TemperatureName"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["InitialImpLowTemp"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["InitialImpHighTemp"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["LowLimitLowTemp"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["LowLimitHighTemp"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["HighLimitLowTemp"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["HighLimitHighTemp"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["InitialImpedance"].Visible = true;           
            DataGridView_ImpedanceFornt.Columns["ImpedanceDelta"].Visible = true;
            DataGridView_ImpedanceFornt.Columns["CurrentImpedance"].Visible = true;           
            DataGridView_ImpedanceFornt.Columns["FailCriteria"].Visible = true;
        }
        private void InitialdataGridView_TemperatureFornt()
        {
            DataGridView_TemperatureFornt.DataSource = initial.FormalTestDataTable;
            InitializedataGridView(DataGridView_TemperatureFornt);
            DataGridView_TemperatureFornt.Columns["DUTName"].Visible = true;
            DataGridView_TemperatureFornt.Columns["ImpedanceChannel"].Visible = true;
            DataGridView_TemperatureFornt.Columns["TemperatureType"].Visible = true;      
        }
        private void InitialDataGridView_Main()
        {
            DataGridView_Main.DataSource = initial.FormalTestDataTable;
            InitializedataGridView(DataGridView_Main);
            DataGridView_Main.Columns["ID"].Visible = true;
            DataGridView_Main.Columns["CompensationADCH"].Visible = true;
            DataGridView_Main.Columns["CompensationADCL"].Visible = true;            
            DataGridView_Main.Columns["Chain"].Visible = true;
            DataGridView_Main.Columns["ImpedanceChannel"].Visible = true;          
            DataGridView_Main.Columns["CurrentImpedance"].Visible = true;
            DataGridView_Main.Columns["ImpedanceDelta"].Visible = true;
            DataGridView_Main.Columns["FailCriteria"].Visible = true;
            DataGridView_Main.Columns["CurrentTemperature"].Visible = true;
            DataGridView_Main.Columns["VoltageH"].Visible = true;
            DataGridView_Main.Columns["LayerH"].Visible = true;
            DataGridView_Main.Columns["BeforeLayer"].Visible = true;
            DataGridView_Main.Columns["TemperatureCycle"].Visible = true;
            DataGridView_Main.Columns["Status"].Visible = true;
        }
        private void InitialDataGridView_Cal()
        {
            DataGridView_CalChart.DataSource = initial.FormalTestDataTable;
            InitializedataGridView(DataGridView_CalChart);
            DataGridView_CalChart.Columns["ID"].Visible = true;
            DataGridView_CalChart.Columns["CompensationADCH"].Visible = true;
            DataGridView_CalChart.Columns["CompensationADCL"].Visible = true;
            DataGridView_CalChart.Columns["ImpedanceChannel"].Visible = true;
            DataGridView_CalChart.Columns["CurrentImpedance"].Visible = true;
            DataGridView_CalChart.Columns["CurrentTemperature"].Visible = true;
            DataGridView_CalChart.Columns["VoltageH"].Visible = true;
            DataGridView_CalChart.Columns["LayerH"].Visible = true;
            DataGridView_CalChart.Columns["BeforeLayer"].Visible = true;
        }
        private void InitailTCCondition()
        {
            textBox_CalculateCycle.Text = initial.ConditionTC.CalculateCycle.ToString();
            textBox_OffsetCycle.Text = initial.ConditionTC.OffsetCycle.ToString();
            textBox_DwellHighTime.Text = initial.ConditionTC.HighTempThreshold.ToString();
            textBox_DwellLowTime.Text = initial.ConditionTC.LowTempThreshold.ToString();
            textBox_RampLowDown.Text = initial.ConditionTC.RampDown.ToString();
            textBox_RampHighUp.Text = initial.ConditionTC.RampUp.ToString();
            textBox_HighTempThreshold.Text = initial.ConditionTC.HighTempThreshold.ToString();
            textBox_LowTempThreshold.Text = initial.ConditionTC.LowTempThreshold.ToString();
            textBox_TestCycles.Text = initial.ConditionTC.TestCycle.ToString();
        }
        private void InitailSampleRate()
        {
            textBox_FailSampleRate.Text = initial.SampleRate.FailSampleRate.ToString();
            textBox_InitialSampleRate.Text = initial.SampleRate.InitialSampleRate.ToString();
            textBox_SaveLogafterFail.Text = initial.SampleRate.SaveLogAfterFail.ToString();
            textBox_SaveLogbeforeFail.Text = initial.SampleRate.SaveLogBeforeFail.ToString();
        }
        private void InitialChamber()
        {
            textBox_ChamberID.Text = initial.Chamber.ChamberID;
        }
        private void InitialStopCriteria()
        {
            textBox_ByFailDUTQty.Text = initial.StopCriteria.FailDUTQty.ToString();
            CheckBox_ByFailDUTQty.Checked = initial.StopCriteria.IsCriteria;
            CheckBox_ByCycles.Checked = initial.StopCriteria.IsCycles;
        }
        private void InitialCorrespondentTemperature()
        {
            foreach (var configs in initial.ConfigsDictionary)
            {
                foreach (var config in configs.Value)
                {
                    ComboBox_CorrespondentTemperatureUniqueChannel.Items.Add(config.Channel);
                }
            }
            ComboBox_CorrespondentTemperatureUniqueChannel.SelectedItem = initial.CorrespondentTemperature.Channel;
        }
        private void InitialEmail()
        {
            textBox_NotifyCondition.Text = initial.EmailConfigStruct.NotifyCondition;
            textBox_SMTPServer.Text = initial.EmailConfigStruct.SMTPServer;
            textBox_EmailID.Text = initial.EmailConfigStruct.ID;
            textBox_EmailPassword.Text = initial.EmailConfigStruct.Password;
            textBox_Sender.Text = initial.EmailConfigStruct.Sender;
            CheckBox_UsingTLS.Checked = initial.EmailConfigStruct.UsingTLS;
            foreach (var mail in initial.EmailConfigStruct.MailList)
            {
                CheckedListBox_MailList.Items.Add(mail);
            }
        }
        private void InitialFailCritiria()
        {
            CheckBox_BySettingCriteria.Checked = initial.FailCriteria.IsCriteria;
            CheckBox_BySettingCriteria2.Checked = initial.FailCriteria.IsCriteria;
            ComboBox_FailCondition.SelectedItem = initial.FailCriteria.FailCondition;
            ComboBox_FailCondition2.SelectedItem = initial.FailCriteria.FailCondition;
            ComboBox_Method.SelectedItem = initial.FailCriteria.Method;
            ComboBox_Method2.SelectedItem = initial.FailCriteria.Method;
            if (ComboBox_Method.SelectedItem.ToString() == "Below")
            {
                textBox_ImpedanceDelta.Text = initial.FailCriteria.ValuesMin.ToString();
                textBox_ImpedanceDelta2.Text = initial.FailCriteria.ValuesMin.ToString();
            }
            else if (ComboBox_Method.SelectedItem.ToString() == "Above")
            {
                textBox_ImpedanceDelta.Text = initial.FailCriteria.ValuesMax.ToString();
                textBox_ImpedanceDelta2.Text = initial.FailCriteria.ValuesMax.ToString();
            }
            textBox_Precentage.Text = initial.FailCriteria.Precentage.ToString();
            textBox_Precentage2.Text = initial.FailCriteria.Precentage.ToString();
            textBox_ImpedanceDeltaHighLimit.Text = initial.FailCriteria.ValuesMax.ToString();
            textBox_ImpedanceDeltaHighLimit2.Text = initial.FailCriteria.ValuesMax.ToString();
            textBox_ImpedanceDeltaLowLimit.Text = initial.FailCriteria.ValuesMin.ToString();
            textBox_ImpedanceDeltaLowLimit2.Text = initial.FailCriteria.ValuesMin.ToString();
            ComboBox_Base.SelectedItem = initial.FailCriteria.Base;
            CheckBox_ByDPAT.Checked = initial.FailCriteria.IsDPAT;
            CheckBox_ByDPAT2.Checked = initial.FailCriteria.IsDPAT;
            textBox_DPATContinuousCount.Text = initial.FailCriteria.ContinuousCount.ToString();
            textBox_DPATContinuousCount2.Text = initial.FailCriteria.ContinuousCount.ToString();
            textBox_SettingCriteriaContinuousCount.Text = initial.FailCriteria.ContinuousCount.ToString();
            textBox_SettingCriteriaContinuousCount2.Text = initial.FailCriteria.ContinuousCount.ToString();
        }
        private void InitialTemperature()
        {
            foreach(var temp in initial.PortDictionary)
            {
                foreach(var value in temp.Value)
                {
                    if(value.Type =="Temperature")
                    {
                        ComboBox_TemperatureChannel.Items.Add(temp.Key);
                    }
                }               
            }           
        }
        private void InitialImpedance()
        {
            foreach (var temp in initial.PortDictionary)
            {
                foreach (var value in temp.Value)
                {
                    if (value.Type == "Impedance")
                    {
                        ComboBox_ImpedanceChannel.Items.Add(temp.Key);
                    }
                }
            }
        }
        private void ComboBox_TemperatureChannel_SelectedValueChanged(object sender, EventArgs e)
        {
            string port = ComboBox_TemperatureChannel.SelectedItem.ToString();
            foreach (var temp in initial.PortDictionary)
            {
                foreach (var value in temp.Value)
                {
                    if (value.Port.ToString() == port)
                    {
                        textBox_TemperatureName.Text = value.DUTName;
                        ComboBox_TemperatureType.SelectedItem = value.TemperatureType;
                    }
                }
            }
        }        
        private void InitailCalChart()
        {
            TextBox_CalChartImpedance.Text = initial.CalConfigsStruct.IdelImpedance.ToString();
            TextBox_CalChartTemperature.Text = initial.CalConfigsStruct.IdelTemperature.ToString();
        }
        private void Button_TCConditionOK_Click(object sender, EventArgs e)
        {
            ConditionTC conditionTC = new ConditionTC()
            {
                CalculateCycle = int.Parse(textBox_CalculateCycle.Text),
                OffsetCycle = int.Parse(textBox_OffsetCycle.Text),
                DwellTimeH = double.Parse(textBox_DwellHighTime.Text),
                DwellTimeL = double.Parse(textBox_DwellLowTime.Text),
                RampDown = double.Parse(textBox_RampLowDown.Text),
                RampUp = double.Parse(textBox_RampHighUp.Text),
                HighTempThreshold = double.Parse(textBox_HighTempThreshold.Text),
                LowTempThreshold = double.Parse(textBox_LowTempThreshold.Text),
                TestCycle = int.Parse(textBox_TestCycles.Text)
            };
            initial.Save(conditionTC);
        }
        private void Button_SampleRateOK_Click(object sender, EventArgs e)
        {
            SampleRate sampleRate = new SampleRate()
            {
                FailSampleRate = int.Parse(textBox_FailSampleRate.Text),
                InitialSampleRate = int.Parse(textBox_InitialSampleRate.Text),
                SaveLogAfterFail = int.Parse(textBox_SaveLogafterFail.Text),
                SaveLogBeforeFail = int.Parse(textBox_SaveLogbeforeFail.Text)
            };
            initial.Save(sampleRate);
        }
        private void Button_ChamberOK_Click(object sender, EventArgs e)
        {
            Chamber chamber = new Chamber()
            {
                ChamberID = textBox_ChamberID.Text
            };
            initial.Save(chamber);
        }
        private void Button_StopCriteriaOK_Click(object sender, EventArgs e)
        {
            StopCriteria stopCriteria = new StopCriteria()
            {
                FailDUTQty = int.Parse(textBox_ByFailDUTQty.Text),
                IsCriteria = CheckBox_ByFailDUTQty.Checked,
                IsCycles = CheckBox_ByCycles.Checked
            };
            initial.Save(stopCriteria);
        }
        private void Button_CorrespondentTemperatureOK_Click(object sender, EventArgs e)
        {
            CorrespondentTemperature correspondentTemperature = new CorrespondentTemperature()
            {
                Channel = ComboBox_CorrespondentTemperatureUniqueChannel.SelectedItem.ToString()
            };
            initial.Save(correspondentTemperature);
        }
        private void TextBox_CalChartImpedance_TextChanged(object sender, EventArgs e)
        {
            if (TextBox_CalChartImpedance.Text != null
            && TextBox_CalChartTemperature.Text != null)
            {
                CalConfigsStruct calConfigsStruct = new CalConfigsStruct()
                {
                    IdelImpedance = Double.Parse(TextBox_CalChartImpedance.Text),
                    IdelTemperature = Double.Parse(TextBox_CalChartTemperature.Text)
                };
                initial.Save(calConfigsStruct);
            }               
        }
        private void TextBox_CalChartTemperature_TextChanged(object sender, EventArgs e)
        {
            if(TextBox_CalChartImpedance.Text != null
            && TextBox_CalChartTemperature.Text != null)
            {
                CalConfigsStruct calConfigsStruct = new CalConfigsStruct()
                {
                    IdelImpedance = Double.Parse(TextBox_CalChartImpedance.Text),
                    IdelTemperature = Double.Parse(TextBox_CalChartTemperature.Text)
                };
                initial.Save(calConfigsStruct);
            }            
        }
        private void Button_EmailOK_Click(object sender, EventArgs e)
        {
            List<string> mailList = new List<string>();
            foreach (var mail in CheckedListBox_MailList.Items)
            {
                mailList.Add(mail.ToString());
            }

            EmailConfigStruct emailConfigStruct = new EmailConfigStruct()
            {
                NotifyCondition = textBox_NotifyCondition.Text,
                SMTPServer = textBox_SMTPServer.Text,
                ID = textBox_EmailID.Text,
                Password = textBox_EmailPassword.Text,
                Sender = textBox_Sender.Text,
                MailList = mailList,
                FileName = Path.Combine(System.Environment.CurrentDirectory, "Log")
            };
            initial.Save(emailConfigStruct);
        }
        private void Button_FailCriteriaOK_Click(object sender, EventArgs e)
        {
            double valuesMax = double.NaN;
            double valuesMin = double.NaN;
            int continuousCount = 0;            
            if (ComboBox_Method.SelectedItem.ToString() == "Between")
            {
                valuesMax = double.Parse(textBox_ImpedanceDeltaHighLimit.Text);
                valuesMin = double.Parse(textBox_ImpedanceDeltaLowLimit.Text);
            }
            else if (ComboBox_Method.SelectedItem.ToString() == "Above")
            {
                valuesMax = double.Parse(textBox_ImpedanceDelta.Text);
            }
            else if (ComboBox_Method.SelectedItem.ToString() == "Below")
            {
                valuesMin = double.Parse(textBox_ImpedanceDelta.Text);
            }
            if (CheckBox_BySettingCriteria.Checked)
            {
                continuousCount = int.Parse(textBox_SettingCriteriaContinuousCount.Text);
            }
            else if (CheckBox_ByDPAT.Checked)
            {
                continuousCount = int.Parse(textBox_DPATContinuousCount.Text);
            }

            FailCriteria failCriteria = new FailCriteria()
            {
                IsCriteria = CheckBox_BySettingCriteria.Checked,
                IsDPAT = CheckBox_ByDPAT.Checked,
                FailCondition = ComboBox_FailCondition.SelectedItem.ToString(),
                Method = ComboBox_Method.SelectedItem.ToString(),
                Precentage = double.Parse(textBox_Precentage.Text),
                ValuesMax = valuesMax,
                ValuesMin = valuesMin,
                Base = ComboBox_Base.SelectedItem.ToString(),
                ContinuousCount = continuousCount
            };
            initial.Save(failCriteria);
            CheckBox_BySettingCriteria2.Checked = CheckBox_BySettingCriteria.Checked;
            CheckBox_ByDPAT2.Checked = CheckBox_ByDPAT.Checked;
            ComboBox_FailCondition2.SelectedItem = ComboBox_FailCondition.SelectedItem;
            ComboBox_Method2.SelectedItem = ComboBox_Method.SelectedItem;
            textBox_Precentage2.Text = textBox_Precentage.Text;
            textBox_ImpedanceDeltaHighLimit2.Text = textBox_ImpedanceDeltaHighLimit.Text;
            textBox_ImpedanceDeltaLowLimit2.Text = textBox_ImpedanceDeltaLowLimit.Text;
            textBox_ImpedanceDelta2.Text = textBox_ImpedanceDelta.Text;
            textBox_SettingCriteriaContinuousCount2.Text = textBox_SettingCriteriaContinuousCount.Text;
        }
        private void Button_EmailAdd_Click(object sender, EventArgs e)
        {
            EmailInputBox emailInputBox = new EmailInputBox();
            DialogResult dr = emailInputBox.ShowDialog();
            if (dr == DialogResult.OK)
            {
                CheckedListBox_MailList.Items.Add(emailInputBox.Msg);
            }
        }
        private void Button_EmailDelete_Click(object sender, EventArgs e)
        {
            foreach (var item in CheckedListBox_MailList.CheckedItems.OfType<string>().ToList())
            {
                CheckedListBox_MailList.Items.Remove(item);
            }
        }
        #endregion
        #region flowLayoutPanel_IndexMain      
        private void DelegeteSetting()
        {
            counter.doFormal.display += DisplayDoFormalData;
            counter.doPreCheck.display += DisplayDoFormalData;
            counter.doPreCheck.displayPreCheck += DisplayDoPrecheckData;
            counter.doPreCheck.TestEnd.processTestEnd += ProcessTestEnd;
            counter.doPreTest.display += DisplayDoFormalData;
            counter.doPreTest.displayPreTest += DisplayDoPreTestData;
            counter.doPreTest.TestEnd.processTestEnd += ProcessTestEnd;
            counter.doNinePoint.display += DisplayDoFormalData;
            counter.doNinePoint.displayNinePoint += DisplayDoNinePointData;
            counter.doNinePoint.TestEnd.processTestEnd += ProcessTestEnd;
            counter.doLossTest.display += DisplayDoFormalData;
            counter.doLossTest.displayLossTest += DisplayDoLossTestData;
            counter.doLossTest.TestEnd.processTestEnd += ProcessTestEnd;
            counter.doCalTest.display += DisplayDoFormalData;
            counter.doCalTest.displayCalTest += DisplayDoCalTestData;
            counter.doCalTest.TestEnd.processTestEnd += ProcessTestEnd;            
        }
        //************************************************************************Setting
        private void Button_Setting_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Setting;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Setting.BackColor = Color.ForestGreen;            
        }
        //************************************************************************Formal
        
        private void Button_StartReStart_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Screen;
            InitializeFlowLayoutPanel_IndexMain();
            Button_StartReStart.BackColor = Color.ForestGreen;

            counter.Start(initial, ConterEnum.Formal, comports);
            //mail.Start(initial, ConterEnum.Formal);
            
            CheckedListBox_Main_Change();
        }
        private void Button_Pause_Click(object sender, EventArgs e)
        {
            InitializeFlowLayoutPanel_IndexMain();
            Button_Pause.BackColor = Color.ForestGreen;
            
            counter.Stop();
            //mail.Stop();
        }
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            InitializeFlowLayoutPanel_IndexMain();
            Button_Stop.BackColor = Color.ForestGreen;
           
            counter.Stop();
            //mail.Stop();
        }
        //************************************************************************Precheck        
        private void Button_Precheck_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Precheck;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Precheck.BackColor = Color.ForestGreen;

            counter.Start(initial, ConterEnum.PreCheck, comports);
        }
        //************************************************************************Pre-Test        
        private void Button_Pretest_Click(object sender, EventArgs e)
        {           
            tabControl_Fill.SelectedTab = tabPage_Pretest;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Pretest.BackColor = Color.ForestGreen;            

            counter.Start(initial, ConterEnum.PreTest, comports);                    
        }
        //************************************************************************9-points       
        private void Button_9points_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_9points;
            InitializeFlowLayoutPanel_IndexMain();
            Button_9points.BackColor = Color.ForestGreen;

            counter.Start(initial, ConterEnum.NinePoint, comports);            
        }
        //************************************************************************Loss       
        private void Button_Loss_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Loss;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Loss.BackColor = Color.ForestGreen;

            counter.Start(initial, ConterEnum.Loss, comports);            
        }
        //************************************************************************Report       
        private void Button_ReportAnalysis_Click(object sender, EventArgs e)
        {
            tabControl_Index.SelectedTab = tabPage_Report;
            InitializeFlowLayoutPanel_IndexMain();
            Button_ReportAnalysis.BackColor = Color.ForestGreen;
        }
        //************************************************************************Screen      
        private void Button_Screen_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Screen;
            InitializeFlowLayoutPanel_IndexMain();
            Button_Screen.BackColor = Color.ForestGreen;            
        }
        //************************************************************************Cal
        private int calCount = 0;
        private void Button_Cal_Click(object sender, EventArgs e)
        {
            tabControl_Fill.SelectedTab = tabPage_Cal;
            InitializeFlowLayoutPanel_IndexMain();           
            Button_Cal.BackColor = Color.ForestGreen;

            counter.Start(initial, ConterEnum.Cal, comports, calCount);
        }
        #endregion
        #region Change on Time
        private void CheckedListBox_Main_Change()
        {
            CheckedListBox_Main.Items.Clear();
            foreach (var temp in initial.ChainDictionary)
            {
                CheckedListBox_Main.Items.Add(temp.Key, true);
            }            
        }
        private void DataGridView_ImpedanceFornt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            initial.SaveFormalTest((DataTable)DataGridView_ImpedanceFornt.DataSource);
            InitializeFormalTestDataTable();
        }

        private void DataGridView_TemperatureFornt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            initial.SaveFormalTest((DataTable)DataGridView_TemperatureFornt.DataSource);
            InitializeFormalTestDataTable();
        }
        private void DataGridView_Main_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            initial.SaveFormalTest((DataTable)DataGridView_Main.DataSource);
            InitializeFormalTestDataTable();
        }        
        #endregion
        #region Display
        private void DisplayDoFormalData(DataTable dataTable)
        {
            this.Invoke((Action)(() => {
                //紀錄當前的滑軸位置
                int r = 0;
                if(DataGridView_Main.FirstDisplayedScrollingColumnIndex>0)
                {
                    r = DataGridView_Main.FirstDisplayedScrollingColumnIndex;
                }
                //獲取dataTable
                DataGridView_Main.DataSource = dataTable;                

                //獲取CheckedListBox並修改DataGridView
                CurrencyManager cm = (CurrencyManager)BindingContext[DataGridView_Main.DataSource];
                cm.SuspendBinding();
                DataGridView_Main.FirstDisplayedScrollingColumnIndex = r;                
                cm.ResumeBinding();

                CheckedListBox_Main_Clicked();
                
                label_TotalCycleNo.Text = $"Total Cycle No.:                           { dataTable.Rows[0]["TemperatureCycle"]}/ Cycles";
            }));
        }
        private void DisplayDoPrecheckData(DataTable dataTable)
        {            
            this.Invoke((Action)(() => {               
                DataGridView_Precheck.DataSource = dataTable;
                initial.SaveFormalTest(dataTable);
            }));
        }
        private void DisplayDoPreTestData(DataTable dataTable)
        {
            this.Invoke((Action)(() => {
                DataGridView_Pretest.DataSource = dataTable;
                initial.SavePreTest(dataTable);
            }));
        }
        private void DisplayDoNinePointData(DataTable dataTable)
        {
            this.Invoke((Action)(() => {
                DataGridView_9points.DataSource = dataTable;
                initial.SavePreTest(dataTable);
            }));
        }       
        private void DisplayDoCalTestData(DataTable dataTable,double calCount)
        {
            this.Invoke((Action)(() => {
                DataGridView_CalChart.DataSource = dataTable;
                TextBox_CalChartCalCount.Text = calCount.ToString();
                initial.SaveFormalTest(dataTable);
            }));
        }       
        private void DisplayDoLossTestData(DataTable dataTable)
        {
            this.Invoke((Action)(() => {
                DataGridView_LossChart.DataSource = dataTable;
                //顯示阻抗
                HideAlldataGridView(DataGridView_LossChart);
                ShowdataGridView(DataGridView_LossChart,"Type", "Impedance");
                initial.SaveFormalTest(dataTable);
            }));
        }       
        private void ProcessTestEnd(ConterEnum conterEnum)
        {
            switch(conterEnum)
            {
                case ConterEnum.Cal:
                    if(calCount<3)
                    {
                        calCount++;
                        counter.Start(initial, ConterEnum.Cal, comports, calCount);
                    }
                    else
                    {                        
                        calCount = 0;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region Display subFunction
        private void CheckedListBox_Main_Clicked()
        {
            HideAlldataGridView(DataGridView_Main);
            for (int checkCount = 0; checkCount < CheckedListBox_Main.CheckedItems.Count; checkCount++)
            {                
                ShowdataGridView(DataGridView_Main, "Chain", CheckedListBox_Main.CheckedItems[checkCount].ToString());
            }
        }

        #endregion

        private void Button_MainEdit_Click(object sender, EventArgs e)
        {

        }

        private void Button_ImpedanceClear_Click(object sender, EventArgs e)
        {

        }
    }
}