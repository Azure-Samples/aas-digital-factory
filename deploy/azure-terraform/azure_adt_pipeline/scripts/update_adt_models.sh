# Update Adt Models:
# Download models from link and upload them to ADT instance.

usage () {
    echo "**Upload ADT Models**"
    echo "Usage: ./update_adt_models.sh \\"
    echo "  -d <adt_models_directory> \\"
    echo "  -g <resource_group> \\"
    echo "  -m <adt_model_link> \\"
    echo "  -n <adt_name> \\"
    echo "---Parameters---"
    echo "d=    :ADT Model directory"
    echo "g=    :Resource Group"
    echo "m=    :ADT model link"
    echo "n=    :ADT Name"
}

while getopts d:g:m:n:: flag
do
    case "${flag}" in
        d) adt_models_directory=${OPTARG};;
        g) resource_group=${OPTARG};;
        m) adt_models_link=${OPTARG};;
        n) adt_name=${OPTARG};;
        *) usage && exit 1;;
    esac
done

if [ -z $adt_name ] || [ -z $adt_models_link ] || [ -z $adt_models_directory ] || [ -z $resource_group ];  then
    usage
    exit 1
fi

echo "retrieving models for ${adt_name} from link ${adt_models_link}"
az extension add --name azure-iot

# Download model files from link and unzip in temporary directory.
mkdir adt_models
curl -L $adt_models_link -o adt_models/models.tar.gz
tar xzf adt_models/models.tar.gz --directory adt_models/
# Delete all nodes from ADT, add new models.
az dt model delete-all --yes -n $adt_name
echo "\"az dt model delete\" exited with code $?"
az dt model create -g $resource_group -n $adt_name --from-directory "adt_models/$adt_models_directory"
echo "\"az dt model create\" exited with code $?"
# Cleanup.
rm -v -R adt_models
