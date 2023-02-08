resource "azurerm_storage_account" "model_data_storage_account" {
  name                     = var.model_data_storage_account_name
  resource_group_name      = var.resource_group_name
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = var.model_data_storage_account_replication_type

  allow_nested_items_to_be_public = false

  tags = {
    Environment = var.env
    Description = "Managed by Terraform"
  }
}

resource "azurerm_storage_container" "model_data_storage_container" {
  name                  = var.model_data_storage_container_name
  storage_account_name  = azurerm_storage_account.model_data_storage_account.name
  container_access_type = "private"
}

resource "azurerm_storage_management_policy" "example" {
  storage_account_id = azurerm_storage_account.model_data_storage_account.id

  rule {
    name    = "Delete expire objects"
    enabled = true

    filters {
      blob_types = ["blockBlob"]
    }
    actions {
      base_blob {
        delete_after_days_since_modification_greater_than = 100
      }
    }
  }
}

resource "azurerm_role_assignment" "user_storage_account_role_assignment" {
  role_definition_name = "Storage Blob Data Contributor"
  scope                = azurerm_storage_account.model_data_storage_account.id
  principal_id         = data.azuread_user.current_object_id.object_id
}