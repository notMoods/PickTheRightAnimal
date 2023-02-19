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

namespace PickTheRightAnimal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int highScore = 0;
        public MainWindow()
        {
            InitializeComponent();
            SetUp();
        }
        int lives;
        int points;
        private void SetUp()
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


            lives = 3;
            points = 0;

            TempSetUp();
            
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
                GameOver();
            }
            else
            {
                TempSetUp();
            }
        }

        private void TempSetUp()
        {
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

        private void GameOver()
        {
            highScore = Math.Max(highScore, points);
            finalScoreTextBlock.Text = $"Score: {points}";
            highScoreTextBlock.Text = $"High Score: {highScore}";
           

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
            SetUp();
        }
    }
}
