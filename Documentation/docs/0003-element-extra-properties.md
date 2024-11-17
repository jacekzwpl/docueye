## Element extra properties

Bellow You can see list of additional docueye properties that can be add to element.

| Property name | Description |
| --- | ------|
| `docueye.ownerteam` | The name of owner team for model element. If defined the value can be viewed in elements overview screen |
| `docueye.sourcecodeurl` | The url link for source code of model element. If defined source code button is enabled and You can navigate to source code directly from documentation |

Example:  
```
mysystem = softwaresystem "My System" "My Great System Description" {
    properties {
        "docueye.ownerteam" "My System Development Team"
        "docueye.sourcecodeurl" "https://docueue.com"
    }
}
```