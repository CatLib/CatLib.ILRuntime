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

namespace Game.Hotfix.Type2Service
{
    public class ProviderTypeToService : ServiceProvider
    {
        public override void Init()
        {
            Util.Log("Init() : ProviderTypeToService");

            if (App.Type2Service<ProviderTypeToService>() != "Game.Hotfix.Type2Service.ProviderTypeToService")
            {
                Util.Faild("TypeToService_1");
                return;
            }

            Util.Success("TypeToService");
        }

        public override IEnumerator CoroutineInit()
        {
            Util.Log("CoroutineInit() : ProviderTypeToService");
            return base.CoroutineInit();
        }

        public override void Register()
        {
            Util.Log("Register() : ProviderTypeToService");
        }
    }
}
