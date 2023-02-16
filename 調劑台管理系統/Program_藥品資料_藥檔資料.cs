﻿using System;
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
using H_Pannel_lib;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承

namespace 調劑台管理系統
{
    public enum ContextMenuStrip_藥品資料_藥檔資料
    {
        [Description("S39007")]
        匯出,
        [Description("S39007")]
        匯入,
        [Description("S39007")]
        匯出選取資料,
        [Description("S39007")]
        登錄資料,
        [Description("S39007")]
        刪除選取資料,
        [Description("S39007")]
        設定安全庫存,
        [Description("S39021")]
        藥品群組設定,
    }

    public enum enum_藥品資料_藥檔資料
    {
        GUID,
        藥品碼,
        藥品中文名稱,
        藥品名稱,
        藥品學名,
        藥品群組,
        健保碼,
        藥品條碼,
        包裝單位,
        庫存,
        安全庫存,
        圖片網址,
        警訊藥品,
    }
    public enum enum_藥品資料_藥檔資料_匯入
    {
        藥品碼,
        藥品中文名稱,
        藥品名稱,
        藥品學名,
        藥品群組,
        健保碼,
        藥品條碼,
        包裝單位,
        庫存,
        安全庫存,
        警訊藥品,
    }
    public enum enum_藥品資料_藥檔資料_匯出
    {
        藥品碼,
        藥品中文名稱,
        藥品名稱,
        藥品學名,
        藥品群組,
        健保碼,
        藥品條碼,
        包裝單位,
        庫存,
        安全庫存,
        警訊藥品,
    }
    public enum enum_藥品群組
    {
        GUID,
        群組序號,
        群組名稱,
    }

    public partial class Form1 : Form
    {
     
        private void Program_藥品資料_藥檔資料_Init()
        {
            this.sqL_DataGridView_藥品群組.Init();
            if (!this.sqL_DataGridView_藥品群組.SQL_IsTableCreat()) this.sqL_DataGridView_藥品群組.SQL_CreateTable();
            Function_藥品群組_初始化表單();
            this.sqL_DataGridView_藥品群組.DataGridRowsChangeEvent += SqL_DataGridView_藥品群組_DataGridRowsChangeEvent;
            this.sqL_DataGridView_藥品群組.RowEnterEvent += SqL_DataGridView_藥品群組_RowEnterEvent;
            this.sqL_DataGridView_藥品群組.SQL_GetAllRows(false);

            this.rJ_ComboBox_藥品資料_藥檔資料_藥品群組.Enter += RJ_ComboBox_藥品資料_藥檔資料_藥品群組_Enter;
            this.rJ_ComboBox_藥品資料_藥檔資料_資料查詢_藥品群組.Enter += RJ_ComboBox_藥品資料_藥檔資料_資料查詢_藥品群組_Enter;
            this.rJ_TextBox_藥品群組_群組名稱.KeyPress += RJ_TextBox_藥品群組_群組名稱_KeyPress;

            this.textBox_藥品資料_藥檔資料_資料查詢_藥品條碼.KeyPress += TextBox_藥品資料_藥檔資料_資料查詢_藥品條碼_KeyPress;

            this.sqL_DataGridView_藥品資料_藥檔資料.Init();
            if (!this.sqL_DataGridView_藥品資料_藥檔資料.SQL_IsTableCreat())
            {
                this.sqL_DataGridView_藥品資料_藥檔資料.SQL_CreateTable();
            }
            this.sqL_DataGridView_藥品資料_藥檔資料.RowEnterEvent += SqL_DataGridView_藥品資料_藥檔資料_RowEnterEvent;
            this.sqL_DataGridView_藥品資料_藥檔資料.RowDoubleClickEvent += SqL_DataGridView_藥品資料_藥檔資料_RowDoubleClickEvent;
            this.sqL_DataGridView_藥品資料_藥檔資料.MouseDown += SqL_DataGridView_藥品資料_藥檔資料_MouseDown;
            this.sqL_DataGridView_藥品資料_藥檔資料.DataGridRefreshEvent += sqL_DataGridView_藥品資料_藥檔資料_DataGridRefreshEvent;
            this.sqL_DataGridView_藥品資料_藥檔資料.DataGridRowsChangeEvent += SqL_DataGridView_藥品資料_藥檔資料_DataGridRowsChangeEvent;
           
            this.comboBox_藥品資料_藥檔資料_警訊藥品.SelectedIndex = 0;

            this.plC_RJ_Button_藥品資料_藥檔資料_資料查詢.MouseDownEvent += PlC_RJ_Button_藥品資料_藥檔資料_資料查詢_MouseDownEvent;

            this.plC_RJ_Button_藥品資料_匯入.MouseDownEvent += PlC_RJ_Button_藥品資料_匯入_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_匯出.MouseDownEvent += PlC_RJ_Button_藥品資料_匯出_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_登錄.MouseDownEvent += PlC_RJ_Button_藥品資料_登錄_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_刪除.MouseDownEvent += PlC_RJ_Button_藥品資料_刪除_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_顯示有儲位藥品.MouseDownEvent += PlC_RJ_Button_藥品資料_顯示有儲位藥品_MouseDownEvent;
            this.plC_RJ_Button_藥品資料_藥檔資料_檢查藥檔.MouseDownEvent += PlC_RJ_Button_藥品資料_藥檔資料_檢查藥檔_MouseDownEvent;

            this.plC_RJ_Button_藥品群組_登錄.MouseDownEvent += PlC_RJ_Button_藥品群組_登錄_MouseDownEvent;

            this.plC_UI_Init.Add_Method(this.sub_Program_藥品資料_藥檔資料);
        }

       

