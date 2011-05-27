using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentSql.Command;
using System.Collections;

namespace FluentSql.Test
{
    [TestFixture]
    public class QueryInsert
    {
        [Test]
        public void Insert_Simples()
        {
            ITable users = new Table("users");
            users.Insert(new { nome = "george", data_nascimento = new DateTime(1989, 8, 22), sexo = "M" });
            string sql_expected = "INSERT INTO users(data_nascimento, nome, sexo) "+
                "VALUES(@users_data_nascimento_1, @users_nome_1, @users_sexo_1)";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Insert_Usando_Hashtable()
        {
            Hashtable hs = new Hashtable();
            hs.Add("nome", "george");
            hs.Add("data_nascimento", new DateTime(1989, 8, 22));
            hs.Add("sexo", "M");
            var t_users = new Table("users");
            t_users.Insert(hs);
			Console.WriteLine(t_users.ToSql());
            string sql_expected = "INSERT INTO users(data_nascimento, nome, sexo) "
                                    +"VALUES(@users_data_nascimento_1, @users_nome_1, @users_sexo_1)";
            Assert.AreEqual(sql_expected, t_users.ToSql());
        }

        [Test]
        public void Insert_Com_Valor_Nulo()
        {
            var users = new Table("users");
            users.Insert(new { nome="george", idade=Utils.NULL });
            string sql_expected = "INSERT INTO users(idade, nome) VALUES(NULL, @users_nome_1)";
            Assert.AreEqual(sql_expected, users.ToSql());
        }
    }
}
