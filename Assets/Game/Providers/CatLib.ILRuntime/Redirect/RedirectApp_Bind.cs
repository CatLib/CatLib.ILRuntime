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

using ILRuntime.CLR.Method;
using ILRuntime.CLR.Utils;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using System;
using System.Collections.Generic;

namespace CatLib.ILRuntime.Redirect
{
    /// <summary>
    /// App.cs 重定向
    /// </summary>
    internal static unsafe partial class RedirectApp
    {
        private static void RegisterBind()
        {
            mapping.Register("Bind", 1, 0, Bind_TService);
            mapping.Register("Bind", 2, 0, Bind_TService_TConcrete);
            mapping.Register("Bind", 1, 1, new string[]
            {
                "System.Func`1[System.Object]"
            }, Bind_TService_Func1);
            mapping.Register("Bind", 1, 1, new string[]
            {
                "System.Func`2[System.Object[],System.Object]"
            }, Bind_TService_Func2);
            mapping.Register("Bind", 1, 3, new string[]
            {
                "System.Func`3[CatLib.IContainer,System.Object[],System.Object]"
            }, Bind_TService_Func2);
        }

        // public static IBindData Bind<TService>()
        private static StackObject* Bind_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);
            var tType = Helper.ITypeToClrType(genericArguments[0]);

            return ILIntepreter.PushObject(esp, mStack, App.Bind(tService, tType, false));
        }

        // public static IBindData Bind<TService, TConcrete>()
        private static StackObject* Bind_TService_TConcrete(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 2 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);
            var tType = Helper.ITypeToClrType(genericArguments[1]);

            return ILIntepreter.PushObject(esp, mStack, App.Bind(tService, tType, false));
        }

        // public static IBindData Bind<TService>(Func<IContainer, object[], object> concrete)
        private static StackObject* Bind_TService_Func3(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 1)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            var ret = ILIntepreter.Minus(esp, 1);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var closure =
                (Delegate)typeof(Delegate).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            return ILIntepreter.PushObject(ret, mStack,
                App.Bind(tService, (container, @params) => closure.DynamicInvoke(container, @params), false));
        }

        // public static IBindData Bind<TService>(Func<object[], object> concrete)
        private static StackObject* Bind_TService_Func2(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 1)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            var ret = ILIntepreter.Minus(esp, 1);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var closure =
                (Delegate)typeof(Delegate).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            return ILIntepreter.PushObject(ret, mStack,
                App.Bind(tService, (container, @params) => closure.DynamicInvoke(@params), false));
        }

        // public static IBindData Bind<TService>(Func<object> concrete)
        private static StackObject* Bind_TService_Func1(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 1)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            var ret = ILIntepreter.Minus(esp, 1);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var closure =
                (Delegate)typeof(Delegate).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            return ILIntepreter.PushObject(ret, mStack,
                App.Bind(tService, (container, @params) => closure.DynamicInvoke(), false));
        }
    }
}
