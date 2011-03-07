using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentQuery.Command;

namespace FluentQuery.Test
{
    [TestFixture]
    public class QueryInsert
    {
        [Test]
        public void Insert_Simples()
        {
            ITable users = new Table("users");
            users.Insert(new { nome = "george", data_nascimento = new DateTime(1989, 8, 22), sexo = "M" });
            Assert.AreEqual("INSERT INTO users(nome, data_nascimento, sexo) VALUES(@users_nome_1, @users_data_nascimento_1, @users_sexo_1)", users.ToSql());
        }
    }
}
