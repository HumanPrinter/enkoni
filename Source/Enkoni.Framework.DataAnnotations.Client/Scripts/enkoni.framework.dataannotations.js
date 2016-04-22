jQuery.validator.addMethod("iban", function (value, element) {
    var newIban = value.toUpperCase(),
    modulo = function (divident, divisor) {
        var cDivident = '';
        var cRest = '';

        for (var i in divident) {
            var cChar = divident[i];
            var cOperator = cRest + '' + cDivident + '' + cChar;

            if (cOperator < parseInt(divisor)) {
                cDivident += '' + cChar;
            }
            else {
                cRest = cOperator % divisor;
                if (cRest == 0) {
                    cRest = '';
                }

                cDivident = '';
            }
        }

        cRest += '' + cDivident;
        if (cRest == '') {
            cRest = 0;
        }

        return cRest;
    };

    if (newIban.search(/^[A-Z]{2}/gi) < 0) {
        return false;
    }

    newIban = newIban.substring(4) + newIban.substring(0, 4);

    newIban = newIban.replace(/[A-Z]/g, function (match) {
        return match.charCodeAt(0) - 55;
    });

    return parseInt(modulo(newIban, 97), 10) === 1;
}, "Check your IBAN number!");

jQuery.validator.unobtrusive.adapters.addBool("iban");