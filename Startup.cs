
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
using store.ProductModule.Interface;
using store.ProductModule;
using store.ProductModule.DTO;

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

            // Auth Module
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<AuthGuard>();

            //User Module
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();

            // Product Module
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            //Validator  
            services.AddScoped<ValidateFilter>();
            services.AddScoped<LoginUserDtoValidator, LoginUserDtoValidator>();
            services.AddScoped<RegisterUserDtoValidator, RegisterUserDtoValidator>();
            services.AddScoped<UpdateUserDtoValidator, UpdateUserDtoValidator>();
            services.AddScoped<UpdateStatusUserDtoValidator, UpdateStatusUserDtoValidator>();
            services.AddScoped<AddCategoryDtoValidator, AddCategoryDtoValidator>();
            services.AddScoped<AddSubCategoryDtoValidator, AddSubCategoryDtoValidator>();
            services.AddScoped<AddProductDtoValidator, AddProductDtoValidator>();

            // Google
            services.AddAuthentication().AddGoogle(options =>
            {

            });
            services.AddCors(options =>
                     options.AddPolicy("AllowSpecific", p => p.WithOrigins("http://localhost:3000").AllowCredentials()
                                                               .WithMethods("GET").WithMethods("POST").WithMethods("PUT")
                                                               .WithHeaders("*")));
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
            app.UseCors("AllowSpecific");
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
