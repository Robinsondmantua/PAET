$(document).ready(function () {
    var deleteLinkObj;
    // delete Link
    $('.glyphicon-trash').click(function () {
        BootstrapDialog.confirm('¿Desea borrar el elemento seleccionado?', function (result) {
            if (result) {
                alert('Elemento eliminado');
            }
        });
    });
});