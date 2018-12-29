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
using Game.Hotfix.API.TestsIsStatic;
using static CatLib.App;

namespace Game.Hotfix.TestsIsStatic
{
    public class ProviderIsStatic : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderIsStatic");
            if (!IsStatic<IIsStatic>())
            {
                Util.Faild("IsStatic_2");
                return;
            }

            Util.Success("IsStatic");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderIsStatic");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderIsStatic");
            if (IsStatic<IIsStatic>())
            {
                Util.Faild("IsStatic_1");
                return;
            }

            Singleton<IIsStatic, IsStatic>();
        }
    }
}
