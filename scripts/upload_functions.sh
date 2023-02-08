#!/bin/bash
# Upload the model data and telemetry data azure functions

usage () {
    echo "**Upload Azure Functions**"
    echo "NOTE: run 'az login' prior to executing this script"
    echo "Usage ./upload_functions.sh"
    echo "  -m <model_data_flow_function_app_name>"
    echo "  -s <streaming_data_flow_funtion_app_name>"
    echo "---Parameters---"
    echo "m=    : Model Data Flow Function App Name"
    echo "s=    : Streaming Data Flow Function App Name"
}

while getopts m:s: flag
do
    case "${flag}" in
        m) model_data_flow_function_app_name=${OPTARG};;
        s) streaming_data_flow_function_app_name=${OPTARG};;
        *) usage && exit 1;;
    esac
done

main () {
    # deploying model data flow
    cd ./src/AasFactory.Azure.Functions.ModelDataFlow
    func azure functionapp publish $model_data_flow_function_app_name

    cd ../AasFactory.Azure.Functions.StreamingDataFlow
    func azure functionapp publish $streaming_data_flow_function_app_name
}

if [ -z $model_data_flow_function_app_name ] || [ -z $streaming_data_flow_function_app_name ]; then
    usage && exit 1
else
    main
fi
