## DocueEye CLI - Workspace commands

Commands for managing workspaces

### workspace import command

Imports workspace to DocuEye.

| Option | Required | Description | Example |
| --- | -- | ------- | ---- |
| `--docueye-address` | Yes | DocuEye address. | `--docueyeAddress=http://localhost:8080` |
| `--admin-token` | Yes | The Admin token from DocuEye configuration. | `--adminToken=docueyedmintoken` |
| `--mode` | No | Specifies import mode. [default: dsl] <br /> `dsl` for import from dsl file. <br /> `json` for import from json file.  | `--mode=dsl` |
| `--file` | Yes | Path to workspace file. Depending on mode option should be path to dsl file or json file. | `--file=workspace.dsl` |
| `--id` | No | The ID of the Workspace. If not provided the new workspace will be created. Also if workspace with given id does not exists new workspace will be created. **It is recommended to allays pass workspace ID.** | `--id=638d0822-12c7-4998-8647-9c7af7ad2989` |
| `--key` | No | Unique for workspace import key. If not provided, one will be generated. <br /> Import Key connects changes and prevents from running multiple imports for workspace at the same time. It can be for example commit hash that points to source version from witch workspace is imported. | `--key=09bb3efb-6de6-486d-9e90-b7aab8e36b7f` | 
| `--source-link` | No | Link to source version from which workspace is imported ex. link to PR or commit on github. | `--source-link=https://localhost:8443` |

**Example import workspace from dsl file**
```Powershell
docueye workspace import --docueye-address=http://localhost:8080 --admin-token=docueyedmintoken --id=638d0822-12c7-4998-8647-9c7af7ad2989 --file="/path/to/workspace.dsl"
```
**Example import workspace from json file**
```Powershell
docueye workspace import --mode=json --docueye-address=http://localhost:8080 --admin-token=docueyedmintoken --id=638d0822-12c7-4998-8647-9c7af7ad2989 --file="/path/to/workspace.json"
```

### workspace delete command

Deletes a workspace from DocuEye.

| Option | Required | Description | Example |
| --- | -- | ------- | ---- |
| `--docueye-address` | Yes | DocuEye address. | `--docueyeAddress=http://localhost:8080` |
| `--admin-token` | Yes | The Admin token from DocuEye configuration. | `--adminToken=docueyedmintoken` |
| `--id` | Yes | The ID of the Workspace to be deleted. | `--id=638d0822-12c7-4998-8647-9c7af7ad2989` |

**Example delete workspace from DocuEye**
```Powershell
docueye workspace delete --docueye-address=http://localhost:8080 --admin-token=docueyedmintoken --id=638d0822-12c7-4998-8647-9c7af7ad2989
```

### workspace export command

Exports a workspace to given format.

| Option | Required | Description | Example |
| --- | -- | ------- | ---- |
| `--file` | Yes | Path to workspace file. | `--file=workspace.dsl` |
| `--format` | No | Specifies export format. [default: json] <br /> `json` for export to json file.  | `--format=json` |

**Example export workspace to json**
```Powershell
docueye workspace export --file="/path/to/workspace.dsl"
```

### workspace validate command

Validates dsl workspace.

| Option | Required | Description | Example |
| --- | -- | ------- | ---- |
| `--file` | Yes | Path to workspace file. | `--file=workspace.dsl` |

**Example validate workspace**
```Powershell
docueye workspace validate --file="/path/to/workspace.dsl"
```