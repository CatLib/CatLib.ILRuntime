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

using System.Collections;
using CatLib;
using Game.Hotfix.API.TestsGetBind;
using UnityEngine;

namespace Game.Hotfix.TestsGetBind
{
    public class ProviderGetBind : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderGetBind");
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
            if (App.GetBind<IGetBind>() != null)
            {
                Util.Faild("GetBind_1");
                return;
            }

            App.Singleton<IGetBind, GetBind>();

            if (App.GetBind<IGetBind>() == null)
            {
                Util.Faild("GetBind_2");
                return;
            }

            if (App.GetBind<IGetBind>() == App.GetBind<IGetBind>())
            {
                return;
            }

            Util.Faild("GetBind_3");
        }
    }
}
