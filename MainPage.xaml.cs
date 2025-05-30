namespace ListaTarefas
{
    public partial class MainPage : ContentPage
    {
        private readonly Database.DbContexts.DatabaseDbContext _contexto;
        DateTime touchStart;
        public MainPage(Database.DbContexts.DatabaseDbContext contexto)
        {
            _contexto = contexto;

            InitializeComponent();

            ListViewTarefas.ItemsSource = _contexto.Tarefas.Where(t => t.StatusTarefa == Database.Models.Enums.StatusTarefa.Pendente).ToList();
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

        }

        private void BtnAdicionar_Released(object sender, EventArgs e)
        {

        }

        private void BtnRefresh_Pressed(object sender, EventArgs e)
        {

        }

        private void BtnRefresh_Released(object sender, EventArgs e)
        {

        }
    }

}
