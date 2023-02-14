﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MyUI;
using Basic;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyVersion("1.0.1.1")]
[assembly: AssemblyFileVersion("1.0.1.1")]
namespace 調劑台管理系統
{

    public partial class Form1 : Form
    {
        private string FormText = "";
        private LadderConnection.LowerMachine PLC;
        MyTimer MyTimer_TickTime = new MyTimer();
        private Stopwatch stopwatch = new Stopwatch();
        List<Locker> List_Locker = new List<Locker>();
        Basic.MyConvert myConvert = new Basic.MyConvert();

        PLC_Device PLC_Device_主頁面頁碼 = new PLC_Device("D0");
        PLC_Device PLC_Device_RFID使用 = new PLC_Device("S1000");
        PLC_Device PLC_Device_主機輸出模式 = new PLC_Device("S1001");
        PLC_Device PLC_Device_主機扣賬模式 = new PLC_Device("S1002");
        PLC_Device PLC_Device_掃碼槍COM通訊 = new PLC_Device("S1003");
        PLC_Device PLC_Device_抽屜不鎖上 = new PLC_Device("S1004");
        PLC_Device PLC_Device_藥物辨識圖片顯示 = new PLC_Device("S1005");

        #region DBConfigClass
        private const string DBConfigFileName = "DBConfig.txt";
        public DBConfigClass dBConfigClass = new DBConfigClass();
        public class DBConfigClass
        {
            private SQL_DataGridView.ConnentionClass dB_Basic = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_person_page = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_order_list = new SQL_DataGridView.ConnentionClass();

            public SQL_DataGridView.ConnentionClass DB_Basic { get => dB_Basic; set => dB_Basic = value; }
            public SQL_DataGridView.ConnentionClass DB_person_page { get => dB_person_page; set => dB_person_page = value; }
            public SQL_DataGridView.ConnentionClass DB_order_list { get => dB_order_list; set => dB_order_list = value; }

        }
        private void LoadDBConfig()
        {
            string jsonstr = MyFileStream.LoadFileAllText($".//{DBConfigFileName}");
            if (jsonstr.StringIsEmpty())
            {

                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(new DBConfigClass());
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{DBConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{DBConfigFileName}檔案失敗!");
                }
                MyMessageBox.ShowDialog($"未建立參數文件!請至子目錄設定{DBConfigFileName}");
                Application.Exit();
            }
            else
            {
                dBConfigClass = Basic.Net.JsonDeserializet<DBConfigClass>(jsonstr);

                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(dBConfigClass);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{DBConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{DBConfigFileName}檔案失敗!");
                }

            }
        }
        #endregion
        #region MyConfigClass
        private const string MyConfigFileName = "MyConfig.txt";
        public MyConfigClass myConfigClass = new MyConfigClass();
        public class MyConfigClass
        {
            private string fTP_Server = "";
            private bool _主機扣帳模式 = false;
            private bool _主機輸出模式 = false;
            private bool _RFID使用 = true;
            private bool _掃碼槍COM通訊 = true;
            private bool _藥物辨識圖片顯示 = true;
            private bool ePD583_Enable = true;
            private bool ePD266_Enable = true;
            private bool rowsLED_Enable = true;
            private bool rFID_Enable = true;
            private bool pannel35_Enable = true;
            private bool _帳密登入_Enable = true;

            private string rFID_COMPort = "COM1";
            private string scanner01_COMPort = "COM2";
            private string scanner02_COMPort = "COM3";


