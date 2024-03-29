﻿namespace BashSoft.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class AliasAttribute : Attribute
    {
        private string name;

        public AliasAttribute(string aliasName)
        {
            this.name = aliasName;
        }

        public string Name => this.name;

        public override bool Equals(object obj)
        {
            return this.name.Equals(obj);
        }
    }
}
