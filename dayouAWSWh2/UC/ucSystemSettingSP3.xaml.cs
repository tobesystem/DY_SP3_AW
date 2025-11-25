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
using System.Windows.Threading;

namespace dayouAWSWh2.UC
{
    /// <summary>
    /// ucSystemSettingSP3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucSystemSettingSP3 : UserControl
    {
        cStatusItemList _CurrentstatusList = new cStatusItemList();
        cStatusItemList _ComsetstatusList = new cStatusItemList();
        cStatusItemList _ProgramList = new cStatusItemList();
        cStatusItemList _programStatusList = new cStatusItemList();

        cStatusData _statusData = new cStatusData();

        private DispatcherTimer _statusTimer = new DispatcherTimer();
        public ucSystemSettingSP3()
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
                { 2, true },   // 조회
                { 3, true },   // 저장
                { 4, true },   // 취소
                { 5, false }   // 삭제
            };

            // 툴바 버튼 클릭 이벤트
            ucToolbarBtn.Button2Clicked += ucToolbarBtn_BtnSearch;
            ucToolbarBtn.Button3Clicked += ucToolbarBtn_BtnSave;
            ucToolbarBtn.Button4Clicked += ucToolbarBtn_BtnCancel;

            // 서버시간을 1분마다 갱신한다.
            _statusTimer.Interval = TimeSpan.FromMilliseconds(2000); // 시간 간격 설정
            _statusTimer.Tick += statusTimer_Tick;
            _statusTimer.Start();

