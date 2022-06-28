using Specra.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Specra.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для EditPerformanseWindow.xaml
    /// </summary>
    public partial class EditPerformanseWindow : Window
    {

        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=(local)\SQLEXPRESS;
                                Initial Catalog=PP03Oros;
                                       Integrated Security=True");
        SqlCommand SqlCommand;
        SqlDataReader dataReader;
        public EditPerformanseWindow()
        {
            InitializeComponent();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[Performanse] Set " +
                    $"title='{title.Text}', " +
                    $"LastName='{LastName.Text}', " +
                    $"FitstName='{FitstName.Text}', " +
                    $"Patronymic='{Patronymic.Text}', " +
                    $"LastNameProduction='{LastNameProduction.Text}', " +
                    $"FirstNameProduction='{FirstNameProduction.Text}', " +
                    $"Patronymicproduction='{Patronymicproduction.Text}', " +
                    $"LastNameStage='{LastNameStage.Text}', " +
                    $"FirstNameStage='{FirstNameStage.Text}', " +
                    $"PatronymicStage='{PatronymicStage.Text}', " +
                    $"LastNameAuthor='{LastNameAuthor.Text}', " +
                    $"FirstNameAuthor='{FirstNameAuthor.Text}', " +
                    $"PatronymicAuthor='{PatronymicAuthor.Text}', " +
                    $"genre='{genre.Text}', " +
                    $"type='{type.Text}', " +
                    $"TicketPrice='{TicketPrice.Text}', " +
                    $"date='{date.Text}', " +
                    $"AddressEvent='{AddressEvent.Text}', " +
                    $"NumberViewers='{NumberViewers.Text}', " +
                    $"NumberPurchased='{NumberPurchased.Text}', " +
                    $"NumberSeats='{NumberSeats.Text}' " +
                    $"Where id='{VariableClass.PerfomanceId}'",
                    sqlConnection);
                SqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Данные спектакля " +
                    $"успешно отредактированы");
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


        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuManagerWindow().ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand = new SqlCommand("Select * from dbo.[Performanse] " +
                    $"Where id='{VariableClass.PerfomanceId}'",
                    sqlConnection);
                dataReader = SqlCommand.ExecuteReader();
                dataReader.Read();
                title.Text = dataReader[1].ToString();
                LastName.Text = dataReader[2].ToString();
                FitstName.Text = dataReader[3].ToString();
                Patronymic.Text = dataReader[4].ToString();
                LastNameProduction.Text = dataReader[5].ToString();
                FirstNameProduction.Text = dataReader[6].ToString();
                Patronymicproduction.Text = dataReader[7].ToString();
                LastNameStage.Text = dataReader[8].ToString();
                FirstNameStage.Text = dataReader[9].ToString();
                PatronymicStage.Text = dataReader[10].ToString();
                LastNameAuthor.Text = dataReader[11].ToString();
                FirstNameAuthor.Text = dataReader[12].ToString();
                PatronymicAuthor.Text = dataReader[13].ToString();
                genre.Text = dataReader[14].ToString();
                type.Text = dataReader[15].ToString();
                TicketPrice.Text = dataReader[16].ToString();
                date.Text = dataReader[17].ToString();
                AddressEvent.Text = dataReader[18].ToString();
                NumberViewers.Text = dataReader[19].ToString();
                NumberPurchased.Text = dataReader[20].ToString();
                NumberSeats.Text = dataReader[21].ToString();
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
