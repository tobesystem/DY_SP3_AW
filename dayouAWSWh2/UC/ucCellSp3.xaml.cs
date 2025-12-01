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
    /// ucCellSp3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCellSp3 : UserControl
    {
        private string _row;
        cCellList _list = new cCellList();
        cCellData _data = new cCellData();


        // 이벤트 정의
        public event Action<cCell> StatusClicked;
        public event EventHandler ButtonClicked;

        cManagerOutData _orderData = new cManagerOutData();

        private Dictionary<string, string> _prevStatusMap = new Dictionary<string, string>();

        // 이벤트 핸들러 재사용을 위한 필드 선언
        private readonly MouseButtonEventHandler _rightClickHandler;

        public ucCellSp3()
        {
            InitializeComponent();
            _rightClickHandler = new MouseButtonEventHandler(Cell_MouseRightButtonDown);
        }

        public async Task SetRowAsync(string row)
        {
            _row = row;

            //  DB 호출은 백그라운드 스레드에서 실행
            var list = await Task.Run(() => _data.GetCell(row));

            //  await 이후에는 다시 WPF UI 스레드로 돌아옴
            _list = list;
            rowCnt.Text = row.Substring(1) + "열";

            foreach (var item in _list)
            {
                string controlName = "c" + item.ID_CODE.Substring(2);

                if (!_prevStatusMap.TryGetValue(item.ID_CODE, out var prevStatus) || prevStatus != item.STATUS)
                {
                    _prevStatusMap[item.ID_CODE] = item.STATUS;

                    if (FindName(controlName) is ContentControl contentControl)
                    {
                        UpdateCell(contentControl, item);  // UI 컨트롤 업데이트
                    }
                }
            }
        }

     
        private void UpdateCell(ContentControl contentControl, cCell item)
        {
            contentControl.Style = (Style)FindResource("cellBorderSet");
            // STATUS 값에 따라 색상 변경
            switch (item.STATUS)
            {
                case "공셀":
                    contentControl.Background = new SolidColorBrush(Colors.White);
                    break;
                case "파레트":
                    contentControl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF66A80F"));
                    break;
                case "공출고":
                    contentControl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFBE4BDB"));
                    break;
                case "금지셀":
                    contentControl.Background = new SolidColorBrush(Colors.Black);
                    break;
                case "입고예약":
                    contentControl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFE066"));
                    break;
                case "출고예약":
                    contentControl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF9800"));
                    break;
                case "이중입고":
                    contentControl.Background = new SolidColorBrush(Colors.Red);
                    break;
                case "실셀":
                    if (item.KIND == "SP3")
                        contentControl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF56DCFF"));
                    else
                        contentControl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00D2B1"));

                    // MouseRightButtonDown 이벤트 핸들러 추가
                    contentControl.MouseRightButtonDown -= _rightClickHandler;
                    contentControl.MouseRightButtonDown += _rightClickHandler;
                    /*     contentControl.RemoveHandler(UIElement.MouseRightButtonDownEvent, new MouseButtonEventHandler(Cell_MouseRightButtonDown));
                         contentControl.AddHandler(UIElement.MouseRightButtonDownEvent, new MouseButtonEventHandler(Cell_MouseRightButtonDown), true);
     */
                    break;
                default:
                    contentControl.Style = (Style)FindResource("cellBorder");
                    break;
            }
        }

        private void ContentControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
            if (sender is ContentControl contentControl)
            {
                // "c"를 제외한 나머지 문자열 추출
                string controlName = _row + contentControl.Name.Substring(1);

                // _list에서 ID_CODE가 일치하는 항목 찾기
                var item = _list.FirstOrDefault(x => x.ID_CODE == controlName);

                if (item != null)
                {
                    // string statusMessage = $"ID: {_row + controlName}\nSTATUS: {item.STATUS}";

                    // 이벤트가 구독된 경우에만 호출
                    if (StatusClicked != null)
                    {
                        StatusClicked(item);
                    }
                }
            }
        }

        private void ContentControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is ContentControl contentControl)
            {
                // "c"를 제거한 Border 이름을 ToolTip으로 설정
                string controlName = _row + contentControl.Name.Substring(1);

                ToolTipService.SetToolTip(contentControl, controlName);
            }
        }

        private void Cell_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ContentControl contentControl)
            {
                ContextMenu contextMenu = new ContextMenu();

                MenuItem orderOut = new MenuItem { Header = "지정출고" };
                orderOut.Click += (s, ev) => OutAction(contentControl);

                contextMenu.Items.Add(orderOut);
                contentControl.ContextMenu = contextMenu;
                contextMenu.IsOpen = true;
            }
        }

        private void OutAction(ContentControl contentControl)
        {
            // "c"를 제외한 나머지 문자열 추출
            string controlName = _row + contentControl.Name.Substring(1);

            // _list에서 ID_CODE가 일치하는 항목 찾기
            var item = _list.FirstOrDefault(x => x.ID_CODE == controlName);

            MessageBoxResult result = MessageBox.Show($"선택한 {item.ID_BANK}-{item.ID_BAY}-{item.ID_LEVEL} 위치의\r\n출고(지정출고)를 진행하시겠습니까? ",
                "확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // 사용자가 "Yes"를 선택한 경우에만 실행
            if (result == MessageBoxResult.Yes)
            {
                _orderData.ManagerOutAdd(item.ID_BANK, item.ID_BAY, item.ID_LEVEL);
            }
        }
    }
}
