using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    /**
     * ClassName: StringBuilderSplit
     * Description: 字符串拼接
     * Version: 1.0
     * Create:  2021/02/03
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class StringBuilderSplit
    {
        private StringBuilder m_sb = new StringBuilder();
        private string m_split = null;

        public int MLength
        {
            get
            {
                return m_sb.Length;
            }
        }


        public StringBuilderSplit(string split = "\n")
        {
            m_split = split;
        }

        public void Append(object value)
        {
            if (!string.IsNullOrEmpty(value.ToString()) || !m_split.Equals("\n"))
            {
                m_sb.Append(value);
                m_sb.Append(m_split);
            }
        }

        public void AppendLast(object value)
        {
            if (m_split.Equals("\n"))
            {
                if (m_sb[m_sb.Length - 1] == '\n')
                {
                    m_sb.Remove(m_sb.Length - 1, 1);
                }
            }
            m_sb.Append(";" + value);
            m_sb.Append(m_split);
        }

        public void AppendLeftBracket()
        {
            m_sb.Append("{ ");
        }

        public void AppendRightBracket()
        {
            if (0 != m_sb.Length && m_split.Equals("\n"))
            {
                m_sb.Remove(m_sb.Length - 1, 1);
                m_sb.Append("}\n");
            }
        }

        public void Clear()
        {
            m_sb.Clear();
        }

        public override string ToString()
        {
            if (0 != m_sb.Length && m_split.Equals("\n"))
            {
                return m_sb.ToString().Remove(m_sb.Length - 1, 1);
            }
            else if (0 == m_sb.Length)
            {
                return "";
            }
            else
            {
                return m_sb.ToString();
            }
        }
    }
}
