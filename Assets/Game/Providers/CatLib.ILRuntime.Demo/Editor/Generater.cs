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

using Game.API.Debugger;
using ILRuntime.Runtime.CLRBinding;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CatLib.ILRuntime
{
    /// <summary>
    /// 代码生成器
    /// </summary>
    public class Generater : MonoBehaviour
    {
        [MenuItem("CatLib/Generater/Generate CLR Binding Code")]
        public static void GenerateCLRBinding()
        {
            var types = new List<Type>
            {
                typeof(int),
                typeof(float),
                typeof(long),
                typeof(object),
                typeof(string),
                typeof(Array),
                typeof(Dictionary<string, int>),
                typeof(IContainer),
                typeof(Dictionary<global::ILRuntime.Runtime.Intepreter.ILTypeInstance, int>)
            };

            // todo: 程序生成路径需要可配置
            BindingCodeGenerator.GenerateBindingCode(types, "Assets/Game/Providers/CatLib.ILRuntime.Demo/Generated");
        }
    }
}

