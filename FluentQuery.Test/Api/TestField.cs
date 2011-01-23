using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentQuery.Test.Api
{
    [TestFixture]
    class TestField
    {
        [Test]
        public void Criar_Field_Sem_Alias()
        {
            var f = new Field(new Table("users"), "nome");
            Assert.AreEqual("users.nome", f.ToSql());
            Assert.AreEqual("users.nome", f.Project);
            Assert.IsNull(f.Alias);
        }

        [Test]
        public void Criar_Field_Com_Alias()
        {
            var f = new Field(new Table("users"), "nome").As("nome_do_usuario");
            Assert.AreEqual("users.nome AS nome_do_usuario", f.ToSql());
            Assert.AreEqual("users.nome", f.Project);
            Assert.NotNull(f.Alias);
        }
    }
}
