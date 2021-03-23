using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyVocabularyCards.Controls;
using MyVocabularyCards.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryControl), typeof(EntryRendererControl))]

namespace MyVocabularyCards.Droid.Controls
{
    public class EntryRendererControl : EntryRenderer
    {
        public EntryRendererControl(Context context) : base(context)
        {
            
        }

        public EntryControl entryControl => Element as EntryControl;

        protected override FormsEditText CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            UpdateBackground(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == EntryControl.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == EntryControl.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == EntryControl.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            }
            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateBackground();
        }
        protected void UpdateBackground(FormsEditText control)
        {
            if (control == null) return;

            var gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(entryControl.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(entryControl.BorderThickness), entryControl.BorderColor.ToAndroid());
            control.SetBackground(gd);

            var padTop = (int)Context.ToPixels(entryControl.Padding.Top);
            var padBottom = (int)Context.ToPixels(entryControl.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(entryControl.Padding.Left);
            var padRight = (int)Context.ToPixels(entryControl.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }
        protected void UpdateBackground()
        {
            UpdateBackground(Control);
        }
    }
}