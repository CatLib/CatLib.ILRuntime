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

using System.Collections;

namespace CatLib.ILRuntime
{
    /// <summary>
    /// ILRuntime服务
    /// </summary>
    public class ProviderILRuntime : ServiceProvider
    {
        /// <summary>
        /// 注册服务提供者
        /// </summary>
        public override void Register()
        {
            App.On(ApplicationEvents.OnInit, () => 
            {
                IBindData bindData;
                App.SingletonIf<IAppDomain, AppDomain>(out bindData);
            });
        }
    }
}