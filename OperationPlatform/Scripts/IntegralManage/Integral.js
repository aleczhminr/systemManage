
function GetExchangeLog(page) {
    var url = "/IntegralManage/GetExchangeLog",
        postJson = {};

    postJson["pageIndex"] = page;
    postJson["pageSize"] = 15;
    postJson["stateValue"] = $("#StoreStateSelect").val();

    if ($("#accidText").val().length > 0) {
        postJson["accid"] = $("#accidText").val();
    }

    if ($("#projectNameText").val().length > 0) {
        postJson["projectName"] = $("#projectNameText").val();
    }

    var startTime = $(".page-where .input-daterange .input-sm[name='start']").val();
    if (startTime != "") {
        postJson["startTime"] = startTime;
    }
    var endTime = $(".page-where .input-daterange .input-sm[name='end']").val();
    if (endTime != "") {
        postJson["endTime"] = endTime;
    }


    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#ExchangeLogListTable table").replaceWith(template("ExchangeLogScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条，"+json.sumInt+"积分");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetExchangeLog", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);
            $("#ListPageHtml").attr("pageIndex", json.pageIndex);
        }
    }, true);
}

function ShipmentsClick(id) {
    var tr = $("#ExchangeLogListTable table tr[logid='" + id + "']");
    var addressJson = $.parseJSON(tr.find("div.TaskDeliveryAddress").html());
    if (addressJson.eRecipient == null || addressJson.eRecipient == "") {
        $("#ShipmentsShowDialog .eRecipient .context").html("<span class='NullTipe'>用户未填写</span>");
    } else {
        $("#ShipmentsShowDialog .eRecipient .context").html(addressJson.eRecipient);
    }

    if (addressJson.ePhoneNumber == null || addressJson.ePhoneNumber == "") {
        $("#ShipmentsShowDialog .ePhoneNumber .context").html("<span class='NullTipe'>用户未填写</span>");
    } else {
        $("#ShipmentsShowDialog .ePhoneNumber .context").html(addressJson.ePhoneNumber);
    }

    var eAddress = "";
    if (addressJson.eProvince != null && addressJson.eProvince != "") {
        eAddress = addressJson.eProvince;
    }
    if (addressJson.eStreet != null && addressJson.eStreet != "") {
        eAddress += addressJson.eStreet;
    }

    if (eAddress == "") {
        $("#ShipmentsShowDialog .eStreet .context").html("<span class='NullTipe'>用户未填写</span>");
    } else {
        $("#ShipmentsShowDialog .eStreet .context").html(eAddress);
    }

    dialog({
        id: "ShipmentsClickdialog",
        title: '发货',
        content: document.getElementById("ShipmentsShowDialog"),
        okValue: '确定',
        ok: function () {
            var url = "/IntegralManage/SetLogistics";
            var postJson = {};
            postJson["id"] = id;
            postJson["loginstics"] = $("#eLogisticsText").val();
            postJson["loginsticsnumber"] = $("#eLogisticsNumberText").val();

            $.doAjax(url, postJson, function (msg) {
                if (msg == "1") {
                    dialog({
                        content: '发货成功',
                        quickClose: true
                    }).showModal();
                    GetExchangeLog($("#ListPageHtml").attr("pageIndex"));
                } else {
                    dialog({
                        title: '错误提示',
                        content: '发货失败',
                        ok: function () { }
                    }).showModal();
                }
            });
            return true;
        },
    }).showModal();

}


function ShipmentsInfoShow(id) {
    var tr = $("#ExchangeLogListTable table tr[logid='" + id + "']");
    var addressJson = $.parseJSON(tr.find("div.TaskDeliveryAddress").html());
    if (addressJson.eRecipient == null || addressJson.eRecipient == "") {
        $("#LogisticsInfoShowDialog .eRecipient .context").html("<span class='NullTipe'>用户未填写</span>");
    } else {
        $("#LogisticsInfoShowDialog .eRecipient .context").html(addressJson.eRecipient);
    }

    if (addressJson.ePhoneNumber == null || addressJson.ePhoneNumber == "") {
        $("#LogisticsInfoShowDialog .ePhoneNumber .context").html("<span class='NullTipe'>用户未填写</span>");
    } else {
        $("#LogisticsInfoShowDialog .ePhoneNumber .context").html(addressJson.ePhoneNumber);
    }

    var eAddress = "";
    if (addressJson.eProvince != null && addressJson.eProvince != "") {
        eAddress = addressJson.eProvince;
    }
    if (addressJson.eStreet != null && addressJson.eStreet != "") {
        eAddress += addressJson.eStreet;
    }

    if (eAddress == "") {
        $("#LogisticsInfoShowDialog .eStreet .context").html("<span class='NullTipe'>用户未填写</span>");
    } else {
        $("#LogisticsInfoShowDialog .eStreet .context").html(eAddress);
    }


    var LogisticsJson = $.parseJSON(tr.find("div.LogisticsInfo").html());

    if (LogisticsJson.eLogistics == null || LogisticsJson.eLogistics == "") {
        $("#LogisticsInfoShowDialog .eLogistics .context").html("<span class='NullTipe'>未填写</span>");
    } else {
        $("#LogisticsInfoShowDialog .eLogistics .context").html(LogisticsJson.eLogistics);
    }

    if (LogisticsJson.eLogisticsNumber == null || LogisticsJson.eLogisticsNumber == "") {
        $("#LogisticsInfoShowDialog .eLogisticsNumber .context").html("<span class='NullTipe'>未填写</span>");
    } else {
        $("#LogisticsInfoShowDialog .eLogisticsNumber .context").html(LogisticsJson.eLogisticsNumber);
    }

    if (LogisticsJson.eSysName == null || LogisticsJson.eSysName == "") {
        $("#LogisticsInfoShowDialog .eSysName .context").html("<span class='NullTipe'>未填写</span>");
    } else {
        $("#LogisticsInfoShowDialog .eSysName .context").html(LogisticsJson.eSysName);
        $("#LogisticsInfoShowDialog .eSysName .context").append("<br>" + LogisticsJson.eSysTime + "");
    }

    dialog({
        id: "ShipmentsClickdialog",
        title: '发货',
        content: document.getElementById("LogisticsInfoShowDialog"),
        okValue: '确定',
        ok: function () { 
            return true;
        },
    }).showModal();
}





