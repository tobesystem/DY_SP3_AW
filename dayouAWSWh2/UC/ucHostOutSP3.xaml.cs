using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dayouAWSWh2.UC
{
    /// <summary>
    /// ucHostOutSP3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucHostOutSP3 : UserControl
    {
        private SerialPort _SP_PRINTER = new SerialPort();
        cHostItemList _hostList = new cHostItemList();
        cHostItemList _hostSubList = new cHostItemList();
        cHostData _hostData = new cHostData();

        private string _BcrForm = "";
        private string _ReplacBcrFrom;

        public ucHostOutSP3()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            // 툴바 버튼 제어
            ucToolbarBtn.ButtonStates = new Dictionary<int, bool>
            {
                { 1, false },  // 신규
                { 2, true },   // 조회
                { 3, false },  // 저장
                { 4, false },  // 취소
                { 5, false }   // 삭제
            };

            // 툴바 버튼 클릭 이벤트
            ucToolbarBtn.Button2Clicked += ucToolbarBtn_BtnSearch;

            // 조회일자 오늘날짜로 
            DatePicker.SelectedDate = DateTime.Now;

            doSearch();

            _BcrForm = "^XA" +
                        "^SEE:UHANGUL.DAT^FS" +
                        "^CW1,E:KFONT3.FNT^CI26^FS" +
                        "^FO35,25^A1N,40,40^FD%COMMIT_NO^FS" +
                        "^FO210,25^A1N,40,40^FD%BODY_NO^FS" +
                        "^FO35,95^A1N,40,40^FD%ALC^FS" +
                        "^FO520,25^A1N,40,40^FD%GUBUN^FS" +
                        "^FO400,95^A1N,40,40^FD%REGION^FS" +
                        "^FO35,165^A1N,40,40^FD%COVER_COLOR^FS" +
                        "^XZ";
        }

        private void doPrint(string BcrForm)
        {
            if (BcrForm != null)
            {
                byte[] buf = Encoding.Default.GetBytes(BcrForm);
                _SP_PRINTER.Write(buf, 0, buf.Length);
            }
            Thread.Sleep(900);
        }

        #region 프린트
        private void SerialOpen()
        {
            string printer_port = ConfigurationManager.AppSettings["PRINTER_PORT"];

            try
            {
                if (_SP_PRINTER.IsOpen)
                    _SP_PRINTER.Close();

                _SP_PRINTER.PortName = printer_port;
                _SP_PRINTER.BaudRate = 9600;
                _SP_PRINTER.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void SerialClose()
        {
            try
            {
                if (_SP_PRINTER.IsOpen)
                    _SP_PRINTER.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        #endregion

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {
            var date = Convert.ToDateTime(DatePicker.Text).ToString("yyyyMMdd");
            _hostList = _hostData.getHostOutListSP3(date);
            dataGrid1.ItemsSource = _hostList;
        }

        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGrid1.SelectedCells.Count > 0)
            {
                DataGridCellInfo cellInfo = dataGrid1.SelectedCells[2]; // 첫 번째 선택된 셀
                if (cellInfo.Column.GetCellContent(cellInfo.Item) is TextBlock cellContent)
                {
                    //MessageBox.Show(cellContent.Text);
                    _hostSubList.Clear();
                    dataGrid2.ItemsSource = null;
                    _hostSubList = _hostData.getHostOutSubListSP3(cellContent.Text);
                    dataGrid2.ItemsSource = _hostSubList;
                }
            }
        }


        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                doSearch();  // 엔터키를 눌렀을 때 조회 버튼 클릭 이벤트 호출
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            SerialOpen();

            for (int i = 0; i < _hostSubList.Count; i++)
            {
                _ReplacBcrFrom = _BcrForm;
                _ReplacBcrFrom = _ReplacBcrFrom.Replace("%COMMIT_NO", _hostSubList[i].COMMIT_NO);
                _ReplacBcrFrom = _ReplacBcrFrom.Replace("%ALC", _hostSubList[i].ALC_CODE);
                _ReplacBcrFrom = _ReplacBcrFrom.Replace("%GUBUN", _hostSubList[i].FR_GUBUN);
                _ReplacBcrFrom = _ReplacBcrFrom.Replace("%REGION", _hostSubList[i].REGION);
                _ReplacBcrFrom = _ReplacBcrFrom.Replace("%COVER_COLOR", _hostSubList[i].COVER_COLOR);
                _ReplacBcrFrom = _ReplacBcrFrom.Replace("%BODY_NO", _hostSubList[i].BODY_NO);

                doPrint(_ReplacBcrFrom);

                //MessageBox.Show(_ReplacBcrFrom);
                _ReplacBcrFrom = "";
            }

            //for(int i =0; i<2; i++)
            //{
            //    doPrint(_BcrForm);

            //}
            SerialClose();
        }
    }
}
