using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Kudomion
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                var shape =
                    new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.SetStyle(Paint.Style.Stroke);
                Control.Background = shape;
            }
        }
    }
}
