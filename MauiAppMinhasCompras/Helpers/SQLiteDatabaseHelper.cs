using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        //A classe SQLiteAsyncConnection é usada para gerenciar operações com o banco de dados de maneira assíncrona
        //o atributo _conn utilizado para gerenciar as operações no banco
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            //o atributo _conn utilizado para gerenciar as operações no banco
            _conn = new SQLiteAsyncConnection(path);
            //O método _conn.CreateTableAsync<Produto>().Wait(); cria, de forma assíncrona, uma tabela chamada Produto, com base na classe modelo Produto
            _conn.CreateTableAsync<Produto>().Wait();
        }
                
        public Task<int> Insert(Produto p) //Gerencia operações com o banco de dados SQLite de forma assíncrona, este método insere um registro do tipoProduto no banco de dados SQLite.
        {
            return _conn.InsertAsync(p);
        }

        /*O método Update tenta atualizar um registro existente na tabela Produto no banco de dados, com base no ID
         fornecido no objeto Produto(p). A consulta SQL atualiza as colunas Descricao, Quantidade e Preco do registro
        que corresponde ao Id especificado.*/
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(
            sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        /*O método Delete remove um registro da tabela Produto com base no Id fornecido como argumento. Ele utiliza
         a funcionalidade assíncrona do SQLite para realizar a exclusão sem bloquear a thread principal.*/
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        /*O método GetAll é utilizado para consulta a tabela Produto no banco de dados e retorna todos os registros na
         forma de uma lista de objetos Produto. A operação é assíncrona, permitindo que o método seja executado sem
        bloquear a thread principal. Semelhante ao select * from (tabela)*/
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        /*O método Search implementa uma funcionalidade de busca na tabela Produto do banco de dados SQLite,
         permitindo filtrar registros de acordo com uma string fornecida.*/
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * Produto WHERE descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }

    }
}
