# Configuration

## Location

Create a default configuration file in the users profile folder under the subfolder `.azctx`.

It resolved to one of the following folders:

* `[%UserProfile%]\.azctx`
* `[%SystemDrive%]\Users\{username}\.azctx`

## Content

```json
{
  "Tenants": {},
  "Users": {},
  "Contexts": {},
  "CurrentContext": ""
}
```

## Fields

### Root

| Field | Description | Type |
|------|-------------|------|
| Tenants | Type | `Array` of [`tenant`](#tenant) items |
| Users | Type | `Array` of [`user`](#user) items |
| Contexts | Type | `Array` of [`context`](#context) items |
| CurrentContext | Type | `string` |

### Tenant

Tenant holds the information of the tenant the users wants to login to.

| Field | Description | Type |
| ------|-------------|------|
| Name | The name of the tenant. | `string` |
| TenantId | The Entra ID tenant, must provide when using service principals | `string` |

### User

User holds the information of the tenant the users wants to login to.

| Field | Description | Type |
| ------|-------------|------|
| DisplayName | The display name which is used  | `string` |
| LoginType | Defines the login type for this user. Can be either `Interactive`, `ServicePrincipal` or `UsernamePassword` | `string` |
| Username [Optional] | Used for the `-u` parameter when login type is `ServicePrincipal` or `UsernamePassword` | `string` |
| Password [Optional] | Used for the `-p` parameter when login type is `ServicePrincipal` or `UsernamePassword` | `string` |

### Context

This field creates a mapping between a user to a tenant and can be used to login based on the context name.

Multiple context items can be created allowing you to prepare different login combinations for a single tenant or user.

### CurrentContext (Experimental)

CurrentContext holds a reference to the latest context used by the user.

Note: This features is experimental as the user can login to another tenant, or with another user, using the Azure CLI. `azctx` is not aware of this change. This feature only works if `azctx` is used for all login changes.
