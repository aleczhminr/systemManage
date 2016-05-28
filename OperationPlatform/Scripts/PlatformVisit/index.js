


function GetTaskList(page) {
    if (page < 1) {
        page = 1;
    }
    var postData = {};
    postData["pageIndex"] = page;

    postData["status"] = $("#TaskStatus").val();

    var level = $("#TaskLevel").val();
    if (level != "all") {
        switch (Number(level)) {
            case 0:
                postData["startlevel"] = 10;
                break;
            case 1:
                postData["startlevel"] = 20;
                postData["endlevel"] = 10;
                break;
            case 2:
                postData["startlevel"] = 30;
                postData["endlevel"] = 20;
                break;
            case 3:
                postData["startlevel"] = 50;
                postData["endlevel"] = 30;
                break;
            case 4:
                postData["startlevel"] = 70;
                postData["endlevel"] = 50;
                break;
            case 5:
                postData["startlevel"] = 80;
                postData["endlevel"] = 70;
                break;
            case 6:
                postData["endlevel"] = 80;
                break;
        }
    }
    var source = $("#TaskSource").val();
    if (source != "all") {
        postData["source"] = source;
    }


    var url = "/PlatformVisit/TaskList";
    $.doAjax(url, postData, function (msg) {
      
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#ListTable tbody").html(template("TaskListScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetTaskList", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);
        }


    });
}


function GetVisitList(page) {
    var jsonPost = {};
    jsonPost["pageIndex"] = page;

    var taskStatus = $("#HandleStat").val();
    if (taskStatus != "all") {
        jsonPost["status"] = taskStatus;
    }
    var vm = $("#VisitManner").val();
    if (vm != "all") {
        jsonPost["manner"] = vm;
    }
    var start=$("#insertTime input[name='start']").val();
    if (start.length>0) {
        jsonPost["starttime"] = start;
    }
    var end = $("#insertTime input[name='end']").val();
    if (end.length > 0) {
        jsonPost["endtime"] = end;
    }
    var accid = $("#AccountId").val();
    if (accid.length > 0) {
        jsonPost["accid"] = accid;
    }
    var insertName = $("#VisitInsertName").val();
    if (insertName.length > 0) {
        jsonPost["insertname"] = insertName;
    }
    var keyword = $("#VisitKeyword").val();
    if (keyword.length > 0) {
        jsonPost["keyword"] = keyword;
    }

    var tagDomList = $("#SelectVisitTagList .tag");
    var tagIdList = new Array();
    $.each(tagDomList, function (i, n) {
        tagIdList.push($(n).attr("tagid"));
    });
    if (tagIdList.length > 0) {
        jsonPost["tag"] = tagIdList.join(',');
    }

    var rt = $("#recordType input:checked").val();
    jsonPost["recordType"] = rt;



    var url = "/PlatformVisit/GetVisitList";
    $.doAjax(url, jsonPost, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#ListTable tbody").html(template("VisitListScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetVisitList", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);

            $.Pages.initTooltipPlugin();
        }

    }, true);
}
function VisitInsertNameOnKeyUp(evt) {
    evt = (evt) ? evt : ((window.event) ? window.event : "");
    var currKey = 0;
    currKey = evt.keyCode || evt.which || evt.charCode;
    if (currKey == 13) {
        GetVisitList(1);
    }
}

function GetVisitTagList() {

    if ($.trim($("#SelectVisitTagDiv .TagJson").html()) == "") {
        var url = "/shopinfo/VisitTagList";
        $.doAjax(url, null, function (msg) {
            if (msg != "null" && msg != "") {
                $("#SelectVisitTagDiv .TagJson").html(msg);
                var json = $.parseJSON(msg);
                var html = template("VisitTagListScript", { "list": json });
                $("#SelectVisitTagDiv .panel").html(html);
                $("#SelectVisitTagDiv .panel ul li:first a").click();
            }
        });

    }
}
function SelectVisitTag(obj) {
    if ($("#SelectVisitTagDiv .TagList").css("display") == "none") {
        $("#SelectVisitTagDiv").show();
        $("#SelectVisitTagDiv .TagList").show();
        GetVisitTagList();
        $(obj).html("查询").removeClass("btn-default").addClass("btn-complete");
    } else {
        $("#SelectVisitTagDiv .TagList").hide();
        if ($("#SelectVisitTagList span.tag ").size() < 1) {
            $("#SelectVisitTagDiv").hide();
        }

        GetVisitList(1);
        $(obj).html("选择分类").removeClass("btn-complete").addClass("btn-default");
    }
}



function VisitAddTag(obj) {
    var dom = $(obj);
    if ($("#SelectVisitTagList .tag[tagid='" + $.trim(dom.attr("tagid")) + "']").size() == 0) {
        var html = '<span class="tag label label-info" tagid="' + $.trim(dom.attr("tagid")) + '" >';

        html += $.trim(dom.html());

        html += '<span data-role="remove" onclick="VisitRemoveTag(this)" ></span></span>';

        $("#SelectVisitTagList").append(html);
    }
}

function VisitRemoveTag(obj) {
    clearTimeout(timeOut);
    $(obj).parent().remove();
    timeOut = setTimeout("GetVisitList(1)", 1000);
}


function ShowVisitInfo(id) {
    $("#modalSlideLeft table tbody").html("<tr><td>正在加载信息</td></tr>");
    var url = "/PlatformVisit/GetVisitInfo/" + id;
    $.doAjax(url, null, function (msg) {
        if (msg != "" && msg != "null") {
            var json = $.parseJSON(msg);
            var table = template("VisitInfoScript", json);
            $("#modalSlideLeft table tbody").html(table);
        }

    }, true);
}


function GetCaseList(page) {

    var postJson = {};

    var dataType= $(".CaseListWhere .btn-group button.btn-select").attr("value")
    var classType = $("#ThreeReasonType").val();
    var revisitName = $("#revisitName").val();
    var bgDate = $("#beginDate").val();
    var endDate = $("#endDate").val();

    postJson["dataType"] = dataType;
    postJson["pageIndex"] = page;
    postJson["classType"] = classType;
    postJson["revisitName"] = revisitName;

    if (dataType == 3) {
        postJson["statTime"] = bgDate;
        postJson["endTime"] = endDate;
    }

    var url = "/PlatformVisit/GetCaseList";
    $.doAjax(url, postJson, function (msg) {
        if (msg != null && msg != "" && msg != "null") {
            var json = $.parseJSON(msg);
            $("#caseTableList tbody").html(template("caseTableListScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetCaseList", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);
        }
    }, true);
}

function CloseVisit(id,accid) {    
    dialog({
        id: "closeVisitDialog",
        title: "关闭回访",
        lock: true,
        opacity: 0.57, // 透明度
        content: document.getElementById("closeReason"),
        ok: function () {
            var postJson = {};            
            
            if ($("#visitCloseReason").val().length <= 0) {
                alert("关闭理由不可以为空！");
                return false;
            }

            postJson["visitId"] = id;
            postJson["accid"] = accid;
            postJson["reason"] = $("#visitCloseReason").val();

            var ur = "/PlatformVisit/CloseVisit";

            $.doAjax(ur, postJson, function (msg) {
                //alert(msg);
                if (msg != "0") {
                    alert("已关闭！");
                    GetTaskList(1);
                } else {
                    alert("关闭失败！");
                }
            }, true);
        },
        cancel: true
    }).show();
    
}
