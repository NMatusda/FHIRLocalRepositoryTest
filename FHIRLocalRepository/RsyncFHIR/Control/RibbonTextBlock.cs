using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls.Ribbon;

namespace RsyncFHIR.Control
{
    public class RibbonTextBlock : System.Windows.Controls.Control
    {
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(RibbonTextBlock), new UIPropertyMetadata(string.Empty));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(RibbonTextBlock), new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// CornerRadius of the RibbonTextBox
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return RibbonControlService.GetCornerRadius(this); }
            set { RibbonControlService.SetCornerRadius(this, value); }
        }

        /// <summary>
        /// DependencyProperty for CornerRadius
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            RibbonControlService.CornerRadiusProperty.AddOwner(typeof(RibbonTextBlock));

        /// <summary>
        ///     Size definition to apply to this control when it's placed in a QuickAccessToolBar.
        /// </summary>
        public RibbonControlSizeDefinition QuickAccessToolBarControlSizeDefinition
        {
            get { return RibbonControlService.GetQuickAccessToolBarControlSizeDefinition(this); }
            set { RibbonControlService.SetQuickAccessToolBarControlSizeDefinition(this, value); }
        }

        /// <summary>
        ///     DependencyProperty for QuickAccessToolBarControlSizeDefinition property.
        /// </summary>
        public static readonly DependencyProperty QuickAccessToolBarControlSizeDefinitionProperty =
            RibbonControlService.QuickAccessToolBarControlSizeDefinitionProperty.AddOwner(typeof(RibbonTextBlock));

        static RibbonTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RibbonTextBlock), new FrameworkPropertyMetadata(typeof(RibbonTextBlock)));
        }
    }
}
