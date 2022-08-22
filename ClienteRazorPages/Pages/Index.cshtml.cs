using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using pruebagRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Greeter.GreeterClient cliente;
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public string Nombre { get; set; }
        public string Mensaje { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            var url = "https://localhost:5001";
            var canal = GrpcChannel.ForAddress(url);
            cliente = new Greeter.GreeterClient(canal);

            _logger = logger;
        }

        public void OnGet()
        {

        }
        public async Task OnPost() 
        {
            var helloRequest = new HelloRequest();
            helloRequest.Name = Nombre;

            var resultado = await cliente.SayHelloAsync(helloRequest);

            Mensaje = resultado.Message;
        }
    }
}
