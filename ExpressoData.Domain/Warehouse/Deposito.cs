namespace ExpressoData.Domain.Warehouse;

public class Deposito
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public int NumeroLoja { get; set; }
}