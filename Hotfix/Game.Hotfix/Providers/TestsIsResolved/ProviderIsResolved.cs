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
using Game.Hotfix.API.TestsIsResolved;

namespace Game.Hotfix.TestsIsResolved
{
    public class ProviderIsResolved : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderIsResolved");

            var instance = App.Make<IIsResolved>();

            if (instance == null)
            {
                Util.Faild("IsResolved_3");
            }

            if (!App.IsResolved<IIsResolved>())
            {
                Util.Faild("IsResolved_4");
                return;
            }

            Util.Success("IsResolved");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderIsResolved");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderIsResolved");
            if (App.IsResolved<IIsResolved>())
            {
                Util.Faild("IsResolved_1");
                return;
            }

            App.Singleton<IIsResolved, IsResolved>();

            if (!App.IsResolved<IIsResolved>())
            {
                return;
            }

            Util.Faild("IsResolved_2");
        }
    }
}
