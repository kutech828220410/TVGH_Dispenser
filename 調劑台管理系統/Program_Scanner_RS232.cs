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
    public partial class Form1 : Form
    {
        MySerialPort MySerialPort_Scanner01 = new MySerialPort();
        MySerialPort MySerialPort_Scanner02 = new MySerialPort();

        int enum_Scanner_陣列內容_Data_Length = Enum.GetValues(typeof(enum_Scanner_陣列內容)).Length;
        private enum enum_Scanner_陣列內容
        {
            藥袋序號,
            病床代號,
            藥品英文名稱,
            藥品中文名稱,
            藥品學名,
            藥品單位,
            藥品劑量,
            使用數量,
            頻次,
            使用方式,
            病歷號,
            病人姓名,
            藥品代碼,
            看診科別,
            看診醫師,
            開方日期,
            開方時間,
        }

        private enum enum_Scanner_藥袋01
        {
            藥袋序號,
            病床代號,
            藥品英文名稱,
            藥品中文名稱,
            藥品單位,
            藥品劑量,
            使用數量,
            頻次,
            使用方式,
            病歷號,
            病人姓名,
            藥品代碼,
            看診科別,
            看診醫師,
            開方日期,
            開方時間,
        }
       
        void sub_Program_Scanner_RS232()
        {
            //this.sub_Program_Scanner_01_QRCode_接收檢查();
            //this.sub_Program_Scanner_02_QRCode_接收檢查();

            //this.sub_Program_Scanner_01_BarCode_接收檢查();
            //this.sub_Program_Scanner_02_BarCode_接收檢查();
        }

        //#region PLC_Scanner_01_QRCode_接收檢查
        //PLC_Device PLC_Device_Scanner_01_QRCode_接收檢查 = new PLC_Device("S30125");
        //PLC_Device PLC_Device_Scanner_01_QRCode_接收資料_OK = new PLC_Device("S30126");
        //PLC_Device PLC_Device_Scanner_01_QRCode_接收旗標 = new PLC_Device("S30104");
        //string[] Scanner_01_QRCode_Data = new string[20];
        //string str_Scanner_01_QRCode = "";
        //string str_Scanner_01_QRCode_buf = "";
        //MyTimer MyTimer_Scanner_01_QRCode_接收逾時時間 = new MyTimer();
        //int cnt_Program_Scanner_01_QRCode_接收檢查 = 65534;
        //void sub_Program_Scanner_01_QRCode_接收檢查()
        //{
        //    if (cnt_Program_Scanner_01_QRCode_接收檢查 == 65534)
        //    {
        //        PLC_Device_Scanner_01_QRCode_接收檢查.SetComment("PLC_Scanner_01_QRCode_接收檢查");
        //        PLC_Device_Scanner_01_QRCode_接收資料_OK.SetComment("PLC_Device_Scanner_01_QRCode_接收資料_OK");
        //        PLC_Device_Scanner_01_QRCode_接收檢查.Bool = false;
        //        cnt_Program_Scanner_01_QRCode_接收檢查 = 65535;
        //    }
        //    if (cnt_Program_Scanner_01_QRCode_接收檢查 == 65535) cnt_Program_Scanner_01_QRCode_接收檢查 = 1;
        //    if (cnt_Program_Scanner_01_QRCode_接收檢查 == 1) cnt_Program_Scanner_01_QRCode_接收檢查_檢查按下(ref cnt_Program_Scanner_01_QRCode_接收檢查);
        //    if (cnt_Program_Scanner_01_QRCode_接收檢查 == 2) cnt_Program_Scanner_01_QRCode_接收檢查_初始化(ref cnt_Program_Scanner_01_QRCode_接收檢查);
        //    if (cnt_Program_Scanner_01_QRCode_接收檢查 == 3) cnt_Program_Scanner_01_QRCode_接收檢查_開始接收(ref cnt_Program_Scanner_01_QRCode_接收檢查);
        //    if (cnt_Program_Scanner_01_QRCode_接收檢查 == 4) cnt_Program_Scanner_01_QRCode_接收檢查 = 65500;
        //    if (cnt_Program_Scanner_01_QRCode_接收檢查 > 1) cnt_Program_Scanner_01_QRCode_接收檢查_檢查放開(ref cnt_Program_Scanner_01_QRCode_接收檢查);

        //    if (cnt_Program_Scanner_01_QRCode_接收檢查 == 65500)
        //    {
        //        this.MySerialPort_Scanner01.ClearReadByte();
        //        PLC_Device_Scanner_01_QRCode_接收檢查.Bool = false;
        //        PLC_Device_Scanner_01_QRCode_接收旗標.Bool = false;
        //        cnt_Program_Scanner_01_QRCode_接收檢查 = 65535;
        //    }
        //}
        //void cnt_Program_Scanner_01_QRCode_接收檢查_檢查按下(ref int cnt)
        //{
        //    if (PLC_Device_Scanner_01_QRCode_接收檢查.Bool)
        //    {
        //        this.MySerialPort_Scanner01.ClearReadByte();
        //        PLC_Device_Scanner_01_QRCode_接收旗標.Bool = false;
        //        PLC_Device_Scanner_01_QRCode_接收資料_OK.Bool = false;
        //        cnt++;
        //    }
        //}
        //void cnt_Program_Scanner_01_QRCode_接收檢查_檢查放開(ref int cnt)
        //{
        //    if (!PLC_Device_Scanner_01_QRCode_接收檢查.Bool) cnt = 65500;
        //}
        //void cnt_Program_Scanner_01_QRCode_接收檢查_初始化(ref int cnt)
        //{
        //    if (MySerialPort_Scanner01.ReadByte()!= null)
        //    {
        //        for (int i = 0; i < this.Scanner_01_QRCode_Data.Length; i++)
        //        {
        //            this.Scanner_01_QRCode_Data[i] = "";
        //        }
        //        MyTimer_Scanner_01_QRCode_接收逾時時間.TickStop();
        //        MyTimer_Scanner_01_QRCode_接收逾時時間.StartTickTime(500);
        //        str_Scanner_01_QRCode = "";
        //        str_Scanner_01_QRCode_buf = "";
        //        cnt++;
        //    }
        //}
        //void cnt_Program_Scanner_01_QRCode_接收檢查_開始接收(ref int cnt)
        //{
        //    if (MyTimer_Scanner_01_QRCode_接收逾時時間.IsTimeOut())
        //    {
        //         this.MySerialPort_Scanner01.ClearReadByte();
        //        cnt = 65500;
        //        return;
        //    }
        //    str_Scanner_01_QRCode_buf = this.MySerialPort_Scanner01.ReadString();
        //    if (str_Scanner_01_QRCode_buf.Length - 2 > 0)
        //    {
        //        string last_str = str_Scanner_01_QRCode_buf.Substring(str_Scanner_01_QRCode_buf.Length - 2, 2);
        //        if (last_str == "\r\n")
        //        {
        //            Scanner_01_QRCode_Data = new string[new enum_Scanner_陣列內容().GetLength()];
        //            str_Scanner_01_QRCode = str_Scanner_01_QRCode_buf.Replace("\r\n", "");

        //            Console.Write($"Scanner 01 Data : {str_Scanner_01_QRCode_buf}\n");
        //            List<object[]> list_value = this.sqL_DataGridView_醫囑資料.SQL_GetAllRows(true);
        //            list_value = (from value in list_value
        //                          where value[(int)enum_醫囑資料.藥袋條碼].ObjectToString() == str_Scanner_01_QRCode || value[(int)enum_醫囑資料.藥袋條碼].ObjectToString() == str_Scanner_01_QRCode
        //                          select value).ToList();
        //            if(list_value.Count == 0)
        //            {
        //                Console.Write($"找無此藥單資料\n");
        //                cnt = 65500;
        //                return;
        //            }
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.藥品代碼] = list_value[0][(int)enum_醫囑資料.藥品碼].ObjectToString();
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.病人姓名] = list_value[0][(int)enum_醫囑資料.病人姓名].ObjectToString();
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.病歷號] = list_value[0][(int)enum_醫囑資料.病歷號].ObjectToString();
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.使用數量] = list_value[0][(int)enum_醫囑資料.交易量].ObjectToString();
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.開方日期] = list_value[0][(int)enum_醫囑資料.開方日期].ObjectToString().StringToDateTime().ToDateString();
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.開方時間] = list_value[0][(int)enum_醫囑資料.開方日期].ObjectToString().StringToDateTime().ToTimeString();

        //            List<object[]> list_藥品資料_藥檔資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetRows(enum_藥品資料_藥檔資料.藥品碼.GetEnumName(), Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.藥品代碼], false);
        //            if(list_藥品資料_藥檔資料.Count == 0)
        //            {
        //                Console.Write($"找無此藥品資料_藥檔資料 Code: { Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.藥品代碼]}\n");
        //                cnt = 65500;
        //                return;
        //            }
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.藥品英文名稱] = list_藥品資料_藥檔資料[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.藥品中文名稱] = list_藥品資料_藥檔資料[0][(int)enum_藥品資料_藥檔資料.藥品中文名稱].ObjectToString();
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.藥品學名] = list_藥品資料_藥檔資料[0][(int)enum_藥品資料_藥檔資料.藥品學名].ObjectToString();
        //            Scanner_01_QRCode_Data[(int)enum_Scanner_陣列內容.藥品單位] = list_藥品資料_藥檔資料[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();

        //            for(int i = 0;  i < new enum_Scanner_陣列內容().GetLength(); i++)
        //            {
        //                string Name = new enum_Scanner_陣列內容().GetEnumNames()[i];
        //                string Value = Scanner_01_QRCode_Data[i];
        //                Console.Write($"{Name} : {Value}\n");
        //            }

        //            PLC_Device_Scanner_01_QRCode_接收資料_OK.Bool = true;

        //            cnt++;
        //        }
        //    }


        //}

        //#endregion
        //#region PLC_Scanner_02_QRCode_接收檢查
        //PLC_Device PLC_Device_Scanner_02_QRCode_接收檢查 = new PLC_Device("S30225");
        //PLC_Device PLC_Device_Scanner_02_QRCode_接收資料_OK = new PLC_Device("S30226");
        //PLC_Device PLC_Device_Scanner_02_QRCode_接收旗標 = new PLC_Device("S30204");
        //string[] Scanner_02_QRCode_Data = new string[17];
        //string str_Scanner_02_QRCode = "";
        //string str_Scanner_02_QRCode_buf = "";
        //MyTimer MyTimer_Scanner_02_QRCode_接收逾時時間 = new MyTimer();
        //int cnt_Program_Scanner_02_QRCode_接收檢查 = 65534;
        //void sub_Program_Scanner_02_QRCode_接收檢查()
        //{
        //    if (cnt_Program_Scanner_02_QRCode_接收檢查 == 65534)
        //    {
        //        PLC_Device_Scanner_02_QRCode_接收檢查.SetComment("PLC_Scanner_02_QRCode_接收檢查");
        //        PLC_Device_Scanner_02_QRCode_接收資料_OK.SetComment("PLC_Device_Scanner_02_QRCode_接收資料_OK");
        //        PLC_Device_Scanner_02_QRCode_接收檢查.Bool = false;
        //        cnt_Program_Scanner_02_QRCode_接收檢查 = 65535;
        //    }
        //    if (cnt_Program_Scanner_02_QRCode_接收檢查 == 65535) cnt_Program_Scanner_02_QRCode_接收檢查 = 1;
        //    if (cnt_Program_Scanner_02_QRCode_接收檢查 == 1) cnt_Program_Scanner_02_QRCode_接收檢查_檢查按下(ref cnt_Program_Scanner_02_QRCode_接收檢查);
        //    if (cnt_Program_Scanner_02_QRCode_接收檢查 == 2) cnt_Program_Scanner_02_QRCode_接收檢查_初始化(ref cnt_Program_Scanner_02_QRCode_接收檢查);
        //    if (cnt_Program_Scanner_02_QRCode_接收檢查 == 3) cnt_Program_Scanner_02_QRCode_接收檢查_開始接收(ref cnt_Program_Scanner_02_QRCode_接收檢查);
        //    if (cnt_Program_Scanner_02_QRCode_接收檢查 == 4) cnt_Program_Scanner_02_QRCode_接收檢查 = 65500;
        //    if (cnt_Program_Scanner_02_QRCode_接收檢查 > 1) cnt_Program_Scanner_02_QRCode_接收檢查_檢查放開(ref cnt_Program_Scanner_02_QRCode_接收檢查);

        //    if (cnt_Program_Scanner_02_QRCode_接收檢查 == 65500)
        //    {
        //        this.MySerialPort_Scanner02.ClearReadByte();
        //        PLC_Device_Scanner_02_QRCode_接收檢查.Bool = false;
        //        PLC_Device_Scanner_02_QRCode_接收旗標.Bool = false;
        //        cnt_Program_Scanner_02_QRCode_接收檢查 = 65535;
        //    }
        //}
        //void cnt_Program_Scanner_02_QRCode_接收檢查_檢查按下(ref int cnt)
        //{
        //    if (PLC_Device_Scanner_02_QRCode_接收檢查.Bool)
        //    {
        //        this.MySerialPort_Scanner02.ClearReadByte();
        //        PLC_Device_Scanner_02_QRCode_接收旗標.Bool = false;
        //        PLC_Device_Scanner_02_QRCode_接收資料_OK.Bool = false;
        //        cnt++;
        //    }
        //}
        //void cnt_Program_Scanner_02_QRCode_接收檢查_檢查放開(ref int cnt)
        //{
        //    if (!PLC_Device_Scanner_02_QRCode_接收檢查.Bool) cnt = 65500;
        //}
        //void cnt_Program_Scanner_02_QRCode_接收檢查_初始化(ref int cnt)
        //{
        //    if (MySerialPort_Scanner02.ReadByte() != null)
        //    {
        //        for (int i = 0; i < this.Scanner_02_QRCode_Data.Length; i++)
        //        {
        //            this.Scanner_02_QRCode_Data[i] = "";
        //        }
        //        MyTimer_Scanner_02_QRCode_接收逾時時間.TickStop();
        //        MyTimer_Scanner_02_QRCode_接收逾時時間.StartTickTime(500);
        //        str_Scanner_02_QRCode = "";
        //        str_Scanner_02_QRCode_buf = "";
        //        cnt++;
        //    }
        //}
        //void cnt_Program_Scanner_02_QRCode_接收檢查_開始接收(ref int cnt)
        //{
        //    if (MyTimer_Scanner_02_QRCode_接收逾時時間.IsTimeOut())
        //    {
        //        this.MySerialPort_Scanner02.ClearReadByte();
        //        cnt = 65500;
        //        return;
        //    }
        //    str_Scanner_02_QRCode_buf = this.MySerialPort_Scanner02.ReadString();
        //    if (str_Scanner_02_QRCode_buf.Length - 2 > 0)
        //    {
        //        string last_str = str_Scanner_02_QRCode_buf.Substring(str_Scanner_02_QRCode_buf.Length - 2, 2);
        //        if (last_str == "\r\n")
        //        {
        //            Scanner_02_QRCode_Data = new string[new enum_Scanner_陣列內容().GetLength()];
        //            str_Scanner_02_QRCode = str_Scanner_02_QRCode_buf.Replace("\r\n", "");

        //            Console.Write($"Scanner 02 Data : {str_Scanner_02_QRCode_buf}\n");
        //            List<object[]> list_value = this.sqL_DataGridView_醫囑資料.SQL_GetAllRows(true);
        //            list_value = (from value in list_value
        //                          where value[(int)enum_醫囑資料.藥袋條碼].ObjectToString() == str_Scanner_02_QRCode || value[(int)enum_醫囑資料.藥袋條碼].ObjectToString() == str_Scanner_02_QRCode
        //                          select value).ToList();
        //            if (list_value.Count == 0)
        //            {
        //                Console.Write($"找無此藥單資料\n");
        //                cnt = 65500;
        //                return;
        //            }
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.藥品代碼] = list_value[0][(int)enum_醫囑資料.藥品碼].ObjectToString();
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.病人姓名] = list_value[0][(int)enum_醫囑資料.病人姓名].ObjectToString();
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.病歷號] = list_value[0][(int)enum_醫囑資料.病歷號].ObjectToString();
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.使用數量] = list_value[0][(int)enum_醫囑資料.交易量].ObjectToString();
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.開方日期] = list_value[0][(int)enum_醫囑資料.開方日期].ObjectToString().StringToDateTime().ToDateString();
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.開方時間] = list_value[0][(int)enum_醫囑資料.開方日期].ObjectToString().StringToDateTime().ToTimeString();

        //            List<object[]> list_藥品資料_藥檔資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetRows(enum_藥品資料_藥檔資料.藥品碼.GetEnumName(), Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.藥品代碼], false);
        //            if (list_藥品資料_藥檔資料.Count == 0)
        //            {
        //                Console.Write($"找無此藥品資料_藥檔資料 Code: { Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.藥品代碼]}\n");
        //                cnt = 65500;
        //                return;
        //            }
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.藥品英文名稱] = list_藥品資料_藥檔資料[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.藥品中文名稱] = list_藥品資料_藥檔資料[0][(int)enum_藥品資料_藥檔資料.藥品中文名稱].ObjectToString();
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.藥品學名] = list_藥品資料_藥檔資料[0][(int)enum_藥品資料_藥檔資料.藥品學名].ObjectToString();
        //            Scanner_02_QRCode_Data[(int)enum_Scanner_陣列內容.藥品單位] = list_藥品資料_藥檔資料[0][(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();

        //            for (int i = 0; i < new enum_Scanner_陣列內容().GetLength(); i++)
        //            {
        //                string Name = new enum_Scanner_陣列內容().GetEnumNames()[i];
        //                string Value = Scanner_02_QRCode_Data[i];
        //                Console.Write($"{Name} : {Value}\n");
        //            }

        //            PLC_Device_Scanner_02_QRCode_接收資料_OK.Bool = true;

        //            cnt++;
        //        }
        //    }


        //}
        //#endregion

        //#region PLC_Scanner_01_BarCode_接收檢查
        //PLC_Device PLC_Device_Scanner_01_BarCode_接收檢查 = new PLC_Device("S30300");
        //PLC_Device PLC_Device_Scanner_01_BarCode_接收資料_OK = new PLC_Device("S30301");
        //PLC_Device PLC_Device_Scanner_01_BarCode_接收旗標 = new PLC_Device("S30104");
        //string str_Scanner_01_BarCode = "";
        //string str_Scanner_01_BarCode_buf = "";
        //MyTimer MyTimer_Scanner_01_BarCode_接收逾時時間 = new MyTimer();
        //int cnt_Program_Scanner_01_BarCode_接收檢查 = 65534;
        //void sub_Program_Scanner_01_BarCode_接收檢查()
        //{
        //    if (cnt_Program_Scanner_01_BarCode_接收檢查 == 65534)
        //    {
        //        PLC_Device_Scanner_01_BarCode_接收檢查.SetComment("PLC_Scanner_01_BarCode_接收檢查");
        //        PLC_Device_Scanner_01_BarCode_接收資料_OK.SetComment("PLC_Device_Scanner_01_BarCode_接收資料_OK");
        //        PLC_Device_Scanner_01_BarCode_接收檢查.Bool = false;
        //        cnt_Program_Scanner_01_BarCode_接收檢查 = 65535;
        //    }
        //    if (cnt_Program_Scanner_01_BarCode_接收檢查 == 65535) cnt_Program_Scanner_01_BarCode_接收檢查 = 1;
        //    if (cnt_Program_Scanner_01_BarCode_接收檢查 == 1) cnt_Program_Scanner_01_BarCode_接收檢查_檢查按下(ref cnt_Program_Scanner_01_BarCode_接收檢查);
        //    if (cnt_Program_Scanner_01_BarCode_接收檢查 == 2) cnt_Program_Scanner_01_BarCode_接收檢查_初始化(ref cnt_Program_Scanner_01_BarCode_接收檢查);
        //    if (cnt_Program_Scanner_01_BarCode_接收檢查 == 3) cnt_Program_Scanner_01_BarCode_接收檢查_開始接收(ref cnt_Program_Scanner_01_BarCode_接收檢查);
        //    if (cnt_Program_Scanner_01_BarCode_接收檢查 == 4) cnt_Program_Scanner_01_BarCode_接收檢查 = 65500;
        //    if (cnt_Program_Scanner_01_BarCode_接收檢查 > 1) cnt_Program_Scanner_01_BarCode_接收檢查_檢查放開(ref cnt_Program_Scanner_01_BarCode_接收檢查);

        //    if (cnt_Program_Scanner_01_BarCode_接收檢查 == 65500)
        //    {
        //        this.MySerialPort_Scanner01.ClearReadByte();
        //        PLC_Device_Scanner_01_BarCode_接收檢查.Bool = false;
        //        PLC_Device_Scanner_01_BarCode_接收旗標.Bool = false;
        //        cnt_Program_Scanner_01_BarCode_接收檢查 = 65535;
        //    }
        //}
        //void cnt_Program_Scanner_01_BarCode_接收檢查_檢查按下(ref int cnt)
        //{
        //    if (PLC_Device_Scanner_01_BarCode_接收檢查.Bool)
        //    {
        //        this.MySerialPort_Scanner01.ClearReadByte();
        //        PLC_Device_Scanner_01_BarCode_接收旗標.Bool = false;
        //        PLC_Device_Scanner_01_BarCode_接收資料_OK.Bool = false;
        //        cnt++;
        //    }
        //}
        //void cnt_Program_Scanner_01_BarCode_接收檢查_檢查放開(ref int cnt)
        //{
        //    if (!PLC_Device_Scanner_01_BarCode_接收檢查.Bool) cnt = 65500;
        //}
        //void cnt_Program_Scanner_01_BarCode_接收檢查_初始化(ref int cnt)
        //{
        //    if (PLC_Device_Scanner_01_BarCode_接收旗標.Bool)
        //    {
        //        MyTimer_Scanner_01_BarCode_接收逾時時間.TickStop();
        //        MyTimer_Scanner_01_BarCode_接收逾時時間.StartTickTime(500);
        //        str_Scanner_01_BarCode = "";
        //        str_Scanner_01_BarCode_buf = "";
        //        cnt++;
        //    }
        //}
        //void cnt_Program_Scanner_01_BarCode_接收檢查_開始接收(ref int cnt)
        //{
        //    if (MyTimer_Scanner_01_BarCode_接收逾時時間.IsTimeOut())
        //    {
        //        // this.plC_SerialPort_Scanner01.ReadBufferClear();
        //        cnt = 65500;
        //        return;
        //    }
        //    str_Scanner_01_BarCode_buf = this.MySerialPort_Scanner01.ReadString();
        //    if (str_Scanner_01_BarCode_buf.Length - 2 > 0)
        //    {
        //        string last_str = str_Scanner_01_BarCode_buf.Substring(str_Scanner_01_BarCode_buf.Length - 2, 2);
        //        if (last_str == "\r\n")
        //        {
        //            str_Scanner_01_BarCode = str_Scanner_01_BarCode_buf.Replace("\r\n", "");

        //            string[] str_array = myConvert.分解分隔號字串(str_Scanner_01_BarCode, "~", StringSplitOptions.None);
        //            str_Scanner_01_BarCode = str_array[(int)enum_Scanner_陣列內容.藥品代碼].ObjectToString().ToString();
         
        //            //  this.plC_SerialPort_Scanner01.ReadBufferClear();

        //            PLC_Device_Scanner_01_BarCode_接收資料_OK.Bool = true;



        //            cnt++;
        //        }
        //    }


        //}

        //#endregion
        //#region PLC_Scanner_02_BarCode_接收檢查
        //PLC_Device PLC_Device_Scanner_02_BarCode_接收檢查 = new PLC_Device("S30310");
        //PLC_Device PLC_Device_Scanner_02_BarCode_接收資料_OK = new PLC_Device("S30311");
        //PLC_Device PLC_Device_Scanner_02_BarCode_接收旗標 = new PLC_Device("S30204");
        //string str_Scanner_02_BarCode = "";
        //string str_Scanner_02_BarCode_buf = "";
        //MyTimer MyTimer_Scanner_02_BarCode_接收逾時時間 = new MyTimer();
        //int cnt_Program_Scanner_02_BarCode_接收檢查 = 65534;
        //void sub_Program_Scanner_02_BarCode_接收檢查()
        //{
        //    if (cnt_Program_Scanner_02_BarCode_接收檢查 == 65534)
        //    {
        //        PLC_Device_Scanner_02_BarCode_接收檢查.SetComment("PLC_Scanner_02_BarCode_接收檢查");
        //        PLC_Device_Scanner_02_BarCode_接收資料_OK.SetComment("PLC_Device_Scanner_02_BarCode_接收資料_OK");
        //        PLC_Device_Scanner_02_BarCode_接收檢查.Bool = false;
        //        cnt_Program_Scanner_02_BarCode_接收檢查 = 65535;
        //    }
        //    if (cnt_Program_Scanner_02_BarCode_接收檢查 == 65535) cnt_Program_Scanner_02_BarCode_接收檢查 = 1;
        //    if (cnt_Program_Scanner_02_BarCode_接收檢查 == 1) cnt_Program_Scanner_02_BarCode_接收檢查_檢查按下(ref cnt_Program_Scanner_02_BarCode_接收檢查);
        //    if (cnt_Program_Scanner_02_BarCode_接收檢查 == 2) cnt_Program_Scanner_02_BarCode_接收檢查_初始化(ref cnt_Program_Scanner_02_BarCode_接收檢查);
        //    if (cnt_Program_Scanner_02_BarCode_接收檢查 == 3) cnt_Program_Scanner_02_BarCode_接收檢查_開始接收(ref cnt_Program_Scanner_02_BarCode_接收檢查);
        //    if (cnt_Program_Scanner_02_BarCode_接收檢查 == 4) cnt_Program_Scanner_02_BarCode_接收檢查 = 65500;
        //    if (cnt_Program_Scanner_02_BarCode_接收檢查 > 1) cnt_Program_Scanner_02_BarCode_接收檢查_檢查放開(ref cnt_Program_Scanner_02_BarCode_接收檢查);

        //    if (cnt_Program_Scanner_02_BarCode_接收檢查 == 65500)
        //    {
        //        this.MySerialPort_Scanner02.ClearReadByte();
        //        PLC_Device_Scanner_02_BarCode_接收檢查.Bool = false;
        //        PLC_Device_Scanner_02_BarCode_接收旗標.Bool = false;
        //        cnt_Program_Scanner_02_BarCode_接收檢查 = 65535;
        //    }
        //}
        //void cnt_Program_Scanner_02_BarCode_接收檢查_檢查按下(ref int cnt)
        //{
        //    if (PLC_Device_Scanner_02_BarCode_接收檢查.Bool)
        //    {
        //        this.MySerialPort_Scanner02.ClearReadByte();
        //        PLC_Device_Scanner_02_BarCode_接收旗標.Bool = false;
        //        PLC_Device_Scanner_02_BarCode_接收資料_OK.Bool = false;
        //        cnt++;
        //    }
        //}
        //void cnt_Program_Scanner_02_BarCode_接收檢查_檢查放開(ref int cnt)
        //{
        //    if (!PLC_Device_Scanner_02_BarCode_接收檢查.Bool) cnt = 65500;
        //}
        //void cnt_Program_Scanner_02_BarCode_接收檢查_初始化(ref int cnt)
        //{
        //    if (PLC_Device_Scanner_02_BarCode_接收旗標.Bool)
        //    {
        //        MyTimer_Scanner_02_BarCode_接收逾時時間.TickStop();
        //        MyTimer_Scanner_02_BarCode_接收逾時時間.StartTickTime(500);
        //        str_Scanner_02_BarCode = "";
        //        str_Scanner_02_BarCode_buf = "";
        //        cnt++;
        //    }
        //}
        //void cnt_Program_Scanner_02_BarCode_接收檢查_開始接收(ref int cnt)
        //{
        //    if (MyTimer_Scanner_02_BarCode_接收逾時時間.IsTimeOut())
        //    {
        //        this.MySerialPort_Scanner02.ClearReadByte();
        //        cnt = 65500;
        //        return;
        //    }
        //    str_Scanner_02_BarCode_buf = this.MySerialPort_Scanner02.ReadString();
        //    if (str_Scanner_02_BarCode_buf.Length - 2 > 0)
        //    {
        //        string last_str = str_Scanner_02_BarCode_buf.Substring(str_Scanner_02_BarCode_buf.Length - 2, 2);
        //        if (last_str == "\r\n")
        //        {
        //            str_Scanner_02_BarCode = str_Scanner_02_BarCode_buf.Replace("\r\n", "");

        //            string[] str_array = myConvert.分解分隔號字串(str_Scanner_02_BarCode, "~", StringSplitOptions.None);
        //            str_Scanner_02_BarCode = str_array[(int)enum_Scanner_陣列內容.藥品代碼].ObjectToString().ToString();
    
        //            //  this.plC_SerialPort_Scanner01.ReadBufferClear();

        //            PLC_Device_Scanner_02_BarCode_接收資料_OK.Bool = true;



        //            cnt++;
        //        }
        //    }


        //}

        //#endregion
    }
}
