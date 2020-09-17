using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataModeling.Common
{
    public class CommonUtility
    {
        /// <summary>
        /// 返回一个新ID (string)
        /// </summary>
        /// <returns></returns>
        public static string NewID()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }


    }
}
