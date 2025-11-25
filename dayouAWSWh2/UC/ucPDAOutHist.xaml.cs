using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using System;
using System.Collections;
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
    /// ucPDAOutHist.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucPDAOutHist : UserControl
    {
        cPDAWmsOutList _list = new cPDAWmsOutList();
        cWmsData _data = new cWmsData();
        private int selectedPart = 0; // 0: Hour, 1: Minute, 2: Second
        private int selectedPart2 = 0; // 0: Hour, 1: Minute, 2: Second
        public ucPDAOutHist()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
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
            DatePicker2.SelectedDate = DateTime.Now;

            timeTextBox.Text = "06:00:00";
            timeTextBox2.Text = DateTime.Now.ToString("HH:mm:ss");

            doSearch();
        }

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {
            _list = _data.PDAWmsOutHist(Convert.ToDateTime(DatePicker.Text).ToString("yyyyMMdd")+ timeTextBox.Text.Replace(":" , ""), Convert.ToDateTime(DatePicker2.Text).ToString("yyyyMMdd")+ timeTextBox2.Text.Replace(":", ""), txtBodyNo.Text, txtLotNo.Text);

            PDAWmsOutDataGrid.ItemsSource = _list;
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

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                doSearch();  // 엔터키를 눌렀을 때 조회 버튼 클릭 이벤트 호출
            }
        }
    }
}
