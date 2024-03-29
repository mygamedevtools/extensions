<h1 align = center>
Extensions
</h1>

<p align=center>
  <a href="LICENSE"><img src="https://img.shields.io/github/license/mygamedevtools/extensions?color=5189bd" /></a>
  <a href="https://github.com/mygamedevtools/extensions/releases/latest"><img src="https://img.shields.io/github/v/release/mygamedevtools/extensions?color=5189bd&sort=semver" /></a>
  <a href="https://openupm.com/packages/com.mygamedevtools.extensions/"><img src="https://img.shields.io/npm/v/com.mygamedevtools.extensions?color=5189bd&label=openupm&registry_uri=https://package.openupm.com" /></a>
  <a href="https://openupm.com/packages/com.mygamedevtools.extensions/"><img src="https://img.shields.io/badge/dynamic/json?color=5189bd&label=downloads&query=%24.downloads&suffix=%2Fmonth&url=https%3A%2F%2Fpackage.openupm.com%2Fdownloads%2Fpoint%2Flast-month%2Fcom.mygamedevtools.extensions" /></a>
</p>

<p align=center>
  <a href="https://codecov.io/github/mygamedevtools/extensions"><img src="https://codecov.io/github/mygamedevtools/extensions/branch/main/graph/badge.svg?token=J4ISVSF390" /></a>
  <a href="https://github.com/mygamedevtools/extensions/actions/workflows/test.yml"><img src="https://github.com/mygamedevtools/extensions/actions/workflows/test.yml/badge.svg" /></a>
  <a href="https://github.com/mygamedevtools/extensions/actions/workflows/release.yml"><img src="https://github.com/mygamedevtools/extensions/actions/workflows/release.yml/badge.svg" /></a>
  <a href="https://github.com/semantic-release/semantic-release"><img src="https://img.shields.io/badge/semantic--release-angular-e10079?logo=semantic-release" /></a>
</p>

<p align=center><i>
A personal collection of Unity Engine tools, extensions, and helpers.
</i></p>

Installation
---

### OpenUPM

