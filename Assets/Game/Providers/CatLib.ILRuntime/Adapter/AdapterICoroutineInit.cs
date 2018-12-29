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

using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using System;
using ILRuntimeAppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace CatLib.ILRuntime.Adapter
{
    public class AdapterICoroutineInit : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get { return typeof(ICoroutineInit); }
        }

        public override Type[] BaseCLRTypes
        {
            get { return null; }
        }

        public override Type AdaptorType
        {
            get { return typeof(AdapterICoroutineInit); }
        }

        public override object CreateCLRInstance(ILRuntimeAppDomain appdomain, ILTypeInstance instance)
        {
            throw new CodeStandardException(
                "If you want to use ICoroutineInit, you must implement the IServiceProvider interface.");
        }
    }
}