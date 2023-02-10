# AAS Naming Convention <!-- omit in toc -->

## Table of Content <!-- omit in toc -->

- [AAS Properties with Ids](#aas-properties-with-ids)
  - [Asset Administration Shell](#asset-administration-shell)
    - [Example](#example)
  - [Reference Element](#reference-element)
    - [Example](#example-1)
  - [Reference](#reference)
    - [Example](#example-2)
  - [Sub Model](#sub-model)
    - [Example](#example-3)
  - [Sub Model Element](#sub-model-element)
    - [Example](#example-4)
  - [Sub Model Element Collection](#sub-model-element-collection)
    - [Example](#example-5)
  - [Sub Model Element List](#sub-model-element-list)
    - [Example](#example-6)
  - [Concept Description](#concept-description)
    - [Example](#example-7)
  - [Data Specification](#data-specification)
    - [Example](#example-8)
- [IRI Naming](#iri-naming)
  - [AAS Twin IRI](#aas-twin-iri)
  - [Submodel Twin IRI](#submodel-twin-iri)
- [Appendix](#appendix)
  - [Sub Model Abbreviations](#sub-model-abbreviations)

## AAS Properties with Ids

This document will outline the naming convention that we will use to define different AAS properties.

### Asset Administration Shell

There are several types that an Asset Administration Shell could be.
These types with their abbreviation are:

- Factory: f
- Line: l
- Machine: m
- Machine Type: mt

The name convention for an Asset Administration Shell should adhere to the following.

`aas_{TypeAbbreviated}_{ModelInstanceId}`

Where:

- aas = AAS
- TypeAbbreviated = (f, l, m, mt)
- ModelInstanceId = The id of the model instance.

#### Example

`aas_f_factory1`

### Reference Element

The name convention for a Reference Element should adhere to one of the following:

`aas_re_{TargetShellTypeAbbreviated}_{TargetModelInstanceId}`

Where:

- aas = AAS
- re = Reference Element
- TargetShellTypeAbbreviated = (f, l, m, mt). For reference elmements, `mt` is never used.
- TargetModelInstanceId = The id of the target model instance.

#### Example

`aas_re_m_oven1`

### Reference

The name convention for a Reference should adhere to the following:

`aas_r_{ReferenceDtId}`

Where:

- aas = AAS
- r = Reference
- ReferenceDtId = The id of the digital twin referenced

#### Example

`aas_r_sm_sme_mt_oven_cy_starttime`

### Sub Model

The name convention for a Sub Model should adhere to the following:

`aas_sm_{TypeAbbreviated}_{ModelInstanceId}_{SmName}`

Where:

- aas = AAS
- sm = Sub Model
- TypeAbbreviated = (f, l, m, mt)
- ModelInstanceId = The id of the model instance.
- SmName = The abbreviated name of the sub model (from the list of [sub models](#sub-model-abbreviations))

#### Example

`aas_sm_mt_oven_kpi`

### Sub Model Element

The name convention for a Sub Model Element should adhere to the following:

`aas_sme_{TypeAbbreviated}_{ModelInstanceId}_{SmName}_{FieldId}`

Where:

- aas = AAS
- sme = Sub Model Element
- TypeAbbreviated = (f, l, m, mt)
- ModelInstanceId = The id of the model instance.
- SmName = The abbreviated name of the sub model (from the list of [sub models](#sub-model-abbreviations))
- FieldId (MachineType/Machine) = The id of the machine type/machine instance field.

#### Example

`aas_sme_mt_oven_cy_starttime`

### Sub Model Element Collection

The name convention for a Sub Model Element Collection should adhere to one of the following:

`aas_smec_{TypeAbbreviated}_{ModelInstanceId}_{SmName}`

or

`aas_smec_{TypeAbbreviated}_{ModelInstanceId}_{SmName}_{MachineId}`

Where:

- aas = AAS
- smec = Sub Model Element Collection
- TypeAbbreviated = (f, l, m, mt)
- ModelInstanceId = The id of the model instance.
- SmName = The abbreviated name of the sub model (from the list of [sub models](#sub-model-abbreviations))
- MachineId = The id of the machine under in the sub model element collection (this should only exist under the process flow sub model element collection)

#### Example

`aas_smec_l_line1_pf_machine1`

### Sub Model Element List

The name convention for a Sub Model Element List should adhere to the following:

`aas_smel_{TypeAbbreviated}_{ModelInstanceId}_{SmName}_{MachineId}_{ListId}`

Where:

- aas = AAS
- sme = Sub Model Element
- TypeAbbreviated = (f, l, m, mt). For this dtid, mt is never used.
- ModelInstanceId = The id of the model instance.
- SmName = The abbreviated name of the sub model (from the list of [sub models](#sub-model-abbreviations))
- MachineId = The id of the machine under the model instance id.
- ListId = The list id. This can be a field id or an abbreviation to represent the list content
  - For process flow, ListId will either be p or s (where p = predecessors and s = successors)

#### Example

`aas_smel_l_line1_pf_machine1_p`

### Concept Description

The name convention for a Concept Description should adhere to the following:

`aas_cd_{TypeAbbreviated}_{ModelInstanceId}_{SmName}_{FieldId}`

Where:

- aas = AAS
- cd = Concept Description
- TypeAbbreviated = (f, l, m, mt)
- ModelInstanceId = The id of the model instance.
  (If this is the content description for a machine type the shell id should always be the machine type id)
- SmName = The abbreviated name of the sub model (from the list of [sub models](#sub-model-abbreviations))
- FieldId (MachineType/Machine) = The id of the machine type/machine instance field.

**Note:** If the concept description will be created from the machine type field array, we will use `mt` as the `TypeAbbreviated`.

#### Example

`aas_cd_mt_oven_cy_starttime`

### Data Specification

The name convention for a Data Specification should adhere to the following:

`aas_ds_{TypeAbbreviated}_{ModelInstanceId}_{SmName}_{FieldId}`

Where:

- aas = AAS
- ds = Data Specification
- TypeAbbreviated = (f, l, m, mt)
- ModelInstanceId = The id of the model instance.
  (If this is the content description for a machine type the shell id should always be the machine type id)
- SmName = The abbreviated name of the sub model (from the list of [sub models](#sub-model-abbreviations))
- FieldId (MachineType/Machine) = The id of the machine type/machine instance field.

**Note:** The data specification `TypeAbbreviated` should be the same as the concept description's `TypeAbbreviated`.

#### Example

`aas_ds_mt_oven_cy_starttime`

## IRI Naming

While the Id will be used as the dtid (Digital Twin Id) when writing to Azure Digital Twins, the IRI will be used as the id for AAS and sub model twins.
The naming convension will follow

### AAS Twin IRI

`https://aasfactory.com/aas/{CompanyName}/aas/{AssetKind}/{Id}`

Where:

- CompanyName = the abbreviated name of the company (We will need this in the initial payload from SM or this could be another identifier for the company)
- AssetKind = instance or type
- Id = The dtid of the element (factory name, line name, machine name, etc.)

### Submodel Twin IRI

`https://aasfactory.com/aas/{CompanyName}/sm/{AssetKind}/{Id}`

Where:

- CompanyName = the abbreviated name of the company (We will need this in the initial payload from SM or this could be another identifier for the company)
- AssetKind = instance or type
- Id = the dtid of the sub model

## Appendix

### Sub Model Abbreviations

- cycles => cy
- defects => df
- documentation => doc
- downtimes => dt
- kpi => kpi
- lines => l
- machines => m
- nameplate => np
- operational data => od
- process flow => pf
- technical data => td
