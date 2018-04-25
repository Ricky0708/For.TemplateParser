﻿using System;

namespace For.TemplateParser.Models
{
    [Serializable]
    internal class NodeModel
    {
        internal delegate object delgGetProperty(object instance);
        internal NodeType Type { get; set; }
        internal string NodeStringValue { get; set; }
        internal delgGetProperty NodeDelegateValue { get; set; }

    }
    internal enum NodeType
    {
        String,
        Property
    }
}