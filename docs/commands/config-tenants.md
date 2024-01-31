# azctx config tenants

[Reference](../reference.md) \ [Config](config.md) \ Tenants

## Summary

Allows you you manage tenant entries in the azctx configuration by adding, listing and viewing tenant items.

## Commands

| Name   | Description      | Status | Available since |
|----------|---------------|-------|-----|
| azctx config tenants add | Add a tenant entry to the configuration file | Experimental | 1.0 |
| [azctx config tenants add-prompt](#azctx-config-tenants-add-prompt) | Add a tenant entry to the configuration file using an interactive prompt | Preview | 1.0.2 |
| azctx config tenants list | List all the tenant in the configuration | GA | 1.0 |

## azctx config tenants add-prompt

Adds a new tenant entry to the configuration using an interactive prompt. Users is asked for the configuration parameters.

The name of the configuration file can be overridden by providing the optional parameter `--cliconfig`.

### Examples

Add a new tenant to the default configuration.

```cli
azctx config tenants add-prompt
```

Add a new tenant to the specified configuration.

```cli
azctx config tenants add-prompt --cliconfig demo
```

### Required Parameters

This command has no required parameters

### Optional parameters

`--cliconfig | -c`

The name of the configuration file to use. The default name is `config`.
