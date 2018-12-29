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
using Game.Hotfix.API.TestsHasBind;

namespace Game.Hotfix.TestsHasBind
{
    public class ProviderHasBind : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderHasInstance");

            if (!App.HasBind<IHasBind>())
            {
                Util.Faild("HasBind_2");
                return;
            }

            Util.Success("HasBind");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderHasBind");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderHasBind");
            if (App.HasBind<IHasBind>())
            {
                Util.Faild("HasBind_1");
                return;
            }

            App.Singleton<IHasBind, HasBind>();
        }
    }
}
