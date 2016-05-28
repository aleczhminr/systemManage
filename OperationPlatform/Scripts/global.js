
//Extend jQuery Ajax
/*
            $.doAjax("url", data, function (msg) {
            //msg 返回结果
            },false,false);
            $.doAjax("url", null, function (msg) {
            //msg 返回结果
            },true);
*/
$.extend($, {
    //url-地址,data-数据,callback-回调函数,loading-加载提示,cache-是否缓存
    doAjax: function (url, data, callback, loading, cache) {
        var defaultSetting = {
            type: "POST",
            cache: false,
            url: url,
            data: data,
            beforeSend: function () { $.LoadingShow(); },
            complete: function () { $.LoadingHide(); },
            error: function (XMLHttpRequest, textStatus, errorThrown) { $.LoadingErroe(XMLHttpRequest, textStatus, errorThrown); },
            success: function (response, textStatus, jqXHR) {
                if (jqXHR.statusText == "logInfo") {
                    window.location.href = "/Account/Login";
                    return false;
                }
                if (typeof callback != 'undefined') {
                    callback.call(this, response);
                    $.SuccessDispose(defaultSetting);
                }
            }
        };
        if (loading !== undefined && loading == false) {
            defaultSetting["beforeSend"] = undefined;
            defaultSetting["complete"] = undefined;
        } else if (loading !== undefined && loading != false && loading != true && loading != "") {
            defaultSetting["beforeSend"] = function () { $.LoadingShow(loading); };
        }
        if (cache !== undefined && cache == true) {
            defaultSetting["cache"] = true;
        }
        $.ajax(defaultSetting);
    },
    doAjaxGet: function (url, data, callback) {
        var defaultSetting = {
            type: "GET",
            cache: false,
            url: url,
            data: data,
            beforeSend: function () { $.LoadingShow(); },
            complete: function () { $.LoadingHide(); },
            success: function (response) {
                if (typeof callback != 'undefined') {
                    callback.call(this, response);
                }
            }
        };
        if (loading !== undefined && loading == false) {
            defaultSetting["beforeSend"] = undefined;
            defaultSetting["complete"] = undefined;
        }
        if (cache !== undefined && cache == true) {
            defaultSetting["cache"] = true;
        }
        $.ajax(defaultSetting);
    },
    LoadingShow: function (message) {
        var strMsg = "正在加载，请稍等...";
        if (message !== undefined && message != "") {
            strMsg = message;
        }
        var ajaxbg = $("#LoadingBackground,#LoadingProgressBar");
        if (ajaxbg.html() == null) {
            $("body").append('<div id="LoadingBackground" class="LoadingBackground" ></div><div num="1" id="LoadingProgressBar" class="LoadingProgressBar" ><img src="/img/news_loading.gif" style="margin: -7px 8px -4px 0px;" />' + strMsg + '</div>');
        } else {
            var num = Number($("#LoadingProgressBar").attr("num"));
            $("#LoadingProgressBar").html(strMsg).attr("num", num + 1);
        }
        $("#LoadingBackground,#LoadingProgressBar").show();
    },
    LoadingHide: function () {
        var num = Number($("#LoadingProgressBar").attr("num"));
        if (num > 1) {
            $("#LoadingProgressBar").attr("num", num - 1);
        } else {
            $("#LoadingBackground,#LoadingProgressBar").remove();
        }
    },
    SuccessDispose: function (data) {
        /*
        此方法用于 其他 JS 需要页面加载 完成后 处理的 功能。
        */
    },
    LoadingErroe: function (XMLHttpRequest, textStatus, errorThrown) {
        //dialog({ content: "请联系技术人员", title: '错误!' }).show();
        //$("#LoadingProgressBar").attr("num", 0);
        //$.LoadingHide();
        return false;
    },
    timePeriod: function (length, defaultTime) {
        if (length > 0) {
            length = length - 1;
        } else if (length < 0) {
            length = length + 1;
        }

        var endTime = new Date();
        if (defaultTime != null) {
            if (defaultTime instanceof Date) {
                endTime = defaultTime;
            } else {
                endTime = new Date(defaultTime);
            }
        }
        var endTimestamp = endTime.getTime();
        var lengthTimestamp = length * (1000 * 60 * 60 * 24);
        var startTimestamp = endTimestamp + lengthTimestamp;
        var startTime = new Date(startTimestamp);
        return { "start": startTime, "end": endTime };
    },
    listPageHtml: function (NowPage, MaxPage, JsFuncName, iPageSize) {
        if (iPageSize == null) {
            iPageSize = 5;
        }
        var  pageHtml = "";
        var pageCnt = Number(MaxPage);
        NowPage = Number(NowPage);

        if (NowPage == 1)
        {
            pageHtml = "<li class='prev disabled'><a href='javascript:void(0)'><i class='pg-arrow_left'></i></a></li>";
        }
        else
        {
            pageHtml = "<li class='prev'><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + (NowPage - 1) + ")'><i class='pg-arrow_left'></i></a></li>";
        }

        if (pageCnt <= 10)
        {
            for (var  i = 1; i <= pageCnt; i++)
            {
                if (NowPage == i)
                {
                    pageHtml += "<li class='active'><a href='javascript:void(0)'>" + i + "</a></li>";
                }
                else
                {
                    pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + i + ")'>" + i + "</a></li>";
                }
            }
        }
        else
        {
            if (NowPage <= 6)
            {
                for ( var  i = 1; i <= 7; i++)
                {
                    if (NowPage == i)
                    {
                        pageHtml += "<li class='active'><a href='javascript:void(0)'>" + i + "</a></li>";
                    }
                    else
                    {
                        pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + i + ")'>" + i + "</a></li>";
                    }
                }
                pageHtml += "<li>...</li>";
                pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + (pageCnt - 1) + ")'>" + (pageCnt - 1) + "</a></li>";
                pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + pageCnt + ")'>" + pageCnt + "</a></li>";
            }
            else if (NowPage > 6 && NowPage <= (pageCnt - 6))
            {
                pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(1)'>1</a></li>";
                pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(2)'>2</a></li>";
                pageHtml += "<li>...</li>";
                for (var  i = -2; i <= 2; i++)
                {
                    if (i == 0)
                    {
                        pageHtml += "<li class='active'><a href='javascript:void(0)'>" + (NowPage + i) + "</a></li>";
                    }
                    else
                    {
                        pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + (NowPage + i) + ")'>" + (NowPage + i) + "</a></li>";
                    }
                }
                pageHtml += "<li>...</li>";
                pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + (pageCnt - 1) + ")'>" + (pageCnt - 1) + "</a></li>";
                pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + pageCnt + ")'>" + pageCnt + "</a></li>";
            }
            else if (NowPage > (pageCnt - 6) && NowPage <= pageCnt)
            {
                pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(1)'>1</a></li>";
                pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(2)'>2</a></li>";
                pageHtml += "...";
                for (var  i = -6; i <= 0; i++)
                {
                    if (NowPage == (pageCnt + i))
                    {
                        pageHtml += "<li class='active'><a href='javascript:void(0)'>" + (pageCnt + i) + "</a></li>";
                    }
                    else
                    {
                        pageHtml += "<li><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + (pageCnt + i) + ")'>" + (pageCnt + i) + "</a></li>";
                    }
                }
            }
        }

        if (NowPage == pageCnt)
        {
            pageHtml += "<li class='next disabled'><a href='javascript:void(0)'><i class='pg-arrow_right'></i></a></li>";
        }
        else
        {
            pageHtml += "<li  class='next'><a href='javascript:void(0)' onclick='" + JsFuncName + "(" + (NowPage + 1) + ")'><i class='pg-arrow_right'></i></a></li>";
        }

        return "<ul>" + pageHtml + "</ul>";
    }
});





