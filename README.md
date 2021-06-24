# 2D Graphics Drawing via .NET

[Aspose.Drawing for .NET](https://products.aspose.com/drawing/net) is a .NET graphics API that provides the capability of 2D drawings identical to GDI+ in your .NET applications. The drawing engine supports rendering vector graphics (such as lines, curves, and figures) and text (in a variety of fonts, sizes, and styles) onto raster images in all commonly used graphics file formats. The project is based on managed .NET core and does not have dependencies on native code and libraries, with the rendering algorithms working the same way on all supported platforms.

<p align="center">

  <a title="Download complete Aspose.Drawing for .NET source code" href="https://github.com/aspose-drawing/Aspose.Drawing-for-.NET/archive/master.zip">
	<img src="http://i.imgur.com/hwNhrGZ.png" />
  </a>
</p>

Directory | Description
--------- | -----------
[Examples](Examples)  | A collection of .NET examples that help you learn and explore the API features.

## 2D Drawing API Features

- Create bitmaps from scratch or load existing files for editing.
- Draw lines, curves, splines and arcs.
- Process and draw graphics paths.
- Render text with different fonts and styles.
- Use different pen widths and styles.
- Supports Alpha blending and anti-aliasing lines and shapes.
- Use affine transformations.
- Work with clip regions

## Read & Write Images

**Raster:** TIFF, BMP, PNG, JPEG, GIF

## Get Started with Aspose.Drawing for .NET

Are you ready to give Aspose.Drawing for .NET a try? Simply execute `Install-Package Aspose.Drawing` from Package Manager Console in Visual Studio to fetch the NuGet package. If you already have Aspose.Drawing for .NET and want to upgrade the version, please execute `Update-Package Aspose.Drawing` to get the latest version.

## Apply Matrix Transformation on an Image

```csharp
Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
Graphics graphics = Graphics.FromImage(bitmap);
graphics.Clear(Color.FromKnownColor(KnownColor.Gray));

Rectangle originalRentangle = new Rectangle(300, 300, 300, 200);
TransformPath(graphics, originalRentangle, (matrix) => matrix.Rotate(15.0f));
TransformPath(graphics, originalRentangle, (matrix) => matrix.Translate(-250, -250));
TransformPath(graphics, originalRentangle, (matrix) => matrix.Scale(0.3f, 0.3f));

bitmap.Save("output.png");
```

[Home](https://www.aspose.com/) | [Product Page](https://products.aspose.com/drawing/net) | [Docs](https://docs.aspose.com/drawing/net/) | [API Reference](https://apireference.aspose.com/drawing/net) | [Examples](https://github.com/aspose-drawing/Aspose.Drawing-for-.NET/tree/master/Examples) | [Blog](https://blog.aspose.com/category/drawing/) | [Search](https://search.aspose.com/) | [Free Support](https://forum.aspose.com/c/drawing) | [Temporary License](https://purchase.aspose.com/temporary-license)
