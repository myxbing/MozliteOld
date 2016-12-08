/// <reference path="lib/jquery/dist/jquery.js" />
; (function ($) {
    $.fn.clickToggle = function (f1, f2) {
        ///<summary>切换执行不同的方法。</summary>
        return this.each(function () {
            var clicked = false;
            $(this).bind('click', function () {
                if (clicked) {
                    clicked = false;
                    return f2.apply(this, arguments);
                }

                clicked = true;
                return f1.apply(this, arguments);
            });
        });

    };
    $.fn.jsTarget = function () {
        ///<summary>返回当前元素内js-target属性指示的元素对象，如果不存在就为当前实例对象。</summary>
        var target = $(this.js('target'));
        if (target.length > 0)
            return target;
        return this;
    };
    $.fn.dset = function (key, func) {
        ///<summary>获取对象的缓存数据。</summary>
        ///<param name="key" type="String">缓存键。</param>
        ///<param name="func" type="Function">如果数据不存在返回的数据值函数。</param>
        var value = this.data(key);
        if (value) {
            return this.data(key);
        }
        value = func();
        this.data(key, value);
        return value;
    };
    $.fn.exec = function (func) {
        ///<summary>如果当前选择器的元素存在[可以使用no-js样式忽略]，则执行func方法，并将当前元素对象作为参数。</summary>
        ///<param name="func" type="Function">执行方法。</param>
        if (this.length > 0) {
            this.each(function () {
                var current = $(this);
                if (!current.hasClass('no-js')) {
                    func(current);
                }
            });
        }
    };
    window.$call = function (name) {
        ///<summary>执行方法。</summary>
        ///<param name="name" type="String">方法名称。</param>
        var func = window;
        name = name.split('.');
        for (var i in name) {
            func = func[name[i]];
        }
        if (typeof func === 'function') {
            var args = [];
            for (var j = 1; j < arguments.length; j++) {
                args.push(arguments[j]);
            }
            return func.apply(null, args);
        }
        return null;
    };
    window.$exec = function (name, func, context) {
        ///<summary>如果当前选择器的元素存在[可以使用no-js样式忽略]，则执行func方法，并将当前元素对象作为参数。</summary>
        ///<param name="name" type="String">属性名称。</param>
        ///<param name="func" type="Function">执行方法。</param>
        ///<param name="context" type="Object">上下文，默认为window。</param>
        var selector = $('[' + name + ']', context);
        if (selector.length > 0) {
            selector.each(function () {
                var current = $(this);
                var value = current.attr(name);
                if (!current.hasClass('no-js') && value !== 'no-js') {
                    func(current, value);
                }
            });
        }
    };
    window.$alert = function (message, type, reload) {
        ///<summary>显示警告消息。</summary>
        if (typeof message === 'object' && message) {
            type = message.type;
            message = message.message;
        }
        if (!message) return;
        var modal = $(document.body)
            .dset('alert',
                function () {
                    return $('<div class="js-alert modal fade"><div class="modal-dialog"><div class="modal-content"><div class="modal-body" style="padding: 50px 30px 30px;"><div class="col-sm-2"><i style="font-size: 50px;"></i></div> <span class="col-sm-10" style="line-height: 26px; padding-left: 0;"></span></div><div class="modal-footer"><button type="button" class="btn btn-primary"><i class="fa fa-check"></i> 确定</button></div></div></div></div>')
                    .appendTo(document.body);
                });
        var body = modal.find('.modal-body');
        type = type || 'warning';
        if (type === 'success')
            body.attr('class', 'modal-body row text-success').find('i').attr('class', 'fa fa-check');
        else
            body.attr('class', 'modal-body row text-' + type).find('i').attr('class', 'fa fa-warning');
        body.find('span').html(message);
        var button = modal.find('button').attr('class', 'btn btn-' + type);
        if (reload) button.attr('onclick', 'location.href=location.href;').removeAttr('data-dismiss');
        else button.attr('data-dismiss', 'modal').removeAttr('onclick');
        modal.modal('show');
    };
    window.$ajax = function (url, data, success, error) {
        $('#js-loading').fadeIn();
        $.ajax({
            url: url,
            data: data,
            dataType: 'JSON',
            type: 'POST',
            success: function (d) {
                $('#js-loading').fadeOut();
                var callback = d.data && success;
                if (d.message && d.type)
                    $alert(d.message, d.type, d.type === 'success' && !callback);
                if (callback)
                    success(d.data);
            },
            error: function (resp) {
                $('#js-loading').fadeOut();
                if (resp.status === 401) {
                    $alert('需要登陆才能够执行此操作！<a href=\"/login?returnurl=' + location.pathname + '\">点击登陆...</a>', 'warning');
                    return;
                }
                if (error) error(resp);
                else document.write(resp.responseText);
            }
        });
    };
    window.$submit = function (form, success, error) {
        ///<summary>提交表单。</summary>
        var $form = $(form);
        var submit = $form.find('button[type=submit]').attr('disabled', 'disabled');
        var icon = submit.find('i.fa');
        var css = icon.attr('class');
        icon.attr('class', 'fa fa-spinner fa-spin');
        $.ajax({
            type: "POST",
            url: $form.attr('action'),
            contentType: false,
            processData: false,
            data: new FormData(form),
            success: function (d) {
                submit.removeAttr('disabled').find('i.fa').attr('class', css);
                if (d.message) {
                    var panel = $form.find('.modal-alert span.errmsg');
                    if (panel.length > 0)
                        panel.html(d.message).parent().attr('class', 'modal-alert alert alert-' + d.type).show();
                    else
                        $alert(d.message, d.type, d.type === 'success' && !success);
                }
                if (d.type === 'success') {
                    if (success)
                        success(d.data || d);
                    else
                        location.href = location.href;
                }
            },
            error: function (e) {
                submit.removeAttr('disabled').find('i.fa').attr('class', css);
                if (e.status === 401) {
                    $alert('需要登陆才能够执行此操作！<a href=\"/login\">点击登陆...</a>', 'warning');
                    return;
                }
                if (error) error(e);
            }
        });
    };
    $.fn.js = function (name, value) {
        ///<summary>获取或设置js-开头的属性。</summary>
        ///<param name="name" type="String">属性名称。</param>
        ///<param name="value" type="Object">属性值。</param>
        if (value) return this.attr('js-' + name, value);
        return this.attr('js-' + name);
    };
    $.fn.createObjectURL = function () {
        ///<summary>预览文件地址，当前元素必须为input[type=file]。</summary>
        if (!this.is('input') || this.attr('type') !== 'file')
            return null;
        if (navigator.userAgent.indexOf("MSIE") > 0) return this.val();
        if (window.createObjectURL) return window.createObjectURL(this[0].files[0]);
        if (window.URL) return window.URL.createObjectURL(this[0].files[0]);
        if (window.webkitURL) return window.webkitURL.createObjectURL(this[0].files[0]);
        return null;
    };
    //modal
    $.fn.jsModal = function (url, func, upload) {
        ///<summary>显示当前地址的Modal模式窗口。</summary>
        ///<param name="url" type="string">URL地址。</param>
        ///<param name="func" type="string">回调方法名称。</param>
        ///<param name="upload" type="Boolean">是否添加文件上传格式。</param>
        var s = this;
        url = url || s.js('url');
        if (!url) {
            alert('js-url属性不能为空！');
            return;
        };
        var modal = s.dset('js-modal', function () {
            var current = $('<div class="js-modal modal fade"><div>')
                .appendTo($container)
                .data('source', s.jsTarget());
            return current;
        });
        modal.load(url, function () {
            var form = modal.find('form');
            if (!form.attr('action'))
                form.attr('action', url);
            if (!form.attr('onsubmit')) {
                func = func || s.js('func');
                if (func) {
                    modal.data('func', func);
                    form.attr('onsubmit', '$(this).jsModalSubmit(' + func + ');return false;');
                }
                else
                    form.attr('onsubmit', '$(this).jsModalSubmit();return false;');
                if (upload || s.js('upload') === 'true')
                    form.attr('enctype', 'multipart/form-data');
            }
        }).modal('show');
    };
    window.$modalInsert = function (current, url, modal) {
        ///<summary>图片浏览器点击图片或上传图片后回调的方法。</summary>
        if (url.url) {
            url = url.url;
            current.val(url);
            modal.modal('hide');
            return;
        }
        modal = $(current).parents('.js-modal');
        current = modal.data('source');
        var func = modal.data('func');
        if (func && func !== '$modalInsert') {
            $call(func, current, url, modal);
        } else if (current) {
            current.val(url);
        }
        modal.modal('hide');
    };
    $.fn.jsModalSubmit = function (callback) {
        var form = this;
        $submit(this[0],
            function (d) {
                callback = callback || form.parents('.js-modal').data('func');
                if (callback) {
                    var modal = form.parents('.js-modal');
                    callback(modal.data('source'), d, modal);
                } else
                    setTimeout(function () { location.href = location.href; }, 1000);
            },
            function (e) {
                form.find('.modal-body')
                    .html(e.responseText);
            });
    };
    //ready
    $(document)
        .ready(function () {
            window.$container = $('#modal-container');
            if (window.$container.length === 0)
                window.$container = document.body;
            //图片浏览器
            $exec('js-browser', function (s, v) {
                s.on(v, function () {
                    var url = s.js('url') || s.attr('href');
                    if (!url) {
                        url = '/browser/';
                        url += s.js('mt') + '/';
                        url += s.js('id');
                        var dir = s.js('dir');
                        if (dir)
                            url += '-' + dir;
                    }
                    s.jsModal(url, '$modalInsert', true);
                    return false;
                });
            });
            //Modal
            $exec('js-modal', function (s, v) {
                s.css('cursor', 'pointer');
                s.on(v, function () {
                    s.jsModal(s.js('url') || s.attr('href'));
                    return false;
                });
            });
            //events
            //toggle
            $exec('_toggle',
                function (s, v) {
                    s.hover(function () {
                        $(this).jsTarget().toggleClass(v);
                    }, function () {
                        $(this).jsTarget().toggleClass(v);
                    });
                });
            //focus
            $exec('_focus',
                function (s, v) {
                    s.focusin(function () {
                        $(this).jsTarget().toggleClass(v);
                    }).focusout(function () {
                        $(this).jsTarget().toggleClass(v);
                    });
                });
            //click
            $exec('_click',
                function (s, v) {
                    s.click(function () {
                        $(this).jsTarget().toggleClass(v);
                    });
                });
            //hover
            $exec('_hover',
                function (s) {
                    s.mouseenter(function (e) {
                        var $this = $(this);
                        var target = $this.target();
                        var name = $this.attr('_hover');
                        target.addClass(name);
                        target.mouseleave(function () {
                            target.removeClass(name);
                        }).click(function (ev) {
                            ev.stopPropagation();
                        });
                        $(document).one("click", function () {
                            target.removeClass(name);
                        });
                        e.stopPropagation();
                    });
                });
            //href
            $exec('_href',
                function (s, v) {
                    s.css('cursor', 'pointer')
                        .click(function () {
                            if (s.attr('target') === '_blank')
                                window.open(v);
                            else
                                location.href = v;
                        });
                });
            //scroll
            window.onscroll = function () {
                $exec('js-scroll',
                    function (s, v) {
                        var top = s.dset('scrollTop', function () { return s.offset().top; });
                        var target = s.jsTarget();
                        if ($(document.body).scrollTop() > top) {
                            if (v === ':visible') {
                                target.show();
                            } else {
                                v = s.dset('class',
                                    function () {
                                        var styles = [];
                                        var names = v.split(' ');
                                        for (var i = 0; i < names.length; i++) {
                                            if (!target.hasClass(names[i])) {
                                                styles.push(names[i]);
                                            }
                                        }
                                        return styles.join(' ');
                                    });
                                s.data('class', v);
                                target.addClass(v);
                            }
                        } else {
                            if (v === ':visible') {
                                target.hide();
                            } else {
                                target.removeClass(s.data('class'));
                            }
                        }
                    });
            };
            //bootstrap
            $('[data-toggle=dropdown]').exec(function (s) {
                if (s.js('event') === 'hover')
                    s.dropdownHover();
                else
                    s.dropdown();
            });
            //action
            $exec('js-action', function (s, v) {
                s.on('click',
                    function () {
                        var confirmStr = s.js('confirm');
                        if (confirmStr && !confirm(confirmStr))
                            return;
                        var success = s.js('success');
                        if (success)
                            success = function (d) { $call(success, d); }
                        var error = s.js('error');
                        if (error)
                            error = function (d) { $call(error, d); }
                        $ajax(v, { id: s.js('value') }, success, error);
                    });
            });
        });
})(jQuery);