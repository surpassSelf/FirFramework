using System;

namespace Utility
{
    public static class AppUtil
    {
        public static int Random(int min, int max)
        {
            var ran = new System.Random();
            return ran.Next(min, max);
        }

        /// <summary>
        /// 产生UID
        /// </summary>
        /// <returns></returns>
        public static long NewGuidId()
        {
            var buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
