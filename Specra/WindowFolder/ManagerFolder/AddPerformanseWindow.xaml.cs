using Specra.ClassFolder;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace Specra.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddPerformanseWindow : Window
    {
        CBClass cB;
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=DESKTOP-VNUSCBE\KOMMO;
                                Initial Catalog=up02oros;
                                Integrated Security=True");
        SqlCommand SqlCommand;

        public AddPerformanseWindow()
        {
            InitializeComponent();
            cB = new CBClass();
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
                SqlCommand = new SqlCommand("Insert Into dbo.[Performanse] " +
                    $"Values ('{titleTb.Text}'," +
                    $"'{lastnameofthestagedirectorTb.Text}'," +
                    $"'{fitstnameofthestagedirectorTb.Text}'," +
                    $"'{patronymicofthestagedirectorTb.Text}'," +
                    $"'{lastnameoftheproductiondesignerTb.Text}'," +
                    $"'{firstnameoftheproductiondesignerTb.Text}'," +
                    $"'{patronymicoftheproductiondesignerTb.Text}'," +
                    $"'{lastnameofthestagemanagerTb.Text}'," +
                    $"'{firstnameofthestagemanagerTb.Text}'," +
                    $"'{patronymicofthestagemanagerTb.Text}'," +
                    $"'{lastnameoftheauthorTb.Text}'," +
                    $"'{firstnameoftheauthorTb.Text}'," +
                    $"'{patronymicoftheauthorTb.Text}'," +
                    $"'{genreTb.Text}'," +
                    $"'{typeTb.Text}'," +
                    $"'{ticketpriceTb.Text}'," +
                    $"'{dateTb.Text}'," +
                    $"'{addressoftheeventTb.Text}'," +
                    $"'{numberofviewersTb.Text}'," +
                    $"'{numberofticketspurchasedTb.Text}'," +
                    $"'{numberofavailableseatsTb.Text}')",
                    sqlConnection);
                SqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Пользователь  успешно добавлен");
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