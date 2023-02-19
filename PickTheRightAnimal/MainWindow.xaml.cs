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

namespace PickTheRightAnimal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int timerDur = 100;

        static int[] highScore = { 0, 0, 0 };
        static string[] txt = { "Easy", "Normal", "Hard" };
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            tenthsOfSecondsElapsed++;
            float h = (float)timerDur - (tenthsOfSecondsElapsed / 10F);
            timerTextBlock.Text = h.ToString("0.0s");

            if (h == 0)
            {
                timer.Stop();
                GameOver(diff);
            }
        }

        int lives;
        int points;

        int diff = -1;
        private void SetUp(int y)
        {
            foreach (TextBlock textBlock in zaGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "gameOverTextBlock" || textBlock.Name != "finalScoreTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                }
            }

            finalScoreTextBlock.Visibility = Visibility.Hidden;
            gameOverTextBlock.Visibility = Visibility.Hidden;
            retryButton.Visibility = Visibility.Hidden;
            highScoreTextBlock.Visibility = Visibility.Hidden;

            easyButton.Visibility = Visibility.Hidden;
            normalButton.Visibility = Visibility.Hidden;
            hardButton.Visibility = Visibility.Hidden;

            
            

            lives = 3;
            points = 0;

            TempSetUp(y);
            
        }

        
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock clicka = sender as TextBlock;
            if(clicka.Text == matchTextBlock.Text)
            {
                points++;
                scoreTextBlock.Text = $"{points} pts.";
            }
            else
            {
                lives--;
                livesTextBlock.Text = $"{lives} ❤️";
            }

            if (lives == 0)
            {
                GameOver(diff);
            }
            else
            {
                timer.Stop();
                TempSetUp(timerDur);
            }
        }

        private void TempSetUp(int g)
        {
            timerDur = g;
            List<string> emojis = new List<string>()
            {
                "🐒", "🐲", "🦛", "🐍",
                "🐬", "🦈", "🐳", "🕊",
                "🐧", "🦩", "🐣", "🐘",
                "🐁", "🦘", "🦥", "🐔"
            };

            Random random = new Random();
            matchTextBlock.Text = emojis[random.Next(emojis.Count)];
            livesTextBlock.Text = $"{lives} ❤️";
            scoreTextBlock.Text = $"{points} pts.";

            tenthsOfSecondsElapsed = 0;
            timer.Start();

            foreach (TextBlock textBlock in zaGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "")
                {
                    int index = random.Next(emojis.Count);
                    textBlock.Text = emojis[index];
                    emojis.RemoveAt(index);
                }
            }
        }

        private void GameOver(int h)
        {

            highScore[h] = Math.Max(highScore[h], points);
            finalScoreTextBlock.Text = $"Score: {points}";
            highScoreTextBlock.Text = $"High Score ({txt[h]}): {highScore[h]}";
           

            foreach (TextBlock textBlock in zaGrid.Children.OfType<TextBlock>())
            {
                if(textBlock.Name != "gameOverTextBlock" || textBlock.Name != "finalScoreTextBlock")
                {
                    textBlock.Visibility = Visibility.Hidden;
                }
            }

            finalScoreTextBlock.Visibility = Visibility.Visible;
            gameOverTextBlock.Visibility = Visibility.Visible;
            retryButton.Visibility = Visibility.Visible;
            highScoreTextBlock.Visibility = Visibility.Visible;
        }


        private void retryButton_Click(object sender, RoutedEventArgs e)
        {
            restarter();
        }

        private void restarter()
        {
            //throw new NotImplementedException();
            finalScoreTextBlock.Visibility = Visibility.Hidden;
            gameOverTextBlock.Visibility = Visibility.Hidden;
            retryButton.Visibility = Visibility.Hidden;
            highScoreTextBlock.Visibility = Visibility.Hidden;

            easyButton.Visibility = Visibility.Visible;
            normalButton.Visibility = Visibility.Visible;
            hardButton.Visibility = Visibility.Visible;
        }

        private void diffButton_Click(object sender, RoutedEventArgs e)
        {
            Button h = sender as Button;

            switch (h.Name)
            {
                case "easyButton":
                    diff = 0;
                    SetUp(5);
                    break;
                case "normalButton":
                    diff = 1;
                    SetUp(3); 
                    break;
                case "hardButton":
                    diff = 2;
                    SetUp(1);
                    break;
            }
        }


    }
}
