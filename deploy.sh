#!/bin/bash
# Deploying terraform:
# Deploys the terraform models

usage () {
    echo "**Deploy terraform**"
    echo "Usage: ./deploy.sh \\"
    echo "  -c <abbreviated_company_name> \\"
    echo "  -e <action_group_email> \\"
    echo "  -l <location> \\"
    echo "  -p <prefix> \\"
    echo "---Parameters---"
    echo "c=    :The abbreviated company name"
    echo "e=    :The action group email"
    echo "l=    :The location of the resources"
    echo "p=    :The prefix for the resources"
}

while getopts c:e:l:p: flag
do
    case "${flag}" in
        b) terraform_backend_config_file=${OPTARG};;
        c) abbreviated_company_name=${OPTARG};;
        e) action_group_email=${OPTARG};;
        l) location=${OPTARG};;
        p) prefix=${OPTARG};;
        *) usage && exit 1;;
    esac
done

terraform_init_and_apply () {
    echo "**terraform init and apply**"

    local abbreviated_company_name=$1
    local action_group_email=$2
    local prefix=$3
    local terraform_dir=$4
    local location=$5

    ./scripts/terraform_init_and_apply.sh \
        -c $abbreviated_company_name \
        -d $terraform_dir \
        -e $action_group_email \
        -l $location \
        -p $prefix
}

upload_functions () {
    echo "**uploading functions**"

    local model_data_function_app_name=$1
    local telemetry_data_function_app_name=$2

    ./scripts/upload_functions.sh \
        -m $model_data_function_app_name \
        -s $telemetry_data_function_app_name
}

main () {
    # init and applying the terraform
    terraform_dir=./deploy/azure-terraform
    terraform_init_and_apply $abbreviated_company_name $action_group_email $prefix $terraform_dir $location
    
    model_data_function_app_name="$(terraform -chdir=$terraform_dir output -raw model_data_function_app_name)"
    telemetry_data_function_app_name="$(terraform -chdir=$terraform_dir output -raw telemetry_data_function_app_name)"

    upload_functions $model_data_function_app_name $telemetry_data_function_app_name

    model_data_storage_account_name="$(terraform -chdir=$terraform_dir output -raw model_data_storage_account_name)"
    model_data_storage_container_name="$(terraform -chdir=$terraform_dir output -raw model_data_storage_container_name)"
    az storage blob upload \
        --account-name $model_data_storage_account_name \
        --container-name $model_data_storage_container_name \
        --file ./samples/model-data/Factory.json \
        --name Factory.json \
        --overwrite true \
        --auth-mode login
}

if [ -z $abbreviated_company_name ] || [ -z $location ] \
    [ -z $action_group_email ] || [ -z $prefix ]; then
    usage && exit 1
else
    main
fi