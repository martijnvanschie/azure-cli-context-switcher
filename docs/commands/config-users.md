# azctx config users

[Reference](../reference.md) \ [Config](config.md) \ Users

## Summary

Allows you you manage user entries in the azctx configuration by adding, listing and viewing user items.

## Commands

| Name   | Description      | Status | Available since |
|----------|---------------|-------|-----|
| azctx config users add | Add a user entry to the configuration file | Experimental | 1.0 |
| [azctx config users add-prompt](#azctx-config-users-add-prompt) | Add a user entry to the configuration file using an interactive prompt | Preview | 1.0.1 |
| azctx config users list | List all the users in the configuration | GA | 1.0 |

## azctx config users add-prompt

Adds a new user entry to the configuration using an interactive prompt. Users is asked for the configuration parameters based on the selected login type.

The name of the configuration file can be overridden by providing the optional parameter `--cliconfig`.

### Examples

Add a new user to the default configuration.

```cli
azctx config users add-prompt
```

Add a new user to the specified configuration.

```cli
azctx config users add-prompt --cliconfig demo
```

### Required Parameters

This command has no required parameters

### Optional parameters

`--cliconfig | -c`

The name of the configuration file to use. The default name is `config`.
