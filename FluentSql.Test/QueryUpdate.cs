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

        [Test]
        public void Update_Com_Where_E_Valor_Nulo()
        {
            var users = new Table("users");
            users.Where(users["nome"] != null)
                .Update(new { idade = Utils.NULL });
            string sql_expected = "UPDATE users SET idade=NULL WHERE users.nome IS NOT NULL";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Update_Com_Where_E_Hashtable()
        {
            var users = new Table("users");
            Hashtable hs = new Hashtable();
            hs.Add("superuser", false);
            hs.Add("ativo", true);
            users.Where(users["salario"] <= 2000)
                .Update(hs);
            string sql_expected = "UPDATE users SET ativo=@users_ativo_1, superuser=@users_superuser_1 WHERE users.salario <= @users_salario_1";
            Assert.AreEqual(sql_expected, users.ToSql());
            Assert.AreEqual(true, users.Params["users_ativo_1"]);
            Assert.AreEqual(false, users.Params["users_superuser_1"]);
        }
    }
}
