var MsgCenter = {
    send: function (accid, title, content, timing, nextFn) {
        var oData = {};
        oData["accids"] = accid;
        oData["title"] = title;
        oData["content"] = content;
        if (timing != null) {
            oData["timing"] = timing;
        }
        var sUrl = "/MsgCenter/PostMessage";
        $.ajax({
            type: "POST",
            url: sUrl,
            data: oData,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    sendGlobal: function (title, content, expire, timing, nextFn) {
        var oData = {};
        oData["title"] = title;
        oData["content"] = content;
        oData["expire"] = expire;
        if (timing != null) {
            oData["timing"] = timing;
        }
        var sUrl = "/MsgCenter/PostGlobal";
        $.ajax({
            type: "POST",
            url: sUrl,
            data: oData,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    status: function (accid, nextFn) {
        var keyString = "accid=" + accid;
        var sUrl = "/MsgCenter/GetStatus?" + keyString;
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    online: function (nextFn) {
        var sUrl = "/MsgCenter/GetOnline";
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    getMessage: function (accid, page, nextFn) {
        var keyString = "accid=" + accid;
        keyString += "&pagesize=" + 20;
        keyString += "&page=" + page;
        var sUrl = "/MsgCenter/GetMessage?" + keyString;
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    getMessageInfo: function (msgid, page, nextFn) {
        var keyString = "boardcastid=" + msgid;
        keyString += "&pagesize=" + 20;
        keyString += "&page=" + page;
        var sUrl = "/MsgCenter/GetMessage?" + keyString;
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    getMessageNotice: function (page, showType, nextFn) {
        var keyString = "";
        keyString += "pagesize=" + 10;
        keyString += "&page=" + page;
        keyString += "&showType=" + showType;
        var sUrl = "/MsgCenter/GetMessageNotice?" + keyString;
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    getMessageAnalysis: function (page, showType, nextFn) {
        var keyString = "";
        keyString += "pagesize=" + 20;
        keyString += "&page=" + page;
        keyString += "&showType=" + showType;
        var sUrl = "/MsgCenter/GetMessageAnalysis?" + keyString;
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    }
}

//移动端消息推送
var MobileMsgCenter = {
    send: function (accid, title, content, timing, nextFn) {
        var oData = {};
        oData["accids"] = accid;
        oData["title"] = title;
        oData["content"] = content;
        if (timing != null) {
            oData["timing"] = timing;
        }
        var sUrl = "/MsgCenter/MobileMessage";
        $.ajax({
            type: "POST",
            url: sUrl,
            data: oData,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    sendGlobal: function (title, content, expire, timing, nextFn) {
        var oData = {};
        oData["title"] = title;
        oData["content"] = content;
        oData["expire"] = expire;
        if (timing != null) {
            oData["timing"] = timing;
        }
        var sUrl = "/MsgCenter/MobileGlobal";
        $.ajax({
            type: "POST",
            url: sUrl,
            data: oData,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    getMessage: function (accid, page, nextFn) {
        var keyString = "accid=" + accid;
        keyString += "&pagesize=" + 20;
        keyString += "&page=" + page;
        var sUrl = "/MsgCenter/GetMobileMessage?" + keyString;
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    getMessageInfo: function (msgid, page, nextFn) {
        var keyString = "boardcastid=" + msgid;
        keyString += "&pagesize=" + 20;
        keyString += "&page=" + page;
        var sUrl = "/MsgCenter/GetMobileMessage?" + keyString;
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    getMessageNotice: function (page, showType, nextFn) {
        var keyString = "";
        keyString += "pagesize=" + 15;
        keyString += "&page=" + page;
        keyString += "&showType=" + showType;
        var sUrl = "/MsgCenter/GetMobileMessageNotice?" + keyString;
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    },
    getMessageAnalysis: function (page, showType, nextFn) {
        var keyString = "";
        keyString += "&pagesize=" + 20;
        keyString += "&page=" + page;
        keyString += "&showType=" + showType;
        var sUrl = "/MsgCenter/GetMobileMessageAnalysis?" + keyString;
        $.ajax({
            type: "GET",
            url: sUrl,
            success: function (msg) {
                nextFn(msg);
            }
        });
    }
}


function ShowMessageBox(accId) {
    var oItem = $("#messageCenter");
    oItem.find(".msgDesc").html("推送消息(网页端)");
    oItem.find(".msgBaseBtn").html("推送消息");
    oItem.find(".exprieArea").hide();
    oItem.show();

    $("#messageSendType").val("0");

    $("#msgAccountId").removeAttr("disabled", "disabled").attr("placeholder", "店铺ID (英文逗号分隔)");
    if (accId != undefined && accId != null) {
        $("#msgAccountId").val(accId);
        $("#msgTitle").focus();
    } else {
        $("#msgAccountId").focus();
    }
}

function SendGlobalMsg() {

    var oItem = $("#messageCenter");
    oItem.find(".msgDesc").html("发送公告(网页端)");
    oItem.find(".msgBaseBtn").html("发送公告");
    oItem.find(".exprieArea").show();
    oItem.show();

    $("#messageSendType").val("1");

    $("#msgAccountId").val("").attr("disabled", "disabled").attr("placeholder", "全部店铺");
    //$("#msgContet").val("");
    MsgEditor.execCommand('cleardoc');
    $("#msgTitle").val("").focus();
}

function CloseMessageBox() {
    $("#messageCenter").fadeOut();
}

function ToggleMessageBox() {
    var oItem = $("#messageCenter");
    var oBtn = oItem.find(".msgBoxTitle .msgToggle");
    var status = oItem.attr("data-mini");
    if (status == "false") {
        oItem.addClass("msgAreaMini");
        oBtn.removeClass("msgMini").addClass("msgMax");
        oItem.attr("data-mini", "true");
    } else {
        oItem.removeClass("msgAreaMini");
        oBtn.removeClass("msgMax").addClass("msgMini");
        oItem.attr("data-mini", "false");
    }
}
 

function GetStoreOnline() {
    MsgCenter.online(function (data) {
        if (data) {
            var json = $.parseJSON(data)
            var oItem = $(".OnlineStatusArea");
            oItem.find(".storeCount").html(json.StoreNum);
            oItem.find(".userCount").html(json.UserNum);
        }
    });
}

function ToggleOnlineCount() {
    var oItem = $(".OnlineStatusArea .CountArea");
    oItem.fadeToggle(function () {
        GetStoreOnline();
    });
}




//移动手机信息推送
function ShowMobileMessageBox(accId) {
    var oItem = $("#mobileMessageCenter");
    oItem.find(".msgDesc").html("推送消息(移动端)");
    oItem.find(".msgBaseBtn").html("推送消息(手机)");
    oItem.find(".exprieArea").hide();
    oItem.show();

    $("#mobileMessageSendType").val("0");

    $("#mobileAccountId").removeAttr("disabled", "disabled").attr("placeholder", "店铺ID (只支持单店铺)");
    if (accId != undefined && accId != null) {
        $("#mobileAccountId").val(accId);
        $("#mobileTitle").focus();
    } else {
        $("#mobileAccountId").focus();
    }
}

function SendMobileGlobalMsg() {

    var oItem = $("#mobileMessageCenter");
    oItem.find(".msgDesc").html("发送公告(移动端)");
    oItem.find(".msgBaseBtn").html("发送公告(手机)");
    oItem.find(".exprieArea").show();
    oItem.show();

    $("#mobileMessageSendType").val("1");

    $("#mobileAccountId").val("").attr("disabled", "disabled").attr("placeholder", "全部店铺");
    $("#mobileContet").val("");
    //MsgEditor.execCommand('cleardoc');
    $("#mobileTitle").val("").focus();

}

function CloseMobileMessageBox() {
    $("#mobileMessageCenter").fadeOut();
}

function ToggleMobileMessageBox() {
    var oItem = $("#mobileMessageCenter");
    var oBtn = oItem.find(".msgBoxTitle .msgToggle");
    var status = oItem.attr("data-mini");
    if (status == "false") {
        oItem.addClass("msgAreaMini");
        oBtn.removeClass("msgMini").addClass("msgMax");
        oItem.attr("data-mini", "true");
    } else {
        oItem.removeClass("msgAreaMini");
        oBtn.removeClass("msgMax").addClass("msgMini");
        oItem.attr("data-mini", "false");
    }
}
 
