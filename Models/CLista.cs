using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VPassSample.Models
{
    public class CLista
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Descricao { get; set; }
        public bool Concluido { get; set; }
    }
}
