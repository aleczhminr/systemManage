
function LoadTimeTimekeeping() {
    if ($("#IntervalSmsReview input").prop("checked")) {
        LoasSmsReview();
        if (smsReviewTime == 0) {
            $("#IntervalSmsReview label").text("正在刷新");
        } else {
            $("#IntervalSmsReview label").text(smsReviewTime + "秒后刷新");
        }
    } else {
        $("#IntervalSmsReview label").text("自动刷新");
    }
    if ($("#IntervalTaskInfo input").prop("checked")) {
        LoadTaskInfo();
        if (taskInfoTime == 0) {
            $("#IntervalTaskInfo label").text("正在刷新");
        } else {
            $("#IntervalTaskInfo label").text(taskInfoTime + "秒后刷新");
        }
    } else {
        $("#IntervalTaskInfo label").text("自动刷新");
    } 
}



function LoadTaskInfo(type) {

    if (type == 1 && $("#IntervalTaskInfo input").prop("checked")) {

        NotifyCheck();
    }

    if (taskInfoTime == 0 || type == 1) {

        $.ajax({
            type: "Get",
            url: '/Home/_Index_TaskInfo',
            success: function (data) {
                $('#refreshTask').html(data);
                if ($("#refreshTask tr td .DataNull").size() < 1) {
                    TitleShowStop("当前有新的回访信息！");
                    TitleShowStart("当前有新的回访信息！");

                    //Chrome Notification
                    try {
                        NotifyShow("当前有新的回访信息");
                        $('#TipsAudio')[0].play();
                    }
                    catch (ex) {
                        console.log(ex.message);
                    }

                } else {
                    TitleShowStop("当前有新的回访信息！");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert(XMLHttpRequest.responseText);
            }

        });
        taskInfoTime = 30;
    } else {
        taskInfoTime--;
    }
}
function LoadShopReview(type) {
    if (type == 1 && $("#IntervalShopReview input").prop("checked")) {

        NotifyCheck();
    }
    if (shopReview == 0 || type == 1) {
        $.ajax({
            type: "Get",
            url: '/Home/_Index_ShopReview)',
            success: function (data) {
                $('#refreshShop').html(data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert(XMLHttpRequest.responseText);
            }

        });
        shopReview = 60;
    } else {
        shopReview--;
    }
}

function LoasSmsReview(type) {

    if (type == 1 && $("#IntervalSmsReview input").prop("checked")) {

        NotifyCheck();
    }
    if (smsReviewTime == 0 || type == 1) {
        $.ajax({
            type: "Get",
            url: '/Home/_Index_SmsReview',
            success: function (data) {
                $('#refreshSms').html(data);
                if ($("#refreshSms tr td .DataNull").size() < 1) {
                    TitleShowStop("当前有新的审核短信！");
                    TitleShowStart("当前有新的审核短信！");
                    $('#TipsAudio')[0].play();


                    //Chrome Notification
                    try {
                        NotifyShow("当前有新的审核短信");
                    }
                    catch (ex) {
                        console.log(ex.message);
                    }

                } else {
                    TitleShowStop("当前有新的审核短信！");
                }
                SmsListContentTdHover();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //alert(XMLHttpRequest.responseText);
            }

        });
        smsReviewTime = 3;
    } else {
        smsReviewTime--;
    }
}


function whereTimeButtonClick(obj) {
    var value = obj.value;
    if (value == "return") {
        $(".btn-group").show().filter(".page-where-othertime").hide();
    } else {
        $(obj).parent().find(".btn-select").removeClass("btn-select");
        $(obj).addClass("btn-select");
        if (value == "other") {
            $(".btn-group").hide().filter(".page-where-othertime").show();
        }
    }
    $("#timeType").val(value);
}

function reviewSmsType(obj) {
    var value = obj.value;
    if (value == "2") {
        $("#rejectDesc").hide();
    } else {
        $("#rejectDesc").show();
    }
}

function changeChannel(obj) {
    var value = obj.value;
    if (value == "0") {
        $("#selectGroup").hide();
    } else {
        $("#selectGroup").show();
    }
}


function updateSmsContent(id) {

    $("#updateSmsContextText").val($.trim($("#smsContext_" + id).html()));


    dialog({
        id: "updateSmsContext",
        title: "记录信息",
        lock: true,
        opacity: 0.57,	// 透明度
        content: document.getElementById("updateSmsContextText"),
        ok: function () {
            var ur = "/Sms/UpdateSmsContent";
            $.ajax({
                type: "POST",
                url: ur,
                data: { "smsid": id, "smscontent": $("#updateSmsContextText").val() },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(XMLHttpRequest.responseText);
                },
                success: function (msg) {
                    if (msg == "1") {
                        dialog({
                            content: '修改成功',
                            quickClose: true// 点击空白处快速关闭
                        }).show();
                        LoasSmsReview(1);
                    } else {
                        dialog({
                            content: '修改失败',
                            quickClose: true// 点击空白处快速关闭
                        }).show();
                    }
                }
            });
        },
        cancel: true
    }).show();

}


function SmsCheckClick(id) {

    $("#notifyId").val(id);
    dialog({
        id: "SMsCheckdialogA",
        title: "审核短信",
        lock: true,
        opacity: 0.57,	// 透明度
        content: document.getElementById("SMsCheckdialog"),
        ok: function () {

            var postJson = {};
            postJson["notifyId"] = $("#notifyId").val();

            postJson["reviewType"] = $("#SMsCheckdialog input:radio[name='reviewType']:checked").val();
            postJson["channelType"] = $("#SMsCheckdialog input:radio[name='channelType']:checked").val();
            postJson["mobile"] = $("#selectGroup select[name='mobile']").val();
            postJson["unicom"] = $("#selectGroup select[name='unicom']").val();
            postJson["telecom"] = $("#selectGroup select[name='telecom']").val();
            postJson["reviewDesc"] = $("#rejectDesc").val();


            var ur = "/Home/IndexReview";
            $.doAjax(ur, postJson, function (msg) {
                if (msg == "1") {
                    dialog.get('SMsCheckdialogA').close().remove();
                    dialog({
                        content: '审核成功',
                        quickClose: true// 点击空白处快速关闭
                    }).show();
                } else {
                    alert("审核失败");
                }
            });
            return false;
        },
        cancel: true
    }).showModal();
}


function ShopCheckClick(accid, phone) {
    var url = "/HomePage/CheckShopInfo";
    $.doAjax(url, { "id": accid, "phone": phone }, function (msg) {
        if (msg == "1") {
            dialog({
                content: '审核成功，密码为 1234 ',
                quickClose: true// 点击空白处快速关闭
            }).show();
        } else {
            alert("审核失败");
        }
    }, true);
}

function titleScroll() {
    TitleMsg = TitleMsg.substring(1, TitleMsg.length) + TitleMsg.substring(0, 1);
    document.title = TitleMsg;
    TitleTimer = setTimeout("titleScroll()", 300);
}
function TitleShowStart(msg) {
    var newTitle = TitleMsgTip;
    if (newTitle.length > 0 && newTitle != "首页") {
        newTitle = newTitle.replace(msg, "") + msg;
    } else {
        newTitle = msg;
    }
    if (newTitle != TitleMsgTip) {
        TitleMsgTip = newTitle;
        TitleMsg = newTitle;
    }

    clearTimeout(TitleTimer);
    titleScroll();
}

function TitleShowStop(msg) {
    var newTitle = TitleMsgTip;
    newTitle = newTitle.replace(msg, "");
    if (newTitle == "") {
        TitleMsg = "首页";
        TitleMsgTip = "";
        clearTimeout(TitleTimer);
        document.title = TitleMsg;
    } else {
        TitleMsg = newTitle;
        TitleMsgTip = newTitle;
    }
}




function NotifyShow(notifyMsg) {
    if (window.webkitNotifications) {
        if (window.webkitNotifications.checkPermission() == 0) {
            var notification = window.webkitNotifications.createNotification("http://img.i200.cn/weblink/logo_tm.png", '审核提示', notifyMsg);
            notification.display = function () { }
            notification.onerror = function () { }
            notification.onclose = function () { }
            notification.onclick = function () { this.cancel(); }

            notification.replaceId = 'SmsReviewNotification';

            notification.show();
        } else {
            window.webkitNotifications.requestPermission(NotifyShow);
        }
    }
}

function NotifyCheck() {
    if (window.webkitNotifications) {
        if (window.webkitNotifications.checkPermission() != 0) {
            window.webkitNotifications.requestPermission();
        }
    }
}








function SmsListContentTdHover() {
    $("#refreshSms table tbody td.SmsContent").hover(
        function () {
            var smsCont = $.trim($(this).html());
            var length = smsCont.length;

            var d = dialog({
                id: "smsContentDialog",
                align: 'top',
                content: '短信长度：' + length + "，短信类型：" + $(this).attr("smstype") + "，点击直接复制"
            });
            d.show(this);
        },
        function () {
            dialog.get('smsContentDialog').close().remove();
        }
        );
}
var KDf435234234 = null;
function ZeroClipClick(t) {
    $("#aeroClipContext-Text").html($.trim($(t).html()));
    KDf435234234=dialog({
        id: 'KDf435234234',
        content: document.getElementById("aeroClipDiv")
    }).show();
}