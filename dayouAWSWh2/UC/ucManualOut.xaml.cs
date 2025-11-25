using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// ucManualOut.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucManualOut : UserControl
    {
        cCommonItemList _carList = new cCommonItemList();
       
        cCommonData _Commondata = new cCommonData();

        cManualOutItemList _manualList = new cManualOutItemList();
        cManualOutItemList _manualSubList = new cManualOutItemList();
        cManualOutData _data = new cManualOutData();

        private string _aclCode = "";
        public ucManualOut()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
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

            // 차종
            _carList = _Commondata.getCommonList("ALC_CLASS", "");
           // _carList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            cbCarCode.ItemsSource = _carList;

            doSearch();
        }

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {
            _manualList = _data.getManualOutList(txtCode.Text, cbCarCode.Text);
            dataGrid1.ItemsSource = _manualList;

            _manualSubList.Clear();
            _manualSubList = _data.getManualOutSubList(cbCarCode.Text);
            dataGrid2.ItemsSource = _manualSubList;
        }


        private void outAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = dataGrid1.SelectedIndex;

                //MessageBox.Show(_list[row].PLT);
                if (MessageBox.Show("[ " + _manualList[row].ALC_CODE + " ]" + " 을(를) 출고하시겠습니까?", "출고", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _data.ManualOutAdd(_manualList[row].ALC_CODE, cbCarCode.Text);

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
                _data.ManualAllOutAdd(cbCarCode.Text);

                doSearch();
            }
            else
            {

            }
        }

        private void outDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = dataGrid2.SelectedIndex;

                if (MessageBox.Show("삭제 진행 하시겠습니까?", "출고", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    _data.OutDel(_manualSubList[row].ID_CODE, cbCarCode.Text);
                    doSearch();
                }
                else
                {

                }
            }
            catch (Exception ex) { }
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
