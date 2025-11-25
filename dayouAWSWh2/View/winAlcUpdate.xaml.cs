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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace dayouAWSWh2.View
{
    /// <summary>
    /// winAlcUpdate.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class winAlcUpdate : Window
    {
        cHostData _hostData = new cHostData();
        cMessage _items = new cMessage();

        private string _sysDt;
        private string _smtNo;
        private string _status;
        private string _carCode;

        public winAlcUpdate(string sys_dt, string cmt_no, string status)
        {
            InitializeComponent();

            _sysDt = sys_dt;
            _smtNo = cmt_no;
            _status = status;
           
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txt1.Text.Length != 9)
            {
                MessageBox.Show("ALC는 9자리를 입력해주세요");
                return;
            }
            _items = _hostData.alcUpdateHost(_sysDt, _smtNo, _status, txt1.Text);
            MessageBox.Show(_items.MSG);
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;
        }
    }
}
