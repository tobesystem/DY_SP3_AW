using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    /// ucCodeStock.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucCodeStock : UserControl
    {
        cCommonItemList _carList = new cCommonItemList();
        cCommonItemList _typeList = new cCommonItemList();
        cCommonItemList _alcAreaList = new cCommonItemList();
        cCommonItemList _coverGList = new cCommonItemList();
        cCommonData _data = new cCommonData();

        //적재 코드별 재고현황용
        cCodeStockItemList _codeStockList = new cCodeStockItemList();
        //적재 코드별 재고현황 세부용
        cCodeStockItemList _codeStockSubList = new cCodeStockItemList();
        cStockData _stockData = new cStockData();
        public ucCodeStock()
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
                { 2, true },
                { 3, false },
                { 4, false },
                { 5, false }
            };

            // 툴바 버튼 클릭 이벤트
            ucToolbarBtn.Button2Clicked += ucToolbarBtn_BtnSearch;
            // 차종
            _carList = _data.getCommonList("ALC_CLASS", "");
            //_carList.Insert(0, new cCommonItem { CODE_TYPE = "ALL", CODE_NAME = "전체" });
            cbCarCode.ItemsSource = _carList;

            doSearch();
        }

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }


        private void doSearch()
        {
            doClear();
            //MessageBox.Show(cbCarCode.Text);
            _codeStockList = _stockData.getCodeStockList(txtCode.Text, cbCarCode.Text == "전체" ? "" : cbCarCode.Text);
            dataGrid1.ItemsSource = _codeStockList;
        }

        private void doClear()
        {
            _codeStockList.Clear();
            dataGrid1.ItemsSource = null;
            _codeStockSubList.Clear();
            dataGrid2.ItemsSource = null;
        }

        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGrid1.SelectedCells.Count > 0)
            {
                DataGridCellInfo cellInfo = dataGrid1.SelectedCells[0]; // 첫 번째 선택된 셀
                if (cellInfo.Column.GetCellContent(cellInfo.Item) is TextBlock cellContent)
                {
                    _codeStockSubList.Clear();
                    dataGrid2.ItemsSource = null;
                    //MessageBox.Show(cellContent.Text);
                    _codeStockSubList = _stockData.getCodeStockSubList(cellContent.Text);
                    dataGrid2.ItemsSource = _codeStockSubList;
                }
            }
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
