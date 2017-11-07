$(function () {
    // when the modal is closed
    $('#modal-container').on('hidden.bs.modal', function () {
        // remove the bs.modal data attribute from it
        $(this).removeData('bs.modal');
        // and empty the modal-content element
        $('#modal-container .modal-content').empty();
    });
});