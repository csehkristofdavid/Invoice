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
using System.Data.SQLite;

namespace InvoiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string dbConnectionString = "Data Source=invoiceapp.db;Version=3;";
        private TextBox nameTextBox;
        public MainWindow()
        {
            InitializeComponent();
            CreateDatabase();
        }

        private void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(dbConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS UserProfile (Name TEXT)",
                    connection);
                command.ExecuteNonQuery();
            }
        }

        private void LoadName()
        {
            using (var connection = new SQLiteConnection(dbConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand("Select Name from UserProfile LIMIT 1",
                    connection);
                var result = command.ExecuteScalar() as string;
                if (nameTextBox != null)
                {
                    nameTextBox.Text = result ?? string.Empty;
                    nameTextBox.IsReadOnly = false;
                }
            }
        }

        private void SaveName(string name)
        {
            using (var connection = new SQLiteConnection(dbConnectionString))
            {
                connection.Open();
                var deleteCommand = new SQLiteCommand("DELETE FROM UserProfile", connection);
                deleteCommand.ExecuteNonQuery();

                var insertCommand = new SQLiteCommand("INSERT INTO UserProfile (Name) VALUES (@Name)", connection);
                insertCommand.Parameters.AddWithValue("@Name", name);
                insertCommand.ExecuteNonQuery();
            }
        }

        private void EditProfile(object sender, RoutedEventArgs e)
        {
            var profilePanel = new StackPanel
            {
                Margin = new Thickness(20)
            };

            profilePanel.Children.Add(new TextBlock
            {
                Text = "Név:",
                FontSize = 16,
                FontWeight = FontWeights.Bold
            });

            nameTextBox = new TextBox
            {
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 10)
            };
            nameTextBox.GotFocus += (s, _) => {
                nameTextBox.SelectAll();
            };
            profilePanel.Children.Add(nameTextBox);
            LoadName();

            var saveButton = new Button
            {
                Content = "Mentés",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Padding = new Thickness(10, 5, 10, 5)
            };
            saveButton.Click += (s, args) =>
            {
                string name = nameTextBox.Text;
                SaveName(name);
                MessageBox.Show($"Név elmentve: {name}");
            };
            profilePanel.Children.Add(saveButton);

            ContentArea.Children.Clear();
            ContentArea.Children.Add(profilePanel);
        }
    }
}
