//#region javascript方法重载. added by zxl@20121030
function callMethod(object, fn_name, fn) {
    var old = object[fn_name];
    if (old)
        object[fn_name] = function () {
            if (fn.length == arguments.length)
                return fn.apply(this, arguments);
            else if (typeof old == 'function')
                return old.apply(this, arguments);
        };
    else
        object[fn_name] = fn;
}
function JQueryUIDialog() {
    var openDialog = "open"; // overload method name
    callMethod(this, openDialog, function (divId) {
        $("#" + divId).dialog("open");
    });
    callMethod(this, openDialog, function (divId, divTitle, divWidth, divHeight, btnOkCallback) {
        var div = $("#" + divId);
        div.dialog({
            autoOpen: true,
            width: divWidth,
            height: divHeight,
            title: divTitle,
            modal: true,
            resizable: false,
            bgiframe: true,
            close: function (evt, ui) {
                div.dialog("destroy");
                //div.html("").remove();
            },
            buttons: {
                "确定": function () {
                    btnOkCallback();
                    //div.dialog("close");
                },
                "取消": function () {
                    div.dialog("close");
                }
            }
        });
    });
    callMethod(this, openDialog, function (divId, divTitle, divWidth, divHeight, btnOkCallback, btnCancelCallback) {
        var div = $("#" + divId);
        div.dialog({
            autoOpen: true,
            width: divWidth,
            height: divHeight,
            title: divTitle,
            modal: true,
            resizable: false,
            bgiframe: true,
            close: function (evt, ui) {
                div.dialog("destroy");
                //div.html("").remove();
            },
            buttons: {
                "确定": function () {
                    btnOkCallback();
                    //div.dialog("close");
                },
                "取消": function () {
                    btnCancelCallback();
                    div.dialog("close");
                }
            }
        });
    });
    /**************方法参数说明*******************
    srcUrl: 页面url
    divId: div的id
    divTitle:弹出dialog的标题
    divWidth:弹出dialog的宽度
    divHeight:弹出dialog的高度
    btnOkCallback:点击"确定"按钮时，需要执行的回调函数名称
    btnCancelCallback:点击"取消"按钮时，需要执行的回调函数名称
    ******************************************/
    callMethod(this, openDialog, function (srcUrl, divId, divTitle, divWidth, divHeight, btnOkCallback, btnCancelCallback) {
        var div = $("#" + divId);
        var content = div.load(srcUrl, {});
        div.dialog({
            autoOpen: true,
            width: divWidth,
            height: divHeight,
            title: divTitle,
            modal: true,
            resizable: false,
            bgiframe: true,
            close: function (evt, ui) {
                div.dialog("destroy");
                //div.html("").remove();
            },
            buttons: {
                "确定": function () {
                    btnOkCallback();
                    //div.dialog("close");
                },
                "取消": function () {
                    btnCancelCallback();
                    div.dialog("close");
                }
            }
        });
    });
}
//#endregion