using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace dayouAWSWh2.UC
{
    /// <summary>
    /// ucCellMonitor.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCellMonitor : UserControl
    {
      
        cCell _cItem = new cCell();
        private int selectedPart = 0; // 0: Hour, 1: Minute, 2: Second

        cCellList _cellAlcLst = new cCellList(); // ALC코드 담을 리스트
        cCellList _cellLst = new cCellList();
        cCellData _data = new cCellData();
        cCellInfoList _cellInfoLst = new cCellInfoList();

        private DispatcherTimer _monitorTimer;

        public ucCellMonitor()
        {
            InitializeComponent();

            Init();

        }

        private void Init()
        {
            // 툴바 버튼 제어
            ucToolbarBtn.ButtonStates = new Dictionary<int, bool>
            {
            { 1, false },
            { 2, false },
            { 3, true },
            { 4, false },
            { 5, false }
            };

            // 툴바 버튼 클릭 이벤트
            ucToolbarBtn.Button3Clicked += ucToolbarBtn_Button3Clicked;

            var userControls2 = new[] { ucCell05, ucCell06, ucCell07, ucCell08 };
            foreach (var userControl in userControls2)
            {
                userControl.ButtonClicked += ucCell_ButtonClicked;
            }

            // CELL 상태를 가져옴
            _cellLst = _data.GetCellStatus("1");
            // _cCellLst에서 STATUS 값을 가져와 리스트로 변환
            var statusList = _cellLst.Select(item => item.STATUS).ToList();
            // ComboBox에 리스트 바인딩
            cbStatus.ItemsSource = statusList;

            // ALC 코드 가져옴
            _cellAlcLst = _data.GetAlcCode("2");
            var alcList = _cellAlcLst.Select(item => item.ALC).ToList();
            cbAlc.ItemsSource = alcList;

            //FR 구분을 가져옴
            _cellLst = _data.GetCellStatus("3");
            var frList = _cellLst.Select(item => item.FRCODE).ToList();
            cbFrgubun.ItemsSource = frList;


            // ucCell 인스턴스를 찾고 이벤트 구독 (SP3)
            for (int i = 5; i <= 8; i++)
            {
                var cell = FindName($"ucCell0{i}") as ucCellSp3;
                if (cell != null)
                    cell.StatusClicked += UpdateTextBlock;
            }

            tbTime.Text = DateTime.Now.ToString("HH:mm:ss");

            // 화면을 계속 refresh 한다.
            StartMonitorTimer();
        }

        private void StartMonitorTimer()
        {
            _monitorTimer = new DispatcherTimer();
            _monitorTimer.Interval = TimeSpan.FromSeconds(1);
            _monitorTimer.Tick += MonitorTimer_Tick;
            _monitorTimer.Start();
        }

        // async void 는 이벤트 핸들러에서만 사용
        private async void MonitorTimer_Tick(object sender, EventArgs e)
        {
            // 필요하면 중복 호출 방지용 플래그 써도 됨
            _monitorTimer.Stop();
            try
            {
                //  여기까지는 UI 스레드

                //  무거운 작업은 백그라운드에서 실행
                var cellInfoLst = await Task.Run(() => _data.GetCellInfo());

                //  await 이후는 다시 UI 스레드로 돌아옴
                _cellInfoLst = cellInfoLst;
                dgCellInfo.ItemsSource = _cellInfoLst;
                //  SetCellUpdate();      // UI 갱신 관련 작업
                await SetCellUpdateAsync();
            }
            finally
            {
                _monitorTimer.Start();
            }
        }

        // 셀 초기화
        private async Task SetCellUpdateAsync()
        {
            // 동시에 돌려도 되면 (병렬)
            await Task.WhenAll(
                ucCell05.SetRowAsync("05"),
                ucCell06.SetRowAsync("06"),
                ucCell07.SetRowAsync("07"),
                ucCell08.SetRowAsync("08")
            );

            // 만약 순서대로 천천히 하고 싶으면 대신에:
            /*
            await ucCell01.SetRowAsync("01");
            await ucCell02.SetRowAsync("02");
            ...
            */
        }
      
        /* private void SetCellUpdate()
         {
             ucCell01.setRow("01");
             ucCell02.setRow("02");
             ucCell03.setRow("03");
             ucCell04.setRow("04");
             ucCell05.setRow("05");
             ucCell06.setRow("06");
             ucCell07.setRow("07");
             ucCell08.setRow("08");
         }*/

        // 선택한 셀 정보 적용
        private void UpdateTextBlock(cCell item)
        {
            if (Dispatcher.CheckAccess()) // 현재 스레드가 UI 스레드인지 확인
            {
                ApplyTextBlockValues(item);
            }
            else
            {
                Dispatcher.Invoke(() => ApplyTextBlockValues(item)); // UI 스레드에서 실행
            }
        }

        private void ApplyTextBlockValues(cCell item)
        {
            _cItem = item;

            // ALC 코드 가져옴
            _cellAlcLst = _data.GetAlcCodeBank(_cItem.ID_BANK);
            var alcList = _cellAlcLst.Select(items => items.ALC).ToList();
            cbAlc.ItemsSource = alcList;

            if (tbBank.Text != _cItem.ID_BANK) tbBank.Text = _cItem.ID_BANK;
            if (tbBay.Text != _cItem.ID_BAY) tbBay.Text = _cItem.ID_BAY;
            if (tbLevel.Text != _cItem.ID_LEVEL) tbLevel.Text = _cItem.ID_LEVEL;
            if (cbStatus.SelectedValue?.ToString() != _cItem.STATUS) cbStatus.SelectedValue = _cItem.STATUS;
            if (cbAlc.SelectedValue?.ToString() != _cItem.ALC) cbAlc.SelectedValue = _cItem.ALC;
            if (cbFrgubun.SelectedValue?.ToString() != _cItem.FRCODE) cbFrgubun.SelectedValue = _cItem.FRCODE;

            if (txtItemCode1.Text != _cItem.ITEM_CODE1) txtItemCode1.Text = _cItem.ITEM_CODE1;
            if (txtItemCode2.Text != _cItem.ITEM_CODE2) txtItemCode2.Text = _cItem.ITEM_CODE2;
            if (txtItemCode3.Text != _cItem.ITEM_CODE3) txtItemCode3.Text = _cItem.ITEM_CODE3;
            if (txtItemCode4.Text != _cItem.ITEM_CODE4) txtItemCode4.Text = _cItem.ITEM_CODE4;
            if (txtItemCode5.Text != _cItem.ITEM_CODE5) txtItemCode5.Text = _cItem.ITEM_CODE5;

            if (tbLotno1.Text != _cItem.LOT_NO1) tbLotno1.Text = _cItem.LOT_NO1;
            if (tbLotno2.Text != _cItem.LOT_NO2) tbLotno2.Text = _cItem.LOT_NO2;
            if (tbLotno3.Text != _cItem.LOT_NO3) tbLotno3.Text = _cItem.LOT_NO3;
            if (tbLotno4.Text != _cItem.LOT_NO4) tbLotno4.Text = _cItem.LOT_NO4;
            if (tbLotno5.Text != _cItem.LOT_NO5) tbLotno5.Text = _cItem.LOT_NO5;

            if (tbPltcode.Text != _cItem.PLT) tbPltcode.Text = _cItem.PLT;

            SetDateTimeValues(_cItem.ID_DATE, _cItem.ID_TIME);

            // 포커스 제거
            Keyboard.ClearFocus();
        }

        private void dgCellInfo_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null)
            {
                int rowIndex = e.Row.GetIndex();

                // 마지막 행인지 확인
                if (e.Row.GetIndex() == dataGrid.Items.Count - 1)
                {
                    e.Row.Foreground = Brushes.Red;
                }
                // 5번째(인덱스 4), 10번째(인덱스 9) 행 색상
                else if (rowIndex == 4 || rowIndex == 9)
                {
                    e.Row.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4C6EF5"));
                }
            }
        }

        private void ucToolbarBtn_Button3Clicked(object sender, EventArgs e)
        {
            addCellInfo();
        }

        // 저장버튼 클릭
        private void addCellInfo()
        {
            MessageBoxResult result = MessageBox.Show("저장하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // 사용자가 "Yes"를 선택한 경우에만 실행
            if (result == MessageBoxResult.Yes)
            {
                _data.AddCellInfo(_cItem.ID_CODE, cbStatus.Text, cbAlc.Text, cbFrgubun.Text, tbLotno1.Text, tbLotno2.Text, tbLotno3.Text,
                    tbLotno4.Text, tbLotno5.Text, tbPltcode.Text, dtIndate.Text.Replace("-", ""), tbTime.Text.Replace(":", ""));

                doClear();
            }
        }

        #region 입고시간 관리 메서드
        private void TimeTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // 클릭한 위치에 따라 선택된 부분을 결정
            var textBox = sender as TextBox;
            var clickPosition = e.GetPosition(textBox);
            var charIndex = textBox.GetCharacterIndexFromPoint(clickPosition, true);

            if (charIndex < 3)
                selectedPart = 0; // Hour
            else if (charIndex < 6)
                selectedPart = 1; // Minute
            else
                selectedPart = 2; // Second
        }
        private void SetDateTimeValues(string dateString, string timeString)
        {
            // 입고시간을 datetime 형식으로 변경
            DateTime parsedDate = ParseDate(dateString);
            DateTime parsedTime = ParseTime(timeString);

            dtIndate.SelectedDate = parsedDate;
            tbTime.Text = parsedTime.ToString("HH:mm:ss");
        }

        private DateTime ParseDate(string dateString)
        {
            if (DateTime.TryParseExact(dateString, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                return parsedDate;

            return DateTime.Today; // 변환 실패 시 오늘 날짜 반환
        }

        private DateTime ParseTime(string timeString)
        {
            if (DateTime.TryParseExact(timeString, "HHmmss", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime))
                return parsedTime;

            return DateTime.Now; // 변환 실패 시 현재 시간 반환
        }



        private void dtIndate_Loaded(object sender, RoutedEventArgs e)
        {
            dtIndate.SelectedDate = DateTime.Today;
        }


        private void timeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            // 숫자와 콜론(:)만 허용
            Regex regex = new Regex(@"^[0-9:]$");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
                return;
            }
        }

        private void timeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateAndFixTime();
        }

        private void ValidateAndFixTime()
        {
            string[] parts = tbTime.Text.Split(':');
            if (parts.Length != 3)
            {
                tbTime.Text = "00:00:00";
                return;
            }

            if (int.TryParse(parts[0], out int hour) &&
                int.TryParse(parts[1], out int minute) &&
                int.TryParse(parts[2], out int second))
            {
                // 시(hour)가 23 초과하면 0으로 설정
                if (hour > 23) hour = 0;
                // 분(minute), 초(second)가 60 이상이면 0으로 설정
                if (minute >= 60) minute = 0;
                if (second >= 60) second = 0;

                // 수정된 값 반영
                tbTime.Text = $"{hour:D2}:{minute:D2}:{second:D2}";
            }
            else
            {
                tbTime.Text = "00:00:00"; // 유효하지 않으면 기본값 설정
            }
        }

        private void IncreaseTimeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustTime(1);
        }
        private void DecreaseTimeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustTime(-1);
        }
        private void AdjustTime(int increment)
        {
            if (DateTime.TryParse(tbTime.Text, out DateTime time))
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
                tbTime.Text = time.ToString("HH:mm:ss");
            }
            else
            {
                MessageBox.Show("Invalid time format. Please use HH:mm:ss.");
            }
        }

        #endregion

        #region 초기화

        private void ucCell_ButtonClicked(object sender, EventArgs e)
        {
            doClear();
        }

        private void doClear()
        {
            tbBank.Text = "";
            tbBay.Text = "";
            tbLevel.Text = "";
            cbStatus.Text = "";
            cbAlc.Text = "";
            cbFrgubun.Text = "";

            txtItemCode1.Text = "";
            txtItemCode2.Text = "";
            txtItemCode3.Text = "";
            txtItemCode4.Text = "";
            txtItemCode5.Text = "";
            tbLotno1.Text = "";
            tbLotno2.Text = "";
            tbLotno3.Text = "";
            tbLotno4.Text = "";
            tbLotno5.Text = "";
            tbPltcode.Text = "";

            dtIndate.SelectedDate = DateTime.Today;
            tbTime.Text = DateTime.Now.ToString("HH:mm:ss");

        }

        #endregion

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _monitorTimer.Stop();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _monitorTimer.Start();
        }

    }
}
