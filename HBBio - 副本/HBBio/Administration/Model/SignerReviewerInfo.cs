using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Administration
{
    /**
     * ClassName: SignerReviewerInfo
     * Description: 签名审核类
     * Version: 1.0
     * Create:  2020/05/16
     * Author:  yangjiuzhou
     * Company: hanbon
     **/
    public class SignerReviewerInfo
    {
        /// <summary>
        /// 签名列表
        /// </summary>
        public List<bool> MListSigner { get; }
        /// <summary>
        /// 审核列表
        /// </summary>
        public List<bool> MListReviewer { get; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public SignerReviewerInfo()
        {
            MListSigner = new List<bool>();
            MListReviewer = new List<bool>();
            for (int i = 0; i < Enum.GetNames(typeof(EnumSignerReviewer)).GetLength(0); i++)
            {
                MListSigner.Add(false);
                MListReviewer.Add(false);
            }
        }       
    }
}
