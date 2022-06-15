using Api.Middleware;

namespace Api;

public static class SetupMiddlewarePipeline
{
    public static WebApplication SetupMiddleware(this WebApplication app)
    {
        // ******** Access the configuration ********
        var config = app.Configuration;
        
        // Configure the pipeline !! ORDER MATTERS !!
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllers();

        app.UseCustomExceptionHandler();

        app.UseCors("Open");

        return app;
    }
}