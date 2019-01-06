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

using System;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

namespace CatLib.ILRuntime.Demo
{
    /// <summary>
    /// Assembly加载器
    /// </summary>
    public class LoaderAssembly : IDisposable
    {
        /// <summary>
        /// 是否已经完成
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// Dll数据
        /// </summary>
        public Stream Dll { get; set; }

        /// <summary>
        /// Pdb数据
        /// </summary>
        public Stream Pdb { get; set; }

        /// <summary>
        /// 调试等级
        /// </summary>
        private readonly DebugLevels debugLevels;

        /// <summary>
        /// 构建一个Assembly加载器
        /// </summary>
        public LoaderAssembly(DebugLevels debugLevels)
        {
            IsDone = false;
            this.debugLevels = debugLevels;
        }

        /// <summary>
        /// 加载指定的资源
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IEnumerator Load(string file)
        {
            using (var www = UnityWebRequest.Get(GetLoadPath(file) + ".dll"))
            {
                yield return www.SendWebRequest();
                if (!string.IsNullOrEmpty(www.error))
                {
                    throw new LogicException("Cannot Load Assembly : " + www.error);
                }

                Dll = new MemoryStream(www.downloadHandler.data);
            }

            if (debugLevels != DebugLevels.Production)
            {
                using (var www = UnityWebRequest.Get(GetLoadPath(file) + ".pdb"))
                {
                    yield return www.SendWebRequest();
                    if (!string.IsNullOrEmpty(www.error))
                    {
                        throw new LogicException("Cannot Load Assembly : " + www.error);
                    }

                    Dll = new MemoryStream(www.downloadHandler.data);
                }
            }
            IsDone = true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (Dll != null)
            {
                Dll.Dispose();
            }

            if (Pdb != null)
            {
                Pdb.Dispose();
            }
        }

        /// <summary>
        /// 获取加载路径
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>加载路径</returns>
        private string GetLoadPath(string file)
        {
#if UNITY_ANDROID
            return Application.streamingAssetsPath + "/" + file;
#else
            return "file:///" + UnityEngine.Application.streamingAssetsPath + "/" + file;
#endif
        }
    }
}