This package is available on the [OpenUPM](https://openupm.com/packages/com.mygamedevtools.extensions) registry. Add the package via the [openupm-cli](https://github.com/openupm/openupm-cli):

```
openupm add com.mygamedevtools.extensions
```

### [Installing from Git](https://docs.unity3d.com/Manual/upm-ui-giturl.html) _(requires [Git](https://git-scm.com/) installed and added to the PATH)_

1. Open `Edit/Project Settings/Package Manager`.
2. Click <kbd>+</kbd>.
3. Select `Add package from git URL...`.
4. Paste `com.mygamedevtools.extensions` into the name.
5. Click `Add`.

Usage
---

This package is focused on 3 main areas: Extensions, Tooling, and Attributes. The purpose of these tools, is to improve the overall Unity Editor experience
with simple, reliable, and maintainable solutions.

### :large_blue_diamond: Attributes

We have a total of **3** attributes available:

#### :large_orange_diamond: ReadOnly

The `[ReadOnly]` attribute prevents changing the field's value in the Unity Inspector

```csharp
public class MyClass : MonoBehaviour
{
    [SerializeField, ReadOnly]
    float myValue = 5.5f;
}
```

#### :large_orange_diamond: LabeledArray

The `[LabeledArray]` attribute allows you to specify names for array elements, instead of the default `Element #n`. You can
either use a string array or an Enum to provide the names. If the array element count passes the provided names count, the following
element names will fall to the default naming

```csharp
public class MyClass : MonoBehaviour
{
    [SerializeField, LabeledArray(new string[]{ "First", "Second", "Third" })]
    float[] myValueArray;
}
```

#### :large_orange_diamond: SceneField

The `[SceneField]` attribute allows you to drag scene assets to fields in the inspector. You can only use this attribute on **int** fields 
since it will store the scene's build index. It does not have any relation with the scene itself, so if you change the build settings it will
not update its value and will reference a different scene. You can add the scene to the build settings via the inspector after dragging the
scene to the field if it hasn't been yet

```csharp
public class MyClass : MonoBehaviour
{
    [SerializeField, SceneField]
    int mySceneIndex;
}
```

### :large_blue_diamond: Tooling

We have **two** tools available:

#### :large_orange_diamond: Group Game Objects

Simply press <kbd>Ctrl</kbd> + <kbd>G</kbd> to group the selected game objects. It creates a parent game object in the center of the selected objects.

#### :large_orange_diamond: Scripting Define Symbols Helper

You can use the `ScriptingDefineSymbolsHelper` to easily add or remove scripting define symbols to your project. For example:

```csharp
using MyGameDevTools.Extensions;
using UnityEditor;

public static class CheatsEnabler
{
    const string _cheatsDefine = "ENABLE_CHEATS";

    [MenuItem("Tools/Add Cheats")]
    public static void AddCheats()
    {
        ScriptingDefineSymbolsHelper.AddScriptingDefineSymbol(_cheatsDefine);
    }

    [MenuItem("Tools/Add Cheats", validate = true)]
    public static bool AddCheatsValidate()
    {
        return !ScriptingDefineSymbolsHelper.HasScriptingDefineSymbol(_cheatsDefine);
    }

    [MenuItem("Tools/Remove Cheats")]
    public static void RemoveCheats()
    {
        ScriptingDefineSymbolsHelper.RemoveScriptingDefineSymbol(_cheatsDefine);
    }

    [MenuItem("Tools/Remove Cheats", validate = true)]
    public static bool RemoveCheatsValidate()
    {
        return ScriptingDefineSymbolsHelper.HasScriptingDefineSymbol(_cheatsDefine);
    }
}
```

### :large_blue_diamond: Extensions

We have a total of **3** extensions available:

#### :large_orange_diamond: Debug Extensions

Allows debugging collections and jagged arrays.

```csharp
public class MyClass : MonoBehaviour
{
    // Can also be a List or any IEnumerable type
    [SerializeField]
    int[] myValueArray;
    
    int[][] myJaggedArray = new int[5][];
    
    void Start()
    {
        for (int i = 0; i < myJaggedArray.Length; i++)
            myJaggedArray[i] = new int[5];
            
        DebugExtensions.LogJaggedArray(myJaggedArray);        
        DebugExtensions.LogCollection(myValueArray);
    }
}
```

#### :large_orange_diamond: Game Object Extensions

A collection of some helpful game object operations

##### :diamond_shape_with_a_dot_inside: Set Children Layer Recursively

Sets all game objects in the given transform's hierarchy to the provided layer

```csharp
public class MyClass : MonoBehaviour
{
    void Start()
    {
        transform.SetChildrenLayerRecursively(LayerMask.NameToLayer("MyLayer"));
    }
}
```

##### :diamond_shape_with_a_dot_inside: Destroy Components

Destroys the given component type instances in a `UnityEngine.GameObject` or its children.

```csharp
public class MyClass : MonoBehaviour
{
    [SerializeField]
    GameObject[] myObjectsToDestroy;

    void Start()
    {        
        gameObject.DestroyAllComponentsOfType<BoxCollider>();
        gameObject.DestroyAllChildrenComponentsOfType<SphereCollider>();
    }
}
```

##### :diamond_shape_with_a_dot_inside: Get Root Game Objects

Gets the root game objects of a scene, or optionally all scenes.

```cs
public class MyClass : MonoBehaviour
{
    void Start()
    {
        List<GameObject> allRootObjects = GameObjectExtensions.GetRootGameObjects(true);
    }
}
```

##### :diamond_shape_with_a_dot_inside: Has Layer

Checks if a `LayerMask` contains the given layer index. Useful for physics validations.

```cs
public class MyClass : MonoBehaviour
{
    [SerializeField]
    LayerMask _mask;

    void Start()
    {
        Debug.Log(_mask.HasLayer(gameObject.layer));
    }
}
```

##### :diamond_shape_with_a_dot_inside: Delay Call

Delays an Action after a given amount of time. You can delay in seconds, frames, or physics (fixed) frames.

```csharp
public class MyClass : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Hello there");
        // In seconds
        DelayCall(6, MyAction);
        // In frames
        DelayCallInFrames(6, MyAction);
        // In physics frammes
        DelayCallInPhysicsFrames(6, MyAction);
    }
    
    void MyAction()
    {
        Debug.Log("General Kenobi")
    }
}
```

#### :large_orange_diamond: Object Extensions

##### :diamond_shape_with_a_dot_inside: Destroy Objects

Destroy a given array of `UnityEngine.Object`, which could be GameObjects, Components, MonoBehaviours, and any type that inherits from `UnityEngine.Object`. Keep in mind that the object is destroyed between the end of the current frame and the beginning of the next one.

```cs
public class MyClass : MonoBehaviour
{
    [SerializeField]
    Object[] _objectsToDestroy;

    void Start()
    {
        ObjectExtensions.DestroyObjects(_objectsToDestroy);
    }
}
```

##### :diamond_shape_with_a_dot_inside: Find Implementation(s) of Type

This is meant to find `interface` implementations. It works like a `FindObjectOfType`, but for `interface`. You can also use the `TryFindImplementationOfType` to return a bool. Keep in mind that the `interface` implementation will also need to inherit from `MonoBehaviour` to be found since it will be searched within GameObject's components.

```cs
public interface IActionable {}

public class MyAction : MonoBehaviour, IActionable {}

public class MyClass : MonoBehaviour
{
    void Start()
    {
        // Common usage
        IActionable implementation = this.FindImplementationOfType<IActionable>();
        // Explicit usage
        IActionable implementation = ObjectExtensions.FindImplementationOfType<IActionable>();
        // Try usage
        bool implementationExists = ObjectExtensions.TryFindImplementationOfType<IActionable>(out IActionable implementation);

        // For multiple implementations
        List<IActionable> implementations = ObjectExtensions.FindImplementationsOfType<IActionable>();
    }
}
```

---

Don't hesitate to create [issues](https://github.com/mygamedevtools/extensions/issues) for suggestions and bugs. Have fun!
