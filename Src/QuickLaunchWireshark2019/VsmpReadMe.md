[AppUrl]: https://www.wireshark.org/
[ExternalToolsUrl]: https://docs.microsoft.com/en-gb/visualstudio/ide/managing-external-tools?view=vs-2017

A simple extension that adds a new toolbar containing a button to open [Wireshark][AppUrl], along with a corresponding Tools menu option.

![Tool bar](Toolbar.png)

![Tools menu](ToolsMenu.png)

### Use Cases

When you only rarely need to launch [Wireshark][AppUrl] you may wish to disable the toolbar, to avoid cluttering up the IDE.

When you need to launch [Wireshark][AppUrl] on an ad-hoc basis you may wish to use the Tools menu option, rather than enable and subsequently disable the toolbar.

When you need to repeatedly launch [Wireshark][AppUrl] to solve a problem you may wish to enable the toolbar, allowing instant access to launch [Wireshark][AppUrl] , and once the problem is solved disable the toolbar, to avoid cluttering up the IDE.

Or, of course, you may wish to keep the toolbar permanently enabled.


### External Tools

This extension is similar to Visual Studio's [External Tools][ExternalToolsUrl] functionality. [External Tools][ExternalToolsUrl] is more feature-rich (e.g allows specification of arguments) but does not associate an icon and creates a tools menu option only, not a toolbar as well.