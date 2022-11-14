using System.Globalization;

namespace CalculatorAPI.Clients
{
    public class ClientV2
    {
        #region Variables

        private static readonly HttpClient _client = new HttpClient();
        private string[] _results;

        #endregion

        #region Public Methods

        public async void GetOperations()
        {
            Task callingTheApi = CallTheAPI();

            callingTheApi.Wait();

            WriteCSV();
        }

        #endregion

        #region Private Methods

        private async Task CallTheAPI()
        {
            string url = $"https://localhost:7088/api/controller/getOperationList";
            Uri uri = new Uri(url);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                PrepareResponse(responseBody);

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private void PrepareResponse(string responseBody)
        {
            string response = responseBody.Remove(0, 1);
            response = response.Remove(response.Length - 1, 1);
            _results = response.Split(',');
        }

        private void WriteCSV()
        {
            string rutaCompleta = @".\Results\results.csv";

            File.Delete(rutaCompleta);

            using (StreamWriter operations = File.AppendText(rutaCompleta))
            {
                for (int i = 0; i < _results.Length; i++)
                {
                    operations.WriteLine(_results[i]);
                }

                operations.Close();
            }
        }

        #endregion
    }
}
