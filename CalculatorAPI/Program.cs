using CalculatorAPI.Clients;

namespace CalculatorAPI
{

    public class Program {

        #region Public Methods

        public static void Main(string[] args)
        {
            BuildTheApi(args);

            Client1Behaviour();

            Client2Behaviour();
        }

        #endregion

        #region Private Methods

        private static void BuildTheApi(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.RunAsync();
        }

        private static void Client1Behaviour()
        {
            ClientV1 clientV1 = new ClientV1();
            clientV1.ReadCSV();
        }

        private static void Client2Behaviour()
        {
            ClientV2 clientV2 = new ClientV2();
            clientV2.GetOperations();
        }

        #endregion
    }
}