//阻止事件冒泡的通用函数  
function stopBubble(e) {
    // 如果传入了事件对象，那么就是非ie浏览器  
    if (e && e.stopPropagation) {
        //因此它支持W3C的stopPropagation()方法  
        e.stopPropagation();
    } else {
        //否则我们使用ie的方法来取消事件冒泡  
        window.event.cancelBubble = true;
    }
}


function VMiddleImg(obj, options) {
    var defaults = {
        "width": null,
        "height": null
    };
    var opts = $.extend({}, defaults, options);
    return $(obj).each(function () {
        var $this = $(this);
        $this.css({ "width": "auto", "height": "auto" });

        var src = $this.attr("src");

        var objHeight = $this.height(); //图片高度
        var objWidth = $this.width(); //图片宽度
        var parentHeight = opts.height || $this.parent().height(); //图片父容器高度
        var parentWidth = opts.width || $this.parent().width(); //图片父容器宽度


        var ratio = objHeight / objWidth;
        if (objHeight > parentHeight && objWidth > parentWidth) {

            var wb = objWidth / parentWidth;
            var hb = objHeight / parentHeight;

            if (wb > hb) {

                objHeight = (objHeight / hb);
                objWidth = (objWidth / hb);

            } else {
                objWidth = (objWidth / wb);
                objHeight = (objHeight / wb);
            }


        } else if (objHeight > parentHeight) {
            objWidth = (parentHeight / ratio);
            objHeight = (parentHeight);
        }


        $this.css({
            "left": (parentWidth - objWidth) / 2,
            "top": (parentHeight - objHeight) / 2,
            "width": objWidth + "px",
            "height": objHeight + "px",
            opacity: 1, position: "inherit"
        });
    });
}