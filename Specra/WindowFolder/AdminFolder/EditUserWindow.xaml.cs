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

namespace Specra.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {

        CBClass cB = new CBClass();
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=DESKTOP-VNUSCBE\KOMMO;
                                Initial Catalog=up02oros;
                                Integrated Security=True");
        SqlCommand SqlCommand;
        SqlDataReader dataReader;
        public EditUserWindow()
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
                    $"password='{PasswordTb.Text}'," +
                    $"login='{LoginTb.Text}'," +
                    $"IDRole='{RoleCb.SelectedValue.ToString()}' " +
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
            cB.RoleCBLoad(RoleCb);
            
            try
            {
                sqlConnection.Open();
                SqlCommand = new SqlCommand("Select * from dbo.[Users] " +
                    $"Where IDUser='{VariableClass.UserId}'",
                    sqlConnection);
                dataReader = SqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                PasswordTb.Text = dataReader[2].ToString();
                RoleCb.SelectedValue = dataReader[3].ToString();
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
            new MenuAdminWindow().ShowDialog();
        }

        private void RoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
