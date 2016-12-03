/// <reference path="../_references.js" />
$(function () {
    //list-body
    window.onload = window.onresize = function () {
        $('.scrolly').height($(document).height() - $('.scrolly').offset().top);
    };

});