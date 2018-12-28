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

using System.Collections.Generic;
using System.Reflection;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;

namespace CatLib.ILRuntime.Redirect
{
    /// <summary>
    /// 重定向映射表
    /// </summary>
    internal sealed unsafe class RedirectMapping : MonoBehaviour
    {
        /// <summary>
        /// 函数签名信息
        /// </summary>
        private sealed class MethodSignature
        {
            /// <summary>
            /// 函数名
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 泛型参数数量
            /// </summary>
            public int GenericArgumentsCount { get; set; }

            /// <summary>
            /// 参数数量
            /// </summary>
            public int ParamsCount { get; set; }
            
            /// <summary>
            /// 重定向回调
            /// </summary>
            public CLRRedirectionDelegate Redirection { get; set; }
        }

        /// <summary>
        /// 映射表
        /// </summary>
        private readonly Dictionary<string, List<MethodSignature>> mapping;

        /// <summary>
        /// 构建一个重定向映射表
        /// </summary>
        public RedirectMapping()
        {
            mapping = new Dictionary<string, List<MethodSignature>>();
        }

        /// <summary>
        /// 注册一个函数签名信息
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="genericCount">泛型参数数量</param>
        /// <param name="paramsCount">参数数量</param>
        /// <param name="redirection">重定向方法</param>
        public void Register(string methodName, int genericCount, int paramsCount, CLRRedirectionDelegate redirection)
        {
            List<MethodSignature> list;
            if (!mapping.TryGetValue(methodName, out list))
            {
                mapping.Add(methodName,list = new List<MethodSignature>());
            }
            list.Add(new MethodSignature
            {
                Name = methodName,
                GenericArgumentsCount = genericCount,
                ParamsCount = paramsCount,
                Redirection = redirection
            });
        }

        /// <summary>
        /// 获取重定向的方法
        /// </summary>
        /// <param name="methodInfo">函数信息</param>
        /// <returns>重定向的方法</returns>
        public CLRRedirectionDelegate GetRedirection(MethodInfo methodInfo)
        {
            List<MethodSignature> list;
            if (!mapping.TryGetValue(methodInfo.Name, out list))
            {
                return null;
            }

            foreach (var methodSignature in list)
            {
                if (methodInfo.IsGenericMethod && methodInfo.GetGenericArguments().Length !=
                    methodSignature.GenericArgumentsCount)
                {
                    continue;
                }

                if (methodInfo.GetParameters().Length != methodSignature.ParamsCount)
                {
                    continue;
                }

                return methodSignature.Redirection;
            }

            return null;
        }
    }
}