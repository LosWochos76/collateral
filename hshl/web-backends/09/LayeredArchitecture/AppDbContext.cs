using System;
using Microsoft.EntityFrameworkCore;

namespace LayeredArchitecture;

public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    { 
    }
}
