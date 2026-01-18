## Architecture Linter

TODO: Describe what is architecture linter

Features:
- JSON based configuration
- C# Expression support
- Support for defining variables
- Extending/Overriding Rules support

### Linter configuration
You can define configuration for linter rules via json files. 
| Field | Required | Type | Description |
| --- | -- | ---- | ----- |
| `MaxAllowedSeverity` | No | string | Max allowed issue severity that will result linter validation success. Allowed values are `Information`, `Warning`, `Error`. Default value is `Warning`.  |
| `Extends` | No | string | | Url or path to configuration file that will be extended. |
| `Variables` | No | object | Variables dictionary that can be used in rules expressions. |
| `Rules` | Yes | array of Rules | List of rules definitions. | 

### Linter Rules Model

| Field | Required | Type | Description |
| --- | -- | ---- | ----- |
| `ID` | Yes | string | Unique identifier of rule. |
| `Name` | Yes if adding new rule | string | The name of the rule. |
| `Description` | No | string | Description for the rule. |
| `Severity` | Yes if adding new rule | string | The rule severity. Allowed values are `Information`, `Warning`, `Error`.  |
| `Scope` | Yes if adding new rule | string | The rule scope. Allowed values are `ModelRelationship`, `ModelElement`, `General`. |
| `Expression` | Yes if adding new rule | string | The rule expression that will be used to find issues. Expression should return `true` if issue was found. |
| `HelpLink` | No | string | Link that points to more information/help about the rule. |
| `Enabled` | No | boolean | Determines if the rule will be executed. Default value is `true`. |

### Rule scope
The rule scope determines in witch context the rule expression will be evaluated.  
`ModelRelationship` - The rule is evaluated against relationships in the architecture model. You can use [Relationship Properties]() in expressions.  
`ModelElement` - The rule is evaluated against elements in the architecture model. You can use [Element Properties]() in expressions.  
`General` - The rule is evaluated against the overall architecture model. Only [predefined linter functions]() can be used in this scope.


### Element Properties
