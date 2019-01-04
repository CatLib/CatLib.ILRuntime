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
using Game.Hotfix.API.TestsOnRelease;
using static CatLib.App;

namespace Game.Hotfix.TestsOnRelease
{
    public class ProviderOnRelease : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderOnRelease");

            Make<IOnRelease1>();

            object inst1 = null;
            OnRelease<IOnRelease1>(instance =>
            {
                inst1 = instance;
            });

            Release<IOnRelease1>();

            if (inst1 == null)
            {
                Util.Faild("OnRelease_1");
                return;
            }

            Make<IOnRelease2>();

            object inst2 = null;
            OnRelease<OnRelease>((c, instance) =>
            {
                inst2 = instance;
            });
            Release<IOnRelease2>();
            if (inst2 == null)
            {
                Util.Faild("OnRelease_2");
                return;
            }

            Util.Success("OnRelease");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderOnRelease");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderOnRelease");
            Singleton<IOnRelease1, OnRelease>();
            Singleton<IOnRelease2, OnRelease>();
        }
    }
}
