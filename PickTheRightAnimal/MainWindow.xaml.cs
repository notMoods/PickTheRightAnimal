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

            Random random= new Random();
            matchTextBlock.Text = emojis[random.Next(emojis.Count)];

            foreach(TextBlock textBlock in zaGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "titleTextBlock" && textBlock.Name !="matchTextBlock" )
                {
                    int index = random.Next(emojis.Count);
                    textBlock.Text = emojis[index];
                    emojis.RemoveAt(index);
                }
            }
        }
    }
}
