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
using Game.Hotfix.API.TestsWatch;
using static CatLib.App;

namespace Game.Hotfix.TestsWatch
{
    public class ProviderWatch : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderWatch");

            Make<IWatch>();
            var isChange = false;
            var isChange2 = false;
            Watch<IWatch>(() =>
            {
                isChange = true;
            });
            Watch<IWatch>((n) =>
            {
                if (n != null)
                {
                    isChange2 = true;
                }
            });

            Release<IWatch>();
            Make<IWatch>();

            if (!isChange)
            {
                Util.Faild("Watch_1");
                return;
            }

            if (!isChange2)
            {
                Util.Faild("Watch_2");
                return;
            }

            Util.Success("Watch");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderWatch");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderWatch");
            Singleton<IWatch, Watch>();
        }
    }
}
