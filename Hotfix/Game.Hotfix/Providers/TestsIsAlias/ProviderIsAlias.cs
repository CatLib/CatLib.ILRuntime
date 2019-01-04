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
using CatLib;
using Game.Hotfix.API.TestsIsAlias;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsIsAlias
{
    public class ProviderIsAlias : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderIsAlias");
            if (IsAlias<IIsAlias>())
            {
                Util.Faild("IsAlias_2");
                return;
            }

            if (IsAlias<IIsAlias2>())
            {
                Util.Faild("IsAlias_3");
                return;
            }

            GetBind<IIsAlias>().Alias<IIsAlias2>();

            if (!IsAlias<IIsAlias2>())
            {
                Util.Faild("IsAlias_4");
                return;
            }

            Util.Success("IsAlias");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderIsAlias");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderIsAlias");
            if (IsAlias<IIsAlias>())
            {
                Util.Faild("IsAlias_1");
                return;
            }

            Singleton<IIsAlias, IsAlias>();
        }
    }
}
