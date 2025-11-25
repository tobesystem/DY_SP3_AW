using dayouAWSWh2.Class;
using dayouAWSWh2.Data;
using dayouAWSWh2.UC;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dayouAWSWh2.View
{
    /// <summary>
    /// winLogin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class winLogin : Window
    {
        cLoginItem _loginItem = new cLoginItem();
        cLoginData _loginData = new cLoginData();

        public winLogin()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
          txtId.Text = Properties.Settings.Default.LoginIDSave;
          txtId.CaretIndex = txtId.Text.Length; // 커서를 맨뒤로 이동

            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                txtPwd.Focus();
            }
            else
            {
                txtId.Focus();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == "")
            {
                lbError.Content = "아이디를 입력해주세요.";
            }
            else if (txtPwd.Password == "")
            {
                lbError.Content = "비밀번호를 입력해주세요.";
            }
            else
            {
                _loginItem = _loginData.getLogin(txtId.Text, ComputeSHA256(txtPwd.Password));

                if(_loginItem.RESULT == "NG")
                {
                  
                    lbError.Content = _loginItem.MESSAGE;
                        
                    return;

                }
                
                if (ckCode.IsChecked == true)
                {
                    Properties.Settings.Default.LoginIDSave = txtId.Text;
                }

                //USER_CODE, USER_NAME 담아놓기
                Properties.Settings.Default.USER_CODE = _loginItem.USER_CODE;
                Properties.Settings.Default.USER_NAME = _loginItem.USER_NAME;
                Properties.Settings.Default.Save();


                DialogResult = true;
            }
        }

        // 비밀번호 암호화 
        private string ComputeSHA256(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(rawData);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);  // 엔터키를 눌렀을 때 로그인 버튼 클릭 이벤트 호출
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void icClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void txtPwd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Console.CapsLock)
                lbError.Content = "Caps Lock이 켜져있습니다.";
            else
                lbError.Content = "";

        }

        private void txtPwd_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Console.CapsLock)
                lbError.Content = "Caps Lock이 켜져있습니다.";
            else
                lbError.Content = "";
        }
    }
}
