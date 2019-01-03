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
        private static void RegisterExtend()
        {
            mapping.Register("Extend", 2, 1, new string[]
            {
                "System.Func`3[TConcrete,CatLib.IContainer,System.Object]"
            }, Extend_TService_TConcrete_Func3);
            mapping.Register("Extend", 2, 1, new string[]
            {
                "System.Func`2[TConcrete,System.Object]"
            }, Extend_TService_TConcrete_Func2);
            mapping.Register("Extend", 1, 1, new string[]
            {
                "System.Func`3[TConcrete,CatLib.IContainer,System.Object]"
            }, Extend_TService_Func3);
            mapping.Register("Extend", 1, 1, new string[]
            {
                "System.Func`2[TConcrete,System.Object]"
            }, Extend_TService_Func2);
        }

        // public static void Extend<TService, TConcrete>(Func<TConcrete, IContainer, object> closure)
        private static StackObject* Extend_TService_TConcrete_Func3(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 2 || method.ParameterCount != 1)
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
            App.Extend(tService, (instance, c) => closure.DynamicInvoke(instance, c));

            return ret;
        }

        // public static void Extend<TService, TConcrete>(Func<TConcrete, object> closure)
        private static StackObject* Extend_TService_TConcrete_Func2(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 2 || method.ParameterCount != 1)
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
            App.Extend(tService, (instance, c) => closure.DynamicInvoke(instance));

            return ret;
        }

        // public static void Extend<TConcrete>(Func<TConcrete, IContainer, object> closure)
        private static StackObject* Extend_TService_Func3(ILIntepreter intp, StackObject* esp, IList<object> mStack,
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
            App.Extend(tService, (instance, c) => closure.DynamicInvoke(instance, c));

            return ret;
        }

        // public static void Extend<TConcrete>(Func<TConcrete, object> closure)
        private static StackObject* Extend_TService_Func2(ILIntepreter intp, StackObject* esp, IList<object> mStack,
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
            App.Extend(tService, (instance, c) => closure.DynamicInvoke(instance));

            return ret;
        }
    }
}
