using Millarow.Presentation.MVP.Services;
using System;
using System.Windows;

namespace Millarow.Presentation.WPF.MVP.Framework
{
    public static class MessageBoxIconConverter
    {
        public static MessageBoxImage Convert(this MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.None:
                    return MessageBoxImage.None;
                case MessageBoxIcon.Information:
                    return MessageBoxImage.Information;
                case MessageBoxIcon.Question:
                    return MessageBoxImage.Question;
                case MessageBoxIcon.Warning:
                    return MessageBoxImage.Warning;
                case MessageBoxIcon.Error:
                    return MessageBoxImage.Error;
                default:
                    throw new ArgumentException($"Unknown {nameof(MessageBoxIcon)} value '{icon}");
            }
        }
    }
}
