![License](https://img.shields.io/github/license/joaoborks/myunitytools)
![Release](https://img.shields.io/github/v/release/joaoborks/myunitytools?sort=semver)
![Last Commit](https://img.shields.io/github/last-commit/joaoborks/myunitytools)

My Unity Tools
===

_A personal collection of Unity Engine tools, extensions, and helpers._

Installation
---

#### - For 2019.1+: [Installing from a git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html) _(requires [Git](https://git-scm.com/) installed and added to the PATH)_
You can open the Package Manager and then click on the `+` button on the top left corner. 
From there select `Add package from git URL...`, type `https://github.com/joaoborks/myunitytools.git` and click `Add`. 
The package will be imported by the Package Manager.

#### - Other Package Manager supported versions: Add manually to manifest
You should add this to your `manifest.json` under the `Packages` folder on the root of your Unity Project:
```
{
  "dependencies": {
	  "com.joaoborks.fpscounter": "https://github.com/joaoborks/myunitytools.git"
  }
}
```

Usage
---

This package is focused in 3 main areas: Extensions, Simple Tools and Attributes. The purpose of these tools, are to improve the overall Unity Editor experience
with simple, reliable and maintainable solutions.

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
element names will fallback to the default naming

```csharp
public class MyClass : MonoBehaviour
{
    [SerializeField, LabeledArray(new string[]{ "First", "Second", "Third" })]
    float[] myValueArray;
}
```

#### :large_orange_diamond: SceneField

The `[SceneField]` attribute allows you to drag scene assets to fields in the inspector. You can only use this attribute on **int** fields, 
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

### :large_blue_diamond: Tools

We have only **one** tool available:

#### :large_orange_diamond: Group Game Objects

Simply press <kbd>Ctrl</kbd> + <kbd>G</kbd> to group the selected game objects. It creates a parent game object in the center of the selected objects.

### :large_blue_diamond: Extensions

We have a total of **2** extensions available:

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

##### :diamond_shape_with_a_dot_inside: Destroy Objects

Destroys any given array of `UnityEngine.Object` inherited objects, like `MonoBehaviour`, `Component` or `GameObject` types. Pretty cool, right?
You can also use its siblings: `DestroyAllComponentsOfType` and `DestroyAllChildrenComponentsOfType`

```csharp
public class MyClass : MonoBehaviour
{
    [SerializeField]
    GameObject[] myObjectsToDestroy;

    void Start()
    {
        GameObjectExtensions.DestroyObjects(myObjectsToDestroy);
        
        gameObject.DestroyAllComponentsOfType<BoxCollider>();
        gameObject.DestroyAllChildrenComponentsOfType<SphereCollider>();
    }
}
```

##### :diamond_shape_with_a_dot_inside: Delay Call

Delays an Action after a given amount of time. You can delay in seconds, frames or physics (fixed) frames.

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

---

Don't hesitate to create [issues](https://github.com/joaoborks/myunitytools/issues) for suggestions and bugs. Have fun!
