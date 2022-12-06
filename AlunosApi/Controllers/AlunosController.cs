using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlunosApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlunosController : ControllerBase
	{
		private IAlunoService _alunoService;

		public AlunosController(IAlunoService alunoService)
		{
			_alunoService = alunoService;
		}

		[HttpGet]
		public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
		{
			try
			{
				var alunos = await _alunoService.GetAlunos();
				return Ok(alunos);
			}
			catch
			{
				//return BadRequest("Request inválido");
				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
			}
		}

		[HttpGet("AlunoByName")]
		public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery] string name)
		{
			try
			{
				var alunos = await _alunoService.GetAlunosByName(name);
				if (alunos.Count() == 0)
				{
					return NotFound($"Não foram encontrados alunos com o nome {name}");
				}
				return Ok(alunos);
			}
			catch
			{
				return BadRequest("Request inválido");
			}
		}

		[HttpGet("{id:int}", Name="GetAluno")]
		public async Task<ActionResult<Aluno>> GetAluno(int id)
		{
			try
			{
				var aluno = await _alunoService.GetAluno(id);
				if (aluno == null)
				{
					return NotFound($"Não foi encontrado o aluno com id {id}");
				}
				return Ok(aluno);
			}
			catch
			{
				return BadRequest("Request inválido");
			}
		}

		[HttpPost]
		public async Task<ActionResult> Create(Aluno aluno)
		{
			try
			{
				await _alunoService.CreateAluno(aluno);
				return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
			}
			catch
			{
				return BadRequest("Request inválido");
			}
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Update(int id, [FromBody]Aluno aluno)
		{
			try
			{
				if (aluno.Id == id)
				{
					await _alunoService.UpdateAluno(aluno);
					return Ok($"Aluno com id = {id} foi atualizado com sucesso");
				}
				else
				{
					return BadRequest("Dados inválidos");
				}
			}
			catch
			{
				return BadRequest("Request inválido");
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				var aluno = await _alunoService.GetAluno(id);
				if (aluno != null)
				{
					await _alunoService.DeleteAluno(aluno);
					return Ok($"Aluno com id = {id} foi excluído com sucesso");
				}
				else
				{
					return NotFound($"Aluno com id = {id} não encontrado");
				}
			}
			catch
			{
				return BadRequest("Request inválido");
			}
		}
	}
}
