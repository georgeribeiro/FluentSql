using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentSql.Command;
using FluentSql.Test.Entities;
using System.Collections;
using FluentSql.Aggregates;

namespace FluentSql.Test
{
    [TestFixture]
    class ComplexQuery
    {
        [Test]
        public void Usar_Uma_Table_No_Select()
        {
            var alunos = new Table("alunos");
            alunos.Project(alunos["nome"], alunos["matricula"]);
            Assert.AreEqual("SELECT alunos.nome, alunos.matricula FROM alunos", alunos.ToSql());
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
            alunos.Project(alunos.All, cursos["nome"])
                .Join(cursos)
                .On(alunos["curso_id"] == cursos["id"]);
            string sql = "SELECT alunos.*, cursos.nome FROM alunos JOIN cursos ON alunos.curso_id = cursos.id";
            Assert.AreEqual(sql, alunos.ToSql());
        }

        [Test]
        public void Usar_Field_Com_Alias()
        {
            var users = new Table("users");
            users.Project(users["nome"].As("nome_do_usuario"), users["senha"]);
            Assert.AreEqual("SELECT users.nome AS nome_do_usuario, users.senha FROM users", users.ToSql());
            var groups = new Table("groups");
            users.Join(groups).On(users["nome"] == groups["nome_user"]);
            Assert.AreEqual("SELECT users.nome AS nome_do_usuario, users.senha FROM users JOIN groups ON nome_do_usuario = groups.nome_user", users.ToSql());
        }

        [Test]
        public void Usar_Table_Com_Alias()
        {
            var users = new Table("users", "u");
            users.Project(users["nome"], users["senha"]);
            Assert.AreEqual("SELECT u.nome, u.senha FROM users AS u", users.ToSql());
        }

        [Test]
        public void Usar_Where_Equal_Na_Consulta()
        {
            var users = new Table("users");
            users.Project(users.All).Where((users["nome"] == "george"));
            Assert.AreEqual("SELECT users.* FROM users WHERE users.nome = @users_nome_1", users.ToSql());  
        }

