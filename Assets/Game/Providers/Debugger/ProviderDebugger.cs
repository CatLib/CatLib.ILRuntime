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

using CatLib;
using Game.API.Debugger;

namespace Game.Debugger
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public class ProviderDebugger : ServiceProvider
    {
        public override void Register()
        {
            App.Singleton<IDebugger, LoggerUnity>();
        }
    }
}
