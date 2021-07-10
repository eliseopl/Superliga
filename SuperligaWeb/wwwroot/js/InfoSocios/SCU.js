var TABLE_SCU_ID = '#tableSCU';
var INDEX_EDAD = 1;

$(document).ready(function () {
    $(TABLE_SCU_ID).DataTable({
        "order": [[INDEX_EDAD, "asc"]],
        "language": languageOptions
    });
});