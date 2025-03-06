using RabbitMQ.Client;
using System.Text;


var factory = new ConnectionFactory {HostName = "localhost"};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


channel.QueueDeclare(queue: "hello",
                    durable: false,
                    exclusive: false,
                    arguments: null);

Console.WriteLine(value: "Digite sua mensagem e aperte <ENTER>");


while (true)
{
    string message = Console.ReadLine();

    if (message == "")
        break;

    // var aluno = new Aluno() { Id = 1, Nome = "Milton" };
    //message = JsonSerializer.Serialize(aluno);

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: string.Empty,          
                         routingKey:"hello",
                         basicProperties: null,
                         body:body);

    Console.WriteLine($"[x] Enviado {message}");
}
