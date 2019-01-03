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
using Game.Hotfix.API.TestsUnbind;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsUnbind
{
    public class ProviderUnbind : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderUnbind");
            if (!CanMake<IUnbind>())
            {
                Util.Faild("Unbind_1");
                return;
            }

            Unbind<IUnbind>();

            if (CanMake<IUnbind>())
            {
                Util.Faild("Unbind_2");
                return;
            }

            Util.Success("Unbind");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderUnbind");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderUnbind");
            Singleton<IUnbind, Unbind>();
        }
    }
}
