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

namespace Specra.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для MenuManagerWindow.xaml
    /// </summary>
    public partial class MenuManagerWindow : Window
    {
        DGClass dGClass;
        public MenuManagerWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(ListUserDG);
            dGClass.LoadDG("Select * From dbo.[Performanse]");
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[Performanse] " +
                $"Where login Like '%{SearchTb.Text}%' ");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            new AddPerformanseWindow().ShowDialog();
            dGClass.LoadDG("Select * From dbo.[Performanse]");
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
                    new EditPerformanseWindow().ShowDialog();
                    dGClass.LoadDG("Select * From dbo.[Performanse]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }
    }
}
