using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace VPassSample.Models
{
    public class CTarefa
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int IDCategoria { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime DataHoraLembrete { get; set; }
        public bool Concluido { get; set; }
    }
}
