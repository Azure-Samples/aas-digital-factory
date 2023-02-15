#!/bin/bash
# Deploying terraform:
# Deploys the terraform models

usage () {
    echo "**Deploy terraform**"
    echo "Usage: ./deply_terraform.sh \\"
    echo "  -c <abbreviated_company_name> \\"
    echo "  -d <terraform_dir> \\"
    echo "  -l <location> \\"
    echo "  -p <prefix> \\"
    echo "---Parameters---"
    echo "c=    :The abbreviated company name"
    echo "d=    :The terraform directory"
    echo "l=    :The location of the resources"
    echo "p=    :The prefix for the resources"
}

while getopts c:d:l:p: flag
do
    case "${flag}" in
        c) abbreviated_company_name=${OPTARG};;
        d) terraform_dir=${OPTARG};;
        l) location=${OPTARG};;
        p) prefix=${OPTARG};;
        *) usage && exit 1;;
    esac
done

main() {
    terraform -chdir=$terraform_dir init
    terraform \
        -chdir=$terraform_dir apply \
        -var="abbreviated_company_name=$abbreviated_company_name" \
        -var="prefix=$prefix" \
        -var="location=$location" \
        -auto-approve
}

if [ -z $abbreviated_company_name ] || [ -z $location ] || [ -z $prefix ] || [ -z $terraform_dir ]; then
    usage && exit 1
else
    main
fi