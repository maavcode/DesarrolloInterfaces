using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace playground_.net_BasicThings.Frontend.Mensajes
{
    public enum MessageType
    {
        Info,
        Warning,
        Error,
        Success
    }
    /// <summary>
    /// Interaction logic for MensajeDialogo.xaml
    /// </summary>

    /// <summary>
    /// Diálogo genérico para mostrar mensajes (Error / Advertencia / Información / Success).
    /// Selecciona color e icono por tipo. Si se proporciona IconSource se usa la imagen en su lugar.
    /// </summary>
    public partial class MensajeDialogo : Window
        {
            public MensajeDialogo()
            {
                InitializeComponent();
                AccentColor = new SolidColorBrush(Color.FromRgb(33, 150, 243)); // azul por defecto
            }

            #region DependencyProperties

            public static readonly DependencyProperty TitleTextProperty =
                DependencyProperty.Register(nameof(TitleText), typeof(string), typeof(MensajeDialogo), new PropertyMetadata(string.Empty));

            public string TitleText
            {
                get => (string)GetValue(TitleTextProperty);
                set => SetValue(TitleTextProperty, value);
            }

            public static readonly DependencyProperty MessageTextProperty =
                DependencyProperty.Register(nameof(MessageText), typeof(string), typeof(MensajeDialogo), new PropertyMetadata(string.Empty));

            public string MessageText
            {
                get => (string)GetValue(MessageTextProperty);
                set => SetValue(MessageTextProperty, value);
            }

            public static readonly DependencyProperty AccentColorProperty =
                DependencyProperty.Register(nameof(AccentColor), typeof(Brush), typeof(MensajeDialogo), new PropertyMetadata(Brushes.DodgerBlue));

            public Brush AccentColor
            {
                get => (Brush)GetValue(AccentColorProperty);
                set => SetValue(AccentColorProperty, value);
            }

            public static readonly DependencyProperty IconSourceProperty =
                DependencyProperty.Register(nameof(IconSource), typeof(ImageSource), typeof(MensajeDialogo), new PropertyMetadata(null));

            public ImageSource IconSource
            {
                get => (ImageSource)GetValue(IconSourceProperty);
                set => SetValue(IconSourceProperty, value);
            }

            // Glyph mostrado cuando no hay IconSource
            public static readonly DependencyProperty IconGlyphProperty =
                DependencyProperty.Register(nameof(IconGlyph), typeof(string), typeof(MensajeDialogo), new PropertyMetadata(string.Empty));

            public string IconGlyph
            {
                get => (string)GetValue(IconGlyphProperty);
                set => SetValue(IconGlyphProperty, value);
            }

            // Tipo de mensaje
            public static readonly DependencyProperty MessageTypeProperty =
                DependencyProperty.Register(nameof(MessageType), typeof(MessageType), typeof(MensajeDialogo), new PropertyMetadata(MessageType.Info, OnMessageTypeChanged));

            public MessageType MessageType
            {
                get => (MessageType)GetValue(MessageTypeProperty);
                set => SetValue(MessageTypeProperty, value);
            }

            private static void OnMessageTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (d is MensajeDialogo dlg && e.NewValue is MessageType mt)
                {
                    dlg.ApplyDefaultsForMessageType(mt);
                }
            }

            #endregion

            #region Helpers: mapping por tipo

            private void ApplyDefaultsForMessageType(MessageType type)
            {
                // Si se ha definido explícitamente IconSource por el llamador, no override de la imagen,
                // pero sí podemos ajustar el color si no se ha establecido.
                if (IconSource == null)
                {
                    switch (type)
                    {
                        case MessageType.Info:
                            IconGlyph = "i";
                            AccentColor = new SolidColorBrush(Color.FromRgb(33, 150, 243)); // blue
                            break;
                        case MessageType.Warning:
                            IconGlyph = "!";
                            AccentColor = new SolidColorBrush(Color.FromRgb(255, 193, 7)); // amber
                            break;
                        case MessageType.Error:
                            IconGlyph = "✖";
                            AccentColor = new SolidColorBrush(Color.FromRgb(211, 47, 47)); // red
                            break;
                        case MessageType.Success:
                            IconGlyph = "✔";
                            AccentColor = new SolidColorBrush(Color.FromRgb(76, 175, 80)); // green
                            break;
                        default:
                            IconGlyph = "i";
                            AccentColor = new SolidColorBrush(Color.FromRgb(33, 150, 243));
                            break;
                    }
                }
            }

            #endregion

            #region Static helpers

            public static void Show(MessageType type, string title, string message, ImageSource icon = null, Brush accent = null, Window owner = null)
            {
                var dlg = new MensajeDialogo
                {
                    TitleText = title ?? string.Empty,
                    MessageText = message ?? string.Empty,
                    IconSource = icon,
                    MessageType = type
                };

                // Si se pasa accent explícito lo usamos; si no, ApplyDefaultsForMessageType ya setea un color
                if (accent != null)
                    dlg.AccentColor = accent;

                if (owner == null)
                    owner = Application.Current?.MainWindow;

                if (owner != null)
                {
                    dlg.Owner = owner;
                    dlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                }
                else
                {
                    dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }

                dlg.ShowDialog();
            }

            public static void ShowInfo(string title, string message, Window owner = null) =>
                Show(MessageType.Info, title, message, null, null, owner);

            public static void ShowWarning(string title, string message, Window owner = null) =>
                Show(MessageType.Warning, title, message, null, null, owner);

            public static void ShowError(string title, string message, Window owner = null) =>
                Show(MessageType.Error, title, message, null, null, owner);

            public static void ShowSuccess(string title, string message, Window owner = null) =>
                Show(MessageType.Success, title, message, null, null, owner);

            public static Task ShowAsync(MessageType type, string title, string message, ImageSource icon = null, Brush accent = null, Window owner = null)
            {
                return Task.Run(() =>
                {
                    Application.Current?.Dispatcher.Invoke(() => Show(type, title, message, icon, accent, owner));
                });
            }

            #endregion

            #region Button handlers

            private void OkButton_Click(object sender, RoutedEventArgs e)
            {
                DialogResult = true;
                Close();
            }

            private void CancelButton_Click(object sender, RoutedEventArgs e)
            {
                DialogResult = false;
                Close();
            }

            #endregion

            #region Helpers to load icons from resources (opcional)

            public static ImageSource LoadIcon(string uri)
            {
                if (string.IsNullOrWhiteSpace(uri)) return null;
                try
                {
                    return new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
                }
                catch
                {
                    return null;
                }
            }

            #endregion
        }
    }

