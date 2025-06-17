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
using System.Data.SqlClient;
using static AppGamesTito.MainWindow;
using System.Security.Cryptography;

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
            string nickName = txtUsuario.Text.Trim().ToLower();
            string senha = txtSenha.Password.Trim();
            string pPasse = "\r\n$2a$11ShXw3NWWr8vihx6tCbHRyV.mDWgnUdYnKrujm2EjIKYWimkjLrEn\r\n00";
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
                            idStatus = (int)reader["idStatus"],
                            idAcl = (int)reader["idAcl"],
                            nickName = reader["nickName"].ToString(),
                            email = reader["email"].ToString(),
                            senha = reader["senha"].ToString(),
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
            string nickNameDB = usuarios[0].nickName;
            string emailDB = usuarios[0].email;
            string senhaDB = usuarios[0].senha;
            int idUsuario = usuarios[0].idUsuario;
            int idStatus = usuarios[0].idStatus;
            int idAcl = usuarios[0].idAcl;
            // inicio da Cripyt
            string nickNameMd5 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(nickNameDB))).Replace("-", "").ToLower();
            string emailMd5 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(emailDB))).Replace("-", "").ToLower();
            string senhaMd5 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(senha))).Replace("-", "").ToLower();
            string pPasseMd5 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(pPasse))).Replace("-", "").ToLower();

            string part1 = nickNameMd5 + emailMd5;
            string part2 = senhaMd5 + pPasseMd5;

            string senhaCryp = part1 + part2;

            bool vSenha = BCrypt.Net.BCrypt.Verify(senhaCryp, senhaDB);

            if (!(vSenha))
            {
                MessageBox.Show("Usuário ou senha invalida! - Senha", "Erro - Senha");
                return;
            }
            MessageBox.Show("Usuário ok!", "Usuário Valido!");
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
