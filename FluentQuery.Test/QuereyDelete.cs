using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentQuery.Command;

namespace FluentQuery.Test
{
    [TestFixture]
    public class QuereyDelete
    {
        [Test]
        public void Delete_Sem_Where()
        {
            var users = new Table<Delete>("users");
            string sql_expected = "DELETE FROM users";
            Assert.AreEqual(sql_expected, users.ToSql());
        }
    }
}
