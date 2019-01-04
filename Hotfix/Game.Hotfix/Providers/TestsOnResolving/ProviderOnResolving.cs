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
using Game.Hotfix.API.TestsOnResolving;
using static CatLib.App;

namespace Game.Hotfix.TestsOnResolving
{
    public class ProviderOnResolving : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderOnResolving");

            object inst1 = null;
            OnResolving<IOnResolving1>(instance =>
            {
                inst1 = instance;
            });

            Make<IOnResolving1>();

            if (inst1 == null)
            {
                Util.Faild("OnResolving_1");
                return;
            }

            object inst2 = null;
            OnResolving<OnResolving>((c, instance) =>
            {
                inst2 = instance;
            });

            Make<IOnResolving2>();

            if (inst2 == null)
            {
                Util.Faild("OnResolving_2");
                return;
            }

            Util.Success("OnResolving");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderOnResolving");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderOnResolving");
            Singleton<IOnResolving1, OnResolving>();
            Singleton<IOnResolving2, OnResolving>();
        }
    }
}
