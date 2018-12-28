using System;
using CatLib;
using Game.API.Debugger;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

public class AdaptorIDebugger : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get { return typeof(IDebugger); }
    }

    public override Type[] BaseCLRTypes
    {
        get
        {
            return null;
        }
    }

    public override Type AdaptorType
    {
        get { return typeof(Adaptor); }
    }

    public override object CreateCLRInstance(AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }

    private class Adaptor : IDebugger, CrossBindingAdaptorType
    {
        private readonly ILTypeInstance instance;
        private readonly AppDomain appdomain;

        private IMethod methodLog;
        private bool methodLogGot;
        private bool isMethodLogInvoking;
        private readonly object[] methodLogParams = new object[1];

        public Adaptor(AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance
        {
            get { return instance; }
        }

        public void Log(string message)
        {
            if (!methodLogGot)
            {
                methodLog = instance.Type.GetMethod("Log", 1);
                methodLogGot = true;
            }

            if (methodLog != null && !isMethodLogInvoking)
            {
                isMethodLogInvoking = true;
                methodLogParams[0] = message;
                appdomain.Invoke(methodLog, instance, methodLogParams);
                isMethodLogInvoking = false;
            }
            else
            {
                App.Make<IDebugger>().Log(message);
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