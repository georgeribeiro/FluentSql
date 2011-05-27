using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentSql.Command;

namespace FluentSql.Test.Api
{
    [TestFixture]
    public class TestField
    {
        [Test]
        public void Criar_Field_Sem_Alias()
        {
            var f = new Field(new Table("users"), "nome");
            Assert.AreEqual("users.nome", f.AsProject());
            Assert.AreEqual("users.nome", f.Project);
            Assert.IsNull(f.Alias);
        }

        [Test]
        public void Criar_Field_Com_Alias()
        {
            var f = new Field(new Table("users"), "nome").As("nome_do_usuario");
            Assert.AreEqual("users.nome AS nome_do_usuario", f.AsProject());
            Assert.AreEqual("users.nome", f.Project);
            Assert.IsNotNull(f.Alias);
        }


        [Test]
        public void Sobrecarga_De_Operadores_Maior_Menor()
        {
            var t = new Table("users");
            var f1 = t["data"];
            var f2 = t["nome"];
            var f3 = t["idade"];
            Assert.AreEqual("users.nome LIKE @users_nome_1", (f2.Like("a%")).ToSql());
            Assert.AreEqual("users.data > @users_data_1", (f1 > new DateTime(1989, 8, 22)).ToSql());
            Assert.AreEqual("users.data < @users_data_2", (f1 < DateTime.Now).ToSql());
            Assert.AreEqual("(users.idade <= @users_idade_1) AND (users.idade >= @users_idade_2)", ((f3 <= 20) & (f3 >= 10)).ToSql());
            Assert.AreEqual("(users.idade > @users_idade_3) OR (users.idade < @users_idade_4)", ((f3 > 10) | (f3 < 20)).ToSql());
        }
    }
}
