using System;
using System.Collections.Generic;
using System.Text;

namespace MetadataModeling.Base
{
    public interface ICopy
    {
        /// <summary>
        /// GetCopyDatas
        /// </summary>
        /// <returns></returns>
        List<CopyData> GetCopyDatas();
    }
}
