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

using System;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Reflection;

namespace CatLib.ILRuntime.Redirect
{
    /// <summary>
    /// 辅助函数
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// 将IType转为字符串
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>字符串</returns>
        public static string ITypeToService(IType type)
        {
            var ilType = type as ILType;
            return ilType != null ? ilType.FullName : App.Type2Service(type.TypeForCLR);
        }

        /// <summary>
        /// 将IType转为Clr Type类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型</returns>
        public static Type ITypeToClrType(IType type)
        {
            var ilType = type as ILType;
            return ilType != null ? new ILRuntimeType(ilType) : type.TypeForCLR;
        }
    }
}