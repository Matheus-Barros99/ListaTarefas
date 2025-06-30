using Database.DbContexts;

namespace ListaTarefas
{
    public partial class App : Application
    {
        private readonly DatabaseDbContext _contexto;
        public App(DatabaseDbContext contexto)
        {
            _contexto = contexto;
            InitializeComponent();

            var timerExpurgo = Application.Current.Dispatcher.CreateTimer();
            timerExpurgo.Interval = TimeSpan.FromMinutes(5);
            timerExpurgo.Tick += (s, e) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        await new Controls.TarefaControl(_contexto).ExpurgarTarefas();
                    }
                    catch (Exception ex)
                    {
                        string erro = ex.Message;
                    }
                    finally{ }

                });
            };
            timerExpurgo.Start();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}