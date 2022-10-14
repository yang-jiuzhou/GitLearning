using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.MethodEdit
{
    public class DefineQuestionsVM : DlyNotifyPropertyChanged
    {
        public DefineQuestions MItem
        { 
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;

                foreach (var it in value.MChoiceList)
                {
                    MChoiceList.Add(it);
                }
            }
        }
        private DefineQuestions m_item = new DefineQuestions();

        /// <summary>
        /// 问题描述
        /// </summary>
        public string MQuestion
        {
            get
            {
                return MItem.MQuestion;
            }
            set
            {
                MItem.MQuestion = value;
            }
        }
        /// <summary>
        /// 答案类型
        /// </summary>
        public EnumAnswerType MType
        {
            get
            {
                return MItem.MType;
            }
            set
            {
                MItem.MType = value;
            }
        }
        /// <summary>
        /// 默认答案
        /// </summary>
        public string MDefaultAnswer
        {
            get
            {
                return MItem.MDefaultAnswer;
            }
            set
            {
                MItem.MDefaultAnswer = value;
            }
        }
        /// <summary>
        /// 范围
        /// </summary>
        public double MMin
        {
            get
            {
                return MItem.MMin;
            }
            set
            {
                MItem.MMin = value;
            }
        }
        /// <summary>
        /// 范围
        /// </summary>
        public double MMax
        {
            get
            {
                return MItem.MMax;
            }
            set
            {
                MItem.MMax = value;
            }
        }
        /// <summary>
        /// 选择列表
        /// </summary>
        public ObservableCollection<string> MChoiceList { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public DefineQuestionsVM()
        {
            MChoiceList = new ObservableCollection<string>();
        }

        /// <summary>
        /// 列表增加
        /// </summary>
        /// <param name="item"></param>
        public void Add(string item)
        {
            MChoiceList.Add(item);
            MItem.MChoiceList.Add(item);
        }

        /// <summary>
        /// 列表删除
        /// </summary>
        /// <param name="index"></param>
        public void Delete(int index)
        {
            if (0 <= index && index < MChoiceList.Count)
            {
                MChoiceList.RemoveAt(index);
                MItem.MChoiceList.RemoveAt(index);
            }
        }

        /// <summary>
        /// 列表清除
        /// </summary>
        public void Clear()
        {
            MChoiceList.Clear();
            MItem.MChoiceList.Clear();
        }
    }
}
