var builder = WebApplication.CreateBuilder(args);

int hostPort = Int16.Parse(builder.Configuration["Host:Port"]);
builder.WebHost.UseKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Any, hostPort);

});

string strRetVal = "meliappback-firebase-adminsdk-1bc42-103fa09fc3.json";
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", strRetVal);


// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IMessageService, MessageService>();



var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();


app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("swagger");
    });
});

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

