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
using System.Windows.Shapes;

namespace dayouAWSWh2.View
{
    /// <summary>
    /// winPalletPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class winPalletPopup : Window
    {
        cPalletItemList _palletStatusList = new cPalletItemList();
        cPalletData _palletData = new cPalletData();
        private string _palletCode = "";
        private string _palletNm = "";
        private string _badCode = "";
        private string _type = "";
        private string _headerName = "";
        private cMessage _message = new cMessage();
        private string _carCode = "";

        public winPalletPopup(string pallet_code, string pallet_nm, string bad_code ,string header_name,string type, string carCode)
        {
            InitializeComponent();
            _palletCode = pallet_code;
            _palletNm = pallet_nm;
            _badCode = bad_code;
            _headerName = header_name;
            _type = type;
            _carCode = carCode;
            Init();
        }

        private void Init()
        {
            if(_type == "0")
            {
                txtPalletCode.IsReadOnly = true;
            }
            txtHeader.Content = _headerName;

            txtPalletCode.Text = _palletCode;
            cbGbn.Items.Add("FRONT");
            cbGbn.Items.Add("REAR");
            cbGbn.SelectedItem = _palletNm;

            _palletStatusList = _palletData.getPalletStatus();
            cbBadPallet.ItemsSource = _palletStatusList;

            cbBadPallet.Text = _badCode;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(_palletStatusList[cbBadPallet.SelectedIndex].BADCD);
            //MessageBox.Show(cbBadPallet.SelectedValue.ToString());
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(_type == "0")
            {
                //수정
                _message =_palletData.updatePallet(txtPalletCode.Text, cbGbn.Text == "REAR" ? "R" : "F", cbBadPallet.Text, "U", _carCode);
                if(_message.RESULT == "NG")
                {
                    MessageBox.Show(_message.MSG);
                    return;
                }
                DialogResult = true;
            }
            else
            {
                //추가
                _message = _palletData.updatePallet(txtPalletCode.Text, cbGbn.Text == "REAR" ? "R" : "F", cbBadPallet.Text, "I", _carCode);
                if (_message.RESULT == "NG")
                {
                    MessageBox.Show(_message.MSG);
                    return;
                }
                DialogResult = true;
            }
        }

        private void icClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
