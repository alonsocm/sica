﻿using Application.DTOs;
using Application.Interfaces;
using DynamicExpresso;

namespace Shared.Utilities.Services
{
    public class ReglaService : IReglaService
    {
        public bool CumpleLimitesDeteccion(LimiteDeteccionDto limites, decimal valorParametro)
        {            
            var target = new Interpreter();

            bool incumpleMinimo = false;
            bool incumpleMaximo = false;

            if (limites.Minimo != null)
                incumpleMinimo = target.Eval<bool>($"{valorParametro}<{limites.Minimo}");

            if (limites.Maximo != null)
                incumpleMaximo = target.Eval<bool>($"{valorParametro}>{limites.Maximo}");

            if (incumpleMinimo || incumpleMaximo)
                return false;

            return true;
        }

        public bool InCumpleReglaMinimoMaximo(string regla = "> 3", string valorParametro = "3")
        {            
            var target = new Interpreter();
            bool incumple = target.Eval<bool>($"{valorParametro}{regla}");
            return incumple;
        }
    }
}
