using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace VPassSample.Models
{
    public class CUsuario
    {
        [PrimaryKey, AutoIncrement]
        public int IDUsuario { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public int Token { get; set; }

        [OneToMany]
        public List<CCategoria> Categorias { get; set; }

    }
}
