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
        var checked = 0;
        var size = items.length;
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
                    checked++;
                    if (checked === size && !checkAll.prop('checked')) {
                        checkAll.attr('checked', 'checked').prop('checked', 'checked');
                        checkAll.iCheck('update');
                    }
                    $(this).parents('.list-view-item').addClass('active');
                } else {
                    checked--;
                    if (checked !== size && checkAll.prop('checked')) {
                        checkAll.removeAttr('checked').removeProp('checked');
                        checkAll.iCheck('update');
                    }
                    $(this).parents('.list-view-item').removeClass('active');
                }
                buttons.each(function () {
                    var enabled = $(this).js('enabled');
                    if (checked > 1) {//选中多项
                        if (enabled === '1+')
                            $(this).removeClass('disabled');
                        else
                            $(this).addClass('disabled');
                    } else if (checked === 1) {//选中1项
                        if (enabled === '1' || enabled === '1+')
                            $(this).removeClass('disabled');
                        else
                            $(this).addClass('disabled');
                    }
                    else
                        $(this).addClass('disabled');
                });
                if (checked > 0) {
                    wrapper.find('.list-header').find('ul.list-cols').hide();
                    var text = wrapper.find('.list-header').find('.list-checked').show().find('.text');
                    text.html(text.js('text').replace('$size', checked));
                    if (checked === size) {
                        var checkbox = wrapper.find('.list-header').find('.list-checked').find(':checkbox');
                        checkbox.attr('checked', 'checked').prop('checked', 'checked');
                        checkbox.iCheck('update');
                    }
                } else {
                    wrapper.find('.list-header').find('ul.list-cols').show();
                    wrapper.find('.list-header').find('.list-checked').hide();
                }
            });

        $('.list-view-item')
            .click(function() {
                var checkbox = $(this).find(':checkbox');
                if (checkbox.is(':checked')) return;
                checkbox.iCheck('check');
            });

        window.checkeds = function () {
            var values = [];
            items.filter(':checked').each(function () {
                values.push($(this).val());
            });
            return values;
        };
        //$exec('.wrapper-main', function (selector, _) {
        //    var hidden = selector.find('tbody .fancy-checkbox').length === 0;
        //    var $all = selector.find('tbody .fancy-checkbox').find(':checkbox').length;
        //    var $checked = 0;

        //    function enabled(cur, visible) {
        //        cur = $(cur);
        //        if (hidden) {//模型不存在，隐藏所有操作按钮
        //            cur.hide();
        //        }
        //        else if (visible) {
        //            cur.find('.btn').removeClass('disabled').removeAttr('disabled');
        //        } else {
        //            cur.find('.btn').addClass('disabled').attr('disabled', 'disabled');
        //        }
        //    };

        //    function toggleMenu() {
        //        selector.find('.top').find('[_visible]').each(function () {
        //            var count = $.trim($(this).attr('_visible'));
        //            var action = 0;
        //            if (count[0] === '-')
        //                action = -1;
        //            else if (count[0] === '+')
        //                action = 1;

        //            if (count.length === 0)
        //                count = 0;
        //            else if (count === 'max')
        //                count = $all;
        //            count = parseInt(count);
        //            if (isNaN(count))
        //                count = 0;

        //            switch (action) {
        //                case 1:
        //                    enabled(this, $checked >= count);
        //                    break;
        //                case 0:
        //                    enabled(this, $checked === count);
        //                    break;
        //                default:
        //                    enabled(this, $checked <= count);
        //                    break;
        //            }
        //        });
        //        if ($checked === $all) {
        //            selector.find('.fancy-checkbox-all').find(':checkbox').prop('checked', true);
        //            selector.find('.top-menu li.check-all').addClass('hide');
        //            selector.find('.top-menu li.uncheck-all').removeClass('hide');
        //        } else {
        //            selector.find('.fancy-checkbox-all').find(':checkbox').prop('checked', false);
        //            selector.find('.top-menu li.check-all').removeClass('hide');
        //            selector.find('.top-menu li.uncheck-all').addClass('hide');
        //        }
        //    };

        //    toggleMenu();

        //    selector.find('.fancy-checkbox-all').change(function () {
        //        if ($(this).find(':checkbox').is(':checked')) {
        //            selector.find('tbody .fancy-checkbox').find(':checkbox').prop('checked', true);
        //            selector.find('tbody tr').addClass('highlighted');
        //            $checked = $all;
        //        } else {
        //            selector.find('tbody .fancy-checkbox').find(':checkbox').prop('checked', false);
        //            selector.find('tbody tr').removeClass('highlighted');
        //            $checked = 0;
        //        }
        //        toggleMenu();
        //    });

        //    selector.find('tbody .fancy-checkbox').change(function () {
        //        if ($(this).find(':checkbox').is(':checked')) {
        //            $(this).parents('tr').addClass('highlighted');
        //            $checked++;
        //        } else {
        //            $(this).parents('tr').removeClass('highlighted');
        //            $checked--;
        //        }
        //        toggleMenu();
        //    });

        //    selector.find('.top-menu .check-all, .top-menu .uncheck-all').click(function () {
        //        if ($(this).hasClass('check-all')) {
        //            selector.find('tbody .fancy-checkbox').find(':checkbox').prop('checked', true);
        //            selector.find('tbody tr').addClass('highlighted');
        //            $checked = $all;
        //        } else {
        //            selector.find('tbody .fancy-checkbox').find(':checkbox').prop('checked', false);
        //            selector.find('tbody tr').removeClass('highlighted');
        //            $checked = 0;
        //        }
        //        toggleMenu();
        //    });
        //    //选中的值
        //    var checked = function () {
        //        var values = [];
        //        selector.find('tbody .fancy-checkbox').find(':checkbox').each(function () {
        //            if ($(this).is(':checked'))
        //                values.push($(this).val());
        //        });
        //        return values;
        //    };
        //    //选中的值附加到URL中：url[?&]id=checked();
        //    var checkedUrl = function (url) {
        //        var id = checked().join(',');
        //        if (id < 1) return url;
        //        if (url.indexOf('?') === -1)
        //            url += '?';
        //        else
        //            url += '&';
        //        return url + 'id=' + id;
        //    };
        //    //载入弹窗
        //    $exec('_modal', function (s, v) {
        //        s.on('click', function () {
        //            s.jsModal(checkedUrl(v));
        //        });
        //    }, selector);
        //    //转向地址
        //    $exec('_goto', function (s, v) {
        //        s.on('click', function () {
        //            location.href = checkedUrl(v);
        //        });
        //    }, selector);
        //    //发送数据
        //    $exec('_action', function (s, v) {
        //        s.on('click', function () {
        //            var data = checked();
        //            if (data.length === 0) {
        //                $alert('请选择相关项目后再进行操作！');
        //                return;
        //            }
        //            var confirmStr = s.attr('_confirm');
        //            if (confirmStr && !confirm(confirmStr))
        //                return;

        //            $ajax(v, { ids: data.join(',') }, function (d) {
        //                var success = s.attr('_success');
        //                if (success)
        //                    $call(success, d);
        //            });
        //        });
        //    }, selector);
        //});
    });
})(jQuery);