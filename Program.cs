using CityInfo.API;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

//configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)//new log file crated daily
    .CreateLogger();
//Serilog configuration ^

var builder = WebApplication.CreateBuilder(args);//automatically creates a set of login providers 
// builder.Logging.ClearProviders();//resulting in nothing being logged anymore - also will not run while active 
// builder.Logging.AddConsole(); // Add services to the container.
//telling aps.netcore to use Serilog - Removing other builder ^ now 
builder.Host.UseSerilog();

// registers supporting controlers on the continers MVC
builder.Services.AddControllers(options => //adding controlers for services connection 
{
    options.ReturnHttpNotAcceptable = true; //returning a message stating not supported depending on json/xml, etc.(status code)
}).AddNewtonsoftJson()//replaces json input & output formatters with json.net
//^added microsoft.aspnetcore.netwtonsoft 
.AddXmlDataContractSerializerFormatters();//this adds support for xml 

// Learn more about configuring Swagger/OpenAPI at 
//https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();//builtin provider (static files), adding "addsingleton" to type through services 
//statement allows us to inject file extension content type provider 

//compiler directives
#if DEBUG//if debug adding local mail service 
//trnasient lightweight, instance of local mailservice is now injectable 
builder.Services.AddTransient<IMailService, LocalMailService>();
#else//else adding cloud mail service 
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

//register citiesdatastore on register container 
builder.Services.AddSingleton<CitiesDataStore>(); 

var app = builder.Build();
 
app.UseSwagger(); //always first 
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting(); //Routing Middle ware

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();//adding endpoints for controllers action without specifying routes 
});

app.Run();//a way of creating middle wear on the fly 
