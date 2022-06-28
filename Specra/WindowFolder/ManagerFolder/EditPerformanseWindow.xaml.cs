using Specra.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
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

        CBClass cB = new CBClass();
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=DESKTOP-VNUSCBE\KOMMO;
                                Initial Catalog=up02oros;
                                Integrated Security=True");
        SqlCommand SqlCommand;
        SqlDataReader dataReader;
        public EditPerformanseWindow()
        {
            InitializeComponent();
            Window_Loaded();
            cB = new CBClass();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[Users] Set " +
                    $"title='{titleTb.Text}'," +
                    $"[fitst name of the stage director]='{fitstnameofthestagedirectorTb.Text}'," +
                    $"[patronymic of the stage director]='{patronymicofthestagedirectorTb.Text}'," +
                    $"[last name of the production designer]='{lastnameoftheproductiondesignerTb.Text}'," +
                    $"[first name of the production designer]='{firstnameoftheproductiondesignerTb.Text}'," +
                    $"[patronymic of the production designer]='{patronymicoftheproductiondesignerTb.Text}'," +
                    $"[last name of the stage manager]='{lastnameofthestagemanagerTb.Text}'," +
                    $"[first name of the stage manager]='{firstnameofthestagemanagerTb.Text}'," +
                    $"[patronymic of the stage manager]='{patronymicofthestagemanagerTb.Text}'," +
                    $"[last name of the author]='{lastnameoftheauthorTb.Text}'," +
                    $"[first name of the author]='{firstnameoftheauthorTb.Text}'," +
                    $"[patronymic of the author]='{patronymicoftheauthorTb.Text}'," +
                    $"genre='{genreTb.Text}'," +
                    $"type='{typeTb.Text}'," +
                    $"[ticket price]='{ticketpriceTb.Text}'," +
                    $"date='{dateTb.Text}'," +
                    $"[address of the event]='{addressoftheeventTb.Text}'," +
                    $"[number of viewers]='{numberofviewersTb.Text}'," +
                    $"[number of ticket spurchased]='{numberofticketspurchasedTb.Text}'," +
                    $"[number of availableseats]='{numberofavailableseatsTb.Text}'," +                   
                    $"Where IDUser='{VariableClass.UserId}'",
                    sqlConnection);
                SqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Данные пользователя " +
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

        private void Window_Loaded()
        {
            Debug.WriteLine(VariableClass.UserId);
            
            try
            {
                sqlConnection.Open();
                SqlCommand = new SqlCommand("Select * from dbo.[Users] " +
                    $"Where IDUser='{VariableClass.UserId}'",
                    sqlConnection);
                dataReader = SqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                titleTb.Text = dataReader[2].ToString();
                Debug.WriteLine($"Role: {dataReader[3].ToString()}");
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

        private void RoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
