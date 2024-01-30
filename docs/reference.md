# CLI Reference

> [!IMPORTANT]  
> This reference is for release v1.0.0-beta.3.

## azctx

The `azctx` command is the main CLI command. It allows users to manage the configuration of the CLI and contains commands to switch between logins.

### Command status

There are three status types for commands. both commands and sub-commands have this indication.

- **GA** means the command is stable and can be used without
- **Preview** means that the command us stable but can have minor changes in upcoming releases.
- **Experimental** means the command is undergoing significant changes and can change signature in upcoming releases.

## Commands

| Name   | Description      | Status |
|----------|---------------|-------|
| [azctx config](commands/config.md) | Manage the configuration | Preview |
| azctx context | Manage the Azure CLI login contact | Preview |
