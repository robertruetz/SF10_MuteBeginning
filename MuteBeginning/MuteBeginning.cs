using System;
using SoundForge;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EntryPoint
{
    public void Begin(IScriptableApp app)
    {
        ISfDataWnd wnd = app.ActiveWindow;
        long currentPos = wnd.Cursor;
        long secToSamp = app.CurrentFile.SecondsToPosition(.350);
        wnd.SetCursorAndScroll(currentPos - secToSamp, DataWndScrollTo.NoMove);
    }

    public void FromSoundForge(IScriptableApp app)
    {
        ForgeApp = app; //execution begins here
        app.SetStatusText(String.Format("Script '{0}' is running.", Script.Name));
        Begin(app);
        app.SetStatusText(String.Format("Script '{0}' is done.", Script.Name));
    }
    public static IScriptableApp ForgeApp = null;
    public static void DPF(string sz) { ForgeApp.OutputText(sz); }
    public static void DPF(string fmt, params object[] args) { ForgeApp.OutputText(String.Format(fmt, args)); }
} //EntryPoint
