function mayus(e) {
    e.value = e.value.toUpperCase();
    console.log('pruab validaciones');
}

function validarAlfanumerico(evento) {
    var pattern = /^([a-zA-ZÀ-ÿ-0-9_\u00f1\u00d1\s])$/;
    var inputChar = String.fromCharCode(evento.charCode);
    if (!pattern.test(inputChar)) { evento.preventDefault(); }
}

function validarCaracteres(evento) {
    var pattern = /^([a-zA-ZÀ-ÿ_\u00f1\u00d1\s])$/;
    var inputChar = String.fromCharCode(evento.charCode);
    if (!pattern.test(inputChar)) { evento.preventDefault(); }

}

// Valida que solo se ingresen números en el campo o un punto decimal
function validaNumerosPunto(e) {
    var pattern = /^\d*\.?\d*$/;
    var inputChar = String.fromCharCode(e.charCode);
    if (!pattern.test(inputChar)) { e.preventDefault(); }
}

function validarInputCURP(input, leyenda) {
    var curp = input.value.toUpperCase(),
        resultado = document.getElementById(leyenda),
        valido = "No válido";

    if (curpValida(curp)) {
        valido = "Válido";
        resultado.classList.add("ok");
    } else {
        resultado.classList.remove("ok");
    }

    resultado.innerText = "CURP: " + curp + "\nFormato: " + valido;
}

function curpValida(curp) {
    var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
        validado = curp.match(re);

    if (!validado) //Coincide con el formato general?
        return false;

    //Validar que coincida el dígito verificador
    function digitoVerificador(curp17) {
        //Fuente https://consultas.curp.gob.mx/CurpSP/
        var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
            lngSuma = 0.0,
            lngDigito = 0.0;
        for (var i = 0; i < 17; i++)
            lngSuma = lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
        lngDigito = 10 - lngSuma % 10;
        if (lngDigito == 10)
            return 0;
        return lngDigito;
    }
    if (validado[2] != digitoVerificador(validado[1]))
        return false;

    return true; //Validado
}


function soloNumeros(evento) {
    var pattern = /[0-9]/;
    var inputChar = String.fromCharCode(evento.charCode);

    if (!pattern.test(inputChar)) {
        // invalid character, prevent input
        evento.preventDefault();
    }
}

//Función para validar un RFC
function rfcValido(rfc) {
    var re = /^([ A-ZÑ&]?[A-ZÑ&]{3}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/,
        validado = rfc.match(re);

    if (!validado) //Coincide con el formato general?
        return false;

    //Separar el dígito verificador del resto del RFC
    var digitoVerificador = validado.pop(),
        rfcSinDigito = validado.slice(1).join('')

    //Obtener el digito esperado
    var diccionario = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ",
        lngSuma = 0.0,
        digitoEsperado;

    if (rfcSinDigito.length == 11) rfc = " " + rfc; //Ajustar a 12
    for (var i = 0; i < 13; i++)
        lngSuma = lngSuma + diccionario.indexOf(rfcSinDigito.charAt(i)) * (13 - i);
    digitoEsperado = 11 - lngSuma % 11;
    if (digitoEsperado == 11) digitoEsperado = 0;
    if (digitoEsperado == 10) digitoEsperado = "A";

    //El dígito verificador coincide con el esperado?
    return digitoVerificador == digitoEsperado;
}


//Handler para el evento cuando cambia el input
//Lleva la RFC a mayúsculas para validarlo
function validarInputRFC(input, leyenda) {
    var rfc = input.value.toUpperCase(),
        resultado = document.getElementById(leyenda),
        valido = "No válido";

    if (rfcValido(rfc)) { // ⬅️ Acá se comprueba
        valido = "Válido";
        resultado.classList.add("ok");
    } else {
        resultado.classList.remove("ok");
    }

    resultado.innerText = "RFC: " + rfc + "\nFormato: " + valido;
}