function GetTaskControlsList(page) {
    var url = "/IntegralManage/GetTaskJourna",
        postJson = {};

    postJson["pageIndex"] = page;
    postJson["pageSize"] = 10;
    postJson["state"] = $("#StoreStateSelect").val();

    if ($("#accountIdText").val().length > 0) {
        postJson["accid"] = $("#accountIdText").val();
    }

    if ($("#projectNameText").val().length > 0) {
        postJson["type"] = $("#projectNameText").val();
    }


     
    var startTime = $(".page-where .input-daterange .input-sm[name='start']").val();
    if (startTime != "") {
        postJson["statTime"] = startTime;
    }
    var endTime = $(".page-where .input-daterange .input-sm[name='end']").val();
    if (endTime != "") {
        postJson["endTime"] = endTime;
    }
    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#TaskControlsListTable table").replaceWith(template("TaskControlsScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetTaskControlsList", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);
            $("#ListPageHtml").attr("pageIndex", json.pageIndex);
        }
    }, true);
}

function AdvanceContentShow(obj, type) {    
    if (type == 1) {
        $("#advanceContentModal").html("备注");
    } else {
        $("#advanceContentModal").html("建议内容");
    }
    var advanceContent = $(obj).attr("advance");

    var accid = $(obj).attr("accid");

    $("#contentH").val(advanceContent);
    $("#accidH").val(accid);

    $("#advanceContent article").html(advanceContent);
    $("#advanceContent").modal("show");
}

function GetThisCategoryTask(obj) {
    var category = $(obj).attr("category");
    if (category != null && category !== "") {
        if (category == "all") {
            $("#StoreStateSelect").val(-1);
            $("#projectNameText").val("");
            $(".cs-placeholder").html("全部");
        } else {
            $("#projectNameText").val(category);
        }
        GetTaskControlsList(1);
    }
}


function ImportFeedBack() {
    var advanceContent = $("#contentH").val();
    var accid = $("#accidH").val();

    $("#advanceContent").modal("hide");

    dialog({
        content: '您确认要导入该条反馈吗？',
        icon: 'question',
        okValue: "确定",
        ok: function () {
            
            var url = "/IntegralManage/ImportFeedBack";
            $.doAjax(url, { "accid": accid, "content": advanceContent }, function (msg) {
                //msg 返回结果
                if (msg == "1") {
                    dialog({
                        content: '导入成功',
                        quickClose: true// 点击空白处快速关闭
                    }).show();
                }
                else if (msg == "2") {
                    dialog({
                        content: '已经存在该条反馈',
                        quickClose: true// 点击空白处快速关闭
                    }).show();
                } else {
                    dialog({
                        content: '导入失败',
                        quickClose: true // 点击空白处快速关闭
                    }).show();
                }
            }, true);
            return true;
        },
        cancelValue: '关闭',
        cancel: true //为true等价于function(){}
    }).showModal();

}






function TaskControlsAudit(obj) {
    var id = $(obj).attr("tId");

    Visiteditor.setContent("");
    dialog({
        id: "ShipmentsClickdialog",
        title: '审核任务',
        content: document.getElementById("VisitDiv"),
        okValue: '审核通过',
        ok: function () {

            var html = Visiteditor.getContent();
            if (html.length > 1) {
                var postJson = {};
                postJson["id"] = id;
                postJson["auditCon"] = escape(html);

                var url = "/IntegralManage/UpdateTaskStatus";
                $.doAjax(url, postJson, function (msg) {
                    if (msg == "1") {
                        dialog({
                            content: '审核成功',
                            quickClose: true
                        }).showModal();
                        GetTaskControlsList($("#ListPageHtml").attr("pageIndex"));
                    } else {
                        dialog({
                            title: '错误提示',
                            content: '审核失败',
                            ok: function () { }
                        }).showModal();
                    }
                },true);
            } else {
                alert("请输入审核内容");
                return false;
            }
            return true;
        },
    }).showModal();
}

function GetIntegralProduct(date) {
    var url = "/IntegralManage/GetIntegralProduct";

    var postJson = {};
    if (date != null && date != "") {
        postJson["dayDate"] = date;
    } else {
        postJson["dayDate"] = "";
    }

    $.doAjax(url, postJson, function (msg) {
        if (msg != "" && msg != null) {

            var jsonData = $.parseJSON(msg);

            $("#bodyData").html(template("ListTableScript", { "list": jsonData.DataList }));

            //$.each(jsonData, function(i, n) {

            //});

            //GetTaskControlsList($("#ListPageHtml").attr("pageIndex"));
        }
    }, true);
}

function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var strDate = date.getDate()-1;
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = year + seperator1 + month + seperator1 + strDate;
    return currentdate;
}

