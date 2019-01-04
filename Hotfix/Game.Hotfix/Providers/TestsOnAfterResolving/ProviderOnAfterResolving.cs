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
using Game.Hotfix.API.TestsOnAfterResolving;
using static CatLib.App;

namespace Game.Hotfix.TestsOnAfterResolving
{
    public class ProviderOnAfterResolving : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderOnAfterResolving");

            object inst1 = null;
            OnAfterResolving<IOnAfterResolving1>(instance =>
            {
                inst1 = instance;
            });

            Make<IOnAfterResolving1>();

            if (inst1 == null)
            {
                Util.Faild("OnAfterResolving_1");
                return;
            }

            object inst2 = null;
            OnAfterResolving<OnAfterResolving>((c, instance) =>
            {
                inst2 = instance;
            });

            Make<IOnAfterResolving2>();

            if (inst2 == null)
            {
                Util.Faild("OnAfterResolving_2");
                return;
            }

            Util.Success("OnAfterResolving");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderOnAfterResolving");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderOnAfterResolving");
            Singleton<IOnAfterResolving1, OnAfterResolving>();
            Singleton<IOnAfterResolving2, OnAfterResolving>();
        }
    }
}
