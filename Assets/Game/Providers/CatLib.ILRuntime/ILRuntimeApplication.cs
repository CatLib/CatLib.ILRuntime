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
using ILRuntime.Reflection;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;

namespace CatLib.ILRuntime
{
    /// <summary>
    /// ILRuntime Application
    /// </summary>
    public class ILRuntimeApplication : Application
    {
        /// <summary>
        /// behaviour
        /// </summary>
        private readonly MonoBehaviour behaviour;

        /// <summary>
        /// App Domain
        /// </summary>
        private AppDomain appDomain;

        /// <summary>
        /// Appdomain
        /// </summary>
        internal AppDomain AppDomain
        {
            get { return appDomain ?? (appDomain = App.Make<AppDomain>()); }   
        }

        /// <summary>
        /// 构建一个ILRuntime Application实例
        /// </summary>
        /// <param name="behaviour">Unity主MonoBehaviour</param>
        public ILRuntimeApplication(MonoBehaviour behaviour)
            : base(behaviour)
        {
            if (behaviour == null)
            {
                return;
            }

            this.Singleton<MonoBehaviour>(() => behaviour)
                .Alias<Component>();
            this.behaviour = behaviour;

            App.Extend<ILTypeInstance>(ExtendILRuntimeInstance);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="makeServiceType">需求构建的类型</param>
        /// <param name="userParams">构造函数参数信息</param>
        /// <returns>创建的实例</returns>
        protected override object CreateInstance(Type makeServiceType, object[] userParams)
        {
            if (makeServiceType is ILRuntimeType || makeServiceType is ILRuntimeWrapperType)
            {
                return AppDomain.Instantiate(makeServiceType.FullName, userParams);
            }

            return base.CreateInstance(makeServiceType, userParams);
        }

        /// <summary>
        /// 对所有为ILRuntimeInstance类型的实例进行扩展
        /// </summary>
        /// <param name="instance">输入实例</param>
        /// <returns>输出值</returns>
        protected object ExtendILRuntimeInstance(ILTypeInstance instance)
        {
            return instance.CLRInstance;
        }

        /// <summary>
        /// 初始化服务提供者
        /// </summary>
        public override void Init()
        {
            if (behaviour)
            {
                behaviour.StartCoroutine(CoroutineInit());
                return;
            }
            base.Init();
        }

        /// <summary>
        /// 注册服务提供者
        /// </summary>
        /// <param name="provider">服务提供者</param>
        public override void Register(IServiceProvider provider)
        {
            if (behaviour)
            {
                behaviour.StartCoroutine(CoroutineRegister(provider));
                return;
            }
            base.Register(provider);
        }
    }
}
