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
    /// winAlarm.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class winAlarm : Window
    {
        cAlarmData _alarmData = new cAlarmData();
        public winAlarm(double _alarmTimeTxt)
        {
            InitializeComponent();
            //lbAlarm.Content = _alarmTxt;
            alarmTimeTxt.Content = _alarmTimeTxt.ToString() + " 분";
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if(checkBox1.IsChecked == true)
            {
                _alarmData.UpdateAlarm();
            }
            this.Close();
        }
    }
}
