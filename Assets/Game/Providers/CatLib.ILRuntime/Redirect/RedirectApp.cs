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
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using System;
using System.Collections.Generic;
using ILRuntimeDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace CatLib.ILRuntime.Redirect
{
    /// <summary>
    /// App.cs 重定向
    /// </summary>
    internal static unsafe class RedirectApp
    {
        /// <summary>
        /// 重定向映射表
        /// </summary>
        private static readonly RedirectMapping mapping;

        /// <summary>
        /// 构建 App.cs 重定向
        /// </summary>
        static RedirectApp()
        {
            mapping = new RedirectMapping();

            mapping.Register("GetBind", 1, 0, GetBind_TService);
            mapping.Register("HasInstance", 1, 0, HasInstance_TService);
            mapping.Register("IsResolved", 1, 0, IsResolved_TService);
            mapping.Register("HasBind", 1, 0, HasBind_TService);
            mapping.Register("CanMake", 1, 0, CanMake_TService);
            mapping.Register("IsStatic", 1, 0, IsStatic_TService);
            mapping.Register("IsAlias", 1, 0, IsAlias_TService);
            mapping.Register("Alias", 2, 0, Alias_TAlias_TService);
            mapping.Register("Singleton", 2, 0, Singleton_TService_TConcrete);
            mapping.Register("Singleton", 1, 0, Singleton_TService);
            mapping.Register("Make", 1, 1, Make);
        }

        /// <summary>
        /// 注册CLR重定向
        /// </summary>
        /// <param name="appDomain">AppDomain</param>
        public static void Register(ILRuntimeDomain appDomain)
        {
            var methods = typeof(App).GetMethods();
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

        // public static IBindData GetBind<TService>()
        public static StackObject* GetBind_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            return ILIntepreter.PushObject(esp, mStack, App.GetBind(tService));
        }

        // public static bool HasInstance<TService>()
        public static StackObject* HasInstance_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            return ILIntepreter.PushObject(esp, mStack, App.HasInstance(tService));
        }

        // public static bool IsResolved<TService>()
        public static StackObject* IsResolved_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            return ILIntepreter.PushObject(esp, mStack, App.IsResolved(tService));
        }

        // public static bool HasBind<TService>()
        public static StackObject* HasBind_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            return ILIntepreter.PushObject(esp, mStack, App.HasBind(tService));
        }

        // public static bool CanMake<TService>()
        public static StackObject* CanMake_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            return ILIntepreter.PushObject(esp, mStack, App.CanMake(tService));
        }

        // public static bool IsStatic<TService>()
        public static StackObject* IsStatic_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            return ILIntepreter.PushObject(esp, mStack, App.IsStatic(tService));
        }

        // public static bool Alias<TService>()
        public static StackObject* IsAlias_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);

            return ILIntepreter.PushObject(esp, mStack, App.IsAlias(tService));
        }

        // public static IContainer Alias<TAlias, TService>()
        public static StackObject* Alias_TAlias_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 2 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tAlias = Helper.ITypeToService(genericArguments[0]);
            var tService = Helper.ITypeToService(genericArguments[1]);

            return ILIntepreter.PushObject(esp, mStack, App.Alias(tAlias, tService));
        }

        // public static IBindData Singleton<TService, TConcrete>()
        public static StackObject* Singleton_TService_TConcrete(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 2 || method.ParameterCount != 0)
            {
                throw new EntryPointNotFoundException();
            }

            var tService  = Helper.ITypeToService(genericArguments[0]);
            var tConcrete = Helper.ITypeToClrType(genericArguments[1]);

            return ILIntepreter.PushObject(esp, mStack, App.Bind(tService, tConcrete, true));
        }

        // public static IBindData Singleton<TService>()
        public static StackObject* Singleton_TService(ILIntepreter intp, StackObject* esp, IList<object> mStack,
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

        // public static TService Make<TService>(params object[] userParams)
        public static StackObject* Make(ILIntepreter intp, StackObject* esp, IList<object> mStack,
            CLRMethod method, bool isNewObj)
        {
            var genericArguments = method.GenericArguments;
            if (genericArguments == null || genericArguments.Length != 1 || method.ParameterCount != 1)
            {
                throw new EntryPointNotFoundException();
            }

            var tService = Helper.ITypeToService(genericArguments[0]);
            return ILIntepreter.PushObject(esp, mStack, App.Make(tService));
        }
    }
}

