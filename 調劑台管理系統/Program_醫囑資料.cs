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

namespace 調劑台管理系統
{
    [Serializable]
    public class OrderClass
    {
        private string _藥局代碼 = "";
        private string _藥品碼 = "";
        private string _藥品名稱 = "";
        private string _包裝單位 = "";
        private string _交易量 = "";
        private string _病歷號 = "";
        private string _開方時間 = "";
        private string pRI_KEY = "";
        private string _藥袋條碼 = "";
        private string _劑量 = "";
        private string _頻次 = "";
        private string _途徑 = "";
        private string _天數 = "";
        private string _處方序號 = "";

        public string 藥局代碼 { get => _藥局代碼; set => _藥局代碼 = value; }
        public string 藥品碼 { get => _藥品碼; set => _藥品碼 = value; }
        public string 藥品名稱 { get => _藥品名稱; set => _藥品名稱 = value; }
        public string 包裝單位 { get => _包裝單位; set => _包裝單位 = value; }
        public string 交易量 { get => _交易量; set => _交易量 = value; }
        public string 病歷號 { get => _病歷號; set => _病歷號 = value; }
        public string 開方時間 { get => _開方時間; set => _開方時間 = value; }
        public string PRI_KEY { get => pRI_KEY; set => pRI_KEY = value; }
        public string 藥袋條碼 { get => _藥袋條碼; set => _藥袋條碼 = value; }
        public string 劑量 { get => _劑量; set => _劑量 = value; }
        public string 頻次 { get => _頻次; set => _頻次 = value; }
        public string 途徑 { get => _途徑; set => _途徑 = value; }
        public string 天數 { get => _天數; set => _天數 = value; }
        public string 處方序號 { get => _處方序號; set => _處方序號 = value; }
    }

    public enum enum_醫囑資料_狀態
    {
        未過帳,
        已過帳
    }
    public enum enum_醫囑資料
    {
        GUID,
        PRI_KEY,
        藥局代碼,
        藥袋條碼,
        藥品碼,
        藥品名稱,
        病人姓名,
        病歷號,
        交易量,
        開方日期,
        產出時間,
        過帳時間,
        狀態,
    }
    public partial class Form1 : Form
    {
    
        private void Program_醫囑資料_Init()
        {
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_醫囑資料, dBConfigClass.DB_order_list);

            this.sqL_DataGridView_醫囑資料.Init();
            if (!this.sqL_DataGridView_醫囑資料.SQL_IsTableCreat()) this.sqL_DataGridView_醫囑資料.SQL_CreateTable();
            this.sqL_DataGridView_醫囑資料.DataGridRowsChangeRefEvent += SqL_DataGridView_醫囑資料_DataGridRowsChangeRefEvent;
            this.sqL_DataGridView_醫囑資料.DataGridRefreshEvent += SqL_DataGridView_醫囑資料_DataGridRefreshEvent;

            this.plC_RJ_Button_醫囑資料_顯示全部.MouseDownEvent += PlC_RJ_Button_醫囑資料_顯示全部_MouseDownEvent;
            this.plC_RJ_Button_醫囑資料_搜尋條件_藥袋條碼_搜尋.MouseDownEvent += PlC_RJ_Button_醫囑資料_搜尋條件_藥袋條碼_搜尋_MouseDownEvent;

            this.plC_UI_Init.Add_Method(Program_醫囑資料);
        }

  

