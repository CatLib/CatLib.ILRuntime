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

using System;
using System.Collections;
using CatLib;
using Game.Hotfix.API.TestsAlias;
using static CatLib.App;

namespace Game.Hotfix.TestsAlias
{
    public class ProviderAlias : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderAlias");
            var alias = Make<IAlias>();

            if (alias == null)
            {
                Util.Faild("Alias_1");
                return;
            }

            Util.Success("Alias");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderAlias");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Singleton<IDisposable, Alias>();
            Alias<IAlias, IDisposable>();
        }
    }
}
