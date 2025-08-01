using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace IDP
{
    public static class TestUsers
    {
        public static List<TestUser> Users => new()
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "mahdi",
            Password = "1234",
            Claims = new[]
            {
                new Claim("name", "Mahdi Mojahedi"),
                new Claim("role", "admin")
            }
        }
    };
    }

}
