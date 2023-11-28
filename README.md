# gouito
> Godot UI Toolkit - MVVM for godot  
> [Get the latest release here](https://github.com/5000K/gouito/releases/latest)

gouito is a c# godot library targeted at establishing basic structures for UIs.

It is targeted at providing a base for  MVVM-UIs (Model - View - ViewModel). It offers a framework for DataBindings, including Converters and Validators.  
The main workflow revolves around defining your UI in-editor (as full xaml-parsing would overcomplicate things), then setting up BindingControllers that in turn set up the needed bindings.

This makes gouito not "pure" MVVM, but MVVM that is shlightly shifted towards MVC for the sake of simplicity and keeping the great WYSIWYG UI-editing experience of godot.

gouito is written in a modular way, letting you use only the parts you need, and will easily be usable within a xaml-based UI-definition library, given it's equally as open and modular.

The author thinks about offering a more code-oriented way of defining your UI, but for now recommends using the gouito way, as there are no good xaml-to-godot-ui-libraries to their knowledge.
