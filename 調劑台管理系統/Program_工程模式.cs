using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
namespace 調劑台管理系統
{
    public partial class Form1 : Form
    {


        private void Program_工程模式_Init()
        {
            this.button_工程模式_門未關閉警報語音內容_下載.Click += Button_工程模式_門未關閉警報語音內容_下載_Click;

            this.plC_UI_Init.Add_Method(this.sub_Program_工程模式);
        }



        bool flag_工程模式_頁面更新 = false;
        private void sub_Program_工程模式()
        {
            if (this.plC_ScreenPage_Main.PageText == "工程模式")
            {
                if (!this.flag_工程模式_頁面更新)
                {
                    this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(true);
                    this.Funnction_交易記錄查詢_動作紀錄新增(enum_交易記錄查詢動作.操作工程模式, this.登入者名稱, "");
                    this.Function_工程模式_鎖控按鈕更新();
                    this.flag_工程模式_頁面更新 = true;
                }
            }
            else
            {
                this.flag_工程模式_頁面更新 = false;
            }
        }

        #region Function
        private void Function_工程模式_鎖控按鈕更新()
        {
            this.Function_從SQL取得儲位到本地資料();
            List<object[]> list_locker_table_value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
            List<object[]> list_locker_table_value_buf = new List<object[]>();
            string OutputAdress = "";
            string IP = "";
            int Num = -1;
            for (int i = 0; i < this.List_Locker.Count; i++)
            {
                OutputAdress = List_Locker[i].Get_OutputAdress();
                list_locker_table_value_buf = list_locker_table_value.GetRows((int)enum_Locker_Index_Table.輸出位置, OutputAdress);
                if (list_locker_table_value_buf.Count == 0)
                {
                    List_Locker[i].Visible = false;
                    continue;
                }
                IP = list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.IP].ObjectToString();
                Num = list_locker_table_value_buf[0][(int)enum_Locker_Index_Table.Num].ObjectToString().StringToInt32();
                object device = Fucnction_從本地資料取得儲位(IP);
                if (device == null)
                {
                    List_Locker[i].Visible = false;
                    continue;
                }
                if(device is Drawer)
                {
                    Drawer drawer = device as Drawer;
                    List_Locker[i].IP = IP;
                    List_Locker[i].Visible = true;
                    if (drawer.Name.StringIsEmpty()) continue;
                    List_Locker[i].Name = drawer.Name;
                }
                if(device is Storage)
                {
                    Storage storage = device as Storage;
                    List_Locker[i].IP = IP;
                    List_Locker[i].Visible = true;
                    if (storage.Name.StringIsEmpty()) continue;
                    List_Locker[i].Name = storage.Name;
                }
                if(device is RowsLED)
                {
                    RowsLED rowsLED = device as RowsLED;
                    List_Locker[i].IP = IP;
                    List_Locker[i].Visible = true;
                    if (rowsLED.Name.StringIsEmpty()) continue;
                    List_Locker[i].Name = rowsLED.Name;
                }
                if(device is RFIDClass)
                {
                    if (Num == -1) return;
                    RFIDClass rFIDClass = device as RFIDClass;
                    RFIDClass.DeviceClass deviceClass = rFIDClass.DeviceClasses[Num];
                    List_Locker[i].IP = IP;
                    List_Locker[i].Num = Num;
                    List_Locker[i].Visible = true;
                    if (deviceClass.Name.StringIsEmpty()) continue;
                    List_Locker[i].Name = deviceClass.Name;

                }
                else
                {
                    
                }
            }

        }

        #endregion
        #region Event
        private void Button_工程模式_門未關閉警報語音內容_下載_Click(object sender, EventArgs e)
        {
            Voice.GoogleSpeaker(this.textBox_工程模式_門未關閉警報語音內容.Text, @".//alarm.mp3");
            MyMessageBox.ShowDialog("下載完成!");
        }
        private void button_工程模式_調劑台名稱儲存_Click(object sender, EventArgs e)
        {
            this.SaveConfig工程模式();
            MyMessageBox.ShowDialog("儲存成功!");
        }
        private void panel_工程模式_領藥台_01_顏色_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.panel_工程模式_領藥台_01_顏色.BackColor = colorDialog.Color;
            }
        }
        private void panel_工程模式_領藥台_02_顏色_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.panel_工程模式_領藥台_02_顏色.BackColor = colorDialog.Color;
            }
        }
        #endregion
        #region StreamIO
        [Serializable]
        private class SaveConfig工程模式Class
        {
            public string 領藥台_01_名稱 = "";
            public string 領藥台_02_名稱 = "";
            public string 領藥台03_名稱 = "";
            public string 領藥台04_名稱 = "";
            public string 門未關閉警報語音內容 = "";

            public Color 領藥台_01_Color = Color.Black;
            public Color 領藥台_02_Color = Color.Black;
            public Color 領藥台03_Color = Color.Black;
            public Color 領藥台04_Color = Color.Black;
        }
        public void SaveConfig工程模式()
        {
            string StreamName = @".\\" + "adminConfig" + ".pro";
            SaveConfig工程模式Class saveConfig = new SaveConfig工程模式Class();
            saveConfig.領藥台_01_名稱 = this.textBox_工程模式_領藥台_01_名稱.Text;
            saveConfig.領藥台_02_名稱 = this.textBox_工程模式_領藥台_02_名稱.Text;

            saveConfig.領藥台_01_Color = this.panel_工程模式_領藥台_01_顏色.BackColor;
            saveConfig.領藥台_02_Color = this.panel_工程模式_領藥台_02_顏色.BackColor;
            saveConfig.門未關閉警報語音內容 = this.textBox_工程模式_門未關閉警報語音內容.Text;
            Basic.FileIO.SaveProperties(saveConfig, StreamName);
        }
        public void LoadConfig工程模式()
        {
            string StreamName = @".\\" + "adminConfig" + ".pro";
            object temp = new object();
            Basic.FileIO.LoadProperties(ref temp, StreamName);
            if(temp is SaveConfig工程模式Class)
            {
                this.Invoke(new Action(delegate 
                {
                    this.textBox_工程模式_領藥台_01_名稱.Text = ((SaveConfig工程模式Class)temp).領藥台_01_名稱;
                    this.textBox_工程模式_領藥台_02_名稱.Text = ((SaveConfig工程模式Class)temp).領藥台_02_名稱;

                    this.panel_工程模式_領藥台_01_顏色.BackColor = ((SaveConfig工程模式Class)temp).領藥台_01_Color;
                    this.panel_工程模式_領藥台_02_顏色.BackColor = ((SaveConfig工程模式Class)temp).領藥台_02_Color;

                    this.rJ_GroupBox_領藥台_01.PannelBorderColor = this.panel_工程模式_領藥台_01_顏色.BackColor;
                    this.rJ_GroupBox_領藥台_01.TitleBackColor = this.panel_工程模式_領藥台_01_顏色.BackColor;

                    this.rJ_GroupBox_領藥台_02.PannelBorderColor = this.panel_工程模式_領藥台_02_顏色.BackColor;
                    this.rJ_GroupBox_領藥台_02.TitleBackColor = this.panel_工程模式_領藥台_02_顏色.BackColor;
                    this.textBox_工程模式_門未關閉警報語音內容.Text = ((SaveConfig工程模式Class)temp).門未關閉警報語音內容;

                }));
            }

        }
        #endregion
    }
}
