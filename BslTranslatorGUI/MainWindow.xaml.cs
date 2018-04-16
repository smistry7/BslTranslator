using System;
using System.ComponentModel;
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
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;
using MessageBox = System.Windows.MessageBox;

namespace BslTranslatorGUI
{

    public partial class MainWindow : System.Windows.Window
    {
        AddGesture addGesture = new AddGesture();
        private WekaClassifier wekaClassifier;
        public static Controller controller;
        private BackgroundWorker worker;
        private BackgroundWorker updater;
        RuleClassifier _ruleClassifier;

        public delegate void UpdateTextCallback(string text);

        public MainWindow()
        {
            InitializeComponent();
            wekaClassifier = new WekaClassifier(true);
            wekaClassifier.LoadGestures();
            GestureList.Text = TextBoxValues.GestureList;
            worker = new BackgroundWorker();
            worker.DoWork += worker_doWork;
            updater = new BackgroundWorker();
            updater.DoWork += TextBoxUpdater;

        }
        public void TextBoxUpdater(object sender, DoWorkEventArgs e)
        {
            while (!updater.CancellationPending)
            {
                Thread.Sleep(100);
                if (TextBoxValues.Text != null)
                {
                    this.Dispatcher.Invoke(() => GestureText2.Selection.Text = TextBoxValues.Text);
                }
                if (TextBoxValues.HandCount != null)
                {
                    this.Dispatcher.Invoke(() => HandCount2.Text = TextBoxValues.HandCount);
                }
                if (TextBoxValues.SecondOption != null)
                {
                    this.Dispatcher.Invoke(() => SecondOption.Text = TextBoxValues.SecondOption);
                }
            }
        }
        private void StopCapture_Click(object sender, RoutedEventArgs e)
        {
            controller.StopConnection();
            controller.Dispose();
            ConnectionLabel.Background = Brushes.Red;
        }

        public void DelegateMethod(string text)
        {
            GestureText2.Selection.Text += text;
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
            if (updater.IsBusy) return;
            worker.RunWorkerAsync();
            updater.RunWorkerAsync();
        }

        private void worker_doWork(object sender,DoWorkEventArgs e)
        {
            controller = new Controller();
            this.SecondOption.KeyDown += new KeyEventHandler(CheckEnterKeyPress);
            wekaClassifier = new WekaClassifier(true);
            controller.Device += wekaClassifier.OnConnect;
            controller.FrameReady += wekaClassifier.OnFrame;
            if (controller.IsConnected)
            {
                this.Dispatcher.Invoke(() => Connection2.Background = Brushes.Green);

            }
            else
            {
                MessageBox.Show("Controller not connected");
                this.Dispatcher.Invoke(() => Connection2.Background = Brushes.Red);
            }
        }

        private void CheckEnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            TextBoxValues.Text = TextBoxValues.Text.Remove(TextBoxValues.Text.Length - 1, 1);
            TextBoxValues.Text += TextBoxValues.SecondOption;
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
            TextBoxValues.Text = "";
        }


        private void StopCapture2_Click(object sender, RoutedEventArgs e)
        {
            controller.StopConnection();
            controller.Dispose();
            Connection2.Background = Brushes.Red;
           
            updater.CancelAsync();
        }




        private void Space2_Click(object sender, RoutedEventArgs e)
        {
            TextBoxValues.Text += " ";
        }

        private void BackSpace2_Click(object sender, RoutedEventArgs e)
        {
            if (GestureText2.Selection.Text.Length > 0)
            {
                TextBoxValues.Text = GestureText2.Selection.Text.Remove(GestureText2.Selection.Text.Length - 1, 1);
            }
        }
    }
}

