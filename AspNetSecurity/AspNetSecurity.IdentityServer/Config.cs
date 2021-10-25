using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace AspNetSecurity.IdentityServer
{
    public class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Manish",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "Bob",
                    Password = "password"
                }
            };
        }
        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("AspNetSecurityApi", "Customer api for aspNetSecurity")
            };
        }
       
        public static IEnumerable<Client> GetClients()
        {
            // Client-Credential based grant type
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "AspNetSecurityApi" }


                },
                 //Resource Owner Password Grant Type
                new Client()
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                     AllowedScopes = { "AspNetSecurityApi" }
                },
                
            };


        }


    }
} 
      

   

