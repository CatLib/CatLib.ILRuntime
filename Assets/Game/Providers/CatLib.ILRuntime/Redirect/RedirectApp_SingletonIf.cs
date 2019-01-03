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
        private static void RegisterSingletonIf()
        {
            mapping.Register("SingletonIf", 1, 1, SingletonIf_TService_IBindData);
            mapping.Register("SingletonIf", 2, 1, SingletonIf_TService_TConcrete_IBindData);
            mapping.Register("SingletonIf", 1, 2, new string[]
            {
                "System.Func`3[CatLib.IContainer,System.Object[],System.Object]",
                "CatLib.IBindData&"
            }, SingletonIf_TService_Func3_IBindData);
            mapping.Register("SingletonIf", 1, 2, new string[]
            {
                "System.Func`2[System.Object[],System.Object]",
                "CatLib.IBindData&"
            }, SingletonIf_TService_Func2_IBindData);
            mapping.Register("SingletonIf", 1, 2, new string[]
            {
                "System.Func`1[System.Object]",
                "CatLib.IBindData&"
            }, SingletonIf_TService_Func1_IBindData);
        }

        // public static bool SingletonIf<TService>(out IBindData bindData)
        private static StackObject* SingletonIf_TService_IBindData(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 1)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);
            var tType = Helper.ITypeToClrType(genericArguments[0]);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var bindData =
                (IBindData)typeof(IBindData).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            var result = App.BindIf(tService, tType, true, out bindData);

            ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            Helper.SetValue(ptrOfThisMethod, mStack, intp.AppDomain, bindData);

            return ILIntepreter.PushObject(ILIntepreter.Minus(esp, 1), mStack, result);
        }

        // public static bool SingletonIf<TService, TConcrete>(out IBindData bindData)
        private static StackObject* SingletonIf_TService_TConcrete_IBindData(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 2 || method.ParameterCount != 1)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);
            var tType = Helper.ITypeToClrType(genericArguments[1]);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var bindData =
                (IBindData)typeof(IBindData).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            var result = App.BindIf(tService, tType, true, out bindData);

            ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            Helper.SetValue(ptrOfThisMethod, mStack, intp.AppDomain, bindData);

            return ILIntepreter.PushObject(ILIntepreter.Minus(esp, 1), mStack, result);
        }

        // public static bool SingletonIf<TService>(Func<IContainer, object[], object> concrete, out IBindData bindData)
        private static StackObject* SingletonIf_TService_Func3_IBindData(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 2)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var bindData =
                (IBindData)typeof(IBindData).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            ptrOfThisMethod = ILIntepreter.Minus(esp, 2);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var concrete =
                (Func<IContainer, object[], object>)typeof(Func<IContainer, object[], object>).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            var result = App.BindIf(tService, concrete, true, out bindData);

            ptrOfThisMethod = ILIntepreter.Minus(esp, 2);
            Helper.SetValue(ptrOfThisMethod, mStack, intp.AppDomain, bindData);

            return ILIntepreter.PushObject(ILIntepreter.Minus(esp, 2), mStack, result);
        }

        // public static bool SingletonIf<TService>(Func<object[], object> concrete, out IBindData bindData)
        private static StackObject* SingletonIf_TService_Func2_IBindData(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 2)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var bindData =
                (IBindData)typeof(IBindData).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));


            ptrOfThisMethod = ILIntepreter.Minus(esp, 2);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var concrete =
                (Func<object[], object>)typeof(Func<object[], object>).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            var result = App.BindIf(tService, (c, p) => concrete(p), true, out bindData);

            ptrOfThisMethod = ILIntepreter.Minus(esp, 2);
            Helper.SetValue(ptrOfThisMethod, mStack, intp.AppDomain, bindData);

            return ILIntepreter.PushObject(ILIntepreter.Minus(esp, 2), mStack, result);
        }

        // public static bool SingletonIf<TService>(Func<object> concrete, out IBindData bindData)
        private static StackObject* SingletonIf_TService_Func1_IBindData(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 2)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var bindData =
                (IBindData)typeof(IBindData).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            ptrOfThisMethod = ILIntepreter.Minus(esp, 2);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var concrete =
                (Func<object>)typeof(Func<object>).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            var result = App.BindIf(tService, (c, p) => concrete(), true, out bindData);

            ptrOfThisMethod = ILIntepreter.Minus(esp, 2);
            Helper.SetValue(ptrOfThisMethod, mStack, intp.AppDomain, bindData);

            return ILIntepreter.PushObject(ILIntepreter.Minus(esp, 2), mStack, result);
        }
    }
}
