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
        public MainWindow()
        {
            InitializeComponent();
            SetUp();
        }
        int lives = 3;
        private void SetUp()
        {
            //throw new NotImplementedException();
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

            foreach(TextBlock textBlock in zaGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "")
                {
                    int index = random.Next(emojis.Count);
                    textBlock.Text = emojis[index];
                    emojis.RemoveAt(index);
                }
            }
        }

        int points = 0;
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
                SetUp();
            }
        }

        private void GameOver()
        {
            finalScoreTextBlock.Text = $"Score: {points}";

            foreach (TextBlock textBlock in zaGrid.Children.OfType<TextBlock>())
            {
                if(textBlock.Name != "gameOverTextBlock" || textBlock.Name != "finalScoreTextBlock")
                {
                    textBlock.Visibility = Visibility.Hidden;
                }
                
                finalScoreTextBlock.Visibility = Visibility.Visible;
                gameOverTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
