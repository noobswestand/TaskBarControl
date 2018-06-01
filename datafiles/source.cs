using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


public static class TaskbarProgress
{
    public enum TaskbarStates
    {
        NoProgress = 0,
        Indeterminate = 0x1,
        Normal = 0x2,
        Error = 0x4,
        Paused = 0x8
    }

    [ComImport()]
    [Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface ITaskbarList3
    {
        // ITaskbarList
        [PreserveSig]
        void HrInit();
        [PreserveSig]
        void AddTab(IntPtr hwnd);
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);

        // ITaskbarList2
        [PreserveSig]
        void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

        // ITaskbarList3
        [PreserveSig]
        void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
        [PreserveSig]
        void SetProgressState(IntPtr hwnd, TaskbarStates state);
    }

    [ComImport()]
    [Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
    [ClassInterface(ClassInterfaceType.None)]
    private class TaskbarInstance
    {
    }

    private static ITaskbarList3 taskbarInstance = (ITaskbarList3)new TaskbarInstance();
    private static bool taskbarSupported = Environment.OSVersion.Version >= new Version(6, 1);

    public static void SetState(IntPtr windowHandle, TaskbarStates taskbarState)
    {
        if (taskbarSupported) taskbarInstance.SetProgressState(windowHandle, taskbarState);
    }

    public static void SetValue(IntPtr windowHandle, double progressValue, double progressMax)
    {
        if (taskbarSupported) taskbarInstance.SetProgressValue(windowHandle, (ulong)progressValue, (ulong)progressMax);
    }
}


namespace ClassLibrary1
{
    public class Class1
    {
        [DllExport("window_error", CallingConvention.Cdecl)]
        public static double window_error(IntPtr Handle)
        {
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Error);
            return 1.0;
        }

        [DllExport("window_indeterminate", CallingConvention.Cdecl)]
        public static double window_indeterminate(IntPtr Handle)
        {
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Indeterminate);
            return 1.0;
        }

        [DllExport("window_normal", CallingConvention.Cdecl)]
        public static double window_normal(IntPtr Handle)
        {
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            return 1.0;
        }

        [DllExport("window_noprogress", CallingConvention.Cdecl)]
        public static double window_noprogress(IntPtr Handle)
        {
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.NoProgress);
            return 1.0;
        }


        [DllExport("window_paused", CallingConvention.Cdecl)]
        public static double window_paused(IntPtr Handle)
        {
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Paused);
            return 1.0;
        }


        [DllExport("window_value", CallingConvention.Cdecl)]
        public static double window_value(IntPtr Handle, double value, double maxvalue)
        {
            TaskbarProgress.SetValue(Handle, value, maxvalue);
            return 1.0;
        }
    }
}

