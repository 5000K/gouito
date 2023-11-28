Gouito is written in c#, thus not a real godot addon.

For simplicity and the sake of a single, expected position in the project, it should still be put here.
The msbuild will automatically pick it up.

## Why do it this way instead of referencing a binary?
gouito contains a lot of helper classes that can be directly dropped onto nodes.
This can happen from within the addons folder.

You may use a binary, but you will loose this functionality this way.