using System.ComponentModel.DataAnnotations;

namespace EditorDeTexto.Models;

public class Documento
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(60)]
    public string Titulo { get; set; } = "Sem Titulo";
    public string Conteudo { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime DataAlteracao { get; set; } = DateTime.Now;
    
}