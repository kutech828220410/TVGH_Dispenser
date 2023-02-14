using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using MyUI;
using Basic;
using System.Diagnostics;//記得取用 FileVersionInfo繼承
using System.Reflection;//記得取用 Assembly繼承
using H_Pannel_lib;
namespace 調劑台管理系統
{

    public partial class Form1 : Form
    {
        public void Function_設定雲端資料更新()
        {
            this.Function_取藥堆疊資料_新增母資料(Guid.NewGuid().ToString(), "更新資料", enum_交易記錄查詢動作.None, "", "", "", "", "", "", "", "", "", "", 0, "");
        }
        public void Function_從SQL取得儲位到雲端資料()
        {
            try
            {
                MyTimer myTimer = new MyTimer();
                myTimer.StartTickTime(50000);
                Console.WriteLine($"開始SQL讀取儲位資料到雲端!");
                List<Task> taskList = new List<Task>();
                taskList.Add(Task.Run(() =>
                {
                    MyTimer myTimer0 = new MyTimer();
                    myTimer0.StartTickTime(50000);
                    List_EPD583_雲端資料 = this.drawerUI_EPD_583.SQL_GetAllDrawers();
                    Console.WriteLine($"讀取EPD583資料! 耗時 :{myTimer0.GetTickTime().ToString("0.000")} ");
                }));
                taskList.Add(Task.Run(() =>
                {
                    MyTimer myTimer1 = new MyTimer();
                    myTimer1.StartTickTime(50000);
                    List_EPD266_雲端資料 = this.storageUI_EPD_266.SQL_GetAllStorage();
                    Console.WriteLine($"讀取EPD266資料! 耗時 :{myTimer1.GetTickTime().ToString("0.000")} ");

                }));
                taskList.Add(Task.Run(() =>
                {
                    MyTimer myTimer2 = new MyTimer();
                    myTimer2.StartTickTime(50000);
                    List_RowsLED_雲端資料 = this.rowsLEDUI.SQL_GetAllRowsLED();
                    Console.WriteLine($"讀取RowsLED資料! 耗時 :{myTimer2.GetTickTime().ToString("0.000")} ");

                }));
                taskList.Add(Task.Run(() =>
                {
                    MyTimer myTimer2 = new MyTimer();
                    myTimer2.StartTickTime(50000);
                    List_RFID_雲端資料 = this.rfiD_UI.SQL_GetAllRFIDClass();
                    Console.WriteLine($"外部設備資料資料! 耗時 :{myTimer2.GetTickTime().ToString("0.000")} ");

                }));
                Task allTask = Task.WhenAll(taskList);
                allTask.Wait();
                Console.WriteLine($"SQL讀取儲位資料到雲端結束! 耗時 : {myTimer.GetTickTime().ToString("0.000")}");
            }
            catch
            {

            }
        
        }
        public List<object> Function_從SQL取得儲位到雲端資料(string 藥品碼)
        {
            List<object> list_value = new List<object>();
            List<Box> boxes = this.List_EPD583_雲端資料.SortByCode(藥品碼);
            List<Storage> storages = this.List_EPD266_雲端資料.SortByCode(藥品碼);
            List<RowsDevice> rowsDevices = this.List_RowsLED_雲端資料.SortByCode(藥品碼);
            List<RFIDDevice> rFIDDevices = this.List_RFID_雲端資料.SortByCode(藥品碼);
            for (int i = 0; i < boxes.Count; i++)
            {
                Box box = this.drawerUI_EPD_583.SQL_GetBox(boxes[i]);
                this.List_EPD583_雲端資料.Add_NewDrawer(box);
                list_value.Add(box);
            }
            for (int i = 0; i < storages.Count; i++)
            {
                Storage storage = this.storageUI_EPD_266.SQL_GetStorage(storages[i]);
                this.List_EPD266_雲端資料.Add_NewStorage(storage);
                list_value.Add(storage);
            }
            for (int i = 0; i < rowsDevices.Count; i++)
            {
                RowsDevice rowsDevice = this.rowsLEDUI.SQL_GetRowsDevice(rowsDevices[i]);
                this.List_RowsLED_雲端資料.Add_NewRowsLED(rowsDevice);
                list_value.Add(rowsDevice);
            }
            for (int i = 0; i < rFIDDevices.Count; i++)
            {
                RFIDDevice rFIDDevice = this.rfiD_UI.SQL_GetDevice(rFIDDevices[i]);
                this.List_RFID_雲端資料.Add_NewRFIDClass(rFIDDevice);
                list_value.Add(rFIDDevices);
            }
            return list_value;
        }    
        public List<object> Function_從雲端資料取得儲位(string 藥品碼)
        {
            List<object> list_value = new List<object>();
            List<Box> boxes = this.List_EPD583_雲端資料.SortByCode(藥品碼);
            List<Storage> storages = this.List_EPD266_雲端資料.SortByCode(藥品碼);
            List<RowsDevice> rowsDevices = this.List_RowsLED_雲端資料.SortByCode(藥品碼);
            List<RFIDDevice> rFIDDevices = this.List_RFID_雲端資料.SortByCode(藥品碼);
            for (int i = 0; i < boxes.Count; i++)
            {
                list_value.Add(boxes[i]);
            }
            for (int i = 0; i < storages.Count; i++)
            {
                list_value.Add(storages[i]);
            }
            for (int i = 0; i < rowsDevices.Count; i++)
            {
                list_value.Add(rowsDevices[i]);
            }
            for(int i = 0; i < rFIDDevices.Count; i++)
            {
                list_value.Add(rFIDDevices[i]);
            }
            return list_value;
        }
        public void Function_從雲端資料取得儲位(string 藥品碼, ref List<string> TYPE, ref List<object> values)
        {
            List<object> list_value = this.Function_從雲端資料取得儲位(藥品碼);
            TYPE.Clear();
            values.Clear();
            for (int i = 0; i < list_value.Count; i++)
            {
                if (list_value[i] is Device)
                {
                    Device device = (Device)list_value[i];
                    values.Add(list_value[i]);
                    TYPE.Add(device.DeviceType.GetEnumName());
                }

            }
        }
        public int Function_從雲端資料取得庫存(string 藥品碼)
        {
            int 庫存 = 0;
            List<object> list_value = new List<object>();
            List<string> 儲位_TYPE = new List<string>();
            this.Function_從雲端資料取得儲位(this.Function_藥品碼檢查(藥品碼), ref 儲位_TYPE, ref list_value);

            for (int i = 0; i < list_value.Count; i++)
            {
                if (list_value[i] is Device)
                {
                    庫存 += ((Device)list_value[i]).Inventory.StringToInt32();
                }
            }
            if (list_value.Count == 0) return -999;
            return 庫存;
        }
        public List<object[]> Function_取得異動儲位資訊從雲端資料(string 藥品碼, int 異動量, string 效期 ,string IP)
        {
            List<object> 儲位 = new List<object>();
            List<string> 儲位_TYPE = new List<string>();
            this.Function_從雲端資料取得儲位(this.Function_藥品碼檢查(藥品碼), ref 儲位_TYPE, ref 儲位);
            List<object[]> 儲位資訊_buf = new List<object[]>();
            List<object[]> 儲位資訊 = new List<object[]>();
            for (int k = 0; k < 儲位.Count; k++)
            {
                object value_device = 儲位[k];
                object[] value = new object[new enum_儲位資訊().GetLength()];
                if (value_device is Device)
                {
                    Device device = (Device)value_device;
                    for (int i = 0; i < device.List_Validity_period.Count; i++)
                    {
                        if (device.List_Validity_period[i] == 效期 && device.IP == IP)
                        {
                            value[(int)enum_儲位資訊.IP] = device.IP;
                            value[(int)enum_儲位資訊.TYPE] = 儲位_TYPE[k];
                            value[(int)enum_儲位資訊.效期] = device.List_Validity_period[i];
                            value[(int)enum_儲位資訊.庫存] = device.List_Inventory[i];
                            value[(int)enum_儲位資訊.異動量] = 異動量.ToString();
                            value[(int)enum_儲位資訊.Value] = value_device;
                            儲位資訊.Add(value);
                            break;
                        }
                    }
                }
            }
            return 儲位資訊;
        }
        public List<object[]> Function_取得異動儲位資訊從雲端資料(string 藥品碼, int 異動量, string 效期)
        {
            List<object> 儲位 = new List<object>();
            List<string> 儲位_TYPE = new List<string>();
            this.Function_從雲端資料取得儲位(this.Function_藥品碼檢查(藥品碼), ref 儲位_TYPE, ref 儲位);
            List<object[]> 儲位資訊_buf = new List<object[]>();
            List<object[]> 儲位資訊 = new List<object[]>();
            for (int k = 0; k < 儲位.Count; k++)
            {
                object value_device = 儲位[k];
                object[] value = new object[new enum_儲位資訊().GetLength()];
                if (value_device is Device)
                {
                    Device device = (Device)value_device;
                    for (int i = 0; i < device.List_Validity_period.Count; i++)
                    {
                        if (device.List_Validity_period[i] == 效期)
                        {
                            value[(int)enum_儲位資訊.IP] = device.IP;
                            value[(int)enum_儲位資訊.TYPE] = 儲位_TYPE[k];
                            value[(int)enum_儲位資訊.效期] = device.List_Validity_period[i];
                            value[(int)enum_儲位資訊.庫存] = device.List_Inventory[i];
                            value[(int)enum_儲位資訊.異動量] = 異動量.ToString();
                            value[(int)enum_儲位資訊.Value] = value_device;
                            儲位資訊.Add(value);
                            break;
                        }
                    }
                }
            }
            return 儲位資訊;
        }
        public List<object[]> Function_取得異動儲位資訊從雲端資料(string 藥品碼, int 異動量)
        {
            List<object> 儲位 = new List<object>();
            List<string> 儲位_TYPE = new List<string>();
            this.Function_從雲端資料取得儲位(this.Function_藥品碼檢查(藥品碼), ref 儲位_TYPE, ref 儲位);
            List<object[]> 儲位資訊_buf = new List<object[]>();
            List<object[]> 儲位資訊 = new List<object[]>();
            if (儲位.Count == 0) return 儲位資訊_buf;


            for (int k = 0; k < 儲位.Count; k++)
            {
                object value_device = 儲位[k];              
                if (value_device is Device)
                {
                    Device device = (Device)value_device;
                    for (int i = 0; i < device.List_Validity_period.Count; i++)
                    {
                        object[] value = new object[new enum_儲位資訊().GetLength()];
                        value[(int)enum_儲位資訊.IP] = device.IP;
                        value[(int)enum_儲位資訊.TYPE] = 儲位_TYPE[k];
                        value[(int)enum_儲位資訊.效期] = device.List_Validity_period[i];
                        value[(int)enum_儲位資訊.庫存] = device.List_Inventory[i];
                        value[(int)enum_儲位資訊.異動量] = "0";
                        value[(int)enum_儲位資訊.Value] = value_device;
                        儲位資訊.Add(value);
                    }
                }
            }
            儲位資訊 = 儲位資訊.OrderBy(r => DateTime.Parse(r[(int)enum_儲位資訊.效期].ToDateString())).ToList();

            if (異動量 == 0) return 儲位資訊;
            int 使用數量 = 異動量;
            int 庫存數量 = 0;
            int 剩餘庫存數量 = 0;
            for (int i = 0; i < 儲位資訊.Count; i++)
            {
                庫存數量 = 儲位資訊[i][(int)enum_儲位資訊.庫存].ObjectToString().StringToInt32();
                if ((使用數量 < 0 && 庫存數量 > 0) || (使用數量 > 0 && 庫存數量 >= 0))
                {
                    剩餘庫存數量 = 庫存數量 + 使用數量;
                    if (剩餘庫存數量 >= 0)
                    {
                        儲位資訊[i][(int)enum_儲位資訊.異動量] = (使用數量).ToString();
                        儲位資訊_buf.Add(儲位資訊[i]);
                        break;
                    }
                    else
                    {
                        儲位資訊[i][(int)enum_儲位資訊.異動量] = (庫存數量 * -1).ToString();
                        使用數量 = 剩餘庫存數量;
                        儲位資訊_buf.Add(儲位資訊[i]);
                    }
                }
            }

            return 儲位資訊_buf;
        }
        public void Function_庫存異動至雲端資料(object[] 儲位資訊)
        {
            object Value = 儲位資訊[(int)enum_儲位資訊.Value];
            string 效期 = 儲位資訊[(int)enum_儲位資訊.效期].ObjectToString();
            string 異動量 = 儲位資訊[(int)enum_儲位資訊.異動量].ObjectToString();
            if (Value is Storage)
            {
                Storage storage = (Storage)Value;
                storage = this.List_EPD266_雲端資料.SortByIP(storage.IP);
                if (storage != null)
                {
                    storage.效期庫存異動(效期, 異動量, false);
                    this.List_EPD266_雲端資料.Add_NewStorage(storage);
                    return;
                }
            }
            else if (Value is Box)
            {
                Box box = (Box)Value;
                box.效期庫存異動(效期, 異動量, false);
                this.List_EPD583_雲端資料.ReplaceBox(box);
            }
            else if (Value is RowsDevice)
            {
                RowsDevice rowsDevice = Value as RowsDevice;
                rowsDevice.效期庫存異動(效期, 異動量, false);
                this.List_RowsLED_雲端資料.Add_NewRowsLED(rowsDevice);
            }
            else if(Value is RFIDDevice)
            {
                RFIDDevice rFIDDevice = Value as RFIDDevice;
                rFIDDevice.效期庫存異動(效期, 異動量, false);
                this.List_RFID_雲端資料.Add_NewRFIDClass(rFIDDevice);
            }

        }




