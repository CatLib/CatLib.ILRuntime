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
        /// 应用程序
        /// </summary>
        protected IApplication Application { get; private set; }

        /// <summary>
        /// 是否已经完成了初始化
        /// </summary>
        private bool inited;

        /// <summary>
        /// 构造一个ILRuntime Appdomain
        /// </summary>
        /// <param name="application">应用程序</param>
        /// <param name="debugLevel">调试等级</param>
        public AppDomain(IApplication application, DebugLevels debugLevel)
        {
            Domain = new ILRuntimeDomain();
            DebugLevel = debugLevel;
            Application = application;
            inited = false;

            RegisterDefaultDelegate();
            RegisterRedirect.Register(Domain);
            RegisterAdapter.Register(Domain);
        }

        /// <summary>
        /// 热更新代码初始化
        /// </summary>
        /// <param name="main">入口函数</param>
        public void Init(string main)
        {
            if(inited)
            {
                throw new CodeStandardException("Repeated Init() is not allowed.");
            }

            CallMain(main);
        }

        /// <summary>
        /// 调用初始化函数
        /// </summary>
        /// <param name="main">入口函数</param>
        protected virtual void CallMain(string main)
        {
            var method = Str.Method(main);
            var type = main.Substring(0, main.Length - method.Length).TrimEnd('.');

            inited = true;

            var application = Application as ILRuntimeApplication;
            if (application != null)
            {
                application.DeferInitServiceProvider(
                    () => Invoke(type, method, null, Application));
                return;
            }

            Invoke(type, method, null, Application);
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="dll">动态链接库</param>
        /// <param name="symbol">调试符</param>
        public virtual void LoadAssembly(Stream dll, Stream symbol = null)
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
        public virtual object Invoke(string type, string method, object instance, params object[] @params)
        {
            AssertInited();
            return Domain.Invoke(type, method, instance, @params);
        }

        /// <summary>
        /// 创建热更新工程中的实例
        /// </summary>
        /// <param name="type">类型全名</param>
        /// <param name="args">构造函数参数</param>
        /// <returns></returns>
        public virtual object CreateInstance(string type, object[] args = null)
        {
            AssertInited();
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
        /// 将CatLib AppDomain转为ILRuntime的AppDomain
        /// </summary>
        /// <param name="domain">CatLib AppDomain</param>
        public static implicit operator ILRuntimeDomain(AppDomain domain)
        {
            return domain.Domain;
        }

        /// <summary>
        /// 断言已经被初始化
        /// </summary>
        protected virtual void AssertInited()
        {
            if(!inited)
            {
                throw new CodeStandardException("Please Init() first.");
            }
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
            // Action<ILTypeInstance>
            RegisterActionDelegate<ILTypeInstance>();
            // Action<IBindData, ILTypeInstance>
            RegisterActionDelegate<IBindData, ILTypeInstance>();
            #endregion
        }
    }
}
