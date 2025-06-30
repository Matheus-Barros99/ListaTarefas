using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaTarefas.Controls
{
    public class TarefaControl
    {
        private readonly Database.DbContexts.DatabaseDbContext _contexto;

        public TarefaControl(Database.DbContexts.DatabaseDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task ExpurgarTarefas()
        {
            var listaTarefasExpurgo = _contexto.Tarefas
                                               .Where(t => t.StatusTarefa == Database.Models.Enums.StatusTarefa.Concluida
                                                        && t.Finalizacao <= DateTime.Now.Date.AddDays(10))
                                               .ToList();

            if (listaTarefasExpurgo.Any())
            {
                _contexto.Tarefas.RemoveRange(listaTarefasExpurgo);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}
