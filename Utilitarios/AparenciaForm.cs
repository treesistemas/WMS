using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Utilitarios
{
    public class AparenciaForm
    {
        //Controle para mover a tela
        public const int WM_NCLBUTTONDOWN = 0XA1;
        public const int HT_CAPITION = 0X2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]

        public static extern bool ReleaseCapture();

    }





}


