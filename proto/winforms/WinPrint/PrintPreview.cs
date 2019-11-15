﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.IO;

namespace WinPrint {
    /// <summary>
    /// WinPrint Print Preview control. Previews a single page.
    /// </summary>
    public partial class PrintPreview : Control {
        private string file;
        static public PrintPreview Instance = null;

        public string File {
            get => file; set {
                file = value;
                if (Document != null) Document.File = file;
            }
        }
        public Document Document { get; set; } = new Document();
        public int CurrentPage { get; set; }

        public PrintPreview() {
            Instance = this;
            InitializeComponent();
            CurrentPage = 1;
        }

        protected override void OnResize(EventArgs e) {
            this.Invalidate();
            base.OnResize(e);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        protected override void OnPaint(PaintEventArgs e) {
            if (e is null) throw new ArgumentNullException(nameof(e));
            if (Document is null) return;

            // Don't do anything if the window's been shrunk too far or GDI+ will crash
            if (ClientSize.Width <= Margin.Left + Margin.Right || ClientSize.Height <= Margin.Top + Margin.Bottom) return;
           
            // Paint rules, header, and footer
            Document.Paint(e.Graphics, CurrentPage);
        }

    }
}
