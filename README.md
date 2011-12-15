PDFBinder v1.2
==============

Contents:

  1. Installation and usage
  2. Contributing to the project
  3. Licensing

Installation and usage
----------------------

PDFBinder can be installed on Microsoft Windows systems using a pretty
installer, which can be downloaded from the [project website][1].

In order to use PDFBinder on other platforms - or if the installer seems
like a bad choice for other reasons - PDFBinder can be built installed from
source. Grab the latest source package the [GitHub repository][2]. Use
whatever C# compiler you have available to build the project, or use the
provided project file for Microsoft Visual Studio 2010.

Hopefully, the user interface of PDFBinder is pretty self-explanatory. You
can add source PDF documents by using the "Add source documents..." button,
or by dragging the files in from a file browser. Documents can be moved up,
moved down or remoevd by pressing the respective buttons in the toolbar.
Press the "Save output..." button when your list of documents seems fine.

If PDFBinder was installed using the pre-built installer, an extra option
should have been automatically added to your context menu (the menu that on
Windows displays when right clicking a file) for all PDF files. Select any
number of PDFs in a file browser and choose the "Add to PDFBinder..." option
to bring up the PDFBinder interface with those files already in the list.

Contributing to the project
---------------------------

Any kind of contibutions to the project are very welcome. Issues should be
reported directly in the issue tracker on the [project website][1], and
reporters are very welcome to attach patches to their reports.

If you wish to encourage further development of PDFBinder by donating to the
project, please get in contact with the project owner (e-mail address can be
found on [website][1]), and we will find some way for you to transfer a
reasonable amount of money or beer to the project.

Licensing
---------

PDFBinder is released under the terms of the GNU General Public License.
Please see LICENSE.txt for the complete legal text.

All of the PDF magic is done using the iTextSharp 4.1.6 library, released
under both MPL and LGPL. Please refer to the [iTextSharp project site][3].

The small icons on the buttons in the user interface are published under the
Creative Commons Attribution 3.0 License by [Mark James][4].

[1]: http://code.google.com/p/pdfbinder/
[2]: https://github.com/schourode/pdfbinder
[3]: http://itextsharp.sourceforge.net/
[4]: http://www.famfamfam.com/