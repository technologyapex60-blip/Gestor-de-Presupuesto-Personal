namespace Gestor_de_Presupuesto_Personal.API.Data;

using Gestor_de_Presupuesto_Personal.API.Model.Entities;
using Microsoft.EntityFrameworkCore;

public class GPPContext : DbContext
{
    public GPPContext(DbContextOptions<GPPContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Ingreso> Ingresos { get; set; }
    public DbSet<Gasto> Gastos { get; set; }
}