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
using H_Pannel_lib;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承

namespace 調劑台管理系統
{
    public partial class Form1 : Form
    {
        public enum enum_UDSDBBCM
        {
            GUID,
            藥品碼,
            藥品名稱,
            料號,
            ATC主碼,
            藥品條碼1,
            藥品條碼2,
            藥品條碼3,
            藥品條碼4,
            藥品條碼5,
        }
        private void Program_藥品資料_藥檔設定_Init()
        {
            SQLUI.SQL_DataGridView.SQL_Set_Properties(this.sqL_DataGridView_UDSDBBCM, "UDSDBBCM", "DSVM", "user", "66437068", "10.97.241.193", 3306, MySql.Data.MySqlClient.MySqlSslMode.None);
            this.sqL_DataGridView_UDSDBBCM.Init();

            this.plC_RJ_Button_藥檔設定_顯示全部.MouseDownEvent += PlC_RJ_Button_藥檔設定_顯示全部_MouseDownEvent;
            this.plC_RJ_Button_藥檔設定_更新藥檔.MouseDownEvent += PlC_RJ_Button_藥檔設定_更新藥檔_MouseDownEvent;

            this.plC_UI_Init.Add_Method(this.sub_Program_藥品資料_藥檔設定);
        }

     

        bool flag_藥品資料_藥檔設定_頁面更新 = false;
        private void sub_Program_藥品資料_藥檔設定()
        {
            if (this.plC_ScreenPage_Main.PageText == "藥品資料" && this.plC_ScreenPage_藥品資料_藥檔資料.PageText == "藥檔設定")
            {
                if (!this.flag_藥品資料_藥檔設定_頁面更新)
                {
               
                    this.flag_藥品資料_藥檔設定_頁面更新 = true;
                }
            }
            else
            {
                this.flag_藥品資料_藥檔設定_頁面更新 = false;
            }


            sub_Program_藥檔設定_更新藥檔();
        }

        #region PLC_藥檔設定_更新藥檔
        PLC_Device PLC_Device_藥檔設定_更新藥檔 = new PLC_Device("");
        PLC_Device PLC_Device_藥檔設定_更新藥檔_OK = new PLC_Device("");
        Task Task_藥檔設定_更新藥檔;
        MyTimer MyTimer_藥檔設定_更新藥檔_結束延遲 = new MyTimer();
        int cnt_Program_藥檔設定_更新藥檔 = 65534;
        void sub_Program_藥檔設定_更新藥檔()
        {
            if (PLC_Device_主機扣賬模式.Bool) PLC_Device_藥檔設定_更新藥檔.Bool = true;
            if (cnt_Program_藥檔設定_更新藥檔 == 65534)
            {
                this.MyTimer_藥檔設定_更新藥檔_結束延遲.StartTickTime(1000);
                PLC_Device_藥檔設定_更新藥檔.SetComment("PLC_藥檔設定_更新藥檔");
                PLC_Device_藥檔設定_更新藥檔_OK.SetComment("PLC_藥檔設定_更新藥檔_OK");
                PLC_Device_藥檔設定_更新藥檔.Bool = false;
                cnt_Program_藥檔設定_更新藥檔 = 65535;
            }
            if (cnt_Program_藥檔設定_更新藥檔 == 65535) cnt_Program_藥檔設定_更新藥檔 = 1;
            if (cnt_Program_藥檔設定_更新藥檔 == 1) cnt_Program_藥檔設定_更新藥檔_檢查按下(ref cnt_Program_藥檔設定_更新藥檔);
            if (cnt_Program_藥檔設定_更新藥檔 == 2) cnt_Program_藥檔設定_更新藥檔_初始化(ref cnt_Program_藥檔設定_更新藥檔);
            if (cnt_Program_藥檔設定_更新藥檔 == 3) cnt_Program_藥檔設定_更新藥檔 = 65500;
            if (cnt_Program_藥檔設定_更新藥檔 > 1) cnt_Program_藥檔設定_更新藥檔_檢查放開(ref cnt_Program_藥檔設定_更新藥檔);

            if (cnt_Program_藥檔設定_更新藥檔 == 65500)
            {
                this.MyTimer_藥檔設定_更新藥檔_結束延遲.TickStop();
                this.MyTimer_藥檔設定_更新藥檔_結束延遲.StartTickTime(60000 * 60);
                PLC_Device_藥檔設定_更新藥檔.Bool = false;
                PLC_Device_藥檔設定_更新藥檔_OK.Bool = false;
                cnt_Program_藥檔設定_更新藥檔 = 65535;
            }
        }
        void cnt_Program_藥檔設定_更新藥檔_檢查按下(ref int cnt)
        {
            if (PLC_Device_藥檔設定_更新藥檔.Bool) cnt++;
        }
        void cnt_Program_藥檔設定_更新藥檔_檢查放開(ref int cnt)
        {
            if (!PLC_Device_藥檔設定_更新藥檔.Bool) cnt = 65500;
        }
        void cnt_Program_藥檔設定_更新藥檔_初始化(ref int cnt)
        {
            if (this.MyTimer_藥檔設定_更新藥檔_結束延遲.IsTimeOut())
            {
                if (Task_藥檔設定_更新藥檔 == null)
                {
                    Task_藥檔設定_更新藥檔 = new Task(new Action(delegate { PlC_RJ_Button_藥檔設定_更新藥檔_MouseDownEvent(null); }));
                }
                if (Task_藥檔設定_更新藥檔.Status == TaskStatus.RanToCompletion)
                {
                    Task_藥檔設定_更新藥檔 = new Task(new Action(delegate { PlC_RJ_Button_藥檔設定_更新藥檔_MouseDownEvent(null); }));
                }
                if (Task_藥檔設定_更新藥檔.Status == TaskStatus.Created)
                {
                    Task_藥檔設定_更新藥檔.Start();
                }
                cnt++;
            }
        }







