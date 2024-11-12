
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using GCB.Api.Services;

var builder = WebApplication.CreateBuilder(args);


// Agregar servicios CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        builder =>
//        {
//            builder.AllowAnyOrigin()
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});


// Registrar el DbContext
builder.Services.AddDbContext<GCBContext>((serviceProvider, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("gcbContext"));
});

// Configurar servicios
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

//Configurar OData
builder.Services.AddControllers().AddOData(
    options => options
    .Select()
    .Filter()
    .OrderBy()
    .Expand()
    .Count()
    .SetMaxTop(null).AddRouteComponents(
    routePrefix: "odata",
        model: GetEdmModel()));

// Configurar AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

// Usar CORS
//app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();

IEdmModel GetEdmModel()
{
    var modelBuilder = new ODataConventionModelBuilder();
    // Configuración para la entidad Bank
    modelBuilder.EntityType<Transaccion>();
    modelBuilder.EntitySet<Transaccion>("Transacciones");
    modelBuilder.ComplexType<TransaccionDto>();


    return modelBuilder.GetEdmModel();
}
