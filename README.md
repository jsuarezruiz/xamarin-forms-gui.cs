## Xamarin.Forms gui.cs Backend

[gui.cs](https://github.com/migueldeicaza/gui.cs) is a simple UI toolkit for .NET, .NET Core and Mono and works on both Windows and Linux/Unix created by [Miguel de Icaza](https://github.com/migueldeicaza).

This project is a small Xamarin.Forms backend of gui.cs. Yes, create **console** Apps with C# and XAML!.

![](images/forms-gui-dialogs.gif)

## Status

It is a project in progress where there is currently implementation of:
* Alert
* Basic Layouts
* Button
* Label
* ProgressBar
* Switch

Remain pending:
* Editor
* HexView
* Frame
* ListView
* Menu
* ScrollView

![](images/forms-gui-live.gif)

## Example App

Here is the complete source code to a Login sample.
We start with a simple class where initialize gui.cs and Xamarin.Forms:

```
public class Program
{
    public static void Main()
    {
        Application.Init();
        Forms.Init();
        var app = new App();
        var window = new FormsWindow("Xamarin.Forms gui.cs Backend");
        window.LoadApplication(app);
        Application.Run();
    }
}

```
Where App is an Application of Xamarin.Forms:

```
public class App : Xamarin.Forms.Application
{
    public App()
    {
        MainPage = new MainPage();
    }
}

```

And MainPage is a just a Xamarin.Forms **XAML** ContentPage:

```
<StackLayout>
    <Label 
        Text="Login"
        Margin="0, 12"/>
    <Label 
        Text="Username" />
    <Entry />
    <Label 
        Text="Password" />
    <Entry 
        IsPassword="True" />
    <Switch />
    <Button
        Text="Login"/>
</StackLayout>

```

As in any Xamarin.Forms App, you can create the entire UI in **C#** code.

```
public MainPageCS()
{
    var panel = new StackLayout();

    var userNameLabel = new Xamarin.Forms.Label
    {
        Text = "Username:"
    };
    panel.Children.Add(userNameLabel);

    var userNameEntry = new Entry();
    panel.Children.Add(userNameEntry);


    var passwordLabel = new Xamarin.Forms.Label
    {
        Text = "Password:"
    };
    panel.Children.Add(passwordLabel);

    var passwordEntry = new Entry
    {
        IsPassword = true
    };
    panel.Children.Add(passwordEntry);

    var loginButton = new Xamarin.Forms.Button
    {
        Text = "Login"
    };
    panel.Children.Add(loginButton);

    Content = panel;
}

```
## Contributing

This project is open source and I love merging PRs. Try to file issues for things that you want to work on before you start the work so that there's no duplicated effort. If you just want to help out, check out the issues and dive in!.

## Copyright and license

Code released under the [MIT license](https://opensource.org/licenses/MIT).