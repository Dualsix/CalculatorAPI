using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CalculatorAPI.Clients
{
    public class ClientV1
    {
        #region Variables

        private static readonly string _samplePath = ".\\Sample\\sample.csv";
        private static readonly HttpClient _client = new HttpClient();

        #endregion

        #region Public Methods

        public async void ReadCSV()
        {
            StreamReader csvFile = new StreamReader(_samplePath);

            while (!csvFile.EndOfStream)
            {
                string line = csvFile.ReadLine();
                string[] values = line.Split(',');

                PrepareTheOperationValues(values);

                Task callingTheApi = CallTheAPI(values);

                callingTheApi.Wait();
            }
        }


        #endregion

        private void PrepareTheOperationValues(string[] values)
        {
            if (values[1] == "+") values[1] = "add";
            else if (values[1] == "-") values[1] = "substract";
            else if (values[1] == "*") values[1] = "multiply";
            else if (values[1] == "/") values[1] = "division";
        }

        private static async Task CallTheAPI(string[] values)
        {

            string url = $"https://localhost:7088/api/controller/calculate?value1={values[0]}&operation={values[1]}&value2={values[2]}";
            Uri uri = new Uri(url);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
