using Microsoft.OpenApi.Models;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseHttpsRedirection();*/

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

// Add services to the container.
void ConfigureServices(IServiceCollection services)
{
    // Add CORS policy
    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });

    // Controller support
    services.AddControllers().ConfigureApiBehaviorOptions(x => { x.SuppressMapClientErrors = true; });
    services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

    // Swagger
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
        // Add header documentation in swagger
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Performance Assessment API",
            Description = "An assessment management API for measuring and improving performance.",
        });

        // Feed generated xml api docs to swagger
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    // Configure Automapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Our services, interface, or DB Contexts that we want to inject
    services.AddTransient<DapperContext>();
    services.AddScoped<IAssessmentRepository, AssessmentRepository>();
    services.AddScoped<IAssessmentService, AssessmentService>();
    services.AddScoped<IItemRepository, ItemRepository>();
    services.AddScoped<IItemService, ItemService>();
    services.AddScoped<IChoiceRepository, ChoiceRepository>();
    services.AddScoped<IChoiceService, ChoiceService>();
    services.AddScoped<IAnswerRepository, AnswerRepository>();
    services.AddScoped<IAnswerService, AnswerService>();
}