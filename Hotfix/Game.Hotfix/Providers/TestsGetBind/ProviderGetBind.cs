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
using Game.Hotfix.API.TestsGetBind;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsGetBind
{
    public class ProviderGetBind : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderGetBind");
            if (GetBind<IGetBind>() == null)
            {
                Util.Faild("GetBind_2");
                return;
            }

            if (GetBind<IGetBind>() != GetBind<IGetBind>())
            {
                Util.Faild("GetBind_3");
                return;
            }

            Util.Success("GetBind");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderGetBind");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderGetBind");
            if (GetBind<IGetBind>() != null)
            {
                Util.Faild("GetBind_1");
                return;
            }

            Singleton<IGetBind, GetBind>();
        }
    }
}
