using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using dayouAWSWh2.View;
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
    /// ucUser.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucUser : UserControl
    {
        cMessage _msg = new cMessage();

        cUserItemList _UserList = new cUserItemList();
        cUserData _UserData = new cUserData();
        public ucUser()
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

            cbUseYN.Items.Add("Y");
            cbUseYN.Items.Add("N");

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
            _UserList = _UserData.getUser("", txtUserName.Text, cbUseYN.Text);
            UserDataGrid.ItemsSource = _UserList;
        }

        private void doAdd()
        {
            winUserPopup _winUser = new winUserPopup("", "I");

            _winUser.ShowDialog();
            if (_winUser.DialogResult == true)
            {
                doSearch();
            }
        }

        private void doDelete()
        {
            int row = UserDataGrid.SelectedIndex;

            try
            {
                if (MessageBox.Show(_UserList[row].USER_CODE + " 을(를) 삭제하시겠습니까?", "삭제", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _msg = _UserData.delUser(_UserList[row].USER_CODE.ToString());

                    _UserList = _UserData.getUser("", txtUserName.Text, cbUseYN.Text);
                    UserDataGrid.ItemsSource = _UserList;
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

        private void UserDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // datagrid 선택한 index
            int row = UserDataGrid.SelectedIndex;

            try
            {
                winUserPopup _winUser = new winUserPopup(_UserList[row].USER_CODE.ToString(), "U");

                _winUser.ShowDialog();
                if (_winUser.DialogResult == true)
                {
                    _UserList = _UserData.getUser("", txtUserName.Text, cbUseYN.Text);
                    UserDataGrid.ItemsSource = _UserList;
                }
            }
            catch { }
        }
    }
}
