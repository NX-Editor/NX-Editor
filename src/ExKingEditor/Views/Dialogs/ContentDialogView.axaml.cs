using Avalonia.Controls;
using ExKingEditor.Dialogs;

namespace ExKingEditor.Views.Dialogs;

public partial class ContentDialogView : UserControl
{
    public ContentDialogView() { }
    public ContentDialogView(ContentDialog content)
    {
        InitializeComponent();
        DataContext = content;
    }

    public void Show()
    {
        App.Desktop?.DropClient.Children.Add(this);
    }

    public void Hide()
    {
        App.Desktop?.DropClient.Children.Remove(this);
    }
}
