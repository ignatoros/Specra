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

namespace Specra.WindowFolder

   {
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=DESKTOP-VNUSCBE\KOMMO;
                                Initial Catalog=up02oros;
                                       Integrated Security=True");
        SqlCommand sqlCommand = new SqlCommand();
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordPsb.Password;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "123457890";
            string znak = "!@#$%^&*()_+";

            if ( manager.IsChecked == true|| admin.IsChecked== true)
            {
                sqlCommand.CommandText = @"Select Count(*) From dbo.[Users] Where login = " + $"'{LoginTb.Text}'";
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();
                if ((int)sqlCommand.ExecuteScalar()>0)
                {
                    MBClass.ErrorMB("Пользователь уже существует");
                    sqlConnection.Close();
                }

                else if (string.IsNullOrWhiteSpace(LoginTb.Text))
                {
                    MBClass.ErrorMB("Некоректный логин");
                    LoginTb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
                {
                    MBClass.ErrorMB("Некоректный пароль");
                    PasswordPsb.Focus();
                }
                else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
                {
                    MBClass.ErrorMB("Пароль должен содержать заглавную букву");
                    PasswordPsb.Focus();
                }
                else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
                {
                    MBClass.ErrorMB("Пароль должен содержать маленькую букву");
                    PasswordPsb.Focus();
                }
                else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
                {
                    MBClass.ErrorMB("Пароль должен содержать цифру");
                    PasswordPsb.Focus();
                }
                else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
                {
                    MBClass.ErrorMB("Пароль должен содержать " +
                        "один из этих знаков " + znak);
                    PasswordPsb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(PasswordDoublePsb.Password))
                {
                    MBClass.ErrorMB("Некоректный повтор пароля");
                    PasswordDoublePsb.Focus();
                }
                else if (PasswordDoublePsb.Password != PasswordPsb.Password)
                {
                    MBClass.ErrorMB("Пароли не совпадают");
                    PasswordDoublePsb.Focus();
                    PasswordPsb.Focus();
                }
                else
                {
                    try
                    {
                        sqlCommand = new SqlCommand("Insert Into dbo.[Users] " +
                            "(login, password,[IDRole]) Values " +
                            $"('{LoginTb.Text}','{PasswordPsb.Password}',{(manager.IsChecked==true?1:2)})",
                            sqlConnection);
                        sqlCommand.ExecuteNonQuery();
                        MBClass.InfoMB("Пользователь зарегистрирован");
                        Close();
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

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow window = new AuthorizationWindow();
            window.ShowDialog();
        }

        private void ExitBtn_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            //MessageBox.Show(pressed.Content.ToString());
        }
    }
}
