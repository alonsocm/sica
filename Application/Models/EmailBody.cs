using System.Text;

namespace Application.Models
{
    internal class EmailBody
    {
        public static string Revisor_A_Operador(string nombre, string cargo)
        {
            StringBuilder body = new();

            body.Append($"<body>");
            body.Append($"<p><b><span>Estimada Ing. Jenni Arce</span></b></p>");
            body.Append($"<p><b>Subgerente de operaciones CONALAB</b></p>");
            body.Append($"<p>Por indicación de la M. en C. Alicia Vázquez se envía la siguiente información:</p>");
            body.Append($"<p>En atención y seguimiento del contrato");
            body.Append($"CNA-GRM-024-2022 “SERVICIO PARA OBTENER DATOS DE CALIDAD DEL AGUA SUPERFICIAL, COSTERA Y SUBTERRÁNEA A NIVEL NACIONAL”,");
            body.Append($"me permito comunicarle los resultados de la supervisión de evidencias de muestreo en el sistema e Baseca,");
            body.Append($"con los muestreos aprobados y rechazados al 12 de diciembre de 2022.</p>");
            body.Append($"<ul><li>Se revisaron 10 evidencias</li>");
            body.Append($"<li>Se aceptaron 10 evidencias</li>");
            body.Append($"<li>Se aceptaron 10 evidencias</li>");
            body.Append($"<li>Se rechazaron 0 evidencias</li></ul>");
            body.Append($"<p>El listado de los muestreos aprobados se encuentra en el archivo anexo.</p> ");
            body.Append($"<p>Cualquier duda, quedo al pendiente.</p>");
            body.Append($"<p>Saludos cordiales,</p>");
            body.Append($"</body>");

            return body.ToString();
        }
    }
}
