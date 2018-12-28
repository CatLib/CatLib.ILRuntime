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
using System.IO;
using ILRuntime.Runtime.Intepreter;
using ILRuntimeDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace CatLib.ILRuntime
{
    /// <summary>
    /// ILRuntime AppDomin
    /// </summary>
    internal sealed class AppDomain
    {
        /// <summary>
        /// ILRuntime AppDomain
        /// </summary>
        private readonly ILRuntimeDomain appDomain;

        /// <summary>
        /// 构造一个ILRuntime Appdomain
        /// </summary>
        public AppDomain()
        {
            appDomain = new ILRuntimeDomain();
            RegisterDelegate();
            appDomain.GetType(typeof(ILRuntimeDomain));
            appDomain.RegisterCrossBindingAdaptor(new AdaptorIDebugger());

            Redirect.Redirect.ClrRedirect(appDomain);
            // global::ILRuntime.Runtime.Generated.CLRBindings.Initialize(appDomain);
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="dll">动态链接库</param>
        /// <param name="symbol">调试符</param>
        public void LoadAssembly(Stream dll, Stream symbol = null)
        {
            appDomain.LoadAssembly(dll, symbol, new Mono.Cecil.Pdb.PdbReaderProvider());
        }

        /// <summary>
        /// 调用入口方法
        /// </summary>
        /// <param name="type">类型全名</param>
        /// <param name="method">调用方法</param>
        /// <param name="instance">类型实例</param>
        /// <param name="p">传递参数</param>
        /// <returns></returns>
        public object Invoke(string type, string method, object instance, params object[] p)
        {
            return appDomain.Invoke(type, method, instance, p);
        }

        /// <summary>
        /// 创建热更新工程中的实例
        /// </summary>
        /// <param name="type">类型全名</param>
        /// <param name="args">构造函数参数</param>
        /// <returns></returns>
        public ILTypeInstance Instantiate(string type, object[] args = null)
        {
            return appDomain.Instantiate(type, args);
        }

        /// <summary>
        /// 注册委托关系
        /// </summary>
        private void RegisterDelegate()
        {
            // TODO: 等整理完成后重构
            // Action<IApplication>
            appDomain.DelegateManager.RegisterMethodDelegate<IApplication>();

            // Func<object>
            appDomain.DelegateManager.RegisterFunctionDelegate<object>();

            #region IBindData.cs
            // Func<IContainer, object[], object>
            appDomain.DelegateManager.RegisterFunctionDelegate<IContainer, object[], object>();
            // Func<IBindData, object>
            appDomain.DelegateManager.RegisterMethodDelegate<IBindData, object>();
            #endregion

            #region IContainer.cs
            // Func<object, IContainer, object>
            appDomain.DelegateManager.RegisterFunctionDelegate<object, IContainer, object>();
            appDomain.DelegateManager.RegisterFunctionDelegate<string, Type>();
            #endregion
        }
    }
}