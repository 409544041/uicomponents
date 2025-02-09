﻿using System;
using JetBrains.Annotations;

namespace UIComponents
{
    /// <summary>
    /// Specifies the path to a .uss stylesheet file used by a UIComponent.
    /// </summary>
    /// <seealso cref="AssetPathAttribute"/>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [BaseTypeRequired(typeof(UIComponent))]
    public class StylesheetAttribute : PathAttribute
    {
        public StylesheetAttribute(string path)
        {
            Path = path;
        }
    }
}