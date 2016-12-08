/// <reference path="../_references.js" />
; (function ($) {
    $(function () {
        //icheck
        $('input[type=checkbox]').iCheck({
            checkboxClass: 'icheckbox_minimal-green',
            radioClass: 'iradio_minimal-green',
            increaseArea: '20%'
        });
        var wrapper = $('.wrapper-main');
        if (wrapper.length <= 0) return;
        var buttons = wrapper.find('.toolbar').find('.btn[js-enabled]')
        .each(function () {
            var enabled = $(this).js('enabled');
            if (enabled === '1' || enabled === '1+')
                $(this).addClass('disabled');
        });
        var checkAll = wrapper.find('.list-header').find(':checkbox');
        var items = wrapper.find('.list-body').find(':checkbox');
        var checkedSize = 0;
        var size = items.length;
        //选中的值
        var checked = function () {
            var values = [];
            items.filter(':checked').each(function () {
                values.push($(this).val());
            });
            return values;
        };
        //选中的值附加到URL中：url[?&]id=checked();
        var checkedUrl = function (url) {
            var id = checked().join(',');
            if (id.length < 1) return url;
            if (url.indexOf('?') === -1)
                url += '?';
            else
                url += '&';
            return url + 'id=' + id;
        };
        checkAll.on('ifChecked ifUnchecked',
            function (e) {
                if (e.type === 'ifChecked') {
                    items.iCheck('check');
                } else {
                    items.iCheck('uncheck');
                }
            });
        items.on('ifChecked ifUnchecked',
            function (e) {
                if (e.type === 'ifChecked') {
                    checkedSize++;
                    if (checkedSize === size && !checkAll.prop('checked')) {
                        checkAll.attr('checked', 'checked').prop('checked', 'checked');
                        checkAll.iCheck('update');
                    }
                    $(this).parents('.list-view-item,.grid-view-item').addClass('active');
                } else {
                    checkedSize--;
                    if (checkedSize !== size && checkAll.prop('checked')) {
                        checkAll.removeAttr('checked').removeProp('checked');
                        checkAll.iCheck('update');
                    }
                    $(this).parents('.list-view-item,.grid-view-item').removeClass('active');
                }
                buttons.each(function () {
                    if (checkedSize > 0) {
                        var excluded = $(this).js('excluded');
                        if (excluded) {
                            var current = checked();
                            for (var i = 0; i < current.length; i++) {
                                if (excluded.indexOf(',' + current[i] + ',') !== -1) {
                                    $(this).addClass('disabled');
                                    return;
                                }
                            }
                        }
                    }
                    var enabled = $(this).js('enabled');
                    if (checkedSize > 1) {//选中多项
                        if (enabled === '1+')
                            $(this).removeClass('disabled');
                        else
                            $(this).addClass('disabled');
                    } else if (checkedSize === 1) {//选中1项
                        if (enabled === '1' || enabled === '1+')
                            $(this).removeClass('disabled');
                        else
                            $(this).addClass('disabled');
                    }
                    else
                        $(this).addClass('disabled');
                });
                if (checkedSize > 0) {
                    wrapper.find('.list-header').find('ul.list-cols').hide();
                    var text = wrapper.find('.list-header').find('.list-checked').show().find('.text');
                    text.html(text.js('text').replace('$size', checkedSize));
                    if (checkedSize === size) {
                        var checkbox = wrapper.find('.list-header').find('.list-checked').find(':checkbox');
                        checkbox.attr('checked', 'checked').prop('checked', 'checked');
                        checkbox.iCheck('update');
                    }
                } else {
                    wrapper.find('.list-header').find('ul.list-cols').show();
                    wrapper.find('.list-header').find('.list-checked').hide();
                }
            });

        $('.list-view-item,.grid-view-item')
            .click(function () {
                var checkbox = $(this).find(':checkbox');
                if (checkbox.is(':checked')) return;
                checkbox.iCheck('check');
            });

        $exec('js-mode',
            function (s, v) {
                var url = s.attr('href');
                s.on('click',
                    function (e) {
                        e.preventDefault();
                        var confirmStr = s.js('confirm');
                        var ignoreChecked = s.js('ignore') === 'checked';
                        if (confirmStr && !confirm(confirmStr))
                            return;
                        switch (v) {
                            case 'goto':
                                location.href = ignoreChecked ? url : checkedUrl(url);
                                break;
                            case 'modal':
                                s.jsModal(ignoreChecked ? url : checkedUrl(url));
                                break;
                            case 'action':
                                {
                                    var data = checked();
                                    if (data.length === 0) {
                                        $alert('请选择相关项目后再进行操作！');
                                        return;
                                    }

                                    $ajax(url, { ids: data.join(',') }, function (d) {
                                        var success = s.js('func');
                                        if (success)
                                            $call(success, d);
                                    });
                                }
                                break;
                        }
                    });
            },
            wrapper.find('.toolbar'));
    });
})(jQuery);