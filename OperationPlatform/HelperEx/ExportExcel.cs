using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OperationPlatform.HelperEx
{
    /*
     * ExportExcel export = new ExportExcel(subject, className, colName);
     * string strFileName = export.ExportFile(model.accid, dtTable, model.webPath);
     */
    /// <summary>
    /// 导出数据
    /// </summary>
    public class ExportExcel
    {
        private readonly HSSFWorkbook _workbook;
        private readonly Dictionary<string, string> _colName;
        private readonly Sheet _sheet;
        private readonly string _className;

        public HSSFWorkbook Workbook
        {
            get { return _workbook; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="subject">导出标题</param>
        /// <param name="className">子目录</param>
        /// <param name="colName">列名中文对照</param>
        public ExportExcel(string subject, string className, Dictionary<string, string> colName)
        {
            this._workbook = new HSSFWorkbook();
            this._sheet = this.Workbook.CreateSheet(subject);
            this._colName = colName;
            this.SetFileInfo();
            this._className = className;
        }

        /// <summary>
        /// 设置Workbook的2个属性信息
        /// </summary>
        private void SetFileInfo()
        {
            //设置Workbook的DocumentSummaryInformation信息
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "生意专家(www.i200.cn)";
            this.Workbook.DocumentSummaryInformation = dsi;

            //设置Workbook的SummaryInformation信息
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "生意专家(www.i200.cn) 导出数据";
            this.Workbook.SummaryInformation = si;
        }

        /// <summary>
        /// 导出DataTable到Excel文件
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <param name="dtTable">数据DataTable</param>
        /// <returns>文件保存路径</returns>
        public string ExportFile(string filename, List<dynamic> data, string webPath = "")
        {
            NPOI.SS.UserModel.Row rowItem = null;
            int rowCnt = 0;

            foreach (dynamic dr in data )
            {
                if (rowCnt == 0)
                {
                    //创建表头
                    rowItem = _sheet.CreateRow(rowCnt);
                    int i = 0;
                    foreach (KeyValuePair<string, object> dynamicItem in dr)
                    {
                        if (_colName.ContainsKey(dynamicItem.Key))
                        {
                            rowItem.CreateCell(i).SetCellValue(_colName[dynamicItem.Key]);
                        }
                        else
                        {
                            rowItem.CreateCell(i).SetCellValue(dynamicItem.Key);
                        }
                        i++;
                    }
                    rowCnt = rowCnt + 1;
                }

                rowItem = _sheet.CreateRow(rowCnt);

                int k = 0;
                foreach (KeyValuePair<string, object> dynamicItem in dr)
                {
                    string sItemVal = "";
                    if (dynamicItem.Value != null)
                    {
                        sItemVal = dynamicItem.Value.ToString().Trim();
                    }
                    rowItem.CreateCell(k).SetCellValue(sItemVal);
                    k++;
                }

                rowCnt = rowCnt + 1;
            }

            //保存文件
            if (webPath == "")
            {
                webPath = System.Web.HttpContext.Current.Server.MapPath("/");
            }
            //文件名
            string strFileName = string.Format("{0}_{1}_{2}.xls", filename.ToString(),
                DateTime.Now.ToString("yyyyMMddHHmm"), CommonLib.Helper.GetRandomNum());
            //文件路径
            string strFilePath = "\\ExportFile\\" + _className + "\\" + DateTime.Now.ToString("yyyyMM") + "\\";
            if (!Directory.Exists(webPath + strFilePath))
            {
                Directory.CreateDirectory(webPath + strFilePath);
            }
            var fileInfo = new FileStream(webPath + strFilePath + strFileName, FileMode.Create);
            Workbook.Write(fileInfo);
            fileInfo.Close();

            return strFilePath + strFileName;
        }
    }
}