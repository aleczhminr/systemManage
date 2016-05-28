

function GetCouponList(page,opid) {
    var postJson = {};
    postJson["page"] = page;

    if ($("#couponDesc").val().length > 0) {
        postJson["couponDesc"] = $("#couponDesc").val();
    }

    if ($("#insertTime input[name='start']").val().length > 0) {
        postJson["statTime"] = $("#insertTime input[name='start']").val();
    }

    if ($("#insertTime input[name='end']").val().length > 0) {
        postJson["endTime"] = $("#insertTime input[name='end']").val();
    }

    if ($("#expiration").is(':checked')) {
        postJson["expired"] = "y";
    } else {
        postJson["expired"] = "n";
    }

    if ($("#InsertName").val().length > 0) {
        postJson["operatorName"] = $("#InsertName").val();
    } else {
        if (opid == null || opid == undefined) {
            postJson["operatorName"] = $("#insertNameType .active input:checked").val();
        } else {
            postJson["operatorName"] = opid;
        }
    }


    var url = "/OrderCoupon/CouponList";
    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#ListTable tbody").html(template("orderCounponListScript", json));
            if (page == 1) {
                $("#ListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#ListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#ListPageHtml").attr("maxpage"), "GetCouponList", 5);
            $("#ListPageHtml .dataTables_paginate").html(pageHtml);
        }
    });
}

function CouponKeyUp(evt) {
    evt = (evt) ? evt : ((window.event) ? window.event : "");
    var currKey = 0;
    currKey = evt.keyCode || evt.which || evt.charCode;
    if (currKey == 13) {
        GetCouponList(1);
    }
}

function CouponInfoClick(cid) {
    var url = "/OrderCoupon/CouponInfo/" + cid;
    $.doAjax(url, null, function (msg) {
        if (msg != "" && msg != null) {
            var json=$.parseJSON(msg);
            var tableHtml = template("CouponInfoScript", json);
            $("#CouponInfoList .couponInfoTable").html(tableHtml);
            dialog({
                id: "ordercouponinfolistdialog",
                title: '优惠券详情管理',
                content: document.getElementById("CouponInfoList")
            }).showModal();
            $("#CouponInfoListTable").attr("ocid", cid);
            CouponInfoList(1);
        } else {
            alert("打开失败");
        }
    }, true);

}

function CouponInfoList(page) {
    var postJson = {};
    postJson["page"] = page;
    postJson["ocid"] = $("#CouponInfoListTable").attr("ocid");




    var url = "/OrderCoupon/CouponInfoList/";
    $.doAjax(url, postJson, function (msg) {
        if (msg != "null" && msg != "") {
            var json = $.parseJSON(msg);
            $("#CouponInfoListTable tbody").html(template("couponInfoListScript", json));
            if (page == 1) {
                $("#CouponInfoListPageHtml").attr("rowcount", json.rowCount).attr("maxpage", json.maxPage);
                $("#CouponInfoListPageHtml .dataTablePagText").html("共" + json.rowCount + "条");
            }
            var pageHtml = $.listPageHtml(page, $("#CouponInfoListPageHtml").attr("maxpage"), "CouponInfoList", 5);
            $("#CouponInfoListPageHtml .dataTables_paginate").html(pageHtml);
        }
    });
}

function generateCoupon() {

    var gNum = prompt("请输入要生成的张数", 1);
    if (gNum != null && gNum != "") {
        var url = "/OrderCoupon/GenerateCoupon/";
        var postJson = {};
        postJson["ocid"] = $("#CouponInfoListTable").attr("ocid");
        postJson["num"] = gNum;
        $.doAjax(url, postJson, function (msg) {
            if (msg == "1") {
                var b = dialog({
                    content: '正在生成，请稍后',
                    quickClose: true// 点击空白处快速关闭
                }).showModal();
                setTimeout(function () {
                    b.close().remove();
                    CouponInfoList(1);
                }, 3000);
            } else {
                alert("生成失败，请联系技术人员");
            }
        }, true);
    }
}

function bindingCoupon(code) {

    dialog({
        id: "bingdingCouponDialog",
        title: '绑定店铺',
        content: "<div>请输入绑定的店铺ID：</div><div><input type='text' value='' id='bingdingCouponText' /></div>",
        ok: function () {
            var url = "/OrderCoupon/BingCoupon";
            var postJson = {};
            postJson["accountid"] = $("#bingdingCouponText").val();
            postJson["CouponID"] = code;
            $.doAjax(url, postJson, function (msg) {
                if (msg == "2") {
                    dialog({
                        content: '绑定成功',
                        quickClose: true// 点击空白处快速关闭
                    }).show();
                    CouponInfoList(1);
                } else {
                    alert("绑定优惠券失败" + msg);
                }
            }, true);

        }
    }).showModal();
}