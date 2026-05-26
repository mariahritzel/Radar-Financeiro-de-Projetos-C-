namespace backend.Models;

public class Despesa
{
    public int Id { get; set; }

    public string Categoria { get; set; }

    public string Tipo { get; set; }

    public string NomeDespesa { get; set; }

    public decimal ValorUnitario { get; set; }

    public int Quantidade { get; set; }

    public decimal ValorTotal { get; set; }

    // Relacionamento com Projeto
    public int ProjetoId { get; set; }

    public Projeto Projeto { get; set; }
}