﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyVet.web.Data;
using MyVet.web.Data.Entities;
using MyVet.web.Helpers;
using MyVet.Web.Data;
using MyVet.Web.Helpers;

namespace MyVet.web
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddIdentity<User, IdentityRole>(cfg =>
			{
				cfg.User.RequireUniqueEmail = true;
				cfg.Password.RequireDigit = false;
				cfg.Password.RequiredUniqueChars = 0;
				cfg.Password.RequireLowercase = false;
				cfg.Password.RequireNonAlphanumeric = false;
				cfg.Password.RequireUppercase = false;
			}).AddEntityFrameworkStores<DataContext>();

			services.AddDbContext<DataContext>(cfg =>
			{
				cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});

			//Add the new configuration for validate the tokens in Startup class:
			services.AddAuthentication()
				.AddCookie()
				.AddJwtBearer(cfg =>
				{
					cfg.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = Configuration["Tokens:Issuer"],
						ValidAudience = Configuration["Tokens:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
					};
				});

			services.AddTransient<SeedDb>();
			services.AddScoped<IUserHelper, UserHelper>();

			//agregar una conexión a la base de datos, aqui se configura para que motor de base de datos se quiere adaptar
			services.AddDbContext<DataContext>(cfg =>
			{
				cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});
			//AddTransient: inyecta el codigo una sola vez
			services.AddTransient<SeedDb>();
			//AddScoped: inyecta mas de una vez creando una nueva instancia
			services.AddScoped<IUserHelper, UserHelper>();
			services.AddScoped<ICombosHelper, CombosHelper>();
			services.AddScoped<IConverterHelper, ConverterHelper>();
			services.AddScoped<IImageHelper, ImageHelper>();
			//AddScoped: inyecta una vez pero lo deja permanentemente en la ejecuón de vida del proyecto
			//services.AddSingleton<UserHelper>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}