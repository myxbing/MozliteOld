/// <reference path="../_references.js" />
$(function () {
    if (jQuery.fn.iCheck)
        $('input[type=checkbox]').iCheck({
            checkboxClass: 'icheckbox_minimal-green',
            radioClass: 'iradio_minimal-green',
            increaseArea: '20%'
        });
});