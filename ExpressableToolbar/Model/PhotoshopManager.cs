// Copyright 2020 Expressable. All Rights Reserved.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Photoshop;

namespace Expressable
{
    enum PhotoshopExceptionCode : int
    {
        NOTAVAILABLE = -2147212704,
        RPCERROR = -2147023174,
    }

    public class PhotoshopBrushData
    {
        public double Diameter = -1;
        public double Hardness = -1;
        public double Flow = -1;
        public double Opacity = -1;
    }

    class PhotoshopManager
    {
        static private Photoshop.Application App;

        static private Photoshop.ActionDescriptor AppDescriptor;
        static private Photoshop.ActionReference AppReference;

        static private Photoshop.ActionReference DocumentReference;
        static private Photoshop.ActionDescriptor DocumentDescriptor;

        static public void Initialize()
        {
            if (App != null)
                return;

            if (Process.GetProcessesByName("Photoshop").Length > 0)
            {
                // Many of these are fairly slow operations. (Thank you, Photoshop!)
                // We will do them once to try and speed things up.

                // Create COM object.

                //App = new Photoshop.Application();

                App = (Photoshop.Application)Activator.CreateInstance(Type.GetTypeFromProgID("Photoshop.Application"));

                // Get an action reference to the photoshop application
                AppReference = new ActionReference();

                AppReference.PutProperty(App.StringIDToTypeID("property"), App.StringIDToTypeID("tool"));
                AppReference.PutEnumerated(App.StringIDToTypeID("application"), App.StringIDToTypeID("ordinal"), App.StringIDToTypeID("targetEnum"));

                UpdateApp();

                // Get an action reference to the document.
                DocumentReference = new ActionReference();

                DocumentReference.PutProperty(App.StringIDToTypeID("property"), App.StringIDToTypeID("zoom"));
                DocumentReference.PutEnumerated(App.StringIDToTypeID("document"), App.StringIDToTypeID("ordinal"), App.StringIDToTypeID("targetEnum"));

                UpdateDocument();
            }
            else
            {
                App = null;
            }
        }

        static public bool UpdateApp()
        {
            try
            {
                AppDescriptor = App.ExecuteActionGet(AppReference);
                return true;
            }
            catch (COMException)
            {
                return false;
            }
        }

        static public bool UpdateDocument()
        {
            try
            {
                if (App.ActiveDocument == null)
                    Trace.WriteLine("Not epic!");
                DocumentDescriptor = App.ExecuteActionGet(DocumentReference);
                return true;
            }
            catch (COMException)
            {
                return false;
            }
        }

        static public bool IsActive()
        {
            Initialize();
            return App != null;
        }

        static public double GetZoom()
        {
            UpdateDocument();
            return DocumentDescriptor?.GetDouble(App.StringIDToTypeID("zoom")) ?? -1.0;
        }

        static public PhotoshopBrushData GetBrushSettings()
        {
            if(!IsActive())
            {
                return new PhotoshopBrushData();
            }

            try
            {
                UpdateApp();

                // Create action descriptors for the current tool options and current brush
                var currentToolOptions = AppDescriptor.GetObjectValue(App.StringIDToTypeID("currentToolOptions"));
                var currentBrush = currentToolOptions.GetObjectValue(App.StringIDToTypeID("brush"));

                var brushData = new PhotoshopBrushData();

                // Grab brush values if they exist
                if (currentBrush.HasKey(App.StringIDToTypeID("diameter")))
                    brushData.Diameter = currentBrush.GetDouble(App.StringIDToTypeID("diameter"));

                if (currentBrush.HasKey(App.StringIDToTypeID("hardness")))
                    brushData.Hardness = currentBrush.GetDouble(App.StringIDToTypeID("hardness"));

                if (currentToolOptions.HasKey(App.StringIDToTypeID("flow")))
                    brushData.Flow = currentToolOptions.GetDouble(App.StringIDToTypeID("flow"));

                if (currentToolOptions.HasKey(App.StringIDToTypeID("opacity")))
                    brushData.Opacity = currentToolOptions.GetDouble(App.StringIDToTypeID("opacity"));

                return brushData;
            }
            catch (COMException ex)
            {
                CatchPhotoshopException(ex);
            }

            return new PhotoshopBrushData();
        }

        static public void SetBrushDiameter(double diameter)
        {
            SetBrushSettings(diameter, -1, -1, -1);
        }
        static public void SetBrushHardness(double hardness)
        {
            SetBrushSettings(-1, hardness, -1, -1);
        }

        static public void SetBrushFlow(double flow)
        {
            SetBrushSettings(-1, -1, flow, -1);
        }

        static public void SetBrushOpacity(double opacity)
        {
            SetBrushSettings(-1, -1, -1, opacity);
        }

        static public void SetBrushSettings(PhotoshopBrushData brush)
        {
            SetBrushSettings(brush.Diameter, brush.Hardness, brush.Flow, brush.Opacity);
        }

        static public void SetBrushSettings(double diameter, double hardness, double flow, double opacity)
        {
            if(!IsActive())
            {
                return;
            }

            try
            {
                UpdateApp();

                // Get action desctiptors for the tool options, the current tool and the brush attributes.
                var currentToolOptions = AppDescriptor.GetObjectValue(App.StringIDToTypeID("currentToolOptions"));
                var currentTool = AppDescriptor.GetEnumerationType(App.StringIDToTypeID("tool"));
                var currentBrush = currentToolOptions.GetObjectValue(App.StringIDToTypeID("brush"));

                // Get an action reference to the current tool
                // so we can set the tool's settings.
                var toolRef = new ActionReference();
                toolRef.PutClass(currentTool);

                // Set brush settings
                if (currentBrush.HasKey(App.StringIDToTypeID("diameter")) && diameter != -1)
                    currentBrush.PutDouble(App.StringIDToTypeID("diameter"), diameter);

                if (currentBrush.HasKey(App.StringIDToTypeID("hardness")) && hardness != -1)
                    currentBrush.PutDouble(App.StringIDToTypeID("hardness"), hardness);

                if (currentToolOptions.HasKey(App.StringIDToTypeID("flow")) && flow != -1)
                    currentToolOptions.PutDouble(App.StringIDToTypeID("flow"), flow);

                if (currentToolOptions.HasKey(App.StringIDToTypeID("opacity")) && opacity != -1)
                    currentToolOptions.PutDouble(App.StringIDToTypeID("opacity"), opacity);

                currentToolOptions.PutObject(App.StringIDToTypeID("brush"), App.StringIDToTypeID("null"), currentBrush);

                var setBrush = new ActionDescriptor(); 
                setBrush.PutReference(App.StringIDToTypeID("null"), toolRef);
                setBrush.PutObject(App.StringIDToTypeID("to"), App.StringIDToTypeID("null"), currentToolOptions);

                App.ExecuteAction(App.StringIDToTypeID("set"), setBrush, PsDialogModes.psDisplayNoDialogs);

                return;
            }
            catch (COMException ex)
            {
                CatchPhotoshopException(ex);
            }
        }

        static private void CatchPhotoshopException(COMException ex)
        {
            switch ((PhotoshopExceptionCode)ex.ErrorCode)
            {
                case PhotoshopExceptionCode.RPCERROR:
                    Trace.WriteLine("Photoshop handle is outdated.");
                    App = null;

                    break;

                case PhotoshopExceptionCode.NOTAVAILABLE:
                    Trace.WriteLine("Tool does not have brush settings.");

                    break;
            }
        }
    }
}
