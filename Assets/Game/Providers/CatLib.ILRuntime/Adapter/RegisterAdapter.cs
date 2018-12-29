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

using ILRuntimeDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace CatLib.ILRuntime.Adapter
{
    /// <summary>
    /// 注册跨域继承适配器
    /// </summary>
    public static class RegisterAdapter
    {
        /// <summary>
        /// 注册跨域继承适配器
        /// </summary>
        /// <param name="appDomain">AppDomain</param>
        public static void Register(ILRuntimeDomain appDomain)
        {
            appDomain.RegisterCrossBindingAdaptor(new AdapterServiceProvider());
            appDomain.RegisterCrossBindingAdaptor(new AdapterIServiceProvider());
            appDomain.RegisterCrossBindingAdaptor(new AdapterICoroutineInit());
            // appDomain.RegisterCrossBindingAdaptor(new AdapterIServiceProvider_ICoroutineInit());
        }
    }
}

