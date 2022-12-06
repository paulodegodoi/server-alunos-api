using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
		{
		}

		public DbSet<Aluno> Alunos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Aluno>().HasData(
				new Aluno
				{
					Id = 1,
					Name = "Paulo Godoi",
					Email = "paulodegodoi@gmail.com",
					Birth = 27072000
				},
				new Aluno
				{
					Id = 2,
					Name = "Maria Clara",
					Email = "mariaclara@gmail.com",
					Birth = 05022004
				}
			);
		}
	}
}
