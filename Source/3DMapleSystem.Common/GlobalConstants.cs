using System;
using System.Linq;

namespace _3DMapleSystem.Common
{
    public class GlobalConstants
    {
        public const string PathDefaultImage = "~/Content/Images/defaultPhoto.jpg";

        public const string AdminRole = "Admin";

        //public const string ValidPreviewType = "image/jpeg";

        public static readonly string[] ValidPreviewTypes = { "image/jpeg", "image/jpg" };

        public const int MaxPreviewFileLength = 1 * 1024 * 1024;

        public const int Max3DModelFileLength = 50 * 1024 * 1024;

        public const string DefaultRank = "free";

        public const int DefaultFreeModels = 3;

        public const int DefaultProModels = 0;

        //public const int ColumnsInMainPage = 4;
    }
}
