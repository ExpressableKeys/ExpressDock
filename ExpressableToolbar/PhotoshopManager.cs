// Copyright 2020 Expressable. All Rights Reserved.

using System;
using System.Runtime.InteropServices;
using Photoshop;

namespace Expressable
{
    class PhotoshopManager
    {
        static private Photoshop.Application App;

        static public void Initialize()
        {
            if (App != null)
                return;

            // Try to get a running instance of the Photoshop COM object.
            try
            {
                App = (Photoshop.Application)Marshal.GetActiveObject("Photoshop.Application");
            }
            catch (COMException ex)
            {
                Console.WriteLine("Photoshop instance is not running.");
                Console.WriteLine(ex.Message);

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
            try
            {
                var ref1 = new ActionReference();

                ref1.PutEnumerated(App.CharIDToTypeID("capp"), App.CharIDToTypeID("Ordn"), App.CharIDToTypeID("Trgt"));

                var currentBrush = App.ExecuteActionGet(ref1)
                    .GetObjectValue(App.StringIDToTypeID("currentToolOptions"))
                    .GetObjectValue(App.CharIDToTypeID("Brsh"));

                return currentBrush.GetDouble(App.CharIDToTypeID("Dmtr"));
            }
            catch (COMException ex)
            {
                Console.WriteLine("Photoshop instance is no longer running.");
                Console.WriteLine(ex.Message);

                App = null;
            }

            return -1;
        }

        static public void SetBrushDiameter(double size)
        {
            if (App == null)
                Initialize();

            // Retry again incase photoshop instance dies
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    var desc1 = new ActionDescriptor();
                    var ref1 = new ActionReference();

                    ref1.PutEnumerated(App.CharIDToTypeID("Brsh"), App.CharIDToTypeID("Ordn"), App.CharIDToTypeID("Trgt"));
                    desc1.PutReference(App.CharIDToTypeID("null"), ref1);

                    var desc2 = new ActionDescriptor();

                    desc2.PutUnitDouble(App.StringIDToTypeID("masterDiameter"), App.CharIDToTypeID("#Pxl"), size);
                    desc1.PutObject(App.CharIDToTypeID("T   "), App.CharIDToTypeID("Brsh"), desc2);

                    App.ExecuteAction(App.CharIDToTypeID("setd"), desc1);

                    return;
                }
                catch (COMException ex)
                {
                    Console.WriteLine("Photoshop handle is outdated.");
                    Console.WriteLine(ex.Message);

                    App = null;

                    Initialize();

                    // If Photoshop does not exist anymore, we cannot continue.
                    if (App == null)
                        return;
                }
            }
        }
    }
}
