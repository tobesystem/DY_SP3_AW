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
using System.Windows.Shapes;

namespace dayouAWSWh2.View
{
    /// <summary>
    /// winErrorPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class winErrorPopup : Window
    {
        cMessage _message = new cMessage();
        cErrorData _errData = new cErrorData();

        private string _machine_type;
        private string _err_code;
        private string _err_name;
        private string _etc;
        private string _type;
        public winErrorPopup(string machine_type, string err_code, string err_name , string etc,string type, string header_txt)
        {
            InitializeComponent();
           
            _type = type;
            _machine_type = machine_type;
            _err_code = err_code;
            _err_name = err_name;
            _etc = etc;
            txtHeader.Content = header_txt;
            Init();
        }

        private void Init() {

            cbType.Items.Add("CV");
            cbType.Items.Add("SC");

            //수정
            if(_type == "U")
            {
                cbType.Text = _machine_type;
                cbType.IsEnabled = false;

                txtErrCode.Text = _err_code;
                txtErrCode.IsReadOnly = true;

                txtErrName.Text = _err_name;

                txtErrEtc.Text = _etc;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(_type == "I")
            {
                _message = _errData.updateErrorCode(cbType.Text, txtErrCode.Text, txtErrName.Text, txtErrEtc.Text, _type);
                if (_message.RESULT == "NG")
                {
                    MessageBox.Show(_message.MSG);
                    return;
                }
                DialogResult = true;
            }
            else if(_type == "U")
            {
                _message = _errData.updateErrorCode(cbType.Text, txtErrCode.Text, txtErrName.Text, txtErrEtc.Text, _type);
                if (_message.RESULT == "NG")
                {
                    MessageBox.Show(_message.MSG);
                    return;
                }
                DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void icClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
