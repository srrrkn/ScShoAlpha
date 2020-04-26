using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Xml.Serialization;
using System.ComponentModel;

[Serializable()]
public class ClsFigure : ICloneable
{
    private string _figCat = "";
    private float _lineWidth = 1;

    [XmlIgnore]
    public Pen linePen;
    [XmlIgnore]
    public Brush fillBrush;

    [XmlIgnore]
    public Color lineColor = Color.Transparent;
    [XmlIgnore]
    public Color fillColor = Color.Transparent;
    //テキストフォント
    [XmlIgnore]
    public Font textFont = new Font("Comic Sans MS",
            15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point,
            ((System.Byte)(0)));
    // テキストカラー
    [XmlIgnore]
    public Color textColor = Color.Transparent;

    public string figCat
    {
        get
        {
            return _figCat;
        }
        set
        {
            _figCat = value;
        }
    }

    public float lineWidth
    {
        get
        {
            return _lineWidth;
        }
        set
        {
            _lineWidth = value;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public string TextFontAsString
    {
        get { return ConvertToString(textFont); }
        set { textFont = ConvertFromString<Font>(value); }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public string TextColorAsString
    {
        get { return ConvertToString(textColor); }
        set { textColor = ConvertFromString<Color>(value); }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public string LineColorAsString
    {
        get { return ConvertToString(lineColor); }
        set { lineColor = ConvertFromString<Color>(value); }
    }


    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public string BrushColorAsString
    {
        get { return ConvertToString(fillColor); }
        set { fillColor = ConvertFromString<Color>(value); }
    }


    public static string ConvertToString<T>(T value) {
        return TypeDescriptor.GetConverter(typeof(T)).ConvertToString(value);
    }
    public static T ConvertFromString<T>(string value) {
        return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
    }

    /// <summary>
    /// 空のコンストラクタ：シリアライズ用
    /// </summary>
    public ClsFigure() {
        linePen = new Pen(lineColor, lineWidth);
        fillBrush = new SolidBrush(fillColor);
    }

    public void setText(Color _color, Font _font)
    {
        textColor = _color;
        textFont = _font;
    }

    public void setPen(Pen _pen)
    {
        linePen = (Pen)_pen.Clone();
        lineColor = _pen.Color;
        lineWidth = _pen.Width;
    }

    public void setBrush(Brush _brush) {
        fillBrush = (Brush)_brush.Clone();
        SolidBrush brush = (SolidBrush)_brush;
        fillColor = brush.Color;
    }

    public ClsFigure castClassFigure() {
        ClsFigure _copy = new ClsFigure();
        _copy._figCat = _figCat;
        _copy._lineWidth = _lineWidth;
        _copy.linePen = (Pen)linePen.Clone();
        _copy.fillBrush = (Brush)fillBrush.Clone();
        _copy.lineColor = lineColor;
        _copy.fillColor = fillColor;
        _copy.textFont = textFont;
        _copy.textColor = textColor;
        return _copy;
    }

    // ICloneable.Cloneの実装
    public object Clone()
    {
        return MemberwiseClone();
    }

    public virtual void Draw(Graphics g)
    {
    }
}