        bool flag_藥品資料_藥檔資料_頁面更新 = false;
        private void sub_Program_藥品資料_藥檔資料()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥品資料" && this.plC_ScreenPage_藥品資料_藥檔資料.PageText == "藥檔資料")
            {
                if (!this.flag_藥品資料_藥檔資料_頁面更新)
                {
                    this.RJ_ComboBox_藥品資料_藥檔資料_藥品群組_Enter(null, null);
                    this.RJ_ComboBox_藥品資料_藥檔資料_資料查詢_藥品群組_Enter(null, null);
                    this.Function_從SQL取得儲位到本地資料();
                    this.sqL_DataGridView_藥品群組.SQL_GetAllRows(true);
                    this.flag_藥品資料_藥檔資料_頁面更新 = true;
                }
            }
            else
            {
                this.flag_藥品資料_藥檔資料_頁面更新 = false;
            }
        }

        #region Function
        #region 藥品群組
       
        private void RJ_TextBox_藥品群組_群組名稱_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                string 序號 = rJ_TextBox_藥品群組_群組序號.Text;
                List<object[]> list_value = sqL_DataGridView_藥品群組.SQL_GetAllRows(false);
                list_value = list_value.GetRows((int)enum_藥品群組.群組序號, 序號);
                if (list_value.Count > 0)
                {
                    list_value[0][(int)enum_藥品群組.群組名稱] = rJ_TextBox_藥品群組_群組名稱.Text;
                    sqL_DataGridView_藥品群組.SQL_ReplaceExtra(list_value, true);
                }
                sqL_DataGridView_藥品資料_藥檔資料.RefreshGrid();
            }
        }
        private void Function_藥品群組_初始化表單()
        {
            List<object[]> list_value = sqL_DataGridView_藥品群組.SQL_GetAllRows(false);
            List<object[]> list_value_buf = new List<object[]>();
            List<object[]> list_Add = new List<object[]>();
            List<string[]> list_Replace_SerchValue = new List<string[]>();
            List<object[]> list_Replace_Value = new List<object[]>();
            List<object[]> list_Delete_ColumnName = new List<object[]>();
            List<object[]> list_Delete_SerchValue = new List<object[]>();
            for (int i = 0; i < list_value.Count; i++)
            {
                int index = list_value[i][(int)enum_藥品群組.群組序號].StringToInt32();
                if (index <= 0 || index > 20)
                {
                    list_Delete_ColumnName.Add(new string[] { enum_藥品群組.GUID.GetEnumName() });
                    list_Delete_SerchValue.Add(new string[] { list_value[i][(int)enum_藥品群組.GUID].ObjectToString() });
                }
            }
            for (int i = 1; i <= 20; i++)
            {
                list_value_buf = list_value.GetRows((int)enum_藥品群組.群組序號, i.ToString("00"));
                if (list_value_buf.Count == 0)
                {
                    object[] value = new object[new enum_藥品群組().GetEnumNames().Length];
                    value[(int)enum_藥品群組.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_藥品群組.群組序號] = i.ToString("00");
                    list_Add.Add(value);
                }
            }
            sqL_DataGridView_藥品群組.SQL_DeleteExtra(list_Delete_ColumnName, list_Delete_SerchValue, false);
            sqL_DataGridView_藥品群組.SQL_AddRows(list_Add, false);

        }
        private void Finction_藥品群組_序號轉名稱(List<object[]> RowsList, int Enum)
        {
            List<object[]> list_藥品群組 = sqL_DataGridView_藥品群組.SQL_GetAllRows(false);
            List<object[]> list_藥品群組_buf = new List<object[]>();
            string 群組序號 = "";
            for (int i = 0; i < RowsList.Count; i++)
            {
                群組序號 = RowsList[i][Enum].ObjectToString();
                list_藥品群組_buf = list_藥品群組.GetRows((int)enum_藥品群組.群組序號, 群組序號);
                if (list_藥品群組_buf.Count > 0)
                {
                    RowsList[i][Enum] = list_藥品群組_buf[0][(int)enum_藥品群組.群組名稱];
                }
            }
        }
        private void Finction_藥品群組_名稱轉序號(object[] value, int Enum)
        {
            List<object[]> RowsList = new List<object[]>();
            RowsList.Add(value);
            Finction_藥品群組_名稱轉序號(RowsList, Enum);
        }
        private void Finction_藥品群組_名稱轉序號(List<object[]> RowsList, int Enum)
        {
            List<object[]> list_藥品群組 = sqL_DataGridView_藥品群組.SQL_GetAllRows(false);
            List<object[]> list_藥品群組_buf = new List<object[]>();
            string 群組名稱 = "";
            for (int i = 0; i < RowsList.Count; i++)
            {
                群組名稱 = RowsList[i][Enum].ObjectToString();
                list_藥品群組_buf = list_藥品群組.GetRows((int)enum_藥品群組.群組名稱, 群組名稱);
                if (list_藥品群組_buf.Count > 0)
                {
                    RowsList[i][Enum] = list_藥品群組_buf[0][(int)enum_藥品群組.群組序號];
                }
            }
        }
        private string[] Function_藥品群組_取得選單(bool spaceEnable)
        {
            List<string> list_data = new List<string>();
            List<object[]> list_藥品群組 = sqL_DataGridView_藥品群組.SQL_GetAllRows(false);
            list_藥品群組.Sort(new Icp_藥品群組());
            string 序號 = "00";
            string 名稱 = "預設空白";
            if(spaceEnable) list_data.Add($"{序號}. {名稱}");

            for (int i = 0; i < list_藥品群組.Count; i++)
            {
                序號 = list_藥品群組[i][(int)enum_藥品群組.群組序號].ObjectToString();
                名稱 = list_藥品群組[i][(int)enum_藥品群組.群組名稱].ObjectToString();
                list_data.Add($"{序號}. {名稱}");
            }
            return list_data.ToArray();
        }
        #endregion
    
        public string Function_藥品資料_藥檔資料_從藥品條碼取得藥品碼(string 藥品條碼)
        {
            string str = null;
            List<object[]> list_obj = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetRows(enum_藥品資料_藥檔資料.藥品條碼.GetEnumName(), 藥品條碼, false);
            if (list_obj.Count > 0)
            {
                str = list_obj[0][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
            }
            return str;
        }
        private bool Function_藥品資料_藥檔資料_確認欄位正確(object[] SQL_Data, bool IsMyMessageBoxShow)
        {
            bool flag_OK = false;
            List<string> List_error_msg = new List<string>();
            string str_error_msg = "";
            if (SQL_Data[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString().StringIsEmpty())
            {
                List_error_msg.Add("'藥品碼'欄位空白");
            }
            //if (SQL_Data[(int)enum_藥品資料_藥檔資料.藥品條碼].ObjectToString().StringIsEmpty())
            //{
            //    List_error_msg.Add("'藥品條碼'欄位空白");
            //}
            //if (SQL_Data[(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString().StringIsEmpty())
            //{
            //    List_error_msg.Add("'藥品名稱'欄位空白");
            //}
            //if (SQL_Data[(int)enum_藥品資料_藥檔資料.健保碼].ObjectToString().StringIsEmpty())
            //{
            //    List_error_msg.Add("'健保碼'欄位空白");
            //}

            if (SQL_Data[(int)enum_藥品資料_藥檔資料.庫存].ObjectToString().StringToInt32() < 0 )
            {
                SQL_Data[(int)enum_藥品資料_藥檔資料.庫存] = "0";
            }

            if (SQL_Data[(int)enum_藥品資料_藥檔資料.安全庫存].ObjectToString().StringToInt32() < 0)
            {
                SQL_Data[(int)enum_藥品資料_藥檔資料.安全庫存] = "0";
            }      
            for (int i = 0; i < List_error_msg.Count; i++)
            {
                str_error_msg += i.ToString("00") + ". " + List_error_msg[i] + "\n\r";
            }
            if (str_error_msg == "") flag_OK = true;
            else
            {
                if (IsMyMessageBoxShow) MyMessageBox.ShowDialog(str_error_msg);
            }
            return flag_OK;
        }
    
        private string Function_藥品資料_藥檔資料_檢查內容(object[] value)
        {
            string str_error = "";
            List<string> list_error = new List<string>();
            if (value[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString().StringIsEmpty())
            {
                list_error.Add("'藥品碼'欄位不得空白!");
            }
            
            for (int i = 0; i < list_error.Count; i++)
            {
                str_error += $"{(i + 1).ToString("00")}. {list_error[i]}";
                if (i != list_error.Count - 1) str_error += "\n";
            }
            return str_error;
        }
        private void Function_藥品資料_藥檔資料_登錄()
        {
            object[] value = new object[new enum_藥品資料_藥檔資料().GetLength()];
            string Code = Function_藥品碼檢查(this.textBox_藥品資料_藥檔資料_藥品碼.Text);
            value[(int)enum_藥品資料_藥檔資料.藥品碼] = Code;
            value[(int)enum_藥品資料_藥檔資料.藥品名稱] = this.textBox_藥品資料_藥檔資料_藥品名稱.Text;
            value[(int)enum_藥品資料_藥檔資料.藥品學名] = this.textBox_藥品資料_藥檔資料_藥品學名.Text;
            value[(int)enum_藥品資料_藥檔資料.藥品中文名稱] = this.textBox_藥品資料_藥檔資料_藥品中文名稱.Text;
            value[(int)enum_藥品資料_藥檔資料.藥品群組] = this.rJ_ComboBox_藥品資料_藥檔資料_藥品群組.SelectedIndex;
            value[(int)enum_藥品資料_藥檔資料.健保碼] = this.textBox_藥品資料_藥檔資料_健保碼.Text;
            value[(int)enum_藥品資料_藥檔資料.藥品條碼] = this.textBox_藥品資料_藥檔資料_藥品條碼.Text;
            value[(int)enum_藥品資料_藥檔資料.包裝單位] = this.textBox_藥品資料_藥檔資料_包裝單位.Text;
            value[(int)enum_藥品資料_藥檔資料.庫存] = this.textBox_藥品資料_藥檔資料_庫存.Text;
            value[(int)enum_藥品資料_藥檔資料.安全庫存] = this.textBox_藥品資料_藥檔資料_安全庫存.Text;
            value[(int)enum_藥品資料_藥檔資料.警訊藥品] = this.comboBox_藥品資料_藥檔資料_警訊藥品.Texts;
            if (this.Function_藥品資料_藥檔資料_確認欄位正確(value, true))
            {
                List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetRows(enum_藥品資料_藥檔資料.藥品碼.GetEnumName(), this.textBox_藥品資料_藥檔資料_藥品碼.Text, false);
                if (list_value.Count > 0)
                {
                    value[(int)enum_藥品資料_藥檔資料.GUID] = list_value[0][(int)enum_藥品資料_藥檔資料.GUID].ObjectToString();
                    this.sqL_DataGridView_藥品資料_藥檔資料.SQL_Replace((int)enum_藥品資料_藥檔資料.GUID, value[(int)enum_藥品資料_藥檔資料.GUID].ObjectToString(), value, false);
                    this.sqL_DataGridView_藥品資料_藥檔資料.ReplaceExtra(value, true);
                }
                else
                {
                    value[(int)enum_藥品資料_藥檔資料.GUID] = Guid.NewGuid().ToString();
                    this.sqL_DataGridView_藥品資料_藥檔資料.SQL_AddRow(value, false);
                    this.sqL_DataGridView_藥品資料_藥檔資料.AddRow(value, true);
                }
                this.Function_藥品資料_藥檔資料_清除攔位();
            }
        }
        private void Function_藥品資料_藥檔資料_清除攔位()
        {
            this.Invoke(new Action(delegate
            {
                this.textBox_藥品資料_藥檔資料_藥品碼.Text = "";
                this.textBox_藥品資料_藥檔資料_藥品名稱.Text = "";
                this.textBox_藥品資料_藥檔資料_藥品學名.Text = "";
                this.textBox_藥品資料_藥檔資料_藥品中文名稱.Text = "";
                this.rJ_ComboBox_藥品資料_藥檔資料_藥品群組.SelectedIndex = 0;
                this.textBox_藥品資料_藥檔資料_健保碼.Text = "";
                this.textBox_藥品資料_藥檔資料_庫存.Text = "";
                this.textBox_藥品資料_藥檔資料_安全庫存.Text = "";
                this.textBox_藥品資料_藥檔資料_包裝單位.Text = "";
                this.textBox_藥品資料_藥檔資料_藥品條碼.Text = "";
                this.comboBox_藥品資料_藥檔資料_警訊藥品.SelectedIndex = 0;
            }));
            
        }
        private void Function_藥品資料_藥檔資料_匯出()
        {
            saveFileDialog_SaveExcel.OverwritePrompt = false;
            if (saveFileDialog_SaveExcel.ShowDialog(this) == DialogResult.OK)
            {
                DataTable datatable = new DataTable();
                datatable = sqL_DataGridView_藥品資料_藥檔資料.GetDataTable();
                datatable = datatable.ReorderTable(new enum_藥品資料_藥檔資料_匯出());
                string Extension = System.IO.Path.GetExtension(this.saveFileDialog_SaveExcel.FileName);
                if (Extension == ".txt")
                {
                    CSVHelper.SaveFile(datatable, this.saveFileDialog_SaveExcel.FileName);
                }
                else if (Extension == ".xls")
                {
                    MyOffice.ExcelClass.NPOI_SaveFile(datatable, this.saveFileDialog_SaveExcel.FileName);
                }

                MyMessageBox.ShowDialog("匯出完成!");
            }
        }
        private void Function_藥品資料_藥檔資料_匯入()
        {
            if (openFileDialog_LoadExcel.ShowDialog(this) == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dataTable = new DataTable();
                CSVHelper.LoadFile(this.openFileDialog_LoadExcel.FileName, 0, dataTable);
                DataTable datatable_buf = dataTable.ReorderTable(new enum_藥品資料_藥檔資料_匯入());
                if (datatable_buf == null)
                {
                    MyMessageBox.ShowDialog("匯入檔案,資料錯誤!");
                    return;
                }
                List<object[]> list_LoadValue = datatable_buf.DataTableToRowList();
                List<object[]> list_SQL_Value = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
                List<object[]> list_Add = new List<object[]>();
                List<object[]> list_Delete_ColumnName = new List<object[]>();
                List<object[]> list_Delete_SerchValue = new List<object[]>();
                List<string> list_Replace_SerchValue = new List<string>();
                List<object[]> list_Replace_Value = new List<object[]>();
                List<object[]> list_SQL_Value_buf = new List<object[]>();

                for (int i = 0; i < list_LoadValue.Count; i++)
                {
                    object[] value_load = list_LoadValue[i];
                    value_load = value_load.CopyRow(new enum_藥品資料_藥檔資料_匯入(), new enum_藥品資料_藥檔資料());
                    if (!Function_藥品資料_藥檔資料_檢查內容(value_load).StringIsEmpty()) continue;

                    value_load[(int)enum_藥品資料_藥檔資料.藥品碼] = this.Function_藥品碼檢查(value_load[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString());

                    list_SQL_Value_buf = list_SQL_Value.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, value_load[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString());
                    if (list_SQL_Value_buf.Count > 0)
                    {
                        object[] value_SQL = list_SQL_Value_buf[0];
                        value_load[(int)enum_藥品資料_藥檔資料.GUID] = value_SQL[(int)enum_藥品資料_藥檔資料.GUID];
                        bool flag_Equal = value_load.IsEqual(value_SQL);
                        if (!flag_Equal)
                        {
                            list_Replace_SerchValue.Add(value_load[(int)enum_藥品資料_藥檔資料.GUID].ObjectToString());
                            list_Replace_Value.Add(value_load);
                        }
                    }
                    else
                    {
                        value_load[(int)enum_藥品資料_藥檔資料.GUID] = Guid.NewGuid().ToString();
                        list_Add.Add(value_load);
                    }
                }
                this.sqL_DataGridView_藥品資料_藥檔資料.SQL_AddRows(list_Add, false);
                this.sqL_DataGridView_藥品資料_藥檔資料.SQL_ReplaceExtra(enum_藥品資料_藥檔資料.GUID.GetEnumName(), list_Replace_SerchValue, list_Replace_Value, false);
                this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(true);
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
            MyMessageBox.ShowDialog("匯入完成!");
        }
        private DialogResult Function_藥品資料_藥檔資料_藥品群組設定()
        {
            DialogResult dialogResult;
            Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(Function_藥品群組_取得選單(true));
            dialog_ContextMenuStrip.TitleText = "藥品群組設定";
            dialog_ContextMenuStrip.ControlsTextAlign = ContentAlignment.MiddleLeft;
            dialog_ContextMenuStrip.ControlsHeight = 40;
            dialogResult = dialog_ContextMenuStrip.ShowDialog();
            if (dialogResult == DialogResult.Yes)
            {
                string[] strArray = myConvert.分解分隔號字串(dialog_ContextMenuStrip.Value, ".");
                if (strArray.Length == 2)
                {
                    int 群組序號 = strArray[0].StringToInt32();
                    List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.Get_All_Select_RowsValues();
                    if (群組序號 >= 1 && 群組序號 <= 20)
                    {
                        List<string[]> list_Replace_SerchValue = new List<string[]>();
                        List<object[]> list_Replace_Value = new List<object[]>();
                       
                        for (int i = 0; i < list_value.Count; i++)
                        {
                            list_value[i][(int)enum_藥品資料_藥檔資料.藥品群組] = 群組序號.ToString("00");
                        }
                        this.sqL_DataGridView_藥品資料_藥檔資料.SQL_ReplaceExtra(list_value, false);
                        this.sqL_DataGridView_藥品資料_藥檔資料.ReplaceExtra(list_value, true);
                    }
                    else if(群組序號 == 0)
                    {
                        for (int i = 0; i < list_value.Count; i++)
                        {
                            list_value[i][(int)enum_藥品資料_藥檔資料.藥品群組] = "";
                        }
                        this.sqL_DataGridView_藥品資料_藥檔資料.SQL_ReplaceExtra(list_value, false);
                        this.sqL_DataGridView_藥品資料_藥檔資料.ReplaceExtra(list_value, true);
                    }
                }

            }
            return dialogResult;
        }
        #endregion
        #region Event
        #region 藥品群組
        private void RJ_ComboBox_藥品資料_藥檔資料_藥品群組_Enter(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                this.rJ_ComboBox_藥品資料_藥檔資料_藥品群組.SetDataSource(this.Function_藥品群組_取得選單(true));
            }));
           
        }
        private void RJ_ComboBox_藥品資料_藥檔資料_資料查詢_藥品群組_Enter(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                this.rJ_ComboBox_藥品資料_藥檔資料_資料查詢_藥品群組.SetDataSource(this.Function_藥品群組_取得選單(false));
            }));
           
        }
        private void SqL_DataGridView_藥品群組_RowEnterEvent(object[] RowValue)
        {
            int index = this.rJ_TextBox_藥品群組_群組序號.Text.StringToInt32();
            if (index > 0)
            {
                List<object[]> list_value = this.sqL_DataGridView_藥品群組.SQL_GetRows(enum_藥品群組.群組序號.GetEnumName(), index.ToString("00"), false);
                if (list_value.Count > 0)
                {
                    string GUID = list_value[0][(int)enum_藥品群組.GUID].ObjectToString();
                    object[] value = new object[new enum_藥品群組().GetEnumNames().Length];
                    value[(int)enum_藥品群組.GUID] = GUID;
                    value[(int)enum_藥品群組.群組序號] = index.ToString("00");
                    value[(int)enum_藥品群組.群組名稱] = this.rJ_TextBox_藥品群組_群組名稱.Text;
                    this.sqL_DataGridView_藥品群組.SQL_Replace(enum_藥品群組.GUID.GetEnumName(), GUID, value, false);
                }

            }
            rJ_TextBox_藥品群組_群組序號.Text = RowValue[(int)enum_藥品群組.群組序號].ObjectToString();
            rJ_TextBox_藥品群組_群組名稱.Text = RowValue[(int)enum_藥品群組.群組名稱].ObjectToString();
        }
        private void SqL_DataGridView_藥品群組_DataGridRowsChangeEvent(List<object[]> RowsList)
        {
            RowsList.Sort(new Icp_藥品群組());
        }
        #endregion
        private void SqL_DataGridView_藥品資料_藥檔資料_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new ContextMenuStrip_藥品資料_藥檔資料());
                if (dialog_ContextMenuStrip.ShowDialog() == DialogResult.Yes)
                {
                    if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥品資料_藥檔資料.匯出.GetEnumName())
                    {
                        Function_藥品資料_藥檔資料_匯出();
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥品資料_藥檔資料.匯入.GetEnumName())
                    {
                        Function_藥品資料_藥檔資料_匯入();
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥品資料_藥檔資料.匯出選取資料.GetEnumName())
                    {
                        saveFileDialog_SaveExcel.OverwritePrompt = false;
                        if (saveFileDialog_SaveExcel.ShowDialog(this) == DialogResult.OK)
                        {
                            DataTable datatable = new DataTable();
                            datatable = sqL_DataGridView_藥品資料_藥檔資料.GetSelectRowsDataTable();
                            datatable = datatable.ReorderTable(new enum_藥品資料_藥檔資料_匯出());
                            CSVHelper.SaveFile(datatable, saveFileDialog_SaveExcel.FileName);
                            MyMessageBox.ShowDialog("匯出完成!");
                        }
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥品資料_藥檔資料.刪除選取資料.GetEnumName())
                    {
                        if (MyMessageBox.ShowDialog("是否刪除選取資料", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.Get_All_Select_RowsValues();
                            List<object> list_delete_serchValue = new List<object>();
                            for (int i = 0; i < list_value.Count; i++)
                            {
                                string GUID = list_value[i][(int)enum_藥品資料_藥檔資料.GUID].ObjectToString();
                                list_delete_serchValue.Add(GUID);
                            }
                            this.sqL_DataGridView_藥品資料_藥檔資料.SQL_DeleteExtra(enum_藥品資料_藥檔資料.GUID.GetEnumName(), list_delete_serchValue, true);
                            this.Cursor = Cursors.Default;
                        }
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥品資料_藥檔資料.登錄資料.GetEnumName())
                    {
                        Function_藥品資料_藥檔資料_登錄();
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥品資料_藥檔資料.設定安全庫存.GetEnumName())
                    {
                        Dialog_NumPannel dialog_NumPannel = new Dialog_NumPannel();
                        if(dialog_NumPannel.ShowDialog() == DialogResult.Yes)
                        {
                            List<object[]> list_value = sqL_DataGridView_藥品資料_藥檔資料.Get_All_Select_RowsValues();
                            for (int i = 0; i < list_value.Count; i++)
                            {
                                list_value[i][(int)enum_藥品資料_藥檔資料.安全庫存] = dialog_NumPannel.Value.ToString();
                            }
                            sqL_DataGridView_藥品資料_藥檔資料.SQL_ReplaceExtra(list_value, true);
                        }
                        else
                        {
                            this.SqL_DataGridView_藥品資料_藥檔資料_MouseDown(sender, e);
                        }
                    }
                    else if (dialog_ContextMenuStrip.Value == ContextMenuStrip_藥品資料_藥檔資料.藥品群組設定.GetEnumName())
                    {
                        if (Function_藥品資料_藥檔資料_藥品群組設定() == DialogResult.No)
                        {
                            this.SqL_DataGridView_藥品資料_藥檔資料_MouseDown(sender, e);
                        }
                        
                    }
                }
            }
        }
        private void SqL_DataGridView_藥品資料_藥檔資料_RowEnterEvent(object[] RowValue)
        {
            //this.rJ_ComboBox_藥品資料_藥檔資料_藥品群組.SetDataSource(this.Function_藥品群組_取得選單());
            //int index = RowValue[(int)enum_藥品資料_藥檔資料.藥品群組].ObjectToString().StringToInt32() - 1;
            //Finction_藥品群組_名稱轉序號(RowValue, (int)enum_藥品資料_藥檔資料.藥品群組);
            //this.textBox_藥品資料_藥檔資料_藥品碼.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
            //this.textBox_藥品資料_藥檔資料_藥品名稱.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
            //this.textBox_藥品資料_藥檔資料_藥品學名.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品學名].ObjectToString();
            //this.textBox_藥品資料_藥檔資料_藥品中文名稱.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品中文名稱].ObjectToString();
            //if (index >= 0) this.rJ_ComboBox_藥品資料_藥檔資料_藥品群組.SelectedIndex = index;
            //this.textBox_藥品資料_藥檔資料_健保碼.Text = RowValue[(int)enum_藥品資料_藥檔資料.健保碼].ObjectToString();
            //this.textBox_藥品資料_藥檔資料_庫存.Text = RowValue[(int)enum_藥品資料_藥檔資料.庫存].ObjectToString();
            //this.textBox_藥品資料_藥檔資料_安全庫存.Text = RowValue[(int)enum_藥品資料_藥檔資料.安全庫存].ObjectToString();
            //this.textBox_藥品資料_藥檔資料_包裝單位.Text = RowValue[(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
            //this.textBox_藥品資料_藥檔資料_藥品條碼.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品條碼].ObjectToString();
            //this.comboBox_藥品資料_藥檔資料_警訊藥品.Texts = RowValue[(int)enum_藥品資料_藥檔資料.警訊藥品].ObjectToString();
        }
        private void SqL_DataGridView_藥品資料_藥檔資料_RowDoubleClickEvent(object[] RowValue)
        {
            this.rJ_ComboBox_藥品資料_藥檔資料_藥品群組.SetDataSource(this.Function_藥品群組_取得選單(false));
            int index = RowValue[(int)enum_藥品資料_藥檔資料.藥品群組].ObjectToString().StringToInt32() - 1;
            Finction_藥品群組_名稱轉序號(RowValue, (int)enum_藥品資料_藥檔資料.藥品群組);
            this.textBox_藥品資料_藥檔資料_藥品碼.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
            this.textBox_藥品資料_藥檔資料_藥品名稱.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString();
            this.textBox_藥品資料_藥檔資料_藥品學名.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品學名].ObjectToString();
            this.textBox_藥品資料_藥檔資料_藥品中文名稱.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品中文名稱].ObjectToString();
            if (index >= 0) this.rJ_ComboBox_藥品資料_藥檔資料_藥品群組.SelectedIndex = index;
            this.textBox_藥品資料_藥檔資料_健保碼.Text = RowValue[(int)enum_藥品資料_藥檔資料.健保碼].ObjectToString();
            this.textBox_藥品資料_藥檔資料_庫存.Text = RowValue[(int)enum_藥品資料_藥檔資料.庫存].ObjectToString();
            this.textBox_藥品資料_藥檔資料_安全庫存.Text = RowValue[(int)enum_藥品資料_藥檔資料.安全庫存].ObjectToString();
            this.textBox_藥品資料_藥檔資料_包裝單位.Text = RowValue[(int)enum_藥品資料_藥檔資料.包裝單位].ObjectToString();
            this.textBox_藥品資料_藥檔資料_藥品條碼.Text = RowValue[(int)enum_藥品資料_藥檔資料.藥品條碼].ObjectToString();
            this.comboBox_藥品資料_藥檔資料_警訊藥品.Texts = RowValue[(int)enum_藥品資料_藥檔資料.警訊藥品].ObjectToString();
        }
        private void SqL_DataGridView_藥品資料_藥檔資料_DataGridRowsChangeEvent(List<object[]> RowsList)
        {
            for (int i = 0; i < RowsList.Count; i++)
            {
                RowsList[i][(int)enum_藥品資料_藥檔資料.庫存] = this.Function_從本地資料取得庫存(RowsList[i][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString()).ToString();
            }
            Finction_藥品群組_序號轉名稱(RowsList, (int)enum_藥品資料_藥檔資料.藥品群組);
            RowsList.Sort(new Icp_藥品資料_藥檔資料());
        }
        private void sqL_DataGridView_藥品資料_藥檔資料_DataGridRefreshEvent()
        {
            for (int i = 0; i < this.sqL_DataGridView_藥品資料_藥檔資料.dataGridView.Rows.Count; i++)
            {
                if (this.sqL_DataGridView_藥品資料_藥檔資料.dataGridView.Rows[i].Cells[enum_藥品資料_藥檔資料.安全庫存.GetEnumName()].Value.ToString().StringToInt32() != 0)
                {
                    if (this.sqL_DataGridView_藥品資料_藥檔資料.dataGridView.Rows[i].Cells[enum_藥品資料_藥檔資料.庫存.GetEnumName()].Value.ToString().StringToInt32() < this.sqL_DataGridView_藥品資料_藥檔資料.dataGridView.Rows[i].Cells[enum_藥品資料_藥檔資料.安全庫存.GetEnumName()].Value.ToString().StringToInt32())
                    {
                        this.sqL_DataGridView_藥品資料_藥檔資料.dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        this.sqL_DataGridView_藥品資料_藥檔資料.dataGridView.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }
   
        private void TextBox_藥品資料_藥檔資料_資料查詢_藥品條碼_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox_藥品資料_藥檔資料_資料查詢_藥品條碼.Text != "")
                {
                    this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetRows(enum_藥品資料_藥檔資料.藥品條碼.GetEnumName(), textBox_藥品資料_藥檔資料_資料查詢_藥品條碼.Text, true);
                }
            }
        }
        private void PlC_RJ_Button_藥品資料_藥檔資料_資料查詢_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            if (!textBox_藥品資料_藥檔資料_資料查詢_藥品碼.Text.StringIsEmpty()) list_value = list_value.GetRowsByLike((int)enum_藥品資料_藥檔資料.藥品碼, textBox_藥品資料_藥檔資料_資料查詢_藥品碼.Text);
            if (!textBox_藥品資料_藥檔資料_資料查詢_藥品名稱.Text.StringIsEmpty())
            {
                list_value = (from value in list_value
                              where value[(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString().ToUpper().Contains(textBox_藥品資料_藥檔資料_資料查詢_藥品名稱.Text.ToUpper())
                              select value).ToList();
            }
            if (!textBox_藥品資料_藥檔資料_資料查詢_藥品條碼.Text.StringIsEmpty()) list_value = list_value.GetRows((int)enum_藥品資料_藥檔資料.藥品條碼, textBox_藥品資料_藥檔資料_資料查詢_藥品條碼.Text);
            if (plC_RJ_ChechBox_藥品資料_藥檔資料_資料查詢_藥品群組.Checked)
            {
                int index = rJ_ComboBox_藥品資料_藥檔資料_資料查詢_藥品群組.SelectedIndex;
                index++;
                if (index > 0)
                {
                    list_value = list_value.GetRows((int)enum_藥品資料_藥檔資料.藥品群組, index.ToString("00"));
                }
            }
            this.sqL_DataGridView_藥品資料_藥檔資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥品資料_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                if (MyMessageBox.ShowDialog("是否刪除選取資料", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.Get_All_Select_RowsValues();
                    this.sqL_DataGridView_藥品資料_藥檔資料.SQL_DeleteExtra(list_value, false);
                    this.sqL_DataGridView_藥品資料_藥檔資料.DeleteExtra(list_value, true);
                    this.Cursor = Cursors.Default;
                }
            }));
        }
        private void PlC_RJ_Button_藥品資料_登錄_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Function_藥品資料_藥檔資料_登錄();
            }));
        }
        private void PlC_RJ_Button_藥品資料_匯出_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Function_藥品資料_藥檔資料_匯出();
            }));
        }
        private void PlC_RJ_Button_藥品資料_匯入_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                Function_藥品資料_藥檔資料_匯入();
            }));
        }
        private void PlC_RJ_Button_藥品資料_顯示有儲位藥品_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_藥品資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            List<object[]> list_藥品資料_buf = new List<object[]>();
            List<object[]> list_value = new List<object[]>();

            List<Device> devices = this.Function_從SQL取得所有儲位();
            List<string> list_code = (from value in devices
                                      select value.Code).ToList().Distinct().ToList();
            for (int i = 0; i < list_code.Count; i++)
            {
                list_藥品資料_buf = list_藥品資料.GetRows((int)enum_藥品資料_藥檔資料.藥品碼, list_code[i]);
                if(list_藥品資料_buf.Count > 0)
                {
                    list_value.Add(list_藥品資料_buf[0]);
                }
            }
            this.sqL_DataGridView_藥品資料_藥檔資料.RefreshGrid(list_value);
        }
        private void PlC_RJ_Button_藥品資料_藥檔資料_檢查藥檔_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            List<object[]> list_delete = new List<object[]>();
            string 藥品碼 = "";
            for (int i = 0; i < list_value.Count; i++)
            {
                藥品碼 = list_value[i][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                if (藥品碼.Length < 5)
                {
                    list_delete.Add(list_value[i]);
                }
            }
            this.sqL_DataGridView_藥品資料_藥檔資料.SQL_DeleteExtra(list_delete, false);
            this.sqL_DataGridView_藥品資料_藥檔資料.DeleteExtra(list_delete, true);
        }
        private void PlC_RJ_Button_藥品群組_登錄_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                RJ_TextBox_藥品群組_群組名稱_KeyPress(null, null);
            }));
        }

        #endregion
        public class Icp_藥品資料_藥檔資料 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                string 藥品碼_0 = x[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                string 藥品碼_1 = y[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString();
                return 藥品碼_0.CompareTo(藥品碼_1);
            }
        }
        public class Icp_藥品群組 : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                int index_0 = x[(int)enum_藥品群組.群組序號].ObjectToString().StringToInt32();
                int index_1 = y[(int)enum_藥品群組.群組序號].ObjectToString().StringToInt32();
                return index_0.CompareTo(index_1);
            }
        }
    }
}
