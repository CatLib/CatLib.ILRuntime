﻿/*
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
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using System;
using ILRuntimeAppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace CatLib.ILRuntime.Adapter
{
    public class AdapterIServiceProvider : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get { return typeof(IServiceProvider); }
        }

        public override Type[] BaseCLRTypes
        {
            get { return null; }
        }

        public override Type AdaptorType
        {
            get { return typeof(Adaptor); }
        }

        public override object CreateCLRInstance(ILRuntimeAppDomain appdomain, ILTypeInstance instance)
        {
            return new Adaptor(appdomain, instance);
        }

        private class Adaptor : IServiceProvider, CrossBindingAdaptorType
        {
            private readonly ILTypeInstance instance;
            private readonly ILRuntimeAppDomain appdomain;

            private IMethod methodInit;
            private bool methodGotInit;
            private bool isMethodInvokingInit;

            private IMethod methodRegister;
            private bool methodGotRegister;
            private bool isMethodInvokingRegister;

            public Adaptor(ILRuntimeAppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            public ILTypeInstance ILInstance
            {
                get { return instance; }
            }

            public void Init()
            {
                if (!methodGotInit)
                {
                    methodInit = instance.Type.GetMethod("Init", 0);
                    methodGotInit = true;
                }

                if (methodInit == null || isMethodInvokingInit)
                {
                    throw new LogicException("Calling base.Init() in a script is not allowed");
                }

                try
                {
                    isMethodInvokingInit = true;
                    appdomain.Invoke(methodInit, instance);
                }
                finally
                {
                    isMethodInvokingInit = false;
                }
            }

            public void Register()
            {
                if (!methodGotRegister)
                {
                    methodRegister = instance.Type.GetMethod("Register", 0);
                    methodGotRegister = true;
                }

                if (methodRegister == null || isMethodInvokingRegister)
                {
                    throw new LogicException("Calling base.Register() in a script is not allowed");
                }

                try
                {
                    isMethodInvokingRegister = true;
                    appdomain.Invoke(methodRegister, instance);
                }
                finally
                {
                    isMethodInvokingRegister = false;
                }
            }

            public override string ToString()
            {
                var method = appdomain.ObjectType.GetMethod("ToString", 0);
                method = instance.Type.GetVirtualMethod(method);
                if (method == null || method is ILMethod)
                {
                    return instance.ToString();
                }
                return instance.Type.FullName;
            }
        }
    }
}