using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;

namespace RabbitMQApp
{
    public class Mensagem
    {
        private readonly ConnectionFactory _factory;
        //private readonly string nomeFila = "MinhaFila";
        public Mensagem()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public void Publicar()
        {
            var conectarRabbit = _factory.CreateConnection();
            var canal = conectarRabbit.CreateModel();

            IBasicProperties iBasicProperties = canal.CreateBasicProperties();

            var cliente = new Cliente()
            {
                Nome = "Fernando",
                DataNascimento = Convert.ToDateTime("2000-03-30 00:00:00"),
                CPF = "222.222.222-22",
                RG = "22.222.222-22"
            };
            var corpoMensagem = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cliente));

            //canal.QueueDeclare(queue: nomeFila, durable: true, exclusive: false, autoDelete: true, arguments: null);
            //canal.BasicPublish(exchange: "", routingKey: nomeFila, basicProperties: iBasicProperties, body: corpoMensagem);
            canal.BasicPublish(exchange: "emails", routingKey: "", basicProperties: iBasicProperties, body: corpoMensagem);

        }
    }
}
