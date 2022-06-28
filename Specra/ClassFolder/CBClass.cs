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
           new SqlConnection(@"Data Source=DESKTOP-VNUSCBE\KOMMO;
                                Initial Catalog=up02oros;
                                Integrated Security=True");
        SqlDataAdapter sqlData;
        DataSet dataSet;

        public void RoleCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select * " +
                    "From dbo.[Role1]",
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "Role1");
                comboBox.ItemsSource = dataSet.Tables["Role1"].DefaultView;
                comboBox.DisplayMemberPath = dataSet.
                    Tables["Role1"].Columns["RoleName"].ToString();
                comboBox.SelectedValuePath = dataSet.
                   Tables["Role1"].Columns["RoleID"].ToString();
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
