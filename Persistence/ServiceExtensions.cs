using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Persistence.Contexts;
using Persistence.Repository;
using System.Text;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration, string environment)
        {
            services.AddDbContext<SicaContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("DbConnection"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 5);
            }));

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("Usted no está autorizado"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("Usted no tiene permisos sobre este recurso"));
                        return context.Response.WriteAsync(result);
                    }
                };
            });

            #region Repositories
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            services.AddTransient<IMuestreoRepository, MuestreoRepository>();
            services.AddTransient<IParametroRepository, ParametroRepository>();
            services.AddTransient<ISitioRepository, SitioRepository>();
            services.AddTransient<IResumenResRepository, ResumenResMuestreosRepository>();
            services.AddTransient<IResultado, ResultadoRepository>();
            services.AddTransient<IVwClaveMonitoreo, VwClaveMuestreoRepository>();
            services.AddTransient<IEvidenciaMuestreoRepository, EvidenciaMuestreoRepository>();
            services.AddTransient<ITipoEvidenciaMuestreoRepository, TipoEvidenciaMuestreoRepository>();
            services.AddTransient<IReplicas, Replicas>();
            services.AddTransient<IVwReplicaRevisionResultadoRepository, VwReplicaRevisionResultadoRepository>();
            services.AddTransient<IEvidenciaReplicaRepository, EvidenciaReplicaRepository>();
            services.AddTransient<IReglasMinimoMaximoRepository, ReglasMinimoMaximoRepository>();
            services.AddTransient<IReglasReporteRepository, ReglasReporteRepository>();
            services.AddTransient<IFormaReporteEspecificaRepository, FormaReporteEspecificaRepository>();
            services.AddTransient<IReglasLaboratorioLDMRepository, ReglasLaboratorioLDMRepository>();
            services.AddTransient<ILaboratorioRepository, LaboratorioRepository>();
            services.AddTransient<ILimiteParametroLaboratorioRepository, LimiteParametroLaboratorioRepository>();
            services.AddTransient<IVwLimiteMaximoComunRepository, VwLimiteMaximoComunRepository>();
            services.AddTransient<IProgramaAnioRepository, ProgramaAnioRepository>();
            services.AddTransient<IVwLimiteLaboratorioRepository, VwLimiteLaboratorioRepository>();
            services.AddTransient<IHistorialSusticionLimiteRepository, HistorialSustitucionLimitesRepository>();
            services.AddTransient<IMuestreoEmergenciasRepository, MuestreoEmergenciasRepository>();
            #endregion
        }
    }
}
