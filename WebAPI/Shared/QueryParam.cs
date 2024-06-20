﻿using Application.Wrappers;

namespace WebAPI.Shared
{
    public static class QueryParam
    {
        public static List<Filter> GetFilters(string filterString)
        {
            var filters = new List<Filter>();

            if (!string.IsNullOrEmpty(filterString))
            {
                var arguments = filterString.Split("%");//Primero separamos el filtro, por columna                

                foreach (var argument in arguments)
                {
                    var isParameter = false;
                    string column = string.Empty;
                    string conditional = string.Empty;
                    string value = string.Empty;
                    List<string> values = new();

                    if (argument.Contains('[') && argument.Contains(']'))
                    {
                        isParameter = true;

                        if (argument.Contains('$'))//Con esto identificamos si se trata de un filtro especial
                        {
                            var splitedArgument = argument.Replace("[", string.Empty).Replace("]", string.Empty).Split('$');
                            column = splitedArgument[0];
                            conditional = splitedArgument[1];
                            value = splitedArgument[2];
                        }
                        else
                        {
                            var splitedArgument = argument.Replace("[", string.Empty).Replace("]", string.Empty).Split('*');

                            if (splitedArgument.Length >= 2)
                            {
                                column = splitedArgument[0];

                                foreach (var item in splitedArgument.Skip(1))
                                {
                                    values.Add(item);
                                }
                            }
                        }
                    }
                    else if (argument.Contains('*'))//Buscamos filtro con opciones de texto o número
                    {
                        var splitedArgument = argument.Split("_*");
                        column = splitedArgument[0];
                        conditional = splitedArgument[1][..splitedArgument[1].IndexOf('_')];//Con esto obtenemos el condicional. Ejemplo: mayor que, menor que
                        value = splitedArgument[1][(splitedArgument[1].LastIndexOf('_') + 1)..];

                        if (!Filter.IsValidFilter(conditional, value))
                        {
                            throw new Exception($"No fue posible procesar el filtro con el condicional: {conditional} con el valor {value}");
                        }
                    }
                    else
                    {
                        var splitedArgument = argument.Split('_');

                        if (splitedArgument.Length >= 2)
                        {
                            column = argument.Split('_')[0];

                            foreach (var item in argument.Split('_').Skip(1))
                            {
                                values.Add(item);
                            }
                        }
                    }

                    filters.Add(new Filter { Column = column, Conditional = conditional, Value= value, Values = values, IsParameter = isParameter });
                }
            }

            return filters;
        }
    }
}
