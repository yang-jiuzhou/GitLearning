using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Chromatogram
{
    /**
    * ClassName: CurveSetStyleVM
    * Description: 曲线集合属性设置类
    * Version: 1.0
    * Create:  2021/05/21
    * Author:  yangjiuzhou
    * Company: jshanbon
    **/
    public class CurveSetStyleVM
    {
        #region 字段
        private double m_min = 0;
        private double m_max = 1;
        #endregion

        #region 属性
        public CurveSetStyle MModel { get; set; }
        public double MMin
        {
            get
            {
                return MModel.MMin;
            }
            set
            {
                MModel.MMin = value;
                ChangeAxisScale();
            }
        }
        public double MMax
        {
            get
            {
                return MModel.MMax;
            }
            set
            {
                MModel.MMax = value;
                ChangeAxisScale();
            }
        }
        public List<CurveStyleVM> MList { get; set; }
        public DelegateCommand MCommand { get; set; }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public CurveSetStyleVM(CurveSetStyle model)
        {
            MModel = model;
            m_min = model.MMin;
            m_max = model.MMax;
            MList = new List<CurveStyleVM>();
            foreach (var it in model.MList)
            {
                MList.Add(new CurveStyleVM(it));
            }

            MCommand = new DelegateCommand();
            MCommand.ExecuteCommand = new Action<object>(SelectAll);
        }

        /// <summary>
        /// 坐标变为固定
        /// </summary>
        private void ChangeAxisScale()
        {
            if (m_min != MModel.MMin || m_max != MModel.MMax)
            {
                MModel.MAxisScale = EnumAxisScale.Fixed;
            }
            else
            {
                MModel.MAxisScale = EnumAxisScale.Auto;
            }
        }

        /// <summary>
        /// 控制全部显隐
        /// </summary>
        /// <param name="obj"></param>
        private void SelectAll(object obj)
        {
            for (int i = 0; i < MList.Count; i++)
            {
                MList[i].MModel.MShow = (bool)obj;
            }
        }
    }

    /**
    * ClassName: CurveStyleVM
    * Description: 曲线属性设置类
    * Version: 1.0
    * Create:  2021/05/21
    * Author:  yangjiuzhou
    * Company: jshanbon
    **/
    public class CurveStyleVM
    {
        #region 字段
        private double m_min = 0;
        private double m_max = 1;
        #endregion

        #region 属性
        public CurveStyle MModel { get; set; }
        public double MMin
        {
            get
            {
                return MModel.MMin;
            }
            set
            {
                MModel.MMin = value;
                ChangeAxisScale();
            }
        }
        public double MMax
        {
            get
            {
                return MModel.MMax;
            }
            set
            {
                MModel.MMax = value;
                ChangeAxisScale();
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model"></param>
        public CurveStyleVM(CurveStyle model)
        {
            MModel = model;
            m_min = model.MMin;
            m_max = model.MMax;
        }

        /// <summary>
        /// 坐标变为固定
        /// </summary>
        private void ChangeAxisScale()
        {
            if (m_min != MModel.MMin || m_max != MModel.MMax)
            {
                MModel.MAxisScale = EnumAxisScale.Fixed;
            }
            else
            {
                MModel.MAxisScale = EnumAxisScale.Auto;
            }
        }
    }
}