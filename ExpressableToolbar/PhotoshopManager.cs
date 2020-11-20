// Copyright 2020 Expressable. All Rights Reserved.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Photoshop;

namespace Expressable
{
    enum PhotoshopExceptionResult : int
    {
        NOTAVAIL = -2147212704,
        RPCERR = -2147023174,
    }

    public class PhotoshopBrush
    {
        public double Diameter = -1;
        public double Hardness = -1;

        public int Flow = -1;
        public int Opacity = -1;
    }

    class PhotoshopManager
    {
        static private Photoshop.Application App;

        static public void Initialize()
        {
            if (Process.GetProcessesByName("Photoshop").Length > 0)
            {
                App = new Photoshop.Application();
            }
            else
            {
                App = null;
            }
        }

        static public bool IsActive()
        {
            Initialize();
            return App != null;
        }

        static public double GetZoom()
        {
            var ref1 = new ActionReference();

            ref1.PutProperty(App.StringIDToTypeID("property"), App.StringIDToTypeID("zoom"));
            ref1.PutEnumerated(App.StringIDToTypeID("document"), App.StringIDToTypeID("ordinal"), App.StringIDToTypeID("targetEnum"));

            return App.ExecuteActionGet(ref1).GetDouble(App.StringIDToTypeID("zoom"));
        }

        static public PhotoshopBrush GetBrushSettings()
        {
            if(!IsActive())
            {
                return null;
            }

            try
            {
                var ref1 = new ActionReference();

                ref1.PutEnumerated(App.StringIDToTypeID("application"), App.StringIDToTypeID("ordinal"), App.StringIDToTypeID("targetEnum"));

                var capp = App.ExecuteActionGet(ref1);

                var currentToolOptions = capp.GetObjectValue(App.StringIDToTypeID("currentToolOptions"));
                var currentBrush = currentToolOptions.GetObjectValue(App.StringIDToTypeID("brush"));

                var brushData = new PhotoshopBrush();

                if (currentBrush.HasKey(App.StringIDToTypeID("diameter")))
                    brushData.Diameter = currentBrush.GetDouble(App.StringIDToTypeID("diameter"));

                if (currentBrush.HasKey(App.StringIDToTypeID("hardness")))
                    brushData.Hardness = currentBrush.GetDouble(App.StringIDToTypeID("hardness"));

                if (currentToolOptions.HasKey(App.StringIDToTypeID("flow")))
                    brushData.Flow = currentToolOptions.GetInteger(App.StringIDToTypeID("flow"));

                if (currentToolOptions.HasKey(App.StringIDToTypeID("opacity")))
                    brushData.Opacity = currentToolOptions.GetInteger(App.StringIDToTypeID("opacity"));

                return brushData;
            }
            catch (COMException ex)
            {
                switch ((PhotoshopExceptionResult)ex.ErrorCode)
                {
                    case PhotoshopExceptionResult.RPCERR:
                        Trace.WriteLine("Photoshop handle is outdated.");
                        Trace.WriteLine(ex.ErrorCode);
                        Trace.WriteLine(ex.Message);

                        break;

                    case PhotoshopExceptionResult.NOTAVAIL:
                        Trace.WriteLine("Tool does not have brush settings.");
                        break;
                }
            }

            return null;
        }
        static public void SetBrushDiameter(double diameter)
        {
            SetBrushSettings(diameter, -1, -1, -1);
        }
        static public void SetBrushHardness(double hardness)
        {
            SetBrushSettings(-1, hardness, -1, -1);
        }

        static public void SetBrushFlow(int flow)
        {
            SetBrushSettings(-1, -1, flow, -1);
        }

        static public void SetBrushOpacity(int opacity)
        {
            SetBrushSettings(-1, -1, -1, opacity);
        }

        static public void SetBrushSettings(PhotoshopBrush brush)
        {
            SetBrushSettings(brush.Diameter, brush.Hardness, brush.Flow, brush.Opacity);
        }

        static public void SetBrushSettings(double diameter, double hardness, int flow, int opacity)
        {
            if(!IsActive())
            {
                return;
            }

            for (int i = 0; i < 1; i++)
            {
                try
                {
                    var ref1 = new ActionReference();
                    ref1.PutProperty(App.StringIDToTypeID("property"), App.StringIDToTypeID("tool"));
                    ref1.PutEnumerated(App.StringIDToTypeID("application"), App.StringIDToTypeID("ordinal"), App.StringIDToTypeID("targetEnum"));

                    var capp = App.ExecuteActionGet(ref1);

                    var currentToolOptions = capp.GetObjectValue(App.StringIDToTypeID("currentToolOptions"));
                    Trace.WriteLine("");
                    var currentTool = capp.GetEnumerationType(App.StringIDToTypeID("tool"));
                    Trace.WriteLine("");
                    var currentBrush = currentToolOptions.GetObjectValue(App.StringIDToTypeID("brush"));

                    var toolRef = new ActionReference();
                    toolRef.PutClass(currentTool);

                    if (currentBrush.HasKey(App.StringIDToTypeID("diameter")) && diameter != -1)
                        currentBrush.PutDouble(App.StringIDToTypeID("diameter"), diameter);

                    if (currentBrush.HasKey(App.StringIDToTypeID("hardness")) && hardness != -1)
                        currentBrush.PutDouble(App.StringIDToTypeID("hardness"), hardness);

                    if (currentToolOptions.HasKey(App.StringIDToTypeID("flow")) && flow != -1)
                        currentToolOptions.PutInteger(App.StringIDToTypeID("flow"), flow);

                    if (currentToolOptions.HasKey(App.StringIDToTypeID("opacity")) && opacity != -1)
                        currentToolOptions.PutInteger(App.StringIDToTypeID("opacity"), opacity);

                    currentToolOptions.PutObject(App.StringIDToTypeID("brush"), App.StringIDToTypeID("null"), currentBrush);

                    var setBrush = new ActionDescriptor(); 
                    setBrush.PutReference(App.StringIDToTypeID("null"), toolRef);
                    setBrush.PutObject(App.StringIDToTypeID("to"), App.StringIDToTypeID("null"), currentToolOptions);

                    App.ExecuteAction(App.StringIDToTypeID("set"), setBrush, PsDialogModes.psDisplayNoDialogs);

                    return;
                }
                catch (COMException ex)
                {
                    switch ((PhotoshopExceptionResult)ex.ErrorCode)
                    {
                        case PhotoshopExceptionResult.RPCERR:
                            Trace.WriteLine("Photoshop handle is outdated.");
                            Trace.WriteLine(ex.ErrorCode);
                            Trace.WriteLine(ex.Message);

                            break;

                        case PhotoshopExceptionResult.NOTAVAIL:
                            Trace.WriteLine("Tool does not have brush settings.");
                            break;
                    }
                }
            }
        }
    }
}
