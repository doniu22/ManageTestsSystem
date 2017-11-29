$(document).ready(function () {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "positionClass": "toast-bottom-right",
        "onclick": null,
        "showDuration": "200",
        "hideDuration": "1500",
        "timeOut": "6000",
        "extendedTimeOut": "1200",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    displayToastr(msg , option);

});

function displayToastr(msg, option) {
    
    if (msg != " ") {
        if (option == "info")
        {
            toastr.info(msg)
        }
        else if (option == "warning")
        {
            toastr.warning(msg)
        }
        else if (option == "success")
        {
            toastr.success(msg)
        }
        else if (option == "error")
        {
            toastr.error(msg)
        }
    }
}