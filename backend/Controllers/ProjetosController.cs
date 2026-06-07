using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjetosController : ControllerBase
{
	private readonly AppDbContext _context;

	public ProjetosController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public IActionResult Get()
	{
		var projetos = _context.Projetos
			.Include(p => p.Pesquisador)
			.ToList();

		return Ok(projetos);
	}

	[HttpPost]
	public IActionResult Post(Projeto projeto)
	{
		_context.Projetos.Add(projeto);

		_context.SaveChanges();

		return CreatedAtAction(nameof(Get), new { id = projeto.Id }, projeto);
	}

	[HttpPut("{id}")]
	public IActionResult Put(int id, Projeto projetoAtualizado)
	{
		var projeto = _context.Projetos.Find(id);

		if (projeto == null)
		{
			return NotFound();
		}

		projeto.Nome = projetoAtualizado.Nome;
		projeto.DataInicio = projetoAtualizado.DataInicio;
		projeto.DataFim = projetoAtualizado.DataFim;
		projeto.Programa = projetoAtualizado.Programa;
		projeto.Descricao = projetoAtualizado.Descricao;
		projeto.PesquisadorId = projetoAtualizado.PesquisadorId;

		_context.SaveChanges();

		return Ok(projeto);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var projeto = _context.Projetos.Find(id);

		if (projeto == null)
		{
			return NotFound();
		}

		_context.Projetos.Remove(projeto);

		_context.SaveChanges();

		return NoContent();
	}
    [HttpGet("{id}/dashboard")]
    public IActionResult Dashboard(int id)
    {
        var projeto = _context.Projetos.Find(id);

        if (projeto == null)
            return NotFound();

        var receitaTotal = _context.Receitas
            .Where(r => r.ProjetoId == id)
            .Sum(r => r.Valor);

        var despesaOrcada = _context.Despesas
            .Where(d => d.ProjetoId == id)
            .Sum(d => d.ValorOrcado);

        var despesaRealizada = _context.Despesas
            .Where(d => d.ProjetoId == id)
            .Sum(d => d.ValorRealizado);

        var valorDisponivel = receitaTotal - despesaRealizada;

        var economia = despesaOrcada - despesaRealizada;

        return Ok(new
        {
            projeto = projeto.Nome,
            receitaTotal,
            despesaOrcada,
            despesaRealizada,
            valorDisponivel,
            economia
        });
    }
}