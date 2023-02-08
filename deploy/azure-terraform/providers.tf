terraform {
  required_version = ">=1.1.4"

  required_providers {
    azapi = {
      source  = "Azure/azapi"
      version = "1.0.0"
    }

    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">=3.24.0"
    }

    null = {
      source  = "hashicorp/null"
      version = "3.2.1"
    }
  }
}

provider "azapi" {
}

provider "azurerm" {
  features {
  }
  storage_use_azuread = true
}

provider "null" {}