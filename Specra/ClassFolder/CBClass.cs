using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Specra.ClassFolder
{
    
    class CBClass
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=(local)\SQLEXPRESS;
                                Initial Catalog=PP03Oros;
                                       Integrated Security=True");
        SqlDataAdapter sqlData;
        DataSet dataSet;

        public void RoleCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select * " +
                    "From dbo.[Role]",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "Role");
                comboBox.ItemsSource = dataSet.Tables["Role"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["Role"].Columns["RoleName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["Role"].Columns["RoleID"].ToString();
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
