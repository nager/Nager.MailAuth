﻿namespace Nager.EmailAuthentication.UnitTest
{
    [TestClass]
    public sealed class DmarcRecordParserTest
    {
        [TestMethod]
        public void TryParse_InvalidDmarcString1_ReturnsTrueAndPopulatesDmarcRecord()
        {
            var isSuccessful = DmarcRecordParser.TryParse("v=DMARC", out var dmarcDataFragment, out var parseErrors);
            Assert.IsTrue(isSuccessful);
            Assert.IsNotNull(dmarcDataFragment);
        }

        [TestMethod]
        public void TryParse_InvalidDmarcString2_ReturnsTrueAndPopulatesDmarcRecord()
        {
            var isSuccessful = DmarcRecordParser.TryParse("v=DMARC1", out var dmarcDataFragment, out var parseErrors);
            Assert.IsTrue(isSuccessful);
            Assert.IsNotNull(dmarcDataFragment);
            Assert.IsNull(parseErrors);
        }

        [TestMethod]
        public void TryParse_InvalidDmarcString3_ReturnsTrueAndPopulatesDmarcRecord()
        {
            var isSuccessful = DmarcRecordParser.TryParse("v=DMARC1; p=Test", out var dmarcDataFragment, out var parseErrors);
            Assert.IsTrue(isSuccessful);
            Assert.IsNotNull(dmarcDataFragment);
            Assert.AreEqual("Test", dmarcDataFragment.DomainPolicy);
            Assert.IsNotNull(parseErrors);
            Assert.IsTrue(parseErrors.Length == 1);
        }

        [TestMethod]
        public void TryParse_InvalidDmarcString4_ReturnsTrueAndPopulatesDmarcRecord()
        {
            var isSuccessful = DmarcRecordParser.TryParse("v=DMARC1; p=Test;", out var dmarcDataFragment, out var parseErrors);
            Assert.IsTrue(isSuccessful);
            Assert.IsNotNull(dmarcDataFragment);
            Assert.AreEqual("Test", dmarcDataFragment.DomainPolicy);
            Assert.IsNotNull(parseErrors);
            Assert.IsTrue(parseErrors.Length == 1);
        }

        [TestMethod]
        public void TryParse_ValidDmarcString1_ReturnsTrueAndPopulatesDmarcRecord()
        {
            var isSuccessful = DmarcRecordParser.TryParse("v=DMARC1; p=reject;", out var dmarcDataFragment, out var parseErrors);
            Assert.IsTrue(isSuccessful);
            Assert.IsNotNull(dmarcDataFragment);
            Assert.AreEqual("reject", dmarcDataFragment.DomainPolicy);
            Assert.IsNull(parseErrors);
        }

        [TestMethod]
        public void TryParse_ValidDmarcString2_ReturnsTrueAndPopulatesDmarcRecord()
        {
            var isSuccessful = DmarcRecordParser.TryParse("v=DMARC1; p=reject; sp=none;", out var dmarcDataFragment, out var parseErrors);
            Assert.IsTrue(isSuccessful);
            Assert.IsNotNull(dmarcDataFragment);
            Assert.AreEqual("reject", dmarcDataFragment.DomainPolicy);
            Assert.AreEqual("none", dmarcDataFragment.SubdomainPolicy);
            Assert.IsNull(parseErrors);
        }

        [TestMethod]
        public void TryParse_CorruptDmarcString1_ReturnsTrueAndPopulatesDmarcRecord()
        {
            var isSuccessful = DmarcRecordParser.TryParse("verification=123456789", out var dmarcDataFragment, out var parseErrors);
            Assert.IsTrue(isSuccessful);
            Assert.IsNotNull(dmarcDataFragment);
            Assert.IsNotNull(parseErrors);
            Assert.IsTrue(parseErrors.Length == 2);
        }

        [TestMethod]
        public void TryParse_CorruptDmarcString2_ReturnsTrueAndPopulatesDmarcRecord()
        {
            var isSuccessful = DmarcRecordParser.TryParse(" ", out var dmarcDataFragment, out var parseErrors);
            Assert.IsFalse(isSuccessful);
            Assert.IsNull(dmarcDataFragment);
            Assert.IsNull(parseErrors);
        }
    }
}
