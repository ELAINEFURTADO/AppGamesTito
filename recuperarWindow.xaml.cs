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
    /// Lógica interna para recuperarWindow.xaml
    /// </summary>
    public partial class recuperarWindow : Window
    {
        public recuperarWindow()
        {
            InitializeComponent();
        }
       
        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            {
                this.Close();
                var recuperar = new recuperarWindow();
                recuperar.Show();
            }
        }
        private void BtnEntrar(object sender, RoutedEventArgs e)
        {
            var login = new loginWindow();
            login.Show();
        }
    }
}