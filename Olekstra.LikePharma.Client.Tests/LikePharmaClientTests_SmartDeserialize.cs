namespace Olekstra.LikePharma.Client
{
    using System;
    using System.Text.Json;
    using Xunit;

    public class LikePharmaClientTests_SmartDeserialize
    {
        public const string ValidGetProgramsResponseSingularJson = @"
{
""program"":[
{
""code"":""code1"",
""name"":""name1""
},
{
""code"":""code2"",
""name"":""name2""
}],
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        public const string ValidGetProgramsResponsePluralJson = @"
{
""programs"":[
{
""code"":""code1"",
""name"":""name1""
},
{
""code"":""code2"",
""name"":""name2""
}],
""status"":""error"",
""error_code"":11,
""message"":""Hello, World!""
}";

        private readonly JsonSerializerOptions jsonSerializerOptions = LikePharmaClientOptions.CreateDefaultJsonSerializerOptions();

        private readonly ProtocolSettings protocolSettings = ProtocolSettings.CreateEmpty();

        [Theory]
        [InlineData(ValidGetProgramsResponseSingularJson)]
        [InlineData(ValidGetProgramsResponsePluralJson)]
        public void DeserializeGetProgramsResponse(string json)
        {
            var obj = LikePharmaClient.DeserializeJson<GetProgramsResponse>(json, protocolSettings, jsonSerializerOptions);
            ValidateObject(obj);
        }

        private static void ValidateObject(GetProgramsResponse value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("error", value.Status);
            Assert.Equal(11, value.ErrorCode);
            Assert.Equal("Hello, World!", value.Message);

            Assert.NotNull(value.Programs);
            Assert.Equal(2, value.Programs.Count);
            Assert.Equal("code1", value.Programs[0].Code);
            Assert.Equal("name1", value.Programs[0].Name);
            Assert.Equal("code2", value.Programs[1].Code);
            Assert.Equal("name2", value.Programs[1].Name);
        }
    }
}