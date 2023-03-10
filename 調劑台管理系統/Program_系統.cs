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

using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace 調劑台管理系統
{
    public partial class Form1 : Form
    {
    
        #region Locker_Index_Table
        enum enum_Locker_Index_Table
        {
            GUID,
            IP,
            Num,
            輸入位置,
            輸入狀態,
            輸出位置,
            輸出狀態,
            Master_GUID,
            Slave_GUID,
            Device_GUID,
        }
        enum enum_Locker_Index_Table_匯出
        {
            IP,
            Num,
            輸入位置,
            輸出位置,
        }
        enum enum_Locker_Index_Table_匯入
        {
            IP,
            Num,
            輸入位置,
            輸出位置,
        }
        public enum ContextMenuStrip_Locker_Index_Table
        {
            匯出,
            匯入,
            刷新,
            刪除選取資料,
        }
        #endregion
        #region 特殊輸出表
        public enum enum_特殊輸出表
        {
            GUID,
            Name,
            IP,
            Num,
            輸出位置,
            輸出狀態
        }
        enum enum_特殊輸出表_匯出
        {
            IP,
            Name,
            Num,
            輸出位置,
        }
        enum enum_特殊輸出表_匯入
        {
            IP,
            Name,
            Num,
            輸出位置,
        }
        public enum ContextMenuStrip_特殊輸出表
        {
            匯出,
            匯入,
            刷新,
            刪除選取資料,
        }
        #endregion



        private void Program_系統_Init()
        {
      



            this.sqL_DataGridView_Locker_Index_Table.Init();
            if (!this.sqL_DataGridView_Locker_Index_Table.SQL_IsTableCreat())
            {
                this.sqL_DataGridView_Locker_Index_Table.SQL_CreateTable();
            }
           
            this.sqL_DataGridView_Locker_Index_Table.MouseDown += SqL_DataGridView_Locker_Index_Table_MouseDown;
            this.sqL_DataGridView_Locker_Index_Table.DataGridRowsChangeEvent += SqL_DataGridView_Locker_Index_Table_DataGridRowsChangeEvent;


            this.sqL_DataGridView_特殊輸出表.Init();
            if (!this.sqL_DataGridView_特殊輸出表.SQL_IsTableCreat()) this.sqL_DataGridView_特殊輸出表.SQL_CreateTable();
            this.sqL_DataGridView_特殊輸出表.MouseDown += SqL_DataGridView_特殊輸出表_MouseDown;


            this.plC_UI_Init.Add_Method(this.sub_Program_系統);
        }



        private bool flag_sub_Program_系統_設定06 = false;
        private bool flag_sub_Program_系統_設定03 = false;
        private void sub_Program_系統()
        {
            if (plC_ScreenPage_Main.PageText == "系統" && plC_ScreenPage_系統.PageText == "設定03")
            {
                if (!flag_sub_Program_系統_設定03)
                {
                    this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(true);
                    this.sqL_DataGridView_特殊輸出表.SQL_GetAllRows(true);
                    flag_sub_Program_系統_設定03 = true;
                }
            }
            else
            {
                flag_sub_Program_系統_設定03 = false;
            }


            if (plC_ScreenPage_Main.PageText == "系統" && plC_ScreenPage_系統.PageText == "設定06")
            {
                if (!flag_sub_Program_系統_設定06)
                {
                    this.sqL_DataGridView_醫囑資料.SQL_GetAllRows(true);
                    flag_sub_Program_系統_設定06 = true;
                }
            }
            else
            {
                flag_sub_Program_系統_設定06 = false;
            }

            sub_Program_醫囑資料_掃碼();
        }

        #region PLC_醫囑資料_掃碼
        PLC_Device PLC_Device_醫囑資料_掃碼 = new PLC_Device("S300");
        PLC_Device PLC_Device_醫囑資料_掃碼_OK = new PLC_Device("S301");
        int cnt_Program_醫囑資料_掃碼 = 65534;
        void sub_Program_醫囑資料_掃碼()
        {
            if (plC_ScreenPage_Main.PageText == "系統" && plC_ScreenPage_系統.PageText == "設定06")
            {
                PLC_Device_醫囑資料_掃碼.Bool = true;
            }
            else
            {
                PLC_Device_醫囑資料_掃碼.Bool = false;
            }

            if (cnt_Program_醫囑資料_掃碼 == 65534)
            {
                PLC_Device_醫囑資料_掃碼.SetComment("PLC_醫囑資料_掃碼");
                PLC_Device_醫囑資料_掃碼_OK.SetComment("PLC_醫囑資料_掃碼_OK");
                PLC_Device_醫囑資料_掃碼.Bool = false;
                cnt_Program_醫囑資料_掃碼 = 65535;
            }
            if (cnt_Program_醫囑資料_掃碼 == 65535) cnt_Program_醫囑資料_掃碼 = 1;
            if (cnt_Program_醫囑資料_掃碼 == 1) cnt_Program_醫囑資料_掃碼_檢查按下(ref cnt_Program_醫囑資料_掃碼);
            if (cnt_Program_醫囑資料_掃碼 == 2) cnt_Program_醫囑資料_掃碼_初始化(ref cnt_Program_醫囑資料_掃碼);
            if (cnt_Program_醫囑資料_掃碼 == 3) cnt_Program_醫囑資料_掃碼 = 65500;
            if (cnt_Program_醫囑資料_掃碼 > 1) cnt_Program_醫囑資料_掃碼_檢查放開(ref cnt_Program_醫囑資料_掃碼);

            if (cnt_Program_醫囑資料_掃碼 == 65500)
            {
                PLC_Device_醫囑資料_掃碼.Bool = false;
                PLC_Device_醫囑資料_掃碼_OK.Bool = false;
                cnt_Program_醫囑資料_掃碼 = 65535;
            }
        }
        void cnt_Program_醫囑資料_掃碼_檢查按下(ref int cnt)
        {
            if (PLC_Device_醫囑資料_掃碼.Bool) cnt++;
        }
        void cnt_Program_醫囑資料_掃碼_檢查放開(ref int cnt)
        {
            if (!PLC_Device_醫囑資料_掃碼.Bool) cnt = 65500;
        }
        void cnt_Program_醫囑資料_掃碼_初始化(ref int cnt)
        {
          
            cnt++;
        }



























































        #endregion

        #region Event
        private void SqL_DataGridView_特殊輸出表_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) return;
            Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new ContextMenuStrip_特殊輸出表());
            if (dialog_ContextMenuStrip.ShowDialog() != DialogResult.Yes) return;
            if (dialog_ContextMenuStrip.Value == ContextMenuStrip_特殊輸出表.匯出.GetEnumName())
            {
                saveFileDialog_SaveExcel.OverwritePrompt = false;
                if (saveFileDialog_SaveExcel.ShowDialog(this) == DialogResult.OK)
                {
                    DataTable datatable = new DataTable();
                    datatable = sqL_DataGridView_特殊輸出表.GetDataTable();
                    datatable = datatable.ReorderTable(new enum_特殊輸出表_匯出());
                    CSVHelper.SaveFile(datatable, saveFileDialog_SaveExcel.FileName);
                    MyMessageBox.ShowDialog("匯出完成!");
                }
            }
            else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_特殊輸出表.匯入.GetEnumName())
            {
                if (openFileDialog_LoadExcel.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dataTable = new DataTable();
                    CSVHelper.LoadFile(this.openFileDialog_LoadExcel.FileName, 0, dataTable);
                    DataTable datatable_buf = dataTable.ReorderTable(new enum_特殊輸出表_匯入());
                    if (datatable_buf == null)
                    {
                        MyMessageBox.ShowDialog("匯入檔案,資料錯誤!");
                        return;
                    }
                    List<object[]> list_LoadValue = datatable_buf.DataTableToRowList();
                    List<object[]> list_SQL_Value = this.sqL_DataGridView_特殊輸出表.SQL_GetAllRows(false);
                    List<object[]> list_Add = new List<object[]>();
                    List<object[]> list_Delete_ColumnName = new List<object[]>();
                    List<object[]> list_Delete_SerchValue = new List<object[]>();
                    List<string> list_Replace_SerchValue = new List<string>();
                    List<object[]> list_Replace_Value = new List<object[]>();
                    List<object[]> list_SQL_Value_buf = new List<object[]>();

                    for (int i = 0; i < list_LoadValue.Count; i++)
                    {
                        object[] value_load = list_LoadValue[i];
                        value_load = value_load.CopyRow(new enum_特殊輸出表_匯入(), new enum_特殊輸出表());
                        list_SQL_Value_buf = list_SQL_Value.GetRows((int)enum_特殊輸出表.IP, value_load[(int)enum_特殊輸出表.IP].ObjectToString());
                        value_load[(int)enum_特殊輸出表.輸出狀態] = false.ToString();
                        if (list_SQL_Value_buf.Count > 0)
                        {
                            object[] value_SQL = list_SQL_Value_buf[0];
                            bool flag_Equal = value_load.IsEqual(value_SQL);
                            if (!flag_Equal)
                            {
                                list_Replace_Value.Add(value_load);
                            }
                        }
                        else
                        {
                            value_load[(int)enum_特殊輸出表.GUID] = Guid.NewGuid().ToString();
                            list_Add.Add(value_load);
                        }
                    }
                    this.sqL_DataGridView_特殊輸出表.SQL_AddRows(list_Add, false);
                    this.sqL_DataGridView_特殊輸出表.SQL_ReplaceExtra(list_Replace_Value, false);
                    this.sqL_DataGridView_特殊輸出表.SQL_GetAllRows(true);
                    this.Cursor = Cursors.Default;
                }
                this.Cursor = Cursors.Default;
                MyMessageBox.ShowDialog("匯入完成!");
            }
            else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_特殊輸出表.刪除選取資料.GetEnumName())
            {
                if (MyMessageBox.ShowDialog("是否刪除選取資料", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    List<object[]> list_value = this.sqL_DataGridView_特殊輸出表.Get_All_Select_RowsValues();
                    List<object> list_delete_serchValue = new List<object>();
                    for (int i = 0; i < list_value.Count; i++)
                    {
                        string GUID = list_value[i][(int)enum_藥品資料_藥檔資料.GUID].ObjectToString();
                        list_delete_serchValue.Add(GUID);
                    }
                    this.sqL_DataGridView_特殊輸出表.SQL_DeleteExtra(list_delete_serchValue, true);
                    this.Cursor = Cursors.Default;
                }
            }
            else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_特殊輸出表.刷新.GetEnumName())
            {
                this.sqL_DataGridView_特殊輸出表.SQL_GetAllRows(true);
            }
        }
        private void SqL_DataGridView_Locker_Index_Table_DataGridRowsChangeEvent(List<object[]> RowsList)
        {
            RowsList.Sort(new ICP_Locker_Index_Table());
        }
        private void SqL_DataGridView_Locker_Index_Table_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) return;
            Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new ContextMenuStrip_Locker_Index_Table());
            if (dialog_ContextMenuStrip.ShowDialog() != DialogResult.Yes) return;
            if (dialog_ContextMenuStrip.Value == ContextMenuStrip_Locker_Index_Table.匯出.GetEnumName())
            {
                saveFileDialog_SaveExcel.OverwritePrompt = false;
                if (saveFileDialog_SaveExcel.ShowDialog(this) == DialogResult.OK)
                {
                    DataTable datatable = new DataTable();
                    datatable = sqL_DataGridView_Locker_Index_Table.GetDataTable();
                    datatable = datatable.ReorderTable(new enum_Locker_Index_Table_匯出());
                    CSVHelper.SaveFile(datatable, saveFileDialog_SaveExcel.FileName);
                    MyMessageBox.ShowDialog("匯出完成!");
                }
            }
            else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_Locker_Index_Table.匯入.GetEnumName())
            {
                if (openFileDialog_LoadExcel.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dataTable = new DataTable();
                    CSVHelper.LoadFile(this.openFileDialog_LoadExcel.FileName, 0, dataTable);
                    DataTable datatable_buf = dataTable.ReorderTable(new enum_Locker_Index_Table_匯入());
                    if (datatable_buf == null)
                    {
                        MyMessageBox.ShowDialog("匯入檔案,資料錯誤!");
                        return;
                    }
                    List<object[]> list_LoadValue = datatable_buf.DataTableToRowList();
                    List<object[]> list_SQL_Value = this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(false);
                    List<object[]> list_Add = new List<object[]>();
                    List<object[]> list_Delete_ColumnName = new List<object[]>();
                    List<object[]> list_Delete_SerchValue = new List<object[]>();
                    List<string> list_Replace_SerchValue = new List<string>();
                    List<object[]> list_Replace_Value = new List<object[]>();
                    List<object[]> list_SQL_Value_buf = new List<object[]>();

                    for (int i = 0; i < list_LoadValue.Count; i++)
                    {
                        object[] value_load = list_LoadValue[i];
                        value_load = value_load.CopyRow(new enum_Locker_Index_Table_匯入(), new enum_Locker_Index_Table());
                        list_SQL_Value_buf = list_SQL_Value.GetRows((int)enum_Locker_Index_Table.IP, value_load[(int)enum_Locker_Index_Table.IP].ObjectToString());
                        value_load[(int)enum_Locker_Index_Table.輸出狀態] = false.ToString();
                        if (list_SQL_Value_buf.Count > 0)
                        {
                            object[] value_SQL = list_SQL_Value_buf[0];
                            bool flag_Equal = value_load.IsEqual(value_SQL);
                            if (!flag_Equal)
                            {
                                list_Replace_Value.Add(value_load);
                            }
                        }
                        else
                        {
                            value_load[(int)enum_Locker_Index_Table.GUID] = Guid.NewGuid().ToString();
                            list_Add.Add(value_load);
                        }
                    }
                    this.sqL_DataGridView_Locker_Index_Table.SQL_AddRows(list_Add, false);
                    this.sqL_DataGridView_Locker_Index_Table.SQL_ReplaceExtra(list_Replace_Value, false);
                    this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(true);
                    this.Cursor = Cursors.Default;
                }
                this.Cursor = Cursors.Default;
                MyMessageBox.ShowDialog("匯入完成!");
            }
            else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_Locker_Index_Table.刪除選取資料.GetEnumName())
            {
                if (MyMessageBox.ShowDialog("是否刪除選取資料", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    List<object[]> list_value = this.sqL_DataGridView_Locker_Index_Table.Get_All_Select_RowsValues();
                    List<object> list_delete_serchValue = new List<object>();
                    for (int i = 0; i < list_value.Count; i++)
                    {
                        string GUID = list_value[i][(int)enum_特殊輸出表.GUID].ObjectToString();
                        list_delete_serchValue.Add(GUID);
                    }
                    this.sqL_DataGridView_Locker_Index_Table.SQL_DeleteExtra(list_delete_serchValue, true);
                    this.Cursor = Cursors.Default;
                }
            }
            else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_Locker_Index_Table.刷新.GetEnumName())
            {
                this.sqL_DataGridView_Locker_Index_Table.SQL_GetAllRows(true);
            }
        }
     
        #endregion

        private class ICP_Locker_Index_Table : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string IP_0 = x[(int)enum_Locker_Index_Table.IP].ObjectToString();
                string IP_1 = y[(int)enum_Locker_Index_Table.IP].ObjectToString();
                string Num_0 = x[(int)enum_Locker_Index_Table.Num].ObjectToString();
                string Num_1 = y[(int)enum_Locker_Index_Table.Num].ObjectToString();
                string[] IP_0_Array = IP_0.Split('.');
                string[] IP_1_Array = IP_1.Split('.');
                if (!IP_0.Check_IP_Adress()) return 0;
                if (!IP_1.Check_IP_Adress()) return 0;
                for (int i = 0; i < 4; i++)
                {
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];
                    if (IP_0_Array[i].Length < 3) IP_0_Array[i] = "0" + IP_0_Array[i];

                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];
                    if (IP_1_Array[i].Length < 3) IP_1_Array[i] = "0" + IP_1_Array[i];

                    IP_0 += IP_0_Array[i];
                    IP_1 += IP_1_Array[i];
                }
                int cmp = IP_0_Array[2].CompareTo(IP_1_Array[2]);
                if (cmp > 0)
                {
                    return 1;
                }
                else if (cmp < 0)
                {
                    return -1;
                }
                else if (cmp == 0)
                {
                    cmp = IP_0_Array[3].CompareTo(IP_1_Array[3]);
                    if (cmp > 0)
                    {
                        return 1;
                    }
                    else if (cmp < 0)
                    {
                        return -1;
                    }
                    else if (cmp == 0)
                    {
                        return Num_0.CompareTo(Num_1);
                    }
                }

                return 0;

            }
        }
    }
}
