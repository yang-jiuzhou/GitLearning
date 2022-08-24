using HBBio.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: SignerReviewerRow
     * Description: 签名审核行信息类
     * Version: 1.0
     * Create:  2021/01/08
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class SignerReviewerRow : DlyNotifyPropertyChanged
    {
        /// <summary>
        /// 签名审核项
        /// </summary>
        public EnumSignerReviewer MIndex { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public bool MSigner
        {
            get
            {
                return m_signer;
            }
            set
            {
                m_signer = value;
                OnPropertyChanged("MSigner");
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        public bool MReviewer
        {
            get
            {
                return m_reviewer;
            }
            set
            {
                m_reviewer = value;
                OnPropertyChanged("MReviewer");
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        private bool m_signer = false;
        /// <summary>
        /// 审核
        /// </summary>
        private bool m_reviewer = false;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        public SignerReviewerRow(EnumSignerReviewer index, string name, bool signer, bool reviewer)
        {
            MIndex = index;
            MName = name;
            MSigner = signer;
            MReviewer = reviewer;
        }
    }
}
