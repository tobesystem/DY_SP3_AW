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
using System.Windows.Threading;

namespace dayouAWSWh2.UC
{
    /// <summary>
    /// ucWmsOutSP3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucWmsOutSP3 : UserControl
    {
        cWmsData _data = new cWmsData();
        cWmsOutItemList _outList = new cWmsOutItemList();

        cMessage _items = new cMessage();

        private DispatcherTimer _SearchTimer = new DispatcherTimer(); // 조회 타이머
        public ucWmsOutSP3()
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

            doSearch();

            // 조회 갱신용
            _SearchTimer.Interval = TimeSpan.FromMilliseconds(1000); // 1초 간격 설정
            _SearchTimer.Tick += _SearchTimer_Tick;
            //_SearchTimer.Start();

            if (ckSearch.IsChecked == true)
            {
                CheckBox_Checked(ckSearch, null);
            }
        }

        private void _SearchTimer_Tick(object sender, EventArgs e)
        {
            doSearch();
        }

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {
            _outList = _data.getWmsOutSP3();
            wmsOutDataGrid.ItemsSource = _outList;
            totalTextBlock.Text = "합계:  " + _outList.Count.ToString();
        }

        //출고 강제완료
        private void outComplete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = wmsOutDataGrid.SelectedIndex;

                if (MessageBox.Show("강제완료 하시겠습니까?", "강제완료", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //MessageBox.Show(_list[row].ID_TYPE + "," + _list[row].ID_DATE + "," + _list[row].ID_TIME + "," + _list[row].ID_INDEX + "," + _list[row].ID_SUBIDX);

                    _data.WmsOutForceSP3(_outList[row].ID_TYPE, _outList[row].ID_DATE, _outList[row].ID_TIME, _outList[row].ID_INDEX, _outList[row].ID_SUBIDX);

                    //재조회
                    doSearch();
                }
            }
            catch (Exception ex)
            {
            }
        }

        //출고취소
        private void outCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = wmsOutDataGrid.SelectedIndex;

                if (MessageBox.Show("출고취소 하시겠습니까?", "출고취소", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //MessageBox.Show(_list[row].ID_TYPE + "," + _list[row].ID_DATE + "," + _list[row].ID_TIME + "," + _list[row].ID_INDEX + "," + _list[row].ID_SUBIDX);

                    _data.WmsOutCancelSP3(_outList[row].ID_TYPE, _outList[row].ID_DATE, _outList[row].ID_TIME, _outList[row].ID_INDEX, _outList[row].ID_SUBIDX);

                    //재조회
                    doSearch();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("체크됨");
            _SearchTimer.Start();

            if (wmsOutDataGrid != null)
                wmsOutDataGrid.ContextMenu = null;  // 아예 제거

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("체크 해제됨");
            _SearchTimer.Stop();
            // 다시 ContextMenu 설정
            ContextMenu menu = new ContextMenu();

            MenuItem menuForceComplete = new MenuItem { Header = "출고 강제완료" };
            menuForceComplete.Click += outComplete_Click;

            MenuItem menuCancel = new MenuItem { Header = "출고 취소" };
            menuCancel.Click += outCancel_Click;

            menu.Items.Add(menuForceComplete);
            menu.Items.Add(menuCancel);

            wmsOutDataGrid.ContextMenu = menu;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                doSearch();  // 엔터키를 눌렀을 때 조회 버튼 클릭 이벤트 호출
            }
        }
    }
}
