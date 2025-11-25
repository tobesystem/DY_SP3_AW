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
    /// ucToolbarBtn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucToolbarBtn : UserControl
    {
        public static readonly DependencyProperty ButtonStatesProperty =
        DependencyProperty.Register("ButtonStates", typeof(Dictionary<int, bool>), typeof(ucToolbarBtn), new PropertyMetadata(new Dictionary<int, bool>()));

        public Dictionary<int, bool> ButtonStates
        {
            get { return (Dictionary<int, bool>)GetValue(ButtonStatesProperty); }
            set { SetValue(ButtonStatesProperty, value); }
        }


        public event EventHandler Button1Clicked;
        public event EventHandler Button2Clicked;
        public event EventHandler Button3Clicked;
        public event EventHandler Button4Clicked;
        public event EventHandler Button5Clicked;

        public ucToolbarBtn()
        {
            InitializeComponent();

            ButtonStates = new Dictionary<int, bool>
        {
            { 1, true },
            { 2, true },
            { 3, true },
            { 4, true },
            { 5, true }
            };
        }

        //신규버튼
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("1");
            Button1Clicked?.Invoke(this, EventArgs.Empty);
        }

        //조회버튼
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("2");
            Button2Clicked?.Invoke(this, EventArgs.Empty);
        }

        //저장버튼
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            Button3Clicked?.Invoke(this, EventArgs.Empty);
        }

        //취소버튼
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            Button4Clicked?.Invoke(this, EventArgs.Empty);
        }

        //삭제버튼
        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            Button5Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
