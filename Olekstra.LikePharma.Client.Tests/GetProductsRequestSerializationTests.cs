﻿namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class GetProductsRequestSerializationTests : SerializationTestsBase<GetProductsRequest>
    {
        private const string ValidJson = @"
{
""pos_id"":""A123"",
""pharmacy_id"":""test_pharmacy""
}";

        private const string ValidXml = @"
<get_products_request>
<pos_id>A123</pos_id>
<pharmacy_id>test_pharmacy</pharmacy_id>
</get_products_request>";

        public GetProductsRequestSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(GetProductsRequest value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal("A123", value.PosId);
            Assert.Equal("test_pharmacy", value.PharmacyId);
        }
    }
}
