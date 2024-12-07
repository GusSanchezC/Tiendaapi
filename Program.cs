var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var mySqlDatabase = new DBConn();
mySqlDatabase.TestConnection();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
