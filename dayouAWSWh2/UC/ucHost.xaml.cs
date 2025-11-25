using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using dayouAWSWh2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// ucHost.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucHost : UserControl
    {
        cCommonItemList _carList = new cCommonItemList();
        cCommonData _Commondata = new cCommonData();

        cHostItemList _hostList = new cHostItemList();
        cHostData _hostData = new cHostData();
        cMessage _items = new cMessage();

        private DispatcherTimer _SearchTimer = new DispatcherTimer(); // 조회 타이머
        public ucHost()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            // 툴바 버튼 제어
            ucToolbarBtn.ButtonStates = new Dictionary<int, bool>
            {
                { 1, true },  // 신규
                { 2, true },   // 조회
                { 3, false },  // 저장
                { 4, false },  // 취소
                { 5, false }   // 삭제
            };

            // 툴바 버튼 클릭 이벤트
            ucToolbarBtn.Button2Clicked += ucToolbarBtn_BtnSearch;
            ucToolbarBtn.Button1Clicked += ucToolbarBtn_BtnAdd;

            cbStatus.Items.Add("전체");
            cbStatus.Items.Add("작업대기");
            cbStatus.Items.Add("작업완료");
            cbStatus.Items.Add("삭제");

            // 차종
            _carList = _Commondata.getCommonList("ALC_CLASS", "");
            //_carList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            cbCarCode.ItemsSource = _carList;

            //재고 콤보박스
            cbStock.Items.Add("전체");
            cbStock.Items.Add("부족");

            // 조회 갱신용
            _SearchTimer.Interval = TimeSpan.FromMilliseconds(1000); // 1초 간격 설정
            _SearchTimer.Tick += _SearchTimer_Tick;
            //_SearchTimer.Start();

            if (ckSearch.IsChecked == true)
            {
                CheckBox_Checked(ckSearch, null);
            }
            doSearch();
        }

        private void _SearchTimer_Tick(object sender, EventArgs e)
        {
            doSearch();
        }
        
        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
           
        }
        // 신규버튼
        private void ucToolbarBtn_BtnAdd(object sender, EventArgs e)
        {
            doAdd();
        }

        private void doSearch()
        {
            _hostList = _hostData.getHostList(cbCarCode.Text == "전체" ? "" : cbCarCode.Text, txtAlcCode.Text, txtBodyNo.Text, txtCommitNo.Text, cbStatus.Text);

            if(cbStock.Text == "부족")
            {
                HostDataGrid.ItemsSource = _hostList.Where(x => x.STOCK_FR == "부족" || x.STOCK_R2 == "부족" || x.STOCK_R3 == "부족").ToList();
            }
            else
            {
                HostDataGrid.ItemsSource = _hostList;
            }
        }

        private void doAdd()
        {
            winHostAdd _hostAdd = new winHostAdd();
            _hostAdd.ShowDialog();
            if (_hostAdd.DialogResult == true)
            {
                doSearch();
            }
        }

        private void hostDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = HostDataGrid.SelectedIndex;

                string report_sys_dt = _hostList[row].REPORT_SYS_DT.Replace("-", "").Replace(":", "").Replace(" ", "");

                //MessageBox.Show(_list[row].PLT);
                if (MessageBox.Show("선택하신 의장정보 [ " + report_sys_dt + " " +  _hostList[row].CMT_NO + " ]" + " 을(를) 삭제하시겠습니까?", "삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _items = _hostData.delHost(report_sys_dt, _hostList[row].CMT_NO, _hostList[row].STATUS);
                    if(_items.RESULT == "NG")
                    {
                        MessageBox.Show(_items.MSG);
                        return;
                    }
                    doSearch();
                }
                else
                {

                }
            }
            catch (Exception ex) { }
        }

        private void hostUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = HostDataGrid.SelectedIndex;

                string report_sys_dt = _hostList[row].REPORT_SYS_DT.Replace("-", "").Replace(":", "").Replace(" ", "");

                //alc변경 팝업
                winAlcUpdate _winAlcUpdate = new winAlcUpdate(report_sys_dt, _hostList[row].CMT_NO, _hostList[row].STATUS);
                if (_winAlcUpdate.ShowDialog() == true)
                {
                    doSearch();
                }
                else
                {
                }
            }
            catch (Exception ex) { }
        }
        
        private void btnOut_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("출고 진행 하시겠습니까?", "출고", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _items = _hostData.outHost();

                if(_items.RESULT == "NG")
                {
                    MessageBox.Show(_items.MSG);
                    return;
                }
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("체크됨");
            _SearchTimer.Start();

            if (HostDataGrid != null)
                HostDataGrid.ContextMenu = null;  // 아예 제거
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("체크 해제됨");
            _SearchTimer.Stop();
            // 다시 ContextMenu 설정
            ContextMenu menu = new ContextMenu();

            MenuItem menuDel = new MenuItem { Header = "서열 삭제" };
            MenuItem menuUpdate = new MenuItem { Header = "ALC 변경" };
            menuDel.Click += hostDel_Click;
            menuUpdate.Click += hostUpdate_Click;

            menu.Items.Add(menuDel);
            menu.Items.Add(menuUpdate);

            HostDataGrid.ContextMenu = menu;
        }

        private void btnCntOut_Click(object sender, RoutedEventArgs e)
        {
            if(txtSkidCnt.Text.Length == 0)
            {
                MessageBox.Show("개수를 입력해주세요");
                return;
            }
            if (MessageBox.Show("지정 갯수 출고를 진행 하시겠습니까?", "출고", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _items = _hostData.outCntHost(Convert.ToInt32(txtSkidCnt.Text));
                if(_items.RESULT == "NG")
                {
                    MessageBox.Show(_items.MSG);
                    return;
                }
                txtSkidCnt.Text = "";
                doSearch();
            }
            else
            {

            }
        }

        private void txtSkidCnt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            // 숫자가 아니면 차단 (스페이스 포함)
            if (!char.IsDigit(e.Text, 0) || e.Text == " ")
            {
                e.Handled = true;
                return;
            }

            // 첫 글자가 0이면 차단
            if (textBox.Text.Length == 0 && e.Text == "0")
            {
                e.Handled = true;
            }
        }

        private void txtSkidCnt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // 스페이스 입력 차단
            }
        }
    }
}
