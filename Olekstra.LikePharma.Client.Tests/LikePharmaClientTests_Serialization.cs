namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Text.Json;
    using Xunit;

    public class LikePharmaClientTests_Serialization
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = LikePharmaClientOptions.CreateDefaultJsonSerializerOptions();
        private readonly ProtocolSettings protocolSettings = ProtocolSettings.CreateEmpty();

        [Theory]
        [InlineData(Internal.GetProgramsResponseHelperSerializationTests.ValidJsonSingular, "s")]
        [InlineData(Internal.GetProgramsResponseHelperSerializationTests.ValidJsonPlural, "p")]
        public void DeserializeGetProgramsResponseAutomatically(string json, string expectedSuffix)
        {
            var value = LikePharmaClient.DeserializeJson<GetProgramsResponse>(json, protocolSettings, jsonSerializerOptions);

            Validate(value, expectedSuffix);
        }

        [Theory]
        [InlineData(true, Internal.GetProgramsResponseHelperSerializationTests.ValidJsonSingular)]
        [InlineData(false, Internal.GetProgramsResponseHelperSerializationTests.ValidJsonPlural)]
        public void SerializeGetProgramsResponseAutomatically(bool singular, string expectedJson)
        {
            var ps = new ProtocolSettings() { SingularGetProgramsResponse = singular };

            // вместо конструирования просто десериализуем, так проще
            var value = LikePharmaClient.DeserializeJson<GetProgramsResponse>(expectedJson, ps, jsonSerializerOptions);
            Validate(value, singular ? "s" : "p");

            var newJson = LikePharmaClient.SerializeJson(value, ps, jsonSerializerOptions);

            Assert.Equal(Helper.ReformatJson(expectedJson), newJson);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UrlEncodingWorks(bool use)
        {
            var ps = new ProtocolSettings().UseUrlEncode(use, use);

            var json = Helper.ReformatJson(Internal.GetProgramsResponseHelperSerializationTests.ValidJsonPlural);

            json = use ? System.Net.WebUtility.UrlEncode(json) : json;

            var value = LikePharmaClient.DeserializeJson<GetProgramsResponse>(json, ps, jsonSerializerOptions);
            Validate(value, "p");

            var newJson = LikePharmaClient.SerializeJson(value, ps, jsonSerializerOptions);

            Assert.Equal(json, newJson);
        }

        private static void Validate(GetProgramsResponse value, string suffix)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);

            Assert.NotNull(value.Programs);
            Assert.Equal(2, value.Programs.Count);
            Assert.Equal("code1" + suffix, value.Programs[0].Code);
            Assert.Equal("name1" + suffix, value.Programs[0].Name);
            Assert.Equal("code2" + suffix, value.Programs[1].Code);
            Assert.Equal("name2" + suffix, value.Programs[1].Name);
        }
    }
}