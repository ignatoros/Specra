using Specra.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Specra.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        CBClass cB;
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=(local)\SQLEXPRESS;
                                Initial Catalog=PP03Oros;
                                       Integrated Security=True");
        SqlCommand SqlCommand;

        public AddUserWindow()
        {
            InitializeComponent();
            cB = new CBClass();
            Window_Loaded();
        }

        private void Window_Loaded()
        {
            cB.RoleCBLoad(RoleCb);
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuAdminWindow().ShowDialog();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordTb.Text;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "123457890";
            string znak = "!@#$%^&*()_+";

            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Некоректный логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTb.Text))
            {
                MBClass.ErrorMB("Некоректный пароль");
                PasswordTb.Focus();
            }
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать заглавную букву");
                PasswordTb.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать маленькую букву");
                PasswordTb.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать цифру");
                PasswordTb.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать " +
                    "один из этих знаков " + znak);
                PasswordTb.Focus();
            }
            else if (RoleCb.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Не выбрана роль");
                RoleCb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand = new SqlCommand("Insert Into dbo.[Users] " +
                        $"Values ('{LoginTb.Text}'," +
                        $"'{PasswordTb.Text}'," +
                        $"'{RoleCb.SelectedValue.ToString()}')",
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
}