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
`ModelRelationship` - The rule is evaluated against relationships in the architecture model. You can use [Relationship Properties](#relationship-properties) in expressions.  
`ModelElement` - The rule is evaluated against elements in the architecture model. You can use [Element Properties](#element-properties) in expressions.  
`General` - The rule is evaluated against the overall architecture model. Only [general expression functions](#general-expression-functions) can be used in this scope.


### Element Properties

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

### Relationship Properties

| Field | Type | Nullable | Description |
| --- | -- | -- | ----- |
| `Source` | [Model element](#element-properties) | No | Relationship source element. |
| `Destination` | [Model element](#element-properties) | No | Relationship destination element. |
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
            "Id": "DE-ARCH-001",
            "Name": "Containers should not have cyclic dependencies",
            "Description": "Containers must not depend on each other in a cyclic manner to avoid tight coupling and maintainability issues.",
            "Severity": "Error",
            "Type": "General",
            "Expression": "GeneralIssuesFinder.CyclicDependenciesExists(\"Container\",@ModelRelationships)",
            "HelpLink": "https://docueye.com/architecture-linter/rules/DE-ARCH-001"
        },
        {
            "Id": "DE-ARCH-002",
            "Name": "Frontend can not connect directly to database",
            "Description": "Frontend services should not connect directly to the database for security and architecture reasons.",
            "Severity": "Error",
            "Type": "ModelRelationship",
            "Expression": "Source.Tags.Contains(\"Frontend\") and Destination.Tags.Contains(\"Database\")",
            "HelpLink": "https://docueye.com/architecture-linter/rules/DE-ARCH-002"
        },
        {
            "Id": "DE-ARCH-003",
            "Name": "Container technology must be defined",
            "Description": "Each container must have a defined technology to ensure clarity in the architecture.",
            "Severity": "Warning",
            "Type": "ModelElement",
            "Expression": "Tags.Contains(\"Container\") and Technology == null",
            "HelpLink": "https://docueye.com/architecture-linter/rules/DE-ARCH-003"
        },
        {
            "Id": "DE-ARCH-004",
            "Name": "Container technology must be one of the allowed technologies",
            "Description": "Containers should use only approved technologies to maintain consistency and supportability.",
            "Severity": "Warning",
            "Type": "ModelElement",
            "Expression": "Tags.Contains(\"Container\") and Technology not_in @AllowedContainerTechnologies",
            "HelpLink": "https://docueye.com/architecture-linter/rules/DE-ARCH-004"
        },
        {
            "Id": "DE-ARCH-005",
            "Name": "Relationships from external systems to database are not allowed",
            "Description": "External systems should not have direct relationships to databases to ensure security and proper architecture.",
            "Severity": "Error",
            "Type": "ModelRelationship",
            "Expression": "Source.ParentIdentifier != Destination.ParentIdentifier and Destination.Tags.Contains(\"Database\")",
            "HelpLink": "https://docueye.com/architecture-linter/rules/DE-ARCH-005"
        },
        {
            "Id": "DE-ARCH-006",
            "Name": "Relationship technology must be defined",
            "Description": "Each relationship must have a defined technology to ensure clarity in the architecture.",
            "Severity": "Warning",
            "Type": "ModelRelationship",
            "Expression": "Technology == null",  
            "HelpLink": "https://docueye.com/architecture-linter/rules/DE-ARCH-006"
        }
    ]
}
```
