
function GetDateDiff(startTime) {
    var sTime = new Date(startTime),
        eTime = new Date();
    if (sTime.toDateString() == eTime.toDateString()) {
        return true;
    } else {
        return false;
    }
};
// function GetDateDiff(startTime) {
//     var sTime = new Date(startTime),
//         eTime = new Date();
//     var day = 1000 * 3600 * 24;
//     return (eTime.getTime() - sTime.getTime()) / parseInt(day);
// };

function ChangeOrder(name) {
    $("#ord").val(name);
    
    GetDetailInfo(1);
}

function GetLocationStatus(location)
{
    var postJson = {};
    postJson["location"] = location;

    var url = "/IndexDetailInfo/GetLocationUsrStatus";

    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            
            var html = "";
            html += "<span style='color:red'>" + json.FormerActive + "(曾活跃)</span>/<span style='color:green'>" + json.NowActive + "(现活跃)</span>"

            $("#statusTitle").append(html);
        }
    }, true);
}

function GetDetailInfo(page) {
    var url = "/IndexDetailInfo/GetDetailData",
        postJson = {};

    postJson["pageIndex"] = page;
    postJson["type"] = $("#type").val();
    postJson["index"] = $("#index").val();
    postJson["cnt"] = $("#cnt").val();
    postJson["location"] = $("#location").val();
    postJson["verif"] = $("#verif").val();
    postJson["day"] = $("#day").val();
    postJson["date"] = $("#date").val();
    postJson["keyword"] = $("#keyword").val();

    var order = $("#ord").val();
    postJson["order"] = $("#ord").val();

    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#AccountActiveListTable table").replaceWith(template("AccountDetailScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetDetailInfo", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);

            $("#accountListTable thead span ").attr("class", "pg-arrow_lright_line_alt");
            $("#" + order).attr("class", "pg-arrow_minimize_line");
        }
    }, true);
}

template.helper('dataFormat', function (data, type) {

    // 判断所有空数据为“-”
    if (data == "" || data == undefined || data == null) {
        return "-";
    };
    // 判断时间格式
    if (data == "1900-01-01 00:00:00") {
        return "-";
    } else {

        if (type == 1 && GetDateDiff(data)) {
            return "<span class='text-key bold'>" + data.substr(0, 10) + "</span>";
        }
        return data.substr(0, 10);
    };
});