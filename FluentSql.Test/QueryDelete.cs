using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentSql.Command;

namespace FluentSql.Test
{
    [TestFixture]
    public class QueryDelete
    {
        [Test]
        public void Delete_Sem_Where()
        {
            var users = new Table("users");
            users.Delete();
            string sql_expected = "DELETE FROM users";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Delete_Com_Where()
        {
            var users = new Table("users");
            users.Where(users["idade"] < 18)
                .Delete();
            string sql_expected = "DELETE FROM users WHERE users.idade < @users_idade_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Delete_Com_Where_Complexo()
        {
            var users = new Table("users");
            users.Where(users["idade"] < 18 | users["nome"] == null)
                .Delete();
            string sql_expected = "DELETE FROM users WHERE (users.idade < @users_idade_1) OR (users.nome IS NULL)";
            Assert.AreEqual(sql_expected, users.ToSql());
        }
    }
}
