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

namespace School
{
    /// <summary>
    /// Логика взаимодействия для adminCon.xaml
    /// </summary>
    public partial class adminCon : Window
    {
        public adminCon()
        {
            InitializeComponent();
            passwordBox.Focus();
        }

        private void confirmBTN_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "1234")
            {
                MainWindow.isAdmin = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректный пароль!");
            }
        }
    }
}
