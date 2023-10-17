using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PerformanceAssessmentApi.Context;
using PerformanceAssessmentApi.Repositories;
using PerformanceAssessmentApi.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Initialize the configuration object
IConfiguration configuration = builder.Configuration;

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

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard();

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

    //Hangfire
    string connectionString = builder.Configuration.GetConnectionString("SqlServer");
    services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
    services.AddHangfireServer();

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

        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();

        // Feed generated xml api docs to swagger
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    // Configure Automapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddSingleton<IConfiguration>(builder.Configuration);
    services.AddHttpContextAccessor();

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

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ITeamRepository, TeamRepository>();
    services.AddScoped<ITeamService, TeamService>();
    services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    services.AddScoped<IEmployeeService, EmployeeService>();
    services.AddScoped<IAssignSchedulerRepository, AssignSchedulerRepository>();
    services.AddScoped<IAssignSchedulerService, AssignSchedulerService>();
    services.AddScoped<IResultRepository, ResultRepository>();
    services.AddScoped<IResultService, ResultService>();
    services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
    services.AddScoped<IAnnouncementService, AnnouncementService>();
    services.AddScoped<IEmployeeNotificationRepository, EmployeeNotificationRepository>();
    services.AddScoped<IEmployeeNotificationService, EmployeeNotificationService>();
    services.AddScoped<IAdminNotificationRepository, AdminNotificationRepository>();
    services.AddScoped<IAdminNotificationService, AdminNotificationService>();

    services.AddScoped<ITokenService, TokenService>();
}