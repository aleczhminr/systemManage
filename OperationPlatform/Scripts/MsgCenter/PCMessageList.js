
function GetMsgList(page) {
    var oItem = $("#msgListArea .MsgCenter");
    oItem.fadeTo("fast", 0.6);

    var showType = -1;
    var oShowType = $("#msgShowType").val();
    if (oShowType) {
        showType = oShowType;
    }

    MsgCenter.getMessageNotice(page, showType, function (data) {
        var noData = "<td colspan='4' class='noneBody'>还没有消息哦</td>";
        if (data) {
            var json = $.parseJSON(data)
            var sumCount = json.sumCount;
            if (sumCount > 0) {
                oItem.find("table tbody").html(template('msgList-tpl', json));
                Pagination(sumCount, page);
                ShowMsgListTips();
            } else {
                oItem.find("table tbody").html(noData);
            }
            oItem.fadeTo("fast", 1);
        } else {
            oItem.find("table tbody").html(noData);
        }
    });
}

function Pagination(sumCount, nowPage) {
    $('#paginationArea').jqPaginator({
        totalCounts: sumCount,
        pageSize: 15,
        visiblePages: 8,
        currentPage: nowPage,
        prev: '<li class="prev"><a href="javascript:;">上一页</a></li>',
        next: '<li class="next"><a href="javascript:;">下一页</a></li>',
        page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
        onPageChange: function (num, type) {
            if (type == "change") {
                GetMsgList(num);
            }
        }
    });
}

function ShowMsgListTips() {
    $("#msgListArea .MsgCenter .msgTitle").qtip({
        content: {
            attr: 'rel'
        },
        position: {
            my: 'left center',
            at: 'right center'
        },
        show: {
            solo: true
        },
        style: {
            classes: 'ui-tooltip-shadow ui-tooltip-light'
        }

    });
}

function GetMessageInfoList(msgId, page) {
    var oItem = $("#MessageInfoBox");
    oItem.attr("rel", msgId);
    window.parent.MsgCenter.getMessageInfo(msgId, page, function (data) {
        if (data != "error") {
            var json = $.parseJSON(data);
            oItem.find(".msgItem").remove();
            oItem.find(".trLoading").before(template('MsgInfoList_Tpl', json));

            if (json.sumCount > 20) {
                oItem.find(".morebtn a").attr("rel", "2");
                oItem.find(".morebtn").show();
            } else {
                oItem.find(".morebtn").hide();
            }

            oItem.find(".msgInfoCount").html(json.sumCount);
            oItem.find(".msgReadCount").html(json.readCount);

            dialog({
                id: 'MsgInfoListBox',
                title: '推送消息详情',
                content: document.getElementById('MessageInfoBox'),
                lock: true,
                opacity: 0.5,
                padding: "20px 30px",
                background: '#FFF',
                ok: function () {

                },
                okVal: '关闭',
                close: function () {

                }
            }).showModal();
        }
    });

    $("#msgListArea .MsgCenter .msgTitle").qtip('api').hide();
}

function LoadMoreMsgInfoList() {
    var oItem = $("#MessageInfoBox");
    var msgId = oItem.attr("rel");
    var loadPage = parseInt(oItem.find(".morebtn a").attr("rel"));
    var timerId = setTimeout(function () {
        oItem.find(".trLoading").show();
    }, 100);
    window.parent.MsgCenter.getMessageInfo(msgId, loadPage, function (data) {
        clearTimeout(timerId);
        oItem.find(".trLoading").hide();
        if (data != "error") {
            var json = $.parseJSON(data);
            oItem.find(".trLoading").before(template('MsgInfoList_Tpl', json));

            var pageNum = (20 * (loadPage - 1)) + json.msgList.length;
            if (json.sumCount > pageNum) {
                oItem.find(".morebtn a").attr("rel", (loadPage + 1));
                oItem.find(".morebtn").show();
            } else {
                oItem.find(".morebtn").hide();
            }
        }
    });
}


//Analysis Function
function GetAnalysis(page) {
    var oItem = $("#msgListArea .analyArea");
    oItem.fadeTo("fast", 0.6);

    var showType = -1;
    var oShowType = $("#msgShowType").val();
    if (oShowType) {
        showType = oShowType;
    }

    window.parent.MsgCenter.getMessageAnalysis(page, showType, function (data) {
        var noData = "<td colspan='4' class='noneBody'>还没有消息哦</td>";
        var noMoreData = "<span>没有更多消息了</span>";
        if (data) {
            var json = $.parseJSON(data)
            var sumCount = json.sumCount;
            var oMoreArea = oItem.find(".moreArea");
            if (json.msgList.length > 0) {
                oItem.find("table tbody").html(template('analysis_Tpl', json));
                 
                if (json.msgList.length == 20) {
                    oMoreArea.show();
                    oMoreArea.find(".analysisLoadMore").attr("rel", "2");
                } else {
                    oMoreArea.hide();
                }
            } else {
                oMoreArea.html(noData);
            }

            $("#msgListArea .sumArea .SumMsgCount").html(json.sumCount);
            $("#msgListArea .sumArea .SumMsgRead").html(json.readCount);
            oItem.fadeTo("fast", 1);
        } else {
            oItem.find("table tbody").html(noData);
        }
    });
}

function LoadMoreAnalysis() {
    var oItem = $("#msgListArea .analysisLoadMore");
    var pageNum = oItem.attr("rel");
    var timerId = setTimeout(function () {
        oItem.html("正在加载...");
    }, 100);

    var showType = -1;
    var oShowType = $("#msgShowType").val();
    if (oShowType) {
        showType = oShowType;
    }

    window.parent.MsgCenter.getMessageAnalysis(pageNum, showType, function (data) {
        clearTimeout(timerId);
        oItem.html("点击加载更多");
        if (data != "error") {
            var json = $.parseJSON(data);
            var oAnaly = $(".analyArea");
            oAnaly.find("tbody").append(template('analysis_Tpl', json));

            if (json.msgList.length == 20) {
                oItem.attr("rel", (parseInt(pageNum) + 1));
                oItem.show();
            } else {
                oItem.hide();
            }
        }
    });
}

function ChangeShowView(item) {
    var oItme = $("#msgListArea");
    if (item == 2) {
        //Analysis
        oItme.find(".analyArea").show();
        oItme.find(".MsgCenter").hide();
        oItme.find(".showMsgList").show();
        oItme.find(".showAnalysis").hide();
        oItme.find(".sumArea").show();
        oItme.find("#msgShowType").attr("rel", "analysis");
        GetAnalysis(1);
    } else {
        //MsgView
        oItme.find(".analyArea").hide();
        oItme.find(".MsgCenter").show();
        oItme.find(".showMsgList").hide();
        oItme.find(".showAnalysis").show();
        oItme.find(".sumArea").hide();
        oItme.find("#msgShowType").attr("rel", "msg");
        GetMsgList(1);
    }
}

function ChangeShowType() {
    var viewType = $("#msgShowType").attr("rel");
    if (viewType == "analysis") {
        GetAnalysis(1);
    } else {
        GetMsgList(1);
    }
}