            public string FTP_Server { get => fTP_Server; set => fTP_Server = value; }
            public bool 主機扣帳模式 { get => _主機扣帳模式; set => _主機扣帳模式 = value; }
            public bool 主機輸出模式 { get => _主機輸出模式; set => _主機輸出模式 = value; }
            public bool RFID使用 { get => _RFID使用; set => _RFID使用 = value; }
            public bool 掃碼槍COM通訊 { get => _掃碼槍COM通訊; set => _掃碼槍COM通訊 = value; }
            public string RFID_COMPort { get => rFID_COMPort; set => rFID_COMPort = value; }
            public string Scanner01_COMPort { get => scanner01_COMPort; set => scanner01_COMPort = value; }
            public string Scanner02_COMPort { get => scanner02_COMPort; set => scanner02_COMPort = value; }
            public bool 藥物辨識圖片顯示 { get => _藥物辨識圖片顯示; set => _藥物辨識圖片顯示 = value; }
            public bool EPD583_Enable { get => ePD583_Enable; set => ePD583_Enable = value; }
            public bool EPD266_Enable { get => ePD266_Enable; set => ePD266_Enable = value; }
            public bool RowsLED_Enable { get => rowsLED_Enable; set => rowsLED_Enable = value; }
            public bool RFID_Enable { get => rFID_Enable; set => rFID_Enable = value; }
            public bool Pannel35_Enable { get => pannel35_Enable; set => pannel35_Enable = value; }
            public bool 帳密登入_Enable { get => _帳密登入_Enable; set => _帳密登入_Enable = value; }


        }
        private void LoadMyConfig()
        {
            string jsonstr = MyFileStream.LoadFileAllText($".//{MyConfigFileName}");
            if (jsonstr.StringIsEmpty())
            {
                jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(new MyConfigClass());
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{MyConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                }
                MyMessageBox.ShowDialog($"未建立參數文件!請至子目錄設定{MyConfigFileName}");
                Application.Exit();
            }
            else
            {
                myConfigClass = Basic.Net.JsonDeserializet<MyConfigClass>(jsonstr);

                jsonstr = Basic.Net.JsonSerializationt<MyConfigClass>(myConfigClass);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{MyConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{MyConfigFileName}檔案失敗!");
                }

            }

            this.ftp_DounloadUI1.FTP_Server = myConfigClass.FTP_Server;
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == false)
            {
                LoadDBConfig();
                LoadMyConfig();

                this.stopwatch.Start();
                this.Text += "Ver" + this.ProductVersion;
                this.FormText = this.Text;
                this.WindowState = FormWindowState.Maximized;
                this.plC_UI_Init.Run(this.FindForm(), this.lowerMachine_Panel);
                this.plC_UI_Init.音效 = false;
                this.plC_UI_Init.全螢幕顯示 = false;

                MyMessageBox.form = this.FindForm();
                Dialog_NumPannel.form = this.FindForm();
                Dialog_輸入批號.form = this.FindForm();
                Dialog_輸入效期.form = this.FindForm();
                Dialog_輸入藥品碼.form = this.FindForm();
                Dialog_手輸醫囑.form = this.FindForm();
                Dialog_醫囑退藥.form = this.FindForm();
                Locker.OuputReverse = true;
                Basic.MyMessageBox.音效 = false;
                string ProcessName = "WINWORD";//換成想要結束的進程名字
                System.Diagnostics.Process[] MyProcess = System.Diagnostics.Process.GetProcessesByName(ProcessName);
                for (int i = 0; i < MyProcess.Length; i++)
                {
                    MyProcess[i].Kill();
                }

                if (myConfigClass.RFID使用)
                {
                    this.rfiD_FX600_UI.Init(myConfigClass.RFID_COMPort);
                }

                this.plC_UI_Init.UI_Finished_Event += PlC_UI_Init_UI_Finished_Event;

            }

        }
        private void PlC_UI_Init_UI_Finished_Event()
        {
            this.PLC = this.lowerMachine_Panel.GetlowerMachine();
        
            PLC_Device_主機輸出模式.Bool = myConfigClass.主機輸出模式;
            PLC_Device_主機扣賬模式.Bool = myConfigClass.主機扣帳模式;
            PLC_Device_掃碼槍COM通訊.Bool = myConfigClass.掃碼槍COM通訊;
            PLC_Device_藥物辨識圖片顯示.Bool = myConfigClass.藥物辨識圖片顯示;

            int index = 0;
            if (myConfigClass.Scanner01_COMPort.StringIsEmpty())
            {
                rJ_GroupBox_領藥台_01.Visible = false;
                index++;
            }
            if (myConfigClass.Scanner02_COMPort.StringIsEmpty())
            {
                rJ_GroupBox_領藥台_02.Visible = false;
                index++;
            }

            if (PLC_Device_掃碼槍COM通訊.Bool)
            {
                MySerialPort_Scanner01.ConsoleWrite = true;
                MySerialPort_Scanner02.ConsoleWrite = true;
                if (!myConfigClass.Scanner01_COMPort.StringIsEmpty()) MySerialPort_Scanner01.Init(myConfigClass.Scanner01_COMPort, 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
                if (!myConfigClass.Scanner02_COMPort.StringIsEmpty()) MySerialPort_Scanner02.Init(myConfigClass.Scanner02_COMPort, 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            }

            H_Pannel_lib.DeviceBasicUI.TimeOut = 11000;

            PLC_UI_Init.Set_PLC_ScreenPage(panel_Main, this.plC_ScreenPage_Main);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_藥品資料_藥檔資料, this.plC_ScreenPage_藥品資料_藥檔資料);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_系統, this.plC_ScreenPage_系統);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_系統_Pannel設定, this.plC_ScreenPage_系統_Pannel設定);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_儲位管理, this.plC_ScreenPage_儲位管理);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_人員資料, this.plC_ScreenPage_人員資料);

            this.plC_RJ_ScreenButton_EPD583.Visible = myConfigClass.EPD583_Enable;
            this.plC_RJ_ScreenButton_EPD266.Visible = myConfigClass.EPD266_Enable;
            this.plC_RJ_ScreenButton_RowsLED.Visible = myConfigClass.RowsLED_Enable;
            this.plC_RJ_ScreenButton_RFID.Visible = myConfigClass.RFID_Enable;
            this.plC_RJ_ScreenButton_Pannel35.Visible = myConfigClass.Pannel35_Enable;


            Dialog_RFID領退藥頁面.connentionClass = dBConfigClass.DB_Basic;
            SQLUI.SQL_DataGridView.SQL_Set_Properties(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, this.FindForm());
            SQLUI.SQL_DataGridView.SQL_Set_Properties(sqL_DataGridView_批次領藥資料, dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode);

            this.Program_系統_Init();
            this.Program_醫囑資料_Init();

            this.Program_儲位管理_EPD583_Init();
            this.Program_儲位管理_EPD266_Init();
            this.Program_儲位管理_RowsLED_Init();
            this.Program_儲位管理_RFID_Init();
            this.Program_儲位管理_Pannel35_Init();
            this.Program_領藥_Init();
            this.Program_藥品資料_藥檔設定_Init();
            this.Program_藥品資料_藥檔資料_Init();
            this.Program_藥品資料_儲位總庫存表_Init();
            this.Program_藥品資料_儲位效期表_Init();
            this.Program_人員資料_Init();
            this.Program_工程模式_Init();
            this.Program_交易記錄查詢_Init();
            this.Program_效期管理_Init();
            this.Program_入庫作業_Init();
            this.Program_後台登入_Init();
            this.Program_批次領藥_Init();
            this.Program_取藥堆疊資料_Init();
            this.Program_輸出入檢查_Init();

            this.plC_UI_Init.Add_Method(this.sub_Program_Scanner_RS232);

            this.LoadConfig工程模式();

            Task task = Task.Run(new Action(delegate
            {
                Function_從SQL取得儲位到本地資料();
            }));

            //this.AutoSize = true;
            //this.AutoScaleDimensions = new System.Drawing.SizeF(70F, 70F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            //AutoReSizeForm.SetFormSize(this.FindForm());

            //this.Font = new System.Drawing.Font("新細明體", 9F / 1.5F);
        }

        #region PLC_Method
        PLC_Device PLC_Device_Method = new PLC_Device("");
        PLC_Device PLC_Device_Method_OK = new PLC_Device("");
        Task Task_Method;
        MyTimer MyTimer_Method_結束延遲 = new MyTimer();
        int cnt_Program_Method = 65534;
        void sub_Program_Method()
        {
            if (cnt_Program_Method == 65534)
            {
                this.MyTimer_Method_結束延遲.StartTickTime(10000);
                PLC_Device_Method.SetComment("PLC_Method");
                PLC_Device_Method_OK.SetComment("PLC_Method_OK");
                PLC_Device_Method.Bool = false;
                cnt_Program_Method = 65535;
            }
            if (cnt_Program_Method == 65535) cnt_Program_Method = 1;
            if (cnt_Program_Method == 1) cnt_Program_Method_檢查按下(ref cnt_Program_Method);
            if (cnt_Program_Method == 2) cnt_Program_Method_初始化(ref cnt_Program_Method);
            if (cnt_Program_Method == 3) cnt_Program_Method = 65500;
            if (cnt_Program_Method > 1) cnt_Program_Method_檢查放開(ref cnt_Program_Method);

            if (cnt_Program_Method == 65500)
            {
                this.MyTimer_Method_結束延遲.TickStop();
                this.MyTimer_Method_結束延遲.StartTickTime(10000);
                PLC_Device_Method.Bool = false;
                PLC_Device_Method_OK.Bool = false;
                cnt_Program_Method = 65535;
            }
        }
        void cnt_Program_Method_檢查按下(ref int cnt)
        {
            if (PLC_Device_Method.Bool) cnt++;
        }
        void cnt_Program_Method_檢查放開(ref int cnt)
        {
            if (!PLC_Device_Method.Bool) cnt = 65500;
        }
        void cnt_Program_Method_初始化(ref int cnt)
        {
            if (this.MyTimer_Method_結束延遲.IsTimeOut())
            {
                if (Task_Method == null)
                {
                    Task_Method = new Task(new Action(delegate { }));
                }
                if (Task_Method.Status == TaskStatus.RanToCompletion)
                {
                    Task_Method = new Task(new Action(delegate { }));
                }
                if (Task_Method.Status == TaskStatus.Created)
                {
                    Task_Method.Start();
                }
                cnt++;
            }
        }







        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
        private void Update()
        {
            if (Basic.MyMessageBox.ShowDialog("是否執行系統更新?下載完成,系統將會關閉!", "Update", Basic.MyMessageBox.enum_BoxType.Asterisk, Basic.MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
            {
                if (this.ftp_DounloadUI1.DownloadFile())
                {
                    if (this.ftp_DounloadUI1.SaveFile())
                    {
                        this.ftp_DounloadUI1.RunFile(this.FindForm());
                    }
                    else
                    {
                        Basic.MyMessageBox.ShowDialog("安裝檔存檔失敗!");
                    }
                }
                else
                {
                    Basic.MyMessageBox.ShowDialog("下載失敗!");
                }
            }
        }
        private void plC_RJ_Button_系統更新_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Update();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.ioC1280.Run(this.FindForm(), PLC);
        }

       
    }


    public class AutoReSizeForm

    {
        static float SH
        {
            get
            {
                return (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 1000F;
            }
        }
        static float SW
        {
            get
            {
                return (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 1920F;
            }
        }

        public static void SetFormSize(Control fm)
        {
            fm.Location = new Point((int)(fm.Location.X * SW), (int)(fm.Location.Y * SH));
            fm.Size = new Size((int)(fm.Size.Width * SW), (int)(fm.Size.Height * SH));
            fm.Font = new Font(fm.Font.Name, fm.Font.Size * SH, fm.Font.Style, fm.Font.Unit, fm.Font.GdiCharSet, fm.Font.GdiVerticalFont);
            if (fm.Controls.Count != 0)
            {
                SetControlSize(fm);
            }
        }
        private static void SetControlSize(Control InitC)
        {
            foreach (Control c in InitC.Controls)
            {
                c.Location = new Point((int)(c.Location.X * SW), (int)(c.Location.Y * SH));
                c.Size = new Size((int)(c.Size.Width * SW), (int)(c.Size.Height * SH));
                c.Font = new Font(c.Font.Name, c.Font.Size * SH, c.Font.Style, c.Font.Unit, c.Font.GdiCharSet, c.Font.GdiVerticalFont);
                if (c.Controls.Count != 0)
                {

                }
            }
        }
    }
}
