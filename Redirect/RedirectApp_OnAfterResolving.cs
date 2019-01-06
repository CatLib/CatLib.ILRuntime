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
        private static void RegisterOnAfterResolving()
        {
            mapping.Register("OnAfterResolving", 1, 1, new string[]
            {
                "System.Action`1[TWhere]"
            }, OnAfterResolving_TWhere_Action1);
            mapping.Register("OnAfterResolving", 1, 1, new string[]
            {
                "System.Action`2[CatLib.IBindData,TWhere]"
            }, OnAfterResolving_TWhere_Action2);
        }

        // public static IContainer OnAfterResolving<TWhere>(Action<TWhere> closure)
        private static StackObject* OnAfterResolving_TWhere_Action1(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 1)
            {
                throw new EntryPointNotFoundException();
            }

            var tWhere = Helper.ITypeToClrType(genericArguments[0]);

            var ret = ILIntepreter.Minus(esp, 1);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var closure =
                (Delegate)typeof(Delegate).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            var result = App.OnAfterResolving((container, instance) =>
            {
                if (tWhere.IsInstanceOfType(instance))
                {
                    closure.DynamicInvoke(instance);
                }
            });

            return ILIntepreter.PushObject(ret, mStack, result);
        }

        // public static IContainer OnAfterResolving<TWhere>(Action<IBindData, TWhere> closure)
        private static StackObject* OnAfterResolving_TWhere_Action2(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 1)
            {
                throw new EntryPointNotFoundException();
            }

            var tWhere = Helper.ITypeToClrType(genericArguments[0]);

            var ret = ILIntepreter.Minus(esp, 1);

            var ptrOfThisMethod = ILIntepreter.Minus(esp, 1);
            ptrOfThisMethod = ILIntepreter.GetObjectAndResolveReference(ptrOfThisMethod);

            var closure =
                (Delegate)typeof(Delegate).CheckCLRTypes(
                    StackObject.ToObject(ptrOfThisMethod, intp.AppDomain, mStack));

            intp.Free(ptrOfThisMethod);

            var result = App.OnAfterResolving((container, instance) =>
            {
                if (tWhere.IsInstanceOfType(instance))
                {
                    closure.DynamicInvoke(container, instance);
                }
            });

            return ILIntepreter.PushObject(ret, mStack, result);
        }
    }
}
