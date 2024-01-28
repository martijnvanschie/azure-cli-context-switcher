# azctx config

[Reference](../reference.md) \ Config

## Summary

Allows you you manage the azctx configuration by adding, listing and viewing configuration items.

## Commands

| Name   | Description      | Status | Available since
|----------|---------------|-------|-----|
| [azctx config init](#azctx-config-init) | Initialize a default configuration file | GA | 1.0
| [azctx config view](#azctx-config-view) | Show the details of the configuration | GA | 1.0
| azctx config tenants | Manage the configuration | Preview | 1.0
| azctx config users | Manage the configuration | Preview | 1.0

## azctx config init

Create a default configuration file in the users profile folder under the subfolder `.azctx`.

It resolved to one of the following folders:  
*  `[%UserProfile%]\.azctx`
*  `[%SystemDrive%]\Users\{username}\.azctx`

The name of the configuration file can be overridden by providing the optional parameter `--cliconfig`.

### Examples

```cli
azctx config init
```

### Required Parameters

This command has no required parameters

### Optional parameters

`--cliconfig | -c`

The name of the configuration file to use. The default name is `config`.

## azctx config view

Displays the content of the configuration file. It also displays the folder of the configuration file.

### Examples

```cli
azctx config view
```

### Required Parameters

This command has no required parameters

### Optional parameters

`--cliconfig | -c`

The name of the configuration file to use. The default name is `config`.
