using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.WindowSize
{
    /**
     * ClassName: WindowSizeManager
     * Description: 窗体宽高控制
     * Version: 1.0
     * Create:  2022/02/21
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    public class WindowSizeManager
    {
        public static bool s_RememberSize = false;

        public bool AddWindowSize(string name, double x, double y, double height, double width)
        {
            WindowSizeTable table = new WindowSizeTable();
            if (null == table.InsertRow(name, x, y, height, width))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetWindowSize(string name, out double x, out double y, out double height, out double width)
        {
            WindowSizeTable table = new WindowSizeTable();
            if (null == table.SelectRow(name, out x, out y, out height, out width))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateWindowSize(string name, double x, double y, double height, double width)
        {
            WindowSizeTable table = new WindowSizeTable();
            if (null == table.UpdateRow(name, x, y, height, width))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
