using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class ValidarResultadosPorReglasCommand : IRequest<Response<bool>>
    {
        public List<int> Anios { get; set; } = new List<int>();
        public List<int> NumeroEntrega { get; set; } = new List<int>();
    }

    public class ValidarResultadosPorReglasCommandHandler : IRequestHandler<ValidarResultadosPorReglasCommand, Response<bool>>
    {
        private readonly IResultado _resultadosRepository;

        public ValidarResultadosPorReglasCommandHandler(IResultado resultadosRepository)
        {
            _resultadosRepository=resultadosRepository;
        }

        public Task<Response<bool>> Handle(ValidarResultadosPorReglasCommand request, CancellationToken cancellationToken)
        {
            /*Obtenemos la lista de resultados correspondientes a los muestreos que cumplan con la condición de año y número de entrega*/

            /*Recorremos la lista de resultados, y con el id de parámetro (o la clave) debemos ir al catálogo de reglas, para obtener las correspondientes*/

            /*Dentro del recorrido de los resultados, utilizamos la regla obtenida y el valor del resultado
             para llamar a la interfaz IRegla y pasamos los argumentos al método*/
            throw new NotImplementedException();
        }
    }
}
