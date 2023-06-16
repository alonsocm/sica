// See https://aka.ms/new-console-template for more information
using Application;
using Application.Features.CargaMasivaEvidencias.Commands;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.Hosting;
using Persistence;
using Shared;
using System;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        await ProcesarArchivos(Configuration, args);
    }

    public static async Task ProcesarArchivos(IConfiguration configuration, string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddApplicationLayer();
                services.AddSharedInfraestructure(configuration);
                services.AddPersistenceInfrastructure(configuration, "Development");
                services.AddScoped<IRequestHandler<CargaEvidenciasCommand, Response<bool>>, CargaEvidenciasCommandHandler>();
                services.AddMediatR(typeof(CargaEvidenciasCommand).GetType().Assembly);
            }).Build();

        var carpetasAños = System.IO.Directory.GetDirectories(configuration["Archivos:EvidenciasMuestreo"]);
        List<IFormFile> archivosEvidencias = new();

        foreach (var carpeta in carpetasAños)
        {
            for (var i = 0; i <= 60; i++) Console.Write("-");
            Console.WriteLine($"\nNombre carpeta: {carpeta}");
            for (var i = 0; i <= 60; i++) Console.Write("-");
            Console.Write("\n");

            var archivosCarpeta = Directory.GetFiles(carpeta);

            archivosCarpeta.ToList().ForEach(archivo =>
            {
                var infoArchivo = new FileInfo(archivo);
                Console.WriteLine($"Archivo: {infoArchivo.Name}"); 
                using var stream = System.IO.File.OpenRead(archivo);
                var archivoEvidencia = new FormFile(stream, 0, stream.Length, string.Empty, stream.Name);
                archivosEvidencias.Add(archivoEvidencia);
            });
        }

        IServiceScope serviceScope = host.Services.CreateScope();
        IServiceProvider provider = serviceScope.ServiceProvider;

        try
        {
            var mediator = provider.GetRequiredService<IMediator>();
            var respuesta = await mediator.Send(new CargaEvidenciasCommand { Archivos=archivosEvidencias });
        }
        catch (Application.Exceptions.ValidationException ex)
        {
            foreach (var error in ex.Errors) Console.WriteLine(error);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally { Console.ReadKey(); }
    }
}