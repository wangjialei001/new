using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace New.Common.Util
{
    public class EnumUtil
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Enum value)
        {
            Type type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? string.Empty : attribute.Description;
        }
    }
}
