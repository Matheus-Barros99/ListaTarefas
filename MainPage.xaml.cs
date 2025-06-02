using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Database.Models;
using ListaTarefas.ViewModels;
using ListaTarefas.Views;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefas
{
    public partial class MainPage : ContentPage
    {
        private readonly Database.DbContexts.DatabaseDbContext _contexto;
        private IList<TarefaViewModel> Tarefas;
        DateTime touchStart;
        public MainPage(Database.DbContexts.DatabaseDbContext contexto)
        {
            _contexto = contexto;

            InitializeComponent();

            RecuperaLista();
        }

        async void CheckboxConcluir_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var idTarefa = (int)checkbox.BindingContext;

            var tarefa = await _contexto.Tarefas.FindAsync(idTarefa);

            if (checkbox.IsChecked)
            {
                tarefa.StatusTarefa = Database.Models.Enums.StatusTarefa.Concluida;
            }
            else
            {
                tarefa.StatusTarefa = Database.Models.Enums.StatusTarefa.Pendente;
            }

            await _contexto.SaveChangesAsync();
        }

        private void BtnAdicionar_Pressed(object sender, EventArgs e)
        {
            touchStart = DateTime.Now;
        }

        async void BtnAdicionar_Released(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdicionarTarefaView(_contexto));
        }

        private void BtnRefresh_Pressed(object sender, EventArgs e)
        {
            touchStart = DateTime.Now;
        }

        private void BtnRefresh_Released(object sender, EventArgs e)
        {
            RecuperaLista();
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            RecuperaLista();
        }

        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {

        }

        //Impede que seja retornada a página
        protected override bool OnBackButtonPressed()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            var toast = Toast.Make("Não é possível retornar!", ToastDuration.Long, 23);
            toast.Show(cancellationTokenSource.Token);

            return true;
        }

        async void RecuperaLista()
        {
            Tarefas = await _contexto.Tarefas
                                     .Where(t => t.StatusTarefa == Database.Models.Enums.StatusTarefa.Pendente)
                                     .Select(t => new TarefaViewModel
                                     {
                                         Id = t.Id,
                                         DescricaoTarefa = t.Descricao
                                     }).ToListAsync();

            ListViewTarefas.ItemsSource = Tarefas.ToList();
        }

        private void ListViewTarefas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }

}
