using Millarow.Presentation.WPF.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Controls
{
    //TODO: вынести все в события и где то в стратегиях уже обрабатывать события контролов как нужно для данного случая (выходит норм framework)
    public partial class EmbeddedKeyboard : UserControl
    {
        private EmbeddedKeyboardLayout _currentLayout;

        public EmbeddedKeyboard()
        {
            InitializeComponent();

            Loaded += EmbeddedKeyboard_Loaded;

            btnLanguage.Click += BtnLanguage_Click;
            btnShift.Click += (s, e) => UpdateSymbolButtons(_currentLayout, btnShift.IsChecked == true);
            btnSpecials.Click += BtnSpecials_Click;

            btnSpace.Click += BtnSpace_Click;
            btnBack.Click += (s, e) => ForTargetTextBox(target => EditingCommands.Backspace.Execute(null, target));
            btnUp.Click += (s, e) => ForTargetTextBox(target => EditingCommands.MoveUpByLine.Execute(null, target));
            btnDown.Click += (s, e) => ForTargetTextBox(target => EditingCommands.MoveDownByLine.Execute(null, target));
            btnLeft.Click += (s, e) => ForTargetTextBox(target => EditingCommands.MoveLeftByCharacter.Execute(null, target));
            btnRight.Click += (s, e) => ForTargetTextBox(target => EditingCommands.MoveRightByCharacter.Execute(null, target));
            btnEnter.Click += BtnEnter_Click;

            foreach (var symbolButton in gridButtons.Children.OfType<ButtonBase>().Where(x => x.Tag != null))
                symbolButton.Click += Button_Click;

            InputLanguageManager.Current.InputLanguageChanged += Current_InputLanguageChanged;
        }

        private void Current_InputLanguageChanged(object sender, InputLanguageEventArgs e)
        {
            var lay = Languages.FirstOrDefault(x => x.Name == e.NewLanguage.Name);
            if (lay != null)
                SetLayout(lay);
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            ForTargetTextBox(target =>
            {
                if (target.AcceptsReturn)
                    target.AddText("\r\n");
                else
                {
                    if (target.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)))
                    {
                        var focused = GetTargetTextBox();
                        if (focused != null)
                            focused.SelectAll();
                    }
                }
            });
        }

        private void BtnSpace_Click(object sender, RoutedEventArgs e)
        {
            //ForTargetTextBox(target =>
            //{
            //    target.RaiseEvent(new TextCompositionEventArgs(Keyboard.PrimaryDevice, new TextComposition(InputManager.Current, target, "z"))
            //    {
            //        RoutedEvent = TextCompositionManager.TextInputEvent
            //    });
            //});
            //return;
            ForTargetTextBox(target => target.AddText(" "));
        }

        private void BtnSpecials_Click(object sender, RoutedEventArgs e)
        {
            if (btnSpecials.IsChecked == true)
            {
                var lay = EmbeddedKeyboardLayout.Special;

                btnLanguage.IsEnabled = false;
                btnShift.IsEnabled = false;
                UpdateSymbolButtons(lay, false);
            }
            else
            {
                btnLanguage.IsEnabled = true;
                SetLayout(_currentLayout);
            }
        }

        private void EmbeddedKeyboard_Loaded(object sender, RoutedEventArgs e)
        {
            SetLayout(Languages.First());
        }

        private void BtnLanguage_Click(object sender, RoutedEventArgs e)
        {
            var nextIndex = (Languages.IndexOf(_currentLayout) + 1) % Languages.Count;
            var language = Languages[nextIndex];

            SetLayout(language);

            if (language.CultureInfo != InputLanguageManager.Current.CurrentInputLanguage)
                InputLanguageManager.Current.CurrentInputLanguage = language.CultureInfo;
        }

        private void SetLayout(EmbeddedKeyboardLayout lay)
        {
            var nextIndex = (Languages.IndexOf(lay) + 1) % Languages.Count;

            _currentLayout = lay;
            btnLanguage.Content = lay.Name;
            btnShift.IsEnabled = lay.ShiftSupported;

            UpdateSymbolButtons(_currentLayout, ShiftPressed);
        }

        private void UpdateSymbolButtons(EmbeddedKeyboardLayout layout, bool shifted)
        {
            var buttons = gridButtons.Children.OfType<ButtonBase>().Where(x => x.Tag != null).ToArray();

            foreach (var button in buttons)
            {
                var key = GetButtonKey(button);

                if (layout.TryGetSymbol(key, out var symbol))
                {
                    button.Content = symbol.GetValue(shifted);
                    button.IsEnabled = true;
                }
                else
                    button.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (ButtonBase)sender;
            ForTargetTextBox(target =>
            {
                var key = GetButtonKey(button);

                if (btnSpecials.IsChecked == false)
                {
                    var symbol = _currentLayout.GetSymbol(key);
                    target.AddText(symbol.GetValue(ShiftPressed));
                }
                else
                {
                    var symbol = EmbeddedKeyboardLayout.Special.GetSymbol(key);
                    target.AddText(symbol.GetValue(ShiftPressed));
                }
            });
        }

        private void ForTargetTextBox(Action<TextBox> action)
        {
            var target = GetTargetTextBox();
            if (target != null)
            {
                if (!target.IsReadOnly && target.IsEnabled)
                    action(target);
            }
        }

        private TextBox GetTargetTextBox()
        {
            if (InputTarget == null)
                return null;

            return InputTarget.GetVisualChildrenBreadthFirst().OfType<TextBox>().FirstOrDefault(x => x.IsFocused);
        }

        private Key GetButtonKey(ButtonBase button)
        {
            return (Key)Enum.Parse(typeof(Key), button.Tag.ToString());
        }

        private bool ShiftPressed => btnShift.IsChecked == true;

        #region InputTargetProperty
        public static DependencyProperty InputTargetProperty = DependencyProperty.Register("InputTarget", typeof(DependencyObject), typeof(EmbeddedKeyboard));
        public DependencyObject InputTarget
        {
            get { return (DependencyObject)GetValue(InputTargetProperty); }
            set { SetValue(InputTargetProperty, value); }
        }
        #endregion

        public static List<EmbeddedKeyboardLayout> Languages { get; } = new List<EmbeddedKeyboardLayout>(new[] { EmbeddedKeyboardLayout.English, EmbeddedKeyboardLayout.Russian });
    }
}