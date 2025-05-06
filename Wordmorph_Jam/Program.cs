using Microsoft.Extensions.FileProviders;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "WordMorphBackend", "static")),
    RequestPath = "/static"
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapPost("/api/generate-image", async (HttpContext context) =>
{
    using var client = new HttpClient();
    var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
    var jsonContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

    var response = await client.PostAsync("http://localhost:8000/generate-image", jsonContent);
    if (response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadAsStringAsync();
        return Results.Ok(JsonConvert.DeserializeObject<dynamic>(result));
    }

    return Results.BadRequest();
});

app.Run();
