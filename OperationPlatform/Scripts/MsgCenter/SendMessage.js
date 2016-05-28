
function MessageTypeClick() {
    var type = $("#MessageType input:radio:checked").val();
    if (type == "1") {
        $("#msgAccountId").hide();
        $("#expireTime").show();
    } else {
        $("#msgAccountId").show();
        $("#expireTime").hide();
    }
}

function SendMessageClick() {

    var pingtai = $("#PingTaiType input:radio:checked").val();
    if (pingtai == "1") {
        SendMessage()
    } else {
        SendMobileMessage();
    }
}



function SendMessage() {
    var oItem = $("#messageCenter");
    var accId = $("#msgAccountId input:text").val();
    var title = $("#msgTitle").val();
    var content = escape($("#VisitContent").code());
    var expireTime = $("#expireTime input:text").val();
    var sendType = $("#MessageType input:radio:checked").val();
    if (sendType == "1") {
        //公告消息
        if (title == "" || content == "") {
            dialog({
                content: '请填写消息内容！',
                quickClose: true
            }).show();
            return false;
        }

        dialog({
            title: '确认提醒',
            content: '确认发送该条公告信息？',
            ok: function () {
                var sendStatus = oItem.attr("data-sending");
                if (sendStatus != "true") {
                    oItem.attr("data-sending", "true");
                    oItem.find(".msgBaseBtn").html("正在发送");
                    MsgCenter.sendGlobal(title, content, expireTime, null, function (data) {
                        oItem.attr("data-sending", "false");
                        if (data == "1") {
                            dialog({
                                content: '公告发送成功！',
                                quickClose: true
                            }).show();
                            oItem.find(".msgBaseBtn").html("发送成功");
                        } else {

                            dialog({
                                content: '发送发送失败，请重试！',
                                quickClose: true
                            }).show();
                            oItem.find(".msgBaseBtn").html("发送公告");
                        }
                    });
                } else {
                    dialog({
                        content: '正在发送公告，不要重复提交！',
                        quickClose: true
                    }).show();
                }
            },
            okValue: "确认",
            cancel: function () { },
            cancelValue:"取消"
        }).showModal();
    } else {
        //普通消息
        if (accId == "" || title == "" || content == "") {
            dialog({
                content: '请填写消息内容！',
                quickClose: true
            }).show();
            return false;
        }

        var sendStatus = oItem.attr("data-sending");

        if (sendStatus != "true") {
            oItem.attr("data-sending", "true");
            oItem.find(".msgBaseBtn").html("正在推送");
            MsgCenter.send(accId, title, content, null, function (data) {
                oItem.attr("data-sending", "false");
                if (data == "1") {
                    dialog({
                        content: '消息发送成功！',
                        quickClose: true
                    }).show(); 
                    oItem.find(".msgBaseBtn").html("推送成功");
                } else {
                    dialog({
                        content: '消息发送失败，请重试！',
                        quickClose: true
                    }).show(); 
                    oItem.find(".msgBaseBtn").html("推送消息");
                }
            });
        } else {
            dialog({
                content: '正在推送消息，不要重复提交！',
                quickClose: true
            }).show();
        }
    }
}


function SendMobileMessage() {
    var oItem = $("#messageCenter");
    var accId = $("#msgAccountId input:text").val();
    var title = $("#msgTitle").val();
    var content = escape($("#VisitContent").code());
    var expireTime = $("#expireTime input:text").val();
    var sendType = $("#MessageType input:radio:checked").val();
    if (sendType == "1") {
        //公告消息
        if (title == "" || content == "") {
            dialog({
                content: '请填写消息内容！',
                quickClose: true
            }).show();
            return false;
        }
        dialog({
            title: '确认提醒',
            content: '确认发送该条公告信息？',
            ok: function () {
                var sendStatus = oItem.attr("data-sending");
                if (sendStatus != "true") {
                    oItem.attr("data-sending", "true");
                    oItem.find(".msgBaseBtn").html("正在发送");
                    MobileMsgCenter.sendGlobal(title, content, expireTime, null, function (data) {
                        oItem.attr("data-sending", "false");
                        if (data == "1") {
                            dialog({
                                content: '公告发送成功！',
                                quickClose: true
                            }).show();
                            oItem.find(".msgBaseBtn").html("发送成功");
                        } else {
                            dialog({
                                content: '发送发送失败，请重试！',
                                quickClose: true
                            }).show();
                            oItem.find(".msgBaseBtn").html("发送公告(手机)");
                        }
                    });
                } else {
                    dialog({
                        content: '正在发送公告，不要重复提交！',
                        quickClose: true
                    }).show();
                }
            },
            okValue: "确认",
            cancel: function () { },
            cancelValue: "取消"
        }).showModal(); 
    } else {
        //普通消息
        if (accId == "" || title == "" || content == "") {
            dialog({
                content: '请填写消息内容！',
                quickClose: true
            }).show(); 
            return false;
        }

        var sendStatus = oItem.attr("data-sending");

        if (sendStatus != "true") {
            oItem.attr("data-sending", "true");
            oItem.find(".msgBaseBtn").html("正在推送");
            MobileMsgCenter.send(accId, title, content, null, function (data) {
                oItem.attr("data-sending", "false");
                if (data == "1") {
                    dialog({
                        content: '消息发送成功！',
                        quickClose: true
                    }).show(); 
                    oItem.find(".msgBaseBtn").html("推送成功");
                } else {
                    dialog({
                        content: '消息发送失败，请重试！',
                        quickClose: true
                    }).show(); 
                    oItem.find(".msgBaseBtn").html("推送消息(手机)");
                }
            });
        } else {
            dialog({
                content: '正在推送消息，不要重复提交！',
                quickClose: true
            }).show(); 
        }
    }
}