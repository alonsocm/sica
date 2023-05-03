using Application.Interfaces;
using DynamicExpresso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Services
{
    public class ReglaService : IReglaService
    {
        public bool InCumpleReglaMinimoMaximo(string regla = "> 3", string valorParametro = "3")
        {            
            var target = new Interpreter();
            bool incumple = target.Eval<bool>($"{valorParametro}{regla}");
            return incumple;
        }
    }
}
