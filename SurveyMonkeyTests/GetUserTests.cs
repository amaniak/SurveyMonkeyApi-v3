﻿using System;
using NUnit.Framework;
using SurveyMonkey;

namespace SurveyMonkeyTests
{
    [TestFixture]
    public class GetUserTests
    {
        [Test]
        public void GetUserMeIsDeserialised()
        {
            var client = new MockWebClient();
            client.Responses.Add(@"
                {""username"":""test@gmail.com"",""first_name"":null,""last_name"":""McTest"",""account_type"":""gold"",""language"":""en"",""email"":""testemail@gmail.com"",""href"":""https:\/\/api.surveymonkey.net\/v3\/users\/me"",""date_last_login"":""2016-09-26T16:23:40.397000+00:00"",""date_created"":""2006-06-28T12:48:00+00:00"",""id"":""123456789""}
            ");

            var api = new SurveyMonkeyApi("TestApiKey", "TestOAuthToken", client);
            var results = api.GetUserDetails();
            Assert.AreEqual("test@gmail.com", results.Username);
            Assert.IsNull(results.FirstName);
            Assert.AreEqual("McTest", results.LastName);
            Assert.AreEqual("gold", results.AccountType);
            Assert.AreEqual("en", results.Language);
            Assert.AreEqual("testemail@gmail.com", results.Email);
            Assert.AreEqual("https://api.surveymonkey.net/v3/users/me", results.Href);
            Assert.AreEqual(new DateTime(2006, 6, 28, 12, 48, 0, DateTimeKind.Utc), results.DateCreated);
            Assert.AreEqual(new DateTime(2016, 9, 26, 16, 23, 40, 397, DateTimeKind.Utc), results.DateLastLogin);
        }
    }
}