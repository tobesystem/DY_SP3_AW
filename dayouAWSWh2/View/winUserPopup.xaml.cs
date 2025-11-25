using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dayouAWSWh2.View
{
    /// <summary>
    /// winUserPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class winUserPopup : Window
    {
        //사용자 세부 조회
        cUserItem _UserItems = new cUserItem();
        cUserData _UserData = new cUserData();

        //부서 comboBox용
        cCommonItemList _DeptList = new cCommonItemList();

        //권한 comboBox용
        cCommonItemList _AuthList = new cCommonItemList();
        cCommonData _CommonData = new cCommonData();

        string _user_code = "";
        string _type = "";
        public winUserPopup(string user_code ,string type)
        {
            InitializeComponent();

            _user_code = user_code;
            _type = type;

            Init();
        }

        private void Init()
        {

            _DeptList = _CommonData.getDeptList();
            cbDept.ItemsSource = _DeptList;

            _AuthList = _CommonData.getAuthList();
            cbAuth.ItemsSource = _AuthList;

            cbUseYN.Items.Add("Y");
            cbUseYN.Items.Add("N");

            //수정 시
            if (_type == "U")
            {
                txtUserCode.Text = _user_code;
                txtUserCode.IsReadOnly = true;

                _UserItems = _UserData.getUserSetting(_user_code);

                txtUserName.Text = _UserItems.USER_NAME;
                txtTelNo.Text = _UserItems.TEL_NO;
                txtEmailF.Text = _UserItems.EMAIL_USER_NAME;
                txtEmailB.Text = _UserItems.EMAIL_DOMAIN;

                cbDept.Text = _UserItems.DEPT_NAME;
            }
        }


        //암호화
        private string ComputeSHA256(string _data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(_data);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // 바이트 배열을 16진수 문자열로 변환
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void icClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtUserCode.Text == "")
            {
                MessageBox.Show("사용자 코드를 입력해주세요.");
                return;
            }

            if(txtUserName.Text == "")
            {
                MessageBox.Show("사용자 명을 입력해주세요.");
                return;
            }

           cMessage _items = new cMessage();

            if(_type == "U")
            {
                _items = _UserData.addUser(txtUserCode.Text, txtUserName.Text, ComputeSHA256(txtUserCode.Text), cbAuth.SelectedValue.ToString(), cbDept.SelectedValue.ToString(), txtTelNo.Text, txtEmailF.Text + "@" + txtEmailB.Text, cbUseYN.Text, "", _type);

            }
            else
            {
                _items = _UserData.addUser(txtUserCode.Text, txtUserName.Text, ComputeSHA256(txtUserCode.Text), cbAuth.SelectedValue.ToString(), cbDept.SelectedValue.ToString(), txtTelNo.Text, txtEmailF.Text + "@" + txtEmailB.Text, cbUseYN.Text, "", _type);
            }

            if(_items.RESULT == "NG")
            {
                MessageBox.Show(_items.MSG);
                return;
            }

            DialogResult = true;
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
