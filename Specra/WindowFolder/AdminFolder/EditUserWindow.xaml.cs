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
            new SqlConnection(@"Data Source=(local)\SQLEXPRESS;
                                Initial Catalog=PP03Oros;
                                       Integrated Security=True");
        SqlCommand SqlCommand;
        SqlDataReader dataReader;
        public EditUserWindow()
        {
            InitializeComponent();
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
                    $"Password='{PasswordTb.Text}'," +
                    $"Login='{LoginTb.Text}'," +
                    $"RoleID='{RoleCb.SelectedValue.ToString()}' " +
                    $"Where UserID='{VariableClass.UserId}'",
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cB.RoleCBLoad(RoleCb);
            
            try
            {
                sqlConnection.Open();
                SqlCommand = new SqlCommand("Select * from dbo.[Users] " +
                    $"Where UserID='{VariableClass.UserId}'",
                    sqlConnection);
                dataReader = SqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                PasswordTb.Text = dataReader[2].ToString();
                RoleCb.SelectedValue = dataReader[3].ToString();
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

 
    }
}
