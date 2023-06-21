using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// registers supporting controlers on the continers MVC
builder.Services.AddControllers(options => //adding controlers for services connection 
{
    options.ReturnHttpNotAcceptable = true; //returning a message stating not supported depending on json/xml, etc.(status code)
}).AddNewtonsoftJson()//replaces json input & output formatters with json.net
.AddXmlDataContractSerializerFormatters();//this adds support for xml 

// Learn more about configuring Swagger/OpenAPI at 
//https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();//builtin provider (static files), adding "addsingleton" to type through services 
    //statement allows us to inject file extension content type provider 
var app = builder.Build();

//Jake & I added - Bad practice
//app.Environment.EnvironmentName = "Development";

//Configure the HTTP request pipeline.
 
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
