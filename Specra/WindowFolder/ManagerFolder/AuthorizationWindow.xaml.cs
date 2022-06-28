using Specra.ClassFolder;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace Specra.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=DESKTOP-VNUSCBE\KOMMO;
                                Initial Catalog=up02oros;
                                       Integrated Security=True");
        SqlCommand SqlCommand;
        SqlDataReader dataReader;

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void LogInBth_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Некоректный логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MBClass.ErrorMB("Некоректный пароль");
                PasswordPsb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand = new SqlCommand("Select * FROM " +
                        "dbo.[Users] " +
                        $"Where login='{LoginTb.Text}'",
                        sqlConnection);
                    dataReader = SqlCommand.ExecuteReader();
                    if (!dataReader.HasRows) {
                        MBClass.ErrorMB("Пользователь отсутствует!");
                    }
                    else
                    {
                        dataReader.Read();

                        if (dataReader[2].ToString() != PasswordPsb.Password)
                        {
                            MBClass.ErrorMB("Введеный пароль не коректен");
                            PasswordPsb.Focus();
                        }
                        else
                        {
                            switch (dataReader[3].ToString())
                            {

                                case "1":
                                    MBClass.InfoMB("Менеджер\nЗдраствуйте "
                                        + dataReader[1].ToString());
                                    break;
                                case "2":
                                    MBClass.InfoMB("Админ\nЗдраствуйте "
                                        + dataReader[1].ToString());

                                    new AdminFolder.MenuAdminWindow().Show();
                                    Close();

                                    break;
                            }
                        }
                    }
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

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().ShowDialog();
        }

        private void LogOutBth_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}