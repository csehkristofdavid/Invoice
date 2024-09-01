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

namespace InvoiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string savedName;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditProfile(object sender, RoutedEventArgs e)
        {
            var profilPanel = new StackPanel
            {
                Margin = new Thickness(20)
            };

            profilPanel.Children.Add(new TextBlock 
            {
                Text = "Cég név",
                FontSize = 16
            });
            var nameTextBox = new TextBox
            {
                FontSize = 16,
                Margin = new Thickness(0,0,0,10)
            };
            profilPanel.Children.Add(nameTextBox);

            var saveButton = new Button
            {
                Content = "Mentés",
                FontSize = 16,
                Padding = new Thickness(10, 5, 10, 5)
            };
            saveButton.Click += (s, args) =>
            {
                savedName = nameTextBox.Text;
                MessageBox.Show($"Név elmentve: {savedName}");
            };
            profilPanel.Children.Add(saveButton);

            ContentArea.Children.Clear();
            ContentArea.Children.Add(profilPanel);
        }
    }
}
