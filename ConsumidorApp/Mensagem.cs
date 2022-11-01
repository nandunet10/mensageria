using RabbitMQ.Client;
using System.Text;

namespace Consumidor
{
    public class Mensagem
    {
        private readonly ConnectionFactory _factory;
        //private readonly string nomeFila = "cadastro";
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

        public void PegarMensagem(string nomeFila)
        {
            var conectarRabbit = _factory.CreateConnection();
            var canal = conectarRabbit.CreateModel();
            while (true)
            {
                BasicGetResult retorno = canal.BasicGet(nomeFila, false);
                if (retorno == null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    Task.Delay(50000);
                    canal.BasicAck(retorno.DeliveryTag, true);

                }
            }
        }
    }
}
