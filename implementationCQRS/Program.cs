// Command the Employee Domain to save data
using implementationCQRS.Command;
using implementationCQRS.Models;
using implementationCQRS.Queries;
using implementationCQRS.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using System.Data.Common;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
#region ConnectionStrings
var connectionString = builder.Configuration.GetConnectionString("RozaneSQL");
var contectionStrings = builder.Configuration.GetConnectionString("SqlServer");
//builder.Services.AddDbContext<ApplicationDbContext>(op =>
//                                                    op.UseSqlServer(connectionString));
#endregion

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto
});
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx => {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
          "Origin, X-Requested-With, Content-Type, Accept");
    },

});
app.UseCors("MyCorsPolicy");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Employee}/{action=Test}/{id?}");
});

app.Run();

