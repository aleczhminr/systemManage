function GetNewAccountReport() {
    var postJson = {};
    //DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate
    var statTime = $(".reportWhere input[name='tstart']").val();
    var endTime = $(".reportWhere input[name='tend']").val();
    var lstatTime = $(".reportWhere input[name='lstart']").val();
    var lendTime = $(".reportWhere input[name='lend']").val();

    if (statTime.length === 0 || endTime.length === 0 || lstatTime.length === 0 || lendTime.length === 0) {
        alert("缺少必要的时间段！");
        return false;
    } else {
        postJson["stDate"] = statTime;
        postJson["edDate"] = endTime;
        postJson["lstDate"] = lstatTime;
        postJson["ledDate"] = lendTime;
    }

    var url = "/OperationReport/GetNewAccountModel";

    $.doAjax(url, postJson, function (msg) {
        if (msg != "" && msg != null) {
            var json = jQuery.parseJSON(msg);
            if (json == null) {
                return false;
            }
            $("#newAccount tbody").html(template("NewAccountScript", { "list": json.ItemGroupList }));

            //if (pageIndex == 1) {
            //    $("#ListPageHtml").attr("rowcount", parseInt(json.rowCount)).attr("maxpage", parseInt(json.maxPage));
            //    $("#ListPageHtml .dataTablePagText").html("共" + parseInt(json.rowCount) + "条   " + "<span class='m-l-10' id='summary'></span>");
            //}
            //var pageHtml = $.listPageHtml(pageIndex, $("#ListPageHtml").attr("maxpage"), "GetFeedbackList", 5);
            //$("#ListPageHtml .dataTables_paginate").html(pageHtml);

            //ShowMsgListTips();
        }
    }, true);
}

function GetConversionReport() {
    var postJson = {};
    //DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate
    var statTime = $(".reportWhere input[name='tstart']").val();
    var endTime = $(".reportWhere input[name='tend']").val();

    if (statTime.length === 0 || endTime.length === 0) {
        alert("缺少必要的时间段！");
        return false;
    } else {
        postJson["stDate"] = statTime;
        postJson["edDate"] = endTime;
    }

    var url = "/OperationReport/GetConversionModel";

    $.doAjax(url, postJson, function (msg) {
        if (msg != "" && msg != null) {
            var json = jQuery.parseJSON(msg);
            if (json == null) {
                return false;
            }
            $("#conversion tbody").html(template("conversionScript", { "list": json.DataList }));

            //if (pageIndex == 1) {
            //    $("#ListPageHtml").attr("rowcount", parseInt(json.rowCount)).attr("maxpage", parseInt(json.maxPage));
            //    $("#ListPageHtml .dataTablePagText").html("共" + parseInt(json.rowCount) + "条   " + "<span class='m-l-10' id='summary'></span>");
            //}
            //var pageHtml = $.listPageHtml(pageIndex, $("#ListPageHtml").attr("maxpage"), "GetFeedbackList", 5);
            //$("#ListPageHtml .dataTables_paginate").html(pageHtml);

            //ShowMsgListTips();
        }
    }, true);
}


function GetRetentionReport() {
    var postJson = {};
    //DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate
    var statTime = $(".reportWhere input[name='tstart']").val();
    var endTime = $(".reportWhere input[name='tend']").val();
    var lstatTime = $(".reportWhere input[name='lstart']").val();
    var lendTime = $(".reportWhere input[name='lend']").val();

    if (statTime.length === 0 || endTime.length === 0 || lstatTime.length === 0 || lendTime.length === 0) {
        alert("缺少必要的时间段！");
        return false;
    } else {
        postJson["stDate"] = statTime;
        postJson["edDate"] = endTime;
        postJson["lstDate"] = lstatTime;
        postJson["ledDate"] = lendTime;
    }

    var url = "/OperationReport/GetRetentionModel";

    $.doAjax(url, postJson, function (msg) {
        if (msg != "" && msg != null) {
            var json = jQuery.parseJSON(msg);
            if (json == null) {
                return false;
            }
            $("#retention tbody").html(template("retentionScript", { "list": json.DataList }));

            //if (pageIndex == 1) {
            //    $("#ListPageHtml").attr("rowcount", parseInt(json.rowCount)).attr("maxpage", parseInt(json.maxPage));
            //    $("#ListPageHtml .dataTablePagText").html("共" + parseInt(json.rowCount) + "条   " + "<span class='m-l-10' id='summary'></span>");
            //}
            //var pageHtml = $.listPageHtml(pageIndex, $("#ListPageHtml").attr("maxpage"), "GetFeedbackList", 5);
            //$("#ListPageHtml .dataTables_paginate").html(pageHtml);

            //ShowMsgListTips();
        }
    }, true);
}

