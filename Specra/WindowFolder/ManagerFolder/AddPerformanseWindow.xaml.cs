using Specra.ClassFolder;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Specra.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddPerformanseWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=(local)\SQLEXPRESS;
                                Initial Catalog=PP03Oros;
                                       Integrated Security=True");
        SqlCommand sqlCommand;
        public AddPerformanseWindow()
        {
            InitializeComponent();
        }


        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuManagerWindow().ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Insert Into dbo.[Performanse] " +
                    "(title, LastName, FitstName, Patronymic, LastNameProduction, FirstNameProduction, " +
                    "Patronymicproduction, LastNameStage, " +
                    "FirstNameStage, PatronymicStage, LastNameAuthor," +
                    " FirstNameAuthor, PatronymicAuthor, genre, type, " +
                    "TicketPrice, date, AddressEvent, NumberViewers, NumberPurchased, NumberSeats) " +
                    $"Values ('{title.Text}'," +
                    $"'{LastName.Text}'," +
                    $"'{FitstName.Text}'," +
                    $"'{Patronymic.Text}'," +
                    $"'{LastNameProduction.Text}'," +
                    $"'{FirstNameProduction.Text}'," +
                    $"'{Patronymicproduction.Text}'," +
                    $"'{LastNameStage.Text}'," +
                    $"'{FirstNameStage.Text}'," +
                    $"'{PatronymicStage.Text}'," +
                    $"'{LastNameAuthor.Text}'," +
                    $"'{FirstNameAuthor.Text}'," +
                    $"'{PatronymicAuthor.Text}'," +
                    $"'{genre.Text}'," +
                    $"'{type.Text}'," +
                    $"'{TicketPrice.Text}'," +
                    $"'{date.Text}'," +
                    $"'{AddressEvent.Text}'," +
                    $"'{NumberViewers.Text}'," +
                    $"'{NumberPurchased.Text}'," +
                    $"'{NumberSeats.Text}')",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Представление {title.Text} " + " успешно добавлено");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }

        }
    }
}