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
                    return "WASTE";
                }
                else
                {
                    return m_txtSet.ToString() + m_indexSet;
                }
            }
            set
            {
                switch (value)
                {
                    case "+1":
                        {
                            switch (m_txtSet)
                            {
                                case EnumCollIndexText.L:
                                    if (m_indexSet < m_countL)
                                    {
                                        m_indexSet += 1;
                                    }
                                    else
                                    {
                                        m_indexSet = 1;
                                        m_txtSet = EnumCollIndexText.R;
                                    }
                                    break;
                                case EnumCollIndexText.R:
                                    if (m_indexSet < m_countR)
                                    {
                                        m_indexSet += 1;
                                    }
                                    else
                                    {
                                        m_indexSet = 1;
                                        m_txtSet = EnumCollIndexText.L;
                                    }
                                    break;
                                case EnumCollIndexText.Out:
                                    if (m_indexSet < m_countL)
                                    {
                                        m_indexSet += 1;
                                    }
                                    else
                                    {
                                        m_indexSet = 1;
                                    }
                                    break;
                            }
                        }
                        break;
                    case "-1":
                        {
                            switch (m_txtSet)
                            {
                                case EnumCollIndexText.L:
                                    if (m_indexSet > 1)
                                    {
                                        m_indexSet -= 1;
                                    }
                                    else
                                    {
                                        m_indexSet = m_countR;
                                        m_txtSet = EnumCollIndexText.R;
                                    }
                                    break;
                                case EnumCollIndexText.R:
                                    if (m_indexSet > 1)
                                    {
                                        m_indexSet -= 1;
                                    }
                                    else
                                    {
                                        m_indexSet = m_countL;
                                        m_txtSet = EnumCollIndexText.L;
                                    }
                                    break;
                                case EnumCollIndexText.Out:
                                    if (m_indexSet  > 1)
                                    {
                                        m_indexSet -= 1;
                                    }
                                    else
                                    {
                                        m_indexSet = m_countL;
                                    }
                                    break;
                            }
                        }
                        break;
                    case "WASTE":
                        {
                            switch (m_txtSet)
                            {
                                case EnumCollIndexText.Out:
                                    m_indexSet = 0;
                                    break;
                            }
                        }
                        break;
                    default:
                        {
                            if (value.Contains("L"))
                            {
                                m_txtSet = EnumCollIndexText.L;
                                m_indexSet = Convert.ToInt32(value.Remove(0, 1));
                            }
                            else if (value.Contains("R"))
                            {
                                m_txtSet = EnumCollIndexText.R;
                                m_indexSet = Convert.ToInt32(value.Remove(0, 1));
                            }
                            else if (value.Contains("O"))
                            {
                                m_txtSet = EnumCollIndexText.Out;
                                m_indexSet = Convert.ToInt32(value.Remove(0, 3));
                            }
                            else
                            {
      
                            }
                        }
                        break;
                }

                OnPropertyChanged("MIndexSet");

                switch (m_txtSet)
                {
                    case EnumCollIndexText.Out:
                        switch (value)
                        {
                            case "WASTE":
                                MStatusSet = false;
                                break;
                            default:
                                MStatusSet = true;
                                break;
                        }
                        break;
                }
            }
        }     
        public string MIndexGet
        {
            get
            {
                if (0 == m_indexGet)
                {
                    return "WASTE";
                }
                else
                {
                    return m_txtGet.ToString() + m_indexGet;
                }
            }
            set
            {
                if (value.Contains("L"))
                {
                    m_txtGet = EnumCollIndexText.L;
                    m_indexGet = Convert.ToInt32(value.Remove(0, 1));
                }
                else if (value.Contains("R"))
                {
                    m_txtGet = EnumCollIndexText.R;
                    m_indexGet = Convert.ToInt32(value.Remove(0, 1));
                }
                else if (value.Contains("O"))
                {
                    m_txtGet = EnumCollIndexText.Out;
                    m_indexGet = Convert.ToInt32(value.Remove(0, 3));
                }
                else
                {

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

                switch (m_txtSet)
                {
                    case EnumCollIndexText.Out:
                        if (!value)
                        {
                            MIndexSet = "WASTE";
                        }
                        else
                        {
                            if (MIndexSet.Equals("WASTE"))
                            {
                                MIndexSet = m_txtSet.ToString() + m_indexGet;
                            }
                        }
                        break;
                }
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
    }
}
