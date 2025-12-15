using Microsoft.EntityFrameworkCore;
using OneCNPJ.Domain;
using OneCNPJ.Domain.Models;
using OneCNPJ.Domain.Models.Satelites;
using System.ComponentModel.DataAnnotations.Schema;

[Table("cnpj_estab_cnae_sec")]
public class CnpjEstabelecimentoCnaeSecundario : BaseModel, IEntity
{
    [Column("estabelecimento_id")]
    public long EstabelecimentoId { get; set; }

    [ForeignKey(nameof(EstabelecimentoId))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public virtual CnpjEstabelecimento? Estabelecimento { get; set; }

    [Column("cnae_id")]
    public string CnaeId { get; set; } = string.Empty;

    [ForeignKey(nameof(CnaeId))]
    [DeleteBehavior(DeleteBehavior.Restrict)]
    public virtual Cnae? Cnae { get; set; }

    public CnpjEstabelecimentoCnaeSecundario() { }
}
