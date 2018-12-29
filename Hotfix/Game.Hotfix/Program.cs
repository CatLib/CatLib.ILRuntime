/*
 * This file is part of the CatLib package.
 *
 * (c) CatLib <support@catlib.io>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * Document: http://catlib.io/
 */

using CatLib;

namespace Game.Hotfix
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="application">Application</param>
        public static void Main(IApplication application)
        {
            foreach (var provider in Providers.ServiceProviders)
            {
                application.Register(provider);
            }
        }
    }
}
