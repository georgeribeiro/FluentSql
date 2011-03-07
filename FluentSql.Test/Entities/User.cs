using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Test.Entities
{
    class User
    {
        public int? Id { get; set; }
        public string Nome { get; private set; }
        public DateTime? Data { get; private set; }
        public int? Nivel { get; set; }
        public bool? Ativo { get; set; }
        public Group Group { get; set; }

        public User(string nome)
        {
            Nome = nome;
            Data = DateTime.Now;
        }
    }
}
