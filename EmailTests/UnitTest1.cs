using AnNetTask;

namespace EmailTests
{
    public class UnitTest1
    {
        [Fact]
        public void GetCopy_DomainAndNoException1_AddressWasAdded()
        {
            string to = "q.qweshnikov@batut.com; w.petrov@alfa.com";
            string copy = "f.patit@buisness.com";

            string resultCopy = new Email(to, copy).Copy;

            Assert.Equal("f.patit@buisness.com; v.vladislavovich@alfa.com", resultCopy);
        }

        [Fact]
        public void GetCopy_DomainAndNoException2_AddressWasAdded()
        {
            string to = "v.vtbshnikovich@vtb.ru";
            string copy = "f.patit@buisness.com";

            string resultCopy = new Email(to, copy).Copy;

            Assert.Equal("f.patit@buisness.com; a.aleksandrov@vtb.ru", resultCopy);
        }

        [Fact]
        public void GetCopy_DomainAndException_AddressWasNotAdded()
        {
            string to = "t.kogni@acl.com";
            string copy = "i.ivanov@tbank.ru";

            string resultCopy = new Email(to, copy).Copy;

            Assert.Equal("i.ivanov@tbank.ru", resultCopy);
        }

        [Fact]
        public void GetCopy_DomainAndException_AddressesWasDeleted()
        {
            string to = "t.kogni@acl.com; i.ivanov@tbank.ru";
            string copy = "e.gras@tbank.ru; t.tbankovich@tbank.ru; v.veronickovna@tbank.ru";

            string resultCopy = new Email(to, copy).Copy;

            Assert.Equal("e.gras@tbank.ru", resultCopy);
        }

        [Fact]
        public void GetCopy_NoDomainAndNoException_NothingHasChanged()
        {
            string to = "z.xcy@email.com";
            string copy = "p.rivet@email.com";

            string resultCopy = new Email(to, copy).Copy;

            Assert.Equal("p.rivet@email.com", resultCopy);
        }

        [Fact]
        public void GetCopy_EmptyArg1_throwsException()
        {
            string to = string.Empty;
            string copy = "p.rivet@email.com";

            var exception = Assert.Throws<Exception>
                (() => new Email(to, copy));

            Assert.Equal("To и Copy должны быть заполнены!", exception.Message);
        }

        [Fact]
        public void GetCopy_EmptyArg2_throwsException()
        {
            string to = "z.xcy@email.com";
            string copy = string.Empty;

            var exception = Assert.Throws<Exception>
                (() => new Email(to, copy));

            Assert.Equal("To и Copy должны быть заполнены!", exception.Message);
        }

        [Fact]
        public void GetCopy_IncorrectFormat1_throwsException()
        {
            string to = "q.qweshnikov@batut.com;; w.petrov@alfa.com";
            string copy = "f.patit@buisness.com";

            var exception = Assert.Throws<Exception>
                (() => new Email(to, copy));

            Assert.Equal("Введенные адреса не соответствуют формату!", exception.Message);
        }

        [Fact]
        public void GetCopy_IncorrectFormat2_throwsException()
        {
            string to = "q.qweshnikovbatut.com; w.petrov@alfa.com";
            string copy = "f.patit@buisness.com";

            var exception = Assert.Throws<Exception>
                (() => new Email(to, copy));

            Assert.Equal("Введенные адреса не соответствуют формату!", exception.Message);
        }

        [Fact]
        public void GetCopy_IncorrectFormat3_throwsException()
        {
            string to = "q.qweshnikov@batut.com; w.petrov@ alfa.com";
            string copy = "f.patit@buisness.com";

            var exception = Assert.Throws<Exception>
                (() => new Email(to, copy));

            Assert.Equal("Введенные адреса не соответствуют формату!", exception.Message);
        }

        [Fact]
        public void GetCopy_IncorrectFormat4_throwsException()
        {
            string to = "q.qweshnikov@batut.com; w.petrov@alfa.com";
            string copy = "f.patit @buisness.com";

            var exception = Assert.Throws<Exception>
                (() => new Email(to, copy));

            Assert.Equal("Введенные адреса не соответствуют формату!", exception.Message);
        }
    }
}