using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    /**
     * ClassName: DeepCopy
     * Description: 深拷贝类
     * Version: 1.0
     * Create:  2021/02/03
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    class DeepCopy
    {
        public static T DeepCopyByXml<T>(T obj)
        {
            object retval=null;
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    //序列化成流
                    bf.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    //反序列化成对象
                    retval = bf.Deserialize(ms);
                    ms.Close();
                }
                catch { }
            }
            return (T)retval;
        }

        public static byte[] GetMemoryStream<T>(T obj)
        {
            byte[] result = null;
            if (null == obj)
            {
                result = new byte[1];
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    try
                    {
                        IFormatter formatter = new BinaryFormatter();

                        formatter.Serialize(ms, obj);
                        ms.Seek(0, SeekOrigin.Begin);
                        result = ms.ToArray();
                        ms.Close();
                    }
                    catch
                    { }
                }
            }

            return result;
        }

        public static T SetMemoryStream<T>(byte[] arr)
        {
            object retval = null;
            using (MemoryStream ms = new MemoryStream(arr))
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    retval = formatter.Deserialize(ms);
                    ms.Close();
                }
                catch
                { }
            }
            return (T)retval;
        }

        public static T SetMemoryStream<T>(Stream ms)
        {
            object retval = null;
            try
            {
                if (null != ms)
                {
                    IFormatter formatter = new BinaryFormatter();
                    retval = formatter.Deserialize(ms);
                    ms.Close();
                }
            }
            catch
            { }
            return (T)retval;
        }
    }
}
