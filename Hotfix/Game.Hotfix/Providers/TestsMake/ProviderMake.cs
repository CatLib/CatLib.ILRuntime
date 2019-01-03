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
using Game.Hotfix.API.TestsMake;
using System.Collections;
using static CatLib.App;

namespace Game.Hotfix.TestsMake
{
    public class ProviderMake : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderMake");

            var make = Make<IMake>("hello world");

            if (make.Name != "hello world")
            {
                Util.Faild("Make_1");
            }

            Util.Success("Make");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderMake");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderMake");
            Singleton<IMake>((p) => new Make {Name = p[0].ToString()});
        }
    }
}
