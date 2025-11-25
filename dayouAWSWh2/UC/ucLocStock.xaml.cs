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
    /// ucLocStock.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucLocStock : UserControl
    {
        cCommonItemList _carList = new cCommonItemList();
        cCommonItemList _alcAreaList = new cCommonItemList();
        cCommonItemList _cellStatusList = new cCommonItemList();
        cCommonItemList _frGubunList = new cCommonItemList();

        cCommonData _data = new cCommonData();

        cLocStockItemList _locStockList = new cLocStockItemList();
        cStockData _stockData = new cStockData();

        public ucLocStock()
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


            // Cell 상태
            _cellStatusList = _data.getCommonList("CELL_STATUS", "");
            _cellStatusList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            cbCellStatus.ItemsSource = _cellStatusList;

            // 차종
            _carList = _data.getCommonList("ALC_CLASS", "");
            //_carList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            cbCarCode.ItemsSource = _carList;

            // F/R 구분(PLT_CODE)
            //_frGubunList = _data.getCommonList("FR_GUBUN", "");
            //_frGubunList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            //cbFR.ItemsSource = _frGubunList;


            //출하구분
            //_alcAreaList = _data.getCommonList("ALC_AREA", "");
            //_alcAreaList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            //cbAlcArea.ItemsSource = _alcAreaList;

            doSearch();
        }

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {
            doClear();
            _locStockList = _stockData.getLocStockList(txtBank.Text, txtBay.Text, txtLevel.Text, cbCellStatus.Text == "전체" ? "" : cbCellStatus.Text, cbCarCode.Text == "전체" ? "" : cbCarCode.Text, txtPallet.Text, txtCode.Text);

            locStockDataGrid.ItemsSource = _locStockList;
        }
    

        private void doClear()
        {
            _locStockList.Clear();
            locStockDataGrid.ItemsSource = null;
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
