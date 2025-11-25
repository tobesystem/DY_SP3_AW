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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace dayouAWSWh2.UC
{
    /// <summary>
    /// ucProd.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucProd : UserControl
    {
        cCommonItemList _carList = new cCommonItemList();
        cCommonData _data = new cCommonData();

        cProdItemList _prodList = new cProdItemList();
        cProdData _prodData = new cProdData();
        cMessage _items = new cMessage();
        public ucProd()
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

            // 차종
            _carList = _data.getCommonList("ALC_CLASS", "");
            _carList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            cbCarCode.ItemsSource = _carList;

            doSearch();
        }

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {
            _prodList = _prodData.getProdList(cbCarCode.Text == "전체" ? "" : cbCarCode.Text, txtAlcCode.Text, txtBodyNo.Text, txtCommitNo.Text, cbStatus.Text == "전체" ? "" : cbStatus.Text);
            prodDataGrid.ItemsSource = _prodList;
        }

        private void prodDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int row = prodDataGrid.SelectedIndex;

                //MessageBox.Show(_list[row].PLT);
                if (MessageBox.Show("선택하신 도장정보[ " + _prodList[row].REPORT_SYS_DT + " " + _prodList[row].CMT_NO + " ]" + " 을(를) 삭제하시겠습니까?", "삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _items = _prodData.delProd(_prodList[row].REPORT_SYS_DT, _prodList[row].CMT_NO);
                    if (_items.RESULT == "NG")
                    {
                        MessageBox.Show(_items.RESULT);
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
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                doSearch();  // 엔터키를 눌렀을 때 조회 버튼 클릭 이벤트 호출
            }
        }
    }
}
