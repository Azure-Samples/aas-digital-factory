# Building Digital Twins using Industrie 4.0 AAS ontology

## Introduction

Digital Twins are virtual models of real-world entities, including places, business processes, and people, that can be created and managed using [Azure Digital Twins (ADT)](https://learn.microsoft.com/en-us/azure/digital-twins/).
Possibilities are immense when these models include real-time data with historization capabilities.
This post will walk you through a serverless solution that can be used to create digital twins for manufacturing data models, utilizing the [Asset Administration Shell (AAS) ontology](https://github.com/digitaltwinconsortium/ManufacturingOntologies/tree/main/Ontologies/AssetAdminShell).

The Industries 4.0 AAS ontology is an open standard for the exchange of information between partners in the manufacturing value chain.
It was specifically created to aid solution providers in speeding up the development of digital twin solutions for manufacturing applications, such as predictive maintenance, asset condition monitoring, OEE calculation, and simulation.
The authors of this [GitHub repository](https://github.com/digitaltwinconsortium/ManufacturingOntologies/tree/main/Ontologies/AssetAdminShell) have proposed and showcased how AAS can be used as a base for building a comprehensive ontology for Smart Production (Discrete Manufacturing, Process Manufacturing and Automotive Production).

### Why should we use the AAS ontology to represent Digital Twins?

1. As a standard, it offers a shared foundation for modeling factories using industry-standard concepts, which reduces the need for reinvention of custom models.
1. Flexibility in modeling a manufacturing facility using pre-defined [DTDL models](https://learn.microsoft.com/en-us/azure/digital-twins/concepts-models#digital-twin-definition-language-dtdl-for-models). Traditionally, what would become a DTDL model add/change followed by an update of a digital twin because of schema changes is now a simple twin update, since the model itself is fixed.
1. A powerful key concept of the AAS metamodel is that all relevant information is provided by submodels. Submodels can refer to other submodels. This enables a high degree of independence from the entire supply chain in building and operating digital twins.

### Sample Scenario

Imagine a real-life scenario of Contoso, a delivery company that uses robots to pick products from a warehouse and prepare them for shipping.
The company has two facilities located in Seattle and Boston, each with a line of two robots that are of the same type and share common parts.

To keep track of the performance of the robots, they collect a vast amount of telemetry data, including operational data and key performance indicators, and send this information to the cloud.
John, who works at Contoso, uses a dashboard to monitor the data and take action if there are any issues, such as downtime, to ensure timely delivery to customers.

Creating this connected system requires representing Contoso's current structure, including its two facilities, one line, two machines, and components, in a graph.

![Contoso's ontology](./assets/data-model-diagram-small.png)

Our goal is to represent Contoso's current structure as a digital twin graph. The process involves two main flows: model transformation and streaming data.
In the model data flow, the system will parse the representation of the factory graph and construct the digital twins.
The streaming data flow will update the digital twins accordingly as operational data or key performance indicators are received.
Additionally, our end users, John and his colleagues, should have access to historical data to identify patterns.

## Architecture

This solution is implemented using a serverless architecture.
It is responsible for (1) building the digital representation of a manufacturing factory in digital twins and (2) updating the machines' telemetry data near real-time for analysis;
these functionalities correspond to the model transformation and streaming data flows, respectively.

Both of these flows will be initiated through events in **Azure Event Hub**. Once initiated, each flow executes two sequential **Azure Functions**, one for AAS conversion and another for ADT conversion, which we'll cover later in this post. The model transformation functions depend on **Azure Blob Storage** as part of their workflows. **Azure Data Explorer** (ADX) is part of the solution of the streaming data flow to record the history of updates to the twins.

A detailed explanation of the architecture and the reasoning behind the choice of resources used can be found [in the sample project documentation](./architecture.md)

### Factory Inputs

The model transformation flow takes a JSON representation of a factory as its input.
The following snippet provides an example of this input and the data model that it needs to follow for a sample Contoso factory:

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
          "id": "robot1",
          "name": "robot1"
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

There are few things to call out here:

- Attributes that describe the factory object. For example, `id` represents the unique identifier, `name` represents the code name, `displayName` represents the friendly name and `placeName` represents the location.
- Attributes that define relationships to other factory objects. For example, the machine object with id `robot1` has a relationship to the factory object `contoso`.

A full Contoso data model representation, which includes lines, machines, machine types and concept description, can be found in the [model data from the sample project](../samples/model-data/Factory.json).

The streaming data flow only requires one data model to describe its input, which defines property updates for a particular machine.

Here is an example machine update as per Contoso data model represented as JSON:

```json
        {
          "header": {
            "machineId": "robot1",
            "modelType": "machineType"
          },
          "data": [
            {
                "id": "temperature",
                "name": "temperature",
                "dataType": "float64",
                "value": 12.34
            },
            {
                "id": "starttime",
                "name": "starttime",
                "dataType": "datetime",
                "value": "2023-02-02T18:00:00.000Z"
            },
            {
                "id": "endtime",
                "name": "endtime",
                "dataType": "datetime",
                "value": "2023-02-02T18:00:00.000Z"
            }
          ]
        }
```

We can observe the machine with id `robot1` sent one update for `temperature` property. The other properties `starttime` and `endtime` are required properties for historization purposes. Keep in mind that a machine update can include more than one property update.

Now let's try to understand the two processing flows in detail. Both flows share a common pattern of data processing: first, they each try to convert the factory-provided data into common AAS format data, and then write the data to Azure Digital Twins.

Let's first review how the AAS conversion is implemented.

### AAS Conversion

For AAS conversion, we use a list of mapping rules to translate the factory data to data based on the Asset Administration Shell (AAS) metamodel.
This step is really important as it helps with converting custom factory data to predefined standard AAS models.

Let's try to understand by starting with the sample code of the custom machine model, and see how it is converted into standard AAS components: Submodels and Reference Elements. First, we'll look at one of the mapping rules defined to transform custom models to standard AAS models. Let's use this sample code [file](https://github.com/Azure-Samples/aas-digital-factory/blob/main/src/AasFactory.Azure.Functions.ModelDataFlow/Services/AasConverters/MachineConverter.cs) as reference. This piece of code does the necessary conversion to standard models.

Here is an example AAS machine as per the Contoso data model, represented as JSON:

```json
{
   "Id":"aas_m_robot1",
   "IdShort":"robot1",
   "DisplayName":{
      "LangString":{
         "EN":"Robot 1"
      }
   },
   "SubModels":[
      {
         "Id":"aas_sm_m_robot1_od",
         "IdShort":5,
         "DisplayName":{
            "LangString":{
               "EN":"OperationalData"
            }
         },
         "Properties":[
            {
               "DisplayName":{
                  "LangString":{
                     "EN":"Temperature"
                  }
               },
               "Id":"aas_sme_m_robot1_od_temperature",
               "IdShort":"temperature",
               "ValueType":4,
               "Value":"",
               ...
            },
            ...
         ],
         "ReferenceElements":[
         ],
      }
   ]
},
```

When a new data model is onboarded, the mapping rules implementation may change based on the definition of the incoming data, but the underlying AAS standard model definitions to which they will be transformed should not have to change. In our sample project, POCO (Plain Old CLR Objects) classes have been defined for all standard AAS models.

Once we've transformed the input data to a standard format, this data is ready to be represented as digital twins. In the next section, we'll take a look at how this ADT conversion works.

### ADT Conversion

Below is the ADT representation of the Seattle Factory (1 line, 2 machines, 1 machine type).

![The ADT representation of the Seattle Factory (1 line, 2 machines, 1 machine type).](./assets/seattle-factory-adt-graph-small.png)

The AAS-format factory data is converted to DTDL-specific format to be able to create digital twins and relationships on ADT. These DTDL models are pre-defined in the [Asset Administration Shell (AAS) ontology](https://github.com/digitaltwinconsortium/ManufacturingOntologies/tree/main/Ontologies/AssetAdminShell).

Based on the ADT representation above, you can observe by colors that:

- Purple nodes are DTDL AAS shells such as factory, line, machine and machine type.
- Aqua nodes are DTDL Submodels. A DTDL AAS shell may contain many submodels. For example, the AAS Factory (Seattle's Factory) has three submodels to access information about its Nameplate, Machines, and Lines.
- Blue, yellow and red nodes are DTDL Properties (representing different data types), which are useful for property fields that receive telemetry updates. For example, the Submodel Operational Data (of each Robot) reports temperature data.

[This class](https://github.com/Azure-Samples/aas-digital-factory/blob/main/src/AasFactory.Azure.Models/Adt/Twins/Submodel.cs#L12) shows how we've defined the POCO class for the ADT twin representation for a Submodel.
In this way, we've built up POCO classes for all of the necessary AAS components, as well as the relationship between these elements in the graph hierarchy.
Now that the POCO classes for AAS twins and relationships are defined, you need to implement the logic to map the AAS-formatted representation into the ADT representation. For that, you need to traverse all your AAS factory data from top (Shell) to bottom (Properties). For example, create first the twin for DTDL AAS, then create twins of the DTDL submodels that are connected, and finally create the relationships between these DTDL AAS and Submodels using the Azure ADT SDK. This [code snippet](https://github.com/Azure-Samples/aas-digital-factory/blob/main/src/AasFactory.Azure.Functions.ModelDataFlow/Services/ShellRepository.cs#L27-L47) describes what we just talked about.

At this point, we've implemented both of logical components that our data processing pipeline called for and applied them to the model transformation and streaming data workflows.
With this solution, we're able to take JSON inputs of unique factory data models and telemetry updates and transform them, via the AAS ontology, to get an up-to-date digital twin graph that follows this standardized format.
Our colleagues at Contoso will be able to use this system to flexibly model and manage their existing manufacturing facilities, and also rest easy with the knowledge that this system can be easily extended to incorporate factory updates and new data as they continue to develop their digital manufacturing journey into the future.

## Summary

Asset Administration Shell (AAS) is a standard developed by the Industrial Internet Consortium (IIC) for describing the assets in an industrial system. Azure Digital Twins (ADT) is a platform that allows you to model, analyze, and monitor digital representations of physical environments, such as buildings, factories, and cities.

We can use the AAS standard as a metamodel to represent manufacturing assets of a factory, such as machines, equipment, and devices, in ADT. This provides a standardized way to completely describe these assets, which can help to simplify integration with other systems, without dealing with custom model definitions for each factory and asset.

In the proposed architecture presented in this sample solution, we have encapsulated the complexity of converting a factory data model to AAS
by adding a logical layer to handle the AAS requests on top of any ADT-level interactions.
You can check out the entire [sample repository in GitHub](https://github.com/Azure-Samples/aas-digital-factory) to explore the code and deployment scripts for the architecture we've presented in this post - we hope you find it a useful resource, and we welcome any comments/feedback you might have in this space.
