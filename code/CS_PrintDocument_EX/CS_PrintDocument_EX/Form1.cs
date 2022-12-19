using System.Drawing.Printing;

/*
    * C# 列印/預覽 入門教學 [淺析C#打印和C#打印預覽的實現]
    
    * C#印表機相關主要就是包括：頁面設置、打印預覽、打印三大部分

    * WINDOWS打印的原理是：生成mdi文件，系統碰到mdi的時候會自動以打印的方式處理。所以不管用什麼模板/什麼方式；只要能在PrintPage事件處理中,生成一張要打印內容的圖片就OK了

    * C# 列印/預覽 的實現主要通過PrintDocument類來完成，另外還包括幾個輔助類：PrintDialog(打印對話框)、PrintPreviewDialog(打印預覽對話框)、PageSetupDialog。
*/
//https://github.com/zhangsanlzh/Solutions/blob/master/Micro.NET/C%23%E5%88%A4%E6%96%AD%E6%89%93%E5%8D%B0%E6%9C%BA%E5%B7%A5%E4%BD%9C%E7%8A%B6%E6%80%81.md
//https://blog.csdn.net/lishimin1012/article/details/102638874

namespace CS_PrintDocument_EX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//列印紙張設定
        {
            PageSetupDialog page = new PageSetupDialog();
            page.Document = printDocument1;
            page.AllowMargins = true;
            page.AllowOrientation = true;
            page.AllowPaper = true;
            page.AllowPrinter = true;
            page.ShowHelp = true;
            if (page.ShowDialog() == DialogResult.OK)
            {
                printDocument1.DefaultPageSettings = page.PageSettings;
            }
        }

        private void button2_Click(object sender, EventArgs e)//印表機設定
        {
            PrintDialog print = new PrintDialog();

            print.Document = printDocument1;
            print.AllowCurrentPage = true;
            print.AllowPrintToFile = true;
            print.AllowSelection = true;
            print.AllowSomePages = true;
            print.ShowHelp = true;
            if (print.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = print.PrinterSettings;
            }
        }

        private void button3_Click(object sender, EventArgs e)//列印預覽
        {
            if (MessageBox.Show("是否要預覽列印檔案？", "預覽列印", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                // 設置要預覽的文檔
                printPreviewDialog1.Document = printDocument1;
                // 開啟作業系統的抗鋸齒功能
                printPreviewDialog1.UseAntiAlias = true;

                // 打開預覽視窗
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    // 如果選擇的是系統預設印表機，點擊“列印”按鈕之後，會跳出“檔另存為”窗口；
                    // 如果選擇別的印表機，點擊“列印”之後，會直接列印，不會返回“OK”。
                    MessageBox.Show("開始列印");
                }
                else
                {
                    MessageBox.Show("關閉預覽");
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)//開始列印
        {
            // PrintController：控制一個PrintDocument是如何列印的。
            PrintController printController = new StandardPrintController();
            printDocument1.PrintController = printController;
            printDocument1.DocumentName = "社保樣卡";
            //printDocument1.PrinterSettings.PrinterName = "XID8600 U1";
            printDocument1.Print(); // 觸發Print_Page事件。
        }

        // PrintDocument 三個事件中的第二個參數 e 有如下屬性：
        // e.Cancel：設置為true，將取消這次列印工作。
        // e.Griphics：所使用印表機的設備環境。
        // e.HasMorePages：PrintPage事件列印一頁後，如果仍有資料未列印，退出事件前設置
        // HasMorePages=true；退出事件之後將再次出發PrintPage事件，列印下一頁。
        // e.MarginBounds：列印範圍的大小，是Rectangle結構，元素包括左上角座標（Left和Top），
        //                 寬和高(Width和Height)，單位為1/100英寸。
        // e.PageSettings：PageSettings類物件，包含用對話方塊PageSetupDialog設置的頁面列印方式的
        //                全部資訊，

        // 在調用 Print 方法後，在列印文檔的第一頁之前發生。
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        // 需要列印新的一頁時發生，負責列印一頁所需要的資料
        // 預覽列印會調用該事件，預覽的內容就是此處設置好的內容。
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image iamge = Image.FromFile(@"D:\photo\test.jpg");

            e.Graphics.DrawString("社保卡樣卡", new Font("黑體", 35), Brushes.Black, new Point(400, 120));

            e.Graphics.DrawString("姓名 張三", new Font("黑體", 25), Brushes.Black, new Point(480, 270));
            e.Graphics.DrawString("社會保障號碼 32032032302030230", new Font("黑體", 25), Brushes.Black, new Point(480, 360));
            e.Graphics.DrawString("社會保障卡號 JS2018098", new Font("黑體", 25), Brushes.Black, new Point(480, 450));
            e.Graphics.DrawString("制卡日期 2016年5月", new Font("黑體", 25), Brushes.Black, new Point(480, 540));
            e.Graphics.DrawImage(iamge, new Point(100, 240));
        }

        // 在列印完最後一頁文檔時發生。
        private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }




        private void button5_Click(object sender, EventArgs e)//印表機狀態
        {
            PrinterSys PrinterSys = new PrinterSys(comboBox1.SelectedItem.ToString());
            textBox1.Text = $"{PrinterSys.getStatus()}";
        }

        private void button6_Click(object sender, EventArgs e)//所有印表機列表
        {
            PrintDocument print = new PrintDocument();
            string sDefault = print.PrinterSettings.PrinterName;//預設印表機名

            foreach (string sPrint in PrinterSettings.InstalledPrinters)//獲取所有印表機名稱
            {
                comboBox1.Items.Add(sPrint);
                if (sPrint == sDefault)
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(sPrint);
            }
        }

    }
}