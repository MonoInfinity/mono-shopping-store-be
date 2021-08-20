
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;

using store.UserModule;
using store.UserModule.DTO;
using store.UserModule.Interface;

using store.Utils;
using store.Utils.Locale;
using store.Utils.Interface;

using FluentValidation;
using System.Globalization;

using store.AuthModule;
using store.AuthModule.Interface;
using store.AuthModule.DTO;
using store.Utils.Validator;
using Microsoft.AspNetCore.Http;

namespace store
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Dependency Injection 
            services.AddScoped<IConfig, Config>();
            services.AddScoped<IDBHelper, DBHelper>();
            services.AddScoped<IRedis, Redis>();
            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<IJwtService, JwtService>();

            //User Module
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            // Auth Module

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<AuthGuard>();

            //Validator  
            services.AddScoped<ValidateFilter>();
            services.AddScoped<LoginUserDtoValidator>();
            services.AddScoped<RegisterUserDtoValidator>();
            services.AddScoped<UpdateUserDtoValidator>();
            services.AddScoped<UpdateUserPasswordDtoValidator>();


            // Google
            services.AddAuthentication().AddGoogle(options =>
            {

            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "store", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            ValidatorOptions.Global.LanguageManager = new CustomLanguageValidator();
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
            if (env.IsDevelopment())
            {
                app.UseStaticFiles();
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "docs")),
                    RequestPath = "/document"
                });
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/document/swagger/v1.json", "Store v1"));
            }

            app.Use(next => context =>
                        {
                            context.Request.EnableBuffering();
                            return next(context);
                        });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
