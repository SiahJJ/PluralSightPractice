var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// registers supporting controlers on the continers MVC
builder.Services.AddControllers(options => //adding controlers for services connection 
{
    options.ReturnHttpNotAcceptable = true; //returning a message stating not supported depending on json/xml, etc.(status code)
}).AddXmlDataContractSerializerFormatters();//this adds support for xml 

// Learn more about configuring Swagger/OpenAPI at 
//https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Jake & I added - Bad practice
app.Environment.EnvironmentName = "Development";

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()) //used so that the swagger devlopment middle ware is only added in dvlopment enviornemnt  
// {
//     app.UseSwagger(); //always first 
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseRouting(); //Routing Middle ware

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();//adding endpoints for controllers action without specifying routes 
});

app.Run();//a way of creating middle wear on the fly 
