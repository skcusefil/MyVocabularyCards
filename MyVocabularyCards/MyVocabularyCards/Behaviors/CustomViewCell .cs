using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

//namespace MyVocabularyCards.Behaviors
//{
//    public class CustomViewCell :ViewCell
//    {
//        public static readonly BindableProperty SelectedItemBackgroundColorProperty =
//        BindableProperty.Create("SelectedItemBackgroundColor",
//                                typeof(Color),
//                                typeof(CustomViewCell),
//                                Color.Accent);

//        public Color SelectedItemBackgroundColor
//        {
//            get { return (Color)GetValue(SelectedItemBackgroundColorProperty); }
//            set 
//            { 
//                SetValue(SelectedItemBackgroundColorProperty, value); 
//            }
//        }

//        private static void SelectionColorChanged(BindableObject bindable, object oldvalue, object newvalue)
//        {
//            if (!(bindable is CustomViewCell viewCell)) return;

//            var color = (Color)newvalue;

//            viewCell.View.BackgroundColor = color;
//        }
//    }
//}
