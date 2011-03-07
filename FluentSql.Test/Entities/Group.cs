using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentSql.Test.Entities
{
    class Group
    {
        public int Id { get; set; }
        public string Descricao { get; private set; }
        public IList<string> _Permissoes;
        public IList<User> _Users;

        public Group(string descricao)
        {
            Descricao = descricao;
            _Permissoes = new List<string>();
            _Users = new List<User>();
        }

        public void AddPermissao(string permissao)
        {
            _Permissoes.Add(permissao);
        }

        public IEnumerable<string> Permissoes
        {
            get
            {
                return _Permissoes.AsEnumerable();
            }
        }

        public void AddUser(User user)
        {
            user.Group = this;
            _Users.Add(user);
        }

        public IEnumerable<User> Users
        {
            get
            {
                return _Users.AsEnumerable();
            }
        }
    }
}
