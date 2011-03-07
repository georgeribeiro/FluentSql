using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentQuery.Command;

namespace FluentQuery.Test
{
    [TestFixture]
    class ComplexQuery
    {
        [Test]
        public void Usar_Uma_Table_No_Select()
        {
            var alunos = new Table("alunos");
            alunos.Project(alunos["nome"], alunos["matricula"]);
            Assert.AreEqual("SELECT alunos.nome, alunos.matricula FROM alunos", alunos.ToSql());
            Assert.AreEqual("SELECT * FROM Users", new Table("Users").ToSql());
            var users = new Table("Users");
            users.Project(users.All);
            Assert.AreEqual("SELECT Users.* FROM Users", users.ToSql()); 
        }

        [Test]
        public void Usar_Mais_De_Uma_Table_No_Select_E_Join()
        {
            var alunos = new Table("alunos");
            var cursos = new Table("cursos");
            alunos.Project(alunos.All, cursos["nome"])
                .Join(cursos)
                .On(alunos["curso_id"] == cursos["curso_id"]);
            string sql = "SELECT alunos.*, cursos.nome FROM alunos JOIN cursos ON alunos.curso_id = cursos.curso_id";
            Assert.AreEqual(sql, alunos.ToSql());
        }

        [Test]
        public void Usar_Field_Com_Alias()
        {
            var users = new Table("users");
            users.Project(users["nome"].As("nome_do_usuario"), users["senha"]);
            Assert.AreEqual("SELECT users.nome AS nome_do_usuario, users.senha FROM users", users.ToSql());
            var groups = new Table("groups");
            users.Join(groups).On(users["nome"] == groups["nome_user"]);
            Assert.AreEqual("SELECT users.nome AS nome_do_usuario, users.senha FROM users JOIN groups ON nome_do_usuario = groups.nome_user", users.ToSql());
        }

        [Test]
        public void Usar_Table_Com_Alias()
        {
            var users = new Table("users", "u");
            users.Project(users["nome"], users["senha"]);
            Assert.AreEqual("SELECT u.nome, u.senha FROM users AS u", users.ToSql());
        }

        [Test]
        public void Usar_Where_Equal_Na_Consulta()
        {
            var users = new Table("users");
            users.Project(users.All).Where((users["nome"] == "george"));
            Assert.AreEqual("SELECT users.* FROM users WHERE users.nome = @users_nome_1", users.ToSql());  
        }

