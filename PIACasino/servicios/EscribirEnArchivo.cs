namespace PIACasino.servicios
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "Ejecucion.txt";

        public EscribirEnArchivo(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Escribir("Programa iniciado: " + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Escribir("Programa finalizad: " + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            return Task.CompletedTask;
        }

        private void Escribir(string mensaje)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(mensaje); }
        }
    }
}
