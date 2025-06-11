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
    /// Lógica interna para loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        public loginWindow()
        {
            InitializeComponent();
        }

        private void BtnEntrar(object sender, RoutedEventArgs e)
        {
            var login = new loginWindow();
            login.Show();
        }
        private void aEsqueciSenha(object sender, RoutedEventArgs e)
        {
            this.Close();
            var esqueciSenha = new recuperarWindow();
            esqueciSenha.Show();
        }

        private void aCastrarUsuario(object sender, RoutedEventArgs e)
        {
            this.Close();
            var cadastro = new cadastroWindow();
            cadastro.Show();
        }


    }
}
