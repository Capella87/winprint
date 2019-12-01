﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.WinForms;
using WinPrint.Core.Models;

namespace WinPrint {
    /// <summary>
    /// Implements generic HTML file type support. 
    /// </summary>
    sealed class HtmlFileContent : ContentBase, IDisposable  {
        private readonly SheetViewModel containingSheet;

        public HtmlFileContent(SheetViewModel sheet) {
            containingSheet = sheet;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        // Flag: Has Dispose already been called?
        bool disposed = false;
        void Dispose(bool disposing) {
            if (disposed)
                return;

            if (disposing) {
            }
            disposed = true;
        }

        private string html;


        /// <summary>
        /// Get total count of pages. Set any local page-size related values (e.g. linesPerPage)
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal override List<Page> GetPages(StreamReader streamToPrint) {
            List<Page> pages = new List<Page>();

            Page page = null;

            html = streamToPrint.ReadToEnd();
            using Bitmap bitmap = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bitmap);
            //g.PageUnit = GraphicsUnit.Document;

            SizeF size = HtmlRender.MeasureGdiPlus(g, html, containingSheet.GetPageWidth());
            
            int numPages = (int)(size.Height / containingSheet.GetPageHeight());

            for (int i = 0; i < numPages; i++) { 
                page = new Page(containingSheet);
                pages.Add(page);
                page.PageNum = pages.Count;
            }

            return pages;
        }

        /// <summary>
        /// Gets next page from stream. Returns false if no more pages
        /// </summary>
        /// <param name="streamToPrint"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        // TODO: Deal with PageFeeds
        // TODO: Support different forms of linewrap (truncate/clip, word, line)
        // TODO: Support custom line wrap symbol
        //private bool NextPage(StreamReader streamToPrint, out Page page) {
        //    page = new Page(containingSheet);

        //    // define context used for determining glyph metrics.        
        //    using Bitmap bitmap = new Bitmap(1, 1);
        //    Graphics g = Graphics.FromImage(bitmap);
        //    //g = Graphics.FromHwnd(PrintPreview.Instance.Handle);
        //    g.PageUnit = GraphicsUnit.Document;


        //    SizeF size = HtmlRender.MeasureGdiPlus(g, html, containingSheet.GetPageWidth());

        //    return false;
        //}

        /// <summary>
        /// Paints a single page
        /// </summary>
        /// <param name="g">Graphics with 0,0 being the origin of the Page</param>
        /// <param name="pageNum">Page number to print</param>
        internal override void PaintPage(Graphics g, int pageNum) {
            float leftMargin = 0;
            float contentHeight = containingSheet.GetPageHeight();

            SizeF size = new SizeF(containingSheet.GetPageWidth(), containingSheet.GetPageHeight());

            SizeF renderedSize = HtmlRender.RenderGdiPlus(g, html, new PointF(0, 0), size );
            
        }
    }
}
