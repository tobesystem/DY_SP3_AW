using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// ucManagerOut.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucManagerOut : UserControl
    {
        cCommonItemList _carList = new cCommonItemList();
        cCommonItemList _frGubunList = new cCommonItemList();
        cCommonItemList _alcAreaList = new cCommonItemList();

        cCommonData _Commondata = new cCommonData();

        cManagerOutItemList _list = new cManagerOutItemList();
        cManagerOutData _data = new cManagerOutData();

        private int selectedPart = 0; // 0: Hour, 1: Minute, 2: Second
        private int selectedPart2 = 0; // 0: Hour, 1: Minute, 2: Second
        public ucManagerOut()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            DatePicker1.SelectedDate = DateTime.Today;
            DatePicker2.SelectedDate = DateTime.Today;

            timeTextBox.Text = "06:00:00";
            timeTextBox2.Text = DateTime.Now.ToString("HH:mm:ss");

            // 차종
            _carList = _Commondata.getCommonList("ALC_CLASS", "");
            cbCarCode.ItemsSource = _carList;

            // F/R 구분
            _frGubunList = _Commondata.getCommonList("FR_GUBUN", "");
            _frGubunList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            cbFR.ItemsSource = _frGubunList;

            // 툴바 버튼 제어
            ucToolbarBtn.ButtonStates = new Dictionary<int, bool>
            {
                { 1, false }, // 신규
                { 2, true },  // 조회
                { 3, false }, // 저장
                { 4, false }, // 취소
                { 5, false }  // 삭제
            };

            // 툴바 버튼 클릭 이벤트
            ucToolbarBtn.Button2Clicked += ucToolbarBtn_BtnSearch;

            doSearch();
        }

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {
            _list = _data.getManagerOutList(DatePicker1.Text.Replace("-", "") + timeTextBox.Text.Replace(":", ""), DatePicker2.Text.Replace("-", "") + timeTextBox2.Text.Replace(":", ""), txtBank.Text, txtBay.Text, txtLevel.Text, cbCarCode.Text == "전체" ? "" : cbCarCode.Text, txtCode.Text, txtLotNo.Text, cbFR.Text == "전체" ? "" : cbFR.Text);

            managerOutDataGrid.ItemsSource = _list;
            //MessageBox.Show(DatePicker1.Text.Replace("-","") + timeTextBox.Text.Replace(":",""));
        }

        private void outAdd_Click(object sender, RoutedEventArgs e)
        {
            int row = managerOutDataGrid.SelectedIndex;

            //MessageBox.Show(_list[row].PLT);
            if (MessageBox.Show("["+ _list[row].ID_BANK + "-" + _list[row].ID_BAY + "-" + _list[row].ID_LEVEL+ "]"+" 을(를) 출고하시겠습니까?", "출고", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _data.ManagerOutAdd(_list[row].ID_BANK, _list[row].ID_BAY, _list[row].ID_LEVEL);

                //재조회
                doSearch();
            }
            else
            {

            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                doSearch();  // 엔터키를 눌렀을 때 조회 버튼 클릭 이벤트 호출
            }
        }

        #region 일자 관련 메서드
        /// <summary>
        /// 일자 관련 메서드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // 클릭한 위치에 따라 선택된 부분을 결정
            var textBox = sender as TextBox;
            var clickPosition = e.GetPosition(textBox);
            var charIndex = textBox.GetCharacterIndexFromPoint(clickPosition, true);

            if (textBox.Name == "timeTextBox")
            {
                if (charIndex < 3)
                    selectedPart = 0; // Hour
                else if (charIndex < 6)
                    selectedPart = 1; // Minute
                else
                    selectedPart = 2; // Second
            }
            else
            {
                if (charIndex < 3)
                    selectedPart2 = 0; // Hour
                else if (charIndex < 6)
                    selectedPart2 = 1; // Minute
                else
                    selectedPart2 = 2; // Second
            }
        }

        private void IncreaseTimeButton_Click(object sender, RoutedEventArgs e)
        {
            var Button = (Button)sender;

            AdjustTime(1, Button.Name);
        }

        private void DecreaseTimeButton_Click(object sender, RoutedEventArgs e)
        {
            var Button = (Button)sender;

            AdjustTime(-1, Button.Name);
        }

        private void AdjustTime(int increment, string btnName)
        {
            //앞쪽 일자 위/아래 버튼(Name 존재) 클릭시 
            if (btnName != "")
            {
                if (DateTime.TryParse(timeTextBox.Text, out DateTime time))
                {
                    switch (selectedPart)
                    {
                        case 0:
                            time = time.AddHours(increment);
                            break;
                        case 1:
                            time = time.AddMinutes(increment);
                            break;
                        case 2:
                            time = time.AddSeconds(increment);
                            break;
                    }
                    timeTextBox.Text = time.ToString("HH:mm:ss");
                }
                else
                {
                    MessageBox.Show("Invalid time format. Please use HH:mm:ss.");
                }
            }
            else
            {
                if (DateTime.TryParse(timeTextBox2.Text, out DateTime time))
                {
                    switch (selectedPart2)
                    {
                        case 0:
                            time = time.AddHours(increment);
                            break;
                        case 1:
                            time = time.AddMinutes(increment);
                            break;
                        case 2:
                            time = time.AddSeconds(increment);
                            break;
                    }
                    timeTextBox2.Text = time.ToString("HH:mm:ss");
                }
                else
                {
                    MessageBox.Show("Invalid time format. Please use HH:mm:ss.");
                }
            }
        }
        #endregion 
       
    }
}
