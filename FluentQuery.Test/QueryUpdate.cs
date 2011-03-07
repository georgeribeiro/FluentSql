using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentQuery.Command;

namespace FluentQuery.Test
{
    [TestFixture]
    public class QueryUpdate
    {
        [Test]
        public void Update_Sem_Where()
        {
            ITable users = new Table("users");
            users.Update(new
            {
                ativo=true
            });

            string sql_expected = "UPDATE users SET ativo=@users_ativo_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Update_Com_Where()
        {
            var users = new Table("users");
            users.Update(new 
            { 
                ativo=true, 
                salario=500 
            })
            .Where(users["idade"] > 20);
            string sql_expected = "UPDATE users SET ativo=@users_ativo_1, salario=@users_salario_1 WHERE users.idade > @users_idade_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Update_Com_Where_Primeiro()
        {
            var users = new Table("users");
            users.Where(users["idade"] > 20)
            .Update(new
            {
                ativo = true,
                salario = 500
            });
            string sql_expected = "UPDATE users SET ativo=@users_ativo_1, salario=@users_salario_1 WHERE users.idade > @users_idade_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }
    }
}
