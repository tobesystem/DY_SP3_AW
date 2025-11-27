using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// ucEquipMonitorSP3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucEquipMonitorSP3 : UserControl
    { 
        // 스태커크레인 정보
        cSccData _sccData = new cSccData();
        cSccList _sccNo = new cSccList(); // 호기
        cSccList _sccLoc1 = new cSccList(); // 1호키 크레인 위치 리스트
        cSccList _sccLoc2 = new cSccList(); // 2호키 크레인 위치 리스트
        cSccList _sccLst = new cSccList(); // 크레인 정보
        cSccScioItem _sccScio = new cSccScioItem();

        private int sccNo = 0;

        // 통신상태 조회
        cComStatus _comStatus = new cComStatus();
        cComData _comData = new cComData();

        // 적재코드
        cAlcList _alcLst = new cAlcList();


        // 컨베이어
        cCvc _cvcResult = new cCvc();
        cCvcList _list = new cCvcList();
        cCvcData _data = new cCvcData();

        cTrack _track = new cTrack();
        cTrackList _pltList = new cTrackList(); // 입,출고 파레트코드
        cTrackData _trackData = new cTrackData();

        // 바코드 파레트코드
        cBcrrList _bcrLst = new cBcrrList();
        cBcrrData _bcrData = new cBcrrData();

        // 에러정보 조회
        cErrorItemList _errorLst = new cErrorItemList();
        cErrorData _errorData = new cErrorData();

        // 의장도장 조회
        cHostProdItem _hostItem = new cHostProdItem();
        cHostProdData _hostData = new cHostProdData();

        // 방향 정보 조회
        cArrowList _arrowLst = new cArrowList();
        cArrowData _arrowData = new cArrowData();

        private bool isTxtBuffTargetSelected = false;

        private List<string> _bufferNames = new List<string>();

        // OP,MOP 정보 조회
        cOpList _opList = new cOpList();
        cOpData _opData = new cOpData();

        private DispatcherTimer _cvTimer;
        private bool _isCvTimerRunning = false;

       // private Timer _timer;
        private const double sectionHeight = 20; // SCC 구간 간격

        private static readonly Brush CargoBrushYes = new SolidColorBrush(Colors.LimeGreen);
        private static readonly Brush CargoBrushNo = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF0070C0");
        private static readonly Brush ModeBrushOk = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFB9FFB9");
        private static readonly Brush ModeBrushError = new SolidColorBrush(Colors.Red);

        private static readonly SolidColorBrush ReadyBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF15AF00"));
        private static readonly SolidColorBrush ErrorBrush = new SolidColorBrush(Colors.Red);
        private static readonly SolidColorBrush NormalBrush = new SolidColorBrush(Colors.LightGreen);
        private static readonly SolidColorBrush IdleBrush = new SolidColorBrush(Colors.Yellow);
        public ucEquipMonitorSP3()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            // 툴바
            SetupToolbar();
            //콤보박스
           SetupComboBoxes();
            // UI컨트롤
            SetupUIControls();
            //CV제어
            StartCvTimer();
            //CvTimer();
        }

        private void SetupToolbar()
        {
            ucToolbarBtn.ButtonStates = new Dictionary<int, bool>
            {
                { 1, false },
                { 2, false },
                { 3, false },
                { 4, false },
                { 5, false }
            };
        }

        private void SetupComboBoxes()
        {
            // 적재코드 바인딩
            _alcLst = _data.AlcCodeGet();
            cbLoadCode.ItemsSource = _alcLst.Where(item => item.ALC_CLASS == "SP3").Select(item => item.ALC_CODE).ToList();

            // 스태커크레인 호기 바인딩
            _sccNo = _sccData.GetSccNo();
            cbSccNo.ItemsSource = _sccNo;
            cbSccNo.DisplayMemberPath = "SCC_NAME";
            cbSccNo.SelectedValuePath = "SCC_NO";
        }

        private void SetupUIControls()
        {
            // 버튼 비활성화
            btnCvGo.IsEnabled = false;
            btnCvGoComp.IsEnabled = false;
            btnCvQua.IsEnabled = false;
            btnCvQuaComp.IsEnabled = false;
            btnCvJoin.IsEnabled = false;
            btnCvJoinComp.IsEnabled = false;

            // 바코드 수동 입력창 숨김
            stpBcrInput.Visibility = Visibility.Hidden;
        }

        private void StartCvTimer()
        {
            _cvTimer = new DispatcherTimer();
            _cvTimer.Interval = TimeSpan.FromSeconds(1);
            _cvTimer.Tick += CvTimer_Tick;
            _cvTimer.Start();
        }
        private async void CvTimer_Tick(object sender, EventArgs e)
        {
            // 중복 실행 방지
            if (_isCvTimerRunning) return;
            _isCvTimerRunning = true;

            try
            {
                // 백그라운드에서 DB 통신
                var result = await Task.Run(() =>
                {
                    // 로컬 변수로 다 받아오기
                    var comStatus = _comData.ComStatusGet();
                    var list = _data.GetCvc();
                    var sccLoc1 = _sccData.GetSccData(3);
                    var sccLoc2 = _sccData.GetSccData(4);
                    var arrowLst = _arrowData.ArrowGet();
                    var pltList = _trackData.TrackPltGet();
                    var bcrLst = _bcrData.BcrPltGet();
                    var errorLst = _errorData.CvErrorGet();
                    var hostItem = _hostData.HostProdGet();
                    var opList = _opData.OpGet();

                    // 익명 타입으로 묶어서 리턴 (클래스 안 만들어도 됨)
                    return new
                    {
                        comStatus,
                        list,
                        sccLoc1,
                        sccLoc2,
                        arrowLst,
                        pltList,
                        bcrLst,
                        errorLst,
                        hostItem,
                        opList
                    };
                });

                //  await 이후: 다시 UI 스레드
                //    여기서만 필드/컨트롤 건드리기
                _comStatus = result.comStatus;
                _list = result.list;
                _sccLoc1 = result.sccLoc1;
                _sccLoc2 = result.sccLoc2;
                _arrowLst = result.arrowLst;
                _pltList = result.pltList;
                _bcrLst = result.bcrLst;
                _errorLst = result.errorLst;
                _hostItem = result.hostItem;
                _opList = result.opList;

                // 실제 화면 갱신
                TimerUpdate();   // 여기 안에서 UI 컨트롤 자유롭게 사용 가능
            }
            catch (Exception ex)
            {
                // 로그만 찍어주면 됨
                // doLog(ex.ToString());
            }
            finally
            {
                _isCvTimerRunning = false;
            }
        }
     
        private void TimerUpdate()
        {
            //통신상태 업데이트
            UpdateComStatus();
            // 버퍼업데이트
            UpdateBorders();
            //스태커 크레인 제어
            UpdateScc();
            // 화살표 제어
            UpdateArrow();
            //바코드 리딩 정보
            UpdatePalletCheck();
            UpdateBcrCheck();
            //컨베이어 에러정보
            UpdateErrorList();
            // 의장 수신정보
            UpdateHostProdInfo();
            // OP/MOP 제어
            UpdateOp();
        }

        private void UpdateComStatus()
        {
            bdCvcStatus.Background = GetStatusBrush(_comStatus.CVC);
            bdSccStatus.Background = GetStatusBrush(_comStatus.SCC);
            bdBr1Status.Background = GetStatusBrush(_comStatus.BR1);
            bdBr2Status.Background = GetStatusBrush(_comStatus.BR2);
        }

        private Brush GetStatusBrush(string status)
        {
            return status == "OK"
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00FF1B"))
                : new SolidColorBrush(Colors.Red);
        }

        // Visual Tree에서 특정 타입의 자식 요소 찾기
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) yield break;

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            //    Debug.WriteLine($"Parent: {parent}, Child Count: {childCount}"); // 디버깅용 출력

            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild)
                {
                    yield return tChild;
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }

        private void UpdateBorders()
        {

            foreach (var border in FindVisualChildren<Border>(this))
            {
                string name = border.Name;
                string processed = name.Replace("s", "").Split('_')[0];

                if (!_bufferNames.Contains(processed))
                    _bufferNames.Add(processed);

                var match = _list.FirstOrDefault(x => x.BUFF_NO == processed);
                if (match == null) continue;

                // 배경색 설정 
                var newBrush = GetBackgroundColor(match.ERROR_VALUE, match.CARGO_VALUE, match.DATA_VALUE);
                SetBrushIfChanged(border, newBrush);

                // 텍스트 색상
                SetTextBlockColor(border,
                  (match.ERROR_VALUE == "1" || (match.CARGO_VALUE == "1" && match.DATA_VALUE == "1"))
                  ? Brushes.White : Brushes.Black);

                // 태그 및 특수 하이라이트 처리
                UpdateBufferTag(border, processed, match);

                // Ready 상태 처리
                UpdateReadyStatus(processed, match);
            }
        }

        private Brush GetBackgroundColor(string errorValue, string cargoValue, string dataValue)
        {

            if (errorValue == "1")
                return Brushes.Red;

            if (cargoValue == "1" && dataValue == "1") return (SolidColorBrush)new BrushConverter().ConvertFrom("#FF4C6EF5");
            if (cargoValue == "1" && dataValue == "0") return (SolidColorBrush)new BrushConverter().ConvertFrom("#FF57D46E");
            if (cargoValue == "0" && dataValue == "1") return (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF9D594");
            if (cargoValue == "0" && dataValue == "0") return Brushes.White;

            return Brushes.LightGray;
        }

        private void SetBrushIfChanged(Border border, Brush newBrush)
        {
            if (!(border.Background is SolidColorBrush current) ||
                current.Color != ((SolidColorBrush)newBrush).Color)
            {
                border.Background = newBrush;
            }
        }

        private void UpdateArrow()
        {
            var arrowMap = new Dictionary<int, UIElement>
            {
                { 0, arow543 },
                { 1, arow587 },
                { 3, arow565 },
                { 4, arow566_down },
                { 5, arow566 },
                { 6, arow560 },
                { 7, arow561 },
                { 8, arow546 },
                { 9, arow611 },
                { 12, arow708 },
                { 13, arow706 },
                { 14, arow713 },
                { 15, arow714 },
            };

            foreach (var pair in arrowMap)
            {
                var arrowItem = _arrowLst.FirstOrDefault(x => x.WBIT == pair.Key);
                SetArrowVisibility(pair.Value, arrowItem?.NOW_VALUE == 1);
            }
        }

        private void SetArrowVisibility(UIElement arrow, bool isVisible)
        {
            if (arrow != null)
                arrow.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        // OP,MOP 제어
        private void UpdateOp()
        {
            UpdateOpSet("W1000", new[] { op01, op02, op03, op04, op05, op06, op07, op08 });
            UpdateOpSet("W1001", new[] { op09, op10, mop01, mop02, mop03, mop04 }, new[] { 0, 2, 8, 10, 12, 14 });
            UpdateOpSet("W1002", new[] { mop05, mop07, mop08, mop09, mop10 }, new[] { 0, 4, 6, 8, 10 });
            UpdateOpSet("W1003", new[] { mop20, mop21, mop22, mop23 });
        }

        private void UpdateOpSet(string waddr, Border[] controls, int[] normalBits = null)
        {
            for (int i = 0; i < controls.Length; i++)
            {
                int normalBit = normalBits != null ? normalBits[i] : i * 2;
                int errorBit = normalBit + 1;

                UpdateOpControlBackground(controls[i], waddr, normalBit, errorBit);
            }
        }

        private void UpdateOpControlBackground(Border control, string waddr, int normalBit, int errorBit)
        {
            int errorValue = _opList
                .Where(op => op.WADDR == waddr && op.WBIT == errorBit)
                .Select(op => op.NOW_VALUE)
                .FirstOrDefault();

            if (errorValue == 1)
            {
                control.Background = ErrorBrush;
            }
            else
            {
                int nowValue = _opList
                    .Where(op => op.WADDR == waddr && op.WBIT == normalBit)
                    .Select(op => op.NOW_VALUE)
                    .FirstOrDefault();

                control.Background = nowValue == 0 ? IdleBrush : NormalBrush;
            }
        }

        private void SetTextBlockColor(Border border, Brush color)
        {
            foreach (var textBlock in FindVisualChildren<TextBlock>(border))
            {
                textBlock.Foreground = color;
            }
        }

        private void UpdateBufferTag(Border border, string processed, cCvc match)
        {
            string tag = "";

            if (match.ERROR_VALUE == "1")
            {
                tag = "";
            }
            else if (match.CARGO_VALUE == "1" && match.DATA_VALUE == "1")
            {
                tag = "데이터[유] 화물[유]";
            }
            else if (match.CARGO_VALUE == "1" && match.DATA_VALUE == "0")
            {
                tag = "데이터[무] 화물[유]";
            }
            else if (match.CARGO_VALUE == "0" && match.DATA_VALUE == "1")
            {
                tag = "데이터[유] 화물[무]";
            }
            else if (match.CARGO_VALUE == "0" && match.DATA_VALUE == "0")
            {
                tag = "데이터[무] 화물[무]";
            }

            border.Tag = tag;
        }

        private void UpdateReadyStatus(string processed, cCvc match)
        {
            var readyTargets = new Dictionary<string, Border>
            {
                { "572", bdReady572 },
                { "576", bdReady576 },
                { "701", bdReady701 },
                { "704", bdReady704 }
            };

            if (readyTargets.TryGetValue(processed, out var bd))
            {
                bd.Background = match.READY_VALUE == "1" ? ReadyBrush : ErrorBrush;
            }
        }

        private void UpdateScc()
        {
            UpdateSccUnit(_sccLoc1, txtMode1F1, txtMode2F1, txtCargo1F1, txtCargo2F1, bdCargo1F1, bdCargo2F1, bdMode1F1, bdMode2F1);
            UpdateSccUnit(_sccLoc2, txtMode1F2, txtMode2F2, txtCargo1F2, txtCargo2F2, bdCargo1F2, bdCargo2F2, bdMode1F2, bdMode2F2);

            UpdateSccPosition(_sccLoc1, bdScc1F1, bdScc2F1);
            UpdateSccPosition(_sccLoc2, bdScc1F2, bdScc2F2);
        }

        private void UpdateSccUnit(cSccList sccLoc,
                                   TextBlock txtMode1, TextBlock txtMode2,
                                   TextBlock txtCargo1, TextBlock txtCargo2,
                                   Border bdCargo1, Border bdCargo2,
                                   Border bdMode1, Border bdMode2)
        {
            var modeItem = sccLoc.FirstOrDefault(x => x.WBIT == 14);
            var cargoItem = sccLoc.FirstOrDefault(x => x.WBIT == 8);
            var statusItem = sccLoc.FirstOrDefault(x => x.WBIT == 13);

            // Mode (자동/수동 + 입출고 텍스트)
            if (modeItem?.NOW_VALUE == "자동")
            {
                string modeText = "A";
                if (!string.IsNullOrEmpty(sccLoc[0].IO_TYPE))
                {
                    if (sccLoc[0].IO_TYPE.Contains("출고")) modeText = "O";
                    else if (sccLoc[0].IO_TYPE.Contains("입고")) modeText = "I";
                }
                txtMode1.Text = txtMode2.Text = modeText;
            }
            else if (modeItem?.NOW_VALUE == "수동")
            {
                txtMode1.Text = txtMode2.Text = "M";
            }

            // Cargo 유무
            if (cargoItem?.NOW_VALUE == "유")
            {
                bdCargo1.Background = bdCargo2.Background = CargoBrushYes;
                txtCargo1.Text = txtCargo2.Text = "O";
            }
            else if (cargoItem?.NOW_VALUE == "무")
            {
                bdCargo1.Background = bdCargo2.Background = CargoBrushNo;
                txtCargo1.Text = txtCargo2.Text = "X";
            }

            // 정상/에러 상태
            if (statusItem?.NOW_VALUE == "정상")
            {
                bdMode1.Background = bdMode2.Background = ModeBrushOk;
            }
            else if (statusItem?.NOW_VALUE == "에러")
            {
                bdMode1.Background = bdMode2.Background = ModeBrushError;
            }
        }

        private void UpdateSccPosition(cSccList sccLoc, Border bd1, Border bd2)
        {
            var wbit16Items = sccLoc.Where(x => x.WBIT == 16).Take(2).ToList();
            if (wbit16Items.Count < 2) return;

            if (Convert.ToInt32(wbit16Items[0].NOW_VALUE) >= 20)
                wbit16Items[0].NOW_VALUE = "1";

            if (int.TryParse(wbit16Items[0].NOW_VALUE, out int level))
            {
                double newTop = (level - 1) * sectionHeight;
                Canvas.SetTop(bd1, newTop);
                Canvas.SetTop(bd2, newTop);
            }
        }

        private void UpdatePalletCheck()
        {

            var pltCode1 = _pltList.FirstOrDefault(x => x.ID_BUFF == 561);
            var pltCode2 = _pltList.FirstOrDefault(x => x.ID_BUFF == 713);

            txtPltInCode.Text = pltCode1?.PALLET_CODE ?? "";
            txtPltOutCode.Text = pltCode2?.PALLET_CODE ?? "";
        }

        private void UpdateBcrCheck()
        {
            // s341.Background이 SolidColorBrush이고, 색이 #FF4C6EF5인지 확인
            bool isTargetBgIn =
                (s561.Background as SolidColorBrush)?.Color ==
                (Color)ColorConverter.ConvertFromString("#FF4C6EF5");

            bool isTargetBgOut =
              (s713.Background as SolidColorBrush)?.Color ==
              (Color)ColorConverter.ConvertFromString("#FF4C6EF5");

            if (isTargetBgIn && !string.IsNullOrEmpty(txtPltInCode.Text))
            {
                var item = _bcrLst.First(x => x.BCRNO == 1);
                txtPltInCodeBcr.Text = item.PALLET_CODE;
                bool inMatch = txtPltInCode.Text == txtPltInCodeBcr.Text;
                bdBcrInOk.Background = new SolidColorBrush(inMatch ? Colors.Green : Colors.Red);
                txtBcrInOk.Text = inMatch ? "OK" : "NG";
            }
            else
            {
                bdBcrInOk.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E4E8"));
                txtBcrInOk.Text = "";
                txtPltInCodeBcr.Text = "";
            }

            if (isTargetBgOut && !string.IsNullOrEmpty(txtPltOutCode.Text))
            {
                var item2 = _bcrLst.First(x => x.BCRNO == 2);
                txtPltOutCodeBcr.Text = item2.PALLET_CODE;
                bool outMatch = txtPltOutCode.Text == txtPltOutCodeBcr.Text;
                bdBcrOutOk.Background = new SolidColorBrush(outMatch ? Colors.Green : Colors.Red);
                txtBcrOutOk.Text = outMatch ? "OK" : "NG";
            }
            else
            {
                bdBcrOutOk.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE1E4E8"));
                txtBcrOutOk.Text = "";
                txtPltOutCodeBcr.Text = "";
            }
        }

        private void UpdateErrorList()
        {

            lbCvError.ItemsSource = _errorLst;
        }

        private void UpdateHostProdInfo()
        {
            txtHostDate.Text = _hostItem.HOST_CR_DATE;
            txtHostOption.Text = _hostItem.HOST_OPTION == 1 ? "정상" : "에러";
        }

        private void btnScClear_Click(object sender, RoutedEventArgs e)
        {
            if (sccNo != 0)
                _sccData.SccCurrentAdd(sccNo, "1");
        }

        private void btnCvcMove_Click(object sender, RoutedEventArgs e)
        {
            if (txtBuffTarget.Text == "")
            {
                MessageBox.Show("이동할 버퍼를 입력해 주세요.");
                return;
            }

            if (!_bufferNames.Contains(txtBuffTarget.Text))
            {
                MessageBox.Show("해당 버퍼는 존재하지 않습니다.");
                return;
            }

            if (txtBuff.Text == txtBuffTarget.Text)
            {
                MessageBox.Show("버퍼가 동일합니다.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(txtBuff.Text + " 버퍼를 이동하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // 사용자가 "Yes"를 선택한 경우에만 실행
            if (result == MessageBoxResult.Yes)
            {
                _cvcResult = _data.CvMove(Convert.ToInt32(txtBuff.Text), Convert.ToInt32(txtBuffTarget.Text));

                if (_cvcResult.RESULT == "NG")
                {
                    MessageBox.Show(_cvcResult.MSG);
                    return;
                }

                DoClear();
            }
        }

        private void btnCvcDel_Click(object sender, RoutedEventArgs e)
        {
            if (txtBuff.Text == "")
            {
                MessageBox.Show("삭제할 버퍼를 선택해 주세요.");
                return;
            }

            MessageBoxResult result = MessageBox.Show(txtBuff.Text + " 버퍼를 삭제하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // 사용자가 "Yes"를 선택한 경우에만 실행
            if (result == MessageBoxResult.Yes)
            {
                _data.CvDel(Convert.ToInt32(txtBuff.Text));
                DoClear();
            }
        }

        private void btnCvcSwap_Click(object sender, RoutedEventArgs e)
        {
            if (txtBuffTarget.Text == "")
            {
                MessageBox.Show("교환할 버퍼를 입력해 주세요.");
                return;
            }

            if (!_bufferNames.Contains(txtBuffTarget.Text))
            {
                MessageBox.Show("해당 버퍼는 존재하지 않습니다.");
                return;
            }

            if (txtBuff.Text == txtBuffTarget.Text)
            {
                MessageBox.Show("버퍼가 동일합니다.");
                return;
            }


            MessageBoxResult result = MessageBox.Show(txtBuff.Text + ", " + txtBuffTarget.Text + " 버퍼를 교환하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // 사용자가 "Yes"를 선택한 경우에만 실행
            if (result == MessageBoxResult.Yes)
            {
                _cvcResult = _data.CvSwap(Convert.ToInt32(txtBuff.Text), Convert.ToInt32(txtBuffTarget.Text));

                if (_cvcResult.RESULT == "NG")
                {
                    MessageBox.Show(_cvcResult.MSG);
                    return;
                }

                DoClear();
            }
        }

        private void btnCvGo_Click(object sender, RoutedEventArgs e)
        {
            _data.OrderCv(Convert.ToInt32(txtBuff.Text), "0");
            DoClear();
        }

        private void btnCvGoComp_Click(object sender, RoutedEventArgs e)
        {
            _data.OrderCv(Convert.ToInt32(txtBuff.Text), "1");
            DoClear();
        }

        private void btnCvQua_Click(object sender, RoutedEventArgs e)
        {
            _data.OrderCv(Convert.ToInt32(txtBuff.Text), "2");
            DoClear();
        }

        private void btnCvQuaComp_Click(object sender, RoutedEventArgs e)
        {
            _data.OrderCv(Convert.ToInt32(txtBuff.Text), "3");
            DoClear();
        }

        private void btnCvJoin_Click(object sender, RoutedEventArgs e)
        {
            _data.OrderCv(Convert.ToInt32(txtBuff.Text), "4");
            DoClear();
        }

        private void btnCvJoinComp_Click(object sender, RoutedEventArgs e)
        {
            _data.OrderCv(Convert.ToInt32(txtBuff.Text), "5");
            DoClear();
        }

        // 컨베이어 상태 수정
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtBuff.Text == "")
            {
                MessageBox.Show("수정할 버퍼를 선택해 주세요");
                return;
            }
            MessageBoxResult result = MessageBox.Show("수정하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _trackData.TrackUpdate(Convert.ToInt32(txtBuff.Text), cbLoadCode.Text, tbLotno1.Text, tbLotno2.Text,
                                    tbLotno3.Text, tbLotno4.Text, tbLotno5.Text, tbPlt.Text, tbCommit.Text, tbBody.Text);
                DoClear();

                MessageBox.Show("수정완료");
            }
        }

        private void cbSccNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSccNo.SelectedValue != null)
            {
                int selectedSccNo = (int)cbSccNo.SelectedValue;

                sccNo = selectedSccNo;

                _sccLst = _sccData.GetSccData(selectedSccNo);

                var wbit15Item = _sccLst.FirstOrDefault(x => x.WBIT == 15);
                var wbit14Item = _sccLst.FirstOrDefault(x => x.WBIT == 14);
                var wbit13Item = _sccLst.FirstOrDefault(x => x.WBIT == 13);
                var wbit10Item = _sccLst.FirstOrDefault(x => x.WBIT == 10);
                var wbit08Item = _sccLst.FirstOrDefault(x => x.WBIT == 8);
                var wbit07Item = _sccLst.FirstOrDefault(x => x.WBIT == 7);
                var wbit16Items = _sccLst.Where(x => x.WBIT == 16).Take(2).ToList();


                if (wbit15Item != null)
                {
                    txtWbit15.Text = wbit15Item.NOW_VALUE.ToString();
                }
                else
                {
                    txtWbit15.Text = string.Empty;
                }
                if (wbit14Item != null)
                {
                    txtWbit14.Text = wbit14Item.NOW_VALUE.ToString();
                }
                else
                {
                    txtWbit14.Text = string.Empty;
                }
                if (wbit13Item != null)
                {
                    txtWbit13.Text = wbit13Item.NOW_VALUE.ToString();
                }
                else
                {
                    txtWbit13.Text = string.Empty;
                }
                if (wbit10Item != null)
                {
                    txtWbit10.Text = wbit10Item.NOW_VALUE.ToString();
                }
                else
                {
                    txtWbit10.Text = string.Empty;
                }
                if (wbit08Item != null)
                {
                    txtWbit08.Text = wbit08Item.NOW_VALUE.ToString();
                }
                else
                {
                    txtWbit08.Text = string.Empty;
                }
                if (wbit07Item != null)
                {
                    txtWbit07.Text = wbit07Item.NOW_VALUE.ToString();
                }
                else
                {
                    txtWbit07.Text = string.Empty;
                }


                if (wbit16Items.Count == 2)
                {
                    txtWbit16.Text = $"{wbit16Items[0].NOW_VALUE}-{wbit16Items[1].NOW_VALUE}";
                }
                else
                {
                    txtWbit16.Text = ""; // 또는 "값 부족", "N/A" 등 원하는 기본값
                }

                // 작업정보 조회
                _sccScio = _sccData.ScioGet(sccNo);

                txtScioStatus.Text = _sccScio.SC_STATUS;
                txtScioType.Text = _sccScio.IO_TYPE;
                txtScioIdx.Text = _sccScio.ID_INDEX.ToString() + "-" + _sccScio.ID_SUBIDX.ToString();

                string result = "";

                if (string.IsNullOrEmpty(_sccScio.IO_TYPE))
                {
                    result = "";
                }
                else if (_sccScio.IO_TYPE.Contains("출고"))
                    result = "출고";
                else if (_sccScio.IO_TYPE.Contains("입고"))
                    result = "입고";

                if (result == "입고")
                {
                    txtScioLoc.Text = _sccScio.LOAD_BANK + "-" + _sccScio.LOAD_BAY + "-" + _sccScio.LOAD_LEVEL;
                }
                else if (result == "출고")
                {
                    txtScioLoc.Text = _sccScio.UNLOAD_BANK + "-" + _sccScio.UNLOAD_BAY + "-" + _sccScio.UNLOAD_LEVEL;
                }
                else
                {
                    txtScioLoc.Text = "";
                }
            }
        }

        private void txtBuffTarget_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+"); // 숫자가 아닌 문자는 true
            e.Handled = regex.IsMatch(e.Text);  // 숫자가 아니면 입력을 막음
        }

        private void txtBuffTarget_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Backspace, Delete 등은 허용
            if (e.Key == Key.Space)
            {
                e.Handled = true; // 스페이스 입력 막기
            }
        }
        private void txtBuffTarget_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isTxtBuffTargetSelected = true;
        }

        // 파레트코드 수동입력
        private void btnBcrIn_Click(object sender, RoutedEventArgs e)
        {
            if (txtBcrName.Text == "[입고] 561")
            {
                _bcrData.AddBcrrPlt(1, tbBcrPlt.Text);
            }
            if (txtBcrName.Text == "[출고] 713")
            {
                _bcrData.AddBcrrPlt(2, tbBcrPlt.Text);
            }

            _bcrLst = _bcrData.BcrPltGet();
            if (txtPltInCode.Text != "")
            {
                var item = _bcrLst.First(x => x.BCRNO == 1);
                txtPltInCodeBcr.Text = item.PALLET_CODE;
            }
            if (txtPltOutCode.Text != "")
            {
                var item2 = _bcrLst.First(x => x.BCRNO == 2);
                txtPltOutCodeBcr.Text = item2.PALLET_CODE;
            }
        }

        private void btnReBcr_Click(object sender, RoutedEventArgs e)
        {
            _bcrData.AddCommand(1, "0", "");
        }

        private void btnReBcrOut_Click(object sender, RoutedEventArgs e)
        {
            _bcrData.AddCommand(2, "0", "");
        }

        private void bdBcrIn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) // 더블클릭인지 확인
            {
                if (sender is Border border)
                {
                    if (txtBcrName.Text == "[출고] 713")
                    {
                        txtBcrName.Text = border.Tag.ToString();
                        return;
                    }

                    if (stpBcrInput.Visibility == Visibility.Visible)
                    {
                        stpBcrInput.Visibility = Visibility.Hidden;
                        txtBcrName.Text = "";
                    }
                    else
                    {
                        stpBcrInput.Visibility = Visibility.Visible;
                        txtBcrName.Text = border.Tag.ToString();
                    }
                }
            }
        }

        private void bdBcrOut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) // 더블클릭인지 확인
            {
                if (sender is Border border)
                {
                    if (txtBcrName.Text == "[입고] 561")
                    {
                        txtBcrName.Text = border.Tag.ToString();
                        return;
                    }

                    if (stpBcrInput.Visibility == Visibility.Visible)
                    {
                        stpBcrInput.Visibility = Visibility.Hidden;
                        txtBcrName.Text = "";
                    }
                    else
                    {
                        stpBcrInput.Visibility = Visibility.Visible;
                        txtBcrName.Text = border.Tag.ToString();
                    }
                }
            }
        }

        private void bdScc1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cbSccNo.SelectedIndex = 0;
        }

        private void bdScc2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cbSccNo.SelectedIndex = 1;
        }

        private void MouseLeft_Down(object sender, MouseButtonEventArgs e)
        {
            // 지시,완료 버튼을 비활성화한다.
            btnCvGo.IsEnabled = false;
            btnCvGoComp.IsEnabled = false;
            btnCvQua.IsEnabled = false;
            btnCvQuaComp.IsEnabled = false;
            btnCvJoin.IsEnabled = false;
            btnCvJoinComp.IsEnabled = false;

            txtBuffTarget.Text = "";

            if (sender is Border border)
            {

                string borderName = border.Name.Replace("s", "").Split('_')[0];

                if (isTxtBuffTargetSelected)
                {
                    txtBuffTarget.Text = borderName;
                    isTxtBuffTargetSelected = false; // 입력 완료 후 선택 해제
                    return;
                }

                _track = _trackData.TrackGet(Convert.ToInt32(borderName));

                txtLoc.Text = _track.ID_BANK + "-" + _track.ID_BAY + "-" + _track.ID_LEVEL;
                txtOrder.Text = _track.ORDER_DATE + _track.ORDER_TIME + "-" + _track.ORDER_INDEX + "-" + _track.ORDER_SUBIDX;
                txtType.Text = _track.ID_TYPE;
                tbLotno1.Text = _track.LOT_NO1;
                tbLotno2.Text = _track.LOT_NO2;
                tbLotno3.Text = _track.LOT_NO3;
                tbLotno4.Text = _track.LOT_NO4;
                tbLotno5.Text = _track.LOT_NO5;
                cbLoadCode.SelectedValue = _track.LOAD_CODE;
                tbPlt.Text = _track.PALLET_CODE;
                tbCommit.Text = _track.COMMIT_NO;
                tbBody.Text = _track.BODY_NO;
                txtBuff.Text = _track.ID_BUFF.ToString();

                if (border.Tag is null)
                    txtCvStatus.Text = "";
                else
                    txtCvStatus.Text = border.Tag.ToString();

                // 직진완료버튼 활성화
                if (borderName == "543" || borderName == "587" || borderName == "565" || borderName == "566" || borderName == "560"
                     || borderName == "561" || borderName == "546" || borderName == "611" || borderName == "708" || borderName == "713" 
                     || borderName == "714")
                {
                    btnCvGoComp.IsEnabled = true;
                }

                // 직진지시버튼 활성화
                if (borderName == "543" || borderName == "587" || borderName == "565" || borderName == "566" || borderName == "560"
                     || borderName == "561" || borderName == "546" || borderName == "611" || borderName == "708" || borderName == "713"
                     || borderName == "714")
                {
                    btnCvGo.IsEnabled = true;
                }

                // 분기완료버튼 활성화
                if (borderName == "566" || borderName == "706")
                {
                    btnCvQuaComp.IsEnabled = true;
                }
                // 분기지시버튼 활성화
                if (borderName == "566" || borderName == "706")
                {
                    btnCvQua.IsEnabled = true;
                }

             /*   //합류완료버튼 활성화
                if (borderName == "411")
                {
                    btnCvJoinComp.IsEnabled = true;
                }

                //합류지시버튼 활성화
                if (borderName == "406")
                {
                    btnCvJoin.IsEnabled = true;
                }
*/
                // 포커스 제거
                Keyboard.ClearFocus();
            }
        }


        private void DoClear()
        {
            // 지시,완료 버튼을 비활성화한다.
            btnCvGo.IsEnabled = false;
            btnCvGoComp.IsEnabled = false;
            btnCvQua.IsEnabled = false;
            btnCvQuaComp.IsEnabled = false;
            btnCvJoin.IsEnabled = false;
            btnCvJoinComp.IsEnabled = false;

            txtLoc.Text = "";
            txtOrder.Text = "";
            txtType.Text = "";
            tbLotno1.Text = "";
            tbLotno2.Text = "";
            tbLotno3.Text = "";
            tbLotno4.Text = "";
            tbLotno5.Text = "";
            tbPlt.Text = "";
            tbCommit.Text = "";
            tbBody.Text = "";
            txtBuff.Text = "";
            txtCvStatus.Text = "";

            UpdateBorders();

            txtBuffTarget.Text = "";
            txtPltInCodeBcr.Text = "";
            txtPltOutCodeBcr.Text = "";
            bdBcrInOk.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE1E4E8");
            bdBcrOutOk.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE1E4E8");
            txtBcrInOk.Text = "";
            txtBcrOutOk.Text = "";

            isTxtBuffTargetSelected = false;
        }

    }
}
