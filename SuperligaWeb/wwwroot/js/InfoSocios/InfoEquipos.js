var TABLE_EQUIPOS_ID = '#tableEquipos';
var INDEX_CANT_HINCHAS = 0;

$(document).ready(function () {
    var tableEquipos = $(TABLE_EQUIPOS_ID).DataTable({
        "language": languageOptions,
        buttons: [
            {
                text: 'Ordenar por cantidad de hinchas',
                className: 'btnOrderHinchas btn btn-primary',
                action: function (e, dt, node, config) {
                    var order = dt.order();
                    if (order[0][0] == INDEX_CANT_HINCHAS && order[0][1] == 'asc') {
                        dt.order([INDEX_CANT_HINCHAS, 'desc']).draw();
                    }
                    else {
                        dt.order([INDEX_CANT_HINCHAS, 'asc']).draw();
                    }
                }
            }
        ]
    });

    tableEquipos.buttons().container().insertAfter(TABLE_EQUIPOS_ID.concat('_filter'));
    tableEquipos.buttons(0)
});