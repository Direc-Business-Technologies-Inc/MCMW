using System;
using System.Windows.Forms;

namespace DirecLayer
{
    public class EventHelper
    {
        public static void RaisedEvent(object objectRaised,
            EventHandler eventHandlerRaised, EventArgs eventArgs)
        {
            if (eventHandlerRaised != null)
            {
                eventHandlerRaised.Invoke(objectRaised, eventArgs);
            }
        }

        public static void RaisedCellEvent(object objectRaised,
            DataGridViewCellEventHandler eventHandlerRaised, DataGridViewCellEventArgs e)
        {
            if (eventHandlerRaised != null)
            {
                eventHandlerRaised.Invoke(objectRaised, e);
            }
        }

        public static void RaisedCellMouseEvent(object objectRaised,
            DataGridViewCellMouseEventHandler eventHandlerRaised, DataGridViewCellMouseEventArgs e)
        {
            if (eventHandlerRaised != null)
            {
                eventHandlerRaised.Invoke(objectRaised, e);
            }
        }

        public static void RaiseFormCloseEvent(object objectRaised,
            FormClosingEventHandler eventHandlerRaised, FormClosingEventArgs e)
        {
            if (eventHandlerRaised != null)
            {
                eventHandlerRaised.Invoke(objectRaised, e);
            }
        }

        public static void RaisedPreviewKeyDown(object objectRaised,
            PreviewKeyDownEventHandler eventHandlerRaised, PreviewKeyDownEventArgs e)
        {
            if (eventHandlerRaised != null)
            {
                eventHandlerRaised.Invoke(objectRaised, e);
            }
        }

        public static void RaisedKeyPress(object objectRaised,
            KeyPressEventHandler eventHandlerRaised, KeyPressEventArgs e)
        {
            if (eventHandlerRaised != null)
            {
                eventHandlerRaised.Invoke(objectRaised, e);
            }
        }

        public static void RaisedRowPostPaint(object objectRaised,
           DataGridViewRowPostPaintEventHandler eventHandlerRaised, DataGridViewRowPostPaintEventArgs e)
        {
            if (eventHandlerRaised != null)
            {
                eventHandlerRaised.Invoke(objectRaised, e);
            }
        }
    }
}   
