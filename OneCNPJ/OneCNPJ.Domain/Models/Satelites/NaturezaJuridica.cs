using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCNPJ.Domain.Models.Satelites
{
    [Table("natureza_juridica")]
    public class NaturezaJuridica : BaseModel, IEntity
    {
        [Column("descricao")]
        public string Descricao { get; set; } = string.Empty;
    }
}
