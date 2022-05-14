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
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace kursovaRobota
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer = null;
        private int _time = 0;
        private const int _size = 12;
        private int[] arrMap = new int[24];
        private string[] _animals = new string[_size] {"bee", "cat", "cow", "dog", "fox", "hen", "horse", "lion", "owl", 
            "panda", "snake", "whale"};

        public MainWindow()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(timerTick);
            _timer.Interval = new TimeSpan(0, 0, 1);

            for (int i = 0; i < 24; i++)
                arrMap[i] = -1;

            InitializeComponent();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = 0, a = 0;
            for (int j = 0; j < 24; j++)
                arrMap[j] = -1;

            Random rnd = new Random();
            var item = MainGrid.Children.OfType<Button>().ToArray();

            while (a != 12)
            {
                var animal = _animals[i];
                
                var rand1 = rnd.Next(0, 24);
                var newrand = rnd.Next(0, 12);
                var rand2 = rnd.Next(0, 24);
                if (arrMap[rand1] == -1 && rand1 != rand2)
                {
                    while (arrMap[rand2] == -1)
                    {

                        arrMap[rand2] = newrand;
                        arrMap[rand1] = newrand;
                        Uri resourceUri = new Uri(animal + ".png", UriKind.Relative);
                        StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                        BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                        var brush = new ImageBrush();
                        brush.ImageSource = temp;
                        item[rand2].Background = brush;
                        item[rand1].Background = brush;

                        a++;
                        i++;
                    }
                }
                else continue;
            }

            _time = 0;
            _timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            _time++;
            this.timerLabel.Content = _time;
        }
    }
}
