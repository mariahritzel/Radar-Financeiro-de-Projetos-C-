using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceitasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReceitasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var receitas = _context.Receitas
            .Include(r => r.Projeto)
            .ToList();

        return Ok(receitas);
    }

    [HttpPost]
    public IActionResult Post(Receita receita)
    {
        _context.Receitas.Add(receita);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = receita.Id }, receita);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Receita receitaAtualizada)
    {
        var receita = _context.Receitas.Find(id);

        if (receita == null)
            return NotFound();

        receita.Tipo = receitaAtualizada.Tipo;
        receita.Origem = receitaAtualizada.Origem;
        receita.Valor = receitaAtualizada.Valor;
        receita.DataEntrada = receitaAtualizada.DataEntrada;
        receita.ProjetoId = receitaAtualizada.ProjetoId;

        _context.SaveChanges();

        return Ok(receita);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var receita = _context.Receitas.Find(id);

        if (receita == null)
            return NotFound();

        _context.Receitas.Remove(receita);
        _context.SaveChanges();

        return NoContent();
    }
    [HttpGet("projeto/{id}")]
    public IActionResult GetByProjeto(int id)
    {
        var receitas = _context.Receitas
            .Where(r => r.ProjetoId == id)
            .ToList();

        return Ok(receitas);
    }
}