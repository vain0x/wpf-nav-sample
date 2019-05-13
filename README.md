# WPF Navigation Example

Example of DataTemplate-based page navigation for WPF.

## Key items

- Define `DataTemplate`s to define **map from view models to views** (control) in resource dictionary
- Show views by setting view models to content of `ContentControl` (referred to as *frame*)
- Changing content of the frame is navigation

NOTE: You don't need `System.Windows.Controls.Frame`.

## Mechanism

## Mechanism: DataTemplate and ContentControl

Let `Foo` be a class of data context for a view `FooControl`.

If you define a `DataTemplate` for `Foo` like this, the template is automatically applied whenever WPF would display the `Foo` instance.

For example, you have the following in a resource dictionary:

```xml
    <DataTemplate DataType="{x:Type foo:Foo}">
        <foo:FooControl />
    </DataTemplate>
```

and put a control on window to display an instance of `Foo`:

```xml
    <ContentControl Control="{Binding Foo}" />
```

then you will see `FooControl` with DataContext bound to the `Foo` property.

See `AppResourceDictionary.xaml` for real implementation.

## CommandParameter and RelativeSource

Pages in frame (a control to host pages) must dispatch some message to the parent frame to go to another page. The cyclic reference, "the frame know pages and the pages also know the frame", is bad for maintainability as *separation of concerns* principle.

You can de-couple the frame and pages using `RelativeSource`. See the following example:

```xml
<UserControl>
    <!-- Inside FooControl (page) -->
    <Button Content="Go some page"
        Command="{Binding
            DataContext.NavigateCommand,
            RelativeSource={RelativeSource
                AncestorType={x:Type layoutFrames:LayoutFrameControl}
            }" />
</UserControl>
```

Assume that the user control is a page of `LayoutFarmeControl` (with DataContext `LayoutFrame`). The `RelativeSource` binding finds a lowest ancestor of the type specified with `AncestorType`, i.e. `LayoutFrameControl`, and use it as binding source. Hence The value of the binding will be `LayoutFrameControol.DataContext.NavigateCommand` = `LayoutFrame.NavigateCommand`. That's how you can use `NavigateCommand` inside pages, without using reference to the frame.

## Naming

### Naming: Suffix of views

I'm not a big fun of typical `xxxViewModel` suffix for view models. In this repository the view models have no suffixes, but **views have -Control suffix** (except for windows). E.g. `LayoutFrame` is a view model for corresponding view `LayoutFrameControl`.

### Naming: Requests

**Requests** are objects intended to be a parameter of commands. I know the word is NOT the best choice but at least makes sence.

## Extensibility

This system can be exetended to other kind of navigation such as open/close drawer. It should be future work.
