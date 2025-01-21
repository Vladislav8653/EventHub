

using Presentation;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureCors();
builder.Services.AddControllers();
builder.Services.ConfigureValidation();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureImageService(builder.Configuration);
builder.Services.ConfigureCookieService();
builder.Services.ConfigureUseCases();
builder.Services.ConfigureApiAuthentication(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureSwagger();
var app = builder.Build();
app.AppendMiddlewareErrorHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.ConfigureCookiesPolicy();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUi();
app.Run();