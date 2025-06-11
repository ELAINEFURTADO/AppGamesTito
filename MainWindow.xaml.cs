using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace AppGamesTito
{

    public partial class MainWindow : Window
    {
        private void CarregarUsuarios()
        {
            // Conectando com o banco de dados 
            //string conn = "Server=localhost;Database=games_tito;trusted_Connection=True;";
            string conn = "Server=localhost\\SQLEXPRESS;Database=games_tito;Trusted_Connection=True;TrustServerCertificate=True";
            string query = "SELECT * FROM tb_Usuario";

            List<Usuario> usuarios = new List<Usuario>();

            // Usa um bloco 'using' para garantir que a conexão será fechada automaricamente
            using (SqlConnection connection = new SqlConnection(conn)) {
                // Abre a conexão com o banco de dados
                connection.Open();
                // Cria um comando SQL associado é conexão aberta
                SqlCommand command = new SqlCommand(query, connection);
                // Execulta o comando e obtém um leitor para percorrer os resultados
                SqlDataReader reader = command.ExecuteReader();
                // Percore todos os registros e obtém um leitor de resultados e imprime no console
                while (reader.Read())
                {
                    //Crio um vetor para armazenar o idUsuario e o nickname
                    usuarios.Add(new Usuario
                    {
                        idUsuario = (int)reader["idUsuario"],
                        nickName = reader["nickName"].ToString(),
                        email = reader["email"].ToString()
                    });
                }
                //Fechar o leitor de dados 
                reader.Close();

                lstUsuarios.ItemsSource = usuarios;
               
            }
        }

        public class Usuario
        {
            public int idUsuario { get; set; }
            public string nickName { get; set; }
            public string email { get; set; }
        }

public MainWindow()
        {
            InitializeComponent();
            CarregarUsuarios();

            // Evento que é disparado quando o controle de texto "BuscaTxt" recebe o foco (O usuário clica ou
            // interage com o campo )
            BuscaTxt.GotFocus += (s, e) =>
            {// Verifica se o texto do campo ainda é o texto padrão "Pesquisar..."
                if (BuscaTxt.Text == " Pesquisar...")
                {
                    //Limpa o texto do campo de busca quando o usuário clica ou interage com ele 
                    BuscaTxt.Text = "";
                    // Altera a cor do texto para "preto quando o usuário começa a digitar "
                    BuscaTxt.Foreground = Brushes.Black;
                }
                // Torna "PesuisaLbl" invisivel (Oculta o texto sugerido "Pesquisar...")
                Pesquisalbl.Visibility = Visibility.Collapsed;
            };
            // Evento disparado quando o controle de texto "BuscaTxt" perde o foco (o USUÁRIO CLICA FORA OU MUDA O FOCO  )
            BuscaTxt.LostFocus += (s, e) =>
            {
                //VERIFICA SE O CAMPO DE BUSCA ESTÁ VAZIO OU CONTÉM APENAS ESPÇOS EM BRANCO 
                if (string.IsNullOrWhiteSpace(BuscaTxt.Text))
                {
                    // SE O CAMPO ESTIVER VAZIO, TORNA O "PesquisaLbl" Visível novamente
                    Pesquisalbl.Visibility = Visibility.Visible;
                }

            };
            // Evento disparado quando o texto no controle de busca é alterado (o usuário digita algo no campo)
            // Seestiver vazio, o "PesquisarLbl" é visível, caso contrário é oculto.
            BuscaTxt.TextChanged += (s, e) =>
            {
                Pesquisalbl.Visibility = string.IsNullOrWhiteSpace(BuscaTxt.Text)
                ? Visibility.Visible : Visibility.Collapsed;
            };
        }
        private void BuscaTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void bntLogin(object sender, RoutedEventArgs e)
        {
            var login = new loginWindow();
            login.Show();
        }
    }
}
