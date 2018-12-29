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

namespace CatLib.ILRuntime.Redirect
{
    /// <summary>
    /// 重定向App
    /// </summary>
    public static class RegisterRedirect
    {
        /// <summary>
        /// 注册CLR重定向
        /// </summary>
        /// <param name="appDomain">AppDomain</param>
        public static void Register(ILRuntimeDomain appDomain)
        {
            RedirectApp.Register(appDomain);
            RedirectExtendContainer.Register(appDomain);
            RedirectExtendBindData.Register(appDomain);
            RedirectBindable_IBindData.Register(appDomain);
            RedirectBindable_IMethodBind.Register(appDomain);
            RedirectGivenData_IBindData.Register(appDomain);
            RedirectGivenData_IMethodBind.Register(appDomain);
        }
    }
}

