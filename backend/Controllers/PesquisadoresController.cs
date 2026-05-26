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
}