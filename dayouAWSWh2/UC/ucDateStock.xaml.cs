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
    /// ucDateStock.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucDateStock : UserControl
    {
        cCommonItemList _carList = new cCommonItemList();
        cCommonItemList _frGubunList = new cCommonItemList();
        cCommonData _data = new cCommonData();

        cDateStockItemList _DateStockList = new cDateStockItemList();
        cStockData _stockData = new cStockData();

        public ucDateStock()
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
            _carList = _data.getCommonList("ALC_CLASS", "");
            //_carList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            cbCarCode.ItemsSource = _carList;

            // F/R 구분
            _frGubunList = _data.getCommonList("FR_GUBUN", "");
            _frGubunList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            cbFR.ItemsSource = _frGubunList;

            // 조회일자 오늘날짜로 
            DatePicker.SelectedDate = DateTime.Now;
            DatePicker2.SelectedDate = DateTime.Now;

            doSearch();
        }
        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {
            var date = Convert.ToDateTime(DatePicker.Text).ToString("yyyyMMdd");
            var date2 = Convert.ToDateTime(DatePicker2.Text).ToString("yyyyMMdd");

            _DateStockList = _stockData.getDateStockList(date, date2, cbCarCode.Text == "전체" ? "" : cbCarCode.Text, cbFR.Text == "전체"? "" : cbFR.Text);
            DateStockDataGrid.ItemsSource = _DateStockList;
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
