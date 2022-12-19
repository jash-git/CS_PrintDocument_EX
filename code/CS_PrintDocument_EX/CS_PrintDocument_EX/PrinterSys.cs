using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CS_PrintDocument_EX
{
    // 印表機狀態集合
    public enum enum_printerSys_status
    {
        /// <summary>
        /// 其他狀態
        /// </summary>
        other = 1,

        /// <summary>
        /// 未知
        /// </summary>
        unknown,

        /// <summary>
        /// 空閒
        /// </summary>
        free,

        /// <summary>
        /// 正在列印
        /// </summary>
        print,

        /// <summary>
        /// 預熱
        /// </summary>
        warmup,

        /// <summary>
        /// 停止列印
        /// </summary>
        stop,

        /// <summary>
        /// 列印中
        /// </summary>
        printing,

        /// <summary>
        /// 離線
        /// </summary>
        offline,

    }

    public class PrinterSys : Printer
    {
        public PrinterSys(string name) : base(name)
        {

        }

        /// <summary>
        /// 獲取印表機狀態
        /// </summary>
        /// <returns></returns>
        public enum_printerSys_status getStatus()
        {
            return (enum_printerSys_status)base.getStatus();
        }
    }

    public class Printer
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="name">印表機名稱</param>
        public Printer(string name)
        {
            this.printer_name = name;
        }

        // 設備名：EPSON R330 Series
        private string _printer_name;

        /// <summary>
        /// 印表機名稱
        /// </summary>

        public string printer_name
        {
            get
            {
                return _printer_name;
            }

            set
            {
                _printer_name = value;
            }
        }



        /// <summary>
        /// 獲取印表機狀態
        /// </summary>
        /// <returns></returns>
        public int getStatus()
        {
            string path = @"win32_printer.DeviceId='" + this.printer_name + "'";

            ManagementObject printer = new ManagementObject(path);

            printer.Get();

            return Convert.ToInt32(printer.Properties["PrinterStatus"].Value);
        }
    }

}
