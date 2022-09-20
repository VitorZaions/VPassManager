using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VPassSample.Models
{
    public class CCategoria
    {
        [PrimaryKey, AutoIncrement]
        public int IDCategoria { get; set; }
        public string Descricao { get; set; }

        [ForeignKey(typeof(CUsuario))]     // Specify the foreign key
        public int IDUsuario { get; set; }

        [ManyToOne]      // Many to one relationship with Stock
        public CUsuario User { get; set; }

        [OneToMany]
        public List<CSenha> Senhas { get; set; }
    }
}
