var FILE_LIST_ID = '#fileList';
var FILE_INPUT_ID = '#addFileInput';
var BTN_UPLOAD_ID = '#btnUpload';
var FORM_FILES_ID = '#formFiles';
var URL_RESTART = '/Home/RestartUpload';
var URL_INFO_SOCIOS = '/InfoSocios/Index';
var URL_FILE_UPLOAD = "/Home/FileUpload";
var base_url = window.location.origin;

var filesPost = [];

var formFile = {
    remove: function (index) {
        filesPost.splice(index, 1);
        $(FILE_LIST_ID).empty();
        $.each(filesPost, function (index) {
            formFile.addFileListElem(index, filesPost[index]);
        });
        console.log(filesPost);
    },
    addFileListElem: function (index, value) {
        $(FILE_LIST_ID).append('<li class ="list-group-item" dataIndex=' + index + '>' + value.name + '<span> Eliminar</span></li>');
    },
    getInvalidFileNames: function (listOfFiles) {
        var namesInvalid = [];
        $.each(listOfFiles, function (index, value) {
            var fileName = value.name;
            var extension = fileName.substr((fileName.lastIndexOf('.') + 1));
            if (extension != 'csv') {
                namesInvalid.push(fileName);
            }
        });
        return namesInvalid;
    },
    changeBtnUpload: function () {
        var buttonState = $(BTN_UPLOAD_ID).prop('disabled');

        if (filesPost.length > 0 && buttonState == true) {
            $(BTN_UPLOAD_ID).prop('disabled', false);
        }
        else if (filesPost.length == 0 && buttonState == false) {
            $(BTN_UPLOAD_ID).prop('disabled', true);
        }
    },
    getErrorMessageAndRestart: function (message) {
        swal("Error", message, "error")
            .then(function () {
                window.location = base_url + URL_RESTART;
            });
    }
};

$(document).ready(function () {
    $(FILE_INPUT_ID).change(function (e) {
        //files = e.target.files[0]
        var files = $(FILE_INPUT_ID)[0].files
        var namesInvalid = formFile.getInvalidFileNames(files);
        var filesPostLength = filesPost.length;
        if (namesInvalid.length == 0) {
            $.each(files, function (index, value) {
                formFile.addFileListElem(index + filesPostLength, value);
                filesPost.push(value);
            });
        }
        else if (namesInvalid.length == 1) {
            swal("Advertencia", 'El archivo ' + namesInvalid[0] + ' debe tener extensión CSV', "warning");
        }
        else {
            swal("Advertencia", 'Los archivos ' + namesInvalid.join(',') + ' deben tener extensión CSV', "warning");
        }
        $(FILE_INPUT_ID).empty();
        formFile.changeBtnUpload();
    });

    $(this).on("click", "#fileList li span", function (e) {
        e.preventDefault();
        var index = $(this).parent().index();
        formFile.remove(index);
        formFile.changeBtnUpload();
    });

    $(BTN_UPLOAD_ID).click(function (){
        var form = $(FORM_FILES_ID);
        var filesData = new FormData(form.get(0));

        for (var i = 0; i < filesPost.length; i++) {
            filesData.append("filesData", filesPost[i]);
        }

        $.ajax({
            type: "POST",
            url: URL_FILE_UPLOAD,
            dataType: "json",
            contentType: false,
            processData: false,
            data: filesData,
            success: function (result, status, xhr) {
                if (xhr.readyState == 4) {
                    if (xhr.status == 204) {
                        swal("Advertencia", "No se han encontrado registros en el archivo, por favor revise los archivos .csv", "warning");
                    }
                    else if (xhr.status == 200) {
                        swal("Exito!", result.message, "success")
                            .then(function () {
                                window.location = "redirectURL";
                                window.location = base_url + URL_INFO_SOCIOS;
                        });
                    }
                    else {
                        formFile.getErrorMessageAndRestart("Ha ocurrido un error, por favor intente nuevamente");
                    }
                }
            },
            error: function (xhr, status, error) {
                if (xhr.readyState == 4) {
                    if (xhr.status == 400) {
                        formFile.getErrorMessageAndRestart(xhr.responseText);
                    }
                    else {
                        formFile.getErrorMessageAndRestart("Se ha producido un error al procesar el archivo, por favor verifique el formato de los datos");
                    }
                }
            }
        });
    });
});