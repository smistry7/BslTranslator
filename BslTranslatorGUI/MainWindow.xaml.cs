using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
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
        readonly AlterModel _alterModel = new AlterModel();
        private WekaClassifier _wekaClassifier;
        public static Controller Controller;
        private BackgroundWorker _wekaWorker;
        private BackgroundWorker _updater;
        private RuleClassifier _ruleClassifier;
        public delegate void UpdateTextCallback(string text);

        public MainWindow()
        {
            InitializeComponent();
            _wekaClassifier = new WekaClassifier(true);
            _wekaClassifier.LoadGestures();
            GestureList.Text = TextBoxValues.GestureList;
            InitialiseBackgroundWorkers();
        }

        private void InitialiseBackgroundWorkers()
        {
            _wekaWorker = new BackgroundWorker();
            _wekaWorker.DoWork += WekaWorkerDoWork;
            _updater = new BackgroundWorker();
            _updater.DoWork += TextBoxUpdater;


            _wekaWorker.WorkerSupportsCancellation = true;
            _updater.WorkerSupportsCancellation = true;
        }

        public void TextBoxUpdater(object sender, DoWorkEventArgs e)
        {
            while (!_updater.CancellationPending)
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
            if (Controller != null)
            {
                Controller.StopConnection();
                Controller.Dispose();
            }
            ConnectionLabel.Background = Brushes.Red;
        }


        private void BeginCapture_Click(object sender, RoutedEventArgs e)
        {
            Controller = new Controller();
            _ruleClassifier = new RuleClassifier(GestureText, HandCount);


            Controller.Device += _ruleClassifier.OnConnect;
            Controller.FrameReady += _ruleClassifier.OnFrame;
            Thread.Sleep(500);

            if (Controller.IsConnected)
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
            if (_updater.IsBusy) return;
            _wekaWorker.RunWorkerAsync();
            _updater.RunWorkerAsync();
        }

        private void WekaWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            Controller = new Controller();
            this.SecondOption.KeyDown += new KeyEventHandler(CheckEnterKeyPress);
            _wekaClassifier = new WekaClassifier(true);
            Controller.Device += _wekaClassifier.OnConnect;
            Controller.FrameReady += _wekaClassifier.OnFrame;
            if (Controller.IsConnected)
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



        private void Space_Click(object sender, RoutedEventArgs e)
        {
            GestureText.Text += " ";
        }

        private void BackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (GestureText.Text.Length > 0)
            {
                GestureText.Text = GestureText.Text.Remove(GestureText.Text.Length - 1, 1);
            }
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
                _alterModel.AddOneHandedGesture(this.Owner);
            }
            else if (response == MessageBoxResult.No)
            {
                _alterModel.AddTwoHandedGesture(Owner);
            }
            MessageBoxManager.Unregister();
        }

        private void ClearText2_Click(object sender, RoutedEventArgs e)
        {
            TextBoxValues.Text = "";
        }


        private void StopCapture2_Click(object sender, RoutedEventArgs e)
        {
            if (Controller != null)
            {
                Controller.StopConnection();
                Controller.Dispose();
            }
            Connection2.Background = Brushes.Red;

            _updater.CancelAsync();
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

        private void AddTrainingData_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxManager.Yes = "1";
            MessageBoxManager.No = "2";
            MessageBoxManager.Register();
            var response = MessageBox.Show("How many hands does this gesture require", "", MessageBoxButton.YesNoCancel);

            if (response == MessageBoxResult.Yes)
            {
                _alterModel.AddOneHandedTrainingData();
            }
            else if (response == MessageBoxResult.No)
            {
                _alterModel.AddTwoHandedTrainingData();
            }
            MessageBoxManager.Unregister();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var gestures = TextBoxValues.GestureList.Split('\n');
            var searchResults = "";
            var searchTerm = SearchTerm.Text;
            foreach (var gesture in gestures)
            {
                if (gesture.Contains(searchTerm))
                {
                    searchResults += gesture + "\n";
                }
            }
            GestureList.Text = searchResults;
        }

    }
}

