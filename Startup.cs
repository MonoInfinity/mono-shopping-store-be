
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;

using store.Src.UserModule;
using store.Src.UserModule.DTO;
using store.Src.UserModule.Interface;

using store.Src.Utils;
using store.Src.Utils.Locale;
using store.Src.Utils.Interface;

using FluentValidation;
using System.Globalization;

using store.Src.AuthModule;
using store.Src.AuthModule.Interface;
using store.Src.AuthModule.DTO;
using store.Src.Utils.Validator;
using Microsoft.AspNetCore.Http;
using store.Src.ProductModule.Interface;
using store.Src.ProductModule;
using store.Src.ProductModule.DTO;
using store.Src.Providers.Smail;
using store.Src.Providers.Smail.Interface;

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
            services.AddScoped<ISmailService, SmailService>();

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
            services.AddScoped<IImportInfoRepository, ImportInfoRepository>();
            services.AddScoped<IImportInfoService, ImportInfoService>();

            //Validator  
            services.AddScoped<ValidateFilter>();
            services.AddScoped<LoginUserDtoValidator>();
            services.AddScoped<RegisterUserDtoValidator>();
            services.AddScoped<UpdateUserDtoValidator>();
            services.AddScoped<UpdateEmployeeDtoValidator>();
            services.AddScoped<UpdateUserPasswordDtoValidator>();
            services.AddScoped<AddCategoryDtoValidator>();
            services.AddScoped<AddSubCategoryDtoValidator>();
            services.AddScoped<AddProductDtoValidator>();
            services.AddScoped<AddImportInfoDtoValidator>();
            services.AddScoped<UpdateProductDtoValidator>();
            services.AddScoped<DeleteProductDtoValidator>();
            services.AddScoped<UpdateCategoryDtoValidator>();
            services.AddScoped<UpdateSubCategoryDtoValidator>();

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
            // app.UseStaticFiles();
            // app.UseStaticFiles(new StaticFileOptions
            // {
            //     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "public")),
            //     RequestPath = "/public"
            // });

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
