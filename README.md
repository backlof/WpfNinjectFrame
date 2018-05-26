## WpfNinjectFrame

This is a custom controller for navigating between pages, with Ninject resolving viewmodel dependencies in the background.

### Table of Contents
- [WpfNinjectFrame](#wpfninjectframe)
  - [Table of Contents](#table-of-contents)
  - [Important notes](#important-notes)
  - [How it works](#how-it-works)

### Important notes

- The library references `Ninject`
- The interface of the library is based on `Ninject` and `System.Windows.Components.Frame`
- The library only supports pages, not views
- The library gives access to viewmodels altering the dependency kernel in real time

### How it works

Firstly, you implement the frame in your window

```xml
<Window x:Class="TestProject.Window"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TestProject"
        xmlns:nf="clr-namespace:NinjectFrame;assembly=NinjectFrame"
        mc:Ignorable="d"
        Loaded="Window_Loaded"
        Title="{Binding ElementName=Frame, Path=Content.Title}">
	<Grid>
		<nf:KernelFrame Name="Frame"></nf:KernelFrame>
	</Grid>
</Window>
```

`KernelFrame` is a custom control based on `System.Windows.Components.Frame`, but with dependency resolving during navigation

```cs
public class KernelFrame : Frame, IKernelFrameNavigationHandler, IBindingRoot
{
  // Implementation
}
```

You can navigate to the first page from your window code-behind, using the frame instance

```cs
public partial class Window : System.Windows.Window
{
  public Window()
  {
    InitializeComponent();
  }

  private void Window_Loaded(object sender, RoutedEventArgs e)
  {
    #region Dependencies

    Frame.Bind<IProofGenerator>().To<ProofGenerator>();

    #endregion Dependencies

    Frame.NavigateTo<ExamplePageWithoutParameters>();
  }
}
```

The frame implements `IKernelFrameNavigationHandler` so that it can be dependency injected into any page that needs to navigate

```cs
public interface IKernelFrameNavigationHandler
{
  void NavigateTo<TPage>(string name, object @param) where TPage : Page, new();
  void NavigateTo<TPage>() where TPage : Page, new();
  void NavigateTo<TPage>(object @params) where TPage : Page, new();
  bool CanGoForward { get; }
  bool CanGoBack { get; }
  void GoBack();
  void GoForward();
  void RemoveBackEntry();
}
```

You only have to specify the type of viewmodel your page expects, and make sure it has a parameterless constructor

```cs
[DataContextViewModel(typeof(DataViewModel))]
public partial class DataPage : Page
{
  public DataPage()
  {
    InitializeComponent();

    // The navigation handler automatically sets the data context for you, so you don't have to
  }
}
```

If you have declared all the necessary dependencies, you can navigate to any page

```cs
public class NavigationPage
{
  public NavigationPage(Data data, IKernelFrameNavigationHandler navigationHandler)
  {
    navigationHandler.NavigateTo<DataPage>();
    navigationHandler.NavigateTo<DataPage>();
    navigationHandler.NavigateTo<DataPage>(data);
    navigationHandler.NavigateTo<DataPage>(null);
    navigationHandler.NavigateTo<DataPage>(name: "data", param: data);
    navigationHandler.NavigateTo<DataPage>(name: "data", param: null);
    navigationHandler.NavigateTo<DataPage>("data", data);
  }
}
```

You can specify the parameter name in an attribute (will default to `"params"` if never specified)

```cs
[ViewModelParameter("data", typeof(Data))]
public class DataViewModel
{
  public DataViewModel(Data data)
  {
  }
}
```

Or in the navigation

```cs
_navigationHandler.NavigateTo<DataPage>(name: "data", param: data);
_navigationHandler.NavigateTo<ExamplePageWithParameters>("data", data);
_navigationHandler.NavigateTo<ExamplePageWithParameters>("data", null);
```

Keep in mind, that the navigation handler can fail if

- The parameter name isn't correct
- The value sent into the method is not the same type specified in the attribute