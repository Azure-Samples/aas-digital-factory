# Model Data Raw To AAS Conversion <!-- omit in toc -->

This document will outline the process of converting the raw model data to the AAS representation.

## Table of Contents <!-- omit in toc -->

- [Model Update Pipeline](#model-update-pipeline)
  - [Factory](#factory)
  - [Line](#line)
  - [Machine Type](#machine-type)
  - [Machine](#machine)
  - [Full JSON output to blob storage](#full-json-output-to-blob-storage)

## Model Update Pipeline

When new model data comes from the factory,
the first Azure Function will convert the data from the raw types (factory, line, machine types, and machine) to the corresponding AAS representation.
There are a few assumptions:

1. Each type (factory, line, machine type, machine) will be defined as an Asset administration shell
1. There are only two ways for shells to be connected
   1. Reference elements
   1. Derived from connection

A sample of the expected input to function 1 is provided [in this repo](../../samples/model-data/Factory.json).

### Factory

```json
"factory": [
    {
      "id": "contoso",
      "modelType": "factory",
      "name": "contoso",
      "displayName": "Contoso",
      "placeName": "Washington, USA",
      "timezone": "PST",
      "machines": [
        {
          "id": "grill1",
          "name": "grill1"
          },
          ...
      ],
      "lines": [
        {
          "id": "line1",
          "name": "line1"
        },
        ...
      ]
    }
]
```

The above model needs to be transformed into an AAS representation. The fields should map as follows:

```json
{
    "id": "<shell id>",
    "iri": "<shell iri>",
    "idShort": "{{ name }}",
    "referenceElementIds": null,
    "derivedFrom": null,
    "displayName": {
        "langString": {}
    },
    "description": {
        "langString": {}
    },
    "tags": {
        "markers": {},
        "values": {}
    },
    "administration": {
        "version": "1",
        "revision": "1"
    },
    "category": "",
    "subModels": [
        {
            "id": "<sub model id>",
            "iri": "<sub model iri>",
            "idShort": "Nameplate",
            "displayName": {
                "langString": {}
            },
            "kind": {
                "kind": "Instance"
            },
            "description": {
                "langString": {}
            },
            "tags": {
                "markers": {},
                "values": {}
            },
            "administration": {
                "version": "1",
                "revision": "1"
            },
            "category": "",
            "semanticIdValue": "",
            "properties": [
                {
                    "id": "<sub model element id>",
                    "idShort": "placename",
                    "valueType": "string",
                    "value": "{{ place_name }}",
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "kind": {
                        "kind": "Instance"
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {
                    "id": "<sub model element id>",
                    "idShort": "timezone",
                    "valueType": "string",
                    "value": "{{ timezone }}",
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                     "kind": {
                        "kind": "Instance"
                    },
                    "category": "",
                    "semanticIdValue": ""
                }
            ]
        },
        {
            "id": "<sub model id>",
            "iri": "<sub model iri>",
            "idShort": "machines",
            "kind": {
                "kind": "Instance"
            },
            "tags": {
                "markers": {},
                "values": {}
            },
            "description": {
                "langString": {}
            },
            "displayName": {
                "langString": {}
            },
            "administration": {
                "version": "1",
                "revision": "1"
            },
            "category": "",
            "semanticIdValue": "",
            "referenceElements": [
                {% for machine in machines %}
                {
                    "id": "<reference element id>",
                    "idShort": "{{ machine.name }} Ref",
                    "key1": {},
                    "key2": {},
                    "key3": {},
                    "key4": {},
                    "key5": {},
                    "key6": {},
                    "key7": {},
                    "key8": {},
                    "kind": {
                        "kind": "Instance"
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {% endfor %}
            ]
        },
        {
            "id": "<sub model id>",
            "iri": "<sub model iri>",
            "idShort": "lines",
            "kind": {
                "kind": "Instance"
            },
            "tags": {
                "markers": {},
                "values": {}
            },
            "description": {
                "langString": {}
            },
            "displayName": {
                "langString": {}
            },
            "administration": {
                "version": "1",
                "revision": "1"
            },
            "category": "",
            "semanticIdValue": "",
            "referenceElements": [
                {% for line in lines %}
                {
                    "id": "<reference element id>",
                    "idShort": "{{ line.name }} Ref",
                    "key1": {},
                    "key2": {},
                    "key3": {},
                    "key4": {},
                    "key5": {},
                    "key6": {},
                    "key7": {},
                    "key8": {},
                    "kind": {
                        "kind": "Instance"
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {% endfor %}
            ]
        },
    ],
    "assetInformation": {
        "assetKind": {
            "assetKind": "Instance"
        },
        "defaultThumbnailPath": "",
        "globalAssetIdValue": "",
        "specificAssetIdValue": ""
    }
  }
```

### Line

```json
"line": [
    {
      "id": "line1",
      "modelType": "line",
      "name": "line1",
      "displayName": "Line 1",
      "factory": {
        "id": "contoso",
        "name": "contoso"
        },
      "topology": [
        {
          "id": "grill1",
          "name": "grill1",
          "successors": [
            {
              "id": "oven1",
              "name": "oven1"
            }
          ]
        },
        {
          "id": "oven1",
          "name": "oven1",
          "successors": [
            {
              "id": "dishWasher1",
              "name": "dishwasher1"
            }
          ],
          "predecessors": [
            {
              "id": "grill1",
              "name": "grill1"
            }
          ]
        },
        ...
      ]
    }
  ],
```

The above model needs to be transformed into an AAS representation. The fields should map as follows:

```json
{
    "id": "<shell id>",
    "iri": "<shell iri>",
    "idShort": "{{ name }}",
    "referenceElementIds": ["<reference element id to factory>"], // connects the line reference element from the factory to the line shell
    "derivedFrom": null,
    "displayName": {
        "langString": {}
    },
    "description": {
        "langString": {}
    },
    "tags": {
        "markers": {},
        "values": {}
    },
    "administration": {
        "version": "1",
        "revision": "1"
    },
    "category": "",
    "subModels": [
        {
            "id": "<sub model id>",
            "iri": "<sub model iri>",
            "idShort": "machines",
            "kind": {
                "kind": "Instance"
            },
            "tags": {
                "markers": {},
                "values": {}
            },
            "description": {
                "langString": {}
            },
            "displayName": {
                "langString": {}
            },
            "administration": {
                "version": "1",
                "revision": "1"
            },
            "category": "",
            "semanticIdValue": "",
            "referenceElements": [
                {% for element in topology %}
                {
                    "id": "<reference element id>",
                    "idShort": "{{ element.name }} Ref",
                    "key1": {},
                    "key2": {},
                    "key3": {},
                    "key4": {},
                    "key5": {},
                    "key6": {},
                    "key7": {},
                    "key8": {},
                    "kind": {
                        "kind": "Instance"
                    },
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {% endfor %}
            ]
        }
    ],
    "assetInformation": {
        "assetKind": {
            "assetKind": "Instance"
        },
        "defaultThumbnailPath": "",
        "globalAssetIdValue": "",
        "specificAssetIdValue": ""
    }
}
```

### Machine Type

```json
"machineType": [
  {
    "id": "grill",
    "modelType": "machineType",
    "name": "grill",
    "displayName": "Grill",
    "fields": [
        {
            "id": "temperature",
            "name": "temperature",
            "displayName": "Temperature",
            "dataType": "float64",
            "statType": "continuous",
            "unit": "°C"
        },
        {
            "id": "starttime",
            "name": "starttime",
            "displayName": "Start Time",
            "dataType": "datetime"
        },
        {
            "id": "endtime",
            "name": "endtime",
            "displayName": "End Time",
            "dataType": "datetime"
        }
    ]
  },
  ...
]
```

The following model needs to be transformed into an AAS representation. The fields should map as follows:

```json
{
    "id": "<shell id>",
    "iri": "<shell iri>",
    "idShort": "{{ name }} type",
    "derivedFrom": null,
    "referenceElementIds": null,
    "displayName": {
        "langString": {}
    },
    "description": {
        "langString": {}
    },
    "tags": {
        "markers": {},
        "values": {}
    },
    "administration": {
        "version": "1",
        "revision": "1"
    },
    "category": "",
    "subModels": [
        {
            "id": "<sub model id>",
            "iri": "<sub model iri>",
            "idShort": "operationalData",
            "kind": {
                "kind": "Template"
            },
            "tags": {
                "markers": {},
                "values": {}
            },
            "description": {
                "langString": {}
            },
            "displayName": {
                "langString": {}
            },
            "administration": {
                "version": "1",
                "revision": "1"
            },
            "category": "",
            "semanticIdValue": "",
            "properties": [
                {
                    "id": "<sub model element id>",
                    "idShort": "duration",
                    "valueType": "int",
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "kind": {
                        "kind": "Template"
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {
                    "id": "<sub model element id>",
                    "idShort": "starttime",
                    "valueType": "dateTime",
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "kind": {
                        "kind": "Template"
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {
                    "id": "<sub model element id>",
                    "idShort": "endtime",
                    "valueType": "dateTime",
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "kind": {
                        "kind": "Template"
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {% for field in fields %}
                {
                    "id": "<sub model element id>",
                    "idShort": "{{ field.name }}",
                    "valueType": "{{ field.data_type }}",
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "kind": {
                        "kind": "Template"
                    },
                    "category": "",
                    "semanticIdValue": "", // populate if stat_type or unit for the field
                    "semanticIdReference": { // populate if stat_type or unit for the field
                        "id": "<reference id>",
                        "key1": {},
                        "key2": {},
                        "key3": {},
                        "key4": {},
                        "key5": {},
                        "key6": {},
                        "key7": {},
                        "key8": {},
                        "type": "",
                    }
                },
                {% endfor %}
            ]
        }
    ],
    "assetInformation": {
        "assetKind": {
            "assetKind": "Type"
        },
        "defaultThumbnailPath": "",
        "globalAssetIdValue": "",
        "specificAssetIdValue": ""
    }
}
```

Concept description elements which contain data specification objects will also have to be created from the machine type definition.
Creation of concept description element should only occur if `stat_type` and `unit` are not null.

```json
[
    {% for field in fields %}
    {
        "id": "<concept description id>",
        "idShort": "ConceptDescription",
        "referenceElementIds": ["<reference id to the properties reference element>", ...],
        "administration": {
            "version": "1",
            "revision": "1"
        },
        "description": {
            "langString": {}
        },
        "displayName": {
            "langString": {}
        },
        "tags": {
            "markers": {},
            "values": {}
        },
        "category": "",
        "dataSpecifications": [
            {
                "id": "<data specification id>",
                "idShort": "DataSpecification",
                "definition": {
                    "langString": {}
                },
                "preferredName": {
                    "langString": {}
                },
                "shortName": {
                    "langString": {}
                },
                "dataType": "<converted field.stat_type>",
                "levelType": "",
                "sourceOfDefinition": "",
                "symbol": "",
                "unit": "{{ field.unit }}",
                "unitIdValue": "",
                "value": "",
                "valueFormat": ""
            }
        ]
    },
    {% endfor %}
]
```

### Machine

```json
"machine": [
    {
      "id": "grill1",
      "modelType": "machine",
      "name": "grill1",
      "displayName": "Grill 1",
      "machineType": {
        "id": "grill",
        "name": "grill"
      },
      "factory": {
        "id": "contoso",
        "name": "contoso"
      },
      "lines": [
        {
          "id": "line1",
          "name": "line1"
        },
        ...
      ]
    },
    ...
]
```

The following model needs to be transformed into an AAS representation. The fields should map as follows:

```json
{
    "id": "<shell id>",
    "iri": "<shell iri>",
    "idShort": "{{ name }}",
    "derivedFrom": "{{ machine_type.name }}",
    "referenceElementIds": [
        "<reference element id from line to the machine for all lines>",
        "<reference element id from factory to the machine>"
    ],
    "description": {
        "langString": {}
    },
    "displayName": {
        "langString": {}
    },
    "tags": {
        "markers": {}
    },
    "administration": {
        "version": "1",
        "revision": "1"
    },
    "subModels": [
        {
            "id": "<sub model id>",
            "iri": "<sub model iri>",
            "idShort": "operationalData",
            "kind": {
                "kind": "Instance"
            },
            "tags": {
                "markers": {},
                "values": {}
            },
            "description": {
                "langString": {}
            },
            "displayName": {
                "langString": {}
            },
            "administration": {
                "version": "1",
                "revision": "1"
            },
            "category": "",
            "semanticIdValue": "",
            "properties": [
                {
                    "id": "<sub model element id>",
                    "idShort": "duration",
                    "valueType": "int",
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "kind": {
                        "kind": "Instance"
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {
                    "id": "<sub model element id>",
                    "idShort": "starttime",
                    "valueType": "dateTime",
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "kind": {
                        "kind": "Instance"
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {
                    "id": "<sub model element id>",
                    "idShort": "endtime",
                    "valueType": "dateTime",
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "kind": {
                        "kind": "Instance"
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {% for field in fields %}
                {
                    "id": "<sub model element id>",
                    "idShort": "{{ field.name }}",
                    "valueType": "{{ field.data_type }}", // need to convert to the types supported by adt
                    "description": {
                        "langString": {}
                    },
                    "displayName": {
                        "langString": {}
                    },
                    "tags": {
                        "markers": {},
                        "values": {}
                    },
                    "kind": {
                        "kind": "Instance"
                    },
                    "category": "",
                    "semanticIdValue": ""
                },
                {% endfor %}
            ]
        }
    ],
    "assetInformation": {
        "assetKind": {
            "assetKind": "Instance"
        },
        "defaultThumbnailPath": "",
        "globalAssetIdValue": "",
        "specificAssetIdValue": ""
    }
}
```

### Full JSON output to blob storage

```json
{
    "factories": [
        ...factories,
    ],
    "lines": [
        ...lines
    ],
    "machineTypes": [
        ...machineTypes
    ],
    "machines": [
        ...machines
    ],
    "conceptDescriptions": [
        ...conceptDescriptions
    ]
}
```
