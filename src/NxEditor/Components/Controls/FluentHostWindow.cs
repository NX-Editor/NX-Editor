﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Dock.Avalonia.Controls;
using Dock.Model;
using Dock.Model.Core;
using FluentAvalonia.UI.Windowing;

namespace NxEditor.Components.Controls;

internal class FluentHostWindow : AppWindow, IHostWindow
{
    private readonly Window _parent;

    public IDockManager? DockManager { get; }
    public IHostWindowState? HostWindowState { get; }
    public bool IsTracked { get; set; }
    public IDockWindow? Window { get; set; }

    public FluentHostWindow(Window parent)
    {
        _parent = parent;
        DockManager = new DockManager();

        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        TitleBar.SetDragRectangles([
            new Rect(0, 0, double.PositiveInfinity, 35)
        ]);

        const string AVARES_ICON_PATH = "avares://NxEditor/Assets/Icon.ico";
        Bitmap bitmap = new(AssetLoader.Open(new Uri(AVARES_ICON_PATH)));
        Icon = bitmap.CreateScaledBitmap(new(48, 48), BitmapInterpolationMode.HighQuality);

        this.Bind(TitleProperty, new Binding("ActiveDockable.ActiveDockable.Title"));
        Content = new DockControl {
            Margin = new(5, 15, 5, 5),
            [!DockControl.LayoutProperty] = this.GetObservable(DataContextProperty).ToBinding()
        };
    }

    public void Exit()
    {
        if (Window is not null && Window.OnClose() == false) {
            return;
        }

        Close();
    }

    public void Present(bool isDialog)
    {
        if (IsVisible) {
            return;
        }

        Window?.Factory?.OnWindowOpened(Window);

        if (isDialog) {
            ShowDialog(_parent);
            return;
        }

        Show(_parent);
    }

    public void SetLayout(IDock layout)
    {
        DataContext = layout;
    }

    public void SetPosition(double x, double y)
    {
        Position = new((int)x, (int)y);
    }

    public void GetPosition(out double x, out double y)
    {
        x = Position.X;
        y = Position.Y;
    }

    public void SetSize(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public void GetSize(out double width, out double height)
    {
        width = Width;
        height = Height;
    }

    public void SetTitle(string title)
    {
        Title = title;
    }
}
