## DocueEye CLI - Workspace commands

Commands for working with documentation files for components.

### docfile import command

Imports or updates documentation file for given element.

| Option | Required | Description | Example |
| --- | -- | ------- | ---- |
| `--docueye-address` | Yes | DocuEye address. | `--docueyeAddress=http://localhost:8080` |
| `--admin-token` | Yes | The Admin token from DocuEye configuration. | `--admin-token=docueyedmintoken` |
| `--file` | Yes | Path to openapi specification file for element. | `--file=openapi.yml` |
| `--id` | Yes | The ID of the Workspace. | `--id=638d0822-12c7-4998-8647-9c7af7ad2989` |
| `--element-id` | No | The ID of element for which this import is created. Required only if --element-dsl-id option is not set. | `--element-id=e7e7d436-ac1e-4e73-b36b-d34dc32c9bbc` | 
| `--element-dsl-id` | No | The DSL ID of element for which this import is created. Required only if --element-id option is not set. | `--element-dsl-id=docueye.app` | 
| `--type` | Yes | Specifies documentation file type. Allowed values are 'openapi' and 'asyncapi'. | `--type=openapi` | 

**Example import openapi specification for element from dsl file**
```Powershell
docueye docfile import --docueye-address=http://localhost:8080 --admin-token=docueyedmintoken --id=638d0822-12c7-4998-8647-9c7af7ad2989 --file="/path/to/openapi.yml" --element-dsl-id=docueye.app --type=openapi
```

### docfile delete command

Deletes documentation file for element.

| Option | Required | Description | Example |
| --- | -- | ------- | ---- |
| `--docueye-address` | Yes | DocuEye address. | `--docueyeAddress=http://localhost:8080` |
| `--admin-token` | Yes | The Admin token from DocuEye configuration. | `--admin-token=docueyedmintoken` |
| `--id` | Yes | The ID of the Workspace. | `--id=638d0822-12c7-4998-8647-9c7af7ad2989` |
| `--element-id` | No | The ID of element for which this import is created. Required only if --element-dsl-id option is not set. | `--element-id=e7e7d436-ac1e-4e73-b36b-d34dc32c9bbc` | 
| `--element-dsl-id` | No | The DSL ID of element for which this import is created. Required only if --element-id option is not set. | `--element-dsl-id=docueye.app` | 
| `--type` | Yes | Specifies documentation file type. Allowed values are 'openapi' and 'asyncapi'. | `--type=openapi` | 

**Example delete openapi specification for element from dsl file**
```Powershell
docueye docfile delete --docueye-address=http://localhost:8080 --admin-token=docueyedmintoken --id=638d0822-12c7-4998-8647-9c7af7ad2989 --element-dsl-id=docueye.app --type=openapi
```