        #endregion

        #region Event
        private void PlC_RJ_Button_藥檔設定_更新藥檔_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<object[]> list_UDSDBBCM = this.sqL_DataGridView_UDSDBBCM.SQL_GetAllRows(false);
            Console.Write($"取得雲端藥檔資料 耗時{myTimer.ToString()}\n");
            List<object[]> list_藥檔資料 = this.sqL_DataGridView_藥品資料_藥檔資料.SQL_GetAllRows(false);
            List<object[]> list_藥檔資料_add = new List<object[]>();
            List<object[]> list_藥檔資料_replace = new List<object[]>();
            Parallel.ForEach(list_UDSDBBCM, UDSDBBCM =>
            {
                List<object[]> list_value_buf = (from value in list_藥檔資料
                                                 where value[(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString() == UDSDBBCM[(int)enum_UDSDBBCM.藥品碼].ObjectToString()
                                                 select value).ToList();
                if(list_value_buf.Count == 0)
                {
                    object[] value = new object[new enum_藥品資料_藥檔資料().GetLength()];
                    value[(int)enum_藥品資料_藥檔資料.GUID] = Guid.NewGuid().ToString();
                    value[(int)enum_藥品資料_藥檔資料.藥品碼] = UDSDBBCM[(int)enum_UDSDBBCM.藥品碼];
                    value[(int)enum_藥品資料_藥檔資料.藥品名稱] = UDSDBBCM[(int)enum_UDSDBBCM.藥品名稱];
                    value[(int)enum_藥品資料_藥檔資料.藥品條碼] = UDSDBBCM[(int)enum_UDSDBBCM.藥品條碼1];
                    value[(int)enum_藥品資料_藥檔資料.安全庫存] = "0";
                    value[(int)enum_藥品資料_藥檔資料.警訊藥品] = false.ToString();
                    list_藥檔資料_add.LockAdd(value);
                }
                else
                {
                    bool flag_replace = false;
                    string 藥品碼_buf = UDSDBBCM[(int)enum_UDSDBBCM.藥品碼].ObjectToString();
                    string 藥品名稱_buf =UDSDBBCM[(int)enum_UDSDBBCM.藥品名稱].ObjectToString();
                    string 藥品條碼1_buf = UDSDBBCM[(int)enum_UDSDBBCM.藥品條碼1].ObjectToString();
                    if (藥品碼_buf != list_value_buf[0][(int)enum_藥品資料_藥檔資料.藥品碼].ObjectToString()) flag_replace = true;
                    if (藥品名稱_buf != list_value_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱].ObjectToString()) flag_replace = true;
                    if (藥品條碼1_buf != list_value_buf[0][(int)enum_藥品資料_藥檔資料.藥品條碼].ObjectToString()) flag_replace = true;
                    if(flag_replace)
                    {
                        list_value_buf[0][(int)enum_藥品資料_藥檔資料.藥品碼] = 藥品碼_buf;
                        list_value_buf[0][(int)enum_藥品資料_藥檔資料.藥品名稱] = 藥品名稱_buf;
                        list_value_buf[0][(int)enum_藥品資料_藥檔資料.藥品條碼] = 藥品條碼1_buf;
                        list_藥檔資料_replace.LockAdd(list_value_buf[0]);
                    }
                }
            });

            Console.Write($"共需更新藥檔 新增{list_藥檔資料_add.Count}筆資料 ,修改{list_藥檔資料_replace.Count}筆資料 ,\n");

            if (list_藥檔資料_add.Count > 0) this.sqL_DataGridView_藥品資料_藥檔資料.SQL_AddRows(list_藥檔資料_add, false);
            if (list_藥檔資料_replace.Count > 0) this.sqL_DataGridView_藥品資料_藥檔資料.SQL_ReplaceExtra(list_藥檔資料_replace, false);
            Console.Write($"更新藥檔資料 耗時{myTimer.ToString()}\n");
        }
        private void PlC_RJ_Button_藥檔設定_顯示全部_MouseDownEvent(MouseEventArgs mevent)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            List<object[]> list_value = this.sqL_DataGridView_UDSDBBCM.SQL_GetAllRows(false);
            Console.Write($"取得藥檔資料 耗時{myTimer.ToString()}\n");
            this.sqL_DataGridView_UDSDBBCM.RefreshGrid(list_value);
            Console.Write($"更新資訊 耗時{myTimer.ToString()}\n");

        }
        #endregion

    }
}
