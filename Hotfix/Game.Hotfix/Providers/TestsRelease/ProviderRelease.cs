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
using Game.Hotfix.API.TestsRelease;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsRelease
{
    public class ProviderRelease : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderRelease");

            var instance = Make<IRelease>();
            if (Make<IRelease>() != instance)
            {
                Util.Faild("Release_1");
                return;
            }

            Release<IRelease>();

            if (Make<IRelease>() == instance)
            {
                Util.Faild("Release_2");
                return;
            }

            Util.Success("Release");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderRelease");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderRelease");
            Singleton<IRelease, Release>();
        }
    }
}
