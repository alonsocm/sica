using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using System.Linq.Expressions;

namespace Application.Expressions
{
    internal class MuestreoExpression
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public MuestreoExpression(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }
        public Expression<Func<MuestreoDto, bool>> GetExpression(Filter filter)
        {
            return filter.Conditional switch
            {
                #region Text
                "notequals" => _muestreoRepository.GetNotEqualsExpression(filter.Column, filter.Value),
                "beginswith" => _muestreoRepository.GetBeginsWithExpression(filter.Column, filter.Value),
                "notbeginswith" => _muestreoRepository.GetNotBeginsWithExpression(filter.Column, filter.Value),
                "endswith" => _muestreoRepository.GetEndsWithExpression(filter.Column, filter.Value),
                "notendswith" => _muestreoRepository.GetNotEndsWithExpression(filter.Column, filter.Value),
                "contains" => _muestreoRepository.GetContainsExpression(filter.Column, filter.Value),
                "notcontains" => _muestreoRepository.GetNotContainsExpression(filter.Column, filter.Value),
                #endregion
                #region Numeric
                "greaterthan" => _muestreoRepository.GetGreaterThanExpression(filter.Column, Convert.ToInt32(filter.Value)),
                "lessthan" => _muestreoRepository.GetLessThanExpression(filter.Column, Convert.ToInt32(filter.Value)),
                "greaterthanorequalto" => _muestreoRepository.GetGreaterThanOrEqualToExpression(filter.Column, Convert.ToInt32(filter.Value)),
                "lessthanorequalto" => _muestreoRepository.GetLessThanOrEqualToExpression(filter.Column, Convert.ToInt32(filter.Value)),
                #endregion
                #region Date
                "before" => _muestreoRepository.GetBeforeExpression(filter.Column, DateTime.Parse(filter.Value)),
                "after" => _muestreoRepository.GetAfterExpression(filter.Column, DateTime.Parse(filter.Value)),
                "beforeorequal" => _muestreoRepository.GetBeforeOrEqualExpression(filter.Column, DateTime.Parse(filter.Value)),
                "afterorequal" => _muestreoRepository.GetAfterOrEqualExpression(filter.Column, DateTime.Parse(filter.Value)),
                #endregion
                _ => muestreo => true,
            };
        }
    }
}
