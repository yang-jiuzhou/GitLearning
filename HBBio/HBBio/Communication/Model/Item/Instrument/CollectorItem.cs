using HBBio.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HBBio.Communication
{
    public class CollectorItem : BaseInstrument
    {
        public int m_countL = 60;
        public int m_countR = 60;
        public int m_indexSet = 1;
        public int m_indexGet = 1;
        public EnumCollIndexText m_txtSet = EnumCollIndexText.L;
        public EnumCollIndexText m_txtGet = EnumCollIndexText.L;
        public bool m_ingSet = false;
        public bool m_ingGet = false;

        public readonly object m_locker = new object();

        public string MShowSet
        {
            get
            {
                if (0 == m_indexSet)
                {
                    return "WASTE";
                }
                else
                {
                    if (m_ingSet)
                    {
                        return m_txtSet.ToString() + m_indexSet;
                    }
                    else
                    {
                        return m_txtSet.ToString() + m_indexSet + "(WASTE)";
                    }
                }
            }
        }
        public string MShowGet
        {
            get
            {
                if (0 == m_indexGet)
                {
                    return "WASTE";
                }
                else
                {
                    if (m_ingGet)
                    {
                        return m_txtGet.ToString() + m_indexGet;
                    }
                    else
                    {
                        return m_txtGet.ToString() + m_indexGet + "(WASTE)";
                    }
                }
            }
        }

        public string MIndexSet
        {
            get
            {
                if (0 == m_indexSet)
                {
                    return "WASTE(" + m_txtSet.ToString() + m_indexSet + ")";
                }
                else
                {
                    return m_txtSet.ToString() + m_indexSet;
                }
            }
            set
            {
                if (value.Contains("WASTE"))
                {
                    string newValue = value.Replace("WASTE(", "").Replace(")", "");
                    if (newValue.Contains("L"))
                    {
                        m_txtSet = EnumCollIndexText.L;
                        m_indexSet = Convert.ToInt32(newValue.Remove(0, 1));
                    }
                    else
                    {
                        m_txtSet = EnumCollIndexText.R;
                        m_indexSet = Convert.ToInt32(newValue.Remove(0, 1));
                    }
                }
                else if (value.Contains("L"))
                {
                    m_txtSet = EnumCollIndexText.L;
                    m_indexSet = Convert.ToInt32(value.Remove(0, 1));
                }
                else if (value.Contains("R"))
                {
                    m_txtSet = EnumCollIndexText.R;
                    m_indexSet = Convert.ToInt32(value.Remove(0, 1));
                }

                OnPropertyChanged("MIndexSet");
            }
        }     
        public string MIndexGet
        {
            get
            {
                if (0 == m_indexGet)
                {
                    return "WASTE(" + m_txtGet.ToString() + m_indexGet + ")";
                }
                else
                {
                    return m_txtGet.ToString() + m_indexGet;
                }
            }
            set
            {
                if (value.Contains("WASTE"))
                {
                    string newValue = value.Replace("WASTE(", "").Replace(")", "");
                    if (newValue.Contains("L"))
                    {
                        m_txtGet = EnumCollIndexText.L;
                        m_indexGet = Convert.ToInt32(newValue.Remove(0, 1));
                    }
                    else
                    {
                        m_txtGet = EnumCollIndexText.R;
                        m_indexGet = Convert.ToInt32(newValue.Remove(0, 1));
                    }
                }
                else if (value.Contains("L"))
                {
                    m_txtGet = EnumCollIndexText.L;
                    m_indexGet = Convert.ToInt32(value.Remove(0, 1));
                }
                else if (value.Contains("R"))
                {
                    m_txtGet = EnumCollIndexText.R;
                    m_indexGet = Convert.ToInt32(value.Remove(0, 1));
                }

                OnPropertyChanged("MIndexGet");
            }
        }
        public bool MStatusSet
        {
            get
            {
                return m_ingSet;
            }
            set
            {
                if (m_ingSet == value)
                {
                    return;
                }

                m_ingSet = value;
                OnPropertyChanged("MStatusSet");
            }
        }
        public bool MStatusGet
        {
            get
            {
                return m_ingGet;
            }
            set
            {
                m_ingGet = value;
                OnPropertyChanged("MStatusGet");
            }
        }


        public CollectorItem()
        {
            MConstNameList = Enum.GetNames(typeof(ENUMCollectorName));
            MConstName = MConstNameList[0];
        }

        public CollTextIndex GetCurr()
        {
            CollTextIndex curr = new CollTextIndex();
            curr.MText = m_txtSet;
            curr.MIndex = m_indexSet;
            curr.MStatus = m_ingSet;

            return curr;
        }

        public CollTextIndex GetAdd()
        {
            CollTextIndex curr = new CollTextIndex();
            curr.MText = m_txtSet;
            switch (m_txtSet)
            {
                case EnumCollIndexText.L:
                    if (m_indexSet < m_countL)
                    {
                        curr.MIndex = m_indexSet + 1;
                    }
                    else
                    {
                        curr.MText = EnumCollIndexText.R;
                        curr.MIndex = 1;
                    }
                    break;
                case EnumCollIndexText.R:
                    if (m_indexSet < m_countR)
                    {
                        curr.MText = EnumCollIndexText.R;
                        curr.MIndex = m_indexSet + 1;
                    }
                    else
                    {
                        curr.MIndex = 1;
                    }
                    break;
            }
            curr.MStatus = m_ingSet;

            return curr;
        }

        public CollTextIndex GetDel()
        {
            CollTextIndex curr = new CollTextIndex();
            curr.MText = m_txtSet;
            switch (m_txtSet)
            {
                case EnumCollIndexText.L:
                    if (m_indexSet > 1)
                    {
                        curr.MIndex = m_indexSet - 1;
                    }
                    else
                    {
                        curr.MIndex = m_countR;
                        curr.MText = EnumCollIndexText.R;
                    }
                    break;
                case EnumCollIndexText.R:
                    if (m_indexSet > 1)
                    {
                        curr.MIndex = m_indexSet - 1;
                    }
                    else
                    {
                        curr.MIndex = m_countL;
                        curr.MText = EnumCollIndexText.L;
                    }
                    break;
            }
            curr.MStatus = m_ingSet;

            return curr;
        }
    }
}
