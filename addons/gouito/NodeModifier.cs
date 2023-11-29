// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.Linq;
using Godot;

namespace gouito;

public static class NodeModifier
{
    public delegate void ModifierDelegate<T>(T node);
    public delegate void ModifierDelegateIndexed<T>(T node, int i);
    public delegate T NodeCreator<T>() where T: Node;

    public static void ModifyChildren<T>(this Node node, ModifierDelegate<T> modifierDelegate)
    {
        node.ModifyChildren<T>((n, _) => modifierDelegate(n));
    }

    public static void ModifyChildren<T>(this Node node, ModifierDelegateIndexed<T> modifierDelegate)
    {
        var children = node.GetChildren();

        var i = 0;
        foreach (var child in children)
        {
            if (child is T t)
            {
                modifierDelegate(t, i);
            }

            i++;
        }
    }

    public static void Fill<T>(this Node node, int targetCount) where T : Node, new()
    {
        node.Fill(() => new T(), targetCount);
    }
	
    public static void Fill<T>(this Node node, PackedScene packedScene,int targetCount) where T : Node
    {
        node.Fill(() => packedScene.Instantiate<T>(), targetCount);
    }
	
    public static void Fill<T>(this Node node, NodeCreator<T> creator,int targetCount) where T : Node
    {
        // remove all non fitting
        foreach (var toRemove in node.GetChildren().Where(c => c is not null and not T))
        {
            node.RemoveChild(toRemove);
            toRemove.QueueFree();
        }
		
        var i = node.GetChildren().Count;
		
        while (i < targetCount)
        {
            var resourceDialog = creator();
            node.AddChild(resourceDialog);
            i++;
        }
		
        while (i > targetCount)
        {
            node.RemoveChild(node.GetChildren()[0]);
            i--;
        }
    }
}