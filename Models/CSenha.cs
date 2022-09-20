using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VPassSample.Models
{
    public class CSenha
    {
        [PrimaryKey, AutoIncrement]
        public int IDSenha { get; set; }
        public string Identificacao { get; set; }
        public string Senha { get; set; }

        [ForeignKey(typeof(CCategoria))]     // Specify the foreign key
        public int IDCategoria { get; set; }

        [ManyToOne]      // Many to one relationship with CCategoria
        public CCategoria Categorias { get; set; }

    }
}