function GetActiveReport() {
    var postJson = {};
    //DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate
    var statTime = $(".reportWhere input[name='tstart']").val();
    var endTime = $(".reportWhere input[name='tend']").val();
    var lstatTime = $(".reportWhere input[name='lstart']").val();
    var lendTime = $(".reportWhere input[name='lend']").val();

    if (statTime.length === 0 || endTime.length === 0 || lstatTime.length === 0 || lendTime.length === 0) {
        alert("缺少必要的时间段！");
        return false;
    } else {
        postJson["stDate"] = statTime;
        postJson["edDate"] = endTime;
        postJson["lstDate"] = lstatTime;
        postJson["ledDate"] = lendTime;
    }

    var url = "/OperationReport/GetAvgDataModel";

    $.doAjax(url, postJson, function (msg) {
        if (msg != "" && msg != null) {
            var json = jQuery.parseJSON(msg);
            if (json == null) {
                return false;
            }
            $("#activeHtml tbody").html(template("activeScript", { "list": json.DataList }));

            //if (pageIndex == 1) {
            //    $("#ListPageHtml").attr("rowcount", parseInt(json.rowCount)).attr("maxpage", parseInt(json.maxPage));
            //    $("#ListPageHtml .dataTablePagText").html("共" + parseInt(json.rowCount) + "条   " + "<span class='m-l-10' id='summary'></span>");
            //}
            //var pageHtml = $.listPageHtml(pageIndex, $("#ListPageHtml").attr("maxpage"), "GetFeedbackList", 5);
            //$("#ListPageHtml .dataTables_paginate").html(pageHtml);

            //ShowMsgListTips();
        }
    }, true);
}

function GetIncomeReport() {
    var postJson = {};
    //DateTime stDate, DateTime edDate, DateTime lstDate, DateTime ledDate
    var statTime = $(".reportWhere input[name='tstart']").val();
    var endTime = $(".reportWhere input[name='tend']").val();
    var lstatTime = $(".reportWhere input[name='lstart']").val();
    var lendTime = $(".reportWhere input[name='lend']").val();

    if (statTime.length === 0 || endTime.length === 0 || lstatTime.length === 0 || lendTime.length === 0) {
        alert("缺少必要的时间段！");
        return false;
    } else {
        postJson["stDate"] = statTime;
        postJson["edDate"] = endTime;
        postJson["lstDate"] = lstatTime;
        postJson["ledDate"] = lendTime;
    }

    var url = "/OperationReport/GetIncomeModel";

    $.doAjax(url, postJson, function (msg) {
        if (msg != "" && msg != null) {
            var json = jQuery.parseJSON(msg);
            if (json == null) {
                return false;
            }
            $("#income tbody").html(template("incomeScript", { "list": json.DataList }));

            //if (pageIndex == 1) {
            //    $("#ListPageHtml").attr("rowcount", parseInt(json.rowCount)).attr("maxpage", parseInt(json.maxPage));
            //    $("#ListPageHtml .dataTablePagText").html("共" + parseInt(json.rowCount) + "条   " + "<span class='m-l-10' id='summary'></span>");
            //}
            //var pageHtml = $.listPageHtml(pageIndex, $("#ListPageHtml").attr("maxpage"), "GetFeedbackList", 5);
            //$("#ListPageHtml .dataTables_paginate").html(pageHtml);

            //ShowMsgListTips();
        }
    }, true);
}