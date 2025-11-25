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
    /// winHostAdd.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class winHostAdd : Window
    {
        cHostData _hostData = new cHostData();
        cMessage _items = new cMessage();
        string _carCode = "";

        public winHostAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtDate.Text == "")
            {
                MessageBox.Show("의장투입일시를 입력해주세요");
            }
            else if(txtCommit.Text == "")
            {
                MessageBox.Show("COMMITNO를 입력해주세요");
            }
            else if(txtAlc.Text == "")
            {
                MessageBox.Show("ALC를 입력해주세요");
            }
            else if(txtRegion.Text == "")
            {
                MessageBox.Show("지역을 입력해주세요");
            }else if(txtNation.Text == "")
            {
                MessageBox.Show("사양국을 입력해주세요");
            }
            else if(txtColor.Text == "")
            {
                MessageBox.Show("내장색을 입력해주세요");
            }
            else if (txtDate.Text.Length != 14)
            {
                MessageBox.Show("의장투입일시을 정확히 입력해주세요");
            }
            else if(txtAlc.Text.Length != 9)
            {
                MessageBox.Show("ALC는 9자리 입니다");
            }
            else
            {
                _items = _hostData.alcAddHost(txtDate.Text, txtCommit.Text, txtAlc.Text, txtBody.Text ,txtRegion.Text, txtNation.Text, txtColor.Text);

                if(_items.RESULT == "NG")
                {
                    MessageBox.Show(_items.MSG);
                    return;
                }

                this.DialogResult = true;
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
