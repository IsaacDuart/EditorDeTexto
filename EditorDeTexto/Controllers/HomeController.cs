using System.Diagnostics;
using EditorDeTexto.Data;
using Microsoft.AspNetCore.Mvc;
using EditorDeTexto.Models;
using Microsoft.EntityFrameworkCore;

namespace EditorDeTexto.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    
    
    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var documentos = await _context.Documentos.ToListAsync();
        
        
        return View(documentos);
    }

    public IActionResult CriarDocumento()
    {
        return View();
    }

    public IActionResult EditarDocumento(int id)
    {
        var documento = _context.Documentos.FirstOrDefault(x => x.Id == id);
        return View(documento);
    }

    public async Task<IActionResult> RemoverDocumento(int id)
    {
        var documento = _context.Documentos.FirstOrDefault(x => x.Id == id);
        
        _context.Documentos.Remove(documento);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> EditarDocumento(Documento documentoEditado)
    {
        if (ModelState.IsValid)
        {
            var documento = _context.Documentos.FirstOrDefault(x => x.Id == documentoEditado.Id);
            documento.Titulo = documentoEditado.Titulo;
            documento.Conteudo = documentoEditado.Conteudo;
            documento.DataAlteracao = DateTime.Now;
            
            _context.Update(documento);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }
        else
        {
            return View(documentoEditado);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriarDocumento(Documento documentoRecebido)
    {
        if (ModelState.IsValid)
        {
            _context.Documentos.Add(documentoRecebido);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View(documentoRecebido);
        }
    }

    
}