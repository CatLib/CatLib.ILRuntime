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
using CatLib;
using Game.Hotfix.API.TestsInstance;
using static CatLib.App;

namespace Game.Hotfix.TestsInstance
{
    public class ProviderInstance : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderInstance");
            if (!CanMake<IInstance>())
            {
                Util.Faild("Instance_1");
                return;
            }

            if (Make<IInstance>() == null)
            {
                Util.Faild("Instance_2");
                return;
            }

            Util.Success("Instance");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderInstance");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderInstance");
            Instance<IInstance>(new Instance());
        }
    }
}
