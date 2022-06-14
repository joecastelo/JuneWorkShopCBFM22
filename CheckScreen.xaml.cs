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

namespace June22WorkShopPCheck
{
    /// <summary>
    /// Interaction logic for CheckScreen.xaml
    /// </summary>
    public partial class CheckScreen : UserControl
    {

        public string Title { get; set; }

        public string Result { get; set; }
        public string Comment { get; set; }

        public Brush ResultColor { get; set; }

        public CheckScreen(string title, string result, string comment)
        {
            DataContext = this;
            Title = title;
            Comment = comment;
            Result = result;

            if (Result == "OK")
            {
                var brush = new SolidColorBrush(Colors.LightGreen);

                ResultColor = brush;
            }
            else {
                var brush = new SolidColorBrush(Colors.LightSalmon);

                ResultColor = brush;
            }
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CommentBlock.Visibility == Visibility.Collapsed)
            {
                CommentBlock.Visibility = Visibility.Visible;
            }
            else
            {
                CommentBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button_Click(sender, e);
        }
    }
}
