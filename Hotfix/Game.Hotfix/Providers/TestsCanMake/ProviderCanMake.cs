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
using Game.Hotfix.API.TestsCanMake;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsCanMake
{
    public class ProviderCanMake : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderCanMake");
            if (!CanMake<ICanMake>())
            {
                Util.Faild("CanMake_2");
                return;
            }

            Util.Success("CanMake");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderCanMake");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderCanMake");
            if (CanMake<ICanMake>())
            {
                Util.Faild("CanMake_1");
                return;
            }

            Singleton<ICanMake, CanMake>();
        }
    }
}
