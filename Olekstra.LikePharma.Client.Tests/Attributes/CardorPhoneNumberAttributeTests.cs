namespace Olekstra.LikePharma.Client.Attributes
{
    using System;
    using Xunit;

    public class CardOrPhoneNumberAttributeTests
    {
        private const string ValidCardNumber = "12345";
        private const string ValidPhoneNumber = "12345";

        [Theory]
        [InlineData(true, ValidCardNumber, "")]
        [InlineData(false, "", ValidPhoneNumber)]
        [InlineData(false, ValidCardNumber, ValidPhoneNumber)]
        [InlineData(false, "", "")]
        public void CardOnly(bool expectedValid, string card, string phone)
        {
            RunTest(CardAndPhoneUsage.CardOnly, expectedValid, card, phone);
        }

        [Theory]
        [InlineData(false, ValidCardNumber, "")]
        [InlineData(true, "", ValidPhoneNumber)]
        [InlineData(false, ValidCardNumber, ValidPhoneNumber)]
        [InlineData(false, "", "")]
        public void PhoneOnly(bool expectedValid, string card, string phone)
        {
            RunTest(CardAndPhoneUsage.PhoneOnly, expectedValid, card, phone);
        }

        [Theory]
        [InlineData(true, ValidCardNumber, "")]
        [InlineData(true, "", ValidPhoneNumber)]
        [InlineData(true, ValidCardNumber, ValidPhoneNumber)]
        [InlineData(false, "", "")]
        public void CardOrPhone(bool expectedValid, string card, string phone)
        {
            RunTest(CardAndPhoneUsage.CardOrPhone, expectedValid, card, phone);
        }

        [Theory]
        [InlineData(true, ValidCardNumber, "")]
        [InlineData(true, "", ValidPhoneNumber)]
        [InlineData(false, ValidCardNumber, ValidPhoneNumber)]
        [InlineData(false, "", "")]
        public void CardXorPhone(bool expectedValid, string card, string phone)
        {
            RunTest(CardAndPhoneUsage.CardXorPhone, expectedValid, card, phone);
        }

        [Theory]
        [InlineData(false, ValidCardNumber, "")]
        [InlineData(false, "", ValidPhoneNumber)]
        [InlineData(true, ValidCardNumber, ValidPhoneNumber)]
        [InlineData(false, "", "")]
        public void CardAndPhone(bool expectedValid, string card, string phone)
        {
            RunTest(CardAndPhoneUsage.CardAndPhone, expectedValid, card, phone);
        }

        private void RunTest(CardAndPhoneUsage policyValue, bool expectedValid, string card, string phone)
        {
            var protocolSettings = ProtocolSettings.CreateEmpty();
            protocolSettings.CardAndPhoneUsage = policyValue;

            var sampleRequest = new ConfirmPurchaseRequest
            {
                PosId = "12345",
                CardNumber = card,
                PhoneNumber = phone,
                Transactions = { "12345" },
            };

            var isValid = new LikePharmaValidator(protocolSettings).TryValidateObject(sampleRequest, out var results);

            if (expectedValid)
            {
                Assert.True(isValid);
                Assert.Empty(results);
            }
            else
            {
                Assert.False(isValid);
                Assert.Single(results);
            }
        }
    }
}
