
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
    $("#timePeriod").val(value);
}


function SmsInfoDetailsList(page, accid, smsid) {
    if (accid != null) {
        $("#selectSmsAccid").val(accid);
    } else {
        accid = $("#selectSmsAccid").val();
    }
    if (smsid != null) {
        $("#selectSmsId").val(smsid);
    } else {
        smsid = $("#selectSmsId").val();
    }
    if (page < 1) {
        page = 1;
    }

    var url = "/Sms/GetSmsDetailList";
    var postJson = {};
    postJson["page"] = page;
    postJson["noticeid"] = smsid;
    postJson["accid"] = accid;

    var phone = $("#selectSmsPhone").val();
    if (phone.length > 0) {
        postJson["phone"] = phone;
    }

    $.doAjax(url, postJson, function (msg) {
        if (msg != "") {
            var json = $.parseJSON(msg);
            var html = template("smsInfoDetailsListScript", json);
            $("#smsInfoDetailsList table tbody").html(html);
            if (page == 1) {

                var maxRow = json.smsCnt;
                var maxPage = Math.ceil(maxRow / 15);
                $("#smsInfoDetailsList .ListPageHtml").attr("rowcount", maxRow).attr("maxpage", maxPage);
                $("#smsInfoDetailsList .ListPageHtml .dataTablePagText").html("共" + maxRow + "条");
            }


            var pageHtml = $.listPageHtml(page, $("#smsInfoDetailsList .ListPageHtml").attr("maxpage"), "SmsInfoDetailsList", 5);
            $("#smsInfoDetailsList .ListPageHtml .dataTables_paginate").html(pageHtml);


            dialog({
                id: "smsSmsContext",
                title: "信息详情",
                lock: true,
                opacity: 0.57,	// 透明度
                content: document.getElementById("smsInfoDetailsList"),
                cancel: true
            }).showModal();
        }
    }, true);

}


function GetSmsList(page) {
    var url = "/Sms/GetSmsList";
    var postJson = {};
    postJson["iPage"] = page;

    postJson["priorityType"] = $("#priorityType").val();
    postJson["sendType"] = $("#sendType").val();
    postJson["smsType"] = $("#smsType").val();
    postJson["timePeriod"] = $("#timePeriod").val();
    postJson["start"] = $("#startTime").val();
    postJson["end"] = $("#endTime").val();
    postJson["accId"] = $("#accId").val();




    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#SmsListTable tbody").html(template("SmsListScript", json));
            if (page == 1) {
                $("#smsListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.MaxPage);
                $("#smsListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#smsListPageHtml").attr("maxpage"), "GetSmsList", 5);
            $("#smsListPageHtml .dataTables_paginate").html(pageHtml);
            SmsListContentTdHover();
        }
    });
}


function SmsListContentTdHover() {
    $("#SmsListTable tbody td.SmsContent").hover(
        function () {
            var smsCont = $.trim($(this).html());

            var length = smsCont.length;

            var d = dialog({
                id:"smsContentDialog",
                align: 'top',
                content: '短信长度：' + length + "，短信类型：" +$(this).attr("smstype") + "，点击直接复制"
            });
            d.show(this);
        },
        function () {
            dialog.get('smsContentDialog').close().remove();
        }
        );
}
var KDf435 = null;
function ZeroClipClick(t) {
    $("#aeroClipContext-Text").html($.trim($(t).html()));
    KDf435=dialog({
        id: 'KDf435',
        content: document.getElementById("aeroClipDiv"),
        zIndex: 90
    });
    KDf435.show();
}
