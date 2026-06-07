using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DespesasController : ControllerBase
{
	private readonly AppDbContext _context;

	public DespesasController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public IActionResult Get()
	{
		var despesas = _context.Despesas
			.Include(d => d.Projeto)
			.ToList();

		return Ok(despesas);
	}

	[HttpPost]
	public IActionResult Post(Despesa despesa)
	{
		_context.Despesas.Add(despesa);
		_context.SaveChanges();

		return CreatedAtAction(nameof(Get), new { id = despesa.Id }, despesa);
	}

	[HttpPut("{id}")]
	public IActionResult Put(int id, Despesa despesaAtualizada)
	{
		var despesa = _context.Despesas.Find(id);

		if (despesa == null)
			return NotFound();

		despesa.Categoria = despesaAtualizada.Categoria;
		despesa.Tipo = despesaAtualizada.Tipo;
		despesa.NomeDespesa = despesaAtualizada.NomeDespesa;
		despesa.ValorOrcado = despesaAtualizada.ValorOrcado;
		despesa.ValorRealizado = despesaAtualizada.ValorRealizado;
		despesa.ProjetoId = despesaAtualizada.ProjetoId;

		_context.SaveChanges();

		return Ok(despesa);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var despesa = _context.Despesas.Find(id);

		if (despesa == null)
			return NotFound();

		_context.Despesas.Remove(despesa);
		_context.SaveChanges();

		return NoContent();
	}
	[HttpGet("projeto/{id}")]
	public IActionResult GetByProjeto(int id)
	{
		var despesas = _context.Despesas
			.Where(d => d.ProjetoId == id)
			.ToList();

		return Ok(despesas);
	}
}