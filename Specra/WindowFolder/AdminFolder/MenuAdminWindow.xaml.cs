using Specra.ClassFolder;
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
using System.Windows.Shapes;

namespace Specra.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для MenuAdminWindow.xaml
    /// </summary>
    public partial class MenuAdminWindow : Window
    {
        DGClass dGClass;
        public MenuAdminWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(ListUserDG);
            dGClass.LoadDG("Select * From dbo.[Users]");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[Users] " +
                $"Where login Like '%{SearchTb.Text}%' ");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().ShowDialog();
            dGClass.LoadDG("Select * From dbo.[Users]");
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.UserId = dGClass.SelectId();
                    new EditUserWindow().ShowDialog();
                    dGClass.LoadDG("Select * From dbo.[Users]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }
    }
}