        [Test]
        public void Usar_Where_E_Or_Na_Consulta()
        {
            var users = new Table("users");
            users.Project(users.All).Where((users["nome"] == "george") | (users["id"] != 10));
            Assert.AreEqual("SELECT users.* FROM users WHERE (users.nome = @users_nome_1) OR (users.id <> @users_id_1)", users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Complexa_E_Usar_Clear_Para_Zerar()
        {
            var users = new Table("users");
            var groups = new Table("groups");
            users.Project(users.All, groups["nome"])
                .Join(groups).On(users["group_id"] == groups["id"]);
            Assert.AreEqual("SELECT users.*, groups.nome FROM users JOIN groups ON users.group_id = groups.id", users.ToSql());
            users.Clear();
            Assert.AreEqual("SELECT * FROM users", users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Left_Join()
        {
            var users = new Table("users", "u");
            var groups = new Table("groups", "g");
            users.Project(users.All, groups.All)
                .LeftJoin(groups)
                .On(groups["id"] == users["group_id"])
                .Where(users["id"] == 1);
            string sql_expected = "SELECT u.*, g.* FROM users AS u LEFT JOIN groups AS g ON g.id = u.group_id WHERE u.id = @users_id_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Inner_Join()
        {
            var users = new Table("users", "u");
            var groups = new Table("groups", "g");
            users.Project(users.All, groups.All)
                .InnerJoin(groups).On(groups["id"] == users["group_id"])
                .Where(users["id"] == 1 & users["nome"] != "george");
            string sql_expected = "SELECT u.*, g.* FROM users AS u INNER JOIN groups AS g ON g.id = u.group_id WHERE (u.id = @users_id_1) AND (u.nome <> @users_nome_1)";
            Assert.AreEqual(sql_expected, users.ToSql());
        }
        
        [Test]
        public void Fazer_Consulta_Com_Maior_Que_E_Menor_Que()
        {
            var users = new Table("users");
            users.Project(users["nome"]).Where(users["idade"] > 10)
                .Where(users["idade"] < 20);
            string sql_expected = "SELECT users.nome FROM users WHERE users.idade > @users_idade_1 AND users.idade < @users_idade_2";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Maior_Igual_E_Menor_Igual()
        {
            var users = new Table("users", "u");
            users.Project(users.All)
                .Where((users["idade"] >= 10) & (users["idade"] <= 20));
            string sql_expected = "SELECT u.* FROM users AS u WHERE (u.idade >= @users_idade_1) AND (u.idade <= @users_idade_2)";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Like()
        {
            var users = new Table("users");
            users.Project(users["nome"], users["senha"])
                .Where(users["nome"].Like("%n"));
            string sql_expected = "SELECT users.nome, users.senha FROM users WHERE users.nome LIKE @users_nome_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_Not()
        {
            var users = new Table("users");
            users.Where(users["nome"].Not.Like("%n"));
            string sql_expected = "SELECT * FROM users WHERE NOT users.nome LIKE @users_nome_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_Com_In()
        {
            var users = new Table("users", "u");
            users.Where(users["nome"].In("george", "ribeiro"));
            string sql_expected = "SELECT * FROM users AS u WHERE u.nome IN ('george', 'ribeiro')";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Fazer_Consulta_E_Ver_Parametros()
        {
            var users = new Table("users", "u");
            var groups = new Table("groups", "g");
            users.Project(users.All, groups["nome"])
                .LeftJoin(groups).On(groups["id"] == users["groups_id"])
                .Where(users["id"] > 10)
                .Where(groups["id"] > 20);
            string sql_expected = "SELECT u.*, g.nome FROM users AS u LEFT JOIN groups AS g ON g.id = u.groups_id WHERE u.id > @users_id_1 AND g.id > @groups_id_1";
            Assert.AreEqual(sql_expected, users.ToSql());
            Assert.AreEqual(users.Params["users_id_1"], 10);
            Assert.AreEqual(groups.Params["groups_id_1"], 20);
        }

        [Test]
        public void Usar_GroupBy_Na_Query()
        {
            var users = new Table("users");
            users.Project(users.All)
                .Where(users["nome"] == "george")
                .GroupBy(users["data_cadastro"]);
            string sql_expected = "SELECT users.* FROM users WHERE users.nome = @users_nome_1 GROUP BY users.data_cadastro";
            Assert.AreEqual(sql_expected, users.ToSql());
            Assert.AreEqual("george", users.Params["users_nome_1"]);
            users.Clear();
            Assert.AreEqual("SELECT * FROM users", users.ToSql());
        }

        [Test]
        public void Usar_Mais_De_Um_Join_Na_Query()
        {
            var clientes = new Table("clientes", "c");
            var vendas = new Table("vendas", "v");
            var produtos = new Table("produtos", "p");
            vendas.Project(vendas["data"], produtos["descricao"], clientes["nome"])
                .LeftJoin(clientes).On(clientes["id"] == vendas["cliente_id"])
                .LeftJoin(produtos).On(produtos["id"] == vendas["produto_id"])
                .Where(vendas["data"] > new DateTime(2011, 1, 1))
                .Where(produtos["preco"] != null | produtos["custo"] != null)
                .GroupBy(vendas["data"]);
            string sql_expected = "SELECT v.data, p.descricao, c.nome FROM vendas AS v LEFT JOIN clientes AS c ON c.id = v.cliente_id "
                + "LEFT JOIN produtos AS p ON p.id = v.produto_id WHERE v.data > @vendas_data_1 AND (p.preco IS NOT NULL) OR "
                + "(p.custo IS NOT NULL) GROUP BY v.data";
            Assert.AreEqual(sql_expected, vendas.ToSql());
        }

        [Test]
        public void Usar_Join_Sem_Project()
        {
            var users = new Table("users", "u");
            var groups = new Table("groups", "g");
            users.Join(groups).On(users["group_id"] == groups["id"])
                .Where(groups["descricao"].In(new string[] { "admin" }));
            string sql_expected = "SELECT * FROM users AS u JOIN groups AS g ON u.group_id = g.id WHERE g.descricao IN ('admin')";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Usar_Where_Condicional()
        {
            var i = 2;
            var users = new Table("users");
            users.Project(users["nome"]);
            if (i > 0)
            {
                users.Where(users["idade"] > i);
            }
            string sql_expected = "SELECT users.nome FROM users WHERE users.idade > @users_idade_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Usar_Entidade_User_Com_Where()
        {
            var user = new User("george");
            var table_users = new Table("users");
            if (!string.IsNullOrEmpty(user.Nome))
            {
                table_users.Where(table_users["nome"].Like(user.Nome));
            }
            if (user.Ativo.HasValue)
            {
                table_users.Where(table_users["ativo"] == user.Ativo);
            }
            if (user.Data.HasValue)
            {
                table_users.Where(table_users["data"] >= user.Data);
            }
            if (user.Nivel.HasValue)
            {
                table_users.Where(table_users["nivel"] == user.Nivel);
            }
            string sql_expected = "SELECT * FROM users WHERE users.nome LIKE @users_nome_1 AND users.data >= @users_data_1";
            Assert.AreEqual(sql_expected, table_users.ToSql());
        }

        [Test]
        public void Usar_Entidade_User_E_Group_Com_Join_E_Where()
        {
            var user = new User("george");
            user.Ativo = true;
            var group = new Group("admin");
            group.AddPermissao("can_add_user");
            group.AddPermissao("can_remove_user");
            group.AddUser(user);
            ITable t_users = new Table("users");
            ITable t_groups = new Table("groups");
            t_users.Project(t_users["nome"], t_groups["descricao"])
                .Join(t_groups).On(t_users["group_id"] == t_groups["id"]);
            if (!string.IsNullOrEmpty(user.Nome))
            {
                t_users.Where(t_users["nome"].Like(user.Nome));
            }
            if (user.Ativo.HasValue)
            {
                t_users.Where(t_users["ativo"] == user.Ativo);
            }
            if (user.Nivel.HasValue)
            {
                t_users.Where(t_users["nivel"] == user.Nivel);
            }
            if (user.Data.HasValue)
            {
                t_users.Where(t_users["data"] > user.Data);
            }
            if (group.Permissoes.Count() > 0)
            {
                t_users.Where(t_groups["permissoes"].In(group.Permissoes.ToArray()));
            }
            string sql_expected = "SELECT users.nome, groups.descricao FROM users JOIN groups ON users.group_id = groups.id "
                + "WHERE users.nome LIKE @users_nome_1 AND users.ativo = @users_ativo_1 AND users.data > @users_data_1 "
                + "AND groups.permissoes IN ('can_add_user', 'can_remove_user')";
            Assert.AreEqual(sql_expected, t_users.ToSql());
        }

        [Test]
        public void Usar_In_Sem_Ser_Array_De_String()
        {
            ITable users = new Table("users");
            users.Project(users.All)
                .Where(users["permissoes"].In("can_remove_user", "can_add_user"));
            string sql_expected = "SELECT users.* FROM users WHERE users.permissoes IN ('can_remove_user', 'can_add_user')";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Usar_Campo_Com_Underline()
        {
            var users = new Table("tbUsers");
            users.Project(users["USR_SEQUENCIAL"].As("Sequencial"), users["USR_NOME"].As("Nome"))
                .Where(users["USR_NOME"].Like("g%") & users["USR_NOME"].Like("%e"));
            string sql_expected = "SELECT tbUsers.USR_SEQUENCIAL AS Sequencial, tbUsers.USR_NOME AS Nome FROM tbUsers " +
                "WHERE (Nome LIKE @tbUsers_USR_NOME_1) AND (Nome LIKE @tbUsers_USR_NOME_2)";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Usar_Function_Count()
        {
            var users = new Table("users");
            users.Where(users["ativo"] == true).Count();
            string sql_expected = "SELECT COUNT(*) FROM users WHERE users.ativo = @users_ativo_1";
            string sql = users.ToSql();
            Assert.AreEqual(sql_expected, sql);
        }

        [Test]
        public void Usar_Function_Count_Com_Alias()
        {
            var users = new Table("users");
            users.Project(F.Count(users["ativo"]).As("count_usuarios_ativos"));
            string sql_expected = "SELECT COUNT(users.ativo) AS count_usuarios_ativos FROM users";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Usar_Function_Sum_Com_Alias()
        {
            var vendas = new Table("vendas", "v");
            var items_venda = new Table("items_venda", "i");
            vendas.Project(F.Sum(items_venda["valor"]).As("sum_items_venda_valor"))
                .Join(items_venda).On(vendas["id"] == items_venda["venda_id"])
                .Where(vendas["data"] > new DateTime(2011, 1, 1) & items_venda["valor"] > 12 | items_venda["valor"] < 1000);
            string sql_expected = "SELECT SUM(i.valor) AS sum_items_venda_valor FROM vendas AS v "
                +"JOIN items_venda AS i ON v.id = i.venda_id WHERE ((v.data > @vendas_data_1) "
                +"AND (i.valor > @items_venda_valor_1)) OR (i.valor < @items_venda_valor_2)";
            Assert.AreEqual(sql_expected, vendas.ToSql());
        }

        [Test]
        public void Usar_Function_Max()
        {
            var produtos = new Table("produtos");
            produtos.Project(F.Max(produtos["preco"]).As("preco_produto"))
                .Project(produtos["descricao"])
                .Where(produtos["descricao"] != null);
            string sql_expected = "SELECT MAX(produtos.preco) AS preco_produto, produtos.descricao FROM produtos "
                +"WHERE produtos.descricao IS NOT NULL";
            Assert.AreEqual(sql_expected, produtos.ToSql());
        }

        [Test]
        public void Usar_Function_Sum_Com_Having()
        {
            var sales = new Table("sales");
            sales.Project(sales["DeptID"], F.Sum(sales["SaleAmount"]))
                .Where(sales["SaleDate"] == new DateTime(2000, 1, 1))
                .GroupBy(sales["DeptID"])
                .Having(F.Sum(sales["SaleAmount"]) > 1000 | F.Sum(sales["SaleAmount"]) < 2000);
            string sql_expected = "SELECT sales.DeptID, SUM(sales.SaleAmount) FROM sales WHERE sales.SaleDate = @sales_SaleDate_1 "
                +"GROUP BY sales.DeptID HAVING (SUM(sales.SaleAmount) > @sales_sum_SaleAmount_1) "
            +"OR (SUM(sales.SaleAmount) < @sales_sum_SaleAmount_2)";
            Assert.AreEqual(sql_expected, sales.ToSql());
        }

        [Test]
        public void Usar_Function_Count_Com_Having_E_Join()
        {
            var employee = new Table("employee");
            var department = new Table("department");
            employee.Project(employee["DepartmentName"], F.Count(employee.All))
                .Join(department).On(employee["DepartmentID"] == department["DepartmentID"])
                .GroupBy(department["DepartmentName"])
                .Having(F.Count(employee.All) > 1);
            string sql_expected = "SELECT employee.DepartmentName, COUNT(employee.*) FROM employee JOIN department ON " +
                "employee.DepartmentID = department.DepartmentID GROUP BY department.DepartmentName HAVING COUNT(employee.*)"+
            " > @employee_count_*_1";
            Assert.AreEqual(sql_expected, employee.ToSql());
        }

        [Test]
        public void Usar_Top_Na_Consulta()
        {
            ITable users = new Table("users");
            users.Project(users["username"], users["password"])
                .Where(users["ativo"] == true).Top(20);
            string sql_expected = "SELECT TOP 20 users.username, users.password FROM users WHERE users.ativo = @users_ativo_1";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        [Test]
        public void Usar_Subselect_Na_Consulta()
        {
            ITable users = new Table("users");
            ITable groups = new Table("groups");
            groups.Project(groups.All)
                .Where(groups["name"]
                .In(users.Project(users["name"])
                .Where(users["data_criacao"] < DateTime.Today)));
            string sql_expected = "SELECT groups.* FROM groups "+
                "WHERE groups.name IN (SELECT users.name FROM users WHERE users.data_criacao < @users_data_criacao_1)";
            Assert.AreEqual(sql_expected, groups.ToSql());
        }

        [Test]
        public void Consulta_Com_Order_By()
        {
            var users = new Table("users");
            users.Project(users["name"], users["password"])
                .Where(users["active"] == true)
                .OrderBy(users["created_at"].Desc);
            string sql_expected = "SELECT users.name, users.password FROM users WHERE users.active = @users_active_1 "
                +"ORDER BY users.created_at DESC";
            Assert.AreEqual(sql_expected, users.ToSql());
        }

        public void WriteParams(ITable t)
        {
            Console.WriteLine("Params FROM {0}", t.Name);
            Console.WriteLine("-----------------------");
            foreach (DictionaryEntry de in t.Params)
            {
                Console.WriteLine("{0} = {1}", de.Key, de.Value);
            }
            Console.WriteLine("========================");
        }
    }
}
