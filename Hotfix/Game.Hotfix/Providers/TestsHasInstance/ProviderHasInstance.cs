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
using Game.Hotfix.API.TestsHasInstance;
using UnityEngine;

namespace Game.Hotfix.TestsHasInstance
{
    public class ProviderHasInstance : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderHasInstance");

            var instance = App.Make<IHasInstance>();

            if (instance == null || instance.GetValue() != 10086)
            {
                Util.Faild("HasInstance_2");
            }

            if (!App.HasInstance<IHasInstance>())
            {
                Util.Faild("HasInstance_3");
                return;
            }

            Util.Success("HasInstance");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderHasInstance");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderHasInstance");
            if (App.HasInstance<IHasInstance>())
            {
                Util.Faild("HasInstance_1");
                return;
            }

            App.Singleton<IHasInstance, HasInstance>();
        }        
    }
}
