using System.Drawing.Printing;

/*
    * C# �C�L/�w�� �J���о� [�L�RC#���L�MC#���L�w������{]
    
    * C#�L��������D�n�N�O�]�A�G�����]�m�B���L�w���B���L�T�j����

    * WINDOWS���L����z�O�G�ͦ�mdi���A�t�θI��mdi���ɭԷ|�۰ʥH���L���覡�B�z�C�ҥH���ޥΤ���ҪO/����覡�F�u�n��bPrintPage�ƥ�B�z��,�ͦ��@�i�n���L���e���Ϥ��NOK�F

    * C# �C�L/�w�� ����{�D�n�q�LPrintDocument���ӧ����A�t�~�٥]�A�X�ӻ��U���GPrintDialog(���L��ܮ�)�BPrintPreviewDialog(���L�w����ܮ�)�BPageSetupDialog�C
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

        private void button1_Click(object sender, EventArgs e)//�C�L�ȱi�]�w
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

        private void button2_Click(object sender, EventArgs e)//�L����]�w
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

        private void button3_Click(object sender, EventArgs e)//�C�L�w��
        {
            if (MessageBox.Show("�O�_�n�w���C�L�ɮסH", "�w���C�L", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                // �]�m�n�w��������
                printPreviewDialog1.Document = printDocument1;
                // �}�ҧ@�~�t�Ϊ��ܿ����\��
                printPreviewDialog1.UseAntiAlias = true;

                // ���}�w������
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    // �p�G��ܪ��O�t�ιw�]�L����A�I�����C�L�����s����A�|���X���ɥt�s�������f�F
                    // �p�G��ܧO���L����A�I�����C�L������A�|�����C�L�A���|��^��OK���C
                    MessageBox.Show("�}�l�C�L");
                }
                else
                {
                    MessageBox.Show("�����w��");
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)//�}�l�C�L
        {
            // PrintController�G����@��PrintDocument�O�p��C�L���C
            PrintController printController = new StandardPrintController();
            printDocument1.PrintController = printController;
            printDocument1.DocumentName = "���O�˥d";
            //printDocument1.PrinterSettings.PrinterName = "XID8600 U1";
            printDocument1.Print(); // Ĳ�oPrint_Page�ƥ�C
        }

        // PrintDocument �T�Өƥ󤤪��ĤG�ӰѼ� e ���p�U�ݩʡG
        // e.Cancel�G�]�m��true�A�N�����o���C�L�u�@�C
        // e.Griphics�G�ҨϥΦL������]�����ҡC
        // e.HasMorePages�GPrintPage�ƥ�C�L�@����A�p�G������ƥ��C�L�A�h�X�ƥ�e�]�m
        // HasMorePages=true�F�h�X�ƥ󤧫�N�A���X�oPrintPage�ƥ�A�C�L�U�@���C
        // e.MarginBounds�G�C�L�d�򪺤j�p�A�ORectangle���c�A�����]�A���W���y�С]Left�MTop�^�A
        //                 �e�M��(Width�MHeight)�A��쬰1/100�^�o�C
        // e.PageSettings�GPageSettings������A�]�t�ι�ܤ��PageSetupDialog�]�m�������C�L�覡��
        //                ������T�A

        // �b�ե� Print ��k��A�b�C�L���ɪ��Ĥ@�����e�o�͡C
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        // �ݭn�C�L�s���@���ɵo�͡A�t�d�C�L�@���һݭn�����
        // �w���C�L�|�եθӨƥ�A�w�������e�N�O���B�]�m�n�����e�C
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image iamge = Image.FromFile(@"D:\photo\test.jpg");

            e.Graphics.DrawString("���O�d�˥d", new Font("����", 35), Brushes.Black, new Point(400, 120));

            e.Graphics.DrawString("�m�W �i�T", new Font("����", 25), Brushes.Black, new Point(480, 270));
            e.Graphics.DrawString("���|�O�ٸ��X 32032032302030230", new Font("����", 25), Brushes.Black, new Point(480, 360));
            e.Graphics.DrawString("���|�O�٥d�� JS2018098", new Font("����", 25), Brushes.Black, new Point(480, 450));
            e.Graphics.DrawString("��d��� 2016�~5��", new Font("����", 25), Brushes.Black, new Point(480, 540));
            e.Graphics.DrawImage(iamge, new Point(100, 240));
        }

        // �b�C�L���̫�@�����ɮɵo�͡C
        private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }




        private void button5_Click(object sender, EventArgs e)//�L������A
        {
            PrinterSys PrinterSys = new PrinterSys(comboBox1.SelectedItem.ToString());
            textBox1.Text = $"{PrinterSys.getStatus()}";
        }

        private void button6_Click(object sender, EventArgs e)//�Ҧ��L����C��
        {
            PrintDocument print = new PrintDocument();
            string sDefault = print.PrinterSettings.PrinterName;//�w�]�L����W

            foreach (string sPrint in PrinterSettings.InstalledPrinters)//����Ҧ��L����W��
            {
                comboBox1.Items.Add(sPrint);
                if (sPrint == sDefault)
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(sPrint);
            }
        }

    }
}