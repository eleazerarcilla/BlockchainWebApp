$("#frmProductListUpload").submit(function (e) {
    e.preventDefault();
    var form = $(this);
    var url = form.attr('action');
    $.ajax(
        {
            type: "POST",
            url: url,
            dataType: "html",
            data: new FormData(this),
            processData: false,
            contentType: false,
            success: function (data) {
                $("#divResult").html(data);
            },
            error: function (jqXHR, exception) {
                var msg = '';
            }
        });
});

