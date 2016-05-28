/**
 * Created by visual studio 2010
 * User: xuheng
 * Date: 12-3-6
 * Time: 下午21:23
 * To get the value of editor and output the value .
 */
using System;
using System.Web;

namespace Script_BackStage.sysAdmin.Ueditor.net
{

    public class getContent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            //获取数据
            string content = context.Server.HtmlEncode(context.Request.Form["myEditor"]);


            //存入数据库或者其他操作
            //-------------

            //显示
            context.Response.Write("<script src='/sysAdmin/Ueditor/ueditor.parse.js' type='text/javascript'></script>");
            context.Response.Write(
                "<script>uParse('.content',{" +
                      "'highlightJsUrl':'/sysAdmin/Ueditor/third-party/SyntaxHighlighter/shCore.js'," +
                      "'highlightCssUrl':'/sysAdmin/Ueditor/third-party/SyntaxHighlighter/shCoreDefault.css'" +
                  "})" +
                "</script>");

            context.Response.Write("第1个编辑器的值");
            context.Response.Write("<div class='content'>" + context.Server.HtmlDecode(content) + "</div>");

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}