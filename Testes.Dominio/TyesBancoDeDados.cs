using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Testes.Dominio.ModuloDisciplina;

namespace Testes.Dominio
{
    internal class TyesBancoDeDados
    {
        static void Main(string[] args)
        {
            List<Disciplina> disciplinas = SelecionarTodasDisciplinas();

            foreach (var item in disciplinas)
            {
                Console.WriteLine(item);
            }
            int numero = disciplinas[0].Numero;

            var disciplina = ObterTyesDisciplina("Ingles");

            Disciplina disciplinaEncontrada = SelecionarDisciplinaPorNumero(numero);

            InserirDisciplina(disciplina);
            disciplina.Nome = "Biologia";

            EditarDisciplina(disciplina);

            ExcluirDisciplina(disciplina.Numero);

        }

        private static List<Disciplina> SelecionarTodasDisciplinas()
        {

            #region ABRIR A CONEXÃO COM O BANCO DE DADOS

            string enderecoBanco =
                "Data Source=(LocalDB)\\MSSqlLocalDB;" +
                  "Initial Catalog=testesDaDonaMarianaDB;" +
                  "Integrated Security=True;" +
                  "Pooling=False";

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoBanco;
            conexaoComBanco.Open();


            #endregion
            #region CRIAR UM COMANDO DE INSERÇÃO

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;
            string sql = @"SELECT 
                                    [NUMERO]
                                    [NOME]
                           FROM      
                                    [TBDisciplina]";

            comandoSelecao.CommandText = sql;

            #endregion

            //executar o comando 

            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            List<Disciplina> disciplinas = new List<Disciplina>();

            while (leitorDisciplina.Read())
            {
                int numero = Convert.ToInt32(leitorDisciplina["NUMERO"]);
                string nome = Convert.ToString(leitorDisciplina["NOME"]);

                var discpilna = new Disciplina
                {
                    Numero = numero,
                    Nome = nome
                };

                disciplinas.Add(discpilna);
            }
            //fechar a conexão
            conexaoComBanco.Close();

            return disciplinas;
        }
        private static void InserirDisciplina(Disciplina novaDisciplina)
        {
            #region ABRIR A CONEXÃO COM O BANCO DE DADOS

            string enderecoBanco =
                "Data Source=(LocalDB)\\MSSqlLocalDB;" +
                  "Initial Catalog=testesDaDonaMarianaDB;" +
                  "Integrated Security=True;" +
                  "Pooling=False";

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoBanco;
            conexaoComBanco.Open();


            #endregion
            #region CRIAR UM COMANDO DE INSERÇÃO

            SqlCommand comandoInsercao = new SqlCommand();
            comandoInsercao.Connection = conexaoComBanco;
            string sql = @"INSERT INTO [TBDisciplina]
            (
	            [NOME]
            )
            VALUES
            (
	            @[NOME]
            )";

            sql += "SELECT SCOPE_IDENTITY();";
            comandoInsercao.CommandText = sql;

            #endregion
            #region PARAMETROS PARA COMANDO DE INSERÇÃO

            comandoInsercao.Parameters.AddWithValue("[NOME]", novaDisciplina.Nome);

            #endregion

            //executar o comando 
            var id = comandoInsercao.ExecuteScalar();
            novaDisciplina.Numero = Convert.ToInt32(id);
            //fechar a conexão
            conexaoComBanco.Close();
        }
        private static void EditarDisciplina(Disciplina disciplina)
        {

            #region abrir a conexão com o banco de dados

            string enderecoBanco =
                "Data Source=(LocalDB)\\MSSqlLocalDB;" +
                  "Initial Catalog=testesDaDonaMarianaDB;" +
                  "Integrated Security=True;" +
                  "Pooling=False";

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoBanco;
            conexaoComBanco.Open();
            #endregion

            #region  criar um comando de edição
            SqlCommand comandoEdicao = new SqlCommand();
            comandoEdicao.Connection = conexaoComBanco;
            string sql = @"	UPDATE [TBDisciplina]	
		                        SET
			                        [NOME] = @NOME,
		                        WHERE
			                        [NUMERO] = @NUMERO";

            comandoEdicao.CommandText = sql;

            #endregion

            #region passar os parâmetros para o comando de inserção
            comandoEdicao.Parameters.AddWithValue("NOME", disciplina.Nome);
            #endregion

            //executar o comando
            comandoEdicao.ExecuteNonQuery();

            //fechar a conexão
            conexaoComBanco.Close();
        }
        private static void ExcluirDisciplina(int numero)
        {
            #region abrir a conexão com o banco de dados
            string enderecoBanco =
                "Data Source=(LocalDB)\\MSSqlLocalDB;" +
                  "Initial Catalog=testesDaDonaMarianaDB;" +
                  "Integrated Security=True;" +
                  "Pooling=False";

            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoBanco;
            conexaoComBanco.Open();
            #endregion

            #region  criar um comando de edição
            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;
            string sql = @"	DELETE FROM [TBDisciplina]
		                        WHERE
			                        [NUMERO] = @NUMERO";

            comandoExclusao.CommandText = sql;

            #endregion

            #region passar os parâmetros para o comando de inserção
            comandoExclusao.Parameters.AddWithValue("NUMERO", numero);
            #endregion

            //executar o comando
            comandoExclusao.ExecuteNonQuery();

            //fechar a conexão
            conexaoComBanco.Close();
        }
        private static Disciplina ObterTyesDisciplina(string nome)
        {
            return new Disciplina
            {
                Nome = nome
            };
        }
        private static Disciplina SelecionarDisciplinaPorNumero(int numeroPesquisado)
        {
            #region abrir a conexão com o banco de dados
            string enderecoBanco =
                "Data Source=(LocalDB)\\MSSqlLocalDB;" +
                  "Initial Catalog=testesDaDonaMarianaDB;" +
                  "Integrated Security=True;" +
                  "Pooling=False";


            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoBanco;
            conexaoComBanco.Open();
            #endregion

            #region  criar um comando 
            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;
            string sql = @"SELECT 
		                        [NUMERO], 
		                        [NOME]
	                        FROM 
		                        [TBDisciplina]
		                    WHERE
                                [NUMERO] = @NUMERO";

            comandoSelecao.CommandText = sql;

            #endregion

            comandoSelecao.Parameters.AddWithValue("NUMERO", numeroPesquisado);

            //executar o comando
            SqlDataReader leitorContato = comandoSelecao.ExecuteReader();

            Disciplina disciplina = null;
            if (leitorContato.Read())
            {
                int numero = Convert.ToInt32(leitorContato["NUMERO"]);
                string nome = Convert.ToString(leitorContato["NOME"]);

                disciplina = new Disciplina
                {
                    Numero = numero,
                    Nome = nome
                };
            }

            //fechar a conexão
            conexaoComBanco.Close();

            return disciplina;
        }
    }
}
