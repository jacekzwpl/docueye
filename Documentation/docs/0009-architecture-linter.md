## Architecture Linter

Architecture linter verifies architectural models for compliance with established design rules and constraints. This enables early detection of design errors and increases architectural consistency across the entire team or organization.

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
| `Extends` | No | string | Url or path to configuration file that will be extended. |
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
| `Expression` | Yes if adding new rule | string | The rule expression that will be used to find issues. Expression should return `true` if issue was found. <br /> For more details on supported expression language refer - [expression language](https://dynamic-linq.net/expression-language) <br /> For supported linq operations refer - [sequence operators](https://dynamic-linq.net/expression-language#sequence-operators).|
| `HelpLink` | No | string | Link that points to more information/help about the rule. |
| `Enabled` | No | boolean | Determines if the rule will be executed. Default value is `true`. |

### Rule scope
The rule scope determines in witch context the rule expression will be evaluated.  
`ModelRelationship` - The rule is evaluated against relationships in the architecture model. You can use [Model Relationship Properties](#model-relationship-properties) in expressions.  
`ModelElement` - The rule is evaluated against elements in the architecture model. You can use [Model Element Properties](#model-element-properties) in expressions.  
`General` - The rule is evaluated against the overall architecture model. Only [general expression functions](#general-expression-functions) can be used in this scope.


### Model Element Properties

| Field | Type | Nullable | Description |
| --- | -- | -- | ----- |
| `Identifier` | `string` | No | Model element identifier. |
| `ParentIdentifier` | `string` | Yes | Identifier of the parent model element. |
| `Name` | `string` | No | Model element name. |
| `Tags` | `IEnumerable<string>` | No | Model element tags. |
| `Technology` | `string` | Yes | Technology associated with the model element. |
| `Description` | `string` | Yes | Model element description. |
| `Properties` | `IDictionary<string, string>` | No | Model element properties. |
| `InstanceOfIdentifier` | `string` | Yes | Identifier of the model element this instance is based on. |

### Model Relationship Properties

| Field | Type | Nullable | Description |
| --- | -- | -- | ----- |
| `Source` | [Model element](#model-element-properties) | No | Relationship source element. |
| `Destination` | [Model element](#model-element-properties) | No | Relationship destination element. |
| `Technology` | `string` | Yes | Technology associated with the relationship. |
| `Description` | `string` | Yes | Description of the relationship. |
| `Properties` | `IDictionary<string, string>` | No | Properties of the relationship. |
| `Tags` | `IEnumerable<string>` | No | Tags associated with the relationship. |

### General expression functions

Name: `GeneralIssuesFinder.CyclicDependenciesExists`  
Description: Finds cyclic dependencies between elements with same tags.  
Return value: `boolean` - true if cyclic dependencies were found.  
Parameters:  
`elementTag` - tag that is used to filter model elements.  
`relationships` - array of [Model relationships](#relationship-properties).  
Example: `GeneralIssuesFinder.CyclicDependenciesExists("Container",@ModelRelationships)`

### Using variables
Variables can be defined in `Variables` section in linter configuration. For example:  
```json
"Variables": {
    "AllowedContainerTechnologies": [
        "ASP.NET MVC", "REST API", "MSSQL", "REDIS"
    ],
    "TheOnlyOneRelationshipTechnology": "HTTPS"
}
```
Then You can use this variables in rules expressions preceding the name with the `@`.  
For example:  
```
Source.Technology not_in @AllowedContainerTechnologies AND Technology != @TheOnlyOneRelationshipTechnology
```

### Predefined variables

| Name | Type | Description | 
| --- | --- | ----- |
| ModelRelationships | IEnumerable<[Model Relationship](#model-relationship-properties)> | Array of Model Relationships | 



### Extending configuration file example

BaseLinterRules.json
```json
{
    "MaxAllowedSeverity": "Warning",
    "Variables": {
        "AllowedContainerTechnologies": [
            "ASP.NET MVC", "REST API", "MSSQL", "REDIS"
        ]
    },
    "Rules": [
        {
            "Id": "MyRule1Id",
            "Name": "MyRule1 Name",
            "Description": "MyRule1 description",
            "Severity": "Error",
            "Type": "ModelElement",
            "Expression": "Tags.Contains(\"Container\") and Technology not_in @AllowedContainerTechnologies",
            "HelpLink": "https://example.com/myrule1"
        }
    ]
}
```
ChildLinterRules.json
```json
{
    "Extends":"BaseLinterRules.json",
    "Variables": {
        "AllowedContainerTechnologies": [
            "ASP.NET MVC", "REST API", "MSSQL"
        ],
        "NewVariable": "MyNewVariableValue"
    },
    "Rules": [
        {
            "Id": "MyRule1Id",
            "Name": "The name of rule changed",
            "Severity": "Warning"
        },
        {
            "Id": "MyRule2Id",
            "Name": "MyRule2 Name",
            "Description": "MyRule2 description",
            "Severity": "Error",
            "Type": "ModelElement",
            "Expression": "Tags.Contains(\"Container\") and Technology == @NewVariable",
            "HelpLink": "https://example.com/myrule2"
        }
    ]
}
```
Below is the result linter configuration
```json
{
    "MaxAllowedSeverity": "Warning",
    "Variables": {
        "AllowedContainerTechnologies": [
            "ASP.NET MVC", "REST API", "MSSQL"
        ],
        "NewVariable": "MyNewVariableValue"
    },
    "Rules": [
        {
            "Id": "MyRule1Id",
            "Name": "The name of rule changed",
            "Description": "MyRule1 description",
            "Severity": "Warning",
            "Type": "ModelElement",
            "Expression": "Tags.Contains(\"Container\") and Technology not_in @AllowedContainerTechnologies",
            "HelpLink": "https://example.com/myrule1"
        },
        {
            "Id": "MyRule2Id",
            "Name": "MyRule2 Name",
            "Description": "MyRule2 description",
            "Severity": "Error",
            "Type": "ModelElement",
            "Expression": "Tags.Contains(\"Container\") and Technology == @NewVariable",
            "HelpLink": "https://example.com/myrule2"
        }
    ]
}
```

