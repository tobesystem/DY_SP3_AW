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
    /// ucOrderWait.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucOrderWait : UserControl
    {
        cOrderWaitItemList _OrderWaitlist = new cOrderWaitItemList();
        cOrderWaitData _OrderWaitdata = new cOrderWaitData();

        cCommonItemList _carList = new cCommonItemList();
        cCommonData _data = new cCommonData();
        public ucOrderWait()
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
                { 2, true },  // 조회
                { 3, false }, // 저장
                { 4, false }, // 취소
                { 5, true }   // 삭제
            };

            // 툴바 버튼 클릭 이벤트
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

        private void ucToolbarBtn_BtnDelete(object sender, EventArgs e)
        {
            doDelete();
        }

        private void doSearch()
        {
            _OrderWaitlist = _OrderWaitdata.OrderWaitGet();
            OrderWaitDataGrid.ItemsSource = _OrderWaitlist;
        }

        private void doDelete()
        {
            int row = OrderWaitDataGrid.SelectedIndex;
            try
            {
                if (MessageBox.Show("서열출고에 문제가 생길 수 있습니다. 확인하셨다면 '예'를 눌러주세요", "삭제", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    _OrderWaitdata.deleteOrderWait(_OrderWaitlist[row].LOT_NO1.ToString());

                    _OrderWaitlist = _OrderWaitdata.OrderWaitGet();
                    OrderWaitDataGrid.ItemsSource = _OrderWaitlist;
                }
                else
                {

                }
            }
            catch (Exception ex) { }
        }
    }
}
