using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PesquisadoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public PesquisadoresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var pesquisadores = _context.Pesquisadores.ToList();

        return Ok(pesquisadores);
    }

    [HttpPost]
    public IActionResult Post(Pesquisador pesquisador)
    {
        _context.Pesquisadores.Add(pesquisador);

        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = pesquisador.Id }, pesquisador);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Pesquisador pesquisadorAtualizado)
    {
        var pesquisador = _context.Pesquisadores.Find(id);

        if (pesquisador == null)
        {
            return NotFound();
        }

        pesquisador.Nome = pesquisadorAtualizado.Nome;
        pesquisador.Email = pesquisadorAtualizado.Email;
        pesquisador.Curso = pesquisadorAtualizado.Curso;
        pesquisador.Departamento = pesquisadorAtualizado.Departamento;

        _context.SaveChanges();

        return Ok(pesquisador);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pesquisador = _context.Pesquisadores.Find(id);

        if (pesquisador == null)
        {
            return NotFound();
        }

        _context.Pesquisadores.Remove(pesquisador);

        _context.SaveChanges();

        return NoContent();
    }
}