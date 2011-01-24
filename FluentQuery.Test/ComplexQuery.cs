using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

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
            alunos.Project(alunos.All, cursos["nome"]).Join(cursos, alunos["curso_id"] == cursos["curso_id"]);
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
            users.Join(groups, users["nome"] == groups["nome_user"]);
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
            Assert.AreEqual("SELECT users.* FROM users WHERE users.nome = 'george'", users.ToSql());  
        }

        [Test]
        public void Usar_Where_E_Or_Na_Consulta()
        {
            var users = new Table("users");
            users.Project(users.All).Where((users["nome"] == "george") | (users["id"] != 10));
            Assert.AreEqual("SELECT users.* FROM users WHERE (users.nome = 'george') OR (users.id <> 10)", users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Complexa_E_Usar_Clear_Para_Zerar()
        {
            var users = new Table("users");
            var groups = new Table("groups");
            users.Project(users.All, groups["nome"]).Join(groups, users["group_id"] == groups["id"]);
            Assert.AreEqual("SELECT users.*, groups.nome FROM users JOIN groups ON users.group_id = groups.id", users.ToSql());
            users.Clear();
            Assert.AreEqual("SELECT * FROM users", users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Left_Join()
        {
            var users = new Table("users", "u");
            var groups = new Table("groups", "g");
            users.Project(users.All, groups.All).LeftJoin(groups, groups["id"] == users["group_id"]).Where(users["id"] == 1);
            string sql_expected = "SELECT u.*, g.* FROM users AS u LEFT JOIN groups AS g ON g.id = u.group_id WHERE u.id = 1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Inner_Join()
        {
            var users = new Table("users", "u");
            var groups = new Table("groups", "g");
            users.Project(users.All, groups.All).InnerJoin(groups, groups["id"] == users["group_id"]).Where(users["id"] == 1 & users["nome"] != "george");
            string sql_expected = "SELECT u.*, g.* FROM users AS u INNER JOIN groups AS g ON g.id = u.group_id WHERE (u.id = 1) AND (u.nome <> 'george')";
            Assert.AreEqual(sql_expected, users.ToSql());
        }
        
        [Test]
        public void Fazer_Consulta_Com_Maior_Que_E_Menor_Que()
        {
            var users = new Table("users");
            users.Project(users["nome"]).Where(users["idade"] > 10).Where(users["idade"] < 20);
            string sql_expected = "SELECT users.nome FROM users WHERE users.idade > 10 AND users.idade < 20";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Maior_Igual_E_Menor_Igual()
        {
            var users = new Table("users", "u");
            users.Project(users.All).Where((users["idade"] >= 10) & (users["idade"] <= 20));
            string sql_expected = "SELECT u.* FROM users AS u WHERE (u.idade >= 10) AND (u.idade <= 20)";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Like()
        {
            var users = new Table("users");
            users.Project(users["nome"], users["senha"]).Where(users["nome"].Like("%n"));
            string sql_expected = "SELECT users.nome, users.senha FROM users WHERE users.nome LIKE '%n'";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Not()
        {
            var users = new Table("users");
            users.Where(users["nome"].Not.Like("%n"));
            string sql_expected = "SELECT * FROM users WHERE NOT users.nome LIKE '%n'";
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

    }
}
