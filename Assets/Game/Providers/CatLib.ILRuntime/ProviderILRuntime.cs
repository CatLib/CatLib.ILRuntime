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

using System.Collections;
using CatLib.API.ILRuntime;

namespace CatLib.ILRuntime
{
    /// <summary>
    /// ILRuntime服务
    /// </summary>
    public class ProviderILRuntime : ServiceProvider
    {
        /// <summary>
        /// 入口函数名
        /// </summary>
        public string Main { get; set; }

        /// <summary>
        /// 获取需要加载的程序集
        /// </summary>
        /// <returns></returns>
        public string[] GetAssemblies()
        {
            return new string[]
            {
                "Game.Hotfix"
            };
        }

        /// <summary>
        /// 构造一个ILRuntime服务
        /// </summary>
        public ProviderILRuntime()
        {
            Main = "Game.Hotfix.Program.Main";
        }
        
        /// <summary>
        /// 初始化服务
        /// </summary>
        public override void Init()
        {
            App.On(ApplicationEvents.OnInited, () =>
            {
                var domain = App.Make<IAppDomain>();
                var method = Str.Method(Main);
                var type = Main.Substring(0, Main.Length - method.Length).TrimEnd('.');

                var application = App.Handler as ILRuntimeApplication;
                if (application != null)
                {
                    application.DeferInitServiceProvider(
                        () => domain.Invoke(type, method, null, App.Handler));
                    return;
                }

                domain.Invoke(type, method, null, App.Handler);
            });
        }

        /// <summary>
        /// 迭代器初始化
        /// </summary>
        /// <returns>迭代器</returns>
        public override IEnumerator CoroutineInit()
        {
            var domain = App.Make<IAppDomain>() as AppDomain;
            if (domain == null)
            {
                yield break;
            }

            foreach (var assembly in GetAssemblies())
            {
                using (var loader = App.Make<LoaderAssembly>())
                {
                    yield return loader.Load(assembly);
                    domain.LoadAssembly(loader.Dll, loader.Pdb);
                }
            }
        }

        /// <summary>
        /// 注册服务提供者
        /// </summary>
        public override void Register()
        {
            App.Singleton<IAppDomain, AppDomain>();
            App.Bind<LoaderAssembly>();
        }
    }
}