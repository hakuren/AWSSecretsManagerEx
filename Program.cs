using System;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Threading.Tasks;

namespace AWSSecretsManagerEx
{
    public class Program
    {
        static readonly string region = "eu-west-1";
        static readonly IAmazonSecretsManager client = new AmazonSecretsManagerClient("", "", RegionEndpoint.GetBySystemName(region));

        static async Task<string> GetSecret(string secretName)
        {
            GetSecretValueRequest request = new() { SecretId = secretName, VersionStage = "AWSCURRENT" };

            try
            {
                GetSecretValueResponse response = await client.GetSecretValueAsync(request);
                return response.SecretString;
            }
            catch (Exception)
            {
                // For a list of the exceptions thrown, see
                // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
                throw;
            }
        }

        static async Task<string> SetSecret(string secretName, string secretValue)
        {
            CreateSecretRequest createSecretRequest = new() { Name = secretName, SecretString = secretValue };

            try
            {
                CreateSecretResponse response = await client.CreateSecretAsync(createSecretRequest);
                return response.HttpStatusCode.ToString();
            }
            catch (Exception)
            {
                // For a list of the exceptions thrown, see
                // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
                throw;
            }
        }

        public static void Main()
        {
            Console.WriteLine("AWS Secrets Manager");
            Console.WriteLine(GetSecret("serial4").Result);
            Console.WriteLine(SetSecret(Guid.NewGuid().ToString(), "fixe").Result);
        }
    }
}