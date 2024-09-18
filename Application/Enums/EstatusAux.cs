namespace Application.Enums
{
    internal class EstatusAux
    {
        internal static EstatusMuestreo GetEstatus(int estatus)
        {
            bool isValid = Enum.IsDefined(typeof(EstatusMuestreo), estatus);

            if (isValid)
            {
                return (EstatusMuestreo)estatus;
            }
            else
            {
                throw new ArgumentException("Estatus muestreo no válido");
            }
        }
    }
}
