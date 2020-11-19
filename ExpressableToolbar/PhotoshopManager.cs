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

        static public double GetBrushDiameter()
        {
            /*if(!IsActive())
            {
                return -1;
            }*/

            try
            {
                var ref1 = new ActionReference();

                ref1.PutEnumerated(App.StringIDToTypeID("application"), App.StringIDToTypeID("ordinal"), App.StringIDToTypeID("targetEnum"));

                var capp = App.ExecuteActionGet(ref1);
                var currentToolOptions = capp.GetObjectValue(App.StringIDToTypeID("currentToolOptions"));
                var currentBrush = currentToolOptions.GetObjectValue(App.StringIDToTypeID("brush"));

                if (currentBrush.HasKey(App.StringIDToTypeID("hardness")))
                    Trace.WriteLine(currentBrush.GetDouble(App.StringIDToTypeID("hardness")));

                if (currentToolOptions.HasKey(App.StringIDToTypeID("flow")))
                    Trace.WriteLine(currentToolOptions.GetInteger(App.StringIDToTypeID("flow")));

                if (currentToolOptions.HasKey(App.StringIDToTypeID("opacity")))
                    Trace.WriteLine(currentToolOptions.GetInteger(App.StringIDToTypeID("opacity")));

                if (currentToolOptions.HasKey(App.StringIDToTypeID("smoothing")))
                    Trace.WriteLine(currentToolOptions.GetInteger(App.StringIDToTypeID("smoothing")));

                //return currentBrush.GetDouble(App.CharIDToTypeID("Dmtr"));
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

            return -1;
        }

        static public void SetBrushDiameter(double size)
        {
            /*if(!IsActive())
            {
                return;
            }*/

            // Retry again incase photoshop instance dies
            for (int i = 0; i < 1; i++)
            {
                try
                {
                    var desc1 = new ActionDescriptor();
                    var ref1 = new ActionReference();

                    ref1.PutEnumerated(App.CharIDToTypeID("Brsh"), App.CharIDToTypeID("Ordn"), App.CharIDToTypeID("Trgt"));
                    desc1.PutReference(App.CharIDToTypeID("null"), ref1);

                    var desc2 = new ActionDescriptor();

                    desc2.PutUnitDouble(App.StringIDToTypeID("masterDiameter"), App.CharIDToTypeID("#Pxl"), size);
                    desc2.PutDouble(App.StringIDToTypeID("hardness"), 100);

                    desc1.PutObject(App.CharIDToTypeID("T   "), App.CharIDToTypeID("Brsh"), desc2);

                    App.ExecuteAction(App.CharIDToTypeID("setd"), desc1, PsDialogModes.psDisplayNoDialogs);

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
