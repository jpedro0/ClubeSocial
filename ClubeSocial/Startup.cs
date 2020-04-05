using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubeSocial.Context;
using ClubeSocial.Models;
using ClubeSocial.Repository;
using ClubeSocial.Repository.Intefaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClubeSocial
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
            var mvcviews  = services.AddControllersWithViews();
            #if (DEBUG)
            mvcviews.AddRazorRuntimeCompilation();  
            #endif
            services.AddDbContext<ClubeDBContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<ClubeDBContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Conta/Login/");
                options.LogoutPath = new PathString("/Conta/Logout/");
                options.AccessDeniedPath = new PathString("/Home/Error/");
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Socio Andamento", policy => policy.RequireClaim("socio", "andamento"));
                options.AddPolicy("Associar Dependente", policy => policy.RequireClaim("socio", "dependentes"));
            });

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<ICandidatoRepository, CandidatoRepository>();
            services.AddScoped<IClubeRepository, ClubeRepository>();
            services.AddScoped<ISocioRepository, SocioRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IDependenteRepository, DependenteRepository>();
            services.AddScoped<ICartaoRepository, CartaoRepository>();
            services.AddScoped<IMensalidadeRepository, MensalidadeRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            MenuClubeSocial(serviceProvider).Wait();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Conta}/{action=Login}/{id?}");
            });
        }

        public async Task MenuClubeSocial(IServiceProvider serviceProvider) 
        {
            using (var clubeRepository = serviceProvider.GetRequiredService<IClubeRepository>())
            using (var funcionarioRepository = serviceProvider.GetRequiredService<IFuncionarioRepository>())
            using (var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>())
            using (var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
            {
                await CriarRoles(roleManager);
                await CriarAdministradorClube(clubeRepository, userManager);
                await CriarFuncionarioClube(funcionarioRepository, userManager);
            }
        }

        public async Task CriarRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.ToList();

            if (!roles.Any())
            {
                string[] rolesNames = { "Candidato", "Socio", "Clube", "Funcionario" };
                foreach (var namesRole in rolesNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(namesRole);
                    if (!roleExist)
                    {
                        var role = new IdentityRole(namesRole);
                        await roleManager.CreateAsync(role);
                    }
                }
            }
        }

        public async Task CriarAdministradorClube(IClubeRepository clubeRepository, UserManager<IdentityUser> userManager)
        {
            var userexists = await userManager.FindByEmailAsync("clube@social.com");

            if (userexists == null)
            {
                Clube clube = new Clube
                {
                    Email = "clube@social.com",
                    Decricao = "Clube Social TOP",
                    Nome = "ClubeSocial",
                    DataCadastro = DateTime.Now
                };

                IdentityUser identityUser = new IdentityUser
                {
                    UserName = clube.Nome,
                    Email = clube.Email
                };

                clubeRepository.Add(clube);
                var resultregisteraccount = userManager.CreateAsync(identityUser, "123456").Result;
                if (resultregisteraccount.Succeeded)
                {
                    await userManager.AddToRoleAsync(identityUser, "Clube");
                }
            }
        }

        public async Task CriarFuncionarioClube(IFuncionarioRepository funcionarioRepository, UserManager<IdentityUser> userManager)
        {
            var userexists = await userManager.FindByEmailAsync("funcionario@clubesocial.com");

            if (userexists == null)
            {
                Funcionario funcionario = new Funcionario
                {
                    Email = "funcionario@clubesocial.com",
                    DataNacimento = DateTime.Now,
                    Nome = "Funcionario",
                    Pelido = "Funcionario Teste"
                };

                IdentityUser identityUser = new IdentityUser
                {
                    UserName = funcionario.Nome,
                    Email = funcionario.Email
                };

                funcionarioRepository.Add(funcionario);
                var resultregisteraccount = userManager.CreateAsync(identityUser, "123456").Result;
                if (resultregisteraccount.Succeeded)
                {
                    await userManager.AddToRoleAsync(identityUser, "Funcionario");
                }
            }
        }
    }
}
