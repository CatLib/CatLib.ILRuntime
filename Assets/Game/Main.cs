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

using CatLib;
using CatLib.ILRuntime;
using Game.API.Debugger;
using UnityEngine;
using Application = CatLib.Application;

namespace Game
{
    /// <summary>
    /// 项目入口
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Main : Framework
    {
        public static void Make(string service)
        {
            App.Make("Game.Hotfix.ITestHotfix");
        }
        /// <summary>
        /// 项目入口
        /// </summary>
        protected override void OnStartCompleted()
        {
            var log = App.Make<IDebugger>();
            log.Log("hello OnStartCompleted");
        }

        /// <summary>
        /// 创建服务实例
        /// </summary>
        /// <param name="debugLevel"></param>
        /// <returns></returns>
        protected override Application CreateApplication(DebugLevels debugLevel)
        {
            return new ILRuntimeApplication(this)
            {
                DebugLevel = debugLevel
            };
        }

        /// <summary>
        /// 获取引导程序
        /// </summary>
        /// <returns>引导脚本</returns>
        protected override IBootstrap[] GetBootstraps()
        {
            return Arr.Merge(base.GetBootstraps(), Bootstraps.Bootstrap);
        }
    }
}
