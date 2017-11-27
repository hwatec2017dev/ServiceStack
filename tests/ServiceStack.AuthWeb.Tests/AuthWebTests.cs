using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using ServiceStack.Text;

namespace ServiceStack.AuthWeb.Tests
{
    [TestFixture]
    public class AuthWebTests
    {
        public const string BaseUri = "http://localhost:11001/";

        [Test]
        public void Can_authenticate_with_WindowsAuth()
        {
            var client = new JsonServiceClient(BaseUri)
            {
                Credentials = CredentialCache.DefaultCredentials
            };

            var response = client.Get(new RequiresAuth { Name = "Haz Access!" });

            Assert.That(response.Name, Is.EqualTo("Haz Access!"));
        }

        [Test]
        public void Can_authenticate_with__HTTP_Basic_Credentials()
        {
            var client = new JsonServiceClient(BaseUri)
            {
                UserName = "demis.bellot@gmail.com",
                Password = "test",
            };

            var response = client.Get(new RequiresAuth { Name = "Haz Access!" });

            Assert.That(response.Name, Is.EqualTo("Haz Access!"));
        }

        [Test]
        public void Can_Authenticate_with_Metadata()
        {
            var client = new JsonServiceClient(BaseUri);

            var response = client.Send(new Authenticate
            {
                provider = "credentials",
                UserName = "demis.bellot@gmail.com",
                Password = "test",
                Meta = new Dictionary<string, string> { { "custom", "metadata" } }
            });
        }
    }
}