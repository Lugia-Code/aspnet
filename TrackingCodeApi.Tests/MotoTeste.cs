using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TrackingCodeApi.TrackingCodeApi.Tests
{
    public class MotoTeste : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MotoTeste(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Deve_Retornar_200_Quando_Chamar_PreverBateria()
        {
            // Arrange — cria um corpo de requisição com dados de exemplo
            var input = new
            {
                Temperatura = 30.0f,
                TempoUsoHoras = 100f,
                Tensao = 3.8f
            };

            // Act — faz a chamada para o endpoint
            var response = await _client.PostAsJsonAsync("/api/v1/motos/prever-bateria", input);

            // Assert — valida se o status é 200 OK
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Opcional: lê o corpo da resposta e valida se contém a chave "bateriaPrevista"
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("bateriaPrevista", content);
        }
    }
}