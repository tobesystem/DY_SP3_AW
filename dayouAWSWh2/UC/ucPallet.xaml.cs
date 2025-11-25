using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using dayouAWSWh2.View;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// ucPallet.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucPallet : UserControl
    {
        cPalletItemList _palletList = new cPalletItemList();
        cPalletData _palletData = new cPalletData();

        cCommonItemList _carList = new cCommonItemList();
        cCommonData _data = new cCommonData();
        public ucPallet()
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
                { 2, true },  // 조회
                { 3, false }, // 저장
                { 4, false }, // 취소
                { 5, true }   // 삭제
            };

            // 툴바 버튼 클릭 이벤트
            ucToolbarBtn.Button1Clicked += ucToolbarBtn_BtnAdd;
            ucToolbarBtn.Button2Clicked += ucToolbarBtn_BtnSearch;
            ucToolbarBtn.Button5Clicked += ucToolbarBtn_BtnDelete;

            // 차종
            _carList = _data.getCommonList("ALC_CLASS", "");
            cbCarCode.ItemsSource = _carList;

            doSearch();
        }

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void ucToolbarBtn_BtnAdd(object sender, EventArgs e)
        {
            doAdd();
        }

        private void ucToolbarBtn_BtnDelete(object sender, EventArgs e)
        {
            doDelete();
        }

        private void doSearch()
        {
                _palletList = _palletData.getPalletList(txtPallet.Text);
                PalletDataGrid.ItemsSource = _palletList;
        }

        private void doAdd()
        {
            winPalletPopup _winPallet = new winPalletPopup("", "", "", "파레트 코드 추가", "1");

            _winPallet.ShowDialog();
            if (_winPallet.DialogResult == true)
            {
                _palletList = _palletData.getPalletList(txtPallet.Text);
                PalletDataGrid.ItemsSource = _palletList;
            }
        }

        private void doDelete()
        {
            int row = PalletDataGrid.SelectedIndex;
            try
            {
                if (MessageBox.Show(_palletList[row].PALLETCD + " 을(를) 삭제하시겠습니까?", "삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _palletData.deletePallet(_palletList[row].PALLETCD.ToString());

                    _palletList = _palletData.getPalletList(txtPallet.Text);
                    PalletDataGrid.ItemsSource = _palletList;
                }
                else
                {

                }
            }
            catch (Exception ex) { }
        }

       

        private void PalletDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // datagrid 선택한 index
            int row = PalletDataGrid.SelectedIndex;

            try
            {
                winPalletPopup _winPallet = new winPalletPopup(_palletList[row].PALLETCD.ToString(), _palletList[row].PALLETNM.ToString(), _palletList[row].BADCODE.ToString(), "파레트 코드 수정", "0");

                _winPallet.ShowDialog();
                if (_winPallet.DialogResult == true)
                {
                    _palletList = _palletData.getPalletList(txtPallet.Text);
                    PalletDataGrid.ItemsSource = _palletList;
                }
            }
            catch { }

        }


        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                doSearch();  // 엔터키를 눌렀을 때 조회 버튼 클릭 이벤트 호출
            }
        }

        private void cbCarCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
