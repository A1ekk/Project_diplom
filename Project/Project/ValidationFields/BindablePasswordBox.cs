using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Project.ValidationFields
{
    public class BindablePasswordBox : Decorator
    {
        public static readonly DependencyProperty PasswordProperty
            = DependencyProperty.Register(nameof(Password), typeof(string), typeof(BindablePasswordBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

        static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            var bpb = (BindablePasswordBox)d;
            if (bpb.isPreventCallback) return;
            bpb.passwordBox.PasswordChanged -= bpb.HandlePasswordChanged;
            bpb.passwordBox.Password = (string)eventArgs.NewValue;
            bpb.passwordBox.PasswordChanged += bpb.HandlePasswordChanged;
        }

        bool isPreventCallback = false;
        PasswordBox passwordBox = new PasswordBox();

        public BindablePasswordBox()
        {
            passwordBox.PasswordChanged += HandlePasswordChanged;
            Child = passwordBox;
        }

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        void HandlePasswordChanged(object sender, RoutedEventArgs eventArgs)
        {
            isPreventCallback = true;
            Password = passwordBox.Password;
            isPreventCallback = false;
        }
    }
}
