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
        private static void RegisterSingleton()
        {
            mapping.Register("Singleton", 1, 0, Singleton_TService);
            mapping.Register("Singleton", 2, 0, Singleton_TService_TConcrete);
            mapping.Register("Singleton", 1, 1, new string[]
            {
                "System.Func`1[System.Object]"
            }, Singleton_TService_Func1);
            mapping.Register("Singleton", 1, 1, new string[]
            {
                "System.Func`2[System.Object[],System.Object]"
            }, Singleton_TService_Func2);
            mapping.Register("Singleton", 1, 1, new string[]
            {
                "System.Func`3[CatLib.IContainer,System.Object[],System.Object]"
            }, Singleton_TService_Func3);
        }

        // public static IBindData Singleton<TService>()
        private static StackObject* Singleton_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);
            var tConcrete = Helper.ITypeToClrType(genericArguments[0]);

            return ILIntepreter.PushObject(esp, mStack, App.Bind(tService, tConcrete, true));
        }

        // public static IBindData Singleton<TService, TConcrete>()
        private static StackObject* Singleton_TService_TConcrete(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 2 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);
            var tConcrete = Helper.ITypeToClrType(genericArguments[1]);

            return ILIntepreter.PushObject(esp, mStack, App.Bind(tService, tConcrete, true));
        }

        // public static IBindData Singleton<TService>(Func<IContainer, object[], object> concrete)
        private static StackObject* Singleton_TService_Func3(ILIntepreter intp, StackObject* esp, IList<object> mStack,
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
                (Func<IContainer, object[], object>)typeof(Func<IContainer, object[], object>).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            return ILIntepreter.PushObject(ret, mStack,
                App.Bind(tService, (container, @params) => closure(container, @params), true));
        }

        // public static IBindData Singleton<TService>(Func<object[], object> concrete)
        private static StackObject* Singleton_TService_Func2(ILIntepreter intp, StackObject* esp, IList<object> mStack,
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
                (Func<object[], object>)typeof(Func<object[], object>).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            return ILIntepreter.PushObject(ret, mStack,
                App.Bind(tService, (container, @params) => closure(@params), true));
        }

        // public static IBindData Singleton<TService>(Func<object> concrete)
        private static StackObject* Singleton_TService_Func1(ILIntepreter intp, StackObject* esp, IList<object> mStack,
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
                (Func<object>)typeof(Func<object>).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            return ILIntepreter.PushObject(ret, mStack,
                App.Bind(tService, (container, @params) => closure(), true));
        }
    }
}
