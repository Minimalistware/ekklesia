let POSTALCODEAPI = "https://viacep.com.br/ws";

$(document).ready(function () {
    $("#inputPostalCode").blur(searchPortalCode)
});

$("#inputPostalCode").blur(searchPortalCode)

function searchPortalCode() {
    let postal_code = $(this).val();
    $.get(POSTALCODEAPI + /postal_code/ + "json", fillUpFields(data))
};

function fillUpFields(postal_code) {
    $("inputAddress").val(postal_code.logradouro)
    $("inputCity").val(postal_code.localidade)
    $("inputUF").val(postal_code.uf)
}