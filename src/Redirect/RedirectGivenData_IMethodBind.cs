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
using ILRuntime.CLR.Method;
using ILRuntime.CLR.Utils;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntimeDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace CatLib.ILRuntime.Redirect
{
    /// <summary>
    /// GivenData_IMethodBind 重定向
    /// </summary>
    internal static unsafe class RedirectGivenData_IMethodBind
    {
        /// <summary>
        /// 重定向映射表
        /// </summary>
        private static readonly RedirectMapping mapping;

        /// <summary>
        /// 构建 GivenData_IMethodBind 重定向
        /// </summary>
        static RedirectGivenData_IMethodBind()
        {
            mapping = new RedirectMapping();
            mapping.Register("Given", 1, 0, Needs_TService);
        }

        /// <summary>
        /// 注册CLR重定向
        /// </summary>
        /// <param name="appDomain">AppDomain</param>
        public static void Register(ILRuntimeDomain appDomain)
        {
            var methods = typeof(IGivenData<IMethodBind>).GetMethods();

            foreach (var method in methods)
            {
                var redirection = mapping.GetRedirection(method);

                if (redirection == null)
                {
                    continue;
                }

                appDomain.RegisterCLRMethodRedirection(method, redirection);
            }
        }

        //  public TReturn Given<T>()
        public static StackObject* Needs_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);
            var ret = ILIntepreter.Minus(esp, 1);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            var instanceOfThisMethod =
                (IGivenData<IMethodBind>)typeof(IGivenData<IMethodBind>).CheckCLRTypes(StackObject.ToObject(ptrOfThisMethod,
                    intp.AppDomain, mStack));
            intp.Free(ptrOfThisMethod);

            return ILIntepreter.PushObject(ret, mStack, instanceOfThisMethod.Given(tService));
        }
    }
}
