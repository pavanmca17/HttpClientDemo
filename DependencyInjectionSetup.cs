using HttpClientDemo.Model;
using HttpClientDemo.Swagger;
using HttpRequestFactory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace HttpClientDemo
{
    public static class DependencyInjectionSetup
    {
        public static void ConfigureValues(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<HttpClientSettings>(options =>
            {

                options.BaseUrl = Configuration.GetSection("HttpClientSettings:BaseUrl").Value;
               

            });
        }
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient(HttpClientConstants.httpclientname);          
            services.AddEndpointsApiExplorer();
            services.AddScoped<IHttpRequestFactoryService, HttpRequestFactoryService>();
            services.AddTransient<IHttpRequestFactoryService, HttpRequestFactoryService>();
            services.AddTransient<IHttpClientService,HttpClientService>();
        }
        public static void AddCorsSupport(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }
    
      
        public static void AddVersioningSupport(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));

            });

            services.AddSwaggerGen(o =>
            {
                o.OperationFilter<SwaggerDefaultValues>();
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        }
        private static void ConfigureSwaggerGenOptions(SwaggerGenOptions o)
        {
            AddSwaggerXmlComments(o);
            o.OperationFilter<SwaggerDefaultValues>();
        }
        private static void AddSwaggerXmlComments(SwaggerGenOptions o)
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        }
    }
}