        private void Program_醫囑資料()
        {
            this.sub_Program_醫囑資料_檢查刷條碼();
        }
        #region PLC_醫囑資料_檢查刷條碼
        PLC_Device PLC_Device_醫囑資料_檢查刷條碼 = new PLC_Device("");
        PLC_Device PLC_Device_醫囑資料_檢查刷條碼_OK = new PLC_Device("");
        MyTimer MyTimer_醫囑資料_檢查刷條碼_結束延遲 = new MyTimer();
        int cnt_Program_醫囑資料_檢查刷條碼 = 65534;
        void sub_Program_醫囑資料_檢查刷條碼()
        {
            if (this.plC_ScreenPage_Main.PageText == "醫囑資料") PLC_Device_醫囑資料_檢查刷條碼.Bool = true;
            else PLC_Device_醫囑資料_檢查刷條碼.Bool = false;
            if (cnt_Program_醫囑資料_檢查刷條碼 == 65534)
            {
                this.MyTimer_醫囑資料_檢查刷條碼_結束延遲.StartTickTime(10000);
                PLC_Device_醫囑資料_檢查刷條碼.SetComment("PLC_醫囑資料_檢查刷條碼");
                PLC_Device_醫囑資料_檢查刷條碼_OK.SetComment("PLC_醫囑資料_檢查刷條碼_OK");
                PLC_Device_醫囑資料_檢查刷條碼.Bool = false;
                cnt_Program_醫囑資料_檢查刷條碼 = 65535;
            }
            if (cnt_Program_醫囑資料_檢查刷條碼 == 65535) cnt_Program_醫囑資料_檢查刷條碼 = 1;
            if (cnt_Program_醫囑資料_檢查刷條碼 == 1) cnt_Program_醫囑資料_檢查刷條碼_檢查按下(ref cnt_Program_醫囑資料_檢查刷條碼);
            if (cnt_Program_醫囑資料_檢查刷條碼 == 2) cnt_Program_醫囑資料_檢查刷條碼_初始化(ref cnt_Program_醫囑資料_檢查刷條碼);
            if (cnt_Program_醫囑資料_檢查刷條碼 == 3) cnt_Program_醫囑資料_檢查刷條碼 = 65500;
            if (cnt_Program_醫囑資料_檢查刷條碼 > 1) cnt_Program_醫囑資料_檢查刷條碼_檢查放開(ref cnt_Program_醫囑資料_檢查刷條碼);

            if (cnt_Program_醫囑資料_檢查刷條碼 == 65500)
            {
                this.MyTimer_醫囑資料_檢查刷條碼_結束延遲.TickStop();
                this.MyTimer_醫囑資料_檢查刷條碼_結束延遲.StartTickTime(10000);
                PLC_Device_醫囑資料_檢查刷條碼.Bool = false;
                PLC_Device_醫囑資料_檢查刷條碼_OK.Bool = false;
                cnt_Program_醫囑資料_檢查刷條碼 = 65535;
            }
        }
        void cnt_Program_醫囑資料_檢查刷條碼_檢查按下(ref int cnt)
        {
            if (PLC_Device_醫囑資料_檢查刷條碼.Bool) cnt++;
        }
        void cnt_Program_醫囑資料_檢查刷條碼_檢查放開(ref int cnt)
        {
            if (!PLC_Device_醫囑資料_檢查刷條碼.Bool) cnt = 65500;
        }
        void cnt_Program_醫囑資料_檢查刷條碼_初始化(ref int cnt)
        {
            string 一維碼 = "";
            if (MySerialPort_Scanner01.ReadByte() != null)
            {
                string text = this.MySerialPort_Scanner01.ReadString();
                this.MySerialPort_Scanner01.ClearReadByte();
                if (text.Length <= 2 || text.Length > 30) return;
                if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                text = text.Replace("\r\n", "");
                一維碼 = text;
            }
            if (MySerialPort_Scanner02.ReadByte() != null)
            {
                string text = this.MySerialPort_Scanner02.ReadString();
                this.MySerialPort_Scanner02.ClearReadByte();
                if (text.Length <= 2 || text.Length > 30) return;
                if (text.Substring(text.Length - 2, 2) != "\r\n") return;
                text = text.Replace("\r\n", "");
                一維碼 = text;
            }
            if (一維碼.StringIsEmpty()) return;
            List<object[]> list_value = this.sqL_DataGridView_醫囑資料.SQL_GetRowsByBetween((int)enum_醫囑資料.開方日期, dateTimePicke_醫囑資料_開方日期_起始, dateTimePicke_醫囑資料_開方日期_結束, false);
            list_value = list_value.GetRows((int)enum_醫囑資料.藥袋條碼, 一維碼);
            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog($"未搜尋到'{一維碼}'此條碼資料!,請確定開方日期範圍是否設定正確‧");
            }
            this.sqL_DataGridView_醫囑資料.RefreshGrid(list_value);
            cnt++;

        }







        #endregion

        #region Function

        #endregion

        #region Event
        private void SqL_DataGridView_醫囑資料_DataGridRefreshEvent()
        {
            String 狀態 = "";
            for (int i = 0; i < this.sqL_DataGridView_醫囑資料.dataGridView.Rows.Count; i++)
            {
                狀態 = this.sqL_DataGridView_醫囑資料.dataGridView.Rows[i].Cells[(int)enum_醫囑資料.狀態].Value.ToString();
                if (狀態 == enum_醫囑資料_狀態.已過帳.GetEnumName())
                {
                    this.sqL_DataGridView_醫囑資料.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    this.sqL_DataGridView_醫囑資料.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }


            }
        }
        private void SqL_DataGridView_醫囑資料_DataGridRowsChangeRefEvent(ref List<object[]> RowsList)
        {
            RowsList.Sort(new ICP_醫囑資料());
        }
        private void PlC_RJ_Button_醫囑資料_顯示全部_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.TickStop();
            myTimer.StartTickTime(50000);
            List<object[]> list_value = this.sqL_DataGridView_醫囑資料.SQL_GetRowsByBetween((int)enum_醫囑資料.開方日期, dateTimePicke_醫囑資料_開方日期_起始, dateTimePicke_醫囑資料_開方日期_結束, false);
            if (rJ_TextBox_醫囑資料_搜尋條件_藥品碼.Texts.StringIsEmpty() == false) list_value = list_value.GetRowsByLike((int)enum_醫囑資料.藥品碼, rJ_TextBox_醫囑資料_搜尋條件_藥品碼.Texts);
            if (rJ_TextBox_醫囑資料_搜尋條件_藥品名稱.Texts.StringIsEmpty() == false) list_value = list_value.GetRowsByLike((int)enum_醫囑資料.藥品名稱, rJ_TextBox_醫囑資料_搜尋條件_藥品名稱.Texts);
            if (rJ_TextBox_醫囑資料_搜尋條件_病歷號.Texts.StringIsEmpty() == false) list_value = list_value.GetRows((int)enum_醫囑資料.病歷號, rJ_TextBox_醫囑資料_搜尋條件_病歷號.Texts);


            Console.Write($"取得醫囑資料 , 耗時 : {myTimer.ToString()} ms\n");
            this.sqL_DataGridView_醫囑資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_醫囑資料_搜尋條件_藥袋條碼_搜尋_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            string 藥袋條碼 = this.rJ_TextBox_醫囑資料_搜尋條件_藥袋條碼.Texts;

            if (藥袋條碼.StringIsEmpty()) return;
            Console.Write($"開始搜尋條碼資料...\n");
            List<object[]> list_value = this.sqL_DataGridView_醫囑資料.SQL_GetRows((int)enum_醫囑資料.藥袋條碼, 藥袋條碼, false);
            Console.Write($"搜尋條碼資料,耗時{myTimer.ToString()}ms\n");
            list_value = list_value.GetRowsInDate((int)enum_醫囑資料.開方日期, dateTimePicke_醫囑資料_開方日期_起始, dateTimePicke_醫囑資料_開方日期_結束);
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog($"未搜尋到'{藥袋條碼}'此條碼資料!,請確定開方日期範圍是否設定正確‧");
            }
            this.sqL_DataGridView_醫囑資料.RefreshGrid(list_value);
            Console.Write($"更新條碼資料搜尋結果,耗時{myTimer.ToString()}ms\n");
        }

        #endregion


        private class ICP_醫囑資料 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {

                string date01 = x[(int)enum_醫囑資料.產出時間].ToDateTimeString_6();
                string date02 = y[(int)enum_醫囑資料.產出時間].ToDateTimeString_6();
                return date01.CompareTo(date02);

            }
        }
    }
}
