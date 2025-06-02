using Database.Models;

namespace ListaTarefas.Views;

public partial class AdicionarTarefaView : ContentPage
{
    private readonly Database.DbContexts.DatabaseDbContext _contexto;
    private Tarefa Tarefa;
    public AdicionarTarefaView(Database.DbContexts.DatabaseDbContext contexto)
    {
        _contexto = contexto;

        InitializeComponent();

        Tarefa = new Tarefa
        {
            Descricao = "",
            StatusTarefa = Enums.StatusTarefa.Pendente
        };
    }

    private void EntryDescricao_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;

        Tarefa.Descricao = entry.Text;
    }

    async void BtnSalvar_Released(object sender, EventArgs e)
    {
        await _contexto.Tarefas.AddAsync(Tarefa);
        await _contexto.SaveChangesAsync();

        await Navigation.PopToRootAsync();
    }
}