using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using phone_book.Data;
using phone_book.Controllers;
using phone_book.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using phone_book.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//Declarate Services
builder.Services.AddScoped<ContactTypeService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ContactService>();


var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnectionString");
builder.Services.AddDbContext<PhoneBookDb>(options =>
            options.UseNpgsql(connectionString));


var app = builder.Build();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173");
                      });
});

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();*/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

//app.UseHttpsRedirection();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



//User Routes
app.MapControllerRoute(name: "Login",
                pattern: "user/login",
                defaults: new { controller = "User", action = "Login" });

app.MapControllerRoute(name: "PostUser",
                pattern: "user/post",
                defaults: new { controller = "User", action = "Post" });

app.MapControllerRoute(name: "GetUser",
                pattern: "user/get/{id}",
                defaults: new { controller = "User", action = "Get" });
app.MapControllerRoute(name: "UpdateUser",
                pattern: "user/update/{id}",
                defaults: new { controller = "User", action = "Update" });

//Contact Routes
app.MapControllerRoute(name: "PostContact",
                pattern: "contact/post",
                defaults: new { controller = "Contact", action = "Post" });

app.MapControllerRoute(name: "GetContact",
                pattern: "contact/get/{username}",
                defaults: new { controller = "Contact", action = "Get" });


app.MapControllerRoute(name: "DeleteContact",
                pattern: "contact/delete/{id}",
                defaults: new { controller = "Contact", action = "Delete" });

//ContactType Routes
app.MapControllerRoute(name: "PostContactType",
                pattern: "contacttype/post",
                defaults: new { controller = "ContactType", action = "Post" });

app.MapControllerRoute(name: "GetContactType",
                pattern: "contacttype/get/{id}",
                defaults: new { controller = "ContactType", action = "Get" });

app.MapControllerRoute(name: "GetAllContactTypes",
                pattern: "contacttype/getall",
                defaults: new { controller = "ContactType", action = "GetAll" });

app.MapControllerRoute(name: "DeleteContactType",
                pattern: "contacttype/delete/{id}",
                defaults: new { controller = "ContactType", action = "Delete" });

app.MapControllerRoute(name: "UpdateContactType",
                pattern: "contacttype/update/{id}",
                defaults: new { controller = "ContactType", action = "Update" });


app.Run();

