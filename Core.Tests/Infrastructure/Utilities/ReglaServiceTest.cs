using Shared.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests.Infrastructure.Utilities
{
    public class ReglaServiceTest
    {
        [Fact]
        public void InCumpleRegla_MayorQue_RegresaTrue()
        {
            var reglaService = new ReglaService();

            bool incumple = reglaService.InCumpleReglaMinimoMaximo("> 15", "16");

            Assert.True(incumple);  
        }

        [Fact]
        public void InCumpleRegla_MayorQue_RegresaFalse()
        {
            var reglaService = new ReglaService();

            bool incumple = reglaService.InCumpleReglaMinimoMaximo("> 15", "15");

            Assert.False(incumple);
        }
    }
}
