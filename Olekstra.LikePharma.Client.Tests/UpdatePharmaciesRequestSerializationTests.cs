namespace Olekstra.LikePharma.Client
{
    using System;
    using Xunit;

    public class UpdatePharmaciesRequestSerializationTests : SerializationTestsBase<UpdatePharmaciesRequest>
    {
        private const string ValidJson = @"
{
""update_mode"":""merge"",
""pharmacies"":[
{
""id"":""test_pharmacy"",
""brand"":""sample"",
""region"":""center"",
""city"":""Moscow"",
""address"":""Central street, 1"",
""gps"":""55.55, 37.37"",
""work_time"":""24x7"",
""phones"":""No, only fax"",
""pos_ids"":[""test-01"",""test-02""],
""disabled"":true
}
]
}";

        private const string ValidXml = @"
<update_pharmacies_request>
<update_mode>merge</update_mode>
<pharmacies>
<pharmacy>
<id>test_pharmacy</id>
<brand>sample</brand>
<region>center</region>
<city>Moscow</city>
<address>Central street, 1</address>
<gps>55.55, 37.37</gps>
<work_time>24x7</work_time>
<phones>No, only fax</phones>
<pos_ids>
<pos_id>test-01</pos_id>
<pos_id>test-02</pos_id>
</pos_ids>
<disabled>true</disabled>
</pharmacy>
</pharmacies>
</update_pharmacies_request>";

        public UpdatePharmaciesRequestSerializationTests()
            : base(ValidJson, ValidXml)
        {
            // Nothing
        }

        public override void ValidateObject(UpdatePharmaciesRequest value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Assert.Equal(Globals.UpdateModeMerge, value.UpdateMode);

            Assert.Single(value.Pharmacies);

            var p = value.Pharmacies[0];
            Assert.Equal("test_pharmacy", p.Id);
            Assert.Equal("sample", p.Brand);
            Assert.Equal("center", p.Region);
            Assert.Equal("Moscow", p.City);
            Assert.Equal("Central street, 1", p.Address);
            Assert.Equal("55.55, 37.37", p.Gps);
            Assert.Equal("24x7", p.WorkTime);
            Assert.Equal("No, only fax", p.Phones);
            Assert.True(p.Disabled);
        }
    }
}