            doSearch();
            programStatusShow();
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            programStatusShow();
        }


        private void programStatusShow()
        {
            _programStatusList = _statusData.programStatusget_WH2();
            for (int i = 0; i < _programStatusList.Count; i++)
            {
                if (_programStatusList[i].Current_Name == "WH2_CVC")
                {
                    if (_programStatusList[i].Option1 == 0)
                    {
                        border1.Background = new SolidColorBrush(Color.FromRgb(206, 212, 218));
                    }
                    else
                    {
                        border1.Background = new SolidColorBrush(Colors.LightGreen);
                    }
                }
                if (_programStatusList[i].Current_Name == "WH2_SCC")
                {
                    if (_programStatusList[i].Option1 == 0)
                    {
                        border2.Background = new SolidColorBrush(Color.FromRgb(206, 212, 218));
                    }
                    else
                    {
                        border2.Background = new SolidColorBrush(Colors.LightGreen);
                    }
                }
                if (_programStatusList[i].Current_Name == "WH2_BCRIN")
                {
                    if (_programStatusList[i].Option1 == 0)
                    {
                        border3.Background = new SolidColorBrush(Color.FromRgb(206, 212, 218));
                    }
                    else
                    {
                        border3.Background = new SolidColorBrush(Colors.LightGreen);
                    }
                }
                if (_programStatusList[i].Current_Name == "WH2_BCROUT")
                {
                    if (_programStatusList[i].Option1 == 0)
                    {
                        border4.Background = new SolidColorBrush(Color.FromRgb(206, 212, 218));
                    }
                    else
                    {
                        border4.Background = new SolidColorBrush(Colors.LightGreen);
                    }
                }

                if (_programStatusList[i].Current_Name == "WH2_HOST")
                {
                    if (_programStatusList[i].Option1 == 0)
                    {
                        border5.Background = new SolidColorBrush(Color.FromRgb(206, 212, 218));
                    }
                    else
                    {
                        border5.Background = new SolidColorBrush(Colors.LightGreen);
                    }
                }
                if (_programStatusList[i].Current_Name == "WH2_PROD")
                {
                    if (_programStatusList[i].Option1 == 0)
                    {
                        border6.Background = new SolidColorBrush(Color.FromRgb(206, 212, 218));
                    }
                    else
                    {
                        border6.Background = new SolidColorBrush(Colors.LightGreen);
                    }
                }

            }
        }

        private void ucToolbarBtn_BtnSearch(object sender, EventArgs e)
        {
            doSearch();
        }

        private void ucToolbarBtn_BtnSave(object sender, EventArgs e)
        {
            doSave();
        }

        private void ucToolbarBtn_BtnCancel(object sender, EventArgs e)
        {
            doCancel();
        }

        private void doSearch()
        {
            doClear();

            //제어설정 리스트
            _CurrentstatusList = _statusData.getCurrentStatusList_WH2();
            for (int i = 0; i < _CurrentstatusList.Count; i++)
            {
                if (_CurrentstatusList[i].Current_Name == "WH2_AUTO_HOSTOUT")
                {
                    if (_CurrentstatusList[i].Option1 == 0)
                    {
                        cbHostOut.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option1 == 1)
                    {
                        cbHostOut.IsChecked = true;
                    }
                }
                if (_CurrentstatusList[i].Current_Name == "WH2_BCRMODE")
                {
                    if (_CurrentstatusList[i].Option1 == 0)
                    {
                        cbInBcr.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option1 == 1)
                    {
                        cbInBcr.IsChecked = true;
                    }

                    if (_CurrentstatusList[i].Option2 == 0)
                    {
                        cbOutBcr.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option2 == 1)
                    {
                        cbOutBcr.IsChecked = true;
                    }

                }


                if (_CurrentstatusList[i].Current_Name == "WH2_SC_IN_SET")
                {
                    if (_CurrentstatusList[i].Option1 == 0)
                    {
                        cbInSet1.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option1 == 1)
                    {
                        cbInSet1.IsChecked = true;
                    }

                    if (_CurrentstatusList[i].Option2 == 0)
                    {
                        cbInSet2.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option2 == 1)
                    {
                        cbInSet2.IsChecked = true;
                    }
                }

                if (_CurrentstatusList[i].Current_Name == "WH2_SC_IN_ORDER")
                {
                    if (_CurrentstatusList[i].Option1 == 0)
                    {
                        cbInOrder1.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option1 == 1)
                    {
                        cbInOrder1.IsChecked = true;
                    }

                    if (_CurrentstatusList[i].Option2 == 0)
                    {
                        cbInOrder2.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option2 == 1)
                    {
                        cbInOrder2.IsChecked = true;
                    }
                }

                if (_CurrentstatusList[i].Current_Name == "WH2_SC_OUT_SET")
                {
                    if (_CurrentstatusList[i].Option1 == 0)
                    {
                        cbOutSet1.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option1 == 1)
                    {
                        cbOutSet1.IsChecked = true;
                    }

                    if (_CurrentstatusList[i].Option2 == 0)
                    {
                        cbOutSet2.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option2 == 1)
                    {
                        cbOutSet2.IsChecked = true;
                    }
                }

                if (_CurrentstatusList[i].Current_Name == "WH2_SC_OUT_ORDER")
                {
                    if (_CurrentstatusList[i].Option1 == 0)
                    {
                        cbOutOrder1.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option1 == 1)
                    {
                        cbOutOrder1.IsChecked = true;
                    }

                    if (_CurrentstatusList[i].Option2 == 0)
                    {
                        cbOutOrder2.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option2 == 1)
                    {
                        cbOutOrder2.IsChecked = true;
                    }
                }

                if (_CurrentstatusList[i].Current_Type == "PROGRAM")
                {
                    if (!_ProgramList.Any(item => item.Equals(_CurrentstatusList[i])))
                    {
                        _ProgramList.Add(_CurrentstatusList[i]);
                    }
                }

                if (_CurrentstatusList[i].Current_Name == "WH2_CHECKMODE")
                {
                    if (_CurrentstatusList[i].Option1 == 0)
                    {
                        cbLotCheck.IsChecked = false;
                    }
                    else if (_CurrentstatusList[i].Option1 == 1)
                    {
                        cbLotCheck.IsChecked = true;
                    }
                }
            }

            //소켓 통신설정 리스트
            _ComsetstatusList = _statusData.getComsetStatusList_WH2();
            for (int i = 0; i < _ComsetstatusList.Count; i++)
            {
                if (_ComsetstatusList[i].ID_CODE == "WH2_CVC")
                {
                    txtIP1.Text = _ComsetstatusList[i].ID_IP;
                    txt1Port1.Text = _ComsetstatusList[i].ID_PORT1;
                    txt2Port1.Text = _ComsetstatusList[i].ID_PORT2;

                    if (_ComsetstatusList[i].ID_LOG == 0)
                    {
                        cbLog1.IsChecked = false;
                    }
                    else
                    {
                        cbLog1.IsChecked = true;
                    }
                }

                if (_ComsetstatusList[i].ID_CODE == "WH2_SCC")
                {
                    txtIP2.Text = _ComsetstatusList[i].ID_IP;
                    txt1Port2.Text = _ComsetstatusList[i].ID_PORT1;
                    txt2Port2.Text = _ComsetstatusList[i].ID_PORT2;

                    if (_ComsetstatusList[i].ID_LOG == 0)
                    {
                        cbLog2.IsChecked = false;
                    }
                    else
                    {
                        cbLog2.IsChecked = true;
                    }
                }

                if (_ComsetstatusList[i].ID_CODE == "WH2_BCR1")
                {
                    txtIP3.Text = _ComsetstatusList[i].ID_IP;
                    txt1Port3.Text = _ComsetstatusList[i].ID_PORT1;
                    txt2Port3.Text = _ComsetstatusList[i].ID_PORT2;
                }

                if (_ComsetstatusList[i].ID_CODE == "WH2_BCR2")
                {
                    txtIP4.Text = _ComsetstatusList[i].ID_IP;
                    txt1Port4.Text = _ComsetstatusList[i].ID_PORT1;
                    txt2Port4.Text = _ComsetstatusList[i].ID_PORT2;
                }

                if (_ComsetstatusList[i].ID_CODE == "WH2_KIA")
                {
                    txtIP5.Text = _ComsetstatusList[i].ID_IP;
                    txt1Port5.Text = _ComsetstatusList[i].ID_PORT1;
                    txt2Port5.Text = _ComsetstatusList[i].ID_PORT2;

                    if (_ComsetstatusList[i].ID_LOG == 0)
                    {
                        cbLog3.IsChecked = false;
                    }
                    else
                    {
                        cbLog3.IsChecked = true;
                    }
                }

                if (_ComsetstatusList[i].ID_CODE == "WH2_POP")
                {
                    txtIP6.Text = _ComsetstatusList[i].ID_IP;
                    txt1Port6.Text = _ComsetstatusList[i].ID_PORT1;
                    txt2Port6.Text = _ComsetstatusList[i].ID_PORT2;

                    if (_ComsetstatusList[i].ID_LOG == 0)
                    {
                        cbLog4.IsChecked = false;
                    }
                    else
                    {
                        cbLog4.IsChecked = true;
                    }
                }

            }

            comboProgram.ItemsSource = _ProgramList;
            comboProgram.SelectedIndex = 0;

        }

        private void doSave()
        {

            if (MessageBox.Show("저장 하시겠습니까?", "저장", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _statusData.statusUpdate_WH2(cbHostOut.IsChecked == true ? 1 : 0, cbInBcr.IsChecked == true ? 1 : 0, cbOutBcr.IsChecked == true ? 1 : 0, cbInSet1.IsChecked == true ? 1 : 0, cbInSet2.IsChecked == true ? 1 : 0, cbInOrder1.IsChecked == true ? 1 : 0, cbInOrder2.IsChecked == true ? 1 : 0, cbOutSet1.IsChecked == true ? 1 : 0, cbOutSet2.IsChecked == true ? 1 : 0
                                        , cbOutOrder1.IsChecked == true ? 1 : 0, cbOutOrder2.IsChecked == true ? 1 : 0, txtIP1.Text, txtIP2.Text, txtIP3.Text, txtIP4.Text, txtIP5.Text, txtIP6.Text
                                         , txt1Port1.Text, txt1Port2.Text, txt1Port3.Text, txt1Port4.Text, txt1Port5.Text, txt1Port6.Text, txt2Port1.Text, txt2Port2.Text, txt2Port3.Text, txt2Port4.Text, txt2Port5.Text, txt2Port6.Text
                                         , cbLog1.IsChecked == true ? 1 : 0, cbLog2.IsChecked == true ? 1 : 0, cbLog3.IsChecked == true ? 1 : 0, cbLog4.IsChecked == true ? 1 : 0, cbLotCheck.IsChecked == true ? 1 : 0);
            }
            else
            {

            }
        }

        private void doClear()
        {
            _CurrentstatusList = new cStatusItemList();
            _ComsetstatusList = new cStatusItemList();
            _ProgramList = new cStatusItemList();
            _programStatusList = new cStatusItemList();
        }

        private void doCancel()
        {
            doSearch();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            _statusData.programStatusUpdate_WH2(_ProgramList[comboProgram.SelectedIndex].Current_index, "0");
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _statusData.programStatusUpdate_WH2(_ProgramList[comboProgram.SelectedIndex].Current_index, "1");
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            doSearch();
        }
    }
}
