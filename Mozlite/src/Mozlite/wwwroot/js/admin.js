/// <reference path="../_references.js" />
$(function () {
    //list-body
    if ($('.scrolly').length > 0)
        window.onload = window.onresize = function () {
            $('.scrolly').height($(document).height() - $('.scrolly').offset().top);
        };
});