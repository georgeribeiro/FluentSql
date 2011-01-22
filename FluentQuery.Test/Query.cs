using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentQuery.Test
{
    [TestFixture]
    class Query
    {
        [Test]
        public void Usar_Uma_Table_No_Select()
        {
            var alunos = new Table("alunos");
            alunos.Project(alunos["nome"], alunos["matricula"]);
            Assert.AreEqual("SELECT alunos.nome AS alunos_nome, alunos.matricula AS alunos_matricula FROM alunos", alunos.ToSql());
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
            string sql = "SELECT alunos.*, cursos.nome AS cursos_nome FROM alunos JOIN cursos ON alunos.curso_id = cursos.curso_id";
            Assert.AreEqual(sql, alunos.ToSql());
        }
    }
}
