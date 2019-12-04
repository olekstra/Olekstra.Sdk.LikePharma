namespace Olekstra.LikePharma.Server
{
    using System;
    using System.Text.Json;
    using Xunit;

    public class LikePharmaMiddlewareOptionsTests
    {
        [Fact]
        public void DoNotEncodesCyrillic()
        {
            var sample = "Пример";

            var json = JsonSerializer.Serialize(sample, new LikePharmaMiddlewareOptions().JsonSerializerOptions);
            Assert.Equal("\"Пример\"", json);
        }
    }
}
