using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentQuery.Test.Api
{
    [TestFixture]
    class TestTable
    {
        [Test]
        public void Criar_Table_E_Verificar_O_Nome()
        {
            var t = new Table("alunos");
            Assert.AreEqual("alunos", t.Name);
            Assert.IsNull(t.Alias);
            var t2 = new Table("alunos", "a");
            Assert.AreEqual("a", t2.Alias);
        }
    }
}
