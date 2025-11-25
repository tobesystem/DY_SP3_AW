using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using dayouAWSWh2.UC;
using dayouAWSWh2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace dayouAWSWh2
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _alarmTimer = new DispatcherTimer(); // 알람표시 타이머
        cAlarmItem _alarmItem = new cAlarmItem();
        cAlarmData _alarmData = new cAlarmData();
        winAlarm _winAlarm;
        private bool _isAlarmShown = false; // 알림이 표시되었는지 추적하는 플래그
        private double _alarmTime = 0;

        public MainWindow()
        {
            winLogin _winLogin = new winLogin();
            _winLogin.ShowDialog();
            if (_winLogin.DialogResult == true)
            {
                _winLogin.Close();
            }

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ShowVersion(); 

            _alarmTime = 1;
            _alarmTimer.Interval = TimeSpan.FromMinutes(_alarmTime); // 1분 간격 설정
            _alarmTimer.Tick += alarmTimer_Tick;
            _alarmTimer.Start();

            AddTab("출고 진행 현황", new ucWmsOut());
            txtUser.Text = Properties.Settings.Default.USER_NAME;
        }

        private void alarmTimer_Tick(object sender, EventArgs e)
        {
            _alarmItem = _alarmData.getAlarm();
            if (_alarmItem.Current_Desc != null)
            {
                _winAlarm = new winAlarm(_alarmTime);
                _winAlarm.ShowDialog();
            }
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = FileBtn.IsChecked == true ? "Images/위화살표흰.png" : "Images/아래화살표흰.png";
            FileArrowIcon.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            if (FileBtn.IsChecked == true)
            {
                FileSubMenu.Visibility = Visibility.Visible;
            }
            else
            {
                FileSubMenu.Visibility = Visibility.Collapsed; // 닫기
            }
        }


        // 모든 메인 메뉴 닫기
        private void btnAllClose_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.Items.Clear();
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            AddTab("시스템 설정", new ucSystemSetting());
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("로그아웃 하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

                // 새로운 프로세스 시작 (자기 자신)
                System.Diagnostics.Process.Start(exePath);

                // 현재 애플리케이션 종료
                Application.Current.Shutdown();
            }
        }

        private void InOutBtn(object sender, RoutedEventArgs e)
        {
            AddTab("입출고 진행 현황", new ucWmsInOutGet());
        }

        private void InBtn(object sender, RoutedEventArgs e)
        {
            AddTab("입고 진행 현황", new ucWmsIn());
        }

        private void OutBtn(object sender, RoutedEventArgs e)
        {
            AddTab("출고 진행 현황", new ucWmsOut());
        }

        private void ManageBtn(object sender, RoutedEventArgs e)
        {
            AddTab("지정 출고 작업", new ucManagerOut());
        }

        private void ManualOrderBtn(object sender, RoutedEventArgs e)
        {
            AddTab("수동 서열 출고", new ucManualOut());
        }

        private void HostBtn(object sender, RoutedEventArgs e)
        {
            AddTab("의장 정보 현황", new ucHost());
        }

        private void ProdBtn(object sender, RoutedEventArgs e)
        {
            AddTab("도장 정보 현황", new ucProd());
        }
        private void OrderWaitBtn(object sender, RoutedEventArgs e)
        {
            AddTab("서열출고 대기명령", new ucOrderWait());
        }
        
        private void PDAOutBtn(object sender, RoutedEventArgs e)
        {
            AddTab("PDA 출고점검", new ucPDAOut());
        }

        private void PDAOutHitsBtn(object sender, RoutedEventArgs e)
        {
            AddTab("PDA 출고점검이력", new ucPDAOutHist());
        }
        private void CellMonitor(object sender, RoutedEventArgs e)
        {
            AddTab("셀별 모니터링", new ucCellMonitor());
        }
        private void CodeStockBtn(object sender, RoutedEventArgs e)
        {
            AddTab("적재코드별 재고 현황", new ucCodeStock());
        }

        private void LocStockBtn(object sender, RoutedEventArgs e)
        {
            AddTab("적재위치별 재고 현황", new ucLocStock());
        }

        private void TotalJob(object sender, RoutedEventArgs e)
        {
            AddTab("입고대 실적조회", new ucTotalJob());
        }

        private void TimeStockBtn(object sender, RoutedEventArgs e)
        {
            AddTab("시간별 실적조회", new ucTimeStock());
        }

        private void DateStockBtn(object sender, RoutedEventArgs e)
        {
            AddTab("기간별 실적조회", new ucDateStock());
        }

        private void InResult(object sender, RoutedEventArgs e)
        {
            AddTab("입고실적 조회", new ucWmsInResult());
        }

        private void OutResult(object sender, RoutedEventArgs e)
        {
            AddTab("출고실적 조회", new ucWmsOutResult());
        }

        private void LoadPerfBtn(object sender, RoutedEventArgs e)
        {
            AddTab("적재별 실적 조회", new ucLoadPerf());
        }

        private void LoadItemPerfBtn(object sender, RoutedEventArgs e)
        {
            AddTab("품목별 실적 조회", new ucLoadItemPerf());
        }

        private void HostOutBtn(object sender, RoutedEventArgs e)
        {
            AddTab("의장 출고 내역", new ucHostOut());
        }

        private void ProdResultBtn(object sender, RoutedEventArgs e)
        {
            AddTab("도장 실적 조회", new ucProdResult());
        }

        private void HostResultBtn(object sender, RoutedEventArgs e)
        {
            AddTab("의장 실적 조회", new ucHostResult());
        }

        private void PalletBtn(object sender, RoutedEventArgs e)
        {
            AddTab("파레트 코드관리", new ucPallet());
        }

        private void UserInfoBtn(object sender, RoutedEventArgs e)
        {
            AddTab("사용자 관리", new ucUser());
        }

        private void ErrorBtn(object sender, RoutedEventArgs e)
        {
            AddTab("에러코드 관리", new ucError());
        }
        private void TrackingMonitorSP3(object sender, RoutedEventArgs e)
        {
            AddTab("SP3 설비 모니터링", new ucEquipMonitorSP3());
        }

        private void ErrorHistory(object sender, RoutedEventArgs e)
        {
            AddTab("설비 에러 조회", new ucErrorHistory());
        }

        private void AddTab(string tabHeader, UserControl content)
        {
            if (MainTabControl.Items.Count == 10)
            {
                MessageBox.Show("탭은 최대 10개까지만 사용 가능합니다.");
                return;
            }
            // 기존 탭 중 동일한 Tag를 가진 탭이 있는지 확인
            foreach (var item in MainTabControl.Items)
            {
                if (item is TabItem tabItem && tabItem.Tag?.ToString() == tabHeader)
                {
                    // 이미 존재하는 탭이 있다면 해당 탭을 선택하고 함수 종료
                    MainTabControl.SelectedItem = tabItem;
                    return;
                }
            }

            // 동일한 Tag를 가진 탭이 없으므로 새 탭 추가
            TabItem newTab = new TabItem
            {
                Header = CreateTabHeader(tabHeader), // 탭 헤더 UI
                Content = content, // UserControl을 탭 내용으로 설정
                Tag = tabHeader, // 고유 식별자로 사용할 Tag 설정

                //   Style = (Style)FindResource("UniformTabItemStyle") // 스타일 적용
            };

            MainTabControl.Items.Add(newTab);
            MainTabControl.SelectedItem = newTab; // 새 탭을 선택 상태로 설정
        }

        private StackPanel CreateTabHeader(string tabHeader)
        {
            StackPanel headerPanel = new StackPanel { Orientation = Orientation.Horizontal };

            TextBlock headerText = new TextBlock { Text = tabHeader, Margin = new Thickness(5, 0, 5, 0) };

            // 닫기 버튼 생성
            Button closeButton = new Button
            {
                Content = "  ⨉",
                FontSize = 13,
                FontWeight = FontWeights.Normal,
                Width = 20,
                Margin = new Thickness(5, 0, 5, 0),
                Tag = tabHeader, // 닫기 버튼에도 고유 식별자를 저장
                Style = (Style)FindResource("FlatButtonStyle") // 스타일 적용
            };

            // 닫기 버튼 클릭 이벤트 핸들러
            closeButton.Click += (s, e) =>
            {
                // Tag를 이용해 탭 제거
                var parentTab = MainTabControl.Items
                    .Cast<TabItem>()
                    .FirstOrDefault(tab => tab.Tag?.ToString() == closeButton.Tag?.ToString());

                if (parentTab != null)
                {
                    MainTabControl.Items.Remove(parentTab);
                }
            };

            headerPanel.Children.Add(headerText);
            headerPanel.Children.Add(closeButton);

            return headerPanel;
        }

        private void setBtn_Click(object sender, RoutedEventArgs e)
        {
            WinSet winset = new WinSet();
            winset.ShowDialog();
        }

        private void CodeBtn_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = CodeBtn.IsChecked == true ? "Images/위화살표흰.png" : "Images/아래화살표흰.png";
            CodeArrowIcon.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            if (CodeBtn.IsChecked == true)
            {
                CodeSubMenu.Visibility = Visibility.Visible;
            }
            else
            {
                CodeSubMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void ProgressBtn_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = ProgressBtn.IsChecked == true ? "Images/위화살표흰.png" : "Images/아래화살표흰.png";
            ProgressArrowIcon.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            if (ProgressBtn.IsChecked == true)
            {
                ProgressSubMenu.Visibility = Visibility.Visible;
            }
            else
            {
                ProgressSubMenu.Visibility = Visibility.Collapsed;
            }
        }

        //재고관리 
        private void StockManageBtn_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = StockManageBtn.IsChecked == true ? "Images/위화살표흰.png" : "Images/아래화살표흰.png";
            StockManageArrowIcon.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            if (StockManageBtn.IsChecked == true)
            {
                StockManageSubMenu.Visibility = Visibility.Visible;
            }
            else
            {
                StockManageSubMenu.Visibility = Visibility.Collapsed;
            }
        }

        //실적관리
        private void RecordManageBtn_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = RecordManageBtn.IsChecked == true ? "Images/위화살표흰.png" : "Images/아래화살표흰.png";
            RecordManageArrowIcon.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            if (RecordManageBtn.IsChecked == true)
            {
                RecordManageSubMenu.Visibility = Visibility.Visible;
            }
            else
            {
                RecordManageSubMenu.Visibility = Visibility.Collapsed;
            }
        }

        //모니터링
        private void MonitoringBtn_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = MonitoringBtn.IsChecked == true ? "Images/위화살표흰.png" : "Images/아래화살표흰.png";
            MonitoringArrowIcon.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            if (MonitoringBtn.IsChecked == true)
            {
                MonitoringSubMenu.Visibility = Visibility.Visible;
            }
            else
            {
                MonitoringSubMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 작업 표시줄을 제외한 실제 사용 가능한 화면 크기로 설정
            this.Left = SystemParameters.WorkArea.Left;
            this.Top = SystemParameters.WorkArea.Top;
            this.Width = SystemParameters.WorkArea.Width;
            this.Height = SystemParameters.WorkArea.Height;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("종료하시겠습니까?", "종료 확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // 버전 업데이트
        private void ShowVersion()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;
            txtVer.Text = "ver " + ver.ToString();
        }
    }
}

