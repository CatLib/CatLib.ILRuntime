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
    /// ExtendContainer.cs 重定向
    /// </summary>
    internal static unsafe class RedirectExtendContainer
    {
        /// <summary>
        /// 重定向映射表
        /// </summary>
        private static readonly RedirectMapping mapping;

        /// <summary>
        /// 构建 ExtendContainer.cs 重定向
        /// </summary>
        static RedirectExtendContainer()
        {
            mapping = new RedirectMapping();
        }

        /// <summary>
        /// 注册CLR重定向
        /// </summary>
        /// <param name="appDomain">AppDomain</param>
        public static void Register(ILRuntimeDomain appDomain)
        {
            var methods = typeof(ExtendContainer).GetMethods();
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
    }
}
