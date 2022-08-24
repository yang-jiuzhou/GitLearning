using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Print
{
    class ExcelManager
    {
        /// <summary>
        /// 生成Excel文件(审计跟踪)
        /// </summary>
        /// <param name="dt">表格</param>
        /// <param name="path">存储路径</param>
        public bool SetValue(DataTable dt, string path)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(path, FileMode.OpenOrCreate);
                sw = new StreamWriter(fs, System.Text.Encoding.Default);

                DataColumnCollection columns = dt.Columns;      //表格的列数
                DataRowCollection rows = dt.Rows;               //表格的行数

                //写入标题  
                foreach (var it in columns)
                {
                    sw.Write(it.ToString() + "\t");
                }
                sw.Write("\n");

                //写入数值
                foreach (DataRow row in rows)
                {
                    for (int i = 1; i < columns.Count; i++)
                    {
                        sw.Write(row[columns[i].ColumnName].ToString().Replace("\n", " ") + "\t");
                    }
                    sw.Write("\n");
                }
            }
            catch
            {
                if (null != sw)
                {
                    sw.Flush();
                    sw.Close();
                }
                if (null != fs)
                {
                    fs.Close();
                }
                return false;
            }

            sw.Flush();
            sw.Close();
            fs.Close();
            return true;
        }

        /// <summary>
        /// 生成Excel文件(谱图数据)
        /// </summary>
        /// <param name="listList"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool SetValue(List<string> listCurveName, List<List<double>> listList, string path)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(path, FileMode.Create);
                sw = new StreamWriter(fs, System.Text.Encoding.Default);

                foreach(var it in listCurveName)
                {
                    sw.Write(it + "\t");
                }
                sw.Write("\n");
                //谱图数据
                for (int i = 0; i < listList[0].Count; i++)
                {
                    for (int j = 0; j < listList.Count; j++)
                    {
                        sw.Write(listList[j][i] + "\t");
                    }
                    sw.Write("\n");
                }
            }
            catch
            {
                if (null != sw)
                {
                    sw.Flush();
                    sw.Close();
                }
                if (null != fs)
                {
                    fs.Close();
                }
                return false;
            }

            sw.Flush();
            sw.Close();
            fs.Close();
            return true;
        }
    }
}
