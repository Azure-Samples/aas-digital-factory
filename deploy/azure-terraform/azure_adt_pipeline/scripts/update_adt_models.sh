# Update Adt Models:
# Download models from link and upload them to ADT instance.

usage () {
    echo "**Upload ADT Models**"
    echo "Usage: ./update_adt_models.sh \\"
    echo "  -n <adt_name> \\"
    echo "  -p <model_path> \\"
    echo "---Parameters---"
    echo "n=    :ADT Name"
    echo "p=    :Model Path"
}

while getopts n:p: flag
do
    case "${flag}" in
        n) adt_name=${OPTARG};;
        p) model_path=${OPTARG};;
        *) usage && exit 1;;
    esac
done

if [ -z $adt_name ] || [ -z $model_path ]; then
    usage
    exit 1
fi

az extension add --name azure-iot

# Delete all nodes from ADT, add new models.
az dt model delete-all --yes -n $adt_name
echo "\"az dt model delete\" exited with code $?"
az dt model create -n $adt_name --from-directory $model_path
echo "\"az dt model create\" exited with code $?"
