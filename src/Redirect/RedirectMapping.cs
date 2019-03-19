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
using System.Collections.Generic;
using System.Reflection;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;

namespace CatLib.ILRuntime.Redirect
{
    /// <summary>
    /// 重定向映射表
    /// </summary>
    internal sealed unsafe class RedirectMapping
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
            public int ParamCount { get; set; }

            /// <summary>
            /// 参数类型
            /// </summary>
            public string[] ParamTypes { get; set; }

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
            GetMethodList(methodName).Add(new MethodSignature
            {
                Name = methodName,
                GenericArgumentsCount = genericCount,
                ParamCount = paramsCount,
                Redirection = redirection
            });
        }

        /// <summary>
        /// 注册一个函数签名信息
        /// </summary>
        /// <param name="methodName">函数名</param>
        /// <param name="genericCount">泛型参数数量</param>
        /// <param name="paramsCount">参数数量</param>
        /// <param name="paramsTypes">参数类型</param>
        /// <param name="redirection">重定向</param>
        public void Register(string methodName, int genericCount, int paramsCount, string[] paramsTypes,
            CLRRedirectionDelegate redirection)
        {
            GetMethodList(methodName).Add(new MethodSignature
            {
                Name = methodName,
                GenericArgumentsCount = genericCount,
                ParamCount = paramsCount,
                Redirection = redirection,
                ParamTypes = paramsTypes
            });
        }

        /// <summary>
        /// 获取函数列表
        /// </summary>
        /// <param name="methodName">函数名</param>
        /// <returns>函数列表</returns>
        private IList<MethodSignature> GetMethodList(string methodName)
        {
            List<MethodSignature> list;
            if (!mapping.TryGetValue(methodName, out list))
            {
                mapping.Add(methodName, list = new List<MethodSignature>());
            }
            return list;
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

                if (methodInfo.GetParameters().Length != methodSignature.ParamCount)
                {
                    continue;
                }

                if (methodSignature.ParamTypes == null)
                {
                    return methodSignature.Redirection;
                }

                if (Arr.IndexOf(Arr.Map(methodSignature.ParamTypes, (p) => p.ToString()),
                        Arr.Map(methodInfo.GetParameters(), (p) => p.ParameterType.ToString())) != 0)
                {
                    continue;
                }

                return methodSignature.Redirection;
            }

            return null;
        }
    }
}