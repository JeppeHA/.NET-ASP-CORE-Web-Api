using OneReview.Persistence.Database;
using OneReview.Services;
using OneReview.DependencyInjection;
using OneReview.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddServices()
    .AddGlobalErrorHandling()
    .AddPersistence(builder.Configuration)
    .AddControllers();
}

var app = builder.Build();
{
<<<<<<< HEAD
    app.UseDeveloperExceptionPage(); 
    app.UseRouting();            
    app.UseAuthentication();
    app.UseAuthorization();
=======
    app.UseExceptionHandler();
>>>>>>> 0248a6e52eb2276db32b081b7f0bd4353c774b71
    app.MapControllers();
    app.InitializeDatabade();
}

app.Run();

   