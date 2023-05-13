using Avalonia.Controls;
using AvaloniaEdit.TextMate;
using ExKingEditor.ViewModels.Editors;
using TextMateSharp.Grammars;

namespace ExKingEditor.Views.Editors;
public partial class RestblView : UserControl
{
    private static readonly RegistryOptions _registryOptions = new(ThemeName.DarkPlus);

    public RestblView()
    {
        InitializeComponent();

        // Initialize TextEditor
        TextMate.Installation textMateInstallation = TextEditor.InstallTextMate(_registryOptions);
        textMateInstallation.SetGrammar("source.yaml");
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        if (DataContext is RestblViewModel vm) {
            vm.Editor = TextEditor;
            TextEditor.Text = vm.Yaml;
            TextEditor.TextChanged += (s, e) => {
                vm.Yaml = TextEditor.Text;
            };
        }
    }
}