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
using CatLib.API.ILRuntime;
using CatLib.ILRuntime.Adapter;
using CatLib.ILRuntime.Redirect;
using ILRuntime.Runtime.Intepreter;
using ILRuntimeDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace CatLib.ILRuntime
{
    /// <summary>
    /// ILRuntime AppDomin
    /// </summary>
    public class AppDomain : IAppDomain
    {
        /// <summary>
        /// AppDomain
        /// </summary>
        protected ILRuntimeDomain Domain { get; private set; }

        /// <summary>
        /// 调试等级
        /// </summary>
        protected DebugLevels DebugLevel { get; private set; }

        /// <summary>
        /// 构造一个ILRuntime Appdomain
        /// </summary>
        /// <param name="debugLevel">调试等级</param>
        public AppDomain(DebugLevels debugLevel)
        {
            Domain = new ILRuntimeDomain();
            DebugLevel = debugLevel;

            RegisterDefaultDelegate();
            RegisterRedirect.Register(Domain);
            RegisterAdapter.Register(Domain);

            // TODO：注册clr绑定
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="dll">动态链接库</param>
        /// <param name="symbol">调试符</param>
        public void LoadAssembly(Stream dll, Stream symbol = null)
        {
            if (DebugLevel == DebugLevels.Production)
            {
                symbol = null;
            }
            Domain.LoadAssembly(dll, symbol, new Mono.Cecil.Pdb.PdbReaderProvider());
        }

        /// <summary>
        /// 调用热更新中的方法
        /// </summary>
        /// <param name="type">类型全名</param>
        /// <param name="method">调用方法</param>
        /// <param name="instance">类型实例</param>
        /// <param name="params">传递参数</param>
        /// <returns></returns>
        public object Invoke(string type, string method, object instance, params object[] @params)
        {
            return Domain.Invoke(type, method, instance, @params);
        }

        /// <summary>
        /// 创建热更新工程中的实例
        /// </summary>
        /// <param name="type">类型全名</param>
        /// <param name="args">构造函数参数</param>
        /// <returns></returns>
        public object CreateInstance(string type, object[] args = null)
        {
            return Domain.Instantiate(type, args);
        }

        /// <summary>
        /// 注册Action委托
        /// </summary>
        public void RegisterActionDelegate<T1>()
        {
            Domain.DelegateManager.RegisterMethodDelegate<T1>();
        }

        /// <summary>
        /// 注册Action委托
        /// </summary>
        public void RegisterActionDelegate<T1, T2>()
        {
            Domain.DelegateManager.RegisterMethodDelegate<T1, T2>();
        }

        /// <summary>
        /// 注册Action委托
        /// </summary>
        public void RegisterActionDelegate<T1, T2, T3>()
        {
            Domain.DelegateManager.RegisterMethodDelegate<T1, T2, T3>();
        }

        /// <summary>
        /// 注册Action委托
        /// </summary>
        public void RegisterActionDelegate<T1, T2, T3, T4>()
        {
            Domain.DelegateManager.RegisterMethodDelegate<T1, T2, T3, T4>();
        }

        /// <summary>
        /// 注册Func委托
        /// </summary>
        public void RegisterFuncDelegate<TResult>()
        {
            Domain.DelegateManager.RegisterFunctionDelegate<TResult>();
        }

        /// <summary>
        /// 注册Func委托
        /// </summary>
        public void RegisterFuncDelegate<T1, TResult>()
        {
            Domain.DelegateManager.RegisterFunctionDelegate<T1, TResult>();
        }

        /// <summary>
        /// 注册Func委托
        /// </summary>
        public void RegisterFuncDelegate<T1, T2, TResult>()
        {
            Domain.DelegateManager.RegisterFunctionDelegate<T1, T2, TResult>();
        }

        /// <summary>
        /// 注册Func委托
        /// </summary>
        public void RegisterFuncDelegate<T1, T2, T3, TResult>()
        {
            Domain.DelegateManager.RegisterFunctionDelegate<T1, T2, T3, TResult>();
        }

        /// <summary>
        /// 注册Func委托
        /// </summary>
        public void RegisterFuncDelegate<T1, T2, T3, T4, TResult>()
        {
            Domain.DelegateManager.RegisterFunctionDelegate<T1, T2, T3, T4, TResult>();
        }

        /// <summary>
        /// 注册委托转换器
        /// </summary>
        /// <param name="action">转换器实现</param>
        public void RegisterDelegateConvertor<T>(Func<Delegate, Delegate> action)
        {
            Domain.DelegateManager.RegisterDelegateConvertor<T>(action);
        }

        /// <summary>
        /// 注册框架默认的委托关系
        /// </summary>
        private void RegisterDefaultDelegate()
        {
            #region Func
            // Func<object>
            RegisterFuncDelegate<object>();
            // Func<string, object[], object>
            RegisterFuncDelegate<string, object[], object>();
            // Func<IContainer, object[], object>
            RegisterFuncDelegate<IContainer, object[], object>();
            // Func<object, IContainer, object>
            RegisterFuncDelegate<object, IContainer, object>();
            // Func<string, Type>
            RegisterFuncDelegate<string, Type>();
            // Func<object, object>
            RegisterFuncDelegate<object, object>();
            // Func<object[], object>
            RegisterFuncDelegate<object[], object>();
            // Func<ILTypeInstance, IContainer, Object>
            RegisterFuncDelegate<ILTypeInstance, IContainer, object>();
            // Func<ILTypeInstance, Object>
            RegisterFuncDelegate<ILTypeInstance, object>();

            #endregion

            #region Action
            // Action<IApplication>
            RegisterActionDelegate<IApplication>();
            // Action<IBindData, object>
            RegisterActionDelegate<IBindData, object>();
            // Action<object>
            RegisterActionDelegate<object>();
            #endregion
        }
    }
}