        [Test]
        public void Usar_Where_E_Or_Na_Consulta()
        {
            var users = new Table("users");
            users.Project(users.All).Where((users["nome"] == "george") | (users["id"] != 10));
            Assert.AreEqual("SELECT users.* FROM users WHERE (users.nome = @users_nome_1) OR (users.id <> @users_id_1)", users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Complexa_E_Usar_Clear_Para_Zerar()
        {
            var users = new Table("users");
            var groups = new Table("groups");
            users.Project(users.All, groups["nome"])
                .Join(groups).On(users["group_id"] == groups["id"]);
            Assert.AreEqual("SELECT users.*, groups.nome FROM users JOIN groups ON users.group_id = groups.id", users.ToSql());
            users.Clear();
            Assert.AreEqual("SELECT * FROM users", users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Left_Join()
        {
            var users = new Table("users", "u");
            var groups = new Table("groups", "g");
            users.Project(users.All, groups.All)
                .LeftJoin(groups)
                .On(groups["id"] == users["group_id"])
                .Where(users["id"] == 1);
            string sql_expected = "SELECT u.*, g.* FROM users AS u LEFT JOIN groups AS g ON g.id = u.group_id WHERE u.id = @users_id_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Inner_Join()
        {
            var users = new Table("users", "u");
            var groups = new Table("groups", "g");
            users.Project(users.All, groups.All)
                .InnerJoin(groups)
                .On(groups["id"] == users["group_id"])
                .Where(users["id"] == 1 & users["nome"] != "george");
            string sql_expected = "SELECT u.*, g.* FROM users AS u INNER JOIN groups AS g ON g.id = u.group_id WHERE (u.id = @users_id_1) AND (u.nome <> @users_nome_1)";
            Assert.AreEqual(sql_expected, users.ToSql());
        }
        
        [Test]
        public void Fazer_Consulta_Com_Maior_Que_E_Menor_Que()
        {
            var users = new Table("users");
            users.Project(users["nome"]).Where(users["idade"] > 10)
                .Where(users["idade"] < 20);
            string sql_expected = "SELECT users.nome FROM users WHERE users.idade > @users_idade_1 AND users.idade < @users_idade_2";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Maior_Igual_E_Menor_Igual()
        {
            var users = new Table("users", "u");
            users.Project(users.All).Where((users["idade"] >= 10) & (users["idade"] <= 20));
            string sql_expected = "SELECT u.* FROM users AS u WHERE (u.idade >= @users_idade_1) AND (u.idade <= @users_idade_2)";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Like()
        {
            var users = new Table("users");
            users.Project(users["nome"], users["senha"])
                .Where(users["nome"].Like("%n"));
            string sql_expected = "SELECT users.nome, users.senha FROM users WHERE users.nome LIKE @users_nome_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Not()
        {
            var users = new Table("users");
            users.Where(users["nome"].Not.Like("%n"));
            string sql_expected = "SELECT * FROM users WHERE NOT users.nome LIKE @users_nome_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_In()
        {
            var users = new Table("users", "u");
            users.Where(users["nome"].In(new string[] { "george", "ribeiro" }));
            string sql_expected = "SELECT * FROM users AS u WHERE u.nome IN ('george', 'ribeiro')";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_E_Ver_Parametros()
        {
            var users = new Table("users", "u");
            var groups = new Table("groups", "g");
            users.Project(users.All, groups["nome"])
                .LeftJoin(groups).On(groups["id"] == users["groups_id"])
                .Where(users["id"] > 10)
                .Where(groups["id"] > 20);
            string sql_expected = "SELECT u.*, g.nome FROM users AS u LEFT JOIN groups AS g ON g.id = u.groups_id WHERE u.id > @users_id_1 AND g.id > @groups_id_1";
            Assert.AreEqual(sql_expected, users.ToSql());
            Assert.AreEqual(users.Params["users_id_1"], 10);
            Assert.AreEqual(groups.Params["groups_id_1"], 20);
        }

        [Test]
        public void Usar_GroupBy_Na_Query()
        {
            var users = new Table("users");
            users.Project(users.All)
                .Where(users["nome"] == "george")
                .GroupBy(users["data_cadastro"]);
            string sql_expected = "SELECT users.* FROM users WHERE users.nome = @users_nome_1 GROUP BY users.data_cadastro";
            Assert.AreEqual(sql_expected, users.ToSql());
            Assert.AreEqual("george", users.Params["users_nome_1"]);
            users.Clear();
            Assert.AreEqual("SELECT * FROM users", users.ToSql());
        }

        [Test]
        public void Usar_Mais_De_Um_Join_Na_Query()
        {
            var clientes = new Table("clientes", "c");
            var vendas = new Table("vendas", "v");
            var produtos = new Table("produtos", "p");
            vendas.Project(vendas["data"], produtos["descricao"], clientes["nome"])
                .LeftJoin(clientes).On(clientes["id"] == vendas["cliente_id"])
                .LeftJoin(produtos).On(produtos["id"] == vendas["produto_id"])
                .Where(vendas["data"] > new DateTime(2011, 1, 1))
                .Where(produtos["preco"] != null | produtos["custo"] != null)
                .GroupBy(vendas["data"]);
            string sql_expected = "SELECT v.data, p.descricao, c.nome FROM vendas AS v LEFT JOIN clientes AS c ON c.id = v.cliente_id "
                + "LEFT JOIN produtos AS p ON p.id = v.produto_id WHERE v.data > @vendas_data_1 AND (p.preco IS NOT NULL) OR "
                + "(p.custo IS NOT NULL) GROUP BY v.data";
            Assert.AreEqual(sql_expected, vendas.ToSql());
        }
    }
}
