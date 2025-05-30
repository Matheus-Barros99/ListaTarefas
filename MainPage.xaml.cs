namespace ListaTarefas
{
    public partial class MainPage : ContentPage
    {
        private readonly Database.DbContexts.DatabaseDbContext _contexto;
        DateTime touchStart;
        public MainPage()
        {
            InitializeComponent();
        }
    }

}