        public string Function_取得藥品網址(string 藥品碼)
        {
            string URL = @"https://wwwhf.vghks.gov.tw/DIIdentify/KH/drugimages/{0}.jpg";
            if (藥品碼.Length < 5) 藥品碼 = "0" + 藥品碼;
            return string.Format(URL, 藥品碼);
        }

        public void Function_從SQL取得儲位到本地資料()
        {

            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(50000);
            Console.WriteLine($"開始SQL讀取儲位資料到本地!");
            List<Task> taskList = new List<Task>();
            taskList.Add(Task.Run(() =>
            {
                MyTimer myTimer0 = new MyTimer();
                myTimer0.StartTickTime(50000);
                List_EPD583_本地資料 = this.drawerUI_EPD_583.SQL_GetAllDrawers();
                Console.WriteLine($"讀取EPD583資料! 耗時 :{myTimer0.GetTickTime().ToString("0.000")} ");
            }));
            taskList.Add(Task.Run(() =>
            {
                MyTimer myTimer1 = new MyTimer();
                myTimer1.StartTickTime(50000);
                List_EPD266_本地資料 = this.storageUI_EPD_266.SQL_GetAllStorage();
                Console.WriteLine($"讀取EPD266資料! 耗時 :{myTimer1.GetTickTime().ToString("0.000")} ");

            }));
            taskList.Add(Task.Run(() =>
            {
                MyTimer myTimer2 = new MyTimer();
                myTimer2.StartTickTime(50000);
                List_RowsLED_本地資料 = this.rowsLEDUI.SQL_GetAllRowsLED();
                Console.WriteLine($"讀取RowsLED資料! 耗時 :{myTimer2.GetTickTime().ToString("0.000")} ");

            }));
            taskList.Add(Task.Run(() =>
            {
                MyTimer myTimer2 = new MyTimer();
                myTimer2.StartTickTime(50000);
                List_RFID_本地資料 = this.rfiD_UI.SQL_GetAllRFIDClass();
                Console.WriteLine($"外部設備資料資料! 耗時 :{myTimer2.GetTickTime().ToString("0.000")} ");

            }));
            taskList.Add(Task.Run(() =>
            {
                MyTimer myTimer2 = new MyTimer();
                myTimer2.StartTickTime(50000);
                List_Pannel35_本地資料 = this.storageUI_WT32.SQL_GetAllStorage();
                Console.WriteLine($"讀取Pannel35資料! 耗時 :{myTimer2.GetTickTime().ToString("0.000")} ");

            }));
            Task allTask = Task.WhenAll(taskList);
            allTask.Wait();
            Console.WriteLine($"SQL讀取儲位資料到本地結束! 耗時 : {myTimer.GetTickTime().ToString("0.000")}");
        }
        public List<object> Function_從本地資料取得儲位(string 藥品碼)
        {
            List<object> list_value = new List<object>();
            List<Box> boxes = List_EPD583_本地資料.SortByCode(藥品碼);
            List<Storage> storages = List_EPD266_本地資料.SortByCode(藥品碼);
            List<RowsDevice> rowsDevices = this.List_RowsLED_本地資料.SortByCode(藥品碼);
            List<RFIDDevice> rFIDDevices = this.List_RFID_本地資料.SortByCode(藥品碼);
            for (int i = 0; i < boxes.Count; i++)
            {
                list_value.Add(boxes[i]);
            }
            for (int i = 0; i < storages.Count; i++)
            {
                list_value.Add(storages[i]);
            }
            for (int i = 0; i < rowsDevices.Count; i++)
            {
                list_value.Add(rowsDevices[i]);
            }
            for (int i = 0; i < rFIDDevices.Count; i++)
            {
                list_value.Add(rFIDDevices[i]);
            }
            return list_value;
        }
        public object Fucnction_從本地資料取得儲位(string IP)
        {
            Storage storage = this.List_EPD266_本地資料.SortByIP(IP);
            if (storage != null) return storage;
            Drawer drawer = this.List_EPD583_本地資料.SortByIP(IP);
            if (drawer != null) return drawer;
            RowsLED rowsLED = this.List_RowsLED_本地資料.SortByIP(IP);
            if (rowsLED != null) return rowsLED;
            RFIDClass rFIDClass = this.List_RFID_本地資料.SortByIP(IP);
            if (rFIDClass != null) return rFIDClass;
            return null;
        }
        public List<Device> Function_從SQL取得所有儲位()
        {
            List<List<Device>> list_list_devices = new List<List<Device>>();
            List<Device> devices = new List<Device>();
            this.Function_從SQL取得儲位到本地資料();
            List<Box> boxes = this.List_EPD583_本地資料.GetAllBoxes();
            list_list_devices.Add(this.List_EPD583_本地資料.GetAllDevice());
            list_list_devices.Add(this.List_EPD266_本地資料.GetAllDevice());
            list_list_devices.Add(this.List_RowsLED_本地資料.GetAllDevice());
            list_list_devices.Add(this.List_RFID_本地資料.GetAllDevice());

            for(int i = 0; i < list_list_devices.Count; i++)
            {
                foreach(Device device in list_list_devices[i])
                {
                    device.確認效期庫存(true);
                    devices.Add(device);
                }
            }
            return devices;
        }
        public List<object> Function_從SQL取得儲位到本地資料(string 藥品碼)
        {
            List<object> list_value = new List<object>();
            List<Box> boxes = this.List_EPD583_本地資料.SortByCode(藥品碼);
            List<Storage> storages = this.List_EPD266_本地資料.SortByCode(藥品碼);
            List<RowsDevice> rowsDevices = this.List_RowsLED_本地資料.SortByCode(藥品碼);
            List<RFIDDevice> rFIDDevices = this.List_RFID_本地資料.SortByCode(藥品碼);

            for (int i = 0; i < boxes.Count; i++)
            {
                Box box = this.drawerUI_EPD_583.SQL_GetBox(boxes[i]);
                this.List_EPD583_本地資料.Add_NewDrawer(box);
                list_value.Add(box);
            }
            for (int i = 0; i < storages.Count; i++)
            {
                Storage storage = this.storageUI_EPD_266.SQL_GetStorage(storages[i]);
                this.List_EPD266_本地資料.Add_NewStorage(storage);
                list_value.Add(storage);
            }
            for (int i = 0; i < rowsDevices.Count; i++)
            {
                RowsDevice rowsDevice = this.rowsLEDUI.SQL_GetRowsDevice(rowsDevices[i]);
                this.List_RowsLED_本地資料.Add_NewRowsLED(rowsDevice);
                list_value.Add(rowsDevice);
            }
            for (int i = 0; i < rFIDDevices.Count; i++)
            {
                RFIDDevice rFIDDevice = this.rfiD_UI.SQL_GetDevice(rFIDDevices[i]);
                list_value.Add(rFIDDevice);
            }
            return list_value;
        }
        public int Function_從SQL取得庫存(string 藥品碼)
        {
            int 庫存 = 0;
            List<object> list_value = this.Function_從SQL取得儲位到本地資料(藥品碼);
            for (int i = 0; i < list_value.Count; i++)
            {

                if (list_value[i] is Device)
                {
                    Device device = list_value[i] as Device;
                    if (device != null)
                    {
                        庫存 += device.Inventory.StringToInt32();
                    }
                }
             
            }
            return 庫存;
        }
        public int Function_從本地資料取得庫存(string 藥品碼)
        {
            int 庫存 = 0;
            List<object> list_value = this.Function_從本地資料取得儲位(藥品碼);
            for (int i = 0; i < list_value.Count; i++)
            {
                if (list_value[i] is Device)
                {
                    Device device = (Device)list_value[i];
                    if (device != null)
                    {
                        庫存 += device.Inventory.StringToInt32();
                    }
                }
            }
            return 庫存;
        }

        public void Function_顯示藥物辨識圖片(string 藥品碼, PictureBox pictureBox)
        {
            if (PLC_Device_藥物辨識圖片顯示.Bool)
            {
                string URL = this.Function_取得藥品網址(藥品碼);
                Basic.Net.DowloadToPictureBox(URL, this.pictureBox_領藥台_01_藥品圖片);
            }
        }
        public string Function_藥品碼檢查(string Code)
        {
            if (Code.Length < 5) Code = "0" + Code;
            if (Code.Length < 5) Code = "0" + Code;
            if (Code.Length < 5) Code = "0" + Code;
            if (Code.Length < 5) Code = "0" + Code;
            if (Code.Length < 5) Code = "0" + Code;
            return Code;
        }


    }


}
