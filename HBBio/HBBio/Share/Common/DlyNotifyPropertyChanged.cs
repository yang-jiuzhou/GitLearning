using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    /**
     * ClassName: DlyNotifyPropertyChanged
     * Description: 绑定的依赖
     * Version: 1.0
     * Create:  2021/02/03
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    [Serializable]
    public class DlyNotifyPropertyChanged : INotifyPropertyChanged
    {
        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged(string name)
        {
            if (null != PropertyChanged)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }    
        }
    }
}
