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

namespace AppGamesTito
{
    /// <summary>
    /// Lógica interna para alterarWindow.xaml
    /// </summary>
    public partial class alterarWindow : Window
    {
        public alterarWindow()
        {
            InitializeComponent();
        }

        private void bntAlterar_Click(object sender, RoutedEventArgs e)
        {
            var alterar = new alterarWindow();
            alterar.Show();
        }
        private void alogin(object sender, RoutedEventArgs e)
        {
            var login = new loginWindow();
            login.Show();
        }



    }
}




