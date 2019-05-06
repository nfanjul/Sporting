using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Sporting.Api.Extensions;
using Sporting.Api.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sporting.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddApiVersioning(
                options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                }
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SPORTING API",
                    Description = "API rojiblancosite",
                    TermsOfService = "#PuxaSporting",
                    License = new License
                    {
                        Name = "MIT",
                        Url = "https://es.wikipedia.org/wiki/Licencia_MIT"
                    },
                    Contact = new Contact
                    {
                        Name = "Nacho Fanjul",
                        Email = "nfanjul@plainconcepts.com",
                        Url = "https://rojiblancosite.wordpress.com"
                    }
                });

                c.SwaggerDoc("v2", new Info
                {
                    Version = "v2",
                    Title = "SPORTING API",
                    Description = "API rojiblancosite",
                    TermsOfService = "#PuxaSporting",
                    License = new License
                    {
                        Name = "MIT",
                        Url = "https://es.wikipedia.org/wiki/Licencia_MIT"
                    },
                    Contact = new Contact
                    {
                        Name = "Nacho Fanjul",
                        Email = "nfanjul@plainconcepts.com",
                        Url = "https://rojiblancosite.wordpress.com"
                    }
                });

                c.DocInclusionPredicate((docName, apiDesc) => GetPredicate(docName, apiDesc));

                c.OperationFilter<RemoveVersionParameterFilter>();
                c.DocumentFilter<SetVersionInPathFilter>();

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)) + ".xml";
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool GetPredicate(string docName, ApiDescription apiDescription)
        {
            var actionApiVersionModel = apiDescription.ActionDescriptor?.GetApiVersion();
            if (actionApiVersionModel == null)
            {
                return true;
            }
            if (actionApiVersionModel.DeclaredApiVersions.Any())
            {
                return actionApiVersionModel.DeclaredApiVersions.Any(version => $"v{version.ToString()}".Equals(docName));
            }
            return actionApiVersionModel.ImplementedApiVersions.Any(version => $"v{version.ToString()}".Equals(docName));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SPORTING API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "SPORTING API V2");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
