using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
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
using BCrypt.Net;

namespace AppGamesTito
{
    /// <summary>
    /// Lógica interna para cadastroWindow.xaml
    /// </summary>
    public partial class cadastroWindow : Window
    {
        public cadastroWindow()
        {
            InitializeComponent();
        }
        private void bntCadastrar(object sender, RoutedEventArgs e)
        {

            string nickName = txtUsuario.Text.Trim().ToLower(); // ToLower converte tudo para minuscula
            string email = txtEmail.Text.Trim().ToLower();
            string confEmail = txtConfirmarEmail.Text.Trim().ToLower();
            string senha = txtSenha.Password.Trim();
            string confSenha = txtConfirmarSenha.Password.Trim();
            
            string pPasse = "Justin Bieber Lindo";
            DateTime horaC = DateTime.Now;
            
            //Validações de campos preenchidos 

            if (
                string.IsNullOrWhiteSpace(nickName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(confEmail) ||
                string.IsNullOrWhiteSpace(senha) ||
                string.IsNullOrWhiteSpace(confSenha)
                )
            {
                MessageBox.Show("Preencha todos os campos", "Erro!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            //Valida os Emails

            if (email != confEmail)
            {
                MessageBox.Show("Os e-mails não combinam", "Erro!", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus(); // Aqui o foco volta para txtEmail
                txtEmail.SelectAll();
                return;
            }
            
            //Valida as senhas 

            if (senha != confSenha)
            {
                MessageBox.Show("Os senhas não combinam", "Erro!", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtSenha.Focus(); // Aqui o foco volta para txtSenha
                txtSenha.SelectAll();
                return;
            }

            string nickNameMd5 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(nickName))).Replace("-","").ToLower();
            string emailMd5 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(nickName))).Replace("-", "").ToLower();
            string senhaMd5 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(nickName))).Replace("-", "").ToLower();
            string pPasseMd5 = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(nickName))).Replace("-", "").ToLower();
            string part1 = nickNameMd5 + emailMd5;
            string part2 = senhaMd5 + pPasse;


            
            
            string senhaCryp = part1 + part2;
            string hashsCryp = horaC + part2 + part1;

            senha = BCrypt.Net.BCrypt.HashPassword(senhaCryp);
            string hashs = BCrypt.Net.BCrypt.HashPassword(senhaCryp);

            MessageBox.Show("senha criptografada é : " + senha, "MD5");
            MessageBox.Show("senha criptografada é : " + senha, "MD5");


            MessageBox.Show("NickName em MD5 : " + nickNameMd5, "MD5");
            
            
            
            
            
            
            
            
            
            // Conectar com banco de dados
            
             // Comentar todo o bloco pra não gravar dados em excesso
            string conn = "Server=localhost\\SQLEXPRESS;Database=games_tito;Trusted_Connection=True;TrustServerCertificate=True";
           
            try
            {

                using (SqlConnection connetion = new SqlConnection(conn))
                {
                    // Abre conexão com banco de dados 
                    connetion.Open();

                    //Verifica se já existe usuário "nickName" e o email "email"
                    string checkQuery = "SELECT COUNT(*) FROM tb_Usuario WHERE nickname = @nickNmae OR email = @email";
                    using (SqlCommand command = new SqlCommand(checkQuery, connetion))
                    {
                        command.Parameters.AddWithValue("@nickNmae", nickName);
                        command.Parameters.AddWithValue("@email", email);

                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Usuário ou email já está em uso", "Erro!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtUsuario.Focus(); // Aqui o Foco colta para txtUsuário
                            txtUsuario.SelectAll();
                            return;
                        }
                    }

                    // Insere novo usúario no banco de dados 
                    string insertQuery = @"INSERT INTO tb_Usuario

                    (nickName, email, senha, hashs, apikey, idStatus, idAcl, dataCriacao, dataAlteracao)
                    
                    VALUES 
                    
                    (@nickName, @email, @senha, @hashs, @apiKey, @idStatus, @idAcl, @dataCriacao, @dataAlteracao)
                    ";


                    const int idStatus = 2;
                    const int idAcl = 2;
                    //string hashs = "appGames";
                    string apikey = "chaveTito";


                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, connetion))
                    {
                        cmdInsert.Parameters.AddWithValue("@nickName", nickName);
                        cmdInsert.Parameters.AddWithValue("@email", email);
                        cmdInsert.Parameters.AddWithValue("@senha", senha);
                        cmdInsert.Parameters.AddWithValue("@apiKey", apikey);
                        cmdInsert.Parameters.AddWithValue("@idStatus", idStatus);
                        cmdInsert.Parameters.AddWithValue("@idAcl", idAcl);
                        cmdInsert.Parameters.AddWithValue("@hashs", hashs );
                        cmdInsert.Parameters.AddWithValue("@dataCriacao", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmdInsert.Parameters.AddWithValue("@dataAlteracao", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmdInsert.ExecuteNonQuery();
                    }


                }

                MessageBox.Show("Cadastro do Usuário: " + nickName + " realizado com sucesso!", "Cadastro de usuário", MessageBoxButton.OK, MessageBoxImage.Information);
                
                    this.Close();
                    new loginWindow().Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar: " + ex.Message, "Erro!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }



        // MessageBox.Show (nickName + "to aqui ");

        private void aLogin(object sender, RoutedEventArgs e)
        {   //Fecha a janela atual
            this.Close();
            var login = new loginWindow();
            login.Show();


        }
    }
}
