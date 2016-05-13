using Cimbalino.Toolkit.Controls;
using Cimbalino.Toolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace VirtualWallet.Controls
{
    public class HamburgerArgumentMenuButton : ToggleButton
    {
        private static readonly IList<HamburgerArgumentMenuButton> registerdHamburgerArgumentMenuButtons = new List<HamburgerArgumentMenuButton>();
        private HamburgerFrame hamburgerFrame;

        /// <summary>
        /// Gets or sets the <see cref="IconElement"/> for this button.
        /// </summary>
        /// <value>The <see cref="IconElement"/> for this button.</value>
        public IconElement Icon
        {
            get { return (IconElement)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Identifier for the <see cref="IconProperty" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(IconElement), typeof(HamburgerArgumentMenuButton), new PropertyMetadata(null));

        /// <summary>
        /// Gets the Icon <see cref="Visibility"/> for this button.
        /// </summary>
        /// <value>The Icon <see cref="Visibility"/> for this button.</value>
        public Visibility IconVisibility
        {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }

        /// <summary>
        /// Identifier for the <see cref="IconProperty" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register(nameof(IconVisibility), typeof(Visibility), typeof(HamburgerArgumentMenuButton), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the visibility of the label.
        /// </summary>
        /// <value>The visibility of the label.</value>
        public Visibility LabelVisibility
        {
            get { return (Visibility)GetValue(LabelVisibilityProperty); }
            set { SetValue(LabelVisibilityProperty, value); }
        }

        /// <summary>
        /// Identifier for the <see cref="LabelVisibility" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty LabelVisibilityProperty =
            DependencyProperty.Register(nameof(LabelVisibility), typeof(Visibility), typeof(HamburgerArgumentMenuButton), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Gets or sets the navigation source page type.
        /// </summary>
        /// <value>The navigation source page type.</value>
        public Type NavigationSourcePageType
        {
            get { return (Type)GetValue(NavigationSourcePageTypeProperty); }
            set { SetValue(NavigationSourcePageTypeProperty, value); }
        }

        /// <summary>
        /// Identifier for the <see cref="NavigationSourcePageType" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty NavigationSourcePageTypeProperty =
            DependencyProperty.Register(nameof(NavigationSourcePageType), typeof(Type), typeof(HamburgerArgumentMenuButton), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the navigation parameter.
        /// </summary>
        /// <value>The navigation parameter.</value>
        public object NavigationParameter
        {
            get { return GetValue(NavigationParameterProperty); }
            set { SetValue(NavigationParameterProperty, value); }
        }

        /// <summary>
        /// Identifier for the <see cref="NavigationParameter" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty NavigationParameterProperty =
            DependencyProperty.Register(nameof(NavigationParameter), typeof(object), typeof(HamburgerArgumentMenuButton), new PropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the <see cref="HamburgerArgumentMenuButton" /> class.
        /// </summary>
        public HamburgerArgumentMenuButton()
        {
            DefaultStyleKey = typeof(HamburgerArgumentMenuButton);

            Loaded += HamburgerSubMenuButton_Loaded;
            Unloaded += HamburgerSubMenuButton_Unloaded;
        }

        /// <summary>
        /// Called when the ToggleButton receives toggle stimulus. Overrided to disable the auto-toggling of the control.
        /// </summary>
        protected override void OnToggle()
        {
        }

        private void HamburgerSubMenuButton_Loaded(object sender, RoutedEventArgs e)
        {
            hamburgerFrame = this.GetVisualAncestor<HamburgerFrame>();

            if (NavigationSourcePageType != null && hamburgerFrame.CurrentSourcePageType != null)
                IsChecked = NavigationSourcePageType == hamburgerFrame.CurrentSourcePageType;

            if (hamburgerFrame != null)
            {
                lock (registerdHamburgerArgumentMenuButtons)
                {
                    if (!registerdHamburgerArgumentMenuButtons.Any())
                    {
                        hamburgerFrame.Navigated -= HamburgerFrame_Navigated;
                        hamburgerFrame.Navigated += HamburgerFrame_Navigated;
                    }

                    registerdHamburgerArgumentMenuButtons.Add(this);
                    Click += HamburgerSubMenuButton_Click;
                }
            }
        }

        private void HamburgerSubMenuButton_Unloaded(object sender, RoutedEventArgs e)
        {
            lock (registerdHamburgerArgumentMenuButtons)
            {
                Click -= HamburgerSubMenuButton_Click;
                registerdHamburgerArgumentMenuButtons.Remove(this);

                if (hamburgerFrame != null && !registerdHamburgerArgumentMenuButtons.Any())
                    hamburgerFrame.Navigated -= HamburgerFrame_Navigated;
            }
        }

        private void HamburgerSubMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (hamburgerFrame == null)
                return;

            var navigationSourcePageType = ((HamburgerArgumentMenuButton)sender).NavigationSourcePageType;

            if (navigationSourcePageType != null)
                hamburgerFrame.Navigate(navigationSourcePageType, NavigationParameter);

            if (hamburgerFrame.DisplayMode == SplitViewDisplayMode.Overlay || hamburgerFrame.DisplayMode == SplitViewDisplayMode.CompactOverlay)
                hamburgerFrame.IsPaneOpen = false;
        }

        private void HamburgerFrame_Navigated(object sender, NavigationEventArgs e)
        {
            lock (registerdHamburgerArgumentMenuButtons)
            {
                foreach (var hamburgerButton in registerdHamburgerArgumentMenuButtons)
                    if (hamburgerButton.NavigationSourcePageType != null)
                        hamburgerButton.IsChecked = hamburgerButton.NavigationSourcePageType == e.SourcePageType && hamburgerButton.NavigationParameter == e.Parameter;
            }
        }
    }
}
