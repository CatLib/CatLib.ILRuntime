/*
 * This file is part of the CatLib package.
 *
 * (c) CatLib <support@catlib.io>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * Document: https://catlib.io/
 */

namespace Game.Hotfix
{
    /// <summary>
    /// 帮助库
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// 输出失败提示
        /// </summary>
        /// <param name="name"></param>
        public static void Faild(string name, string context = null)
        {
            if (string.IsNullOrEmpty(context))
            {
                context = string.Empty;
            }
            else
            {
                context = " : " + context;
            }

            UnityEngine.Debug.LogError($"[<color=#ff0000>Faild</color>] {name}{context}");
        }

        /// <summary>
        /// 输出成功提示
        /// </summary>
        /// <param name="name"></param>
        public static void Success(string name, string context = null)
        {
            if (string.IsNullOrEmpty(context))
            {
                context = string.Empty;
            }
            else
            {
                context = " : " + context;
            }

            UnityEngine.Debug.Log($"[<color=#00ff00>Success</color>] {name}{context}");
        }

        /// <summary>
        /// 输出常规内容
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}
