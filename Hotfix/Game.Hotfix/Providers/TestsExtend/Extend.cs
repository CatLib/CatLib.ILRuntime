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

using Game.Hotfix.API.TestsExtend;

namespace Game.Hotfix.TestsExtend
{
    public class Extend : IExtend, IExtend2
    {
        public string Name { get; set; }

        public string Name2 { get; set; }
    }
}
