using Application.DTOs;
using Application.DTOs.Users;
using Application.Features.ReplicaDiferente.Commands;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ReplicaDiferente.Commands
{
    public class CargaReplicaDiferenteCommandValidator : AbstractValidator<CargaReplicaDiferenteCommand>
    {
        private readonly IVwReplicaRevisionResultadoRepository _vwReplicaRevisionResultado;

        public CargaReplicaDiferenteCommandValidator(IVwReplicaRevisionResultadoRepository vwReplicaRevisionResultado)
        {
            _vwReplicaRevisionResultado = vwReplicaRevisionResultado;

            
            


            RuleForEach(x => x.Resultados).ChildRules(resultado =>
            {
                resultado.RuleFor(x => x.NoEntrega).Cascade(CascadeMode.Stop)
                                           .NotEmpty().WithMessage(resultado => $"El campo {{PropertyName}} no puede estar vacío. Linea: {resultado.Linea}")
                                           .Must((resultado, noEntrega) => { return ExisteClaveUnica(resultado.ClaveUnica,noEntrega).Result; })
                                           .WithMessage(resultado => $"Los datos cargados {{PropertyValue}} no se encontran en la BD. Linea:{resultado.Linea}");

                

            });
        }

        public async Task<bool> ExisteClaveUnica(string claveUnica, string noEntrega)
        {
            return await  _vwReplicaRevisionResultado.ExisteElementoAsync(x => x.NumeroEntrega == noEntrega && x.ClaveUnica == claveUnica);
        }
        
    }
}