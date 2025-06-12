using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDbContext>(
    opt => opt.UseSqlServer(@"
        Server=localhost;
        Database=sql1;
        User Id=sa;
        Password=StrongP@ssword1;
        TrustServerCertificate=True;
    ")
);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.UseHttpsRedirection(); // Might cause a conflict. Dunno enough about it so will leave in

app.Run();

