using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VPassSample.Models
{
    public class CItemLista
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int IDLista { get; set; }
        public string Descricao { get; set; }
        public bool Concluido { get; set; }
    }
}
