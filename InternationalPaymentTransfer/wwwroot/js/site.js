function ShowNotification(message, type, autoHide = true) {
    $.notify(
        message,
        {
            position: "top center",
            className: type,
            close: true,
            autoHideDelay: 2000,
            autoHide: autoHide,
        },

    );
}
function openDialog(options) {
    $("#exampleModal #confirmButton").off("click")
    var { isAjax = false, title= null, content = null, onConfirm, onAjaxFailure, url = null } = options;
    $("#exampleModal").modal('show');
    
    if(!isAjax) {
        $("#exampleModal .modal-body").html(content);
        $("#exampleModal #confirmButton").on("click", onConfirm);
    }
    else {
        $.ajax({
            url: url,
            success: function(htmlResponse) {
                $("#exampleModal .modal-body").html(htmlResponse);
                $("#exampleModal #confirmButton").on("click", onConfirm);
            },
            error: function() {
                onAjaxFailure();
            }
        })
    }
    $("#exampleModal #exampleModalLabel").html(title);
}

function closeDialog() {
    $("#exampleModal").modal('hide');
    $("#exampleModal .modal-body").html("");
    $("#exampleModal #exampleModalLabel").html("");
    $("#exampleModal #confirmButton").click(null);
}
