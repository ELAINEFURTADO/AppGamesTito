using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using static AppGamesTito.MainWindow;

namespace AppGamesTito
{
    /// <summary>
    /// Lógica interna para alterarWindow.xaml
    /// </summary>
    public partial class alterarWindow : Window
    {
        private string hashs; // variável declarada dentro da classe
        public alterarWindow(string hashs)
        {
            InitializeComponent();
            this.hashs = hashs; // atribui o valor passado para o campo privado
            MessageBox.Show("A hash passada que chegou aqui foi : " + hashs, "Hash");
        }

        private void bntAlterar_Click(object sender, RoutedEventArgs e)

        {
            string nickName = txtUsuario.Text.Trim().ToLower();


            string conn = "Server=localhost\\SQLEXPRESS;Database=games_tito;Trusted_Connection=True;TrustServerCertificate=True";
            string query = "SELECT * FROM tb_Usuario WHERE nickName = @nickName OR email = @nickName";

            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nickName", nickName);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        usuarios.Add(new Usuario
                        {
                            idUsuario = (int)reader["idUsuario"],
                            hashs = reader["hashs"].ToString(),
                        });
                    }
                    reader.Close();
                }
            }
            if (usuarios.Count <= 0)
            {
                MessageBox.Show("Usuário ou senha invalida!", "Erro!");
                return;
            }

            int idUsuario = usuarios[0].idUsuario;
            string hashsDB = usuarios[0].hashs;

            this.Close();

            // MessageBox.Show("a hash recuperada é? " + hashsDB, " Hash")



        }
        private void alogin(object sender, RoutedEventArgs e)
        {
            var login = new loginWindow();
            login.Show();
        }



    }
}




