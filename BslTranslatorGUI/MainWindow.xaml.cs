using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BslTranslatorWeka;
using Leap;
using RandomForestTranslator;
using MessageBox = System.Windows.MessageBox;

namespace BslTranslatorGUI
{

    public partial class MainWindow : System.Windows.Window
    {
        AddGesture addGesture = new AddGesture();
        private WekaClassifier wekaClassifier;
        public static Controller controller;

        RuleClassifier _ruleClassifier;
        public MainWindow()
        {
            InitializeComponent();
            wekaClassifier = new WekaClassifier();
            wekaClassifier.LoadGestures(GestureList);
        }

        private void StopCapture_Click(object sender, RoutedEventArgs e)
        {
            controller.StopConnection();
            controller.Dispose();
            ConnectionLabel.Background = Brushes.Red;
        }


  
        private void BeginCapture_Click(object sender, RoutedEventArgs e)
        {
            controller = new Controller();
            _ruleClassifier = new RuleClassifier(GestureText, HandCount);

            controller.Device += _ruleClassifier.OnConnect;
            controller.FrameReady += _ruleClassifier.OnFrame;
            Thread.Sleep(500);

            if (controller.IsConnected)
            {
                ConnectionLabel.Background = Brushes.Green;
            }
            else
            {
                MessageBox.Show("Controller not connected");
                ConnectionLabel.Background = Brushes.Red;
            }


        }
   
        private void BeginCapture2_Click(object sender, RoutedEventArgs e)
        {
            controller = new Controller();
            wekaClassifier = new WekaClassifier(GestureText2,HandCount2,SecondOption);
            controller.Device += wekaClassifier.OnConnect;
            controller.FrameReady += wekaClassifier.OnFrame;
            if (controller.IsConnected)
            {
                Connection2.Background= Brushes.Green;
            }
            else
            {
                MessageBox.Show("Controller not connected");
                ConnectionLabel.Background = Brushes.Red;
            }


        }
        private void ClearText_Click(object sender, RoutedEventArgs e)
        {
            GestureText.Clear();
        }

        private void HandCount_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Space_Click(object sender, RoutedEventArgs e)
        {
            GestureText.Text += " ";
        }

        private void BackSpace_Click(object sender, RoutedEventArgs e)
        {
            GestureText.Text = GestureText.Text.Remove(GestureText.Text.Length - 1, 1);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GestureText2_TextChanged(object sender, TextChangedEventArgs e)
        {
            GestureText2.ScrollToEnd();
        }

        private void GestureText_TextChanged(object sender, TextChangedEventArgs e)
        {
            GestureText.ScrollToEnd();
        }



        private void AddGesture_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxManager.Yes = "1";
            MessageBoxManager.No = "2";
            MessageBoxManager.Register();
            var response = MessageBox.Show("How many hands does this gesture require", "", MessageBoxButton.YesNoCancel);

            if (response == MessageBoxResult.Yes)
            {
                addGesture.AddOneHandedGesture(this.Owner);
            }
            else if (response == MessageBoxResult.No)
            {
                addGesture.AddTwoHandedGesture(Owner);
            }
            MessageBoxManager.Unregister();
        }

        private void ClearText2_Click(object sender, RoutedEventArgs e)
        {
            GestureText2.Selection.Text = "";
        }


        private void StopCapture2_Click(object sender, RoutedEventArgs e)
        {
            controller.StopConnection();
            controller.Dispose();
            Connection2.Background = Brushes.Red;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void HandCount2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

