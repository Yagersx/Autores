namespace WebApiAutores.Servicios
{
    public interface IServicio
    {
        void RealizarTarea();
    }

    public class ServicioA : IServicio
    {
        private readonly ILogger<ServicioA> logger;
        public ServicioA(ILogger<ServicioA> logger)
        {
            this.logger = logger;
        }
        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }

    public class ServicioB : IServicio
    {
        public ServicioB()
        {
        }
        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }

    public class ServicioTransient
    {
        public Guid Guid = Guid.NewGuid();

    }


    public class ServicioScoped
    {
        public Guid Guid = Guid.NewGuid();

    }

    public class ServicioSingleton
    {
        public Guid Guid = Guid.NewGuid();

    